using CarSharingApp.CarService.Application.Commands.ImageCommands;
using CarSharingApp.CarService.Application.Queries.ImageQueries;
using CarSharingApp.CarService.WebAPI.Roles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarSharingApp.CarService.WebAPI.Controllers;

[Route("api/image")]
[ApiController]
public class ImageController : ControllerBase
{
    private readonly IMediator _mediator;

    public ImageController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetAsync([FromQuery] GetImagesByCarQuery query)
    {
        var response = await _mediator.Send(query);
        
        return Ok(response);
    }
    
    [DisableRequestSizeLimit]
    [HttpPost]
    [Route("")]
    //[Authorize(Roles = RoleNames.Lender)]
    public async Task<IActionResult> AddAsync([FromForm] CreateImageCommand command)
    {
        var response = await _mediator.Send(command);
        
        return Ok(response);
    }
    
    [HttpPut]
    [Route("")]
    [Authorize(Roles = RoleNames.Lender)]
    public async Task<IActionResult> UpdatePriorityAsync(UpdateImagePriorityCommand command)
    {
        var response = await _mediator.Send(command);
        
        return Ok(response);
    }
    
    [HttpDelete]
    [Route("")]
    //[Authorize(Roles = RoleNames.Lender)]
    public async Task<IActionResult> DeleteAsync(DeleteImageCommand query)
    {
        var response = await _mediator.Send(query);
        
        return Ok(response);
    }
}