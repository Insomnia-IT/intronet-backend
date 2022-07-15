using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insomnia.Portal.BI.Interfaces;
using Insomnia.Portal.Data.Return;
using Insomnia.Portal.EF;
using AutoMapper;
using Insomnia.Portal.Data.Dto;
using Microsoft.EntityFrameworkCore;
using Insomnia.Portal.General.Expansions;
using System.IO;

namespace Insomnia.Portal.BI.Services
{
    public class Cartoons : Base<Data.Entity.AnimationTimetable, Data.Dto.AnimationTimetable>, ICartoons, IAdminCartoons
    {
        private readonly IAnimationImport _import;

        public Cartoons(ServiceDbContext context, IMapper mapper, IAnimationImport import) : base(mapper, context)
        {
            _import = import;
        }

        public async Task<CartoonsReturn> GetAll()
        {
            var animations = await Animations.OrderByDescending(x => x.Id).ToListAsync();

            if (animations.IsEmptyOrNull())
                return NotFoundArray("Список анимаций пуст!");

            var model = _mapper.Map < List < Data.Entity.AnimationTimetable > , List <AnimationTimetable>>(animations);

            foreach(var anim in model)
            {
                anim.LocationId = _context.Locations.FirstOrDefault(x => x.Name == GetNameLocation(anim.Screen)).Id;
            }

            return new CartoonsReturn()
            {
                Success = true,
                Model = model
            };
        }

        private string GetNameScreen(string locationName) =>
            locationName switch
            {
                "Экран Детский" => "Детский Экран",
                "Экран Полевой" => "ЦУЭ 1",
                "Экран Речной" => "ЦУЭ 2",
            };

        private string GetNameLocation(string screenName) =>
        screenName switch
        {
            "Детский Экран" => "Экран Детский",
            "ЦУЭ 1" => "Экран Полевой",
            "ЦУЭ 2" => "Экран Речной",
        };

        public async Task<CartoonReturn> Get(int id)
        {
            var location = await _context.Locations.FirstOrDefaultAsync(x => x.Id == id);
            
            var animation = await Animations.FirstOrDefaultAsync(x => x.Screen == GetNameScreen(location.Name));

            if (animation == null)
                return NotFound("Список анимаций пуст!");

            return Ok(animation, location.Id);
        }

        public async Task<BaseReturn> Add(Stream stream)
        {
            var animations = await _import.GetAnimations(stream);

            return await AddToBd(animations);
        }

        private async Task<BaseReturn> AddToBd(List<AnimationTimetable> animations)
        {
            try
            {
                var entity = _mapper.Map<List<Data.Dto.AnimationTimetable>, List<Data.Entity.AnimationTimetable>>(animations);
                if (entity == null)
                    return Error("Не удалось добавить расписание!");

                await _context.AddRangeAsync(entity);
                await _context.SaveChangesAsync();

                return Ok<BaseReturn>();
            }
            catch (Exception ex)
            {
                //добавить логгер
                return Error(ex.Message);
            }
        }

        private IQueryable<Data.Entity.AnimationTimetable> Animations => _context.Animations
            .Include(x => x.Blocks).ThenInclude(x => x.Movies);

        private CartoonsReturn Ok(List<Data.Entity.AnimationTimetable> model) => Ok(model.ToDto<List<Data.Dto.AnimationTimetable>>(_mapper).ToReturn<CartoonsReturn>());

        private CartoonsReturn NotFoundArray(string errorMessage) => base.Error<CartoonsReturn>(errorMessage, Data.Enums.CodeRequest.NotFound);

        private CartoonReturn Ok(Data.Entity.AnimationTimetable model, int locationId)
        {
            var m = _mapper.Map<Data.Dto.AnimationTimetable>(model);

            m.LocationId = locationId;

            return new CartoonReturn()
            {
                Model = m,
                Success = true
            };
        }

        private CartoonReturn NotFound(string errorMessage) => base.Error<CartoonReturn>(errorMessage, Data.Enums.CodeRequest.NotFound);

        private BaseReturn Error(string errorMessage) => base.Error<BaseReturn>(errorMessage);
    }
}
