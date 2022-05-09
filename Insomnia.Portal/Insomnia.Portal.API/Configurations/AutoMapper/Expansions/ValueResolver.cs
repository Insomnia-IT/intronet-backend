using AutoMapper;
using Insomnia.Portal.BI.Options;
using Insomnia.Portal.General.Expansions;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Insomnia.Portal.Data.Enums;
using Insomnia.Portal.Data.Entity;
using Insomnia.Portal.Data.Dto;
using Insomnia.Portal.BI.Interfaces;
using Insomnia.Portal.Data.ViewModels.Input;
using Insomnia.Portal.Data.Generic;

namespace Insomnia.Portal.API.Configurations.AutoMapper
{
    public class FormatterTagToCreateLocation : IValueResolver<CreateLocation, Location, ICollection<Tag>>
    {
        private readonly IEntityTag _tag;

        public FormatterTagToCreateLocation(IEntityTag tag)
        {
            _tag = tag;
        }

        public ICollection<Tag> Resolve(CreateLocation source, Location location, ICollection<Tag> result, ResolutionContext context)
        {
            if (source.Tags.IsEmptyOrNull())
                return new List<Tag>();

            var tags = _tag.GetEntitiesOrCreating(source.Tags);

            return tags;
        }
    }

    public class FormatterTagToEditLocation : IValueResolver<EditLocation, Location, ICollection<Tag>>
    {
        private readonly IEntityTag _tag;

        public FormatterTagToEditLocation(IEntityTag tag)
        {
            _tag = tag;
        }

        public ICollection<Tag> Resolve(EditLocation source, Location location, ICollection<Tag> result, ResolutionContext context)
        {
            if (source.Tags.IsEmptyOrNull())
                return new List<Tag>();

            var tags = _tag.GetEntitiesOrCreating(source.Tags);

            return tags;
        }
    }

    public class FormatterCategoryToCreateNote : IValueResolver<CreateNote, Note, NoteCategory>
    {
        private readonly IEntityNotesCategories _categories;

        public FormatterCategoryToCreateNote(IEntityNotesCategories categories)
        {
            _categories = categories;
        }

        public NoteCategory Resolve(CreateNote source, Note note, NoteCategory result, ResolutionContext context)
        {
            if (source.CategoryId <= 0)
                source.CategoryId = StaticValues.DefaultIdNoteCategories;

            var category = _categories.GetEntityOrCreating(source.CategoryId);

            return category;
        }
    }

    public class FormatterCategoryToEditNote : IValueResolver<EditNote, Note, NoteCategory>
    {
        private readonly IEntityNotesCategories _categories;

        public FormatterCategoryToEditNote(IEntityNotesCategories categories)
        {
            _categories = categories;
        }

        public NoteCategory Resolve(EditNote source, Note note, NoteCategory result, ResolutionContext context)
        {
            if (source.CategoryId <= 0)
                source.CategoryId = StaticValues.DefaultIdNoteCategories;

            var category = _categories.GetEntityOrCreating(source.CategoryId);

            return category;
        }
    }
}