using AutoMapper;
using CarSharingApp.CarService.Application.DTO_s.Comment;
using CarSharingApp.CarService.Application.Queries.CommentQueries;
using CarSharingApp.CarService.Application.Repositories;
using MediatR;

namespace CarSharingApp.CarService.Application.QueryHandlers.CommentQueryHandlers;

public class GetCommentHandler : IRequestHandler<GetCommentQuery, CommentDto>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IMapper _mapper;

    public GetCommentHandler( IMapper mapper, ICommentRepository commentRepository)
    {
        _mapper = mapper;
        _commentRepository = commentRepository;
    }

    public async Task<CommentDto> Handle(GetCommentQuery query, CancellationToken token)
    {
        var comment = await _commentRepository.GetByIdAsync(query.Id, token);
        var commentDto = _mapper.Map<CommentDto>(comment);

        return commentDto;
    }
}