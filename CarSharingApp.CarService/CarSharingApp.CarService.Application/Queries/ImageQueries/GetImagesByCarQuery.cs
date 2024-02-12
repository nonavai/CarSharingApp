using CarSharingApp.CarService.Application.DTO_s.Image;
using CarSharingApp.CarService.Application.Responses.Image;
using MediatR;

namespace CarSharingApp.CarService.Application.Queries.ImageQueries;

public class GetImagesByCarQuery : IRequest<IEnumerable<ImageQueryResponse>>
{
    public string CarId { get; set; }
}