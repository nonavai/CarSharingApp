using System.Linq.Expressions;
using CarSharingApp.CarService.Domain.Entities;
using CarSharingApp.CarService.Domain.Enums;
using CarSharingApp.CarService.Domain.Specifications.SpecSettings;

namespace CarSharingApp.CarService.Domain.Specifications;

public class CarSpecification : ISpecification<Car>
{
    public CarSpecification(Expression<Func<Car, bool>> criteria)
    {
        Criteria = criteria;
    }
    
    public Expression<Func<Car, bool>> Criteria { get; private set; }
    public List<Expression<Func<Car, object>>> Includes { get; } = new();

    public CarSpecification WithCarStatus()
    {
        Includes.Add(u => u.CarState);
        return this;
    }

    public CarSpecification WithComments()
    {
        Includes.Add(u => u.Comments);
        return this;
    }

    public CarSpecification FilterCars(
        int? minYear, int? maxYear, int? minPrice,
        int? maxPrice, VehicleType? vehicleType,
        FuelType? fuelType, string? mark, string? model)
    {
        var predicate = PredicateBuilder.True<Car>();

        if (minYear.HasValue)
        {
            predicate = predicate.And(car => car.Year >= minYear.Value);
        }

        if (maxYear.HasValue)
        {
            predicate = predicate.And(car => car.Year <= maxYear.Value);
        }

        if (minPrice.HasValue)
        {
            predicate = predicate.And(car => car.Price >= minPrice.Value);
        }

        if (maxPrice.HasValue)
        {
            predicate = predicate.And(car => car.Price <= maxPrice.Value);
        }

        if (vehicleType.HasValue)
        {
            predicate = predicate.And(car => car.VehicleType == vehicleType.Value);
        }

        if (fuelType.HasValue)
        {
            predicate = predicate.And(car => car.FuelType == fuelType.Value);
        }

        if (!string.IsNullOrEmpty(mark))
        {
            predicate = predicate.And(car => car.Mark == mark);
        }

        if (!string.IsNullOrEmpty(model))
        {
            predicate = predicate.And(car => car.Model == model);
        }

        Criteria = Criteria.And(predicate);
        
        return this;
    }
}