using CatalogService.Application.Exceptions;
using CatalogService.Application.Interfaces.Repositories;
using CatalogService.Application.Interfaces.UnitOfWork;
using CatalogService.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.UseCases
{
    public class DeleteAuthorCommandHandler(
        IAuthorRepository authorRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<DeleteAuthorCommand>
    {
        private readonly IAuthorRepository _authorRepository = authorRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task Handle(DeleteAuthorCommand request, CancellationToken token)
        {
            var existingAuthor = await _authorRepository.AnyAsync(request.Id, token);
            if (!existingAuthor)
            {
                throw new NotFoundException(nameof(Author));
            }

            await _authorRepository.DeleteAsync(request.Id, token);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
