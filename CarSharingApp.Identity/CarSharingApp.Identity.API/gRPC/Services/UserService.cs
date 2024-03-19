﻿using CarSharingApp.Identity.BusinessLogic.Services;
using Grpc.Core;
using UserService;

namespace CarSharingApp.Identity.API.gRPC.Services;

public class UserService : User.UserBase
{
    private readonly IUserManageService _userManageService;
    private readonly IRolesService _rolesService;

    public UserService(IUserManageService userManageService, IRolesService rolesService)
    {
        _userManageService = userManageService;
        _rolesService = rolesService;
    }

    public override async Task<UserExistResponse> IsUserExist(UserRequest request, ServerCallContext context)
    {
        var result = await _userManageService.IsUserExits(request.UserId);

        return new UserExistResponse
        {
            Exists = result
        };
    }

    public override async Task<GetUserRolesResponse> GetUserRoles(UserRequest request, ServerCallContext context)
    {
        var result = await _rolesService.GetUserRolesAsync(request.UserId);
        var response = new GetUserRolesResponse();
        var stringResult = result.Select(role => role.Name); 
        response.Roles.AddRange(stringResult);

        return response;
    }
}