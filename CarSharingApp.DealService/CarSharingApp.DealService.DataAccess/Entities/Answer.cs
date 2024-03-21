namespace CarSharingApp.DealService.DataAccess.Entities;

public class Answer : BaseEntity
{
    public string FeedBackId { get; set; }
    public string UserId { get; set; }
    public DateTime Posted { get; set; }
    public string Text { get; set; }
    public bool IsChanged { get; set; }
}