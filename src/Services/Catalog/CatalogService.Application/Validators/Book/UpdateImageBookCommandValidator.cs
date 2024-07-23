using CatalogService.Application.UseCases;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CatalogService.Application.Validators.Book
{
    public class UpdateImageBookCommandValidator : AbstractValidator<UpdateImageBookCommand>
    {
        public UpdateImageBookCommandValidator()
        {
            RuleFor(x => x.Image)
                .NotEmpty().WithMessage("Image can not be empty")
                .Must(BeAValidUrl).WithMessage("Incorrect URL");
        }
        private bool BeAValidUrl(string url)
        {
            var pattern = @"^(http|https)://([\w-]+(\.[\w-]+)+)([\w.,@?^=%&:/~+#-]*[\w@?^=%&/~+#-])?$";
            return Regex.IsMatch(url, pattern);
        }
    }
}
