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
            try
            {
                await _mediator.Send(command);
                return Ok("Updated successfully");
            }
            catch (Exception ex)
            {
                if (id != command.ShipmentId && !IsValidStatus(command.Status.ToDomainEnum()))
                {
                    return BadRequest("ID mismatch or Invalid status value");
                }
                else
                {
                    return BadRequest(ex.Message);
                };
            }
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
            try
            {
                var query = new GetShipmentsQuery();
                var shipments = await _mediator.Send(query);
                return Ok(shipments);
            }
            catch (Exception ex)
            {
                return NotFound("Shipments not found. ");
            }
            
        }
        private bool IsValidStatus(ShipmentService.Domain.Enums.Status status)
        {
            return Enum.IsDefined(typeof(ShipmentService.Domain.Enums.Status), status);
        }
    }
}
