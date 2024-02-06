using AutoMapper;
using CarSharingApp.CarService.Application.DTO_s.Comment;
using CarSharingApp.CarService.Application.Queries.CommentQueries;
using CarSharingApp.CarService.Application.Repositories;
using MediatR;

namespace CarSharingApp.CarService.Application.QueryHandlers.CommentQueryHandlers;

public class GetCommentsByCarQueryHandler : IRequestHandler<GetCommentsByCarQuery, IEnumerable<CommentDto>>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IMapper _mapper;

    public GetCommentsByCarQueryHandler( IMapper mapper, ICommentRepository commentRepository)
    {
        _mapper = mapper;
        _commentRepository = commentRepository;
    }

    public async Task<IEnumerable<CommentDto>> Handle(GetCommentsByCarQuery query, CancellationToken cancellationToken)
    {
        var comment = await _commentRepository.GetByCarIdAsync(query.CarId);
        var commentDto = _mapper.Map<IEnumerable<CommentDto>>(comment);

        return commentDto;
    }
}