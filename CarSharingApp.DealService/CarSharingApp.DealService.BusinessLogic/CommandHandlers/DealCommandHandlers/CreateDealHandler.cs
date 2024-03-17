using AutoMapper;
using CarService;
using CarSharingApp.DealService.BusinessLogic.Commands.DealCommands;
using CarSharingApp.DealService.BusinessLogic.Models.Deal;
using CarSharingApp.DealService.DataAccess.Entities;
using CarSharingApp.DealService.DataAccess.Repositories;
using CarSharingApp.DealService.Shared.Constants;
using CarSharingApp.DealService.Shared.Exceptions;
using Grpc.Core;
using Hangfire;
using MediatR;
using UserService;
using Status = CarService.Status;

namespace CarSharingApp.DealService.BusinessLogic.CommandHandlers.DealCommandHandlers;

public class CreateDealHandler : IRequestHandler<CreateDealCommand, DealDto>
{
    private readonly IDealRepository _dealRepository;
    private readonly IMapper _mapper;
    private readonly User.UserClient _userClient;
    private readonly Car.CarClient _carClient;

    public CreateDealHandler(IMapper mapper, IDealRepository dealRepository, User.UserClient userClient, Car.CarClient carClient)
    {
        _mapper = mapper;
        _dealRepository = dealRepository;
        _userClient = userClient;
        _carClient = carClient;
    }
    
    public async Task<DealDto> Handle(CreateDealCommand request, CancellationToken cancellationToken = default)
    {
        var userResponse = await _userClient.IsUserExistAsync(new UserExistRequest
        {
            UserId = request.UserId
        });
        
        if (!userResponse.Exists)
        {
            throw new NotFoundException("User");
        }
        
        var userRolesResponse = await _userClient.GetUserRolesAsync(new GetUserRolesRequest
        {
            UserId = request.UserId
        });
        
        if (userRolesResponse.Roles.All(role => role != RoleNames.Borrower))
        {
            throw new BadRequestException();
        }
        
        var carResponse = await _carClient.IsCarAvailableAsync(new CarAvailableRequest
        {
            UserId = request.CarId
        });
        
        if (!carResponse.IsAvailable)
        {
            throw new NotFoundException("User");
        }
        
        var deal = _mapper.Map<Deal>(request);
        deal.Requested = DateTime.Now;
        var entityResult = await _dealRepository.CreateAsync(deal, cancellationToken);
        var result = _mapper.Map<DealDto>(entityResult);
        BackgroundJob.Schedule<CancelDealHandler>(x =>
            x.Handle(new CancelDealCommand
            {
                Id = result.Id
            }, cancellationToken), 
            TimeSpan.FromMinutes(20));
        BackgroundJob.Enqueue<Car.CarClient>(x =>
            x.ChangeCarStatus(new ChangeStatus
            {
                CarId = result.CarId,
                Status = CarService.Status.Booking
            }, new CallOptions()));
        
        return result;
    }
}