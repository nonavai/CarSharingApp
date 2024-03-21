using AutoMapper;
using CarSharingApp.CarService.Application.DTO_s.Car;
using CarSharingApp.CarService.Application.Queries.CarQueries;
using CarSharingApp.CarService.Application.Repositories;
using CarSharingApp.CarService.Domain.Exceptions;
using MediatR;

namespace CarSharingApp.CarService.Application.QueryHandlers.CarQueryHandlers;

public class GetCarHandler: IRequestHandler<GetCarQuery, CarFullDto>
{
    private readonly ICarRepository _carRepository;
    private readonly IMapper _mapper;

    public GetCarHandler(ICarRepository carRepository, IMapper mapper)
    {
        _carRepository = carRepository;
        _mapper = mapper;
    }

    public async Task<CarFullDto> Handle(GetCarQuery query, CancellationToken token)
    {
        var car = await _carRepository.GetByIdWithIncludeAsync(query.Id, token);

        if (car == null)
        {
            throw new NotFoundException("Car");
        }
        
        var carDto = _mapper.Map<CarFullDto>(car);

        return carDto;
    }
}