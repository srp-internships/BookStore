using Microsoft.AspNetCore.Mvc;
using CartService.Domain.Entities;
using CartService.Aplication.Interfaces;
using CartService.Infrastructure.Repositories;

namespace CartService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<Cart>> GetCart(Guid userId)
        {
            var cart = await _cartService.GetCartAsync(userId);
            if (cart == null)
            {
                return NotFound();
            }
            return Ok(cart);
        }

        [HttpPost]
        public async Task<ActionResult> AddCart([FromBody] Cart cart)
        {
            await _cartService.AddCartAsync(cart);
            return CreatedAtAction(nameof(GetCart), new { userId = cart.UserId }, cart);
        }

        [HttpPost("addOrUpdateItem")]
        public async Task<ActionResult> AddOrUpdateCartItem([FromBody] CartItem cartItem)
        {
            await _cartService.AddOrUpdateCartItemAsync(cartItem);
            return NoContent();
        }

        [HttpPut("updateItemQuantity/{cartItemId}")]
        public async Task<ActionResult> UpdateItemQuantity(Guid cartItemId, [FromBody] int quantity)
        {
            await _cartService.UpdateItemQuantityAsync(cartItemId, quantity);
            return NoContent();
        }

        [HttpDelete("removeItem/{cartItemId}")]
        public async Task<ActionResult> RemoveItemFromCart(Guid cartItemId)
        {
            await _cartService.RemoveItemFromCartAsync(cartItemId);
            return NoContent();
        }

        [HttpDelete("clearCart/{cartId}")]
        public async Task<ActionResult> ClearCart(Guid cartId)
        {
            await _cartService.ClearCartAsync(cartId);
            return NoContent();
        }
    }
}
