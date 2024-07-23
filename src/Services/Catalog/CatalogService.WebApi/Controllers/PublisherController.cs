﻿using CatalogService.Application.UseCases;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.WebApi.Controllers
{
    [ApiController]
    [Route("publisher")]
    public class PublisherController(
        IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePublisherCommand request, CancellationToken token = default)
        {
            var id = await _mediator.Send(request, token);
            return Ok(id);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken token = default)
        {
            var query = new GetAllPublisherQuery();
            await _mediator.Send(query, token);
            return Ok(query);
        }




        [HttpGet]
        [Route("id")]
        public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken token = default)
        {
            var query = new GetByIdPublisherQuery() { Id = id };
            await _mediator.Send(query, token);
            return Ok(query);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdatePublisherCommand request, CancellationToken token = default)
        {
            await _mediator.Send(request, token);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] Guid id, CancellationToken token = default)
        {
            var request = new DeletePublisherCommand { Id = id };
            await _mediator.Send(request, token);
            return Ok();
        }
    }
}
