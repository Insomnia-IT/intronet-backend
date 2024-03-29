﻿using AutoMapper;
using AutoMapper.Collection;
using AutoMapper.EntityFrameworkCore;
using Insomnia.Portal.API.Controllers.Base;
using Insomnia.Portal.BI.Interfaces;
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
    public class CashController : ControllerBase
    {
        private readonly ILogger<CashController> _logger;
        private readonly IMapper _mapper;
        private readonly ICash _cash;

        public CashController(ILogger<CashController> logger, IMapper mapper, ICash cash)
        {
            _logger = logger;
            _mapper = mapper;
            _cash = cash;
        }

        [HttpGet]
        public async Task<IActionResult> CashGetAll()
        {
            var version = await _cash.GetAll();
            
            return Ok(version);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> CashGet(string name)
        {
            var version = await _cash.Get(name);

            AddVersionInHeaders(version?.Version ?? 0);

            return Ok();
        }

        private void AddVersionInHeaders(double version)
        {
            Response.Headers.Add("Version", $"{version}");
        }
    }
}
