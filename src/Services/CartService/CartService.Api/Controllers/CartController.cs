using CartService.Aplication.Commons.DTOs;
using CartService.Aplication.Commons.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CartService.Api.Controllers
{
    [Authorize(Roles = "customer")]
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private Guid GetUserId()
        {
            return Guid.Parse(HttpContext.User.Claims.FirstOrDefault(i => i.Type == ClaimTypes.NameIdentifier).Value);
        }

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("userId")]
        public async Task<IActionResult> GetCartByUserIdAsync()
        {
            var cart = await _cartService.GetCartByUserIdAsync(GetUserId());
            return Ok(cart);
        }

        [HttpPost("userId/items")]
        public async Task<IActionResult> AddToCartAsync([FromBody] AddToCartRequest request)
        {
            await _cartService.AddToCartAsync(GetUserId(), request);
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

        [HttpDelete("userId")]
        public async Task<IActionResult> ClearCartAsync(Guid userId)
        {
            userId = GetUserId();
            await _cartService.ClearCartAsync(userId);
            return NoContent();
        }

        [HttpGet("userId/total")]
        public async Task<IActionResult> GetTotalPriceAsync()
        {
            var totalPrice = await _cartService.GetTotalPriceAsync(GetUserId());
            return Ok(totalPrice);
        }
    }
}


