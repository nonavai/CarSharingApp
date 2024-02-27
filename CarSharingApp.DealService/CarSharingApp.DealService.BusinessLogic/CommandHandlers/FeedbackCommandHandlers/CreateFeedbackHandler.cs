using AutoMapper;
using CarSharingApp.DealService.BusinessLogic.Commands.FeedbackCommands;
using CarSharingApp.DealService.BusinessLogic.Models.FeedBack;
using CarSharingApp.DealService.DataAccess.Entities;
using CarSharingApp.DealService.DataAccess.Repositories;
using MediatR;

namespace CarSharingApp.DealService.BusinessLogic.CommandHandlers.FeedbackCommandHandlers;

public class CreateFeedbackHandler : IRequestHandler<CreateFeedbackCommand, FeedbackDto>
{
    private readonly IFeedBackRepository _feedBackRepository;
    private readonly IMapper _mapper;

    public CreateFeedbackHandler(IMapper mapper, IFeedBackRepository feedBackRepository)
    {
        _mapper = mapper;
        _feedBackRepository = feedBackRepository;
    }
    
    public async Task<FeedbackDto> Handle(CreateFeedbackCommand request, CancellationToken cancellationToken = default)
    {
        var deal = _mapper.Map<Feedback>(request);
        deal.Posted = DateTime.Now;
        var entityResult = await _feedBackRepository.CreateAsync(deal, cancellationToken);
        var result = _mapper.Map<FeedbackDto>(entityResult);
        
        return result;
    }
}