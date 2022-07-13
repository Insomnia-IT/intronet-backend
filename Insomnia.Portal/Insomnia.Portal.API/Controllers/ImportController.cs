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
        private readonly ILocationImport _location;

        public ImportController(ILogger<СartoonsController> logger, IMapper mapper, IAnimationImport animation, ILocationImport location)
        {
            _logger = logger;
            _mapper = mapper;
            _animation = animation;
            _location = location;
        }

        [HttpPost("schedule-animations")]
        public async Task<IActionResult> ScheduleAnimations([FromForm] Excel file)
        {
            var schedule = _animation.GetAnimations(file.File.OpenReadStream());

            return Ok(schedule);
        }

        [HttpPost("schedule-locationss")]
        public async Task<IActionResult> ScheduleLocations([FromForm] Excel file)
        {
            var schedule = _animation.GetAnimations(file.File.OpenReadStream());

            return Ok(schedule);
        }

        [HttpGet("locations")]
        public async Task<IActionResult> Locations()
        {
            var schedule = await _location.Locations();

            return Ok(schedule);
        }
    }

    public class Excel
    {
        public IFormFile File { get; set; }
    }
}
