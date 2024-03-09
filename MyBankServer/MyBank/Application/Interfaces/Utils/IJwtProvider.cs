using MyBank.Core.Models;

namespace MyBank.Application.Interfaces.Utils
{
    public interface IJwtProvider
    {
        string GenerateToken(User user);
    }
}