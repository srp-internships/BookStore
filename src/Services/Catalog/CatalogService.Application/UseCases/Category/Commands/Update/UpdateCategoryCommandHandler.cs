using CatalogService.Application.Exceptions;
using CatalogService.Application.Interfaces.Repositories;
using CatalogService.Application.Interfaces.UnitOfWork;
using CatalogService.Domain.Entities;
using FluentValidation;
using MediatR;

namespace CatalogService.Application.UseCases
{
    public class UpdateCategoryCommandHandler(
        ICategoryRepository categoryRepository,
        IValidator<UpdateCategoryCommand> validator,
        IUnitOfWork unitOfWork) : IRequestHandler<UpdateCategoryCommand>
    {
        private readonly ICategoryRepository _categoryRepository = categoryRepository;
        private readonly IValidator<UpdateCategoryCommand> _validator = validator;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task Handle(UpdateCategoryCommand request, CancellationToken token)
        {
            // TODO same
            await _validator.ValidateAndThrowAsync(request, token);
            var category = await _categoryRepository.GetByIdAsync(request.Id, token);
            if (category == null)
            {
                throw new NotFoundException(nameof(Category));
            }

            category.Name = request.Name;
            category.Description = request.Description;
            await _unitOfWork.SaveChangesAsync(token);
        }
    }
}
