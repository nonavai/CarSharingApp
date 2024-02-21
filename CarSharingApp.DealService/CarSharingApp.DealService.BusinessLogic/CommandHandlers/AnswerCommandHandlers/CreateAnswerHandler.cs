using AutoMapper;
using CarSharingApp.DealService.BusinessLogic.Commands.AnswerCommands;
using CarSharingApp.DealService.BusinessLogic.Models.Answer;
using CarSharingApp.DealService.DataAccess.Entities;
using CarSharingApp.DealService.DataAccess.Repositories;
using MediatR;

namespace CarSharingApp.DealService.BusinessLogic.CommandHandlers.AnswerCommandHandlers;

public class CreateAnswerHandler : IRequestHandler<CreateAnswerCommand, AnswerDto>
{
    private readonly IAnswerRepository _answerRepository;
    private readonly IMapper _mapper;

    public CreateAnswerHandler(IMapper mapper, IAnswerRepository answerRepository)
    {
        _mapper = mapper;
        _answerRepository = answerRepository;
    }
    
    public async Task<AnswerDto> Handle(CreateAnswerCommand request, CancellationToken cancellationToken = default)
    {
        var answer = _mapper.Map<Answer>(request);
        answer.Posted = DateTime.Now;
        var entityResult = await _answerRepository.CreateAsync(answer, cancellationToken);
        var result = _mapper.Map<AnswerDto>(entityResult);
        
        return result;
    }
}