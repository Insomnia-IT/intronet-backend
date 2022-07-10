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
using Microsoft.AspNetCore.Http;

namespace Insomnia.Portal.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ImportController : BaseController
    {
        private readonly ILogger<СartoonsController> _logger;
        private readonly IMapper _mapper;
        private readonly IAnimationImport _animation;

        public ImportController(ILogger<СartoonsController> logger, IMapper mapper, IAnimationImport animation)
        {
            _logger = logger;
            _mapper = mapper;
            _animation = animation;
        }

        [HttpPost("schedule-animations")]
        public async Task<IActionResult> ScheduleAnimations([FromForm] Excel file)
        {
            var schedule = _animation.GetAnimations(file.File.OpenReadStream());

            return Ok(schedule);
        }
    }

    public class Excel
    {
        public IFormFile File { get; set; }
    }
}
