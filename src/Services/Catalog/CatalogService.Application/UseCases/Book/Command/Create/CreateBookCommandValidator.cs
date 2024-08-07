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
    public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
    {
        public CreateBookCommandValidator()
        {
            RuleFor(prop => prop.Title)
                .NotEmpty().WithMessage("Name can not be empty")
                .MinimumLength(3).WithMessage("Name's length can not be less than 3")
                .MaximumLength(50).WithMessage("Name's length can not be more than 50");

            RuleFor(x => x.Image)
                .NotEmpty().WithMessage("Image can not be empty")
                .Must(BeAValidUrl).WithMessage("Incorrect URL");

            RuleFor(book => book.ISBN)
                .NotEmpty().WithMessage("ISBN can not be empty")
                .Must(BeAValidISBN).WithMessage("ISBN is not valid.");

            RuleFor(book => book.PublisherId)
                .NotEmpty().WithMessage("Publisher field can not be empty");

            RuleFor(book => book.CategoryIds)
                .NotEmpty().WithMessage("Category field can not be empty");

            RuleFor(book => book.AuthorIds)
                .NotEmpty().WithMessage("Author field can not be empty");
        }

        private bool BeAValidUrl(string url)
        {
            var pattern = @"^(http|https):\/\/[^\s$.?#].[^\s]*$";
            return Regex.IsMatch(url, pattern);
        }
        public static string NormalizeIsbn(string isbn)
        {
            return isbn.Replace("-", "").Replace(" ", "");
        }
        private bool BeAValidISBN(string isbn)
        {
            var normalizedIsbn = NormalizeIsbn(isbn);
            return normalizedIsbn != null && (normalizedIsbn.Length == 10 
                || normalizedIsbn.Length == 13) && normalizedIsbn.All(char.IsDigit);
        }

        
    }

}
