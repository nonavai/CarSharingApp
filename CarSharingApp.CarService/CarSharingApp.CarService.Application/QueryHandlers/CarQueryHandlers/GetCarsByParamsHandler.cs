using System.Linq.Expressions;
using AutoMapper;
using CarSharingApp.CarService.Application.Caching;
using CarSharingApp.CarService.Application.DTO_s.Car;
using CarSharingApp.CarService.Application.DTO_s.Image;
using CarSharingApp.CarService.Application.Queries.CarQueries;
using CarSharingApp.CarService.Application.Repositories;
using CarSharingApp.CarService.Domain.Entities;
using CarSharingApp.CarService.Domain.Specifications;
using CarSharingApp.CarService.Domain.Specifications.SpecSettings;
using MediatR;
using Newtonsoft.Json;

namespace CarSharingApp.CarService.Application.QueryHandlers.CarQueryHandlers;

public class GetCarsByParamsHandler : IRequestHandler<GetCarsByParamsQuery, IEnumerable<CarWithImageDto>>
{
    private readonly ICarRepository _carRepository;
    private readonly IMinioRepository _minioRepository;
    private readonly ICarImageRepository _carImageRepository;
    private readonly ICacheService _cacheService;
    private readonly IMapper _mapper;

    public GetCarsByParamsHandler(ICarRepository carRepository, IMapper mapper, IMinioRepository minioRepository, ICarImageRepository carImageRepository, ICacheService cacheService)
    {
        _carRepository = carRepository;
        _mapper = mapper;
        _minioRepository = minioRepository;
        _carImageRepository = carImageRepository;
        _cacheService = cacheService;
    }

    public async Task<IEnumerable<CarWithImageDto>> Handle(GetCarsByParamsQuery query, CancellationToken token)
    {
        var serializedValue = JsonConvert.SerializeObject(query);
        var cache = await _cacheService.GetAsync<IEnumerable<CarWithImageDto>>(serializedValue);

        if (cache != null)
        {
            return cache;
        }

        Expression<Func<Car, bool>> customFilter = car => car.CarState.Status == query.Status;
        
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
        
        var spec = new CarSpecification(customFilter)
            .WithCarStatus()
            .FilterCars(
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
        var result = new List<CarWithImageDto>();
        
        foreach (var carDto in carDtos)
        {
            var primaryImage = await _carImageRepository.GetPrimaryAsync(carDto.Id, token);

            if (primaryImage != null)
            {
                var memoryStream = await _minioRepository.GetAsync(primaryImage.Url, token);
                var bytes = memoryStream.ToArray();
                var base64String = Convert.ToBase64String(bytes);
                carDto.File = base64String;
            }

            result.Add(carDto);
        }
        await _cacheService.SetAsync(serializedValue, result.AsEnumerable());
        
        return result.AsEnumerable();
    }
}