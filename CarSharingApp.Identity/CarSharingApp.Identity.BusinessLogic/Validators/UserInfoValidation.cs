using CarSharingApp.Identity.DataAccess.Entities;
using FluentValidation;

namespace CarSharingApp.Identity.BusinessLogic.Validators;

public class UserInfoValidation : AbstractValidator<UserInfo>
{
    public UserInfoValidation()
    {
        RuleFor(borrower => borrower.Birth)
            .NotEmpty().WithMessage("Birth date is required.");

        RuleFor(borrower => borrower.Country)
            .NotEmpty().WithMessage("Country is required.");

        RuleFor(borrower => borrower.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(50).WithMessage("First name cannot exceed 50 characters.");

        RuleFor(borrower => borrower.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters.");

        RuleFor(borrower => borrower.Category)
            .IsInEnum().WithMessage("Invalid licence category.");

        RuleFor(borrower => borrower.LicenceExpiry)
            .NotEmpty().WithMessage("Licence expiry date is required.")
            .Must((borrower, licenceExpiry) => licenceExpiry > borrower.Birth)
            .WithMessage("Licence expiry must be after birth date.");

        RuleFor(borrower => borrower.LicenceIssue)
            .NotEmpty().WithMessage("Licence issue date is required.")
            .Must((borrower, licenceIssue) => licenceIssue <= borrower.LicenceExpiry)
            .WithMessage("Licence issue date must be on or before licence expiry date.");

        RuleFor(borrower => borrower.LicenceId)
            .NotEmpty().WithMessage("Licence ID is required.")
            .Matches(@"^[A-Za-z0-9]+$").WithMessage("Licence ID must contain only letters and numbers.");

        RuleFor(borrower => borrower.PlaceOfIssue)
            .NotEmpty().WithMessage("Place of issue is required.")
            .MaximumLength(100).WithMessage("Place of issue cannot exceed 100 characters.");
    }
}