using AutoMapper;
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
            var publishers = _publisherRepository.GetAllAsync(token);
            var publishersVm = _mapper.Map<PublisherListVm>(publishers);
            return publishersVm;
        }
    }
}
