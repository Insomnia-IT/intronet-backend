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
    public class NotesController : BaseController
    {
        private readonly ILogger<NotesController> _logger;
        private readonly IMapper _mapper;
        private readonly INotesboard _board;
        private readonly INotesCategories _categories;

        public NotesController(ILogger<NotesController> logger, IMapper mapper, INotesboard board, INotesCategories categories)
        {
            _logger = logger;
            _mapper = mapper;
            _board = board;
            _categories = categories;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var notes = await _board.GetAll();

            return Result(notes);
        }

        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categories.GetAll();

            return Result(categories);
        }

        [HttpGet("category/{id}")]
        public async Task<IActionResult> GetCategory(int categoryId)
        {
            var category = await _categories.Get(categoryId);

            return Result(category);
        }

        [HttpGet("all/filter")]
        public async Task<IActionResult> GetAllWithFilter([FromQuery] NotesFilter filter)
        {
            var notes = await _board.GetAllWithFilter(filter);

            return Result(notes);
        }

        [HttpGet("{0}")]
        public async Task<IActionResult> Get(int id)
        {
            var note = await _board.Get(id);

            return Result(note);
        }
    }
}
