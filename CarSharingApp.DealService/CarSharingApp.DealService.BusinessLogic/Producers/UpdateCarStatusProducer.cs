using CarSharingApp.DealService.Shared.Enums;
using MassTransit;
using Newtonsoft.Json;

namespace CarSharingApp.DealService.BusinessLogic.Producers;

public class UpdateCarStatusProducer 
{
    private readonly IBus _bus;

    public UpdateCarStatusProducer(IBus bus)
    {
        _bus = bus;
    }

    public async Task UpdateCarStatusAsync(string carId, Status newStatus)
    {
        var newCarState = new
        {
            CarId = carId,
            Status = newStatus
        };
        var data = JsonConvert.SerializeObject(newCarState);
        
        await _bus.Publish(newCarState);
    }
}