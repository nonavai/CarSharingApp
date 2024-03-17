using CarSharingApp.CarService.Application.Commands.CarStateCommands;
using CarSharingApp.CarService.WebAPI.Hubs;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace CarSharingApp.CarService.WebAPI.Consumers;

public class UpdateCarStatusConsumer : IConsumer<string>
{
    private readonly IHubContext<CarStateHub> _hubContext;
    private readonly IMediator _mediator;

    public UpdateCarStatusConsumer(IHubContext<CarStateHub> hubContext, IMediator mediator)
    {
        _hubContext = hubContext;
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<string> context)
    {
        var command = JsonConvert.DeserializeObject<UpdateCarStatusCommand>(context.Message);
        await _mediator.Send(command);
        await _hubContext.Clients.Group($"{command.CarId}/status").SendAsync("ReceiveCarStatusUpdate", command.Status);
    }
}