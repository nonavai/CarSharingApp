namespace CarSharingApp.CarService.Application.Responses.Image;

public class ImageQueryResponse
{
    public string CarId { get; set; }
    public string Url { get; set; }
    public bool IsPrimary { get; set; }
    public string File { get; set; }
}