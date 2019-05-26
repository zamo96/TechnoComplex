using System;
using Microsoft.EntityFrameworkCore;
namespace TechnologyComplex.Models
{
    public class WorkFlowContext: DbContext
    {
        public DbSet<WorkFlow> WorkFlow { get; set; }
        public WorkFlowContext(DbContextOptions<WorkFlowContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public WorkFlowContext()
        {
        }
    }
}
