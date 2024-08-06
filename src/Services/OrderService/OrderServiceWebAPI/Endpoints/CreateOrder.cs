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

            return Results.Created($"/orders/{response.Id}", response);
        })
        .WithName("CreateOrder")
        .Produces<CreateOrderResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Order")
        .WithDescription("Create Order");
    }
}