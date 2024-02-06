using CarSharingApp.CarService.Application.DTO_s.Car;
using CarSharingApp.CarService.Application.DTO_s.CarState;
using CarSharingApp.CarService.Domain.Enums;
using MediatR;

namespace CarSharingApp.CarService.Application.Commands.CarCommands;

public class UpdateCarActivityCommand :IRequest<CarDto>, IRequest<CarStateDto>
{
    public string CarId { get; set; }
    public Status Status { get; set; }
}