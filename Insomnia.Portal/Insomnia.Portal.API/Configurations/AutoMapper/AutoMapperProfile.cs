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
using Insomnia.Portal.General.Expansions;

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
                .ForMember(x => x.Direction, s => s.MapFrom<FormatterDirectionToCreateOrEditLocation>())
                .ForMember(dest => dest.Lat, opt => opt.Condition(src => src.Lat > 0))
                .ForMember(dest => dest.Lon, opt => opt.Condition(src => src.Lon > 0))
                .ForMember(dest => dest.X, opt => opt.Condition(src => src.X > 0))
                .ForMember(dest => dest.Y, opt => opt.Condition(src => src.Y > 0))
                .ForMember(dest => dest.DirectionId, opt => opt.Condition(src => src.DirectionId > 0));

            CreateMap<LocationDto, Location>()
                .ReverseMap();

            CreateMap<CreateTag, Tag>();

            CreateMap<EditTag, Tag>()
                .ForMember(x => x.Name, y => y.Condition(src => !String.IsNullOrEmpty(src.Name) && src.Name != "string"));

            CreateMap<TagDto, Tag>()
                .ReverseMap();

            CreateMap<CreateNote, Note>()
                .ForMember(x => x.Category, s => s.MapFrom<FormatterCategoryToCreateOrEditNote>());

            CreateMap<EditNote, Note>()
                .ForMember(x => x.Category, s => s.MapFrom<FormatterCategoryToCreateOrEditNote>())
                .ForMember(x => x.Title, y => y.Condition(src => !String.IsNullOrEmpty(src.Title) && src.Title != "string"))
                .ForMember(x => x.Text, y => y.Condition(src => !String.IsNullOrEmpty(src.Text) && src.Text != "string"));

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
