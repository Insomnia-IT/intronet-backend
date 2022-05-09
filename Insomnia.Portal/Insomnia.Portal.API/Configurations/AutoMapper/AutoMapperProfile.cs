using AutoMapper;
using AutoMapper.EquivalencyExpression;
using Insomnia.Portal.Data;
using Insomnia.Portal.Data.Entity;
using System;
using System.Linq;
using Insomnia.Portal.Data.Dto;
using Insomnia.Portal.Data.ViewModels.Input;
using Insomnia.Portal.Data.Enums;

namespace Insomnia.Portal.API.Configurations.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateLocation, Location>()
                .ForMember(x => x.Tags, s => s.MapFrom<FormatterTagToCreateLocation>());

            CreateMap<EditLocation, Location>()
                .ForMember(x => x.Tags, s => s.MapFrom<FormatterTagToEditLocation>());

            CreateMap<LocationDto, Location>()
                .ReverseMap();

            CreateMap<CreateTag, Tag>();

            CreateMap<EditTag, Tag>();

            CreateMap<TagDto, Tag>()
                .ReverseMap();

            CreateMap<CreateNote, Note>()
                .ForMember(x => x.Category, s => s.MapFrom<FormatterCategoryToCreateNote>());

            CreateMap<EditNote, Note>()
                .ForMember(x => x.Category, s => s.MapFrom<FormatterCategoryToEditNote>());

            CreateMap<NoteDto, Note>()
                .ReverseMap();

            CreateMap<CreateNoteCategory, NoteCategory>();

            CreateMap<EditNoteCategory, NoteCategory>();

            CreateMap<NoteCategoryDto, NoteCategory>();

            CreateMap<NoteCategory, NoteCategoryDto>()
                .ForMember(x => x.Count, s => s.MapFrom(x => x.Notes == null ? 0 : x.Notes.Count));
        }
    }
}
