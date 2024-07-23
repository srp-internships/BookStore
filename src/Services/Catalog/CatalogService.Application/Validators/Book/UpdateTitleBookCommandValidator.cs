using CatalogService.Application.UseCases;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.Validators.Book
{
    public class UpdateTitleBookCommandValidator : AbstractValidator<UpdateTitleBookCommand>
    {
        public UpdateTitleBookCommandValidator()
        {
            RuleFor(prop => prop.Title)
                .NotEmpty().WithMessage("Name can not be empty")
                .MinimumLength(3).WithMessage("Name's length can not be less than 3")
                .MaximumLength(50).WithMessage("Name's length can not be more than 50");
        }
    }
}
