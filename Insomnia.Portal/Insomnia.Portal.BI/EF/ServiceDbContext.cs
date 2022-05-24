using Insomnia.Portal.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Insomnia.Portal.EF
{
    public partial class ServiceDbContext : DbContext
    {
        public DbSet<Location> Locations { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Timetable> Timetables { get; set; }
        public DbSet<NoteCategory> NoteCategories { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Direction> Directions { get; set; }
        public DbSet<HistoryElementtable> HistoryElements { get; set; }
        public DbSet<Cash> Cash { get; set; }

        public ServiceDbContext(DbContextOptions<ServiceDbContext> option) : base(option)
        {
          //  Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        #region Automatic generation Date for "CreatedDate" and "ModifiedDate"
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override async Task<int> SaveChangesAsync(
           bool acceptAllChangesOnSuccess,
           CancellationToken cancellationToken = default(CancellationToken)
        )
        {
            OnBeforeSaving();
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess,
                          cancellationToken);
        }

        private void OnBeforeSaving()
        {
            var entries = ChangeTracker.Entries();
            var utcNow = DateTime.UtcNow;
            var nameType = "";
            HistoryElementtable newHistory = null;

            foreach (var entry in entries)
            {
                if (entry.Entity is Base2 trackable)
                {
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            trackable.ModifiedDate = utcNow;

                            entry.Property(nameof(Base2.CreatedDate)).IsModified = false;
                            break;

                        case EntityState.Added:
                            trackable.CreatedDate = utcNow;
                            break;
                    }
                }
                if (entry.Entity is BaseCashing2 || entry.Entity is BaseCashing)
                {
                    nameType = entry.Entity.GetType().Name;
                }
                else if (entry.Entity is Elementtable || entry.Entity is Timetable)
                {
                    nameType = nameof(Location);
                }
                if (entry.Entity is Attachment attach)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            attach.Guid = Guid.NewGuid();
                            break;
                    }
                }
                if (entry.Entity is Elementtable elementtable)
                {
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            newHistory = new HistoryElementtable()
                            {
                                CreatedDateTimeOldValue = utcNow,
                                OldValue = elementtable.Time.ToString("t"),
                                TimetableId = elementtable.Id,
                            };
                            break;
                    }
                }
            }
            if(!String.IsNullOrEmpty(nameType))
                UpdateCash(nameType);

            if (newHistory is not null)
                AddHistory(newHistory);
        }


        private void AddHistory(HistoryElementtable history)
        {
            HistoryElements.Add(history);
        }

        //ДА. ЭТО КОСТЫЛЬ. НО ПЛЕВАТЬ))
        private void UpdateCash(string name)
        {
            var cash = Cash.SingleOrDefault(x => x.Name == name);
            var utcNow = DateTime.UtcNow;

            if (cash is null)
            {
                cash = new Cash() { Name = name, Version = 1, CreatedDate = utcNow };
                Cash.Add(cash);
            }
            else
            {
                cash.Version += 0.01;
                cash.ModifiedDate = utcNow;
                Cash.Update(cash);
            }
        }

        #endregion
    }
}