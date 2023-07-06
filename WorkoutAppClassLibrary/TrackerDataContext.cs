using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WorkoutTrackerClassLibrary
{
    public class TrackerDataContext : DbContext
    {
        public TrackerDataContext() { }
        public TrackerDataContext(DbContextOptions options) : base(options) { }

        //Tables
        public DbSet<WorkoutDTO> Workouts { get; set; }
        public DbSet<ClassTypeDTO> ClassTypes { get; set; }

        //Configuring
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=MATTNEWPC;Initial Catalog=WorkoutTrackerData;Integrated Security=True;TrustServerCertificate=true;Multiple Active Result Sets=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WorkoutDTO>()
                .HasOne(e => e.ClassType)
                .WithMany(c => c.Workouts)
                .HasForeignKey(e => e.ClassTypeId);
        }
    }
}
