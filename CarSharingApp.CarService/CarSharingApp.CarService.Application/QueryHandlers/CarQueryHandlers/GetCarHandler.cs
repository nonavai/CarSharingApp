using AutoMapper;
using CarSharingApp.CarService.Application.Queries.CarQueries;
using CarSharingApp.CarService.Application.Repositories;
using CarSharingApp.CarService.Application.Responses.Car;
using MediatR;

namespace CarSharingApp.CarService.Application.QueryHandlers.CarQueryHandlers;

public class GetCarHandler: IRequestHandler<GetCarQuery, CarFullResponse>
{
    private readonly ICarRepository _carRepository;
    private readonly IMapper _mapper;

    public GetCarHandler(ICarRepository carRepository, IMapper mapper)
    {
        _carRepository = carRepository;
        _mapper = mapper;
    }

    public async Task<CarFullResponse> Handle(GetCarQuery query, CancellationToken token)
    {
        var car = await _carRepository.GetByIdWithInclude(query.Id, token);
        var carDto = _mapper.Map<CarFullResponse>(car);

        return carDto;
    }
}