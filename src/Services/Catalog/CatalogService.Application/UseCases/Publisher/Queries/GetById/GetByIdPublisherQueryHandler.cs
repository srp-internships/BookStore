using AutoMapper;
using CatalogService.Application.Dto;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.UseCases
{
    public class GetByIdPublisherQueryHandler(
        IPublisherRepository publisherRepository,
        IMapper mapper) : IRequestHandler<GetByIdPublisherQuery, PublisherDto>
    {
        private readonly IMapper _mapper = mapper;
        private readonly IPublisherRepository _publisherRepository = publisherRepository;

        public async Task<PublisherDto> Handle(GetByIdPublisherQuery request, CancellationToken token)
        {
            Publisher publisher = await _publisherRepository.GetByIdAsync(request.Id, token);

            return _mapper.Map<PublisherDto>(publisher);
        }
    }
}
