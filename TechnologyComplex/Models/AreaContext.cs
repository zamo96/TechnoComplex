using System;
using Microsoft.EntityFrameworkCore;
namespace TechnologyComplex.Models
{
    public class AreaContext: DbContext
    {
        public DbSet<Area> Area { get; set; }
        public AreaContext(DbContextOptions<AreaContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public AreaContext()
        {
        }
    }
}
