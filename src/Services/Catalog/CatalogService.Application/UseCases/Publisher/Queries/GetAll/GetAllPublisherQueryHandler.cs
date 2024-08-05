using AutoMapper;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Exceptions;
using CatalogService.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.UseCases
{
    public class GetAllPublisherQueryHandler(
        IPublisherRepository publisherRepository,
        IMapper mapper) : IRequestHandler<GetAllPublisherQuery, PublisherListVm>
    {
        private readonly IPublisherRepository _publisherRepository = publisherRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<PublisherListVm> Handle(GetAllPublisherQuery request, CancellationToken token)
        {
            IEnumerable<Publisher> publishers;
            try
            {
                publishers = await _publisherRepository.GetAllAsync(token);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred: {ex.Message}");
                throw;
            }

            if (publishers.Count() == 0)
            {
                throw new NotFoundException(nameof(Publisher));
            }
            
            var publisherDtos = _mapper.Map<IEnumerable<PublisherDto>>(publishers);
            return new PublisherListVm { Publishers = publisherDtos.ToList() };

        }
    }
}
