using System.Text.RegularExpressions;
using CarSharingApp.Identity.BusinessLogic.Models.User;
using FluentValidation;

namespace CarSharingApp.Identity.BusinessLogic.Validators;

public class UserValidator : AbstractValidator<UserDto>
{
    public UserValidator()
    {
        RuleFor(user => user.FirstName)
            .NotEmpty().WithMessage("First name is required.");
        
        RuleFor(user => user.LastName)
            .NotEmpty().WithMessage("Last name is required.");
        
        RuleFor(user => user.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Please provide a valid email address.");
        
        RuleFor(user => user.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
        
        RuleFor(user => user.PhoneNumber)
            .NotEmpty().WithMessage("PhoneNumber is required")
            .MinimumLength(10).WithMessage("PhoneNumber must not be less than 10 characters.")
            .MaximumLength(20).WithMessage("PhoneNumber must not exceed 50 characters.")
            .Matches(new Regex(@"^[+]?[(]?[0-9]{3}[)]?[-\s.]?[0-9]{3}[-\s.]?[0-9]{4,6}$"))
            .WithMessage("PhoneNumber not valid");

        RuleFor(user => user.RecordNumber)
            .NotEmpty().WithMessage("RecordNumber is required")
            .MinimumLength(5).WithMessage("RecordNumber must not be less than 5 characters.")
            .MaximumLength(30).WithMessage("RecordNumber must not exceed 30 characters.");
    }
}