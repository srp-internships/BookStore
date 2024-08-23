namespace OrderService.Application.Common.Interfaces.CQRC;

public interface ICommand : ICommand<Unit>
{
}

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}
