using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    public static class StringExtentions
    {
        public static string ConvertIpToHexString(this string ip)
        {
            var parts = ip.Split('.');

            if (parts.Length != 4) throw new ArgumentException(nameof(ip));

            var partsInt = parts.Select(e => int.TryParse(e, out var partInt) ? partInt : throw new ArgumentException(nameof(ip)));

            var sb = new StringBuilder("0x");

            foreach (var part in partsInt)
            {
                sb.Append(part.ToString("X2"));
            }

            return sb.ToString();
        }
    }
}
