using AutoMapper;
using CatalogService.Application.UseCases;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Interfaces;
using CatalogService.Infostructure;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            await _validator.ValidateAndThrowAsync(request, token);
            var category = await _categoryRepository.GetByIdAsync(request.Id, token);
            category.Name = request.Name;
            category.Description = request.Description;
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
