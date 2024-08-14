using CatalogService.Contracts;
using MassTransit;

namespace RecommendationService.IntegrationTests
{
	public class CatalogTestIntegrationEventPublisher
	{
		private readonly IBusControl _busControl;

		public CatalogTestIntegrationEventPublisher(IBusControl busControl)
		{
			_busControl = busControl;
		}

		public async Task PublishBookCreatedEvent(BookCreatedEvent eventMessage)
		{
			await _busControl.Publish(eventMessage);
		}

		public async Task PublishBookUpdatedEvent(BookUpdatedEvent eventMessage)
		{
			await _busControl.Publish(eventMessage);
		}

		public async Task PublishCategoryCreatedEvent(CategoryCreatedEvent eventMessage)
		{
			await _busControl.Publish(eventMessage);
		}

		public async Task PublishCategoryUpdatedEvent(CategoryUpdatedEvent eventMessage)
		{
			await _busControl.Publish(eventMessage);
		}
	}
}
