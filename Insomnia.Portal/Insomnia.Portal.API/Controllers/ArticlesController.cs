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

namespace Insomnia.Portal.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ArticlesController : BaseController
    {
        private readonly ILogger<ArticlesController> _logger;
        private readonly IMapper _mapper;

        public ArticlesController(ILogger<ArticlesController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            return Ok();
        }
    }
}
