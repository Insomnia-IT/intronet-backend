using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insomnia.Portal.EF;
using Insomnia.Portal.BI.Interfaces;
using Insomnia.Portal.General.Exceptions;
using Insomnia.Portal.Data.Dto;
using Insomnia.Portal.Data.Return;

namespace Insomnia.Portal.BI.Services
{
    public class ImportService : ILocationImport, ITimetableImport
    {
        private readonly ServiceDbContext _context;
        private readonly ISender _sender;

        public ImportService(ServiceDbContext context, ISender sender)
        {
            _context = context;
            _sender = sender;
        }

        public async Task<ImportReturn> Locations()
        {
            return null;
        }

        public async Task<ImportReturn> Timetables()
        {
            return null;
        }

        private async Task<List<GeoLocation>> GetCords()
        {
            return null;
        }

        private async Task<LocationDto> GetGeoLocation()
        {
            return null;
        }
    }
}
