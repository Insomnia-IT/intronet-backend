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

        public async Task<ScheduleReturn> Get(int locationId)
        {
            var schedule = await Timetables.Where(x => x.LocationId == locationId).ToListAsync();

            if (schedule is null)
                return NotFoundArray("Расписние не найдено!");

            return Ok(schedule);
        }

        private async Task<Timetable> GetEntityAsync(int timetableId)
        {
            return await Timetables.SingleOrDefaultAsync(x => x.Id == timetableId);
        }

        public async Task<TimetableReturn> Add(CreateTimetable schedule)
        {
            try
            {
                var entity = GetEntity(schedule);
                if (entity == null)
                    return Error("Не удалось создать раписание!");

                await _context.AddAsync(entity);
                await _context.SaveChangesAsync();

                return Ok(await Timetables.OrderByDescending(x => x.Id).FirstOrDefaultAsync());
            }
            catch (Exception ex)
            {
                //добавить логгер
                return Error(ex.Message);
            }
        }

        public async Task<TimetableReturn> AddOrEdit(EditTimetable schedule)
        {
            try
            {
                var entity = await GetEntityAsync(schedule.Id);

                if(entity == null)
                {
                    var createEntity = new CreateTimetable()
                    {
                        LocationId = schedule.LocationId,
                        Day = schedule.Day.Value,
                        Audiences = schedule.Audiences.Select(x => new CreateAudienceElement()
                        {
                            Number = x.Number,
                            Elements = x.Elements.Select(y => new CreateElementtable()
                            {
                                Description = y.Description,
                                Name = y.Name,
                                Speaker = y.Speaker,
                                Time = y.Time
                            }).ToList(),
                        }).ToList()
                    };

                    return await Add(createEntity);
                }

                entity.Day = schedule.Day.HasValue ? schedule.Day.Value : entity.Day;

                _context.Update(entity);
                await _context.SaveChangesAsync();

                var timetableId = entity.Id > 0 ? entity.Id : Timetables.OrderByDescending(x => x.Id).First().Id;
                        
                await AddOrEditAudiencies(schedule.Audiences, entity.Audiences, timetableId);

                return Ok(await Timetables.OrderByDescending(x => x.Id).FirstOrDefaultAsync());
            }
            catch(Exception ex)
            {
                return Error(ex.Message);
            }
        }

        private async Task<List<EditAudienceElement>> RepareAudience(List<EditAudienceElement> audiences)
        {
            var result = new List<EditAudienceElement>();

            foreach(var a in audiences.GroupBy(x => x.Number))
            {
                var audience = a.First();
                audience.Elements = a.SelectMany(x => x.Elements).ToList();

                result.Add(audience);
            }

            return result;
        }

        private async Task AddOrEditAudiencies(List<EditAudienceElement> audiences, List<AudienceElement> entityAudiences, int timetableId)
        {

            audiences = await RepareAudience(audiences);

            var entityAudiencesIds = entityAudiences.Select(x => x.Id).ToArray();

            var newAudiences = audiences.Where(x => x.IsDeleted != true).Where(x => x.Id == 0).ToList();

            var canseledAudiences = entityAudiences.Where(x => !audiences.Where(x => x.IsDeleted != true).Select(x => x.Id).Contains(x.Id)).ToList();

            var updateAudiences = audiences.Where(x => x.IsDeleted != true).Where(x => entityAudiencesIds.Contains(x.Id)).ToList();

            var entities = _mapper.Map<List<EditAudienceElement>, List<AudienceElement>>(newAudiences);

            var deletedAudiences = audiences.Where(x => x.IsDeleted == true).ToList();

            foreach(var entity in entities)
            {
                entity.TimetableId = timetableId;
            }

            foreach(var a in canseledAudiences)
            {
                foreach(var e in a.Elements)
                {
                    e.IsCanceled = true;
                }
            }

            _context.UpdateRange(canseledAudiences);
            await _context.SaveChangesAsync();

            var removeElements = entityAudiences.Where(x => deletedAudiences.Select(x => x.Id).Contains(x.Id));

            _context.RemoveRange(removeElements);
            await _context.SaveChangesAsync();

            await AddAudiences(entities);

            foreach(var audience in updateAudiences)
            {
                await UpdateAudience(audience, entityAudiences.SingleOrDefault(x => x.Id == audience.Id), timetableId);
            }
        }

        private async Task UpdateAudience(EditAudienceElement audience, AudienceElement entity, int timetableId)
        {
            if(entity == null)
            {
                await AddAudience(audience, timetableId);
                return;
            }    

            entity.Number = audience.Number > 0 ? audience.Number : entity.Number;

            _context.Update(entity);

            var entityElementsIds = entity.Elements.Select(x => x.Id).ToArray();

            var newElements = audience.Elements.Where(x => x.IsDeleted != true).Where(x => x.Id == 0).ToList();

            var canseledElements = audience.Elements.Where(x => x.IsDeleted != true).Where(x => !entityElementsIds.Contains(x.Id)).Select(async x =>
            {
                var e = _mapper.Map<Elementtable>(x);
                e.IsCanceled = true;
                e.AudienceId = entity.Id;
                e.History.Add(await UpdatePropertyElementtable(e.Id, PropertyElementHistory.Canseled, false));
                return e;
            }).ToList();

            var updateElements = audience.Elements.Where(x => x.IsDeleted != true).Where(x => entityElementsIds.Contains(x.Id)).ToList();

            var deletedElements = audience.Elements.Where(x => x.IsDeleted == true).ToList();

            var entities = _mapper.Map<List<EditElementtable>, List<Elementtable>>(newElements);
            foreach (var e in entities)
            {
                e.AudienceId = entity.Id;
            }

            await _context.AddRangeAsync(entities);
            _context.UpdateRange(canseledElements);
            await _context.SaveChangesAsync();

            _context.RemoveRange(entity.Elements.Where(x => deletedElements.Select(x => x.Id).Contains(x.Id)));
            await _context.SaveChangesAsync();

            await AddElementtables(newElements, entity.Id);

            var elements = new List<Elementtable>();

            foreach(var element in updateElements)
            {
                var f = await EditElement(element, entity.Elements.SingleOrDefault(x => x.Id == element.Id));

                elements.Add(f);
            }

            entity.Elements = elements;
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }

        private async Task AddAudiences(List<AudienceElement> audiences)
        {
            await _context.AddRangeAsync(audiences);
            await _context.SaveChangesAsync();
        }

        private async Task AddAudience(EditAudienceElement audience, int id)
        {
            var entity = _mapper.Map<AudienceElement>(audience);
            entity.TimetableId = id;
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        private async Task AddElementtables(List<EditElementtable> elementtables, int audienceId)
        {
            var entities = elementtables.Select(x => new Elementtable()
            {
                AudienceId = audienceId,
                Description = x.Description,
                Name = x.Name,
                Speaker = x.Speaker,
                IsCanceled = false,
                Time = x.Time,
            });

            await _context.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        private Elementtable AddElement(EditElementtable element)
        {
            return _mapper.Map<Elementtable>(element);
        }

        private async Task<Elementtable> EditElement(EditElementtable element, Elementtable entity)
        {
            if (entity == null)
                return AddElement(element);

            entity.Name = String.IsNullOrEmpty(element.Name) ? entity.Name : element.Name;
            entity.Description = String.IsNullOrEmpty(element.Description) ? entity.Description : element.Description;

            if (!String.IsNullOrEmpty(element.Time))
            {
                entity.History.Add(await UpdatePropertyElementtable(element.Id, PropertyElementHistory.Time, entity.Time));

                entity.Time = element.Time;
            }
            if (!String.IsNullOrEmpty(element.Speaker))
            {
                entity.History.Add(await UpdatePropertyElementtable(element.Id, PropertyElementHistory.Speaker, entity.Speaker));

                entity.Speaker = element.Speaker;
            }
            if (element.IsCanceled.HasValue)
            {
                entity.History.Add(await UpdatePropertyElementtable(element.Id, PropertyElementHistory.Canseled, entity.IsCanceled));

                entity.IsCanceled = element.IsCanceled.Value;
            }

            return entity;
        }

        public async Task<HistoryElementtable> UpdatePropertyElementtable(int elementTableId, PropertyElementHistory type, object oldValue)
        {
            var newHistory = new HistoryElementtable() { ElementtableId = elementTableId, Type = type, OldValue = oldValue.ToString() };

            return newHistory;
        }

        public Task<TimetableReturn> Delete(int id)
        {
            throw new NotImplementedException();
        }

        private ScheduleReturn Ok(IList<Timetable> timetables) => Ok(timetables.ToDto<IList<TimetableDto>>(_mapper).ToReturn<ScheduleReturn>());

        private TimetableReturn Ok(Timetable timetable) => Ok(timetable.ToDto<TimetableDto>(_mapper).ToReturn<TimetableReturn>());

        private TimetableReturn Error(string errorMessage) => base.Error<TimetableReturn>(errorMessage);

        private TimetableReturn NotFound(string errorMessage) => base.Error<TimetableReturn>(errorMessage, CodeRequest.NotFound);

        private ScheduleReturn NotFoundArray(string errorMessage) => base.Error<ScheduleReturn>(errorMessage, CodeRequest.NotFound);

        private IQueryable<Timetable> Timetables => _context.Timetables.Include(x => x.Audiences).ThenInclude(x => x.Elements).ThenInclude(x => x.History);
    }
}
