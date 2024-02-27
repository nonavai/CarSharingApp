using CarSharingApp.CarService.Application.DTO_s.Car;
using MediatR;

namespace CarSharingApp.CarService.Application.Queries.CarQueries;

public class GetCarQuery : IRequest<CarFullDto>
{
    public string Id { get; set; }
}