using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FRM.Application.Interfaces;
using FRM.Domain.Constants;
using FRM.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace FRM.Application.Services;

public class TokenService(IConfiguration configuration) : ITokenService
{
    public string Generate(User user)
    {
        var claims = new Claim[]
        {
            new Claim(ClaimConstants.UserId, user.Id.ToString()),
            new Claim(ClaimConstants.Username, user.UserName)
        };

        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]!)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: configuration["JWT:Issuer"],
            audience: configuration["JWT:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddDays(1),
            signingCredentials: credentials);

        var tokenHandler = new JwtSecurityTokenHandler();

        return tokenHandler.WriteToken(token);
    }
}
