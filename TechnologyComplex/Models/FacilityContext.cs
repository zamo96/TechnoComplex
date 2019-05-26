using System;
using Microsoft.EntityFrameworkCore;
namespace TechnologyComplex.Models
{
    public class FacilityContext: DbContext
    {
        public DbSet<Facility> Facility { get; set; }
        public FacilityContext(DbContextOptions<FacilityContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public FacilityContext()
        {
        }
    }
}
