using CarService;
using CarSharingApp.CarService.Application.Queries.CarStateQueries;
using Grpc.Core;
using MediatR;
using Status = CarSharingApp.CarService.Domain.Enums.Status;

namespace CarSharingApp.CarService.WebAPI.gRPC.Services;

public class CarService : Car.CarBase
{
    private readonly IMediator _mediator;

    public CarService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override async Task<CarAvailableResponse> IsCarAvailable(CarAvailableRequest request, ServerCallContext context)
    {
        var carStateDto = await _mediator.Send(new GetCarStateQuery
        {
            CarId = request.UserId
        });

        if (carStateDto.Status == Status.Free)
        {
            return await Task.FromResult(new CarAvailableResponse
            {
                IsAvailable = true
            });
        }
        
        return await Task.FromResult(new CarAvailableResponse
        {
            IsAvailable = false
        });
    }
}