using AutoMapper;
using CarSharingApp.DealService.BusinessLogic.Caching;
using CarSharingApp.DealService.BusinessLogic.Models.Deal;
using CarSharingApp.DealService.BusinessLogic.Queries.DealQueries;
using CarSharingApp.DealService.DataAccess.Repositories;
using MediatR;
using Newtonsoft.Json;

namespace CarSharingApp.DealService.BusinessLogic.QueryHandlers.DealQueryHandlers;

public class GetDealsByCarHandler : IRequestHandler<GetDealsByCarQuery, IEnumerable<DealDto>>
{
    private readonly IDealRepository _dealRepository;
    private readonly ICacheService _cacheService;
    private readonly IMapper _mapper;

    public GetDealsByCarHandler(IMapper mapper, IDealRepository dealRepository, ICacheService cacheService)
    {
        _mapper = mapper;
        _dealRepository = dealRepository;
        _cacheService = cacheService;
    }
    
    public async Task<IEnumerable<DealDto>> Handle(GetDealsByCarQuery request, CancellationToken cancellationToken = default)
    {
        var serializedValue = JsonConvert.SerializeObject(request);
        var cache = await _cacheService.GetAsync<IEnumerable<DealDto>>(serializedValue);

        if (cache != null)
        {
            return cache;
        }
        
        var deals = await _dealRepository.GetByCarIdAsync(
            request.CarId,
            request.CurrentPage,
            request.PageSize,
            cancellationToken);
        var result = _mapper.Map<IEnumerable<DealDto>>(deals);
        await _cacheService.SetAsync(serializedValue, deals);
        
        return result;
    }
}