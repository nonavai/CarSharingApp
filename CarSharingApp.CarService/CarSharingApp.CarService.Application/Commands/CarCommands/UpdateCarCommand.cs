using CarSharingApp.CarService.Domain.Enums;

namespace CarSharingApp.CarService.Application.Commands.CarCommands;

public class UpdateCarCommand
{
    public string Id { get; set; }
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