using Microsoft.AspNetCore.Mvc;
using CartService.Domain.Entities;
using CartService.Aplication.Interfaces;

namespace CartService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;

        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        // GET: api/cart/{userId}
        [HttpGet("{userId}")]
        public async Task<ActionResult<Cart>> GetCart(Guid userId)
        {
            var cart = await _cartRepository.GetCartAsync(userId);

            if (cart == null)
            {
                return NotFound();
            }

            return cart;
        }
        // POST: api/cart/{userId}/items
        [HttpPost("{userId}/items")]
        public async Task<ActionResult> AddOrUpdateCartItem(Guid userId, [FromBody] CartItem cartItem)
        {
            if (cartItem == null)
            {
                return BadRequest("CartItem data is invalid");
            }

            var cart = await _cartRepository.GetCartAsync(userId);
            if (cart == null)
            {
                cart = new Cart
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    Items = new List<CartItem> { cartItem }
                };
                await _cartRepository.AddCartAsync(cart);
            }
            else
            {
                cartItem.CartId = cart.Id;
                await _cartRepository.AddOrUpdateCartItemAsync(cartItem);
            }

            await _cartRepository.SaveChangesAsync(); 
            return Ok();
        }
        // PUT: api/cart/items/{cartItemId}/quantity
        [HttpPut("items/{cartItemId}/quantity")]
        public async Task<ActionResult> UpdateCartItemQuantity(Guid cartItemId, [FromBody] int quantity)
        {
            var cartItem = await _cartRepository.GetCartItemAsync(cartItemId);

            if (cartItem == null)
            {
                return NotFound(); 
            }
            cartItem.Quantity = quantity;
            await _cartRepository.AddOrUpdateCartItemAsync(cartItem);
            await _cartRepository.SaveChangesAsync(); 

            return Ok(); 
        }
        // DELETE: api/cart/items/{cartItemId}
        [HttpDelete("items/{cartItemId}")]
        public async Task<ActionResult> RemoveCartItem(Guid cartItemId)
        {
            await _cartRepository.RemoveCartItemAsync(cartItemId);
            await _cartRepository.SaveChangesAsync();

            return Ok();
        }
    }
}
