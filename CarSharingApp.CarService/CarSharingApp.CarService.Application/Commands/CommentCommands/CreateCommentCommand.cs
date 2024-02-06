using CarSharingApp.CarService.Application.DTO_s.Comment;
using MediatR;

namespace CarSharingApp.CarService.Application.Commands.CommentCommands;

public class CreateCommentCommand : IRequest<CommentDto>
{
    public string UserId { get; set; }
    public string CarId { get; set; }
    public DateTime TimePosted { get; set; }
    public string Text { get; set; }
    public int? Rating { get; set; }
}