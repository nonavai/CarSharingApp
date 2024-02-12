using AutoMapper;
using CarSharingApp.CarService.Application.Commands.CarCommands;
using CarSharingApp.CarService.Application.DTO_s.Car;
using CarSharingApp.CarService.Application.Repositories;
using CarSharingApp.CarService.Domain.Exceptions;
using MediatR;

namespace CarSharingApp.CarService.Application.CommandHandlers.CarCommandHandlers;

public class UpdateCarHandler : IRequestHandler<UpdateCarCommand, CarDto>
{
    private readonly ICarRepository _carRepository;
    private readonly IMapper _mapper;

    public UpdateCarHandler(IMapper mapper, ICarRepository carRepository)
    {
        _mapper = mapper;
        _carRepository = carRepository;
    }

    public async Task<CarDto> Handle(UpdateCarCommand command, CancellationToken cancellationToken)
    {
        var car = await _carRepository.GetByIdAsync(command.Id, cancellationToken);

        if (car == null)
        {
            throw new NotFoundException("Car Not Found");
        }
        
        var updatedCar = _mapper.Map(command, car);
        var newCar = await _carRepository.UpdateAsync(updatedCar, cancellationToken);
        await _carRepository.SaveChangesAsync(cancellationToken);
        var carDto = _mapper.Map<CarDto>(newCar);

        return carDto;
    }
}