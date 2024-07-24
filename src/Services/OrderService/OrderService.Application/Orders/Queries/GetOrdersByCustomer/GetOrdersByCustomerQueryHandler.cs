namespace OrderService.Application.Orders.Queries.GetOrdersByCustomer;

//public class GetOrdersByCustomerHandler(IApplicationDbContext dbContext)
//    : IQueryHandler<GetOrdersByCustomerQuery, GetOrdersByCustomerResult>
//{
//    public async Task<GetOrdersByCustomerResult> Handle(GetOrdersByCustomerQuery query, CancellationToken cancellationToken)
//    {
//        get orders by customer using dbContext
//         return result

//        var orders = await dbContext.Orders
//                        .Include(o => o.Items)
//                        .AsNoTracking()
//                        .Where(((query.CustomerId)))
//                        .ToListAsync(cancellationToken);

//        return new GetOrdersByCustomerResult(orders.ToOrderDtoList());
//    }
//}
