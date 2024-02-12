using CarSharingApp.CarService.Application.Commands.CommentCommands;
using CarSharingApp.CarService.Application.Queries.CommentQueries;
using MediatR;
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
    public async Task<IActionResult> GetByCarAsync(GetCommentsByCarQuery query)
    {
        var response = await _mediator.Send(query);
        
        return Ok(response);
    }
    
    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetAsync(GetCommentQuery query)
    {
        var response = await _mediator.Send(query);
        
        return Ok(response);
    }
    
    [HttpPost]
    [Route("")]
    public async Task<IActionResult> AddAsync(CreateCommentCommand command)
    {
        var response = await _mediator.Send(command);
        
        return Ok(response);
    }
    
    [HttpPut]
    [Route("")]
    public async Task<IActionResult> UpdateAsync(UpdateCommentCommand command)
    {
        var response = await _mediator.Send(command);
        
        return Ok(response);
    }
    
    [HttpDelete]
    [Route("")]
    public async Task<IActionResult> DeleteAsync(DeleteCommentCommand query)
    {
        var response = await _mediator.Send(query);
        
        return Ok(response);
    }
}