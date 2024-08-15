using CatalogService.Application.Exceptions;
using CatalogService.Application.Interfaces.Repositories;
using CatalogService.Application.Interfaces.UnitOfWork;
using CatalogService.Domain.Entities;
using MediatR;

namespace CatalogService.Application.UseCases
{
    public class DeleteCategoryCommandHandler(
        ICategoryRepository categoryRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<DeleteCategoryCommand>
    {
        private readonly ICategoryRepository _categoryRepository = categoryRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task Handle(DeleteCategoryCommand request, CancellationToken token)
        {
            var existingCategory = await _categoryRepository.AnyAsync(request.Id, token);
            if (!existingCategory)
            {
                throw new NotFoundException(nameof(Category));
            }

            await _categoryRepository.DeleteAsync(request.Id, token);
            await _unitOfWork.SaveChangesAsync(token);
        }
    }
}
