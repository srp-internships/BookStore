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
            var pattern = @"^(http|https)://([\w-]+(\.[\w-]+)+)([\w.,@?^=%&:/~+#-]*[\w@?^=%&/~+#-])?$";
            return Regex.IsMatch(url, pattern);
        }


        private bool BeAValidISBN(string isbn)
        {
            return IsValidIsbn10(isbn) || IsValidIsbn13(isbn);
        }

        private bool IsValidIsbn10(string isbn)
        {
            if (isbn.Length != 10)
            {
                return false;
            }

            if (!Regex.IsMatch(isbn, @"^\d{9}[\dX]$"))
            {
                return false;
            }

            int sum = 0;
            for (int i = 0; i < 9; i++)
            {
                if (!int.TryParse(isbn[i].ToString(), out int digit))
                {
                    return false;
                }
                sum += digit * (10 - i);
            }

            char lastChar = isbn[9];
            sum += lastChar == 'X' ? 10 : (int)char.GetNumericValue(lastChar);

            return sum % 11 == 0;
        }

        private bool IsValidIsbn13(string isbn)
        {
            if (isbn.Length != 13)
            {
                return false;
            }

            if (!Regex.IsMatch(isbn, @"^\d{13}$"))
            {
                return false;
            }

            int sum = 0;
            for (int i = 0; i < 13; i++)
            {
                if (!int.TryParse(isbn[i].ToString(), out int digit))
                {
                    return false;
                }
                sum += i % 2 == 0 ? digit : digit * 3;
            }

            return sum % 10 == 0;
        }
    }
}
