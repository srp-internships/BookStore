﻿using CatalogService.Application.UseCases;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.WebApi.Controllers
{
    [Authorize(Roles = "seller, admin")]
    [ApiController]
    [Route("category")]
    public class CategoryController(
        IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody] CreateCategoryCommand request, CancellationToken token = default)
        {
            var id = await _mediator.Send(request, token);
            return Ok(id);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken token = default)
        {
            var vm = await _mediator.Send(new GetAllCategoryQuery(), token);
            return Ok(vm);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken token = default)
        {
            var query = new GetByIdCategoryQuery() { Id = id };
            var categoryDto = await _mediator.Send(query, token);
            return Ok(categoryDto);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateCategoryCommand request, CancellationToken token = default)
        {
            await _mediator.Send(request, token);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] Guid id, CancellationToken token = default)
        {
            var request = new DeleteCategoryCommand { Id = id };
            await _mediator.Send(request, token);
            return Ok();
        }
    }
}
