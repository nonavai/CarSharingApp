namespace CarSharingApp.CarService.Application.CommandHandlers;

public interface ICommandHandler<T, M>
{
    Task<M> Handle(T command);
}