using FRM.Domain.Entities;

namespace FRM.Application.Interfaces;

public interface ITokenService
{
    string Generate(User user);
}
