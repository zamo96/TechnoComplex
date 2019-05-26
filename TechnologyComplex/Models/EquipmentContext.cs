using System;
using Microsoft.EntityFrameworkCore;
namespace TechnologyComplex.Models
{
    public class EquipmentContext : DbContext
    {
        public DbSet<Equipment> Equipment { get; set; }
        public EquipmentContext(DbContextOptions<EquipmentContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public EquipmentContext()
        {
        }
    }
}
