using AutoMapper;
using CarSharingApp.CarService.Application.Commands.CommentCommands;
using CarSharingApp.CarService.Application.DTO_s.Comment;
using CarSharingApp.CarService.Application.Repositories;
using CarSharingApp.CarService.Domain.Exceptions;
using MediatR;

namespace CarSharingApp.CarService.Application.CommandHandlers.CommentCommandHandlers;

public class DeleteCommentHandler : IRequestHandler<DeleteCommentCommand, CommentDto>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IMapper _mapper;

    public DeleteCommentHandler(IMapper mapper, ICommentRepository commentRepository)
    {
        _mapper = mapper;
        _commentRepository = commentRepository;
    }

    public async Task<CommentDto> Handle(DeleteCommentCommand command, CancellationToken cancellationToken)
    {
        var comment = await _commentRepository.GetByIdAsync(command.Id, cancellationToken);
        
        if (comment == null)
        {
            throw new NotFoundException("Comment");
        }
        
        var newComment = await _commentRepository.DeleteAsync(command.Id, cancellationToken);
        await _commentRepository.SaveChangesAsync(cancellationToken);
        var commentDto = _mapper.Map<CommentDto>(newComment);

        return commentDto;
    }
}