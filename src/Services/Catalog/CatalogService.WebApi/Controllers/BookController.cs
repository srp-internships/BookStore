using CatalogService.Application.UseCases;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.WebApi.Controllers
{
    [ApiController]
    [Route("book")]
    public class BookController(
        IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBookCommand request, CancellationToken token = default)
        {
            var id = await _mediator.Send(request, token);
            return Ok(id);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken token = default)
        {
            var query = new GetAllBookQuery();
            await _mediator.Send(query, token);
            return Ok(query);
        }




        [HttpGet]
        [Route("id")]
        public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken token = default)
        {
            var query = new GetByIdBookQuery() { Id = id };
            await _mediator.Send(query, token);
            return Ok(query);
        }

        [HttpPut]
        [Route("authors")]
        public async Task<IActionResult> Update([FromBody] UpdateAuthorsBookCommand request, CancellationToken token = default)
        {
            await _mediator.Send(request, token);
            return Ok();
        }

        [HttpPut]
        [Route("categories")]
        public async Task<IActionResult> Update([FromBody] UpdateCategoriesBookCommand request, CancellationToken token = default)
        {
            await _mediator.Send(request, token);
            return Ok();
        }

        [HttpPut]
        [Route("image")]
        public async Task<IActionResult> Update([FromBody] UpdateImageBookCommand request, CancellationToken token = default)
        {
            await _mediator.Send(request, token);
            return Ok();
        }
        [HttpPut]
        [Route("publisher")]
        public async Task<IActionResult> Update([FromBody] UpdatePublisherBookCommand request, CancellationToken token = default)
        {
            await _mediator.Send(request, token);
            return Ok();
        }
        [HttpPut]
        [Route("title")]
        public async Task<IActionResult> Update([FromBody] UpdateTitleBookCommand request, CancellationToken token = default)
        {
            await _mediator.Send(request, token);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] Guid id, CancellationToken token = default)
        {
            var request = new DeleteBookCommand { Id = id };
            await _mediator.Send(request, token);
            return Ok();
        }
    }
}
