using CarSharingApp.CarService.Domain.Enums;

namespace CarSharingApp.CarService.Domain.Entities;

public class CarState : BaseEntity
{
    public string CarId { get; set; }
    public Status Status { get; set; }
    public bool IsActive { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public Car Car { get; set; }
}