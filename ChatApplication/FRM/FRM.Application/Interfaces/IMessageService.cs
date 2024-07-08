using FRM.Domain.DTOs;
using FRM.Domain.Entities;

namespace FRM.Application.Interfaces;

public interface IMessageService
{
    ValueTask<bool> CreateMessageAsync(Guid userId, MessageDTO messageDTO);
    ValueTask<ICollection<Message>> GetAllMessagesAsync();
    ValueTask<Message?> GetMessageByIdAsync(Guid id);
    ValueTask<bool> UpdateMessageAsync(Guid id, MessageDTO messageDTO);
    ValueTask<bool> DeleteMessageByIdAsync(Guid id);
}
