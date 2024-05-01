using AutoMapper;
using CarService;
using CarSharingApp.DealService.BusinessLogic.Commands.DealCommands;
using CarSharingApp.DealService.BusinessLogic.Models.Deal;
using CarSharingApp.DealService.DataAccess.Repositories;
using CarSharingApp.DealService.Shared.Enums;
using CarSharingApp.DealService.Shared.Exceptions;
using Grpc.Core;
using Hangfire;
using MediatR;

namespace CarSharingApp.DealService.BusinessLogic.CommandHandlers.DealCommandHandlers;

public class ConfirmDealHandler : IRequestHandler<ConfirmDealCommand, DealDto>
{
    private readonly IDealRepository _dealRepository;
    private readonly IMapper _mapper;
    private readonly Car.CarClient _carClient;

    public ConfirmDealHandler(IMapper mapper, IDealRepository dealRepository, Car.CarClient carClient)
    {
        _mapper = mapper;
        _dealRepository = dealRepository;
        _carClient = carClient;
    }
    
    public async Task<DealDto> Handle(ConfirmDealCommand request, CancellationToken cancellationToken = default)
    {
        var deal = await _dealRepository.GetByIdAsync(request.Id, cancellationToken);

        if (deal == null)
        {
            throw new NotFoundException("Deal");
        }

        deal.State = DealState.Active;
        await _dealRepository.UpdateAsync(deal.Id, deal, cancellationToken: cancellationToken);
        var updatedDeal = await _dealRepository.GetByIdAsync(request.Id, cancellationToken);
        var result = _mapper.Map<DealDto>(updatedDeal);
        BackgroundJob.Enqueue<Car.CarClient>(x =>
            x.ChangeCarStatus(new ChangeStatus
            {
                CarId = result.CarId,
                Status = CarService.Status.Taken
            }, new CallOptions()));
        
        return result;
    }
}