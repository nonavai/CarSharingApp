using Shared.Enums;

namespace CarSharingAPI.Requests.Car;

public class CarRequest
{
    public bool? IsActive { get; set; }
    public float? Latitude { get; set; }
    public float? Longitude { get; set; }
    public int? Year { get; set; }
    public string? RegistrationNumber { get; set; }
    public string? Mark { get; set; }
    public string? Model { get; set; }
    public float? Price { get; set; }
    public FuelType? FuelType { get; set; }
    public VehicleType? VehicleType  { get; set; }
    public string? VehicleBody { get; set; }
    public string? Color  { get; set; }
}