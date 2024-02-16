using CarSharingApp.DealService.BusinessLogic.Models.Answer;
using MediatR;

namespace CarSharingApp.DealService.BusinessLogic.Queries.AnswerQueries;

public class GetAnswersByFeedbackQuery : IRequest<IEnumerable<AnswerDto>>
{
    public string FeedbackId { get; set; }
}