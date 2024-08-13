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
    public class UpdatePublisherCommandValidator : AbstractValidator<UpdatePublisherCommand>
    {
        public UpdatePublisherCommandValidator()
        {
            RuleFor(prop => prop.Name)
                .NotEmpty().WithMessage("Name can not be empty")
                .MinimumLength(3).WithMessage("Name's length can not be less than 3")
                .MaximumLength(50).WithMessage("Name's length can not be more than 50");

            RuleFor(prop => prop.Address)
                .NotEmpty().WithMessage("Address can not be empty")
                .MaximumLength(100).WithMessage("Address must not exceed 100 characters.");

            RuleFor(prop => prop.Email)
                .NotEmpty().WithMessage("Email can not be empty")
                .EmailAddress().WithMessage("Email must be a valid email address.");
        }
        private bool BeAValidUrl(string url)
        {
            var pattern = @"^(http|https)://([\w-]+(\.[\w-]+)+)([\w.,@?^=%&:/~+#-]*[\w@?^=%&/~+#-])?$";
            return Regex.IsMatch(url, pattern);
        }
    }

}
