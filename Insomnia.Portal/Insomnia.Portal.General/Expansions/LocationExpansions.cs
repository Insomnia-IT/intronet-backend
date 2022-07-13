using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insomnia.Portal.Data.Dto;

namespace Insomnia.Portal.General.Expansions
{
    public static class LocationExpansions
    {
        public static string GetDirection(this MiniLocation location)
        {
            if (location.Directions == null)
                return String.Empty;
            if (location.Directions.Count == 1)
                return location.Directions[0];
            if (location.ShortName == "Экран Полевой" || location.ShortName == "Экран Речной")
                return "Экран";
            return null;
        }

        public static string[] GetTags(this MiniLocation locations)
        {
            return locations.Tags.Where(x => !String.IsNullOrEmpty(x)).Where(x => x != "Гостевая зона").ToArray();
        }
    }
}
