using AutoMapper;
using CarSharingApp.CarService.Application.Commands.CarCommands;
using CarSharingApp.CarService.Application.DTO_s.Car;
using CarSharingApp.CarService.Application.Repositories;
using CarSharingApp.CarService.Domain.Exceptions;
using MediatR;

namespace CarSharingApp.CarService.Application.CommandHandlers.CarCommandHandlers;

public class DeleteCarByUserHandler : IRequestHandler<DeleteCarByUserCommand, IEnumerable<CarDto>>
{
    private readonly ICarRepository _carRepository;
    private readonly IMapper _mapper;

    public DeleteCarByUserHandler(ICarRepository carRepository, IMapper mapper)
    {
        _carRepository = carRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CarDto>> Handle(DeleteCarByUserCommand request, CancellationToken cancellationToken = default)
    {
        var cars = await _carRepository.GetByUserIdAsync(request.UserId);

        if (!cars.Any())
        {
            throw new NotFoundException("Cars");
        }
        
        await _carRepository.DeleteManyAsync(cars.ToArray());
        await _carRepository.SaveChangesAsync(cancellationToken);
        
        return _mapper.Map<IEnumerable<CarDto>>(cars);
    }
}