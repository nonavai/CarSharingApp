﻿namespace CarSharingApp.Identity.BusinessLogic.Services;

public interface ITokenService
{
    Task<string> GenerateToken(string userId);
}