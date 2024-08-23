using AutoMapper;
using CatalogService.Application.Exceptions;
using CatalogService.Application.Interfaces.BlobServices;
using CatalogService.Application.Interfaces.Repositories;
using CatalogService.Application.Interfaces.UnitOfWork;
using CatalogService.Domain.Entities;
using FluentValidation;
using MediatR;

namespace CatalogService.Application.UseCases
{
    public class CreatePublisherCommandHandler(
        IPublisherRepository publisherRepository,
        IValidator<CreatePublisherCommand> validator,
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IBlobService blobService) : IRequestHandler<CreatePublisherCommand, Guid>
    {
        private readonly IPublisherRepository _publisherRepository = publisherRepository;
        private readonly IValidator<CreatePublisherCommand> _validator = validator;
        private readonly IMapper _mapper = mapper;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IBlobService _blobService = blobService;

        public async Task<Guid> Handle(CreatePublisherCommand request, CancellationToken token)
        {
            // TODO same
            await _validator.ValidateAndThrowAsync(request, token);

            if (request.Logo == null)
                throw new NoFileUploaded();

            var uri = await _blobService.UploadAsync(request.Logo.Content, request.Logo.ContentType, token);

            var publisher = _mapper.Map<Publisher>(request);
            publisher.Logo = uri;
            var id = await _publisherRepository.CreateAsync(publisher, token);
            await _unitOfWork.SaveChangesAsync();
            return id;
        }
    }
}
