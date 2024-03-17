using CarSharingApp.DealService.BusinessLogic.Models.Deal;
using MediatR;

namespace CarSharingApp.DealService.BusinessLogic.Queries.DealQueries;

public class GetDealsByUserQuery : GetCollectionBaseQuery, IRequest<IEnumerable<DealDto>>
{
    public string UserId { get; set; }
}