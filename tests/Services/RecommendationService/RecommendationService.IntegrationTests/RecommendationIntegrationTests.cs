using CatalogService.Contracts;
using MassTransit.Testing;
using Microsoft.Extensions.DependencyInjection;
using RecommendationService.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationService.IntegrationTests
{
	public class RecommendationIntegrationTests
	{
		private readonly ServiceProvider DI;
		private readonly ITestHarness _harness;

		public RecommendationIntegrationTests()
		{
			var services = ExtensionMethods.CreateServiceProvider();
			services.RegisterInMemoryDbContext();
			services.RegisterMassTransit();
			DI = services.BuildServiceProvider();
			_harness = DI.GetRequiredService<ITestHarness>();
		}

		[Fact]
		public async Task BookCreatedEvent_Should_CorrectlyHandled()
		{
			// Arrange
			await _harness.Start();
			var bookCreatedEvent = new BookCreatedEvent()
			{
				Id = Guid.NewGuid(),
				AuthorIds = [Guid.NewGuid(), Guid.NewGuid()],
				CategoryIds = [Guid.NewGuid(), Guid.NewGuid()],
				Image = "example",
				Title = "title example"
			};
			var publisher = DI.GetRequiredService<CatalogTestIntegrationEventPublisher>();
			var db = DI.GetRequiredService<RecommendationDbContext>();

			// Act
			await publisher.PublishBookCreatedEvent(bookCreatedEvent);

			// Assert

			Assert.True((await _harness.Published.Any<BookCreatedEvent>()));
			await Task.Delay(1000);
			var createdBookFromDb = db.Books.FirstOrDefault(i => i.Id == bookCreatedEvent.Id);
			Assert.NotNull(createdBookFromDb);
			Assert.Equal(bookCreatedEvent.Title, createdBookFromDb.Title);

			await _harness.Stop();
		}
	}
}
