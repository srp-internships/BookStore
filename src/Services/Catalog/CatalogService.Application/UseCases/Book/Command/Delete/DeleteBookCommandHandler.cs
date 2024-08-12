using CatalogService.Application.Interfaces.BlobServices;
using CatalogService.Application.Interfaces.Repositories;
using CatalogService.Application.Interfaces.UnitOfWork;
using CatalogService.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatalogService.Domain.Entities;

namespace CatalogService.Application.UseCases
{
    public class DeleteBookCommandHandler(
        IBookRepository bookRepository,
        IUnitOfWork unitOfWork,
        IBlobService blobService) : IRequestHandler<DeleteBookCommand>
    {
        private readonly IBookRepository _bookRepository = bookRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IBlobService _blobService = blobService;

        public async Task Handle(DeleteBookCommand request, CancellationToken token)
        {
            var book = await _bookRepository.GetByIdAsync(request.Id, token);
            if (book == null)
            {
                throw new NotFoundException(nameof(Book));
            }
            await _blobService.DeleteAsync(book.Image);
            await _bookRepository.DeleteAsync(request.Id, token);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
