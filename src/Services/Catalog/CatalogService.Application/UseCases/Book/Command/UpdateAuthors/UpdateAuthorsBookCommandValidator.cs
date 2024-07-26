using CatalogService.Application.UseCases;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.UseCases
{
    public class UpdateAuthorsBookCommandValidator : AbstractValidator<UpdateAuthorsBookCommand>
    {
        public UpdateAuthorsBookCommandValidator()
        {
            RuleFor(p => p.AuthorIds)
                .NotEmpty();
        }
    }
}
