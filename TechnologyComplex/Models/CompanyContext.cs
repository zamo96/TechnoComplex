using System;
using Microsoft.EntityFrameworkCore;
namespace TechnologyComplex.Models
{
    public class CompanyContext: DbContext
    {
        public DbSet<Company> Company { get; set; }
        public CompanyContext(DbContextOptions<CompanyContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public CompanyContext()
        {
        }
    }
}
