using CarSharingApp.CarService.Application.Commands.CommentCommands;
using FluentValidation;

namespace CarSharingApp.CarService.Application.Validators;

public class UpdateCommentValidation : AbstractValidator<UpdateCommentCommand>
{
    public UpdateCommentValidation()
    {
        RuleFor(comment => comment.Rating)
            .InclusiveBetween(0, 10)
            .WithMessage("Rating must be between 0 and 10.");

        RuleFor(comment => comment.Text)
            .NotEmpty().WithMessage("Text is required.")
            .MaximumLength(500).WithMessage("Message cannot exceed 500 characters");
    }
}