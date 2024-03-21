using CarSharingApp.DealService.BusinessLogic.Commands.AnswerCommands;
using FluentValidation;

namespace CarSharingApp.DealService.BusinessLogic.Validators;

public class CreateAnswerValidation : AbstractValidator<CreateAnswerCommand>
{
    public CreateAnswerValidation()
    {
        RuleFor(answer => answer.Text)
            .MinimumLength(5);
    }
}