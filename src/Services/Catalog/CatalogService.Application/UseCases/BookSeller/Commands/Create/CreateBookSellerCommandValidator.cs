using CatalogService.Application.UseCases;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.UseCases
{
    public class CreateBookSellerCommandValidator : AbstractValidator<CreateBookSellerCommand>
    {
        public CreateBookSellerCommandValidator()
        {
            RuleFor(p => p.Price)
                .NotEmpty()
                .GreaterThanOrEqualTo(1);
        }
    }
}
