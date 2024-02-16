using CarSharingApp.DealService.BusinessLogic.Models.Deal;
using MediatR;

namespace CarSharingApp.DealService.BusinessLogic.Commands.DealCommands;

public class CreateDealCommand : IRequest<DealDto>
{
    public string CarId { get; set; }
    public string UserId { get; set; }
    public DateTime Requested { get; set; }
    public float TotalPrice { get; set; }
}