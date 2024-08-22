using CatalogService.Application.UseCases;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.WebApi.Controllers
{
    [Authorize(Roles = "seller, admin")]
    [ApiController]
    [Route("bookseller")]
    public class BookSellerController(
        IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBookSellerCommand request, CancellationToken token = default)
        {
            var id = await _mediator.Send(request, token);
            return Ok(id);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken token = default)
        {
            var query = new GetByIdBookSellerQuery() { Id = id };
            var bookSellerDto = await _mediator.Send(query, token);
            return Ok(bookSellerDto);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("book_id")]
        public async Task<IActionResult> GetListByBookId([FromQuery] Guid book_id, CancellationToken token = default)
        {
            var query = new GetListByBookIdBookSellerQuery() { BookId = book_id };
            var bookSellerDtos = await _mediator.Send(query, token);
            return Ok(bookSellerDtos);
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
