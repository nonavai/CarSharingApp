using CarSharingApp.DealService.BusinessLogic.Models;
using CarSharingApp.DealService.BusinessLogic.Models.FeedBack;
using MediatR;

namespace CarSharingApp.DealService.BusinessLogic.Queries.FeedbackQueries;

public class GetFeedbackByDealQueries : IRequest<IEnumerable<FeedbackDto>>
{
    public string DealId { get; set; }
    public int CurrentPage { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}