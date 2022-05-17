using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Insomnia.Portal.BI.Interfaces;
using Insomnia.Portal.Data.Return;
using Insomnia.Portal.Data.Dto;
using Insomnia.Portal.EF;
using Insomnia.Portal.Data.Filters;
using Insomnia.Portal.General.Expansions;
using Insomnia.Portal.Data.Enums;
using Insomnia.Portal.Data.ViewModels.Input;
using Insomnia.Portal.Data.Entity;

namespace Insomnia.Portal.BI.Services
{
    public class Schedule : Base<Timetable, TimetableDto>, ISchedule, IAdminSchedule
    {
        public Schedule(IMapper mapper, ServiceDbContext context) : base(mapper, context)
        {
        }

        public async Task Add(TimetableDto schedule)
        {

        }

        public async Task Edit(TimetableDto schedule)
        {
        }

        public async Task<ScheduleReturn> Get(int locationId)
        {
            return null;
        }

        public async Task GetTimetable(int timetableId)
        {

        }

        public async Task GetTimetableHistory(int timetableId)
        {

        }

        private TagReturn Ok(Timetable timetable) => Ok(timetable.ToDto<TimetableDto>(_mapper).ToReturn<TagReturn>());

        private TagReturn Error(string errorMessage) => base.Error<TagReturn>(errorMessage);

        private TagsReturn Ok(IList<Data.Entity.Tag> tag) => Ok(tag.ToDto<IList<TagDto>>(_mapper).ToReturn<TagsReturn>());

        private TagsReturn Errors(string errorMessage) => base.Error<TagsReturn>(errorMessage);

        private TagReturn NotFound(string errorMessage) => base.Error<TagReturn>(errorMessage, CodeRequest.NotFound);

        private TagsReturn NotFoundArray(string errorMessage) => base.Error<TagsReturn>(errorMessage, CodeRequest.NotFound);

        private IQueryable<Data.Entity.Tag> Tags => _context.Tags;
    }
}
