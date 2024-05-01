using CarSharingApp.DealService.BusinessLogic.Models.FeedBack;
using CarSharingApp.DealService.Shared.Enums;
using MediatR;

namespace CarSharingApp.DealService.BusinessLogic.Commands.FeedbackCommands;

public class UpdateFeedbackCommand : IRequest<FeedbackDto>
{
    public string Id { get; set; }
    public string Text { get; set; }
    public int? Rating { get; set; }
}