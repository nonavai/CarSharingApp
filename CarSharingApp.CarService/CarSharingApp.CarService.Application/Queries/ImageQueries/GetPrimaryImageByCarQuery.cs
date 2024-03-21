using CarSharingApp.CarService.Application.DTO_s.Image;
using MediatR;

namespace CarSharingApp.CarService.Application.Queries.ImageQueries;

public class GetPrimaryImageByCarQuery: IRequest<ImageFullDto>
{
    public string CarId { get; set; }
}