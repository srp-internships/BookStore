using CartService.Aplication.Interfaces;
using CartService.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CartService.Api.Controllers
{
    public class CartControllerV2: ControllerBase
    {
        private readonly ICartRepositoryV2 _cartRepository;

        public CartControllerV2(ICartRepositoryV2 cartRepository)
        {
            _cartRepository = cartRepository;
        }

        // GET: api/cart/{userId}
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCartByUserId(Guid userId)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);
            if (cart == null)
            {
                return NotFound();
            }
            return Ok(cart);
        }

        // POST: api/cart/{userId}/items
        [HttpPost("{userId}/items")]
        public async Task<IActionResult> AddItemToCart(Guid userId, [FromBody] CartItem item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            await _cartRepository.AddItemToCartAsync(userId, item);

            return Ok();
        }

        // PUT: api/cart/{userId}/items/{itemId}
        [HttpPut("{userId}/items/{itemId}")]
        public async Task<IActionResult> UpdateCartItem(Guid userId, Guid itemId, [FromBody] CartItem item)
        {
            if (item == null || item.Id != itemId)
            {
                return BadRequest();
            }

            await _cartRepository.UpdateCartItemAsync(userId, item);
            return Ok();
        }


        // DELETE: api/cart/{userId}/items/{itemId}
        [HttpDelete("{userId}/items/{itemId}")]
        public async Task<IActionResult> RemoveItemFromCart(Guid userId, Guid itemId)
        {
            await _cartRepository.RemoveItemFromCartAsync(userId, itemId);
            return Ok();
        }

        // GET: api/cart/{userId}/total
        [HttpGet("{userId}/total")]
        public async Task<IActionResult> GetCartTotal(Guid userId)
        {
            var total = await _cartRepository.GetCartTotalAsync(userId);
            return Ok(total);
        }
    }
}
