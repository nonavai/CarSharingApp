using CarSharingApp.Common.Enums;
using CarSharingApp.Common.Messages;
using MassTransit;

namespace CarSharingApp.DealService.BusinessLogic.Producers;

public class UpdateCarStatusProducer
{
    private readonly IBus _bus;

    public UpdateCarStatusProducer(IBus bus)
    {
        _bus = bus;
    }

    public async Task UpdateCarStatus(string carId, Status newStatus)
    {
        var newCarState = new CarStatusMessage
        {
            CarId = carId,
            Status = newStatus
        };
        
        await _bus.Publish(newCarState);
    }
}