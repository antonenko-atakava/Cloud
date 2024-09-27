using System.Security.Cryptography;
using System.Text;

namespace Cloud.Service.Infrastructure;

public class PasswordHasherService
{
    public static string HashPassword(string password, string salt)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            string saltedPassword = password + salt;
            
            byte[] saltedPasswordBytes = Encoding.UTF8.GetBytes(saltedPassword);
            byte[] hashBytes = sha256.ComputeHash(saltedPasswordBytes);
            
            return Convert.ToBase64String(hashBytes);
        }
    }
}