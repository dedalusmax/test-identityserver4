using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;

namespace Test.IdentityServer4.NoData.Services
{
    public static class CryptographyService
    {
        public static string CreateHash(string value, string salt)
        {
            var bytes = KeyDerivation.Pbkdf2(value, Convert.FromBase64String(salt ?? string.Empty), KeyDerivationPrf.HMACSHA1, 10000, 256 / 8);
            var hash = Convert.ToBase64String(bytes);
            return hash;
        }

        public static string CreateSalt()
        {
            byte[] salt = new byte[128 / 8];
            using (var random = RandomNumberGenerator.Create())
            {
                random.GetBytes(salt);
            }

            return Convert.ToBase64String(salt);
        }
    }
}
