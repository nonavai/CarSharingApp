using CarSharingApp.DealService.BusinessLogic.Models;
using CarSharingApp.DealService.BusinessLogic.Models.Deal;
using MediatR;

namespace CarSharingApp.DealService.BusinessLogic.Queries.DealQueries;

public class GetDealsByUserQuery : IRequest<IEnumerable<DealDto>>
{
    public string UserId { get; set; }
    public int CurrentPage { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}