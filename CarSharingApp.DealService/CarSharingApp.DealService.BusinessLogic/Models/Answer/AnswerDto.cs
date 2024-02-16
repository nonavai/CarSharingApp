namespace CarSharingApp.DealService.BusinessLogic.Models.Answer;

public class AnswerDto
{
    public string Id { get; set; }
    public string FeedBackId { get; set; }
    public string UserId { get; set; }
    public DateTime Posted { get; set; }
    public string Text { get; set; }
    public bool IsChanged { get; set; }
}