using CatalogService.Application.UseCases;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.UseCases
{
    public class UpdateCategoriesBookCommandValidator : AbstractValidator<UpdateCategoriesBookCommand>
    {
        public UpdateCategoriesBookCommandValidator()
        {
            RuleFor(p => p.CategoryIds)
                .NotEmpty();
        }
    }
}
