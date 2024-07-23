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
            RuleFor(prop => prop.ISBN)
                .NotEmpty().WithMessage("ISBN is required.")
                .Length(10, 13).WithMessage("ISBN must be either 10 or 13 characters long.")
                .Matches(@"^\d{9}[\d|X]$|^\d{13}$").WithMessage("ISBN must be either a 10-digit or 13-digit number, or a 10-digit number ending with an 'X'.");

        }
        private bool BeAValidUrl(string url)
        {
            var pattern = @"^(http|https)://([\w-]+(\.[\w-]+)+)([\w.,@?^=%&:/~+#-]*[\w@?^=%&/~+#-])?$";
            return Regex.IsMatch(url, pattern);
        }
    }
}
