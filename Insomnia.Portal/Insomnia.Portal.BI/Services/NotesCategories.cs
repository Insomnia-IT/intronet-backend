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
    public class NotesCategories : Base<NoteCategory, NoteCategoryDto>, INotesCategories, IEntityNotesCategories, IAdminNotesCategories
    {
        public NotesCategories(IMapper mapper, ServiceDbContext context) : base(mapper, context)
        {
        }

        public IList<NoteCategory> GetEntities(int[] categories)
        {
            return Categories.Where(x => categories.Contains(x.Id)).ToListOrNull();
        }

        public NoteCategory GetEntity(int categoryId)
        {
            return Categories.SingleOrDefault(x => x.Id == categoryId);
        }

        public async Task<NoteCategory> GetEntityAsync(int categoryId)
        {
            return await CategoriesFull.SingleOrDefaultAsync(x => x.Id == categoryId);
        }

        private NoteCategory GetLastEntity(int id = 0)
        {
            return id == 0 ? Categories.OrderByDescending(x => x.Id).FirstOrDefault() : GetEntity(id);
        }

        private async Task<NoteCategory> GetLastEntityAsync()
        {
            return await Categories.OrderByDescending(x => x.Id).FirstOrDefaultAsync();
        }

        private IList<NoteCategory> GetLastEntities(int count)
        {
            return Categories.OrderByDescending(x => x.Id).Take(count).ToListOrNull();
        }

        public NoteCategory Create(int categoryId)
        {
            var entity = GetNoteCategoryEntityModel(categoryId);

            _context.Add(entity);
            _context.SaveChanges();

            return GetLastEntity();
        }

        public IList<NoteCategory> Create(int[] categories)
        {
            var entities = GetEntities(categories);
            var newCategories = entities is null ? categories : categories.Except(entities.Select(x => x.Id).ToArray());

            if (newCategories.Any())
            {
                _context.NoteCategories.AddRange(newCategories.Select(x => GetNoteCategoryEntityModel(x)));
                _context.SaveChanges();
            }
            else
                return entities;

            return GetLastEntities(categories.Length);
        }

        public async Task<NoteCategoryReturn> Get(int id)
        {
            var category = await CategoriesFull.FirstOrDefaultAsync(x => x.Id == id);

            if (category is null)
                return NotFound("Категория не найдена!");

            return Ok(category);
        }

        public async Task<NoteCategoriesReturn> GetAll()
        {
            var categories = await CategoriesFull.OrderBy(x => x.Id).ToListAsync();

            if (categories.IsEmptyOrNull())
                return NotFoundArray("Список категорий пуст!");

            return Ok(categories);
        }

        public NoteCategory GetEntityOrCreating(int categoryId)
        {
            return GetEntity(categoryId) ?? Create(categoryId);
        }

        public IList<NoteCategory> GetEntitiesOrCreating(int[] categories)
        {
            var entities = GetEntities(categories);

            if (entities?.Count() != categories.Length)
                return Create(categories);

            return entities;
        }

        private NoteCategory GetNoteCategoryEntityModel(int categoryId) => categoryId > 1 && GetLastEntity().Id > categoryId ?
            new NoteCategory()
            {
                Id = categoryId,
                Name = categoryId.ToString(),
            } :
            new NoteCategory()
            {
                Name = categoryId.ToString(),
            };

        public async Task<NoteCategoryReturn> Add(CreateNoteCategory category)
        {
            try
            {
                var entity = GetEntity(category);
                if (entity == null)
                    return Error("Не удалось создать категорию для доски объявлений!");

                await _context.AddAsync(entity);
                await _context.SaveChangesAsync();

                return Ok<NoteCategoryReturn>();
            }
            catch (Exception ex)
            {
                //добавить логгер
                return Error(ex.Message);
            }
        }

        public async Task<NoteCategoryReturn> Edit(EditNoteCategory category)
        {
            try
            {
                var entity = await Categories.FirstOrDefaultAsync(x => x.Id == category.Id);
                if (entity == null)
                    return NotFound("Категория с указанным ID не найден!");

                entity.Name = category.Name;

                _context.Update(entity);
                await _context.SaveChangesAsync();
                return Ok<NoteCategoryReturn>();
            }
            catch (Exception ex)
            {
                //добавить логгер
                return Error(ex.Message);
            }
        }

        public async Task<NoteCategoryReturn> Delete(int id)
        {
            if (id == StaticValues.DefaultIdNoteCategories)
                return Error("Нельзя удалить данную категорию!");

            try
            {
                var entity = await GetEntityAsync(id);

                if (entity is null)
                    return NotFound("Категория не найден!");

                foreach(var note in entity.Notes)
                    note.CategoryId = StaticValues.DefaultIdNoteCategories;

                _context.Remove(entity);
                await _context.SaveChangesAsync();

                return Ok<NoteCategoryReturn>();
            }
            catch (Exception ex)
            {
                //Добавить логгер
                return Error(ex.Message);
            }
        }

        private NoteCategoryReturn Ok(NoteCategory category) => Ok(category.ToDto<NoteCategoryDto>(_mapper).ToReturn<NoteCategoryReturn>());

        private NoteCategoryReturn Error(string errorMessage) => base.Error<NoteCategoryReturn>(errorMessage);

        private NoteCategoriesReturn Ok(IList<NoteCategory> categories)
        {
            var list = categories.ToDto<IList<NoteCategoryDto>>(_mapper);

            list[0].Count = list.Sum(x => x.Count);

            return Ok(list.ToReturn<NoteCategoriesReturn>());
        }

        private NoteCategoriesReturn Errors(string errorMessage) => base.Error<NoteCategoriesReturn>(errorMessage);

        private NoteCategoryReturn NotFound(string errorMessage) => base.Error<NoteCategoryReturn>(errorMessage, CodeRequest.NotFound);

        private NoteCategoriesReturn NotFoundArray(string errorMessage) => base.Error<NoteCategoriesReturn>(errorMessage, CodeRequest.NotFound);

        private IQueryable<NoteCategory> Categories => _context.NoteCategories;

        private IQueryable<NoteCategory> CategoriesFull => _context.NoteCategories.Include(x => x.Notes);
    }
}
