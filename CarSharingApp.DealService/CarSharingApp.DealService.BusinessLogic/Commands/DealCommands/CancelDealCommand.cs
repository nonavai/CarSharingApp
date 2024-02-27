using CarSharingApp.DealService.BusinessLogic.Models.Deal;
using MediatR;

namespace CarSharingApp.DealService.BusinessLogic.Commands.DealCommands;

public class CancelDealCommand : IRequest<DealDto>
{
    public string Id { get; set; }
}