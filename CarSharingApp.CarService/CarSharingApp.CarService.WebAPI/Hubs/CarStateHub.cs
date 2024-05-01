using CarSharingApp.CarService.Application.Commands.CarStateCommands;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace CarSharingApp.CarService.WebAPI.Hubs;

public class CarStateHub : Hub
{
    private readonly IMediator _mediator;

    public CarStateHub(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task ConnectToCar(string carId, string type)
    {
        var connectionId = Context.ConnectionId;
        
        await Groups.AddToGroupAsync(connectionId, $"{carId}/{type}");
    }
    
    public async Task DisconnectFromCar(string carId, string type)
    {
        var connectionId = Context.ConnectionId;
        
        await Groups.RemoveFromGroupAsync(connectionId, $"{carId}/{type}");
    }
    
    public async Task SendLocation(UpdateCarLocationCommand command)
    {
        await Clients.Group($"{command.CarId}/location")
            .SendAsync("ReceiveCarLocationUpdate", command.Latitude, command.Longitude); 
        await _mediator.Send(command);
    }

    public async Task SendStatus(UpdateCarStatusCommand command)
    {
        await Clients.Group($"{command.CarId}/status")
            .SendAsync("ReceiveCarStatusUpdate", command.Status); 
        await _mediator.Send(command);
    }
    
    
}