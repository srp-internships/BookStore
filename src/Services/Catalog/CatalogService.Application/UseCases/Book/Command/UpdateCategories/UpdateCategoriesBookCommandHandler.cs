using AutoMapper;
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
    public class UpdateCategoriesBookCommandHandler(
        IBookRepository bookRepository,
        ICategoryRepository categoryRepository,
        IValidator<UpdateCategoriesBookCommand> validator) : IRequestHandler<UpdateCategoriesBookCommand>
    {
        private readonly IBookRepository _bookRepository = bookRepository;
        private readonly ICategoryRepository _categoryRepository = categoryRepository;
        private readonly IValidator<UpdateCategoriesBookCommand> _validator = validator;
        public async Task Handle(UpdateCategoriesBookCommand request, CancellationToken token)
        {
            await _validator.ValidateAndThrowAsync(request, token);
            var categories = await _categoryRepository.GetByIdsAsync(request.CategoryIds, token);
            await _bookRepository.UpdateCategoriesAsync(request.Id, categories, token);
        }
    }
}
