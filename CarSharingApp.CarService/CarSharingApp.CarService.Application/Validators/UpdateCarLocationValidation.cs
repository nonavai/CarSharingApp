using CarSharingApp.CarService.Application.Commands.CarStateCommands;
using FluentValidation;

namespace CarSharingApp.CarService.Application.Validators;

public class UpdateCarLocationValidation : AbstractValidator<UpdateCarLocationCommand>
{
    public UpdateCarLocationValidation()
    {
        RuleFor(location => location.Latitude)
            .InclusiveBetween(-90, 90)
            .WithMessage("Latitude must be between -90 and 90 degrees.");
        
        RuleFor(location => location.Longitude)
            .InclusiveBetween(-180, 180)
            .WithMessage("Longitude must be between -180 and 180 degrees.");
    }
}