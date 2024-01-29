namespace CarSharingApp.CarService.Domain.Entities;

public class CarState : BaseEntity
{
    public Guid CarId { get; set; }
    public float Latitude { get; set; }
    public float Longitude { get; set; }
    public Car Car { get; set; }
}