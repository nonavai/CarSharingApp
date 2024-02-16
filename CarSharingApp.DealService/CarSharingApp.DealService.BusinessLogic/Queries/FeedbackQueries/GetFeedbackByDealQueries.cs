using CarSharingApp.DealService.BusinessLogic.Models;
using CarSharingApp.DealService.BusinessLogic.Models.FeedBack;
using MediatR;

namespace CarSharingApp.DealService.BusinessLogic.Queries.FeedbackQueries;

public class GetFeedbackByDealQueries : IRequest<IEnumerable<FeedbackDto>>
{
    public string DealId { get; set; }
}