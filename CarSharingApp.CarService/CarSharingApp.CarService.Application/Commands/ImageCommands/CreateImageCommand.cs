namespace CarSharingApp.CarService.Application.Commands.ImageCommands;

public class CreateImageCommand
{
    public string CarId { get; set; }
    public string Url { get; set; }
    public bool IsPrimary { get; set; }
    public Stream File { get; set; }
}