using AutoMapper;
using CarSharingApp.DealService.BusinessLogic.Commands.DealCommands;
using CarSharingApp.DealService.BusinessLogic.Models.Deal;
using CarSharingApp.DealService.BusinessLogic.Producers;
using CarSharingApp.DealService.DataAccess.Entities;
using CarSharingApp.DealService.DataAccess.Repositories;
using CarSharingApp.DealService.Shared.Enums;
using Hangfire;
using MediatR;

namespace CarSharingApp.DealService.BusinessLogic.CommandHandlers.DealCommandHandlers;

public class CreateDealHandler : IRequestHandler<CreateDealCommand, DealDto>
{
    private readonly IDealRepository _dealRepository;
    private readonly IMapper _mapper;

    public CreateDealHandler(IMapper mapper, IDealRepository dealRepository)
    {
        _mapper = mapper;
        _dealRepository = dealRepository;
    }
    
    public async Task<DealDto> Handle(CreateDealCommand request, CancellationToken cancellationToken = default)
    {
        var deal = _mapper.Map<Deal>(request);
        deal.Requested = DateTime.Now;
        var entityResult = await _dealRepository.CreateAsync(deal, cancellationToken);
        var result = _mapper.Map<DealDto>(entityResult);
        BackgroundJob.Schedule<CancelDealHandler>(x =>
            x.Handle(new CancelDealCommand
            {
                Id = result.Id
            }, cancellationToken), 
            TimeSpan.FromMinutes(20));
        BackgroundJob.Enqueue<UpdateCarStatusProducer>(x =>
            x.UpdateCarStatus(request.CarId, CarStatus.Booking));
        
        return result;
    }
}