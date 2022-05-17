using AutoMapper;
using AutoMapper.Collection;
using AutoMapper.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Insomnia.Portal.BI.Interfaces;
using Insomnia.Portal.Data.ViewModels.Input;

using Insomnia.Portal.API.Controllers.Base;

namespace Insomnia.Portal.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class AttachmentsController : BaseController
    {
        private readonly ILogger<AttachmentsController> _logger;
        private readonly IMapper _mapper;
        private readonly IAttachment _attachment;

        public AttachmentsController(ILogger<AttachmentsController> logger, IMapper mapper, IAttachment attachment)
        {
            _logger = logger;
            _mapper = mapper;
            _attachment = attachment;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] CreateAttachment file)
        {
            if (file.File == null || file.File.Length == 0)
                BadRequest("Данные о файле недоступны!");

            try
            {
                var result = await _attachment.Upload(file);
                if (String.IsNullOrEmpty(result))
                    return BadRequest("Не удалось сохранить файл!");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{guid}")]
        public async Task<IActionResult> Get(Guid guid)
        {
            try
            {
                var result = await _attachment.Get(guid);
                return File(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
