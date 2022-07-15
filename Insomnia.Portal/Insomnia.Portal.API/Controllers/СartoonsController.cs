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
    public class CartoonsController : BaseController
    {
        private readonly ILogger<CartoonsController> _logger;
        private readonly IMapper _mapper;
        private readonly ICartoons _сartoons;

        public CartoonsController(ILogger<CartoonsController> logger, IMapper mapper, ICartoons сartoons)
        {
            _logger = logger;
            _mapper = mapper;
            _сartoons = сartoons;
        }

        [HttpGet("schedule")]
        public async Task<IActionResult> Get()
        {
            var schedule = await _сartoons.GetAll();

            return Result(schedule);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetId(int id)
        {
            var schedule = await _сartoons.Get(id);

            return Ok(new
            {
                schedule.Success,
                schedule.Model
            });
        }
    }
}
