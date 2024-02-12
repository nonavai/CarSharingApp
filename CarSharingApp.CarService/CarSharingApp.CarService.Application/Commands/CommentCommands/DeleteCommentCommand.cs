using CarSharingApp.CarService.Application.Responses.Comment;
using MediatR;

namespace CarSharingApp.CarService.Application.Commands.CommentCommands;

public class DeleteCommentCommand : IRequest<CommentResponse>
{
    public string Id { get; set; }
}