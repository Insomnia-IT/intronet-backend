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
        private readonly ILogger<ImportController> _logger;
        private readonly IMapper _mapper;
        private readonly IAnimationImport _animation;
        private readonly ILocationImport _location;
        private readonly ITimetableImport _timetable;

        public ImportController(ILogger<ImportController> logger, IMapper mapper, IAnimationImport animation, ILocationImport location, ITimetableImport timetable)
        {
            _logger = logger;
            _mapper = mapper;
            _animation = animation;
            _location = location;
            _timetable = timetable;
        }

        [HttpPost("schedule-animations")]
        public async Task<IActionResult> ScheduleAnimations([FromForm] Excel file)
        {
            var schedule = _animation.GetAnimations(file.File.OpenReadStream());

            return Ok(schedule);
        }

        [HttpGet("schedule-locations")]
        public async Task<IActionResult> ScheduleLocations()
        {
            var schedule = await _timetable.Timetables();

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
