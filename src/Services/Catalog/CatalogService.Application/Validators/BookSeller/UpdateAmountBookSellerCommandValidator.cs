
using CatalogService.Application.UseCases;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.Validators.BookSeller
{
    public class UpdateAmountBookSellerCommandValidator : AbstractValidator<UpdateBookSellerCommand>
    {
        public UpdateAmountBookSellerCommandValidator()
        {
            RuleFor(p => p.Amount)
                .NotEmpty()
                .GreaterThanOrEqualTo(1);
        }
    }
}
