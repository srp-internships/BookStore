using OrderService.Domain.Exceptions;

namespace OrderService.Domain.ValueObjects;

public record BookId
{
    public Guid Value { get; }
    private BookId(Guid value) => Value = value;
    public static BookId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty)
        {
            throw new DomainException("BookId cannot be empty.");
        }

        return new BookId(value);
    }
}
