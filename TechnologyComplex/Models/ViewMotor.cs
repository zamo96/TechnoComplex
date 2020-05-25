using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace TechnologyComplex.Models
{
    public class ViewMotor
    {
        public IQueryable<Motor> Motors { get; set; }

        public string Text { get; set; }
    }
}
