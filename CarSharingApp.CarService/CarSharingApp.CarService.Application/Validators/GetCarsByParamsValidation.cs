using CarSharingApp.CarService.Application.Queries.CarQueries;
using FluentValidation;

namespace CarSharingApp.CarService.Application.Validators;

public class GetCarsByParamsValidation : AbstractValidator<GetCarsByParamsQuery>
{
    public GetCarsByParamsValidation()
    {
        RuleFor(car => car.MinYear)
            .InclusiveBetween(1900, DateTime.UtcNow.Year)
            .WithMessage("Year must be between 1900 and the current year.");
        
        RuleFor(car => car.MaxYear)
            .InclusiveBetween(1900, DateTime.UtcNow.Year)
            .WithMessage("Year must be between 1900 and the current year.");
        
        RuleFor(car => car.Mark)
            .MaximumLength(30).WithMessage("Car mark cannot exceed 30 characters.");

        RuleFor(car => car.Model)
            .MaximumLength(30).WithMessage("Car model cannot exceed 30 characters.");
        
        RuleFor(car => car.MinPrice)
            .GreaterThan(0).WithMessage("Price must be greater than 0.");
        
        RuleFor(car => car.MaxPrice)
            .GreaterThan(0).WithMessage("Price must be greater than 0.");
        
        RuleFor(location => location.Latitude)
            .InclusiveBetween(-90, 90)
            .WithMessage("Latitude must be between -90 and 90 degrees.");
        
        RuleFor(location => location.Longitude)
            .InclusiveBetween(-180, 180)
            .WithMessage("Longitude must be between -180 and 180 degrees.");
    }
}