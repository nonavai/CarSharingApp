using AutoMapper;
using CarSharingApp.CarService.Application.Commands.CarCommands;
using CarSharingApp.CarService.Application.Commands.CarStateCommands;
using CarSharingApp.CarService.Application.DTO_s.CarState;
using CarSharingApp.CarService.Application.Repositories;
using MediatR;

namespace CarSharingApp.CarService.Application.CommandHandlers.CarStateHandlers;

public class UpdateCarStatusHandler : IRequestHandler<UpdateCarStatusCommand, CarStateDto>
{
    private readonly ICarStateRepository _carStateRepository;
    private readonly IMapper _mapper;

    public UpdateCarStatusHandler(IMapper mapper, ICarStateRepository carStateRepository)
    {
        _mapper = mapper;
        _carStateRepository = carStateRepository;
    }

    public async Task<CarStateDto> Handle(UpdateCarStatusCommand command, CancellationToken cancellationToken)
    {
        var carState = await _carStateRepository.GetByCarIdAsync(command.CarId, cancellationToken);
        _mapper.Map(command, carState);
        var newCarState = await _carStateRepository.UpdateAsync(carState, cancellationToken);
        await _carStateRepository.SaveChangesAsync(cancellationToken);
        var carStateDto = _mapper.Map<CarStateDto>(newCarState);

        return carStateDto;
    }
}