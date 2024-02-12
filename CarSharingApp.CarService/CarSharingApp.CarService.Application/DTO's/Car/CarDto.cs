using CarSharingApp.CarService.Application.DTO_s.CarState;
using CarSharingApp.CarService.Application.Responses.Comment;
using CarSharingApp.CarService.Application.Responses.Image;
using CarSharingApp.CarService.Domain.Enums;

namespace CarSharingApp.CarService.Application.DTO_s.Car;

public class CarDto
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
    public string VehicleBody { get; set; }
    public string Color  { get; set; }
    public WheelDrive WheelDrive { get; set; }
    public float EngineCapacity { get; set; }
    public string? Description { get; set; }
    
    public CarStateDto CarState { get; set; }
    public IEnumerable<CommentResponse> Comments { get; set; }
    public IEnumerable<ImageQueryResponse> CarImage { get; set; }
}