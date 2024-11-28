using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace StremoCloud.Shared.Helpers;

public class SecurityHelper(IConfiguration configuration) : ISecurityHelper
{
    public string Encrypt(string value)
    {
        string encryptionKey = configuration["Encryption:Key"]!;
        byte[] clearBytes = Encoding.Unicode.GetBytes(value);

        using (Aes encryptor = Aes.Create())
        {
            // Updated Rfc2898DeriveBytes constructor
            using (var pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
        }, 1000, HashAlgorithmName.SHA256)) // Specify iteration count and hash algorithm
            {
                encryptor.Key = pdb.GetBytes(32); // AES key size: 256 bits
                encryptor.IV = pdb.GetBytes(16);  // AES block size: 128 bits
            }

            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.FlushFinalBlock(); // Ensure all data is written
                }
                value = Convert.ToBase64String(ms.ToArray());
            }
        }

        return value;
    }

    public string Decrypt(string cipherText)
    {
        string encryptionKey = configuration["Encryption:Key"]!;
        cipherText = cipherText.Replace(" ", "+");
        byte[] cipherBytes = Convert.FromBase64String(cipherText);

        using (Aes encryptor = Aes.Create())
        {
            // Updated Rfc2898DeriveBytes constructor
            using (var pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
        }, 1000, HashAlgorithmName.SHA256)) // Specify iteration count and hash algorithm
            {
                encryptor.Key = pdb.GetBytes(32); // AES key size: 256 bits
                encryptor.IV = pdb.GetBytes(16);  // AES block size: 128 bits
            }

            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.FlushFinalBlock(); // Ensure all data is written
                }
                cipherText = Encoding.Unicode.GetString(ms.ToArray());
            }
        }

        return cipherText;
    }

}

public interface ISecurityHelper
{
    string Encrypt(string value);
    string Decrypt(string cipherText);
}