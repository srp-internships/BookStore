using MassTransit;
using OrderService.Application.UseCases.Orders.Commands.CreateOrder;
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
                OrderId: orderId,
                CustomerId: command.CustomerId,
                ShippingAddress: new Address(
                    FirstName: command.ShippingAddress.FirstName,
                    LastName: command.ShippingAddress.LastName,
                    EmailAddress: command.ShippingAddress.EmailAddress,
                    Country: command.ShippingAddress.Country,
                    State: command.ShippingAddress.State,
                    Street: command.ShippingAddress.Street
                ),
                Status: command.Status.ToIntegrationEnum(),
                Items: command.Items.Select(i => new OrderItem(
                    BookId: i.BookId,
                    SellerId: i.SellerId,
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

public static class DomainIntegrationEnumsConverter
{
    public static OrderStatus ToIntegrationEnum(this OrderService.Domain.Enums.OrderStatus status)
    {
        return status switch
        {
            OrderService.Domain.Enums.OrderStatus.Failed => OrderStatus.Failed,
            OrderService.Domain.Enums.OrderStatus.Completed => OrderStatus.Completed,
            OrderService.Domain.Enums.OrderStatus.PaymentProcessing => OrderStatus.PaymentProcessing,
            OrderService.Domain.Enums.OrderStatus.ShipmentProcessing => OrderStatus.ShipmentProcessing,
            _ => throw new InvalidCastException($"Not matchable domain enum value detected: {status}"),
        };
    }

    public static OrderService.Domain.Enums.OrderStatus ToDomainEnum(this OrderStatus status)
    {
        return status switch
        {
            OrderStatus.Failed => OrderService.Domain.Enums.OrderStatus.Failed,
            OrderStatus.Completed => OrderService.Domain.Enums.OrderStatus.Completed,
            OrderStatus.PaymentProcessing => OrderService.Domain.Enums.OrderStatus.PaymentProcessing,
            OrderStatus.ShipmentProcessing => OrderService.Domain.Enums.OrderStatus.ShipmentProcessing,
            _ => throw new InvalidCastException($"Not matchable domain enum value detected: {status}"),
        };
    }
}
