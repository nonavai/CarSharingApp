using CarSharingApp.CarService.Application.DTO_s.Car;
using CarSharingApp.CarService.Domain.Enums;
using MediatR;

namespace CarSharingApp.CarService.Application.Queries.CarQueries;

public class GetCarsByParamsQuery : IRequest<IEnumerable<CarWithImageDto>>
{
    public bool IsActive { get; set; }
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
    public WheelDrive? WheelDrive { get; set; }
    public float? MinEngineCapacity { get; set; }
    public float? MaxEngineCapacity { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
}