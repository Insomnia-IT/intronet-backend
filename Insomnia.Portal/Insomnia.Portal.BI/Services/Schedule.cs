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

        private async Task<Timetable> GetEntityAsync(int timetableId)
        {
            return await Timetables.SingleOrDefaultAsync(x => x.Id == timetableId);
        }

        public async Task Add(TimetableDto schedule)
        {

        }

        public async Task<TimetableReturn> Edit(EditTimetable schedule)
        {
            try
            {
                var entity = await GetEntityAsync(schedule.Id);
                if (entity == null)
                    return NotFound("Расписание с указанным ID не найдено!");

                entity = _mapper.Map(schedule, entity);

                entity.Audiences = await EditAudiences(schedule, entity.Audiences);

                _context.Update(entity);
                await _context.SaveChangesAsync();
                return Ok<TimetableReturn>();
            }
            catch (Exception ex)
            {
                //добавить логгер
                return Error(ex.Message);
            }
        }

        private async Task<List<AudienceElement>> EditAudiences(EditTimetable schedule, List<AudienceElement> audiences)
        {
            var result = new List<AudienceElement>();
            foreach (var audience in audiences.Where(x => x.Id != 0))
            {
                var editAudience = GetAudienceElement(schedule, audience.Id);
                if (editAudience is null)
                    continue;

                audience.Elements = await UpdateElementtables(editAudience, audience);
                result.Add(audience);
            }
            foreach(var audience in audiences.Where(x => x.Id == 0))
            {
                var entity = _mapper.Map<AudienceElement>(audience);
                //entity.Elements = await UpdateElementtables(entity, audience);
                _context.Add(entity);
                result.Add(entity);
            }

            return result;
        }

        private EditAudienceElement GetAudienceElement(EditTimetable schedule, int audienceId) => schedule.Audiences.FirstOrDefault(x => x.Id == audienceId);

        private async Task<List<Elementtable>> UpdateElementtables(EditAudienceElement editAudience, AudienceElement audience)
        {
            var result = new List<Elementtable>();
            foreach(var element in audience.Elements.Where(x => x.Id != 0))
            {
                var editElement = GetElementtable(editAudience, element.Id);
                if (editElement is null)
                    continue;

                if (editElement.IsDeleted)
                {
                    _context.Remove(element);
                    continue;
                }

                var entity = element;

                entity.Name = String.IsNullOrEmpty(editElement.Name) ? entity.Name : editElement.Name;
                entity.Description = String.IsNullOrEmpty(editElement.Description) ? entity.Description : editElement.Description;

                if (!String.IsNullOrEmpty(editElement.Time))
                {
                    entity.History.Add(await UpdatePropertyElementtable(element.Id, PropertyElementHistory.Time, entity.Time));

                    entity.Time = editElement.Time;
                }
                if (!String.IsNullOrEmpty(editElement.Speaker))
                {
                    entity.History.Add(await UpdatePropertyElementtable(element.Id, PropertyElementHistory.Speaker, entity.Speaker));

                    entity.Speaker = editElement.Speaker;
                }
                if (editElement.IsCanceled.HasValue)
                {
                    entity.History.Add(await UpdatePropertyElementtable(element.Id, PropertyElementHistory.Canseled, entity.IsCanceled));

                    entity.IsCanceled = editElement.IsCanceled.Value;
                }

                result.Add(entity);
            }
            foreach(var newElement in editAudience.Elementtables.Where(x => x.Id == 0))
            {
                var entity = _mapper.Map<Elementtable>(newElement);
                _context.Add(entity);
                result.Add(entity);
            }

            audience.Elements = result;

            await _context.SaveChangesAsync();

            return result;
        }

        private EditElementtable GetElementtable(EditAudienceElement audience, int id) => audience.Elementtables.FirstOrDefault(x => x.Id == id);

        public async Task<HistoryElementtable> UpdatePropertyElementtable(int elementTableId, PropertyElementHistory type, object oldValue)
        {
            var newHistory = new HistoryElementtable() { ElementtableId = elementTableId, Type = type, OldValue = oldValue.ToString() };
            _context.Add(newHistory);
            await _context.SaveChangesAsync();

            return newHistory;
        }

        private TimetableReturn Ok(Timetable timetable) => Ok(timetable.ToDto<TimetableDto>(_mapper).ToReturn<TimetableReturn>());

        private TimetableReturn Error(string errorMessage) => base.Error<TimetableReturn>(errorMessage);

        private TimetableReturn NotFound(string errorMessage) => base.Error<TimetableReturn>(errorMessage, CodeRequest.NotFound);

        public Task<ScheduleReturn> Get(int locationId)
        {
            throw new NotImplementedException();
        }

        private IQueryable<Timetable> Timetables => _context.Timetables.Include(x => x.Audiences).ThenInclude(x => x.Elements).ThenInclude(x => x.History);
    }
}
