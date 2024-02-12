namespace CarSharingApp.CarService.Application.Responses.Comment;

public class CommentResponse
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public DateTime TimePosted { get; set; }
    public string Text { get; set; }
    public int? Rating { get; set; }
}