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

        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBookSellerCommand request, CancellationToken token = default)
        {
            var id = await _mediator.Send(request, token);
            return Ok(id);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken token = default)
        {
            var query = new GetByIdBookSellerQuery() { Id = id };
            var bookSellerDto = await _mediator.Send(query, token);
            return Ok(bookSellerDto);
        }

        [HttpGet]
        [Route("book_id")]
        public async Task<IActionResult> GetListByBookId([FromRoute] Guid book_id, CancellationToken token = default)
        {
            var query = new GetListByBookIdBookSellerQuery() { BookId = book_id };
            var bookSellerDtos = await _mediator.Send(query, token);
            return Ok(bookSellerDtos);
        }
        //[Authorize]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateBookSellerCommand request, CancellationToken token = default)
        {
            await _mediator.Send(request, token);
            return Ok();
        }

        //[Authorize]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] Guid id, CancellationToken token = default)
        {
            var request = new DeleteBookSellerCommand { Id = id };
            await _mediator.Send(request, token);
            return Ok();
        }
    }
}
