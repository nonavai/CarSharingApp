using CarSharingApp.CarService.Application.Responses.Image;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CarSharingApp.CarService.Application.Commands.ImageCommands;

public class CreateImageCommand : IRequest<ImageCommandResponse>
{
    public string CarId { get; set; }
    public string Url { get; set; }
    public bool IsPrimary { get; set; }
    public IFormFile File { get; set; }
}