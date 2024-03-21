using CarSharingApp.DealService.BusinessLogic.Commands.FeedbackCommands;
using FluentValidation;

namespace CarSharingApp.DealService.BusinessLogic.Validators;

public class CreateFeedbackValidation: AbstractValidator<CreateFeedbackCommand>
{
    public CreateFeedbackValidation()
    {
        RuleFor(feedback => feedback.Text)
            .MinimumLength(5);
    }
}