using CarSharingApp.CarService.Application.Commands.CarCommands;
using MassTransit;
using MediatR;

namespace CarSharingApp.CarService.WebAPI.Consumers;

public class DeleteCarConsumer : IConsumer<DeleteCarByUserCommand>
{
    private readonly IMediator _mediator;

    public DeleteCarConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<DeleteCarByUserCommand> context)
    {
        await _mediator.Send(context.Message);
    }
}