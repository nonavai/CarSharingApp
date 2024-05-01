namespace CarSharingApp.CarService.Application.DTO_s.Image;

public class ImageFullDto
{
    public string Id { get; set; }
    public string CarId { get; set; }
    public string Url { get; set; }
    public bool IsPrimary { get; set; }
    public string File { get; set; }
}