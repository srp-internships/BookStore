using AutoMapper;
using CatalogService.Application.Interfaces.Repositories;
using CatalogService.Domain.Entities;
using MediatR;

namespace CatalogService.Application.UseCases
{
    public class GetAllPublisherQueryHandler(
        IPublisherRepository publisherRepository,
        IMapper mapper) : IRequestHandler<GetAllPublisherQuery, List<PublisherDto>>
    {
        private readonly IPublisherRepository _publisherRepository = publisherRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<List<PublisherDto>> Handle(GetAllPublisherQuery request, CancellationToken token)
        {
            List<Publisher> publishers;
            try
            {
                publishers = await _publisherRepository.GetAllAsync(token);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred: {ex.Message}");
                throw;
            }

            if (publishers.Count == 0)
            {
                return [];
            }

            var publisherDtos = _mapper.Map<IEnumerable<PublisherDto>>(publishers);
            return publisherDtos.ToList();

        }
    }
}
