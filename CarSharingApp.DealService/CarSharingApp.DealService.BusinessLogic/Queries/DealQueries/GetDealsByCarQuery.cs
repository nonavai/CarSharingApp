using CarSharingApp.DealService.BusinessLogic.Models;
using CarSharingApp.DealService.BusinessLogic.Models.Deal;
using MediatR;

namespace CarSharingApp.DealService.BusinessLogic.Queries.DealQueries;

public class GetDealsByCarQuery : IRequest<IEnumerable<DealDto>>
{
    public string CarId { get; set; }
}