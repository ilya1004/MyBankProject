using System;

namespace MyBank.Application.Utils;

public class CardNumberProvider : ICardNumberProvider
{
    public bool CheckValidity(string cardNumber)
    {
        int sum = 0;
        for (int i = 0; i < cardNumber.Length; i++)
        {
            int digit = int.Parse(cardNumber[i].ToString());
            if (i % 2 == 0)
            {
                sum += (digit * 2 > 9) ? digit * 2 - 9 : digit * 2;
            }
            else
            {
                sum += digit;
            }
        }
        return sum % 10 == 0;
    }

    public string GenerateCardNumber(int len)
    {
        var random = new Random();
        var systemCode = "1";
        string cardNumber = systemCode;

        for (int i = 0; i < len - 2; i++)
        {
            cardNumber += random.Next(0, 10).ToString();
        }

        int checksum = CalculateLuhnChecksum(cardNumber);
        cardNumber += checksum;
        return cardNumber;
    }

    public int CalculateLuhnChecksum(string cardNumber)
    {
        int sum = 0;

        for (int i = 0; i < cardNumber.Length; i++)
        {
            int digit = int.Parse(cardNumber[i].ToString());

            if (i % 2 == 0)
            {
                sum += (digit * 2 > 9) ? digit*2 - 9 : digit*2;
            } 
            else
            {
                sum += digit;
            }
        }
        int module = sum % 10;
        int checksum = module == 0 ? 0 : 10 - module;

        return checksum;
    }

    public string GenerateCardCvv(int len)
    {
        var random = new Random();
        var cardCvv = "";
        for (int i = 0; i < len; i++)
        {
            cardCvv += random.Next(0, 10).ToString();
        }
        return cardCvv;
    }
}
