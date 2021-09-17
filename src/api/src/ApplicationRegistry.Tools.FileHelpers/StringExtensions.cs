using System;
using System.Linq;

namespace System
{
    public static class StringExtensions
    {
        public static bool IsJson(this string text)
        {
            var firstCharacter = text.SkipWhile(c => Char.IsWhiteSpace(c)).FirstOrDefault();

            if (firstCharacter == default(Char))
                return false;

            switch(firstCharacter)
            {
                case '[':
                case '{':
                    return true;
                default:
                    return false;
            }
        }
    }
}
