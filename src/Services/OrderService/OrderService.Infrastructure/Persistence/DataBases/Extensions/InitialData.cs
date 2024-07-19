using OrderService.Domain.Entities;
using OrderService.Domain.ValueObjects;

namespace OrderService.Infrastructure.Persistence.DataBases.Extensions;

internal class InitialData
{
    public static IEnumerable<Customer> Customers =>
    new List<Customer>
    {
        Customer.Create(CustomerId.Of(new Guid("58c49479-ec65-4de2-86e7-033c546291aa")), "alice", "alice@example.com"),
        Customer.Create(CustomerId.Of(new Guid("189dc8dc-990f-48e0-a37b-e6f2b60b9d7d")), "bob", "bob@example.com")
    };

    public static IEnumerable<Book> Books =>
        new List<Book>
        {
            Book.Create(BookId.Of(new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61")), "The Great Gatsby", 20),
            Book.Create(BookId.Of(new Guid("c67d6323-e8b1-4bdf-9a75-b0d0d2e7e914")), "1984", 15),
            Book.Create(BookId.Of(new Guid("4f136e9f-ff8c-4c1f-9a33-d12f689bdab8")), "To Kill a Mockingbird", 18),
            Book.Create(BookId.Of(new Guid("6ec1297b-ec0a-4aa1-be25-6726e3b51a27")), "Moby Dick", 25)
        };

    public static IEnumerable<Order> OrdersWithItems
    {
        get
        {
            var address1 = Address.Of("alice", "smith", "alice@example.com", "123 Main St", "USA", "New York", "10001");
            var address2 = Address.Of("bob", "johnson", "bob@example.com", "456 Elm St", "USA", "Los Angeles", "90001");

            var payment1 = Payment.Of("alice", "4111111111111111", "12/25", "123", 1);
            var payment2 = Payment.Of("bob", "4222222222222222", "06/26", "456", 2);

            var order1 = Order.Create(
                            OrderId.Of(Guid.NewGuid()),
                            CustomerId.Of(new Guid("58c49479-ec65-4de2-86e7-033c546291aa")),
                            shippingAddress: address1,
                            billingAddress: address1,
                            payment1);
            order1.Add(BookId.Of(new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61")), 1, 20);
            order1.Add(BookId.Of(new Guid("c67d6323-e8b1-4bdf-9a75-b0d0d2e7e914")), 2, 15);

            var order2 = Order.Create(
                            OrderId.Of(Guid.NewGuid()),
                            CustomerId.Of(new Guid("189dc8dc-990f-48e0-a37b-e6f2b60b9d7d")),
                            shippingAddress: address2,
                            billingAddress: address2,
                            payment2);
            order2.Add(BookId.Of(new Guid("4f136e9f-ff8c-4c1f-9a33-d12f689bdab8")), 1, 18);
            order2.Add(BookId.Of(new Guid("6ec1297b-ec0a-4aa1-be25-6726e3b51a27")), 1, 25);

            return new List<Order> { order1, order2 };
        }
    }
}
