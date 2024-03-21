using AutoMapper;
using CarSharingApp.DealService.BusinessLogic.Commands.AnswerCommands;
using CarSharingApp.DealService.BusinessLogic.Models.Answer;
using CarSharingApp.DealService.DataAccess.Repositories;
using CarSharingApp.DealService.Shared.Exceptions;
using MediatR;

namespace CarSharingApp.DealService.BusinessLogic.CommandHandlers.AnswerCommandHandlers;

public class UpdateAnswerHandler : IRequestHandler<UpdateAnswerCommand, AnswerDto>
{
    private readonly IAnswerRepository _answerRepository;
    private readonly IMapper _mapper;

    public UpdateAnswerHandler(IMapper mapper, IAnswerRepository answerRepository)
    {
        _mapper = mapper;
        _answerRepository = answerRepository;
    }
    
    public async Task<AnswerDto> Handle(UpdateAnswerCommand request, CancellationToken cancellationToken = default)
    {
        var answer = await _answerRepository.GetByIdAsync(request.Id, cancellationToken);

        if (answer == null)
        {
            throw new NotFoundException("Answer");
        }

        answer.IsChanged = true;
        _mapper.Map(request, answer);
        await _answerRepository.UpdateAsync(request.Id, answer, cancellationToken);
        var result = _mapper.Map<AnswerDto>(answer);
        
        return result;
    }
}