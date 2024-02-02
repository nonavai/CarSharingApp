﻿using System.Linq.Expressions;
using AutoMapper;
using CarSharingApp.CarService.Application.DTO_s.Car;
using CarSharingApp.CarService.Application.Queries.CarQueries;
using CarSharingApp.CarService.Application.Repositories;
using CarSharingApp.CarService.Domain.Entities;
using CarSharingApp.CarService.Domain.Specifications;
using CarSharingApp.CarService.Domain.Specifications.SpecSettings;

namespace CarSharingApp.CarService.Application.QueryHandlers.CarQueryHandlers;

public class GetCarsByParamsHandler : IQueryHandler<GetCarsByParamsQuery, IEnumerable<CarDto>>
{
    private readonly ICarRepository _carRepository;
    private readonly IMapper _mapper;

    public GetCarsByParamsHandler(ICarRepository carRepository, IMapper mapper)
    {
        _carRepository = carRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CarDto>> Handle(GetCarsByParamsQuery query, CancellationToken token)
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
            query.Model
        );
        var cars = await _carRepository.GetBySpecAsync(spec, token);
        var carDtos = _mapper.Map<IEnumerable<CarDto>>(cars);

        return carDtos;
    }
}