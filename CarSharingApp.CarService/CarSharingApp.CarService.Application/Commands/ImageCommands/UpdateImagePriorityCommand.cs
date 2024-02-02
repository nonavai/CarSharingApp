namespace CarSharingApp.CarService.Application.Commands.ImageCommands;

public class UpdateImagePriorityCommand
{
    public string Id { get; set; }
    public bool IsPrimary { get; set; }
}