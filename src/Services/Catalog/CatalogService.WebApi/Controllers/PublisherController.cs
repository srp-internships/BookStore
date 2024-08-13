using AutoMapper;
using CatalogService.Application.UseCases;
using CatalogService.Application.UseCases.Queries;
using CatalogService.Application.Interfaces.BlobServices;
using CatalogService.WebApi.Dto;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.WebApi.Controllers
{
    [ApiController]
    [Route("publisher")]
    public class PublisherController(
        IMediator mediator,
        IMapper mapper,
        IBlobService blobService) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly IMapper _mapper = mapper;
        private readonly IBlobService _blobService = blobService;

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreatePublisherDto publisherDto, CancellationToken token = default)
        {
            var request = _mapper.Map<CreatePublisherCommand>(publisherDto);

            var id = await _mediator.Send(request, token);
            return Ok(id);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken token = default)
        {
            var vm = await _mediator.Send(new GetAllPublisherQuery(), token);
            return Ok(vm);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken token = default)
        {
            var query = new GetByIdPublisherQuery() { Id = id };
            var publisherDto = await _mediator.Send(query, token);
            return Ok(publisherDto);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Update([FromForm] UpdatePublisherDto publisherDto, CancellationToken token = default)
        {
            var request = _mapper.Map<UpdatePublisherCommand>(publisherDto);
            await _mediator.Send(request, token);
            return Ok();
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] Guid id, CancellationToken token = default)
        {
            var request = new DeletePublisherCommand { Id = id };
            await _mediator.Send(request, token);
            return Ok();
        }
    }
}
