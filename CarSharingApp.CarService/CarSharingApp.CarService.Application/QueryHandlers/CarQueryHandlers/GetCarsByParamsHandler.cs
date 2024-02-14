using System.Linq.Expressions;
using AutoMapper;
using CarSharingApp.CarService.Application.DTO_s.Car;
using CarSharingApp.CarService.Application.DTO_s.Image;
using CarSharingApp.CarService.Application.Queries.CarQueries;
using CarSharingApp.CarService.Application.Repositories;
using CarSharingApp.CarService.Domain.Entities;
using CarSharingApp.CarService.Domain.Specifications;
using CarSharingApp.CarService.Domain.Specifications.SpecSettings;
using MediatR;

namespace CarSharingApp.CarService.Application.QueryHandlers.CarQueryHandlers;

public class GetCarsByParamsHandler : IRequestHandler<GetCarsByParamsQuery, IEnumerable<CarWithImageDto>>
{
    private readonly ICarRepository _carRepository;
    private readonly IMinioRepository _minioRepository;
    private readonly ICarImageRepository _carImageRepository;
    private readonly IMapper _mapper;

    public GetCarsByParamsHandler(ICarRepository carRepository, IMapper mapper, IMinioRepository minioRepository, ICarImageRepository carImageRepository)
    {
        _carRepository = carRepository;
        _mapper = mapper;
        _minioRepository = minioRepository;
        _carImageRepository = carImageRepository;
    }

    public async Task<IEnumerable<CarWithImageDto>> Handle(GetCarsByParamsQuery query, CancellationToken token)
    {
        Expression<Func<Car, bool>> customFilter = car => car.CarState.IsActive == query.IsActive;
        
        if (query.RadiusKm.HasValue && query.Latitude.HasValue && query.Longitude.HasValue)
        {
            var earthRadiusKm = 6371; 
            var maxDistance = query.RadiusKm / earthRadiusKm;
            customFilter = customFilter.And(car => Math.Acos(
                Math.Sin((double)(query.Latitude * (Math.PI / 180))) * Math.Sin(car.CarState.Latitude * (Math.PI / 180)) +
                Math.Cos((double)(query.Latitude * (Math.PI / 180))) * Math.Cos(car.CarState.Latitude * (Math.PI / 180)) *
                Math.Cos((double)((query.Longitude - car.CarState.Longitude) * (Math.PI / 180)))
            ) <= maxDistance);
        }
        
        var spec = new CarSpecification(customFilter).FilterCars(
            query.MinYear,
            query.MaxYear,
            query.MinPrice,
            query.MaxPrice,
            query.VehicleType,
            query.FuelType,
            query.Mark,
            query.Model,
            query.MinEngineCapacity,
            query.MaxEngineCapacity,
            query.WheelDrive
        );
        var cars = await _carRepository.GetBySpecAsync(spec, query.CurrentPage, query.PageSize, token);
        var carDtos = _mapper.Map<IEnumerable<CarWithImageDto>>(cars);
        var carsWithImage = carDtos.Select(async car =>
        {
            var primaryImage = await _carImageRepository.GetPrimaryAsync(car.Id, token);
            var memoryStream = await _minioRepository.GetAsync(primaryImage.Url, token);
            var bytes = memoryStream.ToArray();
            var base64String = Convert.ToBase64String(bytes);
            car.File = base64String;
            return car;
        });
        var result = await Task.WhenAll(carsWithImage);
        
        return result.AsEnumerable();
    }
}