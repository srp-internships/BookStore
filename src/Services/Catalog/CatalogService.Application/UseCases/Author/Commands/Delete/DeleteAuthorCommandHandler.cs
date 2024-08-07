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
    public class DeleteAuthorCommandHandler(
        IAuthorRepository authorRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<DeleteAuthorCommand>
    {
        private readonly IAuthorRepository _authorRepository = authorRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task Handle(DeleteAuthorCommand request, CancellationToken token)
        {
            await _authorRepository.DeleteAsync(request.Id, token);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
