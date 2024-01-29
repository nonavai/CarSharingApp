using CarSharingApp.CarService.Domain.Enums;

namespace CarSharingApp.CarService.Domain.Entities;

public class Car : BaseEntity
{
    public Guid UserId { get; set; }
    public int Year { get; set; }
    public string RegistrationNumber { get; set; }
    public string Mark { get; set; }
    public string Model { get; set; }
    public float Price { get; set; }
    public FuelType FuelType { get; set; }
    public VehicleType VehicleType  { get; set; }
    public string VehicleBody { get; set; }
    public string Color  { get; set; }
    public bool IsActive { get; set; }

    public IEnumerable<Comment> Comments { get; set; }
    public CarState CarState { get; set; }
    public IEnumerable<Picture> Pictures { get; set; }
}