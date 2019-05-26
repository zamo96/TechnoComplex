using System;
using Microsoft.EntityFrameworkCore;
namespace TechnologyComplex.Models
{
    public class Area_UnitContext: DbContext
    {
        public DbSet<Area_Unit> Area_Unit { get; set; }
        public Area_UnitContext(DbContextOptions<Area_UnitContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public Area_UnitContext()
        {
        }
    }
}
