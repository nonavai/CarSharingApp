using CarSharingApp.CarService.Application.DTO_s.Car;

namespace CarSharingApp.CarService.Application.Services;

public interface ICarManageService
{
    Task<IEnumerable<CarDto>>
        GetByRadiusAsync(float radiusKm, float latitude, float longitude, CancellationToken token);
    Task<IEnumerable<CarDto>> GetByParametersAsync();
}