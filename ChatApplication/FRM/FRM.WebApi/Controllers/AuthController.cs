using FRM.Application.Interfaces;
using FRM.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace FRM.WebApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class AuthController(
    IAuthService authService,
    ILogger<AuthController> logger) : ControllerBase
{
    [HttpPost]
    public async ValueTask<IActionResult> Login(LoginDetails loginDetails)
    {
        try
        {
            var token = await authService.LoginAsync(loginDetails);

            return Ok(token);
        }
        catch(Exception exception)
        {
            logger.LogError(exception.Message, exception.StackTrace);
            return BadRequest("Username or password is not valid");
        }
    }
}
