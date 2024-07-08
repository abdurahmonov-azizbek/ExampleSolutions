using FRM.Application.Interfaces;
using FRM.Domain.DTOs;
using FRM.Domain.Entities;
using FRM.Infrastructure.DataContexts;
using Microsoft.EntityFrameworkCore;

namespace FRM.Application.Services;

public class MessageService(FRMContext dbContext) : IMessageService
{
    public async ValueTask<bool> CreateMessageAsync(Guid userId, MessageDTO messageDTO)
    {
        try
        {
            var message = new Message
            {
                Id = Guid.NewGuid(),
                Content = messageDTO.Content,
                UserId = userId,
                CreatedDate = DateTime.UtcNow
            };

            await dbContext.Messages.AddAsync(message);
            await dbContext.SaveChangesAsync();

            return true;
        }
        catch
        {
            return false;
        }
    }

    public async ValueTask<bool> DeleteMessageByIdAsync(Guid id)
    {
        try
        {
            var message = await dbContext.Messages.FindAsync(id);

            if(message is null)
            {
                return false;
            }

            message.IsDeleted = true;
            message.DeletedDate = DateTime.UtcNow;
            dbContext.Messages.Update(message);
            await dbContext.SaveChangesAsync();

            return true;
        }
        catch
        {
            return true;
        }
    }

    public async ValueTask<ICollection<Message>> GetAllMessagesAsync()
    {
        var messages = await dbContext.Messages.ToListAsync();

        return messages;
    }

    public async ValueTask<Message?> GetMessageByIdAsync(Guid id)
    {
        var message = await dbContext.Messages
            .FirstOrDefaultAsync(message => message.Id == id);

        return message;
    }

    public async ValueTask<bool> UpdateMessageAsync(Guid id, MessageDTO messageDTO)
    {
        try
        {
            var message = await dbContext.Messages.FindAsync(id);

            if(message is not null)
            {
                message.Content = messageDTO.Content;
                message.UpdatedDate = DateTime.UtcNow;

                dbContext.Messages.Update(message);
                await dbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }
        catch
        {
            return false;
        }
    }
}
