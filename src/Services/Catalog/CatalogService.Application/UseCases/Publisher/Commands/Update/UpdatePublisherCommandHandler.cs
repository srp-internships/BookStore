using AutoMapper;
using CatalogService.Domain.Entities;
using CatalogService.Application.Exceptions;
using CatalogService.Application.Interfaces.BlobServices;
using CatalogService.Application.Interfaces.Repositories;
using CatalogService.Application.Interfaces.UnitOfWork;
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
        IUnitOfWork unitOfWork,
        IBlobService blobService) : IRequestHandler<UpdatePublisherCommand>
    {
        private readonly IPublisherRepository _publisherRepository = publisherRepository;
        private readonly IValidator<UpdatePublisherCommand> _validator = validator;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IBlobService _blobService = blobService;

        public async Task Handle(UpdatePublisherCommand request, CancellationToken token)
        {
            await _validator.ValidateAndThrowAsync(request, token);

            if (request.Logo == null)
                throw new NoFileUploaded();

            var uri = await _blobService.UploadAsync(request.Logo.Content, request.Logo.ContentType, token);

            var publisher = await _publisherRepository.GetByIdAsync(request.Id, token);
            publisher.Email = request.Email;
            publisher.Name = request.Name;
            publisher.Address = request.Address;
            publisher.Logo = uri;
            await _unitOfWork.SaveChangesAsync();

        }
    }
}
