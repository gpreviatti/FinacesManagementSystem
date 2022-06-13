using BC = BCrypt.Net.BCrypt;

namespace Helpers;

public static class EncryptHelper
{
    public static string HashField(string field)
    {
        return BC.HashPassword(field);
    }
    public static bool CheckHashedField(string field, string hash)
    {
        return BC.Verify(field, hash);
    }
}
