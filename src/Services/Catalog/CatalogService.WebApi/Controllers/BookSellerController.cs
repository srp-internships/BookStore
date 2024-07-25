using CatalogService.Application.UseCases;
using MediatR;
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
            await _mediator.Send(request, token);
            return Ok();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken token = default)
        {
            var query = new GetByIdBookSellerQuery() { Id = id };
            var bookSellerDto = _mediator.Send(query, token);
            return Ok(bookSellerDto);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateBookSellerCommand request, CancellationToken token = default)
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
