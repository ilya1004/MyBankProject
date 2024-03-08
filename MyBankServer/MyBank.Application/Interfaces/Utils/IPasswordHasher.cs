namespace MyBank.Application.Interfaces.Utils
{
    public interface IPasswordHasher
    {
        string GenerateHash(string password);
        bool VerifyPassword(string password, string hashedPassword);
    }
}