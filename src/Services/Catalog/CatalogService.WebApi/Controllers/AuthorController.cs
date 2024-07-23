﻿using CatalogService.Application.UseCases;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.WebApi.Controllers
{
    [ApiController]
    [Route("author")]
    public class AuthorController(
        IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAuthorCommand request, CancellationToken token = default)
        {
            var Id = await _mediator.Send(request, token);
            return Ok(Id);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken token = default)
        {
            var query = new GetAllAuthorQuery();
            await _mediator.Send(query, token);
            return Ok(query);
        }




        [HttpGet]
        [Route("id")]
        public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken token = default)
        {
            var query = new GetByIdAuthorQuery() { Id = id };
            await _mediator.Send(query, token);
            return Ok(query);

        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateAuthorCommand request, CancellationToken token = default)
        {
            await _mediator.Send(request, token);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] Guid id, CancellationToken token = default)
        {
            var request = new DeleteAuthorCommand { Id = id };
            await _mediator.Send(request, token);
            return Ok();
        }
    }
}
