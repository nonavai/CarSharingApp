using CarSharingApp.CarService.Application.Commands.CommentCommands;
using CarSharingApp.CarService.Application.Queries.CommentQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarSharingApp.CarService.WebAPI.Controllers;

[Route("api/commetns")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly IMediator _mediator;

    public CommentController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    [Route("car")]
    public async Task<IActionResult> GetByCarAsync([FromQuery] GetCommentsByCarQuery query, CancellationToken token = default)
    {
        var response = await _mediator.Send(query, token);
        
        return Ok(response);
    }
    
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAsync([FromQuery] GetCommentQuery query, CancellationToken token = default)
    {
        var response = await _mediator.Send(query, token);
        
        return Ok(response);
    }
    
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddAsync(CreateCommentCommand command, CancellationToken token = default)
    {
        var response = await _mediator.Send(command, token);
        
        return Ok(response);
    }
    
    [HttpPut]
    [Authorize]
    public async Task<IActionResult> UpdateAsync(UpdateCommentCommand command, CancellationToken token = default)
    {
        var response = await _mediator.Send(command, token);
        
        return Ok(response);
    }
    
    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> DeleteAsync(DeleteCommentCommand query, CancellationToken token = default)
    {
        var response = await _mediator.Send(query, token);
        
        return Ok(response);
    }
}