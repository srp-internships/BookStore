using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentService.Aplication.CQRS.Shipments.Commands.Create
{
    public class CreateShipmentCommandValidator
        : AbstractValidator<CreateShipmentCommand>
    {
        public CreateShipmentCommandValidator()
        {
            RuleFor(x => x.OrderId)
          .NotEmpty().WithMessage("OrderId is required.");

            RuleFor(x => x.CustomerId)
                .NotEmpty().WithMessage("CustomerId is required.");

            RuleFor(x => x.ShippingAddress)
                .NotNull().WithMessage("ShippingAddress is required.")
                .SetValidator(new ShippingAddressValidator());

            RuleFor(x => x.Items)
                .NotEmpty().WithMessage("At least one shipment item is required.")
                .ForEach(item => item.SetValidator(new ShipmentItemValidator()));
        }
    }
}
