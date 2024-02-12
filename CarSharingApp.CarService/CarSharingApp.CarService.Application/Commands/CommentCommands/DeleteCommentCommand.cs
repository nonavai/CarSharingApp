using CarSharingApp.CarService.Application.DTO_s.Comment;
using MediatR;

namespace CarSharingApp.CarService.Application.Commands.CommentCommands;

public class DeleteCommentCommand : IRequest<CommentDto>
{
    public string Id { get; set; }
}