using AutoMapper;
using CarSharingApp.CarService.Application.DTO_s.Car;
using CarSharingApp.CarService.Application.Queries.CarQueries;
using CarSharingApp.CarService.Application.Repositories;
using MediatR;

namespace CarSharingApp.CarService.Application.QueryHandlers.CarQueryHandlers;

public class GetCarByUserHandler : IRequestHandler<GetCarByUserQuery, IEnumerable<CarDto>>
{
    private readonly ICarRepository _carRepository;
    private readonly IMapper _mapper;

    public GetCarByUserHandler(ICarRepository carRepository, IMapper mapper)
    {
        _carRepository = carRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CarDto>> Handle(GetCarByUserQuery query, CancellationToken cancellationToken = default)
    {
        var cars = await _carRepository.GetByUserIdAsync(query.UserId);
        var carDtos = _mapper.Map<IEnumerable<CarDto>>(cars);

        return carDtos;
    }
}