using CarSharingApp.CarService.Application.DTO_s.Car;
using CarSharingApp.CarService.Application.Responses.Car;
using MediatR;

namespace CarSharingApp.CarService.Application.Commands.CarCommands;

public class DeleteCarCommand : IRequest<CarResponse>
{
    public string Id { get; set; }
}