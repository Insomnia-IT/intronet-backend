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

            return Ok(animations);
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
        
        private BaseReturn Error(string errorMessage) => base.Error<BaseReturn>(errorMessage);
    }
}
