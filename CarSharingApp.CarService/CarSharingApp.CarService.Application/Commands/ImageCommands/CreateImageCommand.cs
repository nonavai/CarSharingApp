using CarSharingApp.CarService.Application.DTO_s.Image;
using MediatR;

namespace CarSharingApp.CarService.Application.Commands.ImageCommands;

public class CreateImageCommand : IRequest<ImageDto>
{
    public string CarId { get; set; }
    public string Url { get; set; }
    public bool IsPrimary { get; set; }
    public Stream File { get; set; }
}