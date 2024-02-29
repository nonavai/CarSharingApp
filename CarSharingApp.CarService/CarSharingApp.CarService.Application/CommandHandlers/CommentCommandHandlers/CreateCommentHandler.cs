using AutoMapper;
using CarSharingApp.CarService.Application.Commands.CommentCommands;
using CarSharingApp.CarService.Application.DTO_s.Comment;
using CarSharingApp.CarService.Application.Repositories;
using CarSharingApp.CarService.Domain.Entities;
using CarSharingApp.CarService.Domain.Exceptions;
using MediatR;
using UserService;

namespace CarSharingApp.CarService.Application.CommandHandlers.CommentCommandHandlers;

public class CreateCommentHandler : IRequestHandler<CreateCommentCommand, CommentDto>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IMapper _mapper;
    private readonly User.UserClient _userClient;

    public CreateCommentHandler(IMapper mapper, ICommentRepository commentRepository, User.UserClient userClient)
    {
        _mapper = mapper;
        _commentRepository = commentRepository;
        _userClient = userClient;
    }

    public async Task<CommentDto> Handle(CreateCommentCommand command, CancellationToken cancellationToken)
    {
        var userResponse = await _userClient.IsUserExistAsync(new UserExistRequest
        {
            UserId = command.UserId
        });
        
        if (!userResponse.Exists)
        {
            throw new NotFoundException("User");
        }
        
        var comment = _mapper.Map<Comment>(command);
        comment.TimePosted = DateTime.Now;
        var newComment = await _commentRepository.AddAsync(comment, cancellationToken);
        await _commentRepository.SaveChangesAsync(cancellationToken);
        var commentDto = _mapper.Map<CommentDto>(newComment);

        return commentDto;
    }
}