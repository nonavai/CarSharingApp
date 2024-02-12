using CarSharingApp.CarService.Application.Responses.Image;
using MediatR;

namespace CarSharingApp.CarService.Application.Commands.ImageCommands;

public class DeleteImageCommand : IRequest<ImageCommandResponse>
{
    public string Id { get; set; }
}