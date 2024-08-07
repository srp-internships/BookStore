using AutoMapper;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Interfaces;
using CatalogService.Infostructure;
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
        IMapper mapper,
        IUnitOfWork unitOfWork) : IRequestHandler<CreatePublisherCommand, Guid>
    {
        private readonly IPublisherRepository _publisherRepository = publisherRepository;
        private readonly IValidator<CreatePublisherCommand> _validator = validator;
        private readonly IMapper _mapper = mapper;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Guid> Handle(CreatePublisherCommand request, CancellationToken token)
        {
            await _validator.ValidateAndThrowAsync(request, token);
            var publisher = _mapper.Map<Publisher>(request);
            var id = await _publisherRepository.CreateAsync(publisher, token);
            await _unitOfWork.SaveChangesAsync();
            return id;
        }
    }
}
