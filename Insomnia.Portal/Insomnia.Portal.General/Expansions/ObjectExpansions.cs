using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insomnia.Portal.General.Expansions
{
    public static class ObjectExpansions
    {
        public static double GetOrOther(this double element, double otherElement) => element == 0 ? otherElement : element;

        public static int GetOrOther(this int element, int otherElement) => element == 0 ? otherElement : element;

        public static string GetOrOther(this string element, string otherElement) => String.IsNullOrEmpty(element) ? otherElement : element;

        public static T[] GetOrOther<T>(this T[] element, T[] otherElement) => element.IsEmptyOrNull() ? otherElement : element;

        public static IEnumerable<T> GetOrOther<T>(this IEnumerable<T> element, IEnumerable<T> otherElement) => element.IsEmptyOrNull() ? otherElement : element;
    }
}
