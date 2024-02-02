namespace CarSharingApp.CarService.Application.Commands.CarStateCommands;

public class UpdateCarStateCommand
{
    public string CarId { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}