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
    [Route("")]
    public async Task<IActionResult> GetAsync([FromQuery] GetFeedbackByDealQueries query)
    {
        var response = await _mediator.Send(query);
        
        return Ok(response);
    }

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> AddAsync(CreateFeedbackCommand command)
    {
        var response = await _mediator.Send(command);
        
        return Ok(response);
    }
    
    [HttpPut]
    [Route("")]
    public async Task<IActionResult> UpdateAsync(UpdateFeedbackCommand command)
    {
        var response = await _mediator.Send(command);
        
        return Ok(response);
    }
    
    [HttpDelete]
    [Route("")]
    public async Task<IActionResult> DeleteAsync(DeleteFeedbackCommand query)
    {
        var response = await _mediator.Send(query);
        
        return Ok(response);
    }
}