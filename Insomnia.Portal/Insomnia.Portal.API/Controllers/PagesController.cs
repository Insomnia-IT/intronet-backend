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
using Insomnia.Portal.Data.Filters;

namespace Insomnia.Portal.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class PagesController : BaseController
    {
        private readonly ILogger<PagesController> _logger;
        private readonly IMapper _mapper;
        private readonly IBlog _blog;

        public PagesController(ILogger<PagesController> logger, IMapper mapper, IBlog blog)
        {
            _logger = logger;
            _mapper = mapper;
            _blog = blog;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var pages = await _blog.GetAll();

            return Result(pages);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var page = await _blog.Get(id);

            return Result(page);
        }
    }
}
