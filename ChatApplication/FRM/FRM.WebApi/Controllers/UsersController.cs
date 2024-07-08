using FRM.Application.Interfaces;
using FRM.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace FRM.WebApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class UsersController(IUserService userService) : ControllerBase
{
    [HttpPost]
    public async ValueTask<IActionResult> CreateUser(UserDTO userDTO)
    {
        var result = await userService.CreateUserAsync(userDTO);

        return result ? Ok("Added") : BadRequest("Error!");
    }

    [HttpGet]
    public async ValueTask<IActionResult> GetAllUsers()
    {
        var result = await userService.GetAll();
        return Ok(result);
    }

    [HttpGet]
    public async ValueTask<IActionResult> GetUserById(Guid id)
    {
        var result = await userService.GetUserByIdAsync(id);

        return result is not null 
            ? Ok(result)
            : NotFound("User not found!");
    }

    [HttpPut]
    public async ValueTask<IActionResult> UpdateUser(Guid id, UserDTO userDTO)
    {
        var result = await userService.UpdateUserAsync(id, userDTO);
        return result ? Ok("Updated") : BadRequest("Error!");
    }

    [HttpDelete]
    public async ValueTask<IActionResult> DeleteUserById(Guid id)
    {
        var result = await userService.DeleteUserByIdAsync(id);
        return result ? Ok("Deleted") : BadRequest("NotDeleted!");        
    }
}
