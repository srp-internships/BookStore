﻿using AutoMapper;
using CatalogService.Application.UseCases;
using CatalogService.WebApi.Dto;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.WebApi.Controllers
{
    [Authorize(Roles = "seller, admin")]
    [ApiController]
    [Route("book")]
    public class BookController(
        IMediator mediator,
        IMapper mapper) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly IMapper _mapper = mapper;


        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateBookDto bookDto, CancellationToken token = default)
        {
            var request = _mapper.Map<CreateBookCommand>(bookDto);
            var id = await _mediator.Send(request, token);
            return Ok(id);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll(CancellationToken token = default)
        {
            var vm = await _mediator.Send(new GetAllBookQuery(), token);
            return Ok(vm);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken token = default)
        {
            var query = new GetByIdBookQuery() { Id = id };
            var bookDto = await _mediator.Send(query, token);
            return Ok(bookDto);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] UpdateBookDto bookDto, CancellationToken token = default)
        {
            var request = _mapper.Map<UpdateBookCommand>(bookDto);
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
