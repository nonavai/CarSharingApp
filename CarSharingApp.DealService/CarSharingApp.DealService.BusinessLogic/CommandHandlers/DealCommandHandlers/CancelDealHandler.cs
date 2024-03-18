using AutoMapper;
using CarSharingApp.Common.Enums;
using CarSharingApp.DealService.BusinessLogic.Commands.DealCommands;
using CarSharingApp.DealService.BusinessLogic.Models.Deal;
using CarSharingApp.DealService.BusinessLogic.Producers;
using CarSharingApp.DealService.DataAccess.Repositories;
using CarSharingApp.DealService.Shared.Enums;
using CarSharingApp.DealService.Shared.Exceptions;
using Hangfire;
using MediatR;

namespace CarSharingApp.DealService.BusinessLogic.CommandHandlers.DealCommandHandlers;

public class CancelDealHandler : IRequestHandler<CancelDealCommand, DealDto>
{
    private readonly IDealRepository _dealRepository;
    private readonly IMapper _mapper;

    public CancelDealHandler(IMapper mapper, IDealRepository dealRepository)
    {
        _mapper = mapper;
        _dealRepository = dealRepository;
    }
    
    public async Task<DealDto> Handle(CancelDealCommand request, CancellationToken cancellationToken = default)
    {
        var deal = await _dealRepository.GetByIdAsync(request.Id, cancellationToken);

        if (deal == null)
        {
            throw new NotFoundException("Deal");
        }

        if (deal.State != DealState.Booking)
        {
            var dto = _mapper.Map<DealDto>(deal);
            
            return dto;
        }
        
        deal.State = DealState.Canceled;
        deal.Finished = DateTime.Now; 
        await _dealRepository.UpdateAsync(deal.Id, deal, cancellationToken: cancellationToken);
        var result = _mapper.Map<DealDto>(deal);
        BackgroundJob.Enqueue<UpdateCarStatusProducer>(x =>
            x.UpdateCarStatus(deal.CarId, Status.Free));
        
        return result;
    }
}