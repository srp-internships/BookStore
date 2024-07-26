using CatalogService.Application.UseCases;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.UseCases
{
    public class UpdatePublisherBookCommandValidator : AbstractValidator<UpdatePublisherBookCommand>
    {
        public UpdatePublisherBookCommandValidator()
        {
            RuleFor(p => p.PublisherId).NotEmpty();
        }
    }
}
