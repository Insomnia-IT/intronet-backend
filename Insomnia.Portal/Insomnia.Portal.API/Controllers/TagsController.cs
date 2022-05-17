using AutoMapper;
using AutoMapper.Collection;
using AutoMapper.EntityFrameworkCore;
using Insomnia.Portal.API.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Insomnia.Portal.BI.Interfaces;

namespace Insomnia.Portal.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class TagsController : BaseController
    {
        private readonly ILogger<TagsController> _logger;
        private readonly IMapper _mapper;
        private readonly ITag _tag;

        public TagsController(ILogger<TagsController> logger, IMapper mapper, ITag tag)
        {
            _logger = logger;
            _mapper = mapper;
            _tag = tag;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var tags = await _tag.GetAll();

            return Result(tags);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var tag = await _tag.Get(id);

            return Result(tag);
        }
    }
}
