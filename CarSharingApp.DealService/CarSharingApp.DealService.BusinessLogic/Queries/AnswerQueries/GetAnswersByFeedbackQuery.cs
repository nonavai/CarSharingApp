using CarSharingApp.DealService.BusinessLogic.Models.Answer;
using MediatR;

namespace CarSharingApp.DealService.BusinessLogic.Queries.AnswerQueries;

public class GetAnswersByFeedbackQuery : IRequest<IEnumerable<AnswerDto>>
{
    public string FeedbackId { get; set; }
    public int CurrentPage { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}