using System;
using System.Security.Cryptography;

namespace ltunes.Security.Tools
{
    public class SecurityHelper
    {
        public static string GenerateSalt(int nSalt)
        {
            var saltBytes = new byte[nSalt];

            using (var provider = new RNGCryptoServiceProvider())
            {
                provider.GetNonZeroBytes(saltBytes);
            }

            return Convert.ToBase64String(saltBytes);
        }

        public static string HashPassword(string password, string salt)
        {
            var saltBytes = Convert.FromBase64String(salt);

            using var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, 10101);
            return Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(70));
        }
    }
}