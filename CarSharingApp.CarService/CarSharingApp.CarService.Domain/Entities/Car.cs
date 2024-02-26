using CarSharingApp.CarService.Domain.Enums;

namespace CarSharingApp.CarService.Domain.Entities;

public class Car : BaseEntity
{
    public string UserId { get; set; }
    public int Year { get; set; }
    public string RegistrationNumber { get; set; }
    public string Mark { get; set; }
    public string Model { get; set; }
    public float Price { get; set; }
    public FuelType FuelType { get; set; }
    public VehicleType VehicleType  { get; set; }
    public string VehicleBody { get; set; }
    public string Color  { get; set; }
    public WheelDrive WheelDrive { get; set; }
    public float EngineCapacity { get; set; }
    public string? Description { get; set; }

    public IEnumerable<Comment> Comments { get; set; }
    public CarState CarState { get; set; }
    public IEnumerable<CarImage> CarImage { get; set; }
}