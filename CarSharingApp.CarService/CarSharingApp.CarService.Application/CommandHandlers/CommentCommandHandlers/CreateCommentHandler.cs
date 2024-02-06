using AutoMapper;
using CarSharingApp.CarService.Application.Commands.CarCommands;
using CarSharingApp.CarService.Application.Commands.CommentCommands;
using CarSharingApp.CarService.Application.DTO_s.CarState;
using CarSharingApp.CarService.Application.DTO_s.Comment;
using CarSharingApp.CarService.Application.Repositories;
using CarSharingApp.CarService.Domain.Entities;
using MediatR;

namespace CarSharingApp.CarService.Application.CommandHandlers.CommentCommandHandlers;

public class CreateCommentHandler : IRequestHandler<CreateCommentCommand, CommentDto>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IMapper _mapper;

    public CreateCommentHandler(IMapper mapper, ICommentRepository commentRepository)
    {
        _mapper = mapper;
        _commentRepository = commentRepository;
    }

    public async Task<CommentDto> Handle(CreateCommentCommand command, CancellationToken cancellationToken)
    {
        var comment = _mapper.Map<Comment>(command);
        var newComment = await _commentRepository.AddAsync(comment, cancellationToken);
        var commentDto = _mapper.Map<CommentDto>(newComment);

        return commentDto;
    }
}