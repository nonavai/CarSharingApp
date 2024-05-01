using CarSharingApp.Common.Messages;
using MassTransit;

namespace CarSharingApp.Identity.BusinessLogic.Producers;

public class UserDeletedProducer
{
    private readonly IBus _bus;

    public UserDeletedProducer(IBus bus)
    {
        _bus = bus;
    }

    public async Task DeleteCarsByUser(string userId)
    {
        var command = new UserMessage
        {
            Id = userId
        };

        await _bus.Publish(command);
    }
}