namespace MyBank.Application.Interfaces;

public interface ICardNumberProvider
{
    bool CheckValidity(string cardNumber);
    public string GenerateCardNumber(int len);
    public int CalculateLuhnChecksum(string cardNumber);
    public string GenerateCardCvv(int len);
}
