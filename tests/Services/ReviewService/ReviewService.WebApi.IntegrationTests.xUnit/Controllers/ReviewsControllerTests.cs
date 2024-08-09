using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewService.WebApi.IntegrationTests.xUnit.Controllers
{
    public class ReviewsControllerTests : IClassFixture<WebApplicationFactory<ReviewService.WebApi.Program>>
    {
        private readonly HttpClient _client;

        public ReviewsControllerTests(WebApplicationFactory<ReviewService.WebApi.Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Post_Should_ReturnCreated_When_ReviewIsValid()
        {
            // Arrange
            var reviewDto = new
            {
                bookId = Guid.NewGuid(),
                userId = Guid.NewGuid(),
                comment = "This is a test review.",
                rating = 5
            };

            var content = new StringContent(JsonConvert.SerializeObject(reviewDto), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/reviews", content);

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
        }

        [Fact]
        public async Task Post_Should_ReturnNotFound_When_BookDoesNotExist()
        {
            // Arrange
            var reviewDto = new
            {
                bookId = Guid.NewGuid(), // Use a bookId that you know does not exist
                userId = Guid.NewGuid(),
                comment = "This is a test review.",
                rating = 5
            };

            var content = new StringContent(JsonConvert.SerializeObject(reviewDto), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/reviews", content);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetById_Should_ReturnReview_When_ReviewExists()
        {
            // Arrange
            var existingReviewId = Guid.NewGuid(); // Use an ID that exists in your test database or set up a fixture to insert a test review

            // Act
            var response = await _client.GetAsync($"/api/reviews/{existingReviewId}");

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task Delete_Should_ReturnOk_When_ReviewExists()
        {
            // Arrange
            var existingReviewId = Guid.NewGuid(); // Use an ID that exists in your test database

            // Act
            var response = await _client.DeleteAsync($"/api/reviews/{existingReviewId}");

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
    }
}
