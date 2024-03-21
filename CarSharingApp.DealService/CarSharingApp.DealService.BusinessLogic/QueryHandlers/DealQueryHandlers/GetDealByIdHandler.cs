using AutoMapper;
using CarSharingApp.DealService.BusinessLogic.Models.Deal;
using CarSharingApp.DealService.BusinessLogic.Queries.DealQueries;
using CarSharingApp.DealService.DataAccess.Repositories;
using CarSharingApp.DealService.Shared.Exceptions;
using MediatR;

namespace CarSharingApp.DealService.BusinessLogic.QueryHandlers.DealQueryHandlers;

public class GetDealByIdHandler : IRequestHandler<GetDealByIdQuery, DealDto>
{
    private readonly IDealRepository _dealRepository;
    
    private readonly IMapper _mapper;

    public GetDealByIdHandler(IMapper mapper, IDealRepository dealRepository)
    {
        _mapper = mapper;
        _dealRepository = dealRepository;
     
    }
    
    public async Task<DealDto> Handle(GetDealByIdQuery request, CancellationToken cancellationToken = default)
    {
        var deal = await _dealRepository.GetByIdAsync(request.Id, cancellationToken);

        if (deal == null)
        {
            throw new NotFoundException("Deal");
        }
        
        var result = _mapper.Map<DealDto>(deal);

        return result;
    }
}