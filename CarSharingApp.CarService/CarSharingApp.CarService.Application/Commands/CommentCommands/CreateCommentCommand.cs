namespace CarSharingApp.CarService.Application.Commands.CommentCommands;

public class CreateCommentCommand
{
    public string UserId { get; set; }
    public string CarId { get; set; }
    public DateTime TimePosted { get; set; }
    public string Text { get; set; }
    public int? Rating { get; set; }
}