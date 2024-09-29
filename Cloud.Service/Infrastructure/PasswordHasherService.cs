using System.Security.Cryptography;
using System.Text;

namespace Cloud.Service.Infrastructure;

public class PasswordHasherService
{
    public static string HashPassword(string password, string salt)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            string saltedPassword = $"{password}::{salt}";
            
            byte[] bytes = Encoding.UTF8.GetBytes(saltedPassword);
            byte[] hash = sha256.ComputeHash(bytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }
            return sb.ToString();
        }
    }
}