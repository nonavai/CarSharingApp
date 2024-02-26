namespace CarSharingApp.CarService.Domain.Entities;

public class CarImage : BaseEntity
{
    public string CarId { get; set; }
    public string Url { get; set; }
    public bool IsPrimary { get; set; }
    public Car Car { get; set; }
}