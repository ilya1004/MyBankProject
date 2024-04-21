namespace MyBank.Application.Interfaces;

public interface IAccNumberProvider
{
    string GenerateIBAN(int accountId);
}
