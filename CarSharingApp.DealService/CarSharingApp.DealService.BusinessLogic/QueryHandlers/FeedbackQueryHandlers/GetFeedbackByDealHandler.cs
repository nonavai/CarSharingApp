using AutoMapper;
using CarSharingApp.DealService.BusinessLogic.Models.FeedBack;
using CarSharingApp.DealService.BusinessLogic.Queries.FeedbackQueries;
using CarSharingApp.DealService.DataAccess.Repositories;
using MediatR;

namespace CarSharingApp.DealService.BusinessLogic.QueryHandlers.FeedbackQueryHandlers;

public class GetFeedbackByDealHandler : IRequestHandler<GetFeedbackByDealQueries, IEnumerable<FeedbackDto>>
{
    private readonly IFeedBackRepository _feedBackRepository;
    private readonly IMapper _mapper;

    public GetFeedbackByDealHandler(IMapper mapper, IFeedBackRepository feedBackRepository)
    {
        _mapper = mapper;
        _feedBackRepository = feedBackRepository;
    }
    
    public async Task<IEnumerable<FeedbackDto>> Handle(GetFeedbackByDealQueries request, CancellationToken cancellationToken = default)
    {
        var deals = await _feedBackRepository.GetByDealIdAsync(
            request.DealId,
            request.CurrentPage,
            request.PageSize,
            cancellationToken);
        var result = _mapper.Map<IEnumerable<FeedbackDto>>(deals);
        
        return result;
    }
}