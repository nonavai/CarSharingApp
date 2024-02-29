using AutoMapper;
using CarSharingApp.CarService.Application.Commands.CarCommands;
using CarSharingApp.CarService.Application.DTO_s.Car;
using CarSharingApp.CarService.Application.Repositories;
using CarSharingApp.CarService.Domain.Entities;
using CarSharingApp.CarService.Domain.Enums;
using CarSharingApp.CarService.Domain.Exceptions;
using MediatR;
using UserService;

namespace CarSharingApp.CarService.Application.CommandHandlers.CarCommandHandlers;

public class CreateCarHandler : IRequestHandler<CreateCarCommand, CarDto>
{
    private readonly ICarRepository _carRepository;
    private readonly ICarStateRepository _carStateRepository;
    private readonly IMapper _mapper;
    private readonly User.UserClient _userClient;
    
    public CreateCarHandler(IMapper mapper, ICarRepository carRepository, ICarStateRepository carStateRepository, User.UserClient userClient)
    {
        _mapper = mapper;
        _carRepository = carRepository;
        _carStateRepository = carStateRepository;
        _userClient = userClient;
    }

    public async Task<CarDto> Handle(CreateCarCommand command, CancellationToken cancellationToken)
    {
        var userResponse = await _userClient.IsUserExistAsync(new UserExistRequest
        {
            UserId = command.UserId
        });
        
        if (!userResponse.Exists)
        {
            throw new NotFoundException("User");
        }
        
        var car = _mapper.Map<Car>(command);
        var newCar = await _carRepository.AddAsync(car, cancellationToken);
        var newCarState = new CarState
        {
            Status = Status.Free,
            IsActive = true,
            Latitude = 0,
            Longitude = 0,
            Car = newCar
        };
        var carState = await _carStateRepository.AddAsync(newCarState, token: cancellationToken);
        await _carRepository.SaveChangesAsync(cancellationToken);
        newCar.CarState = carState;
        var newCarDto = _mapper.Map<CarDto>(newCar);

        return newCarDto;
    }
}