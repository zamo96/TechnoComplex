using System;
using Microsoft.EntityFrameworkCore;
namespace TechnologyComplex.Models
{
    public class Facility_WorkFlowContext: DbContext
    {
        public DbSet<Facility_WorkFlow> Facility_WorkFlow { get; set; }
        public Facility_WorkFlowContext(DbContextOptions<Facility_WorkFlowContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public Facility_WorkFlowContext()
        {
        }
    }
}
