namespace OrderService.Domain.Entities;

public class Book : Entity<BookId>
{
    public string Name { get; private set; } = default!;
    public decimal Price { get; private set; } = default!;

    public static Book Create(BookId id, string name, decimal price)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

        var book = new Book
        {
            Id = id,
            Name = name,
            Price = price
        };

        return book;
    }
}