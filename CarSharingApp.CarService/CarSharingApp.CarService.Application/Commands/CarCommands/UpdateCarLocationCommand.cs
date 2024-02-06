using CarSharingApp.CarService.Application.DTO_s.CarState;
using MediatR;

namespace CarSharingApp.CarService.Application.Commands.CarCommands;

public class UpdateCarLocationCommand : IRequest<CarStateDto>
{
    public string CarId { get; set; }
    public float Latitude { get; set; }
    public float Longitude { get; set; }
}