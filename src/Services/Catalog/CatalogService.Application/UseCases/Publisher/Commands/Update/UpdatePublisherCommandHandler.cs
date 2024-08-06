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
    public class UpdatePublisherCommandHandler(
        IPublisherRepository publisherRepository,
        IValidator<UpdatePublisherCommand> validator,
        IUnitOfWork unitOfWork) : IRequestHandler<UpdatePublisherCommand>
    {
        private readonly IPublisherRepository _publisherRepository = publisherRepository;
        private readonly IValidator<UpdatePublisherCommand> _validator = validator;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task Handle(UpdatePublisherCommand request, CancellationToken token)
        {
            await _validator.ValidateAndThrowAsync(request, token);
            var publisher = await _publisherRepository.GetByIdAsync(request.Id, token);
            publisher.Email = request.Email;
            publisher.Name = request.Name;
            publisher.Address = request.Address;
            publisher.Logo = request.Logo;
            await _unitOfWork.SaveChangesAsync();

        }
    }
}
