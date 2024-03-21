using AutoMapper;
using CarSharingApp.CarService.Application.DTO_s.Car;
using CarSharingApp.CarService.Application.DTO_s.CarState;
using CarSharingApp.CarService.Application.Queries.CarStateQueries;
using CarSharingApp.CarService.Application.Repositories;
using CarSharingApp.CarService.Domain.Exceptions;
using MediatR;

namespace CarSharingApp.CarService.Application.QueryHandlers.CarStateHandlers;

public class GetCarStateHandler : IRequestHandler<GetCarStateQuery, CarStateDto>
{
    private readonly ICarStateRepository _carStateRepository;
    private readonly IMapper _mapper;

    public GetCarStateHandler(ICarStateRepository carStateRepository, IMapper mapper)
    {
        _carStateRepository = carStateRepository;
        _mapper = mapper;
    }

    public async Task<CarStateDto> Handle(GetCarStateQuery request, CancellationToken cancellationToken)
    {
        var carState = await _carStateRepository.GetByCarIdAsync(request.CarId, cancellationToken);

        if (carState == null)
        {
            throw new NotFoundException("Car");
        }
        
        var carStateDto = _mapper.Map<CarStateDto>(carState);

        return carStateDto;
    }
}