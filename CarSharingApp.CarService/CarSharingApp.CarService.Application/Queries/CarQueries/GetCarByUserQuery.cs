using CarSharingApp.CarService.Application.DTO_s.Car;
using MediatR;

namespace CarSharingApp.CarService.Application.Queries.CarQueries;

public class GetCarByUserQuery : IRequest<IEnumerable<CarDto>>
{
    public string UserId { get; set; }
}