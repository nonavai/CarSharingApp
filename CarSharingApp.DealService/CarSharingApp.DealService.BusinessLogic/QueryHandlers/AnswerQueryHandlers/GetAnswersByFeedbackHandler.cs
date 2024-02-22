using AutoMapper;
using CarSharingApp.DealService.BusinessLogic.Caching;
using CarSharingApp.DealService.BusinessLogic.Models.Answer;
using CarSharingApp.DealService.BusinessLogic.Queries.AnswerQueries;
using CarSharingApp.DealService.DataAccess.Repositories;
using MediatR;
using Newtonsoft.Json;

namespace CarSharingApp.DealService.BusinessLogic.QueryHandlers.AnswerQueryHandlers;

public class GetAnswersByFeedbackHandler : IRequestHandler<GetAnswersByFeedbackQuery, IEnumerable<AnswerDto>>
{
    private readonly IAnswerRepository _answerRepository;
    private readonly ICacheService _cacheService;
    private readonly IMapper _mapper;

    public GetAnswersByFeedbackHandler(IMapper mapper, IAnswerRepository answerRepository, ICacheService cacheService)
    {
        _mapper = mapper;
        _answerRepository = answerRepository;
        _cacheService = cacheService;
    }
    
    public async Task<IEnumerable<AnswerDto>> Handle(GetAnswersByFeedbackQuery request, CancellationToken cancellationToken = default)
    {
        var serializedValue = JsonConvert.SerializeObject(request);
        var cache = await _cacheService.GetAsync<IEnumerable<AnswerDto>>(serializedValue);

        if (cache != null)
        {
            return cache;
        }
        var deals = await _answerRepository.GetByFeedBackIdAsync(
            request.FeedbackId,
            request.CurrentPage,
            request.PageSize,
            cancellationToken);
        var result = _mapper.Map<IEnumerable<AnswerDto>>(deals);
        await _cacheService.SetAsync(serializedValue, deals);
        
        return result;
    }
}