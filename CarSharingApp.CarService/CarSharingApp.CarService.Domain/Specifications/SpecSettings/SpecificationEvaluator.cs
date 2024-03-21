using Microsoft.EntityFrameworkCore;

namespace CarSharingApp.CarService.Domain.Specifications.SpecSettings;

public static class SpecificationEvaluator
{
    public static IQueryable<T> ApplySpecification<T>(this IQueryable<T> query, ISpecification<T> spec) where T : class
    {
        var queryResult = query;

        queryResult = queryResult.Where(spec.Criteria);

        queryResult = spec.Includes.Aggregate(
            queryResult,
            (current, include) =>
                current.Include(include));

        return queryResult;
    }
}