using AutoMapper;
using CarSharingApp.CarService.Application.Commands.CarCommands;
using CarSharingApp.CarService.Application.DTO_s.CarState;
using CarSharingApp.CarService.Application.Repositories;
using MediatR;

namespace CarSharingApp.CarService.Application.CommandHandlers.CarStateHandlers;

public class UpdateCarActivityHandler : IRequestHandler<UpdateCarActivityCommand, CarStateDto>
{
    private readonly ICarStateRepository _carStateRepository;
    private readonly IMapper _mapper;

    public UpdateCarActivityHandler(IMapper mapper, ICarStateRepository carStateRepository)
    {
        _mapper = mapper;
        _carStateRepository = carStateRepository;
    }

    public async Task<CarStateDto> Handle(UpdateCarActivityCommand command, CancellationToken cancellationToken)
    {
        var carState = await _carStateRepository.GetByCarIdAsync(command.CarId, cancellationToken);
        carState.Status = command.Status;
        var newCarState = await _carStateRepository.UpdateAsync(carState, cancellationToken);
        var carStateDto = _mapper.Map<CarStateDto>(newCarState);

        return carStateDto;
    }
}