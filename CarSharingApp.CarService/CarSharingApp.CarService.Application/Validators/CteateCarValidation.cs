using CarSharingApp.CarService.Application.Commands.CarCommands;
using FluentValidation;

namespace CarSharingApp.CarService.Application.Validators;

public class CreateCarValidation : AbstractValidator<CreateCarCommand>
{
    public CreateCarValidation()
    {
        RuleFor(car => car.Year)
            .InclusiveBetween(1900, DateTime.UtcNow.Year)
            .WithMessage("Year must be between 1900 and the current year.");

        RuleFor(car => car.RegistrationNumber)
            .NotEmpty().WithMessage("Registration number is required.")
            .Matches(@"^[A-Za-z0-9]+$").WithMessage("Registration number must contain only letters and numbers.");

        RuleFor(car => car.Mark)
            .NotEmpty().WithMessage("Car mark is required.")
            .MaximumLength(30).WithMessage("Car mark cannot exceed 30 characters.");

        RuleFor(car => car.Model)
            .NotEmpty().WithMessage("Car model is required.")
            .MaximumLength(30).WithMessage("Car model cannot exceed 30 characters.");

        RuleFor(car => car.VehicleBody)
            .NotEmpty()
            .MinimumLength(9).WithMessage("VehicleBody num must be longer than 9.")
            .MaximumLength(17).WithMessage("VehicleBody cannot exceed 17 characters.");

        RuleFor(car => car.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0.");

        RuleFor(car => car.Color)
            .MinimumLength(2).WithMessage("Min length must be greater than 2")
            .MaximumLength(20).WithMessage("Max length cannot exceed 20 characters");
        
        RuleFor(car => car.Description)
            .MaximumLength(500).WithMessage("Max length cannot exceed 500 characters");
    }
}