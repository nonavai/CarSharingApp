using AutoMapper;
using CarSharingApp.CarService.Application.DTO_s.Comment;
using CarSharingApp.CarService.Application.Queries.CommentQueries;
using CarSharingApp.CarService.Application.Repositories;
using CarSharingApp.CarService.Application.Responses.Comment;
using MediatR;

namespace CarSharingApp.CarService.Application.QueryHandlers.CommentQueryHandlers;

public class GetCommentsByCarHandler : IRequestHandler<GetCommentsByCarQuery, IEnumerable<CommentResponse>>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IMapper _mapper;

    public GetCommentsByCarHandler( IMapper mapper, ICommentRepository commentRepository)
    {
        _mapper = mapper;
        _commentRepository = commentRepository;
    }

    public async Task<IEnumerable<CommentResponse>> Handle(GetCommentsByCarQuery query, CancellationToken cancellationToken)
    {
        var comment = await _commentRepository.GetByCarIdAsync(query.CarId);
        var commentDto = _mapper.Map<IEnumerable<CommentResponse>>(comment);

        return commentDto;
    }
}