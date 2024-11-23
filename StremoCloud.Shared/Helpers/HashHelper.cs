namespace StremoCloud.Shared.Helpers;
using BC = BCrypt.Net.BCrypt;
public static class HashHelper
{
    public static string GenerateHash(this string clearText)
    {
        return BC.HashPassword(clearText);
    }

    public static bool Verify(string clearText, string hashEquivalent)
    {
        return BC.Verify(clearText, hashEquivalent);
    }
}
