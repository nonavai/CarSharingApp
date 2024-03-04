using CarSharingApp.DealService.BusinessLogic.Commands.AnswerCommands;
using CarSharingApp.DealService.BusinessLogic.Queries.AnswerQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarSharingApp.DealService.API.Controllers;

[Route("api/answer")]
[ApiController]
public class AnswerController : ControllerBase
{
    private readonly IMediator _mediator;

    public AnswerController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAsync([FromQuery] GetAnswersByFeedbackQuery query, CancellationToken token = default)
    {
        var response = await _mediator.Send(query, token);
        
        return Ok(response);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddAsync(CreateAnswerCommand command, CancellationToken token = default)
    {
        var response = await _mediator.Send(command, token);
        
        return Ok(response);
    }
    
    [HttpPut]
    [Authorize]
    public async Task<IActionResult> UpdateAsync(UpdateAnswerCommand command, CancellationToken token = default)
    {
        var response = await _mediator.Send(command, token);
        
        return Ok(response);
    }
    
    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> DeleteAsync(DeleteAnswerCommand query, CancellationToken token = default)
    {
        var response = await _mediator.Send(query, token);
        
        return Ok(response);
    }
}