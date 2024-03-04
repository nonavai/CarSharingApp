using AutoMapper;
using CarSharingApp.DealService.BusinessLogic.Commands.FeedbackCommands;
using CarSharingApp.DealService.BusinessLogic.Models.FeedBack;
using CarSharingApp.DealService.DataAccess.Repositories;
using CarSharingApp.DealService.Shared.Exceptions;
using MediatR;

namespace CarSharingApp.DealService.BusinessLogic.CommandHandlers.FeedbackCommandHandlers;

public class UpdateFeedbackHandler : IRequestHandler<UpdateFeedbackCommand, FeedbackDto>
{
    private readonly IFeedBackRepository _feedBackRepository;
    private readonly IMapper _mapper;

    public UpdateFeedbackHandler(IMapper mapper, IFeedBackRepository feedBackRepository)
    {
        _mapper = mapper;
        _feedBackRepository = feedBackRepository;
    }
    
    public async Task<FeedbackDto> Handle(UpdateFeedbackCommand request, CancellationToken cancellationToken = default)
    {
        var feedback = await _feedBackRepository.GetByIdAsync(request.Id, cancellationToken);

        if (feedback == null)
        {
            throw new NotFoundException("Feedback");
        }

        _mapper.Map(request, feedback);
        await _feedBackRepository.UpdateAsync(request.Id, feedback, cancellationToken);
        var result = _mapper.Map<FeedbackDto>(feedback);
        
        return result;
    }
}