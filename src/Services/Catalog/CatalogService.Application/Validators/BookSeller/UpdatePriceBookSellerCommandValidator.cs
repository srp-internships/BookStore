using CatalogService.Application.UseCases;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.Validators.BookSeller
{
    public class UpdatePriceBookSellerCommandValidator : AbstractValidator<UpdatePriceBookSellerCommand>
    {
        public UpdatePriceBookSellerCommandValidator()
        {
            RuleFor(p => p.Price)
                .NotEmpty()
                .GreaterThanOrEqualTo(1);
        }
    }
}
