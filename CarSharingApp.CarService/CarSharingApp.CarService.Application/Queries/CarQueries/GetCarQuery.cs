using CarSharingApp.CarService.Application.DTO_s.Car;
using CarSharingApp.CarService.Application.Responses.Car;
using MediatR;

namespace CarSharingApp.CarService.Application.Queries.CarQueries;

public class GetCarQuery : IRequest<CarFullResponse>
{
    public string Id { get; set; }
}