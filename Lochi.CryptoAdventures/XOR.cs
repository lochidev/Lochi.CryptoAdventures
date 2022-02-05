
public static class XOR
{
    private static string xor(string text, string key)
    {
        var result = new StringBuilder();
        for (int c = 0; c < text.Length; c++)
            result.Append((char)((uint)text[c] ^ (uint)key[c % key.Length]));
        return result.ToString();
    }
    public static void RunXOR(string text, string key)
    {
        string encrypt = xor(text, key);
        string decrypt = xor(encrypt, key);
        Console.WriteLine($"Decryption is equal to text: {text == decrypt}");
        Console.WriteLine("Encrypt " + encrypt);
        Console.WriteLine("Decrypt " + decrypt);
    }
}

