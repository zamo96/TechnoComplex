using System;
using Microsoft.EntityFrameworkCore;
namespace TechnologyComplex.Models
{
    public class Technology_Complex_Context : DbContext
    {
        public DbSet<Facility> Facility { get; set; }
        public DbSet<WorkFlow> WorkFlow { get; set; }
        public DbSet<Area> Area { get; set; }
        public DbSet<Unit> Unit { get; set; }
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<Facility_WorkFlow> Facility_WorkFlow { get; set; }
        public DbSet<WorkFlow_Area> WorkFlow_Area { get; set; }
        public DbSet<Motor> Motor { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Facility_WorkFlow>()
                .HasKey(Fw => new { Fw.Id_Facility, Fw.Id_WorkFLow });
            modelBuilder.Entity<WorkFlow_Area>()
               .HasKey(Wa => new { Wa.Id_WorkFlow, Wa.Id_Area });
        }

        public Technology_Complex_Context(DbContextOptions<Technology_Complex_Context> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public Technology_Complex_Context()
        {
        }
    }
}
