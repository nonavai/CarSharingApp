namespace CarSharingApp.CarService.Application.Commands.CommentCommands;

public class UpdateCommentCommand
{
    public string Id { get; set; }
    public string? Text { get; set; }
    public int? Rating { get; set; }
}