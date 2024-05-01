using CarSharingApp.CarService.Application.DTO_s.CarState;
using CarSharingApp.CarService.Application.DTO_s.Comment;
using CarSharingApp.CarService.Domain.Enums;

namespace CarSharingApp.CarService.Application.DTO_s.Car;

public class CarFullDto
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public int Year { get; set; }
    public string RegistrationNumber { get; set; }
    public string Mark { get; set; }
    public string Model { get; set; }
    public float Price { get; set; }
    public FuelType FuelType { get; set; }
    public VehicleType VehicleType  { get; set; }
    public string Color  { get; set; }
    public WheelDrive WheelDrive { get; set; }
    public float EngineCapacity { get; set; }
    public string? Description { get; set; }
    
    public CarStateDto CarState { get; set; }
}