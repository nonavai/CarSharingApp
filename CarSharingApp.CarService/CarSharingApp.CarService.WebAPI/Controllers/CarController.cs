using CarSharingApp.CarService.Application.Commands.CarCommands;
using CarSharingApp.CarService.Application.Queries.CarQueries;
using CarSharingApp.CarService.WebAPI.Roles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarSharingApp.CarService.WebAPI.Controllers;

[Route("api/car")]
[ApiController]
public class CarController : ControllerBase
{
    private readonly IMediator _mediator;

    public CarController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAsync([FromQuery] GetCarQuery query)
    {
        var response = await _mediator.Send(query);
        
        return Ok(response);
    }
    
    [HttpGet]
    [Route("search")]
    public async Task<IActionResult> GetByParamsAsync([FromQuery] GetCarsByParamsQuery query)
    {
        var response = await _mediator.Send(query);
        
        return Ok(response);
    }
    
    [HttpGet]
    [Route("user")]
    [Authorize]
    public async Task<IActionResult> GetByUserAsync([FromQuery] GetCarByUserQuery query)
    {
        var response = await _mediator.Send(query);
        
        return Ok(response);
    }
    
    [HttpPost]
    [Authorize(Roles = RoleNames.Lender)]
    public async Task<IActionResult> AddAsync(CreateCarCommand command)
    {
        var response = await _mediator.Send(command);
        
        return Ok(response);
    }
    
    [HttpPut]
    [Authorize(Roles = RoleNames.Lender)]
    public async Task<IActionResult> UpdateAsync(UpdateCarCommand command)
    {
        var response = await _mediator.Send(command);
        
        return Ok(response);
    }
    
    [HttpDelete]
    [Authorize(Roles = RoleNames.Lender)]
    public async Task<IActionResult> DeleteAsync(DeleteCarCommand query)
    {
        var response = await _mediator.Send(query);
        
        return Ok(response);
    }
}