using CarSharingApp.CarService.Application.DTO_s.Image;
using MediatR;

namespace CarSharingApp.CarService.Application.Commands.ImageCommands;

public class DeleteImageCommand : IRequest<ImageDto>
{
    public string Id { get; set; }
}