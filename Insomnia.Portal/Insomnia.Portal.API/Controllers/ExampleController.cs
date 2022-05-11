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
    [Route("")]
    public class ExampleController : BaseController
    {
        private readonly ILogger<ExampleController> _logger;
        private readonly IMapper _mapper;

        public ExampleController(ILogger<ExampleController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("alive")]
        public async Task<IActionResult> Alive()
        {
            return Ok("I BELIVE I CAN FLY!");
        }
    }
}
