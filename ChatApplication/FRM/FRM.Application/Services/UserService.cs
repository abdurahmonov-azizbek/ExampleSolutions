using FRM.Application.Interfaces;
using FRM.Domain.DTOs;
using FRM.Domain.Entities;
using FRM.Infrastructure.DataContexts;
using Microsoft.EntityFrameworkCore;

namespace FRM.Application.Services;

public class UserService(FRMContext dbContext) : IUserService
{
    public async ValueTask<bool> CreateUserAsync(UserDTO userDTO)
    {
        try
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                UserName = userDTO.UserName,
                Password = userDTO.Password,
                CreatedDate = DateTime.UtcNow
            };

            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();

            return true;
        }
        catch
        {
            return false;
        }
    }

    public async ValueTask<bool> DeleteUserByIdAsync(Guid id)
    {
        try
        {
            var foundedUser = dbContext.Users.FirstOrDefault(user =>user.Id == id);

            if(foundedUser is null)
            {
                return false;
            }

            foundedUser.IsDeleted = true;
            foundedUser.DeletedDate = DateTime.UtcNow;
                
            dbContext.Users.Update(foundedUser);
            await dbContext.SaveChangesAsync();

            return true;
        }
        catch
        {
            return false;
        }
    }

    public async ValueTask<ICollection<User>> GetAll()
    {
        var users = await dbContext.Users.ToListAsync();
        return users;
    }

    public async ValueTask<User?> GetUserByIdAsync(Guid id)
    {
        var user = await dbContext.Users
            .FirstOrDefaultAsync(user => user.Id == id);

        return user;
    }

    public async ValueTask<bool> UpdateUserAsync(Guid id, UserDTO userDTO)
    {
        try
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(user => user.Id == id);

            if(user is not null)
            {
                user.FirstName = userDTO.FirstName;
                user.LastName = userDTO.LastName;
                user.UserName = userDTO.UserName;
                user.Password = userDTO.Password;
                user.UpdatedDate = DateTime.UtcNow;

                dbContext.Users.Update(user);
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
