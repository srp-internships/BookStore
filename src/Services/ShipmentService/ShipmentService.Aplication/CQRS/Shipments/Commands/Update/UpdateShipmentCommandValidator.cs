using FluentValidation;

namespace ShipmentService.Aplication.CQRS.Shipments.Commands.Update
{
    public class UpdateShipmentCommandValidator : AbstractValidator<UpdateShipmentCommand>
    {
        public UpdateShipmentCommandValidator()
        {
            RuleFor(s => s.ShipmentId)
                .NotEmpty()
                .WithMessage("ShipmentId is required. ")
                .WithName("ShipmentId");
            RuleFor(s => s.Status)
                .IsInEnum().WithMessage("Invalid status value");
        }
    }
}
