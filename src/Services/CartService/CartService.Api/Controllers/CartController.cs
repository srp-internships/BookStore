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





















        //private readonly ICartRepository _cartRepository;

        //public CartController(ICartRepository cartRepository)
        //{
        //    _cartRepository = cartRepository;
        //}

        //// GET: api/cart/{userId}
        //[HttpGet("{userId}")]
        //public async Task<IActionResult> GetCartByUserId(Guid userId)
        //{
        //    var cart = await _cartRepository.GetCartByUserIdAsync(userId);
        //    if (cart == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(cart);
        //}

        //// POST: api/cart/{userId}/items
        //[HttpPost("{userId}/items")]
        //public async Task<IActionResult> AddItemToCart(Guid userId, [FromBody] CartItem item)
        //{
        //    if (item == null)
        //    {
        //        return BadRequest();
        //    }

        //    await _cartRepository.AddItemToCartAsync(userId, item);

        //    return Ok();
        //}

        //// PUT: api/cart/{userId}/items/{itemId}
        //[HttpPut("{userId}/items/{itemId}")]
        //public async Task<IActionResult> UpdateCartItem(Guid userId, Guid itemId, [FromBody] CartItem item)
        //{
        //    if (item == null || item.Id != itemId)
        //    {
        //        return BadRequest();
        //    }

        //    await _cartRepository.UpdateCartItemAsync(userId, item);
        //    return Ok();
        //}

        //// DELETE: api/cart/{userId}/items/{itemId}
        //[HttpDelete("{userId}/items/{itemId}")]
        //public async Task<IActionResult> RemoveItemFromCart(Guid userId, Guid itemId)
        //{
        //    await _cartRepository.RemoveItemFromCartAsync(userId, itemId);
        //    return Ok();
        //}

        //// GET: api/cart/{userId}/total
        //[HttpGet("{userId}/total")]
        //public async Task<IActionResult> GetCartTotal(Guid userId)
        //{
        //    var total = await _cartRepository.GetCartTotalAsync(userId);
        //    return Ok(total);
        //}
    }
}
