using AutoMapper;
using CarSharingApp.CarService.Application.Commands.CarCommands;
using CarSharingApp.CarService.Application.DTO_s.Car;
using CarSharingApp.CarService.Application.Repositories;
using CarSharingApp.CarService.Domain.Entities;
using CarSharingApp.CarService.Domain.Enums;
using MediatR;

namespace CarSharingApp.CarService.Application.CommandHandlers.CarCommandHandlers;

public class CreateCarHandler : IRequestHandler<CreateCarCommand, CarDto>
{
    private readonly ICarRepository _carRepository;
    private readonly ICarStateRepository _carStateRepository;
    private readonly IMapper _mapper;

    public CreateCarHandler(IMapper mapper, ICarRepository carRepository, ICarStateRepository carStateRepository)
    {
        _mapper = mapper;
        _carRepository = carRepository;
        _carStateRepository = carStateRepository;
    }

    public async Task<CarDto> Handle(CreateCarCommand command, CancellationToken cancellationToken)
    {
        var car = _mapper.Map<Car>(command);
        var newCar = await _carRepository.AddAsync(car, cancellationToken);
        var newCarState = await _carStateRepository.AddAsync(new CarState
        {
            Status = Status.Free,
            IsActive = true,
            Latitude = 0,
            Longitude = 0,
            Car = newCar
        }, cancellationToken);
        newCar.CarState = newCarState;
        var newCarDto = _mapper.Map<CarDto>(newCar);

        return newCarDto;
    }
}