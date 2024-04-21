using System.Numerics;

namespace MyBank.Application.Utils;


public class AccNumberProvider : IAccNumberProvider
{
    private int upperCharToInt(char c)
    {
        return (byte)c - 55;
    }

    private string upperStrToInt(string str)
    {
        string res = "";
        for (int i = 0; i < str.Length; i++)
        {
            if (48 <= (byte)str[i] && (byte)str[i] <= 57)
            {
                res += str[i];
            }
            else
            {
                res += upperCharToInt(str[i]).ToString();
            }
        }
        return res;
    }

    string generateAccNumberPrefix(int len)
    {
        string res = "";
        var random = new Random();
        for (int i = 0; i < len; i++)
        {
            res += random.Next(0, 10).ToString();
        }
        return res;
    }
    string generateBalanceAccNumber(int len)
    {
        var list = new List<char> { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
        'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O',
        'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        var random = new Random();
        string res = "";
        for (var i = 0; i < len; i++)
        {
            res += list[random.Next(0, list.Count)];
        }
        return res;
    }

    public string GenerateIBAN(int accountId)
    {
        string nationalCode = "BY";
        string nationalBankCode = "MYBK";

        string balanceAccNumber = generateBalanceAccNumber(4);

        string accNum = accountId.ToString().PadLeft(8, '0');

        string accNumberPrefix = generateAccNumberPrefix(8);

        string strToCheck = upperStrToInt(nationalBankCode) + upperStrToInt(balanceAccNumber) + accNumberPrefix + accNum + upperStrToInt(nationalCode);

        BigInteger bigInt = BigInteger.Parse(strToCheck + "00");

        BigInteger mod = bigInt % 97;

        string checkNumber = (98 - mod).ToString();

        string iban = "";
        iban += nationalCode;
        iban += checkNumber;
        iban += nationalBankCode;
        iban += balanceAccNumber;
        iban += accNumberPrefix;
        iban += accNum;

        return iban;
    }
}
