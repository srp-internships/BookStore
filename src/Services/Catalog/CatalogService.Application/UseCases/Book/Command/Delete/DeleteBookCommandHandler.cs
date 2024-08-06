
using CatalogService.Domain.Interfaces;
using CatalogService.Infostructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.UseCases
{
    public class DeleteBookCommandHandler(
        IBookRepository bookRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<DeleteBookCommand>
    {
        private readonly IBookRepository _bookRepository = bookRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task Handle(DeleteBookCommand request, CancellationToken token)
        {
            await _bookRepository.DeleteAsync(request.Id, token);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
