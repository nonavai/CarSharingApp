namespace CarSharingApp.CarService.Application.DTO_s.Image;

public class ImageDto
{
    public string CarId { get; set; }
    public string Url { get; set; }
    public bool IsPrimary { get; set; }
    public Stream File { get; set; }
}