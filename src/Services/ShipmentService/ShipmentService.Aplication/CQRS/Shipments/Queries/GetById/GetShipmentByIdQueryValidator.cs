using FluentValidation;
using ShipmentService.Aplication.CQRS.Shipments.Commands.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentService.Aplication.CQRS.Shipments.Queries.GetById
{
    public class GetShipmentByIdQueryValidator : AbstractValidator<GetShipmentByIdQuery>
    {
        public GetShipmentByIdQueryValidator() 
        {
            RuleFor(x => x.ShipmentId)
           .NotEmpty().WithMessage("ShipmentId is required.");
        }
    }
}
