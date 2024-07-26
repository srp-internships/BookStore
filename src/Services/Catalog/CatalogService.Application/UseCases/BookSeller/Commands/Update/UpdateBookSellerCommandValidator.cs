using CatalogService.Application.UseCases;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.UseCases
{
    public class UpdateBookSellerCommandValidator : AbstractValidator<UpdateBookSellerCommand>
    {
        public UpdateBookSellerCommandValidator()
        {
            RuleFor(p => p.Price)
                .NotEmpty()
                .GreaterThanOrEqualTo(1);
            RuleFor(prop => prop.Description)
                .NotEmpty()
                .MinimumLength(15).WithMessage("Descriptions's length can not be less than 15")
                .MaximumLength(500).WithMessage("Descriptions's length can not be more than 500");
        }
    }
}
