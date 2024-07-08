using FRM.Application.Interfaces;
using FRM.Domain.Models;
using FRM.Infrastructure.DataContexts;
using Microsoft.EntityFrameworkCore;

namespace FRM.Application.Services;

public class AuthService(
    ITokenService tokenService,
    FRMContext dbContext) : IAuthService
{
    public async ValueTask<string> LoginAsync(LoginDetails loginDetails)
    {
        var user = await dbContext.Users
            .FirstOrDefaultAsync(user => user.UserName == loginDetails.Username);

        if(user is null)
        {
            throw new Exception("User not found!");
        }
        else if(user.Password != loginDetails.Password)
        {
            throw new Exception("Password is wrong!");
        }
        
        return tokenService.Generate(user);
    }
}
