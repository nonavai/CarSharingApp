using AutoMapper;
using CarSharingApp.DealService.BusinessLogic.Commands.FeedbackCommands;
using CarSharingApp.DealService.BusinessLogic.Models.FeedBack;
using CarSharingApp.DealService.DataAccess.Entities;
using CarSharingApp.DealService.DataAccess.Repositories;
using CarSharingApp.DealService.Shared.Exceptions;
using MediatR;
using UserService;

namespace CarSharingApp.DealService.BusinessLogic.CommandHandlers.FeedbackCommandHandlers;

public class CreateFeedbackHandler : IRequestHandler<CreateFeedbackCommand, FeedbackDto>
{
    private readonly IFeedBackRepository _feedBackRepository;
    private readonly IMapper _mapper;
    private readonly User.UserClient _userClient;

    public CreateFeedbackHandler(IMapper mapper, IFeedBackRepository feedBackRepository, User.UserClient userClient)
    {
        _mapper = mapper;
        _feedBackRepository = feedBackRepository;
        _userClient = userClient;
    }
    
    public async Task<FeedbackDto> Handle(CreateFeedbackCommand request, CancellationToken cancellationToken = default)
    {
        var userResponse = await _userClient.IsUserExistAsync(new UserRequest()
        {
            UserId = request.UserId
        });
        
        if (!userResponse.Exists)
        {
            throw new NotFoundException("User");
        }
        
        var deal = _mapper.Map<Feedback>(request);
        deal.Posted = DateTime.Now;
        var entityResult = await _feedBackRepository.CreateAsync(deal, cancellationToken);
        var result = _mapper.Map<FeedbackDto>(entityResult);
        
        return result;
    }
}