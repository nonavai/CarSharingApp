using CarSharingApp.DealService.BusinessLogic.Models.Deal;
using MediatR;

namespace CarSharingApp.DealService.BusinessLogic.Commands.DealCommands;

public class CompleteDealCommand : IRequest<DealDto>
{
    public string Id { get; set; }
    public DateTime Finished { get; set; }
    public int Rating { get; set; }
}