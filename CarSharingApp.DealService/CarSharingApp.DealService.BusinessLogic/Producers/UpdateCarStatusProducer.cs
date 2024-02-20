using CarSharingApp.DealService.BusinessLogic.Models.CarState;
using CarSharingApp.DealService.Shared.Enums;
using MassTransit;

namespace CarSharingApp.DealService.BusinessLogic.Producers;

public class UpdateCarStatusProducer
{
    private readonly IBus _bus;

    public UpdateCarStatusProducer(IBus bus)
    {
        _bus = bus;
    }

    public async Task UpdateCarStatus(string carId, CarStatus newStatus)
    {
        var newCarState = new CarState
        {
            CarId = carId,
            Status = newStatus
        };
        
        await _bus.Publish(newCarState, context =>
        {
            context.SetRoutingKey("state");
        });
    }
}