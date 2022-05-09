﻿using AutoMapper;
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
    public class FormatterTagToCreateOrEditLocation : IValueResolver<CreateLocation, Location, ICollection<Tag>>
    {
        private readonly IEntityTag _tag;

        public FormatterTagToCreateOrEditLocation(IEntityTag tag)
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

    public class FormatterCategoryToCreateOrEditNote : IValueResolver<CreateNote, Note, NoteCategory>
    {
        private readonly IEntityNotesCategories _categories;

        public FormatterCategoryToCreateOrEditNote(IEntityNotesCategories categories)
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

    public class FormatterDirectionToCreateOrEditLocation : IValueResolver<CreateLocation, Location, Direction>
    {
        private readonly IEntityDirection _direction;

        public FormatterDirectionToCreateOrEditLocation(IEntityDirection direction)
        {
            _direction = direction;
        }

        public Direction Resolve(CreateLocation source, Location location, Direction result, ResolutionContext context)
        {
            var direction = _direction.GetEntityOrCreating(source.DirectionId);

            return direction;
        }
    }

    public class FormatterDirectionToCreateOrEditDirection : IValueResolver<CreateDirection, Direction, string>
    {
        private readonly IAttachment _attachment;

        public FormatterDirectionToCreateOrEditDirection(IAttachment attachment)
        {
            _attachment = attachment;
        }

        public string Resolve(CreateDirection source, Direction destination, string result, ResolutionContext context)
        {
            if(!String.IsNullOrEmpty(source.Image) && source.Image.IsUrl())
                return source.Image;

            if (source.File is not null && source.File.IsImage())
                return Task.Run(async () => await _attachment.Upload(new CreateAttachment()
                {
                    File = source.File,
                })).Result;

            return StaticValues.DefaultImageForLocationDirection;
        }
    }
}