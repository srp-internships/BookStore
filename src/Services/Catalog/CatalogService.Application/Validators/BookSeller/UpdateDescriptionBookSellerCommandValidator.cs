using CatalogService.Application.UseCases;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.Validators.BookSeller
{
    public class UpdateDescriptionBookSellerCommandValidator : AbstractValidator<UpdateDescriptionBookSellerCommand>
    {
        public UpdateDescriptionBookSellerCommandValidator()
        {
            RuleFor(prop => prop.Description)
                .MinimumLength(15).WithMessage("Name's length can not be less than 15")
                .MaximumLength(500).WithMessage("Name's length can not be more than 500");
        }
    }
}
