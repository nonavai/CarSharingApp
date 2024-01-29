using AutoMapper;
using CarSharingApp.CarService.Application.DTO_s.Car;
using CarSharingApp.CarService.Domain.Interfaces;
using CarSharingApp.CarService.Domain.Specifications;

namespace CarSharingApp.CarService.Application.Services.Implementations;

public class CarManageService : ICarManageService
{
    private readonly ICarRepository _carRepository;
    private readonly IMapper _mapper;

    public CarManageService(ICarRepository carRepository, IMapper mapper)
    {
        _carRepository = carRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CarDto>> GetByRadiusAsync(float radiusKm, float latitude, float longitude, CancellationToken token)
    {
        var earthRadiusKm = 6371; 
        var maxDistance = radiusKm / earthRadiusKm;
        var spec = new CarSpecification(car => Math.Acos(
            Math.Sin(latitude * (Math.PI / 180)) * Math.Sin(car.CarState.Latitude * (Math.PI / 180)) +
            Math.Cos(latitude * (Math.PI / 180)) * Math.Cos(car.CarState.Latitude * (Math.PI / 180)) *
            Math.Cos((longitude - car.CarState.Longitude) * (Math.PI / 180))
        ) <= maxDistance);
        var cars = await _carRepository.GetBySpecAsync(spec, token);
        var carDtos = _mapper.Map<IEnumerable<CarDto>>(cars);

        return carDtos;
    }

    public async Task<IEnumerable<CarDto>> GetByParametersAsync()
    {
        throw new NotImplementedException();
    }
}