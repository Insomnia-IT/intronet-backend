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
        private static string[] SystemLocations = new[]
        {
            "гостевая зона",
            "федеральная",
            "не федеральная"
        };

        private static string[] ArtDirections = new[]
        {
            "Свадебная локация",
            "Ветви Дерева",
            "Альпенисты",
            "Конкурс \"Затмение\""
        };

        private static string[] Lagerya = new[]
        {
            "Лесной лагерь “Байка”",
            "Лесной лагерь \"Байка\"",
            "Лагерь у детской поляны",
            "Автокемпинг"
        };


        private static string[] Lektorii = new[]
        {
            "Лекторий",
            "Шатер \"Хатифнариум\"",
            "Шатер “Хатифнариум”"
        };

        public static string GetDirection(this MiniLocation location)
        {
            if (location.Directions == null)
                return String.Empty;
            if (location.Name == "Кафе-библиотека “Locus Solus”" || location.Name == "Кафе-библиотека \"Locus Solus\"")
                return "locus";
            if (location.Directions.Contains("Еда на фестивале"))
                return "Кафе";
            if (location.Directions.Any(x => Lektorii.Contains(x)))
                return "Лекторий";
            if (location.ShortName == "Экран Полевой" || location.ShortName == "Экран Речной" || location.ShortName == "Экран Детский")
                return "Экран";
            if (location.Directions.Any(x => ArtDirections.Contains(x)))
                return "Арт-объект";
            if (location.Directions.Contains("КПП-1") || location.Directions.Contains("КПП-2"))
                return "КПП";
            if (location.Directions.Contains("Пожарная безопастность") || location.ShortName == "Большой костёр")
                return "Костёр";
            if (Lagerya.Contains(location.Name))
                return "Платный лагерь";
            if (location.Directions.Count == 1)
                return location.Directions[0];


            return null;
        }

        public static string[] GetTags(this MiniLocation locations)
        {
            return locations.Tags.Select(x => x.ToLower()).Where(x => !String.IsNullOrEmpty(x)).Where(x => !SystemLocations.Contains(x)).Select(x => x.Substring(0, 1).ToUpper() + x.Remove(0, 1)).ToArray();
        }
    }
}
