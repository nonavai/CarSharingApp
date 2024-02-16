using CarSharingApp.DealService.BusinessLogic.Models.FeedBack;
using CarSharingApp.DealService.Shared.Enums;
using MediatR;

namespace CarSharingApp.DealService.BusinessLogic.Commands.FeedbackCommands;

public class CreateFeedbackCommand : IRequest<FeedbackDto>
{
    public string Id { get; set; }
    public string DealId { get; set; }
    public string UserId { get; set; }
    public IssueType IssueType { get; set; }
    public DateTime Posted { get; set; }
    public string Text { get; set; }
    public int? Rating { get; set; }
}