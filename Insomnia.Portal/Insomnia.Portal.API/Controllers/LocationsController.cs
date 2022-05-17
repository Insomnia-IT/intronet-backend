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
    public class LocationsController : BaseController
    {
        private readonly ILogger<LocationsController> _logger;
        private readonly IMapper _mapper;
        private readonly ILocation _location;

        public LocationsController(ILogger<LocationsController> logger, IMapper mapper, ILocation location)
        {
            _logger = logger;
            _mapper = mapper;
            _location = location;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var locations = await _location.GetAll();

            return Result(locations);
        }

        [HttpGet("all/full")]
        public async Task<IActionResult> GetAllFull()
        {
            var locations = await _location.GetAllFull();

            return Result(locations);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetForFilter([FromQuery] LocationsFilter filter)
        {
            var locations = await _location.GetAllWithFilter(filter);

            return Result(locations);
        }

        [HttpGet("filter/full")]
        public async Task<IActionResult> GetFullForFilter([FromQuery] LocationsFilter filter)
        {
            var locations = await _location.GetAllFullWithFilter(filter);

            return Result(locations);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var location = await _location.Get(id);

            return Result(location);
        }

        [HttpGet("{id}/full")]
        public async Task<IActionResult> GetFull(int id)
        {
            var location = await _location.GetFull(id);

            return Result(location);
        }
    }
}
