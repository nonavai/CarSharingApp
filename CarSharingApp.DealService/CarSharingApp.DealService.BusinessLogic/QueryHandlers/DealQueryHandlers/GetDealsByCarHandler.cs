using AutoMapper;
using CarSharingApp.DealService.BusinessLogic.Models.Deal;
using CarSharingApp.DealService.BusinessLogic.Queries.DealQueries;
using CarSharingApp.DealService.DataAccess.Repositories;
using MediatR;

namespace CarSharingApp.DealService.BusinessLogic.QueryHandlers.DealQueryHandlers;

public class GetDealsByCarHandler : IRequestHandler<GetDealsByCarQuery, IEnumerable<DealDto>>
{
    private readonly IDealRepository _dealRepository;
    private readonly IMapper _mapper;

    public GetDealsByCarHandler(IMapper mapper, IDealRepository dealRepository)
    {
        _mapper = mapper;
        _dealRepository = dealRepository;
    }
    
    public async Task<IEnumerable<DealDto>> Handle(GetDealsByCarQuery request, CancellationToken cancellationToken = default)
    {
        var deals = await _dealRepository.GetByCarIdAsync(
            request.CarId,
            request.CurrentPage,
            request.PageSize,
            cancellationToken);
        var result = _mapper.Map<IEnumerable<DealDto>>(deals);
        
        return result;
    }
}