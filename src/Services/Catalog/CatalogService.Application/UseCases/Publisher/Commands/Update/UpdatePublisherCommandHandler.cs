using AutoMapper;
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
    public class UpdatePublisherCommandHandler(
        IPublisherRepository publisherRepository,
        IValidator<UpdatePublisherCommand> validator,
        IMapper mapper) : IRequestHandler<UpdatePublisherCommand>
    {
        private readonly IPublisherRepository _publisherRepository = publisherRepository;
        private readonly IValidator<UpdatePublisherCommand> _validator = validator;
        private readonly IMapper _mapper = mapper;

        public async Task Handle(UpdatePublisherCommand request, CancellationToken token)
        {
            await _validator.ValidateAndThrowAsync(request, token);
            var publisher = _mapper.Map<Publisher>(request);
            await _publisherRepository.UpdateAsync(request.Id, publisher, token);

        }
    }
}
