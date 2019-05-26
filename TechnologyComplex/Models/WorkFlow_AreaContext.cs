using System;
using Microsoft.EntityFrameworkCore;
namespace TechnologyComplex.Models
{
    public class WorkFlow_AreaContext: DbContext
    {
        public DbSet<WorkFlow_Area> WorkFlow_Area { get; set; }
        public WorkFlow_AreaContext(DbContextOptions<WorkFlow_AreaContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public WorkFlow_AreaContext()
        {
        }
    }
}
