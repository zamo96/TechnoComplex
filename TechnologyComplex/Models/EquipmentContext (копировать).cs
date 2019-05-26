using System;
using Microsoft.EntityFrameworkCore;
namespace TechnologyComplex.Models
{
    public class DriveContext : DbContext
    {
        public DbSet<Drive> Drive { get; set; }
        public DriveContext(DbContextOptions<DriveContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public DriveContext()
        {
        }
    }
}
