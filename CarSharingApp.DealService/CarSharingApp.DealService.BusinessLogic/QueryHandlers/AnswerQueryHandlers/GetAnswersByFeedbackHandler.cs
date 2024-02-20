using AutoMapper;
using CarSharingApp.DealService.BusinessLogic.Models.Answer;
using CarSharingApp.DealService.BusinessLogic.Queries.AnswerQueries;
using CarSharingApp.DealService.DataAccess.Repositories;
using MediatR;

namespace CarSharingApp.DealService.BusinessLogic.QueryHandlers.AnswerQueryHandlers;

public class GetAnswersByFeedbackHandler : IRequestHandler<GetAnswersByFeedbackQuery, IEnumerable<AnswerDto>>
{
    private readonly IAnswerRepository _answerRepository;
    private readonly IMapper _mapper;

    public GetAnswersByFeedbackHandler(IMapper mapper, IAnswerRepository answerRepository)
    {
        _mapper = mapper;
        _answerRepository = answerRepository;
    }
    
    public async Task<IEnumerable<AnswerDto>> Handle(GetAnswersByFeedbackQuery request, CancellationToken cancellationToken = default)
    {
        var deals = await _answerRepository.GetByFeedBackIdAsync(
            request.FeedbackId,
            request.CurrentPage,
            request.PageSize,
            cancellationToken);
        var result = _mapper.Map<IEnumerable<AnswerDto>>(deals);
        
        return result;
    }
}