using CatalogService.Application.UseCases;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.UseCases
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(prop => prop.Name)
                .NotEmpty().WithMessage("Name can not be empty")
                .MinimumLength(3).WithMessage("Name's length can not be less than 3")
                .MaximumLength(50).WithMessage("Name's length can not be more than 50");
            RuleFor(prop => prop.Description)
                .MinimumLength(15).WithMessage("Descriptions's length can not be less than 15")
                .MaximumLength(500).WithMessage("Descriptions's length can not be more than 500")
                .When(prop => !string.IsNullOrEmpty(prop.Description));
        }
    }
}
