using CatalogService.Application.Interfaces.Repositories;
using CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Infostructure.Repositories
{
    public class PublisherRepository(
        CatalogDbContext dbContext) : IPublisherRepository
    {
        private readonly CatalogDbContext _dbcontext = dbContext;

        public async Task<Guid> CreateAsync(Publisher publisher, CancellationToken token = default)
        {
            var existingPublisher = await _dbcontext.Publishers.FirstOrDefaultAsync(x => x.Name.Equals(publisher.Name), token);

            if (existingPublisher != null)
            {
                return existingPublisher.Id;
            }
            await _dbcontext.Publishers.AddAsync(publisher, token);
            return publisher.Id;
        }

        public Task<Publisher?> GetByIdAsync(Guid id, CancellationToken token = default)
        {
            return _dbcontext.Publishers.FirstOrDefaultAsync(x => x.Id.Equals(id), token); ;
        }

        public Task<List<Publisher>> GetAllAsync(CancellationToken token)
        {
            return _dbcontext.Publishers.ToListAsync(token);
        }

        public async Task UpdateAsync(Publisher publisher, CancellationToken token = default)
        {
            await _dbcontext.Publishers
               .Where(u => u.Id.Equals(publisher.Id))
               .ExecuteUpdateAsync(update => update
                    .SetProperty(u => u.Name, publisher.Name)
                    .SetProperty(u => u.Address, publisher.Address)
                    .SetProperty(u => u.Email, publisher.Email)
                    .SetProperty(u => u.Logo, publisher.Logo), token);
        }
        public async Task DeleteAsync(Guid id, CancellationToken token = default)
        {
            await _dbcontext.Publishers.Where(p => p.Id.Equals(id)).ExecuteDeleteAsync(token);
        }
    }
}
