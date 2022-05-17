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
    public class Notesboard : Base<Note, NoteDto>, INotesboard, IAdminNotesboard
    {
        public Notesboard(IMapper mapper, ServiceDbContext context) : base(mapper, context)
        {
        }

        public async Task<NoteReturn> Add(CreateNote note)
        {
            try
            {
                var entity = GetEntity(note);
                if (entity == null)
                    return Error("Не удалось создать запись!");

                await _context.AddAsync(entity);
                await _context.SaveChangesAsync();

                return Ok<NoteReturn>();
            }
            catch (Exception ex)
            {
                //добавить логгер
                return Error(ex.Message);
            }
        }

        public async Task<NoteReturn> Edit(EditNote note)
        {
            try
            {
                var entity = await GetEntityAsync(note.Id);
                if (entity == null)
                    return NotFound("Запись с указанным ID не найдена!");

                entity = _mapper.Map(note, entity);

                _context.Update(entity);
                await _context.SaveChangesAsync();
                return Ok<NoteReturn>();
            }
            catch (Exception ex)
            {
                //добавить логгер
                return Error(ex.Message);
            }
        }

        public async Task<NoteReturn> Delete(int id)
        {
            try
            {
                var entity = await GetEntityAsync(id);

                if (entity is null)
                    return NotFound("Запись не найдена!");

                _context.Remove(entity);
                await _context.SaveChangesAsync();

                return Ok<NoteReturn>();
            }
            catch (Exception ex)
            {
                //Добавить логгер
                return Error(ex.Message);
            }
        }

        private async Task<Note> GetEntityAsync(int noteId)
        {
            return await Notes.SingleOrDefaultAsync(x => x.Id == noteId);
        }

        public async Task<NoteReturn> Get(int id)
        {
            var note = await Notes.FirstOrDefaultAsync(x => x.Id == id);

            if (note is null)
                return NotFound("Запись не найдена!");

            return Ok(note);
        }

        public async Task<NotesboardReturn> GetAll()
        {
            var notes = await Notes.OrderByDescending(x => x.CreatedDate).ToListAsync();

            if (notes.IsEmptyOrNull())
                return NotFoundArray("Список записей пуст!");

            return Ok(notes);
        }

        public async Task<NotesboardReturn> GetAllWithFilter(NotesFilter filter)
        {
            var notes = await Notes.AsQueryable().FilterToList(filter);

            if (notes.IsEmptyOrNull())
                return NotFoundArray("Список локаций пуст!");

            return Ok(notes);
        }

        private NoteReturn Ok(Note tag) => Ok(tag.ToDto<NoteDto>(_mapper).ToReturn<NoteReturn>());

        private NoteReturn Error(string errorMessage) => base.Error<NoteReturn>(errorMessage);

        private NotesboardReturn Ok(IList<Note> tag) => Ok(tag.ToDto<IList<NoteDto>>(_mapper).ToReturn<NotesboardReturn>());

        private NotesboardReturn Errors(string errorMessage) => base.Error<NotesboardReturn>(errorMessage);

        private NoteReturn NotFound(string errorMessage) => base.Error<NoteReturn>(errorMessage, CodeRequest.NotFound);

        private NotesboardReturn NotFoundArray(string errorMessage) => base.Error<NotesboardReturn>(errorMessage, CodeRequest.NotFound);

        private IQueryable<Note> Notes => _context.Notes.Include(x => x.Category);
    }
}
