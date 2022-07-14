using Insomnia.Portal.Data.Attributes;
using Insomnia.Portal.Data.Entity;
using Insomnia.Portal.General.Expansions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insomnia.Portal.Data.Generic;

namespace Insomnia.Portal.EF
{
    public partial class ServiceDbContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var defaultCategory = new NoteCategory()
            {
                Id = StaticValues.DefaultIdNoteCategories,
                Name = StaticValues.DefaultNameNoteCategories,
            };

            modelBuilder.Entity<NoteCategory>().HasData(defaultCategory);
        }
    }
}
