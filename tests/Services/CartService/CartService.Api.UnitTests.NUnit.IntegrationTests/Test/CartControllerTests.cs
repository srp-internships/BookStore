using CartService.Api.UnitTests.NUnit.IntegrationTests.CommonData;
using CartService.Domain.Entities;
using Microsoft.AspNetCore.Hosting.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CartService.Api.UnitTests.NUnit.IntegrationTests.Test
{
    public class CartControllerTests : BaseTestEntity
    {
        [Test]
        public async Task GetCart_ShouldReturnCartForValidUserId()
        {
            // Arrange
            var userId = await GetFirstUserId();
            var _client = Server.CreateClient();

            // Act
            var response = await _client.GetAsync($"api/Cart/{userId}");

            // Assert
            response.EnsureSuccessStatusCode();
            var cart = await response.Content.ReadFromJsonAsync<Cart>();
            Assert.That(cart, Is.Not.Null);
            Assert.That(cart.UserId, Is.EqualTo(userId));
        }

        [Test]
        public async Task GetCart_ShouldNotReturnCartForInvalidUserId()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var _client = Server.CreateClient();

            // Act
            var response = await _client.GetAsync($"api/Cart/{userId}");

            // Assert
            Assert.That(response.IsSuccessStatusCode, Is.False);
        }

        [Test]
        public async Task AddItemToCart_ShouldAddItemSuccessfully()
        {
            // Arrange
            var userId = await GetFirstUserId();
            var _client = Server.CreateClient();
            var item = new CartItem
            {
                BookId = Guid.NewGuid(),
                Quantity = 1
            };
            var content = new StringContent(JsonSerializer.Serialize(item), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync($"api/Cart/{userId}/items", content);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Test]
        public async Task UpdateItemQuantity_ShouldUpdateSuccessfully()
        {
            // Arrange
            var userId = await GetFirstUserId();
            var itemId = await GetFirstCartItemId(userId);
            var _client = Server.CreateClient();
            var quantity = 3;

            // Act
            var response = await _client.PutAsJsonAsync($"api/Cart/{userId}/items/{itemId}?quantity={quantity}", new { });

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Test]
        public async Task UpdateItemQuantity_ShouldNotUpdateWithInvalidItemId()
        {
            // Arrange
            var userId = await GetFirstUserId();
            var itemId = Guid.NewGuid();
            var _client = Server.CreateClient();
            var quantity = 3;

            // Act
            var response = await _client.PutAsJsonAsync($"api/Cart/{userId}/items/{itemId}?quantity={quantity}", new { });

            // Assert
            Assert.That(response.IsSuccessStatusCode, Is.False);
        }

        [Test]
        public async Task RemoveItemFromCart_ShouldRemoveItemSuccessfully()
        {
            // Arrange
            var userId = await GetFirstUserId();
            var itemId = await GetFirstCartItemId(userId);
            var _client = Server.CreateClient();

            // Act
            var response = await _client.DeleteAsync($"api/Cart/{userId}/items/{itemId}");

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Test]
        public async Task ClearCart_ShouldClearCartSuccessfully()
        {
            // Arrange
            var userId = await GetFirstUserId();
            var _client = Server.CreateClient();

            // Act
            var response = await _client.DeleteAsync($"api/Cart/{userId}");

            // Assert
            response.EnsureSuccessStatusCode();
        }

        private async Task<Guid> GetFirstUserId()
        {
            return Guid.NewGuid();
        }

        private async Task<Guid> GetFirstCartItemId(Guid userId)
        {
            var _client = Server.CreateClient();
            var response = await _client.GetAsync($"api/Cart/{userId}");
            response.EnsureSuccessStatusCode();
            var cart = await response.Content.ReadFromJsonAsync<Cart>();
            return cart.Items.First().Id;
        }
    }
}
