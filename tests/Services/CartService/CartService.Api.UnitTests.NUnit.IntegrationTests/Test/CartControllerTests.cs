using CartService.Api.UnitTests.NUnit.IntegrationTests.CommonData;
using CartService.Domain.Entities;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CartService.Api.UnitTests.NUnit.IntegrationTests.Test
{
    public class CartControllerTests 
    {
        private WebApplicationFactory<Program> _factory;
        private HttpClient _client;

        [SetUp]
        public void SetUp()
        {
            _factory = new ServerApiFactory();
            _client = _factory.CreateClient();
        }

        [TearDown]
        public void TearDown()
        {
            _client.Dispose();
            _factory.Dispose();
        }

        [Test]
        public async Task GetCart_ReturnsNotFound_WhenCartDoesNotExist()
        {
            // Arrange
            var userId = Guid.NewGuid();

            // Act
            var response = await _client.GetAsync($"/api/cart/{userId}");

            // Assert
            Assert.AreEqual(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public async Task AddCart_AddsCartSuccessfully()
        {
            // Arrange
            var cart = new Cart
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid()
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/cart", cart);

            // Assert
            response.EnsureSuccessStatusCode();
            var createdCart = await response.Content.ReadFromJsonAsync<Cart>();
            Assert.NotNull(createdCart);
            Assert.AreEqual(cart.UserId, createdCart.UserId);
        }

        [Test]
        public async Task AddOrUpdateCartItem_AddsItemSuccessfully()
        {
            // Arrange
            var cart = new Cart
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid()
            };

            await _client.PostAsJsonAsync("/api/cart", cart);

            var cartItem = new CartItem
            {
                Id = Guid.NewGuid(),
                CartId = cart.Id,
                BookId = Guid.NewGuid(),
                BookName = "Test Book",
                Price = 10.0m,
                Quantity = 1,
                SellerId = Guid.NewGuid()
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/cart/addOrUpdateItem", cartItem);

            // Assert
            response.EnsureSuccessStatusCode();
            var updatedCart = await _client.GetFromJsonAsync<Cart>($"/api/cart/{cart.UserId}");
            Assert.NotNull(updatedCart);
            Assert.AreEqual(1, updatedCart.Items.Count);
            Assert.AreEqual(cartItem.BookName, updatedCart.Items[0].BookName);
        }

        [Test]
        public async Task RemoveItemFromCart_RemovesItemSuccessfully()
        {
            // Arrange
            var cart = new Cart
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid()
            };

            await _client.PostAsJsonAsync("/api/cart", cart);

            var cartItem = new CartItem
            {
                Id = Guid.NewGuid(),
                CartId = cart.Id,
                BookId = Guid.NewGuid(),
                BookName = "Test Book",
                Price = 10.0m,
                Quantity = 1,
                SellerId = Guid.NewGuid()
            };

            await _client.PostAsJsonAsync("/api/cart/addOrUpdateItem", cartItem);

            // Act
            var response = await _client.DeleteAsync($"/api/cart/removeItem/{cartItem.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
            var updatedCart = await _client.GetFromJsonAsync<Cart>($"/api/cart/{cart.UserId}");
            Assert.NotNull(updatedCart);
            Assert.AreEqual(0, updatedCart.Items.Count);
        }

        [Test]
        public async Task ClearCart_ClearsItemsSuccessfully()
        {
            // Arrange
            var cart = new Cart
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid()
            };

            await _client.PostAsJsonAsync("/api/cart", cart);

            var cartItem = new CartItem
            {
                Id = Guid.NewGuid(),
                CartId = cart.Id,
                BookId = Guid.NewGuid(),
                BookName = "Test Book",
                Price = 10.0m,
                Quantity = 1,
                SellerId = Guid.NewGuid()
            };

            await _client.PostAsJsonAsync("/api/cart/addOrUpdateItem", cartItem);

            // Act
            var response = await _client.DeleteAsync($"/api/cart/clearCart/{cart.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
            var updatedCart = await _client.GetFromJsonAsync<Cart>($"/api/cart/{cart.UserId}");
            Assert.NotNull(updatedCart);
            Assert.AreEqual(0, updatedCart.Items.Count);
        }
    }
}
