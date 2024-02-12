using CarSharingApp.CarService.Application.DTO_s.Image;
using CarSharingApp.CarService.Application.Responses.Image;
using MediatR;

namespace CarSharingApp.CarService.Application.Commands.ImageCommands;

public class UpdateImagePriorityCommand : IRequest<ImageCommandResponse>
{
    public string Id { get; set; }
    public bool IsPrimary { get; set; }
}