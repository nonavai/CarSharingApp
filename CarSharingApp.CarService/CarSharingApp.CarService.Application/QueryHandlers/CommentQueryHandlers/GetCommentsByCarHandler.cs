using AutoMapper;
using CarSharingApp.CarService.Application.Caching;
using CarSharingApp.CarService.Application.DTO_s.Comment;
using CarSharingApp.CarService.Application.Queries.CommentQueries;
using CarSharingApp.CarService.Application.Repositories;
using MediatR;
using Newtonsoft.Json;

namespace CarSharingApp.CarService.Application.QueryHandlers.CommentQueryHandlers;

public class GetCommentsByCarHandler : IRequestHandler<GetCommentsByCarQuery, IEnumerable<CommentDto>>
{
    private readonly ICommentRepository _commentRepository;
    private readonly ICacheService _cacheService;
    private readonly IMapper _mapper;

    public GetCommentsByCarHandler( IMapper mapper, ICommentRepository commentRepository, ICacheService cacheService)
    {
        _mapper = mapper;
        _commentRepository = commentRepository;
        _cacheService = cacheService;
    }

    public async Task<IEnumerable<CommentDto>> Handle(GetCommentsByCarQuery query, CancellationToken cancellationToken)
    {
        var serializedValue = JsonConvert.SerializeObject(query);
        var cache = await _cacheService.GetAsync<IEnumerable<CommentDto>>(serializedValue);

        if (cache != null)
        {
            return cache;
        }

        var comment = await _commentRepository.GetByCarIdAsync(query.CarId);
        var commentDto = _mapper.Map<IEnumerable<CommentDto>>(comment);
        await _cacheService.SetAsync(serializedValue, commentDto, TimeSpan.Zero);
        
        return commentDto;
    }
}