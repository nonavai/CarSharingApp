using CarSharingApp.DealService.BusinessLogic.Models.Answer;
using MediatR;

namespace CarSharingApp.DealService.BusinessLogic.Commands.AnswerCommands;

public class UpdateAnswerCommand : IRequest<AnswerDto>
{
    public string Id { get; set; }
    public string Text { get; set; }
}