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

        }
    }
}
