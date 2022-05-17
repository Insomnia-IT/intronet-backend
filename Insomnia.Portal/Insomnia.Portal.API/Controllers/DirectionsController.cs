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
    public class DirectionsController : BaseController
    {
        private readonly ILogger<DirectionsController> _logger;
        private readonly IMapper _mapper;
        private readonly IDirection _direction;

        public DirectionsController(ILogger<DirectionsController> logger, IMapper mapper, IDirection direction)
        {
            _logger = logger;
            _mapper = mapper;
            _direction = direction;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var directions = await _direction.GetAll();

            return Result(directions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var direction = await _direction.Get(id);

            return Result(direction);
        }
    }
}
