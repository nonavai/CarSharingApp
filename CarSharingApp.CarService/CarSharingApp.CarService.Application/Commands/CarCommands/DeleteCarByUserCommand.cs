using CarSharingApp.CarService.Application.DTO_s.Car;
using MediatR;

namespace CarSharingApp.CarService.Application.Commands.CarCommands;

public class DeleteCarByUserCommand : IRequest<IEnumerable<CarDto>>
{
    public string UserId { get; set; }
}