using CarSharingApp.DealService.Shared.Enums;

namespace CarSharingApp.DealService.DataAccess.Entities;

public class Feedback : BaseEntity
{
    public string DealId { get; set; }
    public string UserId { get; set; }
    public IssueType IssueType { get; set; }
    public DateTime Posted { get; set; }
    public string Text { get; set; }
    public int? Rating { get; set; }
    public bool IsChanged { get; set; }
}