using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insomnia.Portal.General.Expansions
{
    public static class EnumerableExpansions
    {
        public static bool IsEmptyOrNull<T>(this IEnumerable<T> enumerable)
        {
            if(enumerable == null)
                return true;

            return !enumerable.Any();
        }

        public static T[] ToArrayOrNull<T>(this IEnumerable<T> enumerable)
        {
            if(enumerable.Any())
                return enumerable.ToArray();
            return null;
        }

        public static IEnumerable<T> ToEnumerableOrNull<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable.Any())
                return enumerable.AsEnumerable();
            return null;
        }

        public static IList<T> ToListOrNull<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable.Any())
                return enumerable.ToList();
            return null;
        }

        public static IList<T> AddGroupBy<T>(this List<T> list, T element)
        {
            if(list.Contains(element))
                return list;
            
            list.Add(element);

            return list;
        }
    }
}
