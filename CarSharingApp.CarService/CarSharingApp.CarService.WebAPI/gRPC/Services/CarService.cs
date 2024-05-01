using CarService;
using CarSharingApp.CarService.Application.Commands.CarStateCommands;
using CarSharingApp.CarService.Application.Queries.CarStateQueries;
using CarSharingApp.CarService.WebAPI.Hubs;
using Grpc.Core;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Status = CarSharingApp.CarService.Domain.Enums.Status;

namespace CarSharingApp.CarService.WebAPI.gRPC.Services;

public class CarService : Car.CarBase
{
    private readonly IMediator _mediator;
    private readonly IHubContext<CarStateHub> _hubContext;

    public CarService(IMediator mediator, IHubContext<CarStateHub> hubContext)
    {
        _mediator = mediator;
        _hubContext = hubContext;
    }

    public override async Task<CarAvailableResponse> IsCarAvailable(CarAvailableRequest request, ServerCallContext context)
    {
        var carStateDto = await _mediator.Send(new GetCarStateQuery
        {
            CarId = request.UserId
        });

        return await Task.FromResult(new CarAvailableResponse
        {
            IsAvailable = carStateDto.Status == Status.Free
        });
    }

    public override async Task<ChangeCarStatusResponse> ChangeCarStatus(ChangeStatus request, ServerCallContext context)
    {
        var command = new UpdateCarStatusCommand
        {
            CarId = request.CarId,
            Status = (Status)request.Status
        };
        var result = await _mediator.Send(command);
        await _hubContext.Clients.Group($"{command.CarId}/status").SendAsync("ReceiveCarStatusUpdate", command.Status);
        
        return new ChangeCarStatusResponse
        {
            IsChanged = result.Status != command.Status
        };
        
    }
}