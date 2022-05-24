using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Insomnia.Portal.BI.Interfaces;
using Insomnia.Portal.Data.Return;
using Insomnia.Portal.Data.Dto;
using Insomnia.Portal.EF;
using Insomnia.Portal.Data.Filters;
using Insomnia.Portal.General.Expansions;
using Insomnia.Portal.Data.Enums;
using Insomnia.Portal.Data.ViewModels.Input;
using Insomnia.Portal.Data.Entity;

namespace Insomnia.Portal.BI.Services
{
    public class Menu : Base<Data.Entity.Location, LocationDto>, IAdminLocationMenu
    {
        public Menu(IMapper mapper, ServiceDbContext context) : base(mapper, context)
        {
        }

        public async Task<MenuReturn> Edit(EditMenu menu)
        {
            try
            {
                var location = await Locations.SingleOrDefaultAsync(x => x.Id == menu.LocationId);

                if (location is null)
                    return NotFound("Локация с указанным ID не найдена!");

                location.Menu = menu.Text;
                _context.Update(location);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }

        public async Task<MenuReturn> Delete(int locationId)
        {
            try
            {
                var location = await Locations.SingleOrDefaultAsync(x => x.Id == locationId);

                if (location is null)
                    return NotFound("Локация с указанным ID не найдена!");

                location.Menu = null;
                _context.Update(location);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }

        private MenuReturn Ok() => base.Ok<MenuReturn>();

        private MenuReturn Error(string errorMessage) => base.Error<MenuReturn>(errorMessage);

        private MenuReturn NotFound(string errorMessage) => base.Error<MenuReturn>(errorMessage, CodeRequest.NotFound);

        private IQueryable<Data.Entity.Location> Locations => _context.Locations;

    }
}
