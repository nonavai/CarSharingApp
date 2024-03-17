using CarSharingApp.DealService.BusinessLogic.Models.FeedBack;
using MediatR;

namespace CarSharingApp.DealService.BusinessLogic.Queries.FeedbackQueries;

public class GetFeedbackByDealQueries : GetCollectionBaseQuery, IRequest<IEnumerable<FeedbackDto>>
{
    public string DealId { get; set; }
}