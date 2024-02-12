using AutoMapper;
using CarSharingApp.CarService.Application.Commands.CommentCommands;
using CarSharingApp.CarService.Application.Repositories;
using CarSharingApp.CarService.Application.Responses.Comment;
using MediatR;

namespace CarSharingApp.CarService.Application.CommandHandlers.CommentCommandHandlers;

public class DeleteCommentHandler : IRequestHandler<DeleteCommentCommand, CommentResponse>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IMapper _mapper;

    public DeleteCommentHandler(IMapper mapper, ICommentRepository commentRepository)
    {
        _mapper = mapper;
        _commentRepository = commentRepository;
    }

    public async Task<CommentResponse> Handle(DeleteCommentCommand command, CancellationToken cancellationToken)
    {
        var newComment = await _commentRepository.DeleteAsync(command.Id, cancellationToken);
        await _commentRepository.SaveChangesAsync(cancellationToken);
        var commentDto = _mapper.Map<CommentResponse>(newComment);

        return commentDto;
    }
}