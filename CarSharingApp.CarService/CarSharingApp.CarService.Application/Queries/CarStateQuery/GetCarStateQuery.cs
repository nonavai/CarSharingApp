using CarSharingApp.CarService.Domain.Enums;

namespace CarSharingApp.CarService.Application.Queries.CarStateQuery;

public class GetCarStateQuery
{
    public string CarId { get; set; }
    public Status Status { get; set; }
    public float Latitude { get; set; }
    public float Longitude { get; set; }
}