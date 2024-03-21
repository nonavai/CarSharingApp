using CarSharingApp.CarService.Application.DTO_s.Car;
using MediatR;

namespace CarSharingApp.CarService.Application.Commands.CarCommands;

public class DeleteCarCommand : IRequest<CarDto>
{
    public string Id { get; set; }
}