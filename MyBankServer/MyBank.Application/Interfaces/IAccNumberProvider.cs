namespace MyBank.Application.Interfaces;

public interface IAccNumberProvider
{
    public string GenerateIBAN(int accountId);
}
