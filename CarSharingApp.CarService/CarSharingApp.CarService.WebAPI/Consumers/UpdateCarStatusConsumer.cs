using CarSharingApp.CarService.Application.Commands.CarStateCommands;
using CarSharingApp.CarService.WebAPI.Hubs;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace CarSharingApp.CarService.WebAPI.Consumers;

public class UpdateCarStatusConsumer : IConsumer<UpdateCarStatusCommand>
{
    private readonly IHubContext<CarStateHub> _hubContext;
    private readonly IMediator _mediator;

    public UpdateCarStatusConsumer(IHubContext<CarStateHub> hubContext, IMediator mediator)
    {
        _hubContext = hubContext;
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<UpdateCarStatusCommand> context)
    {
        await _mediator.Send(context.Message);
        await _hubContext.Clients.Group($"{context.Message.CarId}/status").SendAsync("ReceiveCarStatusUpdate", context.Message.Status);
    }
}