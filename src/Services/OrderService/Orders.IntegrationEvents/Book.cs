namespace Orders.IntegrationEvents;

public class Book
{
    public Guid Id { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public Guid SellerId { get; set; }

}
