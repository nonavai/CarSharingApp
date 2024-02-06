﻿using CarSharingApp.CarService.Application.Commands.ImageCommands;
using CarSharingApp.CarService.Application.Queries.CarQueries;
using CarSharingApp.CarService.Application.Queries.ImageQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarSharingApp.CarService.WebAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class CarController : ControllerBase
{
    private readonly Mediator _mediator;

    public CarController(Mediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetAsync(GetCarQuery query)
    {
        var response = await _mediator.Send(query);
        
        return Ok(response);
    }
    
    [HttpPost]
    [Route("")]
    public async Task<IActionResult> AddAsync(CreateImageCommand command)
    {
        var response = await _mediator.Send(command);
        
        return Ok(response);
    }
    
    [HttpPut]
    [Route("")]
    public async Task<IActionResult> UpdatePriorityAsync(UpdateImagePriorityCommand command)
    {
        var response = await _mediator.Send(command);
        
        return Ok(response);
    }
    
    [HttpDelete]
    [Route("")]
    public async Task<IActionResult> DeleteAsync(DeleteImageCommand query)
    {
        var response = await _mediator.Send(query);
        
        return Ok(response);
    }
}