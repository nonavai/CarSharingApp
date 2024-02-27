using AutoMapper;
using CarSharingApp.DealService.BusinessLogic.Caching;
using CarSharingApp.DealService.BusinessLogic.Models.FeedBack;
using CarSharingApp.DealService.BusinessLogic.Queries.FeedbackQueries;
using CarSharingApp.DealService.DataAccess.Repositories;
using MediatR;
using Newtonsoft.Json;

namespace CarSharingApp.DealService.BusinessLogic.QueryHandlers.FeedbackQueryHandlers;

public class GetFeedbackByDealHandler : IRequestHandler<GetFeedbackByDealQueries, IEnumerable<FeedbackDto>>
{
    private readonly IFeedBackRepository _feedBackRepository;
    private readonly ICacheService _cacheService;
    private readonly IMapper _mapper;

    public GetFeedbackByDealHandler(IMapper mapper, IFeedBackRepository feedBackRepository, ICacheService cacheService)
    {
        _mapper = mapper;
        _feedBackRepository = feedBackRepository;
        _cacheService = cacheService;
    }
    
    public async Task<IEnumerable<FeedbackDto>> Handle(GetFeedbackByDealQueries request, CancellationToken cancellationToken = default)
    {
        var serializedValue = JsonConvert.SerializeObject(request);
        var cache = await _cacheService.GetAsync<IEnumerable<FeedbackDto>>(serializedValue);

        if (cache != null)
        {
            return cache;
        }
        
        var deals = await _feedBackRepository.GetByDealIdAsync(
            request.DealId,
            request.CurrentPage,
            request.PageSize,
            cancellationToken);
        var result = _mapper.Map<IEnumerable<FeedbackDto>>(deals);
        await _cacheService.SetAsync(serializedValue, deals);
        
        return result;
    }
}