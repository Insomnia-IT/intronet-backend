﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Insomnia.Portal.Data.Entity;
using Insomnia.Portal.Data.Filters;

namespace Insomnia.Portal.General.Expansions
{
    public static class FiltersExpansions
    {
        public static async Task<IList<Location>> FilterToList(this IQueryable<Location> locations, LocationsFilter filter)
        {
            if (filter.Tags?.Length > 0)
                locations = locations.Where(x => x.Tags.Any(t => filter.Tags.Contains(t.Id)));
            if (filter.Name is not null)
                locations = locations.Where(x => x.Name == filter.Name);

            return await locations.BaseFilterToListAsync(filter);
        }

        public static async Task<IList<Note>> FilterToList(this IQueryable<Note> notes, NotesFilter filter)
        {
            if(filter.CategoriesIds?.Length > 0)
            {
                if (filter.IsSmartFilter)
                {
                    return (await notes.Where(x => filter.CategoriesIds.Contains(x.CategoryId)).ToListAsync())
                            .GroupBy(x => x.CategoryId)
                            .SelectMany(x => x.BaseFilter(filter)).OrderByDescending(x => x.CreatedDate).ToList();
                }
                else
                {
                    notes = notes.Where(x => filter.CategoriesIds.Contains(x.CategoryId));
                }
            }
            else
            {
                if (filter.IsSmartFilter)
                {
                    return notes.AsEnumerable()
                           .GroupBy(x => x.CategoryId)
                           .SelectMany(x => x.BaseFilter(filter)).OrderByDescending(x => x.CreatedDate)
                           .ToList();
                }
            }

            return await notes.BaseFilterToListAsync(filter);
        }

        private static IQueryable<T> BaseFilter<T>(this IQueryable<T> entity, BaseFilter filter) where T : Base2
        {
            entity = entity.Skip(filter.Page * filter.Count);
            entity = entity.Take(filter.Count);

            return entity.OrderByDescending(x => x.CreatedDate);
        }

        private static IEnumerable<TElement> BaseFilter<Tkey, TElement>(this IGrouping<Tkey, TElement> entity, BaseFilter filter) where TElement : Base2
        {
            var result = entity.Skip(filter.Page * filter.Count);
            result = result.Take(filter.Count);

            return result.OrderByDescending(x => x.CreatedDate);
        }

        private static async Task<IList<T>> BaseFilterToListAsync<T>(this IQueryable<T> entity, BaseFilter filter) where T : Base2
        {
            return await entity.BaseFilter(filter).ToListAsync();
        }
    }
}