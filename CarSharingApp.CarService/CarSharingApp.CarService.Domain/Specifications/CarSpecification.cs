using System.Linq.Expressions;
using CarSharingApp.CarService.Domain.Entities;
using CarSharingApp.CarService.Domain.Specifications.SpecSettings;

namespace CarSharingApp.CarService.Domain.Specifications;

public class CarSpecification : ISpecification<Car>
{
    public CarSpecification(Expression<Func<Car, bool>> criteria)
    {
        Criteria = criteria;
    }
    public Expression<Func<Car, bool>> Criteria { get; }
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
    
    public CarSpecification WithPictures()
    {
        Includes.Add(u => u.Pictures);
        return this;
    }
}