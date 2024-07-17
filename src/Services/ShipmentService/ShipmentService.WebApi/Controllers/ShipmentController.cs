using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShipmentService.Aplication.CQRS.Shipments.Commands.Create;
using ShipmentService.Aplication.CQRS.Shipments.Queries;
using ShipmentService.Aplication.Interfaces;

namespace ShipmentService.WebApi.Controllers
{
    public class ShipmentController: ControllerBase
    {
        private readonly  IMediator _mediator;
        public ShipmentController(IMediator mediator) 
        {
            _mediator = mediator;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetShipmentById(Guid id)
        {
            var query = new GetShipmentByIdQuery { ShipmentId = id };
            var shipment = await _mediator.Send(query);
            return Ok(shipment);
        }

        [HttpPost]
        public async Task<IActionResult> CreateShipment(CreateShipmentCommand command)
        {
            var shipmentId = await _mediator.Send(command);
            return Ok(shipmentId);
        }
        
    }
}
