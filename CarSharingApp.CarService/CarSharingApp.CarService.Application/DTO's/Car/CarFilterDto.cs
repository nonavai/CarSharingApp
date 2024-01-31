using CarSharingApp.CarService.Domain.Enums;

namespace CarSharingApp.CarService.Application.DTO_s.Car;

public class CarFilterDto
{
    public bool? IsActive { get; set; }
    public double? RadiusKm { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public int? MinYear { get; set; }
    public int? MaxYear { get; set; }
    public int? MinPrice { get; set; }
    public int? MaxPrice { get; set; }
    public VehicleType? VehicleType { get; set; }
    public FuelType? FuelType { get; set; }
    public string? Mark { get; set; }
    public string? Model { get; set; }
}