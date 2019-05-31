using System;
using Microsoft.EntityFrameworkCore;
namespace TechnologyComplex.Models
{
    public class MotorContext: DbContext
    {
        public DbSet<Motor> Motor { get; set; }
        public MotorContext(DbContextOptions<MotorContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public MotorContext()
        {
        }
    }
}
