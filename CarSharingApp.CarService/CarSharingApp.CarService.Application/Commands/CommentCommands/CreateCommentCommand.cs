using CarSharingApp.CarService.Application.Responses.Comment;
using MediatR;

namespace CarSharingApp.CarService.Application.Commands.CommentCommands;

public class CreateCommentCommand : IRequest<CommentResponse>
{
    public string UserId { get; set; }
    public string CarId { get; set; }
    public string Text { get; set; }
    public int? Rating { get; set; }
}