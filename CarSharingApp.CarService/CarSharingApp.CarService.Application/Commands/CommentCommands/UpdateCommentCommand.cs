using CarSharingApp.CarService.Application.Responses.Comment;
using MediatR;

namespace CarSharingApp.CarService.Application.Commands.CommentCommands;

public class UpdateCommentCommand :IRequest<CommentResponse>
{
    public string Id { get; set; }
    public string? Text { get; set; }
    public int? Rating { get; set; }
}