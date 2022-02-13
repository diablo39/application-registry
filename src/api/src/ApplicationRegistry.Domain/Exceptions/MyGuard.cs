using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationRegistry.Domain.Exceptions
{
    internal static class MyGuard
    {
        public static void IsNotDefault<T>(T value, string fieldName)
            where T : struct, IEquatable<T>
        {
            if (value.Equals(default))
            {
                throw new DomainException(fieldName, "Field is required");
            }
        }

        public static void IsNotNullOrWhiteSpace(string value, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new DomainException(fieldName, "Field is required");
            }
        }
    }
}
