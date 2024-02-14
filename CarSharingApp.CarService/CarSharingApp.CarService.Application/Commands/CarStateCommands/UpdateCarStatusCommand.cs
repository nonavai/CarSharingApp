using CarSharingApp.CarService.Application.DTO_s.CarState;
using CarSharingApp.CarService.Domain.Enums;
using MediatR;

namespace CarSharingApp.CarService.Application.Commands.CarStateCommands;

public class UpdateCarStatusCommand : IRequest<CarStateDto>
{
    public string CarId { get; set; }
    public Status Status { get; set; }
}