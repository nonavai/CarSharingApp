using CarSharingApp.DealService.BusinessLogic.Models.Answer;
using MediatR;

namespace CarSharingApp.DealService.BusinessLogic.Commands.AnswerCommands;

public class CreateAnswerCommand : IRequest<AnswerDto>
{
    public string FeedBackId { get; set; }
    public string UserId { get; set; }
    public DateTime Posted { get; set; }
    public string Text { get; set; }
}