using CarSharingApp.CarService.Application.DTO_s.Comment;
using MediatR;

namespace CarSharingApp.CarService.Application.Commands.CommentCommands;

public class UpdateCommentCommand :IRequest<CommentDto>
{
    public string Id { get; set; }
    public string? Text { get; set; }
    public int? Rating { get; set; }
}