using System;
using System.Security.Cryptography;
using System.Text;

namespace System
{
    public static class HashStringExtensions
    {
        public static string CalculateSHA256(this string text)
        {
            using (var algorithm = SHA256.Create())
            {
                // Create the at_hash using the access token returned by CreateAccessTokenAsync.
                var hashedBytes = algorithm.ComputeHash(Encoding.UTF8.GetBytes(text));

                var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

                return hash;
            }
        }
    }
}
