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
        #region GetCart
        /// <summary>
        /// Retrieves the cart for a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>The user's cart.</returns>

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
        #endregion

        #region PostCart
        /// <summary>
        /// Creates a new cart.
        /// </summary>
        /// <param name="cart">The cart to create.</param>
        /// <returns>Action result.</returns>
        [HttpPost]
        public async Task<ActionResult> AddCart([FromBody] Cart cart)
        {
            await _cartService.AddCartAsync(cart);
            return CreatedAtAction(nameof(GetCart), new { userId = cart.UserId }, cart);
        }
        #endregion

        #region AddOrUpdateItem
        /// <summary>
        /// Adds or updates an item in the cart.
        /// </summary>
        /// <param name="cartItem">The item to add or update.</param>
        /// <returns>Action result.</returns>
        [HttpPost("addOrUpdateItem")]
        public async Task<ActionResult> AddOrUpdateCartItem([FromBody] CartItem cartItem)
        {
            await _cartService.AddOrUpdateCartItemAsync(cartItem);
            return NoContent();
        }
        #endregion

        #region UpdateCartItemQuantity
        /// <summary>
        /// Updates the quantity of a specific item in the cart.
        /// </summary>
        /// <param name="cartItemId">The ID of the cart item.</param>
        /// <param name="quantity">The new quantity of the item.</param>
        /// <returns>Action result.</returns>
        [HttpPut("updateItemQuantity/{cartItemId}")]
        public async Task<ActionResult> UpdateItemQuantity(Guid cartItemId, [FromBody] int quantity)
        {
            await _cartService.UpdateItemQuantityAsync(cartItemId, quantity);
            return NoContent();
        }
        #endregion

        #region DeleteCartItemById
        /// <summary>
        /// Removes a specific item from the cart.
        /// </summary>
        /// <param name="cartItemId">The ID of the cart item to remove.</param>
        /// <returns>Action result.</returns>
        [HttpDelete("removeItem/{cartItemId}")]
        public async Task<ActionResult> RemoveItemFromCart(Guid cartItemId)
        {
            await _cartService.RemoveItemFromCartAsync(cartItemId);
            return NoContent();
        }
        #endregion

        #region DeleteCart
        /// <summary>
        /// Clears all items from the cart.
        /// </summary>
        /// <param name="cartId">The ID of the cart to clear.</param>
        /// <returns>Action result.</returns>
        [HttpDelete("clearCart/{cartId}")]
        public async Task<ActionResult> ClearCart(Guid cartId)
        {
            await _cartService.ClearCartAsync(cartId);
            return NoContent();
        }
        #endregion
    }
}
