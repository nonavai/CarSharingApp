using CarSharingApp.DealService.BusinessLogic.Commands.FeedbackCommands;
using CarSharingApp.DealService.BusinessLogic.Queries.FeedbackQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarSharingApp.DealService.API.Controllers;

[Route("api/feedback")]
[ApiController]
public class FeedbackController : ControllerBase
{
    private readonly IMediator _mediator;

    public FeedbackController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAsync([FromQuery] GetFeedbackByDealQueries query, CancellationToken token = default)
    {
        var response = await _mediator.Send(query, token);
        
        return Ok(response);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddAsync(CreateFeedbackCommand command, CancellationToken token = default)
    {
        var response = await _mediator.Send(command, token);
        
        return Ok(response);
    }
    
    [HttpPut]
    [Authorize]
    public async Task<IActionResult> UpdateAsync(UpdateFeedbackCommand command, CancellationToken token = default)
    {
        var response = await _mediator.Send(command, token);
        
        return Ok(response);
    }
    
    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> DeleteAsync(DeleteFeedbackCommand query, CancellationToken token = default)
    {
        var response = await _mediator.Send(query, token);
        
        return Ok(response);
    }
}