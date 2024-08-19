using CartService.Aplication.Commons.DTOs;
using CartService.Aplication.Commons.Interfaces;
using CartService.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CartService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCartByUserIdAsync(Guid userId)
        {
            var cart = await _cartService.GetCartByUserIdAsync(userId);
            return Ok(cart);
        }

        [HttpPost("{userId}/items")]
        public async Task<IActionResult> AddToCartAsync(Guid userId, [FromBody] AddToCartRequest request)
        {
            await _cartService.AddToCartAsync(userId, request);
            return NoContent();
        }

        [HttpPut("items/{cartItemId}")]
        public async Task<IActionResult> UpdateCartItemQuantityAsync(Guid cartItemId, [FromBody] int quantity)
        {
            await _cartService.UpdateCartItemQuantityAsync(cartItemId, quantity);
            return NoContent();
        }

        [HttpDelete("items/{cartItemId}")]
        public async Task<IActionResult> RemoveCartItemAsync(Guid cartItemId)
        {
            await _cartService.RemoveCartItemAsync(cartItemId);
            return NoContent();
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> ClearCartAsync(Guid userId)
        {
            await _cartService.ClearCartAsync(userId);
            return NoContent();
        }

        [HttpGet("{userId}/total")]
        public async Task<IActionResult> GetTotalPriceAsync(Guid userId)
        {
            var totalPrice = await _cartService.GetTotalPriceAsync(userId);
            return Ok(totalPrice);
        }
    }
}


