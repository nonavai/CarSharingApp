using CarSharingApp.CarService.Application.Commands.CarCommands;
using CarSharingApp.Common.Messages;
using MassTransit;
using MediatR;

namespace CarSharingApp.CarService.WebAPI.Consumers;

public class DeleteCarConsumer : IConsumer<UserMessage>
{
    private readonly IMediator _mediator;

    public DeleteCarConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<UserMessage> context)
    {
        await _mediator.Send(context.Message);
    }
}