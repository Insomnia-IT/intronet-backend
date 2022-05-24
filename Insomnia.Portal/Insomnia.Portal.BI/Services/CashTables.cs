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
    public class CashTables : ICash
    {
        private readonly ServiceDbContext _context;

        public CashTables(ServiceDbContext context)
        {
            _context = context;
        }

        public async Task<Cash> Get(string nameTable)
        {
            return await _context.Cash.SingleOrDefaultAsync(x => x.Name == nameTable);
        }

        public async Task<IList<Cash>> GetAll()
        {
            return await _context.Cash.ToListAsync();
        }
    }
}
