using CatalogService.Domain.Entities;
using CatalogService.Application.Exceptions;
using CatalogService.Application.Interfaces.BlobServices;
using CatalogService.Application.Interfaces.Repositories;
using CatalogService.Application.Interfaces.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.UseCases
{
    public class DeletePublisherCommandHandler(
        IPublisherRepository publisherRepository,
        IBlobService blobService,
        IUnitOfWork unitOfWork) : IRequestHandler<DeletePublisherCommand>
    {
        private readonly IPublisherRepository _publisherRepository = publisherRepository;
        private readonly IBlobService _blobService = blobService;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task Handle(DeletePublisherCommand request, CancellationToken token)
        {
            var publisher = await _publisherRepository.GetByIdAsync(request.Id, token);
            if (publisher == null)
            {
                throw new NotFoundException(nameof(Publisher));
            }

            await _blobService.DeleteAsync(publisher.Logo, token);
            await _publisherRepository.DeleteAsync(request.Id, token);
            await _unitOfWork.SaveChangesAsync(token);
        }
    }
}
