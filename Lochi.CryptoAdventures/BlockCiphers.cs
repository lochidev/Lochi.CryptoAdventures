public static class BlockCiphers
{
    public static void RunAes(string input)
    {
        // Create a new instance of the Aes
        // class.  This generates a new key and initialization
        // vector (IV).
        using Aes myAes = Aes.Create();
        // Encrypt the string to an array of bytes.
        byte[] encrypted = EncryptStringToBytes_Aes(input, myAes.Key, myAes.IV);

        // Decrypt the bytes to a string.
        string roundtrip = DecryptStringFromBytes_Aes(encrypted, myAes.Key, myAes.IV);

        //Display the original data and the decrypted data.
        Console.WriteLine("Original:   {0}", input);
        Console.WriteLine("Encrypted: {0}", Convert.ToBase64String(encrypted));
        Console.WriteLine("Decrypted: {0}", roundtrip);
    }
    private static byte[] EncryptStringToBytes_Aes(string plainText, byte[] key, byte[] iv)
    {
        // Check arguments.
        if (plainText is not {Length: > 0})
            throw new ArgumentNullException(nameof(plainText));
        if (key is not {Length: > 0})
            throw new ArgumentNullException(nameof(key));
        if (iv is not {Length: > 0})
            throw new ArgumentNullException(nameof(iv));

        // Create an Aes object
        // with the specified key and IV.
        using Aes aesAlg = Aes.Create();
        aesAlg.Key = key;
        aesAlg.IV = iv;

        // Create an encryptor to perform the stream transform.
        ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

        // Create the streams used for encryption.
        using MemoryStream msEncrypt = new();
        using CryptoStream csEncrypt = new(msEncrypt, encryptor, CryptoStreamMode.Write);
        using (StreamWriter swEncrypt = new(csEncrypt))
        {
            //Write all data to the stream.
            swEncrypt.Write(plainText);
        }

        byte[] encrypted = msEncrypt.ToArray();

        // Return the encrypted bytes from the memory stream.
        return encrypted;
    }

    private static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] key, byte[] iv)
    {
        // Check arguments.
        if (cipherText is not {Length: > 0})
            throw new ArgumentNullException(nameof(cipherText));
        if (key is not {Length: > 0})
            throw new ArgumentNullException(nameof(key));
        if (iv is not {Length: > 0})
            throw new ArgumentNullException(nameof(iv));

        // Declare the string used to hold
        // the decrypted text.

        // Create an Aes object
        // with the specified key and IV.
        using Aes aesAlg = Aes.Create();
        aesAlg.Key = key;
        aesAlg.IV = iv;

        // Create a decryptor to perform the stream transform.
        ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

        // Create the streams used for decryption.
        using MemoryStream msDecrypt = new(cipherText);
        using CryptoStream csDecrypt = new(msDecrypt, decryptor, CryptoStreamMode.Read);
        using StreamReader srDecrypt = new(csDecrypt);
        // Read the decrypted bytes from the decrypting stream
        // and place them in a string.
        string plaintext = srDecrypt.ReadToEnd();

        return plaintext;
    }
}