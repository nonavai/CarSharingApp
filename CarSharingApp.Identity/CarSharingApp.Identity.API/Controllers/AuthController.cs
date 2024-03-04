using CarSharingApp.Identity.BusinessLogic.Models.User;
using CarSharingApp.Identity.BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarSharingApp.Identity.API.Controllers;

[Route("api/user")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> LoginAsync(LogInDto dto)
    {
        var userDto = await _authService.LogInAsync(dto);

        return Ok(userDto);
    }

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> CreateAsync(UserNecessaryDto dto)
    {
        await _authService.RegistrationAsync(dto);
        
        return Created("User Added Successfully", dto);
    }
}