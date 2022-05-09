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
using Insomnia.Portal.Data.ViewModels.Input;
using Insomnia.Portal.Data.Dto;
using Insomnia.Portal.BI.Interfaces;

namespace Insomnia.Portal.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class AdminController : BaseController
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IMapper _mapper;
        private readonly IAdminLocation _location;
        private readonly IAdminTag _tag;
        private readonly IAdminNotesboard _notesboard;
        private readonly IAdminSchedule _schedule;
        private readonly IAdminNotesCategories _notescategories;

        public AdminController(ILogger<AdminController> logger, IMapper mapper,
            IAdminLocation location, IAdminTag tag, IAdminNotesboard notesboard, IAdminSchedule schedule, IAdminNotesCategories notescategories)
        {
            _logger = logger;
            _mapper = mapper;
            _location = location;
            _tag = tag;
            _notesboard = notesboard;
            _schedule = schedule;
            _notescategories = notescategories;
        }

        [HttpPost("locations/add")]
        public async Task<IActionResult> AddLocation([FromBody] CreateLocation model)
        {
            try
            {
                var result = await _location.Add(model);

                return Result(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("locations/edit")]
        public async Task<IActionResult> EditLocation([FromBody] EditLocation model)
        {
            //Перенести в Middleware
            if (model == null)
                return BadRequest("Пустая модель запроса!");

            if (model.Id <= 0)
                return NotFound("Локации с ID = 0 не существует!");

            try
            {
                var result = await _location.Edit(model);

                return Result(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("locations/delete/{id}")]
        public async Task<IActionResult> DeleteLocation(int id)
        {
            try
            {
                var result = await _location.Delete(id);

                return Result(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("tags/add")]
        public async Task<IActionResult> AddTag([FromBody] CreateTag model)
        {
            try
            {
                var result = await _tag.Add(model);

                return Result(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("tags/edit")]
        public async Task<IActionResult> EditTag([FromBody] EditTag model)
        {
            if (model == null)
                return BadRequest("Пустая модель запроса!");

            if (model.Id <= 0)
                return NotFound("Локации с ID = 0 не существует!");

            try
            {
                var result = await _tag.Edit(model);

                return Result(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("tags/delete/{id}")]
        public async Task<IActionResult> DeleteTag(int id)
        {
            try
            {
                var result = await _tag.Delete(id);

                return Result(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("notes/add")]
        public async Task<IActionResult> AddNote([FromBody] CreateNote model)
        {
            try
            {
                var result = await _notesboard.Add(model);

                return Result(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("notes/edit")]
        public async Task<IActionResult> EditNote([FromBody] EditNote model)
        {
            if (model == null)
                return BadRequest("Пустая модель запроса!");

            if (model.Id <= 0)
                return NotFound("Записи с ID = 0 не существует!");

            try
            {
                var result = await _notesboard.Edit(model);

                return Result(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("notes/delete/{id}")]
        public async Task<IActionResult> DeleteNote(int id)
        {
            try
            {
                var result = await _notesboard.Delete(id);

                return Result(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("notes/categories/add")]
        public async Task<IActionResult> AddNoteCategory([FromBody] CreateNoteCategory model)
        {
            try
            {
                var result = await _notescategories.Add(model);

                return Result(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("notes/categories/edit")]
        public async Task<IActionResult> EditNoteCategory([FromBody] EditNoteCategory model)
        {
            if (model == null)
                return BadRequest("Пустая модель запроса!");

            if (model.Id <= 0)
                return NotFound("Записи с ID = 0 не существует!");

            try
            {
                var result = await _notescategories.Edit(model);

                return Result(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("notes/categories/delete/{id}")]
        public async Task<IActionResult> DeleteNoteCategory(int id)
        {
            try
            {
                var result = await _notescategories.Delete(id);

                return Result(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
