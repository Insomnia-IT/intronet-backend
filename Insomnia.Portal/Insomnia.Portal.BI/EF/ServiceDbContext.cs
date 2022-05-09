﻿using Insomnia.Portal.Data.Entity;
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

        public ServiceDbContext(DbContextOptions<ServiceDbContext> option) : base(option)
        {
            //Database.EnsureDeleted();
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
            }
        }

        #endregion
    }
}