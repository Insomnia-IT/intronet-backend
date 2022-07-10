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
using Insomnia.Portal.Data.Generic;

namespace Insomnia.Portal.BI.Services
{
    public class LocationDirection : Base<Direction, DirectionDto>, IDirection, IEntityDirection, IAdminDirection
    {
        public LocationDirection(ServiceDbContext context, IMapper mapper) : base(mapper, context)
        {
        }

        public IList<Direction> GetEntities(int[] directionsIds)
        {
            return Directions.Where(x => directionsIds.Contains(x.Id)).ToListOrNull();
        }

        public Direction GetEntity(int directionId)
        {
            return Directions.SingleOrDefault(x => x.Id == directionId);
        }

        private async Task<Direction> GetEntityAsync(int directionId)
        {
            return await Directions.SingleOrDefaultAsync(x => x.Id == directionId);
        }

        private Direction GetLastEntity()
        {
            return Directions.OrderByDescending(x => x.Id).FirstOrDefault();
        }

        private IList<Direction> GetLastEntities(int count)
        {
            return Directions.OrderByDescending(x => x.Id).Take(count).ToListOrNull();
        }

        public Direction Create(int directionId)
        {
            var entity = GetDirectionEntityModel(directionId);

            _context.Add(entity);
            _context.SaveChanges();

            return GetLastEntity();
        }

        public IList<Direction> Create(int[] directionsIds)
        {
            var entities = GetEntities(directionsIds);
            var newDirections = entities is null ? directionsIds : directionsIds.Except(entities.Select(x => x.Id).ToArray());

            if (newDirections.Any())
            {
                _context.Directions.AddRange(newDirections.Select(x => GetDirectionEntityModel(x)));
                _context.SaveChanges();
            }
            else
                return entities;

            return GetLastEntities(directionsIds.Length);
        }

        public async Task<DirectionReturn> Get(int id)
        {
            var direction = await Directions.FirstOrDefaultAsync(x => x.Id == id);

            if (direction is null)
                return NotFound("Направление не найдено!");

            return Ok(direction);
        }

        public async Task<DirectionsReturn> GetAll()
        {
            var directions = await Directions.ToListAsync();

            if (directions.IsEmptyOrNull())
                return NotFoundArray("Список направлений пуст!");

            return Ok(directions);
        }

        public Direction GetEntityOrCreating(int directionId)
        {
            return GetEntity(directionId) ?? Create(directionId);
        }

        public IList<Direction> GetEntitiesOrCreating(int[] directionsIds)
        {
            var entities = GetEntities(directionsIds);

            if (entities?.Count() != directionsIds.Length)
                return Create(directionsIds);

            return entities;
        }

        private Direction GetDirectionEntityModel(int directionId) => directionId > 1 && GetLastEntity().Id > directionId ?
            new Direction()
            {
                Id = directionId,
                Name = directionId.ToString(),
            } :
            new Direction()
            {
                Name = directionId.ToString(),
            };

        public async Task<DirectionReturn> Add(CreateDirection direction)
        {
            try
            {
                var entity = GetEntity(direction);
                if (entity == null)
                    return Error("Не удалось создать направление!");

                await _context.AddAsync(entity);
                await _context.SaveChangesAsync();

                return Ok<DirectionReturn>();
            }
            catch (Exception ex)
            {
                //добавить логгер
                return Error(ex.Message);
            }
        }

        public async Task<DirectionReturn> Edit(EditDirection direction)
        {
            try
            {
                var entity = await GetEntityAsync(direction.Id);
                if (entity == null)
                    return NotFound("Направление с указанным ID не найден!");

                entity.Name = direction.Name;

                _context.Update(entity);
                await _context.SaveChangesAsync();
                return Ok<DirectionReturn>();
            }
            catch (Exception ex)
            {
                //добавить логгер
                return Error(ex.Message);
            }
        }

        public async Task<DirectionReturn> Delete(int id)
        {
            if (id == StaticValues.DefaultIdForLocationDirection)
                return Error("Нельзя удалить данное направление!");

            try
            {
                var entity = await GetEntityAsync(id);

                if (entity is null)
                    return NotFound("Направление не найдено!");

                _context.Remove(entity);
                await _context.SaveChangesAsync();

                return Ok<DirectionReturn>();
            }
            catch (Exception ex)
            {
                //Добавить логгер
                return Error(ex.Message);
            }
        }

        private DirectionReturn Ok(Direction direction) => Ok(direction.ToDto<DirectionDto>(_mapper).ToReturn<DirectionReturn>());

        private DirectionReturn Error(string errorMessage) => base.Error<DirectionReturn>(errorMessage);

        private DirectionsReturn Ok(IList<Direction> directions) => Ok(directions.ToDto<IList<DirectionDto>>(_mapper).ToReturn<DirectionsReturn>());

        private DirectionsReturn Errors(string errorMessage) => base.Error<DirectionsReturn>(errorMessage);

        private DirectionReturn NotFound(string errorMessage) => base.Error<DirectionReturn>(errorMessage, CodeRequest.NotFound);

        private DirectionsReturn NotFoundArray(string errorMessage) => base.Error<DirectionsReturn>(errorMessage, CodeRequest.NotFound);

        private IQueryable<Direction> Directions => _context.Directions;
    }
}
