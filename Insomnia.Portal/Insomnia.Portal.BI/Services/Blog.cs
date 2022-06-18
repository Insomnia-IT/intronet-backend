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
    public class Blog : Base<Page, PageDto>, IAdminBlog, IBlog
    {
        public Blog(IMapper mapper, ServiceDbContext context) : base(mapper, context)
        {
        }

        public async Task<PageReturn> Add(CreatePage page)
        {
            try
            {
                var entity = GetEntity(page);
                if (entity == null)
                    return Error("Не удалось создать страницу!");

                await _context.AddAsync(entity);
                await _context.SaveChangesAsync();

                return Ok<PageReturn>();
            }
            catch (Exception ex)
            {
                //добавить логгер
                return Error(ex.Message);
            }
        }

        public async Task<PageReturn> Edit(EditPage page)
        {
            try
            {
                var entity = await GetEntityAsync(page.Id);

                if (entity is null)
                    return NotFound("Страница с указанным ID не найдена!");

                entity.Text = String.IsNullOrEmpty(page.Text) ? entity.Text : page.Text;
                entity.Title = String.IsNullOrEmpty(page.Title) ? entity.Title : page.Title;

                _context.Update(page);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }

        public async Task<PageReturn> Delete(int pageId)
        {
            try
            {
                var entity = await GetEntityAsync(pageId);

                if (entity is null)
                    return NotFound("Локация с указанным ID не найдена!");

                _context.Remove(entity);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }

        private async Task<Page> GetEntityAsync(int pageId)
        {
            return await Pages.SingleOrDefaultAsync(x => x.Id == pageId);
        }

        private PageReturn Ok() => base.Ok<PageReturn>();

        private PageReturn Error(string errorMessage) => base.Error<PageReturn>(errorMessage);

        private PageReturn NotFound(string errorMessage) => base.Error<PageReturn>(errorMessage, CodeRequest.NotFound);

        public Task<PageReturn> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PagesReturn> GetAll()
        {
            throw new NotImplementedException();
        }

        private IQueryable<Page> Pages => _context.Pages;

    }
}
