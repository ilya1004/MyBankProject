using MyBank.Core.Models;

namespace MyBank.Application.Interfaces
{
    public interface IJwtProvider
    {
        string GenerateToken(User user);
    }
}