using CartService.Aplication.Interfaces;
using CartService.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CartService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartControllerV3 : ControllerBase
    {
        private readonly ICartServiceV3 _cartService;

        public CartControllerV3(ICartServiceV3 cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("{userId}")]
        public ActionResult<Cart> GetCart(Guid userId)
        {
            if (!_cartService.UserExists(userId))
            {
                return NotFound("User not found.");
            }
            return Ok(_cartService.GetCart(userId));
        }

        [HttpPost("{userId}")]
        public ActionResult AddToCart(Guid userId, [FromBody] CartItem item)
        {
            if (!_cartService.UserExists(userId))
            {
                return NotFound("User not found.");
            }
            _cartService.AddToCart(userId, item);
            return Ok();
        }

        [HttpDelete("{userId}/{productId}")]
        public ActionResult RemoveFromCart(Guid userId, Guid bookId)
        {
            if (!_cartService.UserExists(userId))
            {
                return NotFound("User not found.");
            }
            _cartService.RemoveFromCart(userId, bookId);
            return Ok();
        }

        [HttpDelete("{userId}/clear")]
        public ActionResult ClearCart(Guid userId)
        {
            if (!_cartService.UserExists(userId))
            {
                return NotFound("User not found.");
            }
            _cartService.ClearCart(userId);
            return Ok();
        }

        [HttpPut("{userId}/update-quantity")]
        public IActionResult UpdateQuantity(Guid userId, [FromBody] UpdateQuantityRequest request)
        {
            if (!_cartService.UserExists(userId))
            {
                return NotFound("User not found.");
            }
            _cartService.UpdateQuantity(userId, request.BookId, request.NewQuantity);
            return Ok();
        }
    }
}
