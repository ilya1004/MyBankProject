namespace MyBank.API.Utils;

public static class StringDecoder
{
    static public byte[] Base64Decode(string base64String)
    {
        int padding = getBase64Padding(base64String);
        int length = base64String.Length * 6 / 8 - padding;
        byte[] buffer = new byte[length];

        int bufferIndex = 0;
        int bitBuffer = 0;
        int bitCount = 0;

        foreach (char c in base64String)
        {
            int value = getBase64Value(c);
            if (value >= 0)
            {
                bitBuffer = (bitBuffer << 6) | value;
                bitCount += 6;

                if (bitCount >= 8)
                {
                    bitCount -= 8;
                    buffer[bufferIndex++] = (byte)(bitBuffer >> bitCount);
                }
            }
        }

        return buffer;
    }

    static private int getBase64Padding(string base64String)
    {
        int padding = 0;

        if (base64String.Length > 0 && base64String[base64String.Length - 1] == '=')
            padding++;
        if (base64String.Length > 1 && base64String[base64String.Length - 2] == '=')
            padding++;

        return padding;
    }

    static private int getBase64Value(char c)
    {
        if (c >= 'A' && c <= 'Z')
            return c - 'A';
        if (c >= 'a' && c <= 'z')
            return c - 'a' + 26;
        if (c >= '0' && c <= '9')
            return c - '0' + 52;
        if (c == '+')
            return 62;
        if (c == '/')
            return 63;

        return -1;
    }
}
