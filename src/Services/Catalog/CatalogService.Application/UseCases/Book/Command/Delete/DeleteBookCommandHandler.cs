
using CatalogService.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.UseCases
{
    public class DeleteBookCommandHandler(IBookRepository bookRepository) : IRequestHandler<DeleteBookCommand>
    {
        private readonly IBookRepository _bookRepository = bookRepository;

        public async Task Handle(DeleteBookCommand request, CancellationToken token)
        {
            await _bookRepository.DeleteAsync(request.Id, token);
        }
    }
}
