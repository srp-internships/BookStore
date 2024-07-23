using AutoMapper;
using CatalogService.Application.UseCases;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Interfaces;
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
        IMapper mapper) : IRequestHandler<UpdateCategoryCommand>
    {
        private readonly ICategoryRepository _categoryRepository = categoryRepository;
        private readonly IValidator<UpdateCategoryCommand> _validator = validator;
        private readonly IMapper _mapper = mapper;

        public async Task Handle(UpdateCategoryCommand request, CancellationToken token)
        {
            await _validator.ValidateAndThrowAsync(request, token);
            var category = _mapper.Map<Category>(request);
            await _categoryRepository.UpdateAsync(request.Id, category, token);
        }
    }
}
