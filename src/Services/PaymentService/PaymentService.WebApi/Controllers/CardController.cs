using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaymentService.Application.Cards.Commands.AddOrUpdateCard;
using PaymentService.Application.Cards.Queries.GetByUserId;
using PaymentService.Domain.Entities.Cards;
using PaymentService.WebApi.Contracts;
using System.Security.Claims;

namespace PaymentService.WebApi.Controllers
{
    [Authorize(Roles = "customer, seller")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/{v:apiVersion}/[controller]/[action]")]
    public class CardController : BaseController
    {
        private Guid GetUserId()
        {
            return Guid.Parse(HttpContext.User.Claims.FirstOrDefault(i => i.Type == ClaimTypes.NameIdentifier).Value);
        }

        [HttpPost]
        public async Task<ActionResult> AddOrUpdate([FromBody] CreateCardDto request, CancellationToken cancellation = default)
        {
            var command = new AddOrUpdateCardCommand()
            {
                UserId = GetUserId(), // TODO make this to get user id from jwt token
                CardHolderRole = CardHolderRole.Customer, // TODO make this to setup dependent on user role 
                CardNumber = request.CardNumber,
                CardCvc = request.CardCvc,
                CardHolderName = request.CardHolderName,
                CardExpirationDate = DateOnly.FromDateTime(request.CardExpirationDate),
            };

            var result = await Mediator.Send(command, cancellation);

            if (result.IsSuccess)
                return NoContent();
            else
                return HandleFailure(result);
        }

        [HttpGet]
        public async Task<ActionResult<CardDto>> Get(CancellationToken cancellation = default)
        {
            var result = await Mediator.Send(new GetByUserIdQuery
            {
                UserId = GetUserId() // TODO make this to get user id from jwt token
            }, cancellation);

            if (result.IsSuccess)
                return Ok(result.Value);
            else
                return HandleFailure(result);
        }
    }
}
