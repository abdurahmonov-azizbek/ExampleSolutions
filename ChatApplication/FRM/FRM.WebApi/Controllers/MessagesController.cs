using FRM.Application.Interfaces;
using FRM.Domain.Constants;
using FRM.Domain.DTOs;
using FRM.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FRM.WebApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class MessagesController(IMessageService messageService) : ControllerBase
{
    [HttpPost]
    [Authorize]
    public async ValueTask<IActionResult> CreateMessage(MessageDTO messageDTO)
    {
        var userId = Guid.Parse(User.Claims.FirstOrDefault(claim => claim.Type == ClaimConstants.UserId)!.Value);
        var result = await messageService.CreateMessageAsync(userId, messageDTO);

        return result ? Ok("Added") : BadRequest("Error!");
    }

    [HttpGet]
    public async ValueTask<IActionResult> GetAllMessages()
    {
        var result = await messageService.GetAllMessagesAsync();
        return result is not null ? Ok(result) : NoContent();  
    }

    [HttpGet]
    public async ValueTask<IActionResult> GetMessageById(Guid id)
    {
        var result = await messageService.GetMessageByIdAsync(id);
        
        return result is not null
            ? Ok(result)
            : NotFound("Message not found!");
    }

    [HttpPut]
    [Authorize]
    public async ValueTask<IActionResult> UpdateMessage(Guid id, MessageDTO messageDTO)
    {
        var result = await messageService.UpdateMessageAsync(id, messageDTO);
        return result ? Ok("Updated") : BadRequest("Error!");
    }

    [HttpDelete]
    [Authorize]
    public async ValueTask<IActionResult> DeleteMessageById(Guid id)
    {
        var result = await messageService.DeleteMessageByIdAsync(id);

        return result 
            ? Ok("Deleted")
            : BadRequest("Error!");
    }
}