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

public class CompleteDealHandler : IRequestHandler<CompleteDealCommand, DealDto>
{
    private readonly IDealRepository _dealRepository;
    private readonly IMapper _mapper;

    public CompleteDealHandler(IMapper mapper, IDealRepository dealRepository)
    {
        _mapper = mapper;
        _dealRepository = dealRepository;
    }
    
    public async Task<DealDto> Handle(CompleteDealCommand request, CancellationToken cancellationToken = default)
    {
        var deal = await _dealRepository.GetByIdAsync(request.Id, cancellationToken);

        if (deal == null)
        {
            throw new NotFoundException("Deal");
        }

        if (deal.State != DealState.Active)
        {
            throw new BadRequestException();
        }

        deal.State = DealState.Succeeded;
        deal.Finished = DateTime.Now;
        var difference = deal.Finished - deal.Requested;
        deal.TotalPrice = (float)(deal.TotalPrice * difference.Value.TotalHours);
        var confirmed = _dealRepository.UpdateAsync(deal.Id, deal, cancellationToken: cancellationToken);
        var result = _mapper.Map<DealDto>(confirmed);
        BackgroundJob.Enqueue<UpdateCarStatusProducer>(x =>
            x.UpdateCarStatus(deal.CarId, Status.Free));
        
        return result;
    }
}