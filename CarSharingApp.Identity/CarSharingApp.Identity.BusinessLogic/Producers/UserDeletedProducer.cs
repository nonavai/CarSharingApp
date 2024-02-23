using CarSharingApp.Identity.BusinessLogic.Models.Car;
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
        var command = new DeleteCarByUser
        {
            UserId = userId
        };

        await _bus.Publish(command);
    }
}