using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using OrderService.Application.Common.Interfaces.Repositories;
using OrderService.Domain.Entities;

namespace OrderService.Infrastructure.Persistence.Repositories
{
    public class CachedGetOrdersByCustomerRepository : IGetOrdersByCustomerRepository
    {
        private readonly IGetOrdersByCustomerRepository _ordersByCustomerRepository;
        private readonly IDistributedCache _distributedCache;

        public CachedGetOrdersByCustomerRepository(IGetOrdersByCustomerRepository ordersByCustomerRepository, IDistributedCache distributedCache)
        {
            _ordersByCustomerRepository = ordersByCustomerRepository;
            _distributedCache = distributedCache;
        }

        public Task<Order> CreateAsync(Order entity, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Order entity, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public Task<Order?> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Order>> GetOrdersByCustomerAsync(Guid customerId, CancellationToken cancellationToken = default)
        {
            var cacheKey = $"CustomerOrders_{customerId}";
            var cachedData = await _distributedCache.GetStringAsync(cacheKey, cancellationToken);

            if (cachedData != null)
            {
                return JsonConvert.DeserializeObject<List<Order>>(cachedData, GetJsonSerializerSettings()) ?? new List<Order>();
            }

            var orders = await _ordersByCustomerRepository.GetOrdersByCustomerAsync(customerId, cancellationToken);

            var cacheEntryOptions = new DistributedCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromDays(1));

            var serializedData = JsonConvert.SerializeObject(orders, GetJsonSerializerSettings());
            await _distributedCache.SetStringAsync(cacheKey, serializedData, cacheEntryOptions, cancellationToken);

            return orders;
        }

        public Task<Order> UpdateAsync(Order entity, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        private JsonSerializerSettings GetJsonSerializerSettings()
        {
            return new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            };
        }
    }
}
