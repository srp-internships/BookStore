using CatalogService.Application.UseCases;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.WebApi.Controllers
{
    [ApiController]
    [Route("bookseller")]
    public class BookSellerController(
        IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateBookSellerCommand request, CancellationToken token = default)
        {
            var id = _mediator.Send(request, token);
            return Ok(id);
        }

        [HttpGet]
        [Route("id")]
        public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken token = default)
        {
            var query = new GetByIdBookSellerQuery() { Id = id };
            await _mediator.Send(query, token);
            return Ok(query);
        }

        [HttpPut]
        [Route("price")]
        public async Task<IActionResult> UpdatePrice([FromBody] UpdatePriceBookSellerCommand request, CancellationToken token = default)
        {
            await _mediator.Send(request, token);
            return Ok();
        }

        [HttpPut]
        [Route("amount")]
        public async Task<IActionResult> UpdateAmount([FromBody] UpdateAmountBookSellerCommand request, CancellationToken token = default)
        {
            await _mediator.Send(request, token);
            return Ok();
        }

        [HttpPut]
        [Route("description")]
        public async Task<IActionResult> UpdateDescription([FromBody] UpdateDescriptionBookSellerCommand request, CancellationToken token = default)
        {
            await _mediator.Send(request, token);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] Guid id, CancellationToken token = default)
        {
            var request = new DeleteBookSellerCommand { Id = id };
            await _mediator.Send(request, token);
            return Ok();
        }
    }
}
