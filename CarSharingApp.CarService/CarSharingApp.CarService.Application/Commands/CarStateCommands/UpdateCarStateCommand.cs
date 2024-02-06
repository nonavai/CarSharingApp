using CarSharingApp.CarService.Application.DTO_s.CarState;

namespace CarSharingApp.CarService.Application.Commands.CarStateCommands;

public class UpdateCarStateCommand : CarStateDto
{
    public string CarId { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}