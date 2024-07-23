using AutoMapper;
using CatalogService.Application.Dto;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Interfaces;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.UseCases
{
    public class CreatePublisherCommandHandler(
        IPublisherRepository publisherRepository,
        IValidator<CreatePublisherCommand> validator,
        IMapper mapper) : IRequestHandler<CreatePublisherCommand, Guid>
    {
        private readonly IPublisherRepository _publisherRepository = publisherRepository;
        private readonly IValidator<CreatePublisherCommand> _validator = validator;
        private readonly IMapper _mapper = mapper;

        public async Task<Guid> Handle(CreatePublisherCommand request, CancellationToken token)
        {
            await _validator.ValidateAndThrowAsync(request, token);
            var publisher = _mapper.Map<Publisher>(request);
            return await _publisherRepository.CreateAsync(publisher, token);
        }
    }
}
