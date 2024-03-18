using AutoMapper;
using CarSharingApp.Common.Enums;
using CarSharingApp.DealService.BusinessLogic.Commands.DealCommands;
using CarSharingApp.DealService.BusinessLogic.Models.Deal;
using CarSharingApp.DealService.BusinessLogic.Producers;
using CarSharingApp.DealService.DataAccess.Repositories;
using CarSharingApp.DealService.Shared.Enums;
using CarSharingApp.DealService.Shared.Exceptions;
using MediatR;
using Hangfire;

namespace CarSharingApp.DealService.BusinessLogic.CommandHandlers.DealCommandHandlers;

public class ConfirmDealHandler : IRequestHandler<ConfirmDealCommand, DealDto>
{
    private readonly IDealRepository _dealRepository;
    private readonly IMapper _mapper;

    public ConfirmDealHandler(IMapper mapper, IDealRepository dealRepository)
    {
        _mapper = mapper;
        _dealRepository = dealRepository;
    }
    
    public async Task<DealDto> Handle(ConfirmDealCommand request, CancellationToken cancellationToken = default)
    {
        var deal = await _dealRepository.GetByIdAsync(request.Id, cancellationToken);

        if (deal == null)
        {
            throw new NotFoundException("Deal");
        }

        deal.State = DealState.Active;
        var confirmed = _dealRepository.UpdateAsync(deal.Id, deal, cancellationToken: cancellationToken);
        var result = _mapper.Map<DealDto>(confirmed);
        BackgroundJob.Enqueue<UpdateCarStatusProducer>(x =>
            x.UpdateCarStatus(deal.CarId, Status.Taken));
        
        return result;
    }
}