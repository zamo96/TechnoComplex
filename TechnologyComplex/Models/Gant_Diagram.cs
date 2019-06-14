using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TechnologyComplex.Models
{
    public class Gant_Diagram
    {
        public double x { get; set; }
        public double x2 { get; set; }
        public int y { get; set; }

        public Gant_Diagram(double x_value, double x2_value, int y_value)
        {
            x = x_value;
            x2 = x2_value;
            y = y_value;
        }
    }
}
