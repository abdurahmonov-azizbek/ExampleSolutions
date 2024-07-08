using FRM.Domain.Models;

namespace FRM.Application.Interfaces;

public interface IAuthService
{
    ValueTask<string> LoginAsync(LoginDetails loginDetails);
}
