using Autofac;
using AutoMapper;
using Divergic.Configuration.Autofac;
using Insomnia.Portal.API.Configurations.AutoMapper;
using Insomnia.Portal.BI.Options;
using Insomnia.Portal.BI.Interfaces;
using Insomnia.Portal.BI.Services;
using System;

namespace Insomnia.Portal.API.Configurations.Autofac
{
    public class ServiceModules : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<Location>()
                .As<ILocation>()
                .As<IAdminLocation>();

            builder.RegisterType<Schedule>()
                .As<ISchedule>()
                .As<IAdminSchedule>();

            builder.RegisterType<LocationTag>()
                .As<ITag>()
                .As<IEntityTag>()
                .As<IAdminTag>();

            builder.RegisterType<Notesboard>()
                .As<INotesboard>()
                .As<IAdminNotesboard>();

            builder.RegisterType<NotesCategories>()
                .As<INotesCategories>()
                .As<IEntityNotesCategories>()
                .As<IAdminNotesCategories>();

            builder.RegisterType<LocationDirection>()
                .As<IDirection>()
                .As<IEntityDirection>()
                .As<IAdminDirection>();

            builder.RegisterType<AttachmentService>()
                .As<IAttachment>();

            builder.RegisterType<CashTables>()
                .As<ICash>();

            builder.RegisterType<Menu>()
                .As<IAdminLocationMenu>();

            builder.RegisterType<Cartoons>()
                .As<ICartoons>()
                .As<IAdminCartoons>();

            builder.RegisterType<Blog>()
                .As<IBlog>()
                .As<IAdminBlog>();

            builder.RegisterType<AnimationParser>()
                .As<IAnimationImport>();

            builder.RegisterType<SendData>()
                .As<ISender>();

            builder.RegisterType<ImportService>()
                .As<ILocationImport>();

            builder.RegisterType<FormatterTagToCreateOrEditLocation>();

            builder.RegisterType<FormatterCategoryToCreateOrEditNote>();

            builder.RegisterType<FormatterDirectionToCreateOrEditLocation>();

            builder.Register(context => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            }
            )).AsSelf().SingleInstance();

            builder.Register(c =>
            {
                var context = c.Resolve<IComponentContext>();
                var config = context.Resolve<MapperConfiguration>();
                return config.CreateMapper(context.Resolve);
            })
            .As<IMapper>()
            .InstancePerLifetimeScope();

            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var resolver = new EnvironmentJsonResolver<Config>("appsettings.json", $"appsettings.{env}.json");
            var module = new ConfigurationModule(resolver);

            builder.RegisterModule(module);
        }
    }
}
