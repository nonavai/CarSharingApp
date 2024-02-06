using CarSharingApp.CarService.Application.DTO_s.Image;
using MediatR;

namespace CarSharingApp.CarService.Application.Commands.ImageCommands;

public class UpdateImagePriorityCommand : IRequest<ImageDto>
{
    public string Id { get; set; }
    public bool IsPrimary { get; set; }
}