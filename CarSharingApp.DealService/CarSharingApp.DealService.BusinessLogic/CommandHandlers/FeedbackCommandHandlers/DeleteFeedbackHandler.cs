using AutoMapper;
using CarSharingApp.DealService.BusinessLogic.Commands.FeedbackCommands;
using CarSharingApp.DealService.BusinessLogic.Models.FeedBack;
using CarSharingApp.DealService.DataAccess.Repositories;
using CarSharingApp.DealService.Shared.Exceptions;
using MediatR;

namespace CarSharingApp.DealService.BusinessLogic.CommandHandlers.FeedbackCommandHandlers;

public class DeleteFeedbackHandler : IRequestHandler<DeleteFeedbackCommand, FeedbackDto>
{
    private readonly IFeedBackRepository _feedBackRepository;
    private readonly IAnswerRepository _answerRepository;
    private readonly IMapper _mapper;

    public DeleteFeedbackHandler(IMapper mapper, IFeedBackRepository feedBackRepository, IAnswerRepository answerRepository)
    {
        _mapper = mapper;
        _feedBackRepository = feedBackRepository;
        _answerRepository = answerRepository;
    }
    
    public async Task<FeedbackDto> Handle(DeleteFeedbackCommand request, CancellationToken cancellationToken = default)
    {
        var feedback = await _feedBackRepository.GetByIdAsync(request.Id, cancellationToken);

        if (feedback == null)
        {
            throw new NotFoundException("Feedback");
        }
        
        await _feedBackRepository.DeleteAsync(request.Id, cancellationToken);
        await _answerRepository.DeleteByFeedbackAsync(request.Id, cancellationToken);
        var result = _mapper.Map<FeedbackDto>(feedback);
        
        return result;
    }
}