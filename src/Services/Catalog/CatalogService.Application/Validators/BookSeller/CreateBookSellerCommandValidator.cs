using CatalogService.Application.UseCases;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.Validators.BookSeller
{
    public class CreateBookSellerCommandValidator : AbstractValidator<CreateBookSellerCommand>
    {
        public CreateBookSellerCommandValidator()
        {
            RuleFor(p => p.Amount)
                .NotEmpty()
                .GreaterThanOrEqualTo(1);
            RuleFor(p => p.Price)
                .NotEmpty()
                .GreaterThanOrEqualTo(1);
            RuleFor(prop => prop.Description)
                .MinimumLength(15).WithMessage("Name's length can not be less than 15")
                .MaximumLength(500).WithMessage("Name's length can not be more than 500");
        }
    }
}
