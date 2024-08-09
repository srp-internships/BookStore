using Microsoft.AspNetCore.Http;
using Moq;
using ReviewService.WebApi.Middlewares;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewService.WebApi.UnitTests.xUnit.Middlewares
{
    public class RateLimitingMiddlewareTests
    {
        private readonly RateLimitingMiddleware _middleware;
        private readonly Mock<RequestDelegate> _nextMock;

        public RateLimitingMiddlewareTests()
        {
            _nextMock = new Mock<RequestDelegate>();
            _middleware = new RateLimitingMiddleware(_nextMock.Object);
            RateLimitingMiddleware.RequestTimes.Clear(); // Сброс состояния перед каждым тестом
        }

        [Fact]
        public async Task Should_Return429_When_RequestExceedsLimit()
        {
            // Arrange
            var context = new DefaultHttpContext();
            context.Connection.RemoteIpAddress = System.Net.IPAddress.Parse("127.0.0.1");
            var ipAddress = context.Connection.RemoteIpAddress.ToString();

            // Simulate exceeding the limit
            for (int i = 0; i < RateLimitingMiddleware.MaxRequests; i++)
            {
                // Adding request times to exceed the limit
                RateLimitingMiddleware.RequestTimes[ipAddress] = new ConcurrentQueue<DateTime>(
                    Enumerable.Repeat(DateTime.UtcNow, RateLimitingMiddleware.MaxRequests)
                );
                await _middleware.InvokeAsync(context);
            }

            // Act
            await _middleware.InvokeAsync(context);

            // Assert
            Assert.Equal(StatusCodes.Status429TooManyRequests, context.Response.StatusCode);
        }

        [Fact]
        public async Task Should_CallNextMiddleware_When_RequestIsWithinLimit()
        {
            // Arrange
            var context = new DefaultHttpContext();
            context.Connection.RemoteIpAddress = System.Net.IPAddress.Parse("127.0.0.1");
            _nextMock.Setup(m => m(It.IsAny<HttpContext>())).Returns(Task.CompletedTask);

            // Act
            await _middleware.InvokeAsync(context);

            // Assert
            _nextMock.Verify(m => m(It.IsAny<HttpContext>()), Times.Once);
            Assert.NotEqual(StatusCodes.Status429TooManyRequests, context.Response.StatusCode);
        }

        [Fact]
        public async Task Should_RemoveOldRequests_And_AllowNewRequests()
        {
            // Arrange
            var context = new DefaultHttpContext();
            context.Connection.RemoteIpAddress = System.Net.IPAddress.Parse("127.0.0.1");
            var ipAddress = context.Connection.RemoteIpAddress.ToString();

            // Simulate old request
            RateLimitingMiddleware.RequestTimes[ipAddress] = new ConcurrentQueue<DateTime>();
            for (int i = 0; i < RateLimitingMiddleware.MaxRequests; i++)
            {
                RateLimitingMiddleware.RequestTimes[ipAddress].Enqueue(DateTime.UtcNow - TimeSpan.FromMinutes(2));
            }

            _nextMock.Setup(m => m(It.IsAny<HttpContext>())).Returns(Task.CompletedTask);

            // Act
            await _middleware.InvokeAsync(context);

            // Assert
            _nextMock.Verify(m => m(It.IsAny<HttpContext>()), Times.Once);
            Assert.NotEqual(StatusCodes.Status429TooManyRequests, context.Response.StatusCode);
        }
    }
}
