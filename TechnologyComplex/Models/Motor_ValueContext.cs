using System;
using Microsoft.EntityFrameworkCore;
namespace TechnologyComplex.Models
{
    public class Motor_ValueContext: DbContext
    {
        public DbSet<Motor_Value> Motor_Value { get; set; }
        public Motor_ValueContext(DbContextOptions<Motor_ValueContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public Motor_ValueContext()
        {
        }
    }
}
