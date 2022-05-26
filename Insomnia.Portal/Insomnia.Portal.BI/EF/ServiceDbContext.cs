using Insomnia.Portal.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Insomnia.Portal.General.Expansions;

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
            Database.EnsureDeleted();
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
            var nameType = new List<string>();
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
                    if (new[] { EntityState.Added, EntityState.Modified }.Contains(entry.State))
                        nameType.AddGroupBy(entry.Entity.GetType().Name);
                }
                else if (entry.Entity is Elementtable || entry.Entity is Timetable)
                {
                    if (new[] { EntityState.Added, EntityState.Modified }.Contains(entry.State))
                        nameType.AddGroupBy(nameof(Location));
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
            if(!nameType.IsEmptyOrNull())
                UpdateCash(nameType);

            if (newHistory is not null)
                AddHistory(newHistory);
        }


        private void AddHistory(HistoryElementtable history)
        {
            HistoryElements.Add(history);
        }

        //ДА. ЭТО КОСТЫЛЬ. НО ПЛЕВАТЬ))
        private void UpdateCash(List<string> names)
        {
            var cashs = Cash.Where(x => names.Contains(x.Name)).ToList();
            var utcNow = DateTime.UtcNow;

            foreach (var name in names.Except(cashs.Select(x => x.Name)))
            {
                Cash.Add(new Cash() { Name = name, Version = 1, CreatedDate = utcNow });
            }
            foreach(var cash in cashs)
            {
                cash.Version += 0.01;
                cash.Version = Math.Round(cash.Version, 2);
                cash.ModifiedDate = utcNow;
                Cash.Update(cash);
            }
        }

        #endregion
    }
}