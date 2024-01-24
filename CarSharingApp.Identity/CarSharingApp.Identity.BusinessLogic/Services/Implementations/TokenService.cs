using CarSharingApp.Identity.DataAccess.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CarSharingApp.Identity.Shared.Exceptions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CarSharingApp.Identity.BusinessLogic.Services.Implementations;

public class TokenService : ITokenService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;

    public TokenService(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    public async Task<List<Claim>> AddClaims(string userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);

        if (user == null)
        {
            throw new NotFoundException("User Not Found");
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
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: _configuration["JwtSettings:Issuer"],
            audience: _configuration["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddDays(7),
            signingCredentials: creds);
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<string> GetUserIdFromToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var validToken = token.Split()[^1];
        var securityToken = tokenHandler.ReadJwtToken(validToken);
        
        if (securityToken == null)
        {
            throw new NotVerifiedException("Invalid token");
        }
        
        var claims = securityToken.Claims;
        var userIdClaim = claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti);
        
        if (userIdClaim == null)
        {
            throw new NotVerifiedException("Token does not contain a UserId claim");
        }

        var userId = userIdClaim.Value;
        
        return userId;
    }
}