using CarSharingApp.CarService.Application.DTO_s.Car;

namespace CarSharingApp.CarService.Application.Services;

public interface ICarManageService
{
    Task<IEnumerable<CarDto>> GetByParametersAsync(CarFilterDto dto, CancellationToken token);
}