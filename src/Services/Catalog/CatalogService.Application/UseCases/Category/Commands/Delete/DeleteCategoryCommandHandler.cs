using CatalogService.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.UseCases
{
    public class DeleteCategoryCommandHandler(
        ICategoryRepository categoryRepository) : IRequestHandler<DeleteCategoryCommand>
    {
        private readonly ICategoryRepository _categoryRepository = categoryRepository;

        public async Task Handle(DeleteCategoryCommand request, CancellationToken token)
        {
            await _categoryRepository.DeleteAsync(request.Id, token);
        }
    }
}
