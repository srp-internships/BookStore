using FluentValidation;
using ShipmentService.Domain.Entities.Shipments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentService.Aplication.CQRS.Shipments.Commands.Create
{
    public class ShipmentItemValidator : AbstractValidator<ShipmentItem>
    {
        public ShipmentItemValidator()
        {
            RuleFor(x => x.ItemId)
                .NotEmpty().WithMessage("ItemId is required.");
            RuleFor(x => x.BookName)
                .NotNull().WithMessage("Name is required!")
                .MinimumLength(2).MaximumLength(20).WithMessage("Length name 2 and 20");
            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than zero.");
        }
    }
}
