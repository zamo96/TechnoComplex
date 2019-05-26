using System;
using Microsoft.EntityFrameworkCore;
namespace TechnologyComplex.Models
{
    public class Company_FacilityContext : DbContext
    {
        public DbSet<Company_Facility> Company_Facility { get; set; }
        public Company_FacilityContext(DbContextOptions<FacilityContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public Company_FacilityContext()
        {
        }
    }
}
