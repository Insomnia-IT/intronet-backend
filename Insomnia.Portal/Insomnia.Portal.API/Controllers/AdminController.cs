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
using Microsoft.AspNetCore.Authorization;
using Insomnia.Portal.Data.Attributes;
using Insomnia.Portal.BI.Options;
using Insomnia.Portal.Data.Generic;
using Microsoft.AspNetCore.Http;
using Insomnia.Portal.Data.ViewModels.Input.Create;

namespace Insomnia.Portal.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class AdminController : BaseController
    {
        private readonly AuthOptions _config;
        private readonly ILogger<AdminController> _logger;
        private readonly IMapper _mapper;
        private readonly IAdminLocation _location;
        private readonly IAdminTag _tag;
        private readonly IAdminNotesboard _notesboard;
        private readonly IAdminSchedule _schedule;
        private readonly IAdminNotesCategories _notescategories;
        private readonly IAdminDirection _direction;
        private readonly IAdminLocationMenu _locationMenu;
        private readonly IAdminBlog _blog;
        private readonly IAdminCartoons _cartoons;

        public AdminController(ILogger<AdminController> logger, IMapper mapper,
            IAdminLocation location, IAdminTag tag, IAdminNotesboard notesboard, IAdminSchedule schedule, IAdminNotesCategories notescategories, IAdminDirection direction, IAdminLocationMenu locationMenu, AuthOptions config, IAdminBlog blog, IAdminCartoons cartoons)
        {
            _logger = logger;
            _mapper = mapper;
            _location = location;
            _tag = tag;
            _notesboard = notesboard;
            _schedule = schedule;
            _notescategories = notescategories;
            _direction = direction;
            _locationMenu = locationMenu;
            _config = config;
            _blog = blog;
            _cartoons = cartoons;
        }

        [AllowAnonymous]
        [HttpGet("auth")]
        public async Task<IActionResult> Auth([FromQuery] string token)
        {
            if (token == _config.AdminToken)
            {
                HttpContext.Response.Cookies.Append(ResourcesNaming.HeaderToken, token);
                HttpContext.Response.Cookies.Append(ResourcesNaming.HeaderUserName, "admin");
                return Ok();
            }
            if(token == _config.PoteryashkiToken)
            {
                HttpContext.Response.Cookies.Append(ResourcesNaming.HeaderToken, token);
                HttpContext.Response.Cookies.Append(ResourcesNaming.HeaderUserName, "poteryashki");
                return Ok();
            }

            return Unauthorized();
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Response.Cookies.Delete(ResourcesNaming.HeaderToken);
            HttpContext.Response.Cookies.Delete(ResourcesNaming.HeaderUserName);
            return Ok();
        }

        #region Locations

        [User("admin")]
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

        [User("admin")]
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

        [User("admin")]
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

        #endregion

        #region Location menu

        [User("admin")]
        [HttpPut("locations/menu/edit")]
        public async Task<IActionResult> EditLocationMenu([FromBody] EditMenu model)
        {
            //Перенести в Middleware
            if (model == null)
                return BadRequest("Пустая модель запроса!");

            try
            {
                var result = await _locationMenu.Edit(model);

                return Result(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [User("admin")]
        [HttpDelete("locations/menu/delete/{id}")]
        public async Task<IActionResult> DeleteLocationMenu(int id)
        {
            try
            {
                var result = await _locationMenu.Delete(id);

                return Result(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion

        #region Location tags

        [User("admin")]
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

        [User("admin")]
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

        [User("admin")]
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

        #endregion

        #region Location directions

        [User("admin")]
        [HttpPost("directions/add")]
        public async Task<IActionResult> AddDirection([FromForm] CreateDirection model)
        {
            try
            {
                var result = await _direction.Add(model);

                return Result(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [User("admin")]
        [HttpPut("directions/edit")]
        public async Task<IActionResult> EditDirection([FromForm] EditDirection model)
        {
            if (model == null)
                return BadRequest("Пустая модель запроса!");

            if (model.Id <= 0)
                return NotFound("Направления с ID = 0 не существует!");

            try
            {
                var result = await _direction.Edit(model);

                return Result(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [User("admin")]
        [HttpDelete("directions/delete/{id}")]
        public async Task<IActionResult> DeleteDirection(int id)
        {
            try
            {
                var result = await _direction.Delete(id);

                return Result(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion

        #region Location timetables

        [User("admin")]
        [HttpPost("locations/schedule/add-or-edit")]
        public async Task<IActionResult> AddLocationTimetable([FromBody] EditTimetable model)
        {
            try
            {
                var result = await _schedule.AddOrEdit(model);

                return Result(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [User("admin")]
        [HttpDelete("locations/schedule/{id}")]
        public async Task<IActionResult> DeleteLocationTimetable(int id)
        {
            try
            {
                var result = await _schedule.Delete(id);

                return Result(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion

        #region Notes

        [HttpPost("notes/add")]
        public async Task<IActionResult> AddNote([FromBody] CreateNote model)
        {
            try
            {
                string userName = HttpContext.Request.Cookies[ResourcesNaming.HeaderUserName] ?? HttpContext.Request.Headers[ResourcesNaming.HeaderUserName];
                var result = await _notesboard.Add(model, userName);

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
                string userName = HttpContext.Request.Cookies[ResourcesNaming.HeaderUserName] ?? HttpContext.Request.Headers[ResourcesNaming.HeaderUserName];

                var result = await _notesboard.Edit(model, userName);

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
                string userName = HttpContext.Request.Cookies[ResourcesNaming.HeaderUserName] ?? HttpContext.Request.Headers[ResourcesNaming.HeaderUserName];

                var result = await _notesboard.Delete(id, userName);

                return Result(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion

        #region Notes categories

        [User("admin")]
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

        [User("admin")]
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

        [User("admin")]
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

        #endregion

        #region Pages

        [User("admin")]
        [HttpPost("pages/add")]
        public async Task<IActionResult> AddPage([FromBody] CreatePage model)
        {
            try
            {
                var result = await _blog.Add(model);

                return Result(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [User("admin")]
        [HttpPut("pages/edit")]
        public async Task<IActionResult> EditPage([FromBody] EditPage model)
        {
            if (model == null)
                return BadRequest("Пустая модель запроса!");

            if (model.Id <= 0)
                return NotFound("Записи с ID = 0 не существует!");

            try
            {
                var result = await _blog.Edit(model);

                return Result(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [User("admin")]
        [HttpDelete("pages/delete/{id}")]
        public async Task<IActionResult> DeletePage(int id)
        {
            try
            {
                var result = await _blog.Delete(id);

                return Result(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion

        #region Catroons

        [AllowAnonymous]
        [HttpPost("cartoons/import")]
        public async Task<IActionResult> ImportCartoons([FromForm] File file)
        {
            try
            {
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                var result = await _cartoons.Add(file.Get.OpenReadStream());

                return Result(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion
    }
}
