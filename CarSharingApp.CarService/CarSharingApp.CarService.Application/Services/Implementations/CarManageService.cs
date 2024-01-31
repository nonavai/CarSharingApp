using System.Linq.Expressions;
using AutoMapper;
using CarSharingApp.CarService.Application.DTO_s.Car;
using CarSharingApp.CarService.Domain.Entities;
using CarSharingApp.CarService.Domain.Interfaces;
using CarSharingApp.CarService.Domain.Specifications;
using CarSharingApp.CarService.Domain.Specifications.SpecSettings;

namespace CarSharingApp.CarService.Application.Services.Implementations;

public class CarManageService : ICarManageService
{
    private readonly ICarRepository _carRepository;
    private readonly ICarStateRepository _carStateRepository;
    private readonly IMapper _mapper;

    public CarManageService(ICarRepository carRepository, IMapper mapper, ICarStateRepository carStateRepository)
    {
        _carRepository = carRepository;
        _mapper = mapper;
        _carStateRepository = carStateRepository;
    }

    public async Task<IEnumerable<CarDto>> GetByParametersAsync(CarFilterDto dto, CancellationToken token = default)
    {
        Expression<Func<Car, bool>> customFilter = car => car.CarState.IsActive == true;
        
        if (dto.RadiusKm.HasValue && dto.Latitude.HasValue && dto.Longitude.HasValue)
        {
            var earthRadiusKm = 6371; 
            var maxDistance = dto.RadiusKm / earthRadiusKm;
            customFilter = customFilter.And(car => Math.Acos(
                Math.Sin((double)(dto.Latitude * (Math.PI / 180))) * Math.Sin(car.CarState.Latitude * (Math.PI / 180)) +
                Math.Cos((double)(dto.Latitude * (Math.PI / 180))) * Math.Cos(car.CarState.Latitude * (Math.PI / 180)) *
                Math.Cos((double)((dto.Longitude - car.CarState.Longitude) * (Math.PI / 180)))
            ) <= maxDistance);
        }
        
        var spec = new CarSpecification(customFilter).FilterCars(
            dto.MinYear,
            dto.MaxYear,
            dto.MinPrice,
            dto.MaxPrice,
            dto.VehicleType,
            dto.FuelType,
            dto.Mark,
            dto.Model
        );
        var cars = await _carRepository.GetBySpecAsync(spec, token);
        var carDtos = _mapper.Map<IEnumerable<CarDto>>(cars);

        return carDtos;
    }
}