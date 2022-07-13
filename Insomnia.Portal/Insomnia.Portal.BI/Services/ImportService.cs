using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insomnia.Portal.EF;
using Insomnia.Portal.BI.Interfaces;
using Insomnia.Portal.General.Exceptions;
using Insomnia.Portal.Data.Dto;
using Insomnia.Portal.Data.Return;
using Insomnia.Portal.General.Expansions;
using Insomnia.Portal.Data.ViewModels.Input;

namespace Insomnia.Portal.BI.Services
{
    public class ImportService : ILocationImport, ITimetableImport
    {
        private readonly ServiceDbContext _context;
        private readonly ISender _sender;
        private readonly IAdminLocation _location;
        private readonly IAdminTag _tag;
        private readonly IAdminDirection _direction;
        private (string, string)[] localCords = new[]
        {
            ("Инфоцентр", "54.67846, 35.08835"),
            ("Высотные качели (качели + лагерь)", "54.67701, 35.09315"),
            ("Бессонная галерея (выставка + аукцион)", "54.67693, 35.08885"),
            ("Детская поляна", "54.68054, 35.07885"),
            ("Ярмарка “Речная”(ярмарка + лагерь ярмарочников)", "54.67701, 35.08697"),
            ("Ярмарка “Полевая”(только ярмарка)", "54.67804, 35.08702"),
            ("Ярмарка “Лесная”(ярмарка + лагерь ярмарочников)", "54.6819, 35.09354"),
            ("Затмение. Мольберт + Арт-станция", "54.68264, 35.07547"),
            ("Баня ПРО…", "54.67931, 35.09573"),
            ("Кафе-библиотека “Locus Solus”", "54.67871, 35.07965"),
            ("Шатер “Хатифнариум”", "54.67849, 35.08685"),
            ("Шатер “Мастер-классы на ярмарке”", "54.6777, 35.08753"),
            ("Шатёр “Мастер-классы”", "54.67765, 35.08904"),
            ("Loverinth", "54.67891, 35.079"),
            ("Ветер в кронах", "54.68206, 35.07826"),
            ("Арт-объект “Х”", "54.67775, 35.09168"),
            ("Затмение. Мольберт + Арт-станция", "54.68221, 35.07596"),
            ("Затмение. Одуванчик", "54.67872, 35.09339"),
            ("Затмение. Ресса 6", "54.6824, 35.07489"),
            ("Затмение. Самолёт", "54.67799, 35.08575"),
            ("Индейская деревня", "54.6805, 35.07844"),
            ("Костер у фудкорта", "54.67843, 35.08989"),
            ("АниматорSKYя", "54.67664, 35.08909"),
            ("Экран “Полевой” + ЦУЭ", "54.68089, 35.08991"),
            ("Накрывашка “Боевые сказочники”", "54.68091, 35.09021"),
            ("Накрывашка “Фьюжн”", "54.68074, 35.09005"),
            ("Музыкальная сцена (сцена + хозяйственный бекстейдж)", "54.67636, 35.09058"),
            ("Экран “Детский” + ЦУЭ", "54.68039, 35.07801"),
            ("Лаборатория (лаборатория + админка)", "54.67605, 35.09372"),
            ("Витражи (сцена/кафе+ админка)", "54.68196, 35.09177"),
            ("Карусель “Заря” (карусель/кафе + лагерь)", "54.67712, 35.09612"),
            ("Диафильминариум (пространство-кинозал + лагерь)", "54.68226, 35.07651"),
            ("Экран “Речной” + ЦУЭ", "54.678, 35.08335"),
            ("Экран “Речной” + ЦУЭ", "54.67825, 35.08351"),
            ("Накрывашка “Психологическая беседка”", "54.6781, 35.08353"),
            ("Накрывашка “Территория тела”", "54.67816, 35.08332"),
            ("Хедлайнер", "54.67936, 35.08131"),
            ("VR-шатер", "54.67953, 35.09097"),
            ("Шатер анимации", "54.67737, 35.08832"),
            ("Кафе “Guzzler Bird” (кафе+ админка)", "54.67843, 35.08147"),
            ("Катавасия", "54.67741, 35.08514"),
            ("Кафе \"Ragnarock\"", "54.6776, 35.08431"),
            ("Кафе “Ништя4ная”(кафе+ лагерь)", "54.67629, 35.09146"),
            ("Детское кафе “Глаз да глаз”", "54.6805, 35.07729"),
            ("Кафе “Эль-Стейко” (кафе+ лагерь)", "54.67715, 35.08622"),
            ("Кинобар (кафе+ лагерь)", "54.68311, 35.09461"),
            ("Фудкорт (фудкорт с бэкстейджем + лагерь фудкортников)", "54.67857, 35.09037"),
            ("Автокемпинг", "54.68188, 35.09013"),
            ("Души", "54.68063, 35.09548"),
            ("Прокат палаток", "54.67963, 35.0873"),
            ("wc", "54.67988, 35.08363"),
            ("wc", "54.67983, 35.07782"),
            ("wc", "54.68116, 35.08796"),
            ("wc", "54.68117, 35.07807"),
            ("wc", "54.68214, 35.09266"),
            ("wc", "54.68178, 35.09121"),
            ("wc", "54.68083, 35.08164"),
            ("wc", "54.67628, 35.09104"),
            ("wc", "54.67646, 35.09631"),
            ("wc", "54.67591, 35.09309"),
            ("wc", "54.67975, 35.0802"),
            ("wc", "54.68171, 35.07725"),
            ("wc", "54.68302, 35.07525"),
            ("wc", "54.67945, 35.07819"),
            ("wc", "54.6802, 35.07949"),
            ("wc", "54.68056, 35.08774"),
            ("wc", "54.68145, 35.08978"),
            ("wc", "54.67876, 35.08296"),
            ("wc", "54.67886, 35.0846"),
            ("wc", "54.67908, 35.08606"),
            ("wc", "54.67686, 35.0878"),
            ("wc", "54.68141, 35.08353"),
            ("wc", "54.68272, 35.09439"),
            ("wc", "54.68386, 35.0954"),
            ("wc", "54.68403, 35.08332"),
        };

        public ImportService(ServiceDbContext context, ISender sender, IAdminLocation location, IAdminTag tag, IAdminDirection direction)
        {
            _context = context;
            _sender = sender;
            _location = location;
            _tag = tag;
            _direction = direction;
        }

        public async Task<ImportReturn> Locations()
        {
            var locations = await _sender.Get<List<MiniLocation>>("https://agreemod.insomniafest.ru/agreemod/Notion/locations".FixUrl());

            locations.Add(new MiniLocation() { Name = "ws", ShortName = "Туалеты", Description = "Вы сами знаете что тут делать", Tags = new List<string>() { "Инфрастуктура", "С контентом", "Гигиена", "Гостевая зона" } });

            await _location.Clear();
            await _tag.Clear();
            await _direction.Clear();

            foreach (var location in locations.Where(x => x.Tags.Contains("Гостевая зона")))
            {
                var newLocation = new CreateLocation() { Tags = new List<int>(), Name = String.IsNullOrEmpty(location.ShortName) ? location.Name : location.ShortName, Description = location.Description };
                
                foreach (var tag in location.GetTags())
                {
                    var tagId = await _tag.AddOrGetId(new CreateTag() { Name = tag });

                    newLocation.Tags.Add(tagId);
                }

                var direction = location.GetDirection();
                newLocation.DirectionId = await _direction.AddOrGetId(new CreateDirection() { Name = direction });
                
                foreach (var newL in localCords.Where(x => x.Item1 == location.Name))
                {
                    newLocation.Lat = double.Parse(newL.Item2.Split(", ")[0]);
                    newLocation.Lon = double.Parse(newL.Item2.Split(", ")[1]);

                    await _location.Add(newLocation);
                }
            }

            return null;
        }

        public async Task<ImportReturn> Timetables()
        {
            return null;
        }
    }
}
