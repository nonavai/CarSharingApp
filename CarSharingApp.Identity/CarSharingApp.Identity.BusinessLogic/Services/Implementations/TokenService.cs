﻿using CarSharingApp.Identity.DataAccess.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CarSharingApp.Identity.Shared.Constants;
using CarSharingApp.Identity.Shared.Exceptions;
using CarSharingApp.Identity.Shared.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CarSharingApp.Identity.BusinessLogic.Services.Implementations;

public class TokenService : ITokenService
{
    private readonly IUserRepository _userRepository;
    private readonly IOptions<JwtOptions> _options;

    public TokenService(IUserRepository userRepository, IOptions<JwtOptions> options)
    {
        _userRepository = userRepository;
        _options = options;
    }

    private async Task<List<Claim>> AddClaims(string userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);

        if (user == null)
        {
            throw new NotFoundException(ErrorName.UserNotFound);
        }
        
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Jti, user.Id),
            new Claim(JwtRegisteredClaimNames.Sub, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
        };

        var roles = await _userRepository.GetRolesAsync(user);

        claims.AddRange(roles.Select(role =>
            new Claim(ClaimTypes.Role, role)));
        
        return claims;
    }

    public async Task<string> GenerateToken(string userId)
    {
        var claims = await AddClaims(userId);
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: _options.Value.Issuer,
            audience: _options.Value.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddDays(7),
            signingCredentials: creds);
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}