namespace OrderService.Application.Common.Interfaces.CQRC;

public interface IQuery<out TResponse> : IRequest<TResponse>
    where TResponse : notnull
{
}
