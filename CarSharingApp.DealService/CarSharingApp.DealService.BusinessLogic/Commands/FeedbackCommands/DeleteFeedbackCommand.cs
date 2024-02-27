using CarSharingApp.DealService.BusinessLogic.Models.FeedBack;
using MediatR;

namespace CarSharingApp.DealService.BusinessLogic.Commands.FeedbackCommands;

public class DeleteFeedbackCommand : IRequest<FeedbackDto>
{
    public string Id { get; set; }
}