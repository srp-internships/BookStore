using MassTransit;
using OrderService.IntegrationEvents;

namespace OrderServiceWebAPI.Endpoints;

public record CreateOrderResponse(Guid Id);

public class CreateOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/orders", async (IPublishEndpoint publishEndpoint, CreateOrderCommand command, ISender sender) =>
        {
            var request = command.Adapt<CreateOrderCommand>();

            var result = await sender.Send(request);

            var response = result.Adapt<CreateOrderResponse>();

            var orderId = response.Id;

            var orderProcessedEvent = new OrderProcessedIntegrationEvent(
                Id: orderId,
                CustomerId: command.CustomerId,
                ShippingAddress: new Address(
                    FirstName: command.ShippingAddress.FirstName,
                    LastName: command.ShippingAddress.LastName,
                    EmailAddress: command.ShippingAddress.EmailAddress,
                    Country: command.ShippingAddress.Country,
                    State: command.ShippingAddress.State
                ),
                Status: "Processed",
                OrderItems: command.Items.Select(i => new OrderItem(
                    OrderId: orderId,
                    BookId: i.BookId,
                    Quantity: i.Quantity,
                    Price: i.Price
                )).ToList(),
                TotalPrice: command.Items.Sum(i => i.Quantity * i.Price)
            );

            await publishEndpoint.Publish(orderProcessedEvent);

            return Results.Created($"/orders/{response.Id}", response);
        })
        .WithName("CreateOrder")
        .Produces<CreateOrderResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Order")
        .WithDescription("Create Order");
    }
}
