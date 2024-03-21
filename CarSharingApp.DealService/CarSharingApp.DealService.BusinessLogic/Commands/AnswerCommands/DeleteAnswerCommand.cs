using CarSharingApp.DealService.BusinessLogic.Models.Answer;
using MediatR;

namespace CarSharingApp.DealService.BusinessLogic.Commands.AnswerCommands;

public class DeleteAnswerCommand : IRequest<AnswerDto>
{
    public string Id { get; set; }
}