namespace CarSharingApp.CarService.Application.Responses.Image;

public class ImageCommandResponse
{
    public string Id { get; set; }
    public string CarId { get; set; }
    public string Url { get; set; }
    public bool IsPrimary { get; set; }
}