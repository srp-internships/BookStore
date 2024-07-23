using CatalogService.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.UseCases
{
    public class DeleteAuthorCommandHandler(
        IAuthorRepository authorRepository) : IRequestHandler<DeleteAuthorCommand>
    {
        private readonly IAuthorRepository _authorRepository = authorRepository;

        public async Task Handle(DeleteAuthorCommand request, CancellationToken token)
        {
            await _authorRepository.DeleteAsync(request.Id, token);
        }
    }
}
