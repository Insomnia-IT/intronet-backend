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
                .ForMember(x => x.Tags, opt => opt.MapFrom<FormatterTagToCreateOrEditLocation>())
                .ForMember(x => x.Direction, opt => opt.MapFrom<FormatterDirectionToCreateOrEditLocation>())
                .ForMember(x => x.Lat, opt => opt.Condition(src => src.Lat > 0))
                .ForMember(x => x.Lon, opt => opt.Condition(src => src.Lon > 0))
                .ForMember(x => x.X, opt => opt.Condition(src => src.X > 0))
                .ForMember(x => x.Y, opt => opt.Condition(src => src.Y > 0))
                .ForMember(x => x.DirectionId, opt => opt.Condition(src => src.DirectionId > 0))
                .ForMember(x => x.Name, opt => opt.Condition(src => !String.IsNullOrEmpty(src.Name) && src.Name != "string"))
                .ForMember(x => x.Description, opt => opt.Condition(src => !String.IsNullOrEmpty(src.Description) && src.Description != "string"));

            CreateMap<EditElementtable, Elementtable>()
                .ForMember(x => x.Time, s => s.Ignore())
                .ForMember(x => x.Name, s => s.Ignore())
                .ForMember(x => x.Speaker, s => s.Ignore())
                .ForMember(x => x.Description, s => s.Condition(src => !String.IsNullOrEmpty(src.Description) && src.Description != "string"))
                .ForMember(x => x.Audience, s => s.Ignore())
                .ForMember(x => x.AudienceId, s => s.Ignore());

            CreateMap<EditTimetable, Timetable>()
                .ForMember(x => x.Audiences, s => s.Ignore())
                .ForMember(x => x.Day, s => s.Condition(src => src.Day.HasValue))
                .ForMember(x => x.Name, s => s.Condition(src => !String.IsNullOrEmpty(src.Name) && src.Name != "string"))
                .ForMember(x => x.LocationId, s => s.Ignore());

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
                .ForMember(x => x.Title, s => s.Condition(src => !String.IsNullOrEmpty(src.Title) && src.Title != "string"))
                .ForMember(x => x.Text, s => s.Condition(src => !String.IsNullOrEmpty(src.Text) && src.Text != "string"));

            CreateMap<NoteDto, Note>()
                .ReverseMap();

            CreateMap<CreateNoteCategory, NoteCategory>();

            CreateMap<EditNoteCategory, NoteCategory>()
                .ForMember(x => x.Color, s => s.MapFrom(x => !String.IsNullOrEmpty(x.Color) && x.Color != "string"));

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

            CreateMap<CreateTimetable, Timetable>();

            CreateMap<CreateAudienceElement, AudienceElement>();

            CreateMap<CreateElementtable, Elementtable>();

            CreateMap<EditAudienceElement, AudienceElement>();

            CreateMap<CreatePage, Page>();

            CreateMap<EditPage, Page>();
        }
    }
}
