using AutoMapper;
using AutoMapper.EquivalencyExpression;
using Insomnia.Portal.Data;
using Insomnia.Portal.Data.Entity;
using System;
using System.IO;
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
                .ForMember(x => x.Tags, s => s.MapFrom<FormatterTagToCreateOrEditLocation>())
                .ForMember(x => x.Direction, s => s.MapFrom<FormatterDirectionToCreateOrEditLocation>());

            CreateMap<EditLocation, Location>()
                .ForMember(x => x.Tags, s => s.MapFrom<FormatterTagToCreateOrEditLocation>())
                .ForMember(x => x.Direction, s => s.MapFrom<FormatterDirectionToCreateOrEditLocation>());

            CreateMap<LocationDto, Location>()
                .ReverseMap();

            CreateMap<CreateTag, Tag>();

            CreateMap<EditTag, Tag>();

            CreateMap<TagDto, Tag>()
                .ReverseMap();

            CreateMap<CreateNote, Note>()
                .ForMember(x => x.Category, s => s.MapFrom<FormatterCategoryToCreateOrEditNote>());

            CreateMap<EditNote, Note>()
                .ForMember(x => x.Category, s => s.MapFrom<FormatterCategoryToCreateOrEditNote>());

            CreateMap<NoteDto, Note>()
                .ReverseMap();

            CreateMap<CreateNoteCategory, NoteCategory>();

            CreateMap<EditNoteCategory, NoteCategory>();

            CreateMap<NoteCategoryDto, NoteCategory>();

            CreateMap<NoteCategory, NoteCategoryDto>()
                .ForMember(x => x.Count, s => s.MapFrom(x => x.Notes == null ? 0 : x.Notes.Count));

            CreateMap<CreateDirection, Direction>()
                .ForMember(x => x.Image, s => s.MapFrom<FormatterDirectionToCreateOrEditDirection>());

            CreateMap<EditDirection, Direction>()
                .ForMember(x => x.Image, s => s.MapFrom<FormatterDirectionToCreateOrEditDirection>());

            CreateMap<DirectionDto, Direction>()
                .ReverseMap();

            CreateMap<CreateAttachment, Attachment>()
                .ForMember(x => x.FileName, s => s.MapFrom(y => y.File.FileName))
                .ForMember(x => x.Size, s => s.MapFrom(y => y.File.Length))
                .ForMember(x => x.TempName, s => s.MapFrom(y => Path.GetRandomFileName()));

            CreateMap<AttachmentDto, Attachment>()
                .ReverseMap();
        }
    }
}
