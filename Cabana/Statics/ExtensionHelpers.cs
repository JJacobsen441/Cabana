using System.Collections.Generic;
using System.Linq;

namespace Cabana.Statics
{
    public static class Extensions
    {
        public static bool IsNull<T>(this T source)
        {
            return source == null;
        }

        public static bool IsNotNull<T>(this T source)
        {
            return source != null;
        }

        public static bool IsNullOrEmpty<T>(this ICollection<T> value) where T : class
        {
            return value == null || value.Count() == 0;
        }

        public static bool IsNullOrEmpty(this string value)
        {
            return value == null || value.Length == 0;
        }
    }
}