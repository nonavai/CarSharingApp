using AutoMapper;
using CarSharingApp.CarService.Application.Commands.CarCommands;
using CarSharingApp.CarService.Application.DTO_s.Car;
using CarSharingApp.CarService.Application.Repositories;
using CarSharingApp.CarService.Domain.Exceptions;
using MediatR;

namespace CarSharingApp.CarService.Application.CommandHandlers.CarCommandHandlers;

public class DeleteCarHandler : IRequestHandler<DeleteCarCommand, CarDto>
{
    private readonly ICarRepository _carRepository;
    private readonly IMapper _mapper;

    public DeleteCarHandler(IMapper mapper, ICarRepository carRepository)
    {
        _mapper = mapper;
        _carRepository = carRepository;
    }

    public async Task<CarDto> Handle(DeleteCarCommand command, CancellationToken cancellationToken)
    {
        var car = await _carRepository.GetByIdAsync(command.Id, cancellationToken);
        if (car == null)
        {
            throw new NotFoundException("Car not found");
        }
        var newCar = await _carRepository.DeleteAsync(command.Id, cancellationToken);
        var newCarDto = _mapper.Map<CarDto>(newCar);

        return newCarDto;
    }
}