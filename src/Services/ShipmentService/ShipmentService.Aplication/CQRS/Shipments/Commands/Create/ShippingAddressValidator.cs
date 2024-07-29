using FluentValidation;
using ShipmentService.Domain.Entities.Shipments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentService.Aplication.CQRS.Shipments.Commands.Create
{
    public class ShippingAddressValidator : AbstractValidator<ShippingAddress>
    {
        public ShippingAddressValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required. ");
            RuleFor(x => x.Street)
                .NotEmpty().WithMessage("Street is required.");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("City is required.");

            RuleFor(x => x.Country)
                .NotEmpty().WithMessage("Country is required.");

        }
    }
}