using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShipmentService.Aplication.CQRS.Shipments.Commands.Update;
using ShipmentService.Aplication.CQRS.Shipments.Queries.GetAll;
using ShipmentService.Aplication.CQRS.Shipments.Queries.GetById;

namespace ShipmentService.WebApi.Controllers
{
    [Authorize(Roles = "seller")]
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
        [HttpPut]
        public async Task<IActionResult> UpdateShipment([FromForm] UpdateShipmentCommand command)
        {
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
