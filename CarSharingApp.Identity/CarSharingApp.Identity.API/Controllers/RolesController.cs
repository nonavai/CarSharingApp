﻿using CarSharingApp.Identity.BusinessLogic.Services;
using CarSharingApp.Identity.Shared.Constants;
using CarSharingApp.Identity.Shared.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarSharingApp.Identity.API.Controllers;

[Route("api/roles")]
[ApiController]
public class RolesController : ControllerBase
{
    private readonly IRolesService _rolesService;

    public RolesController(IRolesService rolesService)
    {
        _rolesService = rolesService;
    }

    [HttpGet]
    [Route("{userId}")]
    [Authorize]
    public async Task<IActionResult> GetUserRolesAsync([FromRoute] string userId)
    {
        var result = await _rolesService.GetUserRolesAsync(userId);
        
        return Ok(result);
    }
    
    [HttpPost]
    [Route("{userId}/{role}")]
    [Authorize(Roles = RoleNames.Admin)]
    public async Task<IActionResult> AddRoleAsync([FromRoute] string userId, [FromRoute] Roles role)
    {
        await _rolesService.AddUserRoleAsync(userId, role);
        
        return Ok();
    }
    
    [HttpDelete]
    [Route("{userId}/{role}")]
    [Authorize(Roles = RoleNames.Admin)]
    public async Task<IActionResult> RemoveRoleAsync([FromRoute] string userId, [FromRoute] Roles role, CancellationToken token)
    {
        token.ThrowIfCancellationRequested();

        await _rolesService.RemoveUserRoleAsync(userId, role, token);
        
        return Ok();
    }
}