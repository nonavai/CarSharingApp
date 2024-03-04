using CarSharingApp.DealService.BusinessLogic.Commands.DealCommands;
using CarSharingApp.DealService.BusinessLogic.Queries.DealQueries;
using CarSharingApp.DealService.Shared.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarSharingApp.DealService.API.Controllers;

[Route("api/deal")]
[ApiController]
public class DealController : ControllerBase
{
    private readonly IMediator _mediator;

    public DealController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    [Route("car")]
    public async Task<IActionResult> GetByCarAsync([FromQuery] GetDealsByCarQuery query, CancellationToken token = default)
    {
        var response = await _mediator.Send(query);
        
        return Ok(response);
    }
    
    [HttpGet]
    [Route("user")]
    [Authorize]
    public async Task<IActionResult> GetByUserAsync([FromQuery] GetDealsByUserQuery query, CancellationToken token = default)
    {
        var response = await _mediator.Send(query, token);
        
        return Ok(response);
    }

    [HttpPost]
    [Authorize(Roles = RoleNames.Borrower)]
    public async Task<IActionResult> AddAsync(CreateDealCommand command, CancellationToken token = default)
    {
        var response = await _mediator.Send(command, token);
        
        return Ok(response);
    }
    
    [HttpPut]
    [Route("complete")]
    [Authorize(Roles = RoleNames.Borrower)]
    public async Task<IActionResult> CompleteAsync(CompleteDealCommand command, CancellationToken token = default)
    {
        var response = await _mediator.Send(command, token);
        
        return Ok(response);
    }
    
    [HttpPut]
    [Route("confirm")]
    [Authorize(Roles = RoleNames.Borrower)]
    public async Task<IActionResult> ConfirmAsync(ConfirmDealCommand command, CancellationToken token = default)
    {
        var response = await _mediator.Send(command, token);
        
        return Ok(response);
    }
    
    [HttpPut]
    [Route("cancel")]
    [Authorize(Roles = RoleNames.Borrower)]
    public async Task<IActionResult> CancelAsync(CancelDealCommand command, CancellationToken token = default)
    {
        var response = await _mediator.Send(command, token);
        
        return Ok(response);
    }
}