﻿using AutoMapper;
using BusinessLogic.Models.RefreshToken;
using BusinessLogic.Models.User;
using BusinessLogic.Services;
using CarSharingAPI.Identity;
using CarSharingAPI.Requests;
using CarSharingAPI.Requests.User;
using CarSharingAPI.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarSharingAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    private readonly ITokenService _tokenService;

    public UserController(IUserService userService, IMapper mapper, ITokenService tokenService)
    {
        _userService = userService;
        _mapper = mapper;
        _tokenService = tokenService;
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        if (!await _userService.ExistsAsync(id))
        {
            return NotFound();
        }

        var userDto = await _userService.GetByIdAsync(id);
        var response = _mapper.Map<UserResponse>(userDto);
        return Ok(response);
    }
    
    [HttpPost]
    [Route("LogIn")]
    public async Task<IActionResult> Login(LogInRequest entity)
    {
        var user = await _userService.GetByEmailAsync(entity.Email);
        
        if (user.Password != entity.Password)
        {
            return BadRequest("Wrong Password");
        }

        var response = _mapper.Map<LogInResponse>(user);
        var newRefreshToken = await _tokenService.GenerateRefreshToken(user);
        var accessToken = await _tokenService.GenerateAccessToken(newRefreshToken);
        response.RefreshToken = newRefreshToken.Token;
        response.accessToken = accessToken;
        Response.Cookies.Append("Authorization", accessToken);
        Response.Cookies.Append("AuthorizationRefresh", newRefreshToken.Token);
        return Ok(response);
    }

    [HttpPost]
    [Route("Add")]
    public async Task<IActionResult> Create(CreateUserRequest entity)
    {
        var dto = _mapper.Map<UserDto>(entity);
        var responseDto = await _userService.AddAsync(dto);
        var response = _mapper.Map<UserResponse>(responseDto);
        return Ok(response);
    }
    
    //[ValidateToken] //to make it work - comment that attribute
    [Authorize]
    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] UserRequest entity)
    {
        
        if (!await _userService.ExistsAsync(id))
        {
            return NotFound();
        }

        var userDto = _mapper.Map<UserDto>(entity);
        userDto.Id = id;
        var newUserDto = await _userService.UpdateAsync(userDto);
        var response = _mapper.Map<UserResponse>(newUserDto);
        return Ok(response);
    }
    
    [ValidateToken] //to make it work - comment that attribute
    [Authorize]
    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute]int id)
    {
        if (!await _userService.ExistsAsync(id))
        {
            return NotFound();
        }

        var responseDto = await _userService.DeleteAsync(id);
        var response = _mapper.Map<UserDto>(responseDto);
        return Ok(response);
    }
    
    
    [HttpPost]
    [Route("UpdateToken")]
    public async Task<IActionResult> RefreshToken()
    {
        var refreshToken = Request.Cookies["AuthorizationRefresh"];
        if (refreshToken == null)
        {
            return BadRequest("Invalid token");
        }

        var userId = await _tokenService.GetUserIdFromToken(refreshToken);
        var token = new RefreshTokenDto() 
            {
                UserId = userId,
                Token = refreshToken
            };
        var newAccessToken = await _tokenService.GenerateAccessToken(token);
        Response.Cookies.Delete("Authorization");
        Response.Cookies.Append("Authorization", newAccessToken);
        return Ok(newAccessToken);
    }
}