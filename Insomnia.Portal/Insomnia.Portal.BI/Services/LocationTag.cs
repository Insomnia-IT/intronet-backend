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
    public class LocationTag : Base<Tag, TagDto>, ITag, IEntityTag, IAdminTag
    {
        public LocationTag(ServiceDbContext context, IMapper mapper) : base(mapper, context)
        {
        }

        public IList<Tag> GetEntities(int[] tags)
        {
            return Tags.Where(x => tags.Contains(x.Id)).ToListOrNull();
        }

        public Tag GetEntity(int tag)
        {
            return Tags.SingleOrDefault(x => x.Id == tag);
        }

        private Tag GetLastEntity()
        {
            return Tags.OrderByDescending(x => x.Id).FirstOrDefault();
        }

        private IList<Tag> GetLastEntities(int count)
        {
            return Tags.OrderByDescending(x => x.Id).Take(count).ToListOrNull();
        }

        public Tag Create(int tag)
        {
            var entity = GetTagEntityModel(tag);

            _context.Add(entity);
            _context.SaveChanges();

            return GetLastEntity();
        }

        public IList<Tag> Create(int[] tags)
        {
            var entities = GetEntities(tags);
            var newTags = entities is null ? tags : tags.Except(entities.Select(x => x.Id).ToArray());

            if (newTags.Any())
            {
                _context.Tags.AddRange(newTags.Select(x => GetTagEntityModel(x)));
                _context.SaveChanges();
            }
            else
                return entities;

            return GetLastEntities(tags.Length);
        }

        public async Task<TagReturn> Get(int id)
        {
            var tag = await Tags.FirstOrDefaultAsync(x => x.Id == id);

            if (tag is null)
                return NotFound("Тэг не найден!");

            return Ok(tag);
        }

        public async Task<TagsReturn> GetAll()
        {
            var tags = await Tags.ToListAsync();

            if (tags.IsEmptyOrNull())
                return NotFoundArray("Список тэгов пуст!");

            return Ok(tags);
        }

        public Tag GetEntityOrCreating(int tag)
        {
            return GetEntity(tag) ?? Create(tag);
        }

        public IList<Tag> GetEntitiesOrCreating(int[] tags)
        {
            var entities = GetEntities(tags);

            if (entities?.Count() != tags.Length)
                return Create(tags);

            return entities;
        }

        private Tag GetTagEntityModel(int tag) => tag > 1 && GetLastEntity().Id > tag ?
            new Tag()
            {
                Id = tag,
                Name = tag.ToString(),
            } :
            new Tag()
            {
                Name = tag.ToString(),
            };

        public async Task<TagReturn> Add(CreateTag tag)
        {
            try
            {
                var entity = GetEntity(tag);
                if (entity == null)
                    return Error("Не удалось создать тэг!");

                await _context.AddAsync(entity);
                await _context.SaveChangesAsync();

                return Ok<TagReturn>();
            }
            catch (Exception ex)
            {
                //добавить логгер
                return Error(ex.Message);
            }
        }

        public async Task<TagReturn> Edit(EditTag tag)
        {
            try
            {
                var entity = await Tags.FirstOrDefaultAsync(x => x.Id == tag.Id);
                if (entity == null)
                    return NotFound("Тэг с указанным ID не найден!");

                entity.Name = tag.Name;

                _context.Update(entity);
                await _context.SaveChangesAsync();
                return Ok<TagReturn>();
            }
            catch (Exception ex)
            {
                //добавить логгер
                return Error(ex.Message);
            }
        }

        public async Task<TagReturn> Delete(int id)
        {
            try
            {
                var entity = GetEntity(id);

                if (entity is null)
                    return NotFound("Тэг не найден!");

                _context.Remove(entity);
                await _context.SaveChangesAsync();

                return Ok<TagReturn>();
            }
            catch (Exception ex)
            {
                //Добавить логгер
                return Error(ex.Message);
            }
        }

        private TagReturn Ok(Tag tag) => Ok(tag.ToDto<TagDto>(_mapper).ToReturn<TagReturn>());

        private TagReturn Error(string errorMessage) => base.Error<TagReturn>(errorMessage);

        private TagsReturn Ok(IList<Tag> tag) => Ok(tag.ToDto<IList<TagDto>>(_mapper).ToReturn<TagsReturn>());

        private TagsReturn Errors(string errorMessage) => base.Error<TagsReturn>(errorMessage);

        private TagReturn NotFound(string errorMessage) => base.Error<TagReturn>(errorMessage, CodeRequest.NotFound);

        private TagsReturn NotFoundArray(string errorMessage) => base.Error<TagsReturn>(errorMessage, CodeRequest.NotFound);

        private IQueryable<Tag> Tags => _context.Tags;
    }
}
