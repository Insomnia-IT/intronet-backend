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
    public class FormatterTagToCreateOrEditLocation : IValueResolver<CreateLocation, Location, IList<Tag>>
    {
        private readonly IEntityTag _tag;

        public FormatterTagToCreateOrEditLocation(IEntityTag tag)
        {
            _tag = tag;
        }

        public IList<Tag> Resolve(CreateLocation source, Location location, IList<Tag> result, ResolutionContext context)
        {
            if (source.Tags.IsEmptyOrNull())
                return location.Tags ?? new List<Tag>();

            var tags = _tag.GetEntitiesOrCreating(source.Tags.ToArray());

            return tags;
        }
    }

    public class FormatterCategoryToCreateOrEditNote : IValueResolver<CreateNote, Note, NoteCategory>
    {
        private readonly IEntityNotesCategories _categories;

        public FormatterCategoryToCreateOrEditNote(IEntityNotesCategories categories)
        {
            _categories = categories;
        }

        public NoteCategory Resolve(CreateNote source, Note note, NoteCategory result, ResolutionContext context)
        {
            if (source.CategoryId <= StaticValues.DefaultIdNoteCategories)
                source.CategoryId = note.CategoryId >= StaticValues.DefaultIdNoteCategories ? note.CategoryId : StaticValues.DefaultIdNoteCategories;

            var category = _categories.GetEntityOrCreating(source.CategoryId);

            return category;
        }
    }

    public class FormatterDirectionToCreateOrEditLocation : IValueResolver<CreateLocation, Location, Direction>
    {
        private readonly IEntityDirection _direction;

        public FormatterDirectionToCreateOrEditLocation(IEntityDirection direction)
        {
            _direction = direction;
        }

        public Direction Resolve(CreateLocation source, Location location, Direction result, ResolutionContext context)
        {
            if (source.DirectionId <= 0)
                source.DirectionId = location.DirectionId;

            var direction = _direction.GetEntityOrCreating(source.DirectionId);

            return direction;
        }
    }
}