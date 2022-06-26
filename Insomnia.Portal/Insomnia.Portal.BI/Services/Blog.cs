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

        public async Task<PageReturn> Get(int id)
        {
            var page = await Pages.FirstOrDefaultAsync(x => x.Id == id);

            if (page is null)
                return NotFound("Страница не найдена!");

            return Ok(page);
        }

        public async Task<PagesReturn> GetAll()
        {
            var pages = await Pages.OrderByDescending(x => x.Id).ToListAsync();

            if (pages.IsEmptyOrNull())
                return NotFoundArray("Список страниц пуст!");

            return Ok(pages);
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

        private PageReturn Ok() => Ok<PageReturn>();

        private PageReturn Ok(Page page) => Ok(page.ToDto<PageDto>(_mapper).ToReturn<PageReturn>());

        private PagesReturn Ok(IList<Page> pages) => Ok(pages.ToDto<IList<PageDto>>(_mapper).ToReturn<PagesReturn>());

        private PageReturn Error(string errorMessage) => base.Error<PageReturn>(errorMessage);

        private PageReturn NotFound(string errorMessage) => base.Error<PageReturn>(errorMessage, CodeRequest.NotFound);

        private PagesReturn NotFoundArray(string errorMessage) => base.Error<PagesReturn>(errorMessage, CodeRequest.NotFound);

        private IQueryable<Page> Pages => _context.Pages;

    }
}
