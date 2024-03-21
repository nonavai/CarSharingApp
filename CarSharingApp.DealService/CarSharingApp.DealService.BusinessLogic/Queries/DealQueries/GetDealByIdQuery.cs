using CarSharingApp.DealService.BusinessLogic.Models.Deal;
using MediatR;

namespace CarSharingApp.DealService.BusinessLogic.Queries.DealQueries;

public class GetDealByIdQuery : IRequest<DealDto>
{
    public string Id { get; set; }
}