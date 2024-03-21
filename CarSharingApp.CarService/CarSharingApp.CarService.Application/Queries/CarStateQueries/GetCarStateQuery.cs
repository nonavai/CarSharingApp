using CarSharingApp.CarService.Application.DTO_s.CarState;
using MediatR;

namespace CarSharingApp.CarService.Application.Queries.CarStateQueries;

public class GetCarStateQuery : IRequest<CarStateDto>
{
    public string CarId { get; set; }
}