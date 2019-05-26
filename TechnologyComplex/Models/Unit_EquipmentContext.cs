using System;
using Microsoft.EntityFrameworkCore;
namespace TechnologyComplex.Models
{
    public class Unit_EquipmentContext: DbContext
    {
        public DbSet<Unit_Equipment> Unit_Equipment { get; set; }
        public Unit_EquipmentContext(DbContextOptions<Unit_EquipmentContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public Unit_EquipmentContext()
        {
        }
    }
}
