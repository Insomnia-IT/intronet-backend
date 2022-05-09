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
    public class ScheduleController : BaseController
    {
        private readonly ILogger<ScheduleController> _logger;
        private readonly IMapper _mapper;
        private readonly ISchedule _schedule;

        public ScheduleController(ILogger<ScheduleController> logger, IMapper mapper, ISchedule schedule)
        {
            _logger = logger;
            _mapper = mapper;
            _schedule = schedule;
        }

        [HttpGet("{0}")]
        public async Task<IActionResult> Get(int locationId)
        {
            var tag = await _schedule.Get(locationId);

            return Result(tag);
        }
    }
}
