using CatalogService.Application.UseCases;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CatalogService.Application.UseCases
{
    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookCommandValidator()
        {
            RuleFor(x => x.Image)
                .NotEmpty().WithMessage("Image can not be empty")
                .Must(BeAValidUrl).WithMessage("Incorrect URL");
            RuleFor(prop => prop.Title)
                .NotEmpty().WithMessage("Name can not be empty")
                .MinimumLength(3).WithMessage("Name's length can not be less than 3")
                .MaximumLength(50).WithMessage("Name's length can not be more than 50");


        }
        private bool BeAValidUrl(string url)
        {
            var pattern = @"^(http|https)://([\w-]+(\.[\w-]+)+)([\w.,@?^=%&:/~+#-]*[\w@?^=%&/~+#-])?$";
            return Regex.IsMatch(url, pattern);
        }
    }
}
