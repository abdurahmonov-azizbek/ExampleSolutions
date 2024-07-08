using FRM.Domain.DTOs;
using FRM.Domain.Entities;

namespace FRM.Application.Interfaces;

public interface IUserService
{
    ValueTask<bool> CreateUserAsync(UserDTO userDTO);
    ValueTask<ICollection<User>> GetAll();
    ValueTask<User?> GetUserByIdAsync(Guid id);
    ValueTask<bool> UpdateUserAsync(Guid id, UserDTO userDTO);
    ValueTask<bool> DeleteUserByIdAsync(Guid id);
}
