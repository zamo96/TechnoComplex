using System;
using Microsoft.EntityFrameworkCore;
namespace TechnologyComplex.Models
{
    public class HistoryValuesContext: DbContext
    {
        public DbSet<HistoryValues> HistoryValues { get; set; }
        public HistoryValuesContext(DbContextOptions<HistoryValuesContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public HistoryValuesContext()
        {
        }

       

    }
}
