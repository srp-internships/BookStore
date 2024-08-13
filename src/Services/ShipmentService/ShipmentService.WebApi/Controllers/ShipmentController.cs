using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShipmentService.Aplication.Common.Extentions;
using ShipmentService.Aplication.CQRS.Shipments.Commands.Update;
using ShipmentService.Aplication.CQRS.Shipments.Queries.GetAll;
using ShipmentService.Aplication.CQRS.Shipments.Queries.GetById;

namespace ShipmentService.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShipmentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ShipmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // PUT api/shipments/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShipment(Guid id, [FromBody] UpdateShipmentCommand command)
        {
            if (id != command.ShipmentId)
            {
                return BadRequest("ID mismatch.");
            }

            await _mediator.Send(command);
            return Ok("Updated successfully");
        }

        // GET api/shipments/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetShipmentById(Guid id)
        {
            var query = new GetShipmentByIdQuery(id);
            var shipment = await _mediator.Send(query);
            return shipment != null ? Ok(shipment) : NotFound();
        }

        // GET api/shipments
        [HttpGet]
        public async Task<IActionResult> GetAllShipments()
        {
            var query = new GetShipmentsQuery();
            var shipments = await _mediator.Send(query);
            return Ok(shipments);
        }
    }
}
