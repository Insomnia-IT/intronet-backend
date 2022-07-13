using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insomnia.Portal.BI.Interfaces;
using Insomnia.Portal.Data.Return;
using Insomnia.Portal.Data.Dto;
using Insomnia.Portal.EF;
using Microsoft.EntityFrameworkCore;
using Insomnia.Portal.Data.Filters;
using Insomnia.Portal.General.Expansions;
using Microsoft.EntityFrameworkCore.Metadata;
using AutoMapper;
using Insomnia.Portal.Data.ViewModels.Input;

namespace Insomnia.Portal.BI.Services
{
    public class Location : Base<Data.Entity.Location, LocationDto>, ILocation, IAdminLocation
    {
        public Location(ServiceDbContext context, IMapper mapper) : base(mapper, context)
        {
        }

        private async Task<Data.Entity.Location> GetEntityAsync(int locationId)
        {
            return await Locations.SingleOrDefaultAsync(x => x.Id == locationId);
        }

        public async Task<LocationReturn> Add(CreateLocation location)
        {
            try
            {
                var entity = GetEntity(location);
                if (entity == null)
                    return Error("Не удалось создать локацию!");

                await _context.AddAsync(entity);
                await _context.SaveChangesAsync();

                return Ok(await Locations.OrderByDescending(x => x.Id).FirstOrDefaultAsync());
            }
            catch (Exception ex)
            {
                //добавить логгер
                return Error(ex.Message);
            }
        }

        public async Task<LocationReturn> Edit(EditLocation location)
        {
            try
            {
                var entity = await GetEntityAsync(location.Id);
                if (entity == null)
                    return NotFound("Локация с указанным ID не найдена!");

                entity = _mapper.Map(location, entity);

                _context.Update(entity);
                await _context.SaveChangesAsync();
                return Ok<LocationReturn>();
            }
            catch (Exception ex)
            {
                //добавить логгер
                return Error(ex.Message);
            }
        }

        public async Task<LocationReturn> Delete(int id)
        {
            try
            {
                var entity = await GetEntityAsync(id);

                if (entity is null)
                    return NotFound("Локация не найдена!");

                _context.Remove(entity);
                await _context.SaveChangesAsync();

                return Ok<LocationReturn>();
            }
            catch (Exception ex)
            {
                //Добавить логгер
                return Error(ex.Message);
            }
        }

        public async Task<LocationReturn> Get(int id)
        {
            var location = await Locations.FirstOrDefaultAsync(x => x.Id == id);

            if (location is null)
                return NotFound("Локация не найдена!");

            return Ok(location);
        }

        public async Task<LocationReturn> GetFull(int id)
        {
            var location = await LocationsFull.FirstOrDefaultAsync(x => x.Id == id);

            if (location is null)
                return NotFound("Локация не найдена!");

            return Ok(location);
        }

        public async Task<LocationsReturn> GetAll()
        {
            var locations = await Locations.ToListAsync();

            if (locations.IsEmptyOrNull())
                return NotFoundArray("Список локаций пуст!");

            return Ok(locations);
        }

        public async Task<LocationsReturn> GetAllFull()
        {
            var locations = await LocationsFull.ToListAsync();

            if (locations.IsEmptyOrNull())
                return NotFoundArray("Список локаций пуст!");

            return Ok(locations);
        }

        public async Task<LocationsReturn> GetAllWithFilter(LocationsFilter filter)
        {
            var locations = await LocationsFull.AsQueryable().FilterToList(filter);

            if (locations.IsEmptyOrNull())
                return NotFoundArray("Список локаций пуст!");

            return Ok(locations);
        }

        public async Task<LocationsReturn> GetAllFullWithFilter(LocationsFilter filter)
        {
            var locations = await LocationsFull.AsQueryable().FilterToList(filter);

            if (locations.IsEmptyOrNull())
                return NotFoundArray("Список локаций пуст!");

            return Ok(locations);
        }

        private LocationReturn Ok(Data.Entity.Location location) => Ok(location.ToDto<LocationDto>(_mapper).ToReturn<LocationReturn>());

        private LocationReturn Error(string errorMessage) => base.Error<LocationReturn>(errorMessage);

        private LocationsReturn Ok(IList<Data.Entity.Location> location) => Ok(location.ToDto<IList<LocationDto>>(_mapper).ToReturn<LocationsReturn>());

        private LocationsReturn Errors(string errorMessage) => base.Error<LocationsReturn>(errorMessage);

        private LocationReturn NotFound(string errorMessage) => base.Error<LocationReturn>(errorMessage, Data.Enums.CodeRequest.NotFound);

        private LocationsReturn NotFoundArray(string errorMessage) => base.Error<LocationsReturn>(errorMessage, Data.Enums.CodeRequest.NotFound);

        private IQueryable<Data.Entity.Location> Locations => _context.Locations.Include(x => x.Tags);

        private IQueryable<Data.Entity.Location> LocationsFull => _context.Locations
            .Include(x => x.Tags)
            .Include(x => x.Timetables).ThenInclude(x => x.Audiences).ThenInclude(x => x.Elements).ThenInclude(x => x.History)
            .Include(x => x.Direction);
    }
}
