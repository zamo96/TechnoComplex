using System;
using Microsoft.EntityFrameworkCore;
namespace TechnologyComplex.Models
{
    public class UnitContext: DbContext
    {
        public DbSet<Unit> Unit { get; set; }
        public UnitContext(DbContextOptions<UnitContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public UnitContext()
        {
        }
    }
}
