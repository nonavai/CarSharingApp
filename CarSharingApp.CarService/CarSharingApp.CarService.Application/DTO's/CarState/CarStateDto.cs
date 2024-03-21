using CarSharingApp.CarService.Domain.Enums;

namespace CarSharingApp.CarService.Application.DTO_s.CarState;

public class CarStateDto
{
    public string Id { get; set; }
    public string CarId { get; set; }
    public Status Status { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}