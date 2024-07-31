using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentService.Aplication.CQRS.Shipments.Commands.Update
{
    public class UpdateShipmentCommandValidator : AbstractValidator<UpdateShipmentCommand>
    {
        public UpdateShipmentCommandValidator() 
        {
            RuleFor(s=>s.ShipmentId)
                .NotEmpty()
                .WithMessage("ShipmentId is required. ")
                .WithName("ShipmentId");
            RuleFor(s => s.Status)
                .IsInEnum().WithMessage("Invalid status value");
            RuleFor(s => s.UpdatedStatusDateTime)
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage("Updated status date and time must be in the past or present.");
        }
    }
}
