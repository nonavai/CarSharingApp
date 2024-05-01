using AutoMapper;
using CarSharingApp.DealService.BusinessLogic.Commands.AnswerCommands;
using CarSharingApp.DealService.BusinessLogic.Models.Answer;
using CarSharingApp.DealService.DataAccess.Entities;
using CarSharingApp.DealService.DataAccess.Repositories;
using CarSharingApp.DealService.Shared.Exceptions;
using MediatR;
using UserService;

namespace CarSharingApp.DealService.BusinessLogic.CommandHandlers.AnswerCommandHandlers;

public class CreateAnswerHandler : IRequestHandler<CreateAnswerCommand, AnswerDto>
{
    private readonly IAnswerRepository _answerRepository;
    private readonly IMapper _mapper;
    private readonly User.UserClient _userClient;

    public CreateAnswerHandler(IMapper mapper, IAnswerRepository answerRepository, User.UserClient userClient)
    {
        _mapper = mapper;
        _answerRepository = answerRepository;
        _userClient = userClient;
    }
    
    public async Task<AnswerDto> Handle(CreateAnswerCommand request, CancellationToken cancellationToken = default)
    {
        var userResponse = await _userClient.IsUserExistAsync(new UserExistRequest
        {
            UserId = request.UserId
        });
        
        if (!userResponse.Exists)
        {
            throw new NotFoundException("User");
        }
        var answer = _mapper.Map<Answer>(request);
        answer.Posted = DateTime.Now;
        var entityResult = await _answerRepository.CreateAsync(answer, cancellationToken);
        var result = _mapper.Map<AnswerDto>(entityResult);
        
        return result;
    }
}