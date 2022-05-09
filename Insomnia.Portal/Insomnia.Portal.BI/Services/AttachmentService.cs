using AutoMapper;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insomnia.Portal.BI.Interfaces;
using Insomnia.Portal.BI.Options;
using Insomnia.Portal.Data.Dto;
using Insomnia.Portal.EF;
using Microsoft.EntityFrameworkCore;
using Insomnia.Portal.Data.Entity;
using Insomnia.Portal.General.Expansions;
using Insomnia.Portal.Data.ViewModels.Input;
using Insomnia.Portal.Data.Enums;
using Insomnia.Portal.Data.Return;

namespace Insomnia.Portal.BI.Services
{
    public class AttachmentService : Base<Attachment, AttachmentDto>, IAttachment
    {
        private AttachmentOptions _config;

        public AttachmentService(IMapper mapper, ServiceDbContext context, AttachmentOptions config) : base(mapper, context)
        {
            _config = config;

            if (!Directory.Exists(_config.Path))
                Directory.CreateDirectory(_config.Path);
        }

        public async Task<string> Upload(CreateAttachment attachment)
        {
            var entity = GetEntity(attachment);
            if (entity == null)
                throw new Exception("Не удалось создать тэг!");

            using (var file = File.Create(GetPath(entity.TempName)))
            {
                await attachment.File.OpenReadStream().CopyStreamTo(file);
            }

            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();

            return String.Format($"{_config.UrlAttachmentService.FixUrl()}{entity.Guid}");
        }

        public async Task<(MemoryStream,string)> GetFile(Guid guid)
        {
            var entity = await Attachments.SingleOrDefaultAsync(x => x.Guid == guid);
            if (entity is null)
                return (null,"Файл не найден!");

            var dto = entity.ToDto<AttachmentDto>(_mapper);

            MemoryStream ms = new MemoryStream();

            using (FileStream fs = File.Open(GetPath(entity.TempName), FileMode.Open))
            {
                fs.CopyTo(ms);
            }

            dto.Stream = ms;


            return (ms,dto.FileName);
        }

        public async Task<AttachmentReturn> Get(Guid guid)
        {
            var entity = await Attachments.SingleOrDefaultAsync(x => x.Guid == guid);
            if (entity is null)
                return Error("Файла с указанным ID не найдено!");

            return Ok(entity);
        }

        private string GetPath(string nameFile) => _config.Path + "/" + nameFile;

        private AttachmentReturn Ok(Attachment attachment)
        {
            var dto = attachment.ToDto<AttachmentDto>(_mapper);

            MemoryStream ms = new MemoryStream();

            using (FileStream fs = File.Open(GetPath(dto.TempName), FileMode.Open))
            {
                fs.CopyTo(ms);
            }

            dto.Stream = ms;

            ms.CopyTo(dto.Stream);

            return Ok(dto.ToReturn<AttachmentReturn>());
        }

        private AttachmentReturn Error(string errorMessage) => base.Error<AttachmentReturn>(errorMessage);

        private AttachmentReturn NotFound(string errorMessage) => base.Error<AttachmentReturn>(errorMessage, CodeRequest.NotFound);
        
        private IQueryable<Attachment> Attachments => _context.Attachments;

    }
}