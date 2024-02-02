using AutoMapper;
using CarSharingApp.CarService.Application.DTO_s.Car;
using CarSharingApp.CarService.Application.Queries.CarQueries;
using CarSharingApp.CarService.Application.Repositories;

namespace CarSharingApp.CarService.Application.QueryHandlers.CarQueryHandlers;

public class GetCarHandler: IQueryHandler<GetCarQuery, CarDto>
{
    private readonly ICarRepository _carRepository;
    private readonly IMapper _mapper;

    public GetCarHandler(ICarRepository carRepository, IMapper mapper)
    {
        _carRepository = carRepository;
        _mapper = mapper;
    }

    public async Task<CarDto> Handle(GetCarQuery query, CancellationToken token)
    {
        var car = await _carRepository.GetByIdAsync(query.Id);
        var carDto = _mapper.Map<CarDto>(car);

        return carDto;
    }
}