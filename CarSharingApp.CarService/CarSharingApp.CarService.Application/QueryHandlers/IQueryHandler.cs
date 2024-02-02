namespace CarSharingApp.CarService.Application.QueryHandlers;

public interface IQueryHandler<T, M>
{
    Task<M> Handle(T query, CancellationToken token);
}