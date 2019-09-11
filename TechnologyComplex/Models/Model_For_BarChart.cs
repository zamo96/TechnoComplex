using System;
namespace TechnologyComplex.Models
{
    public class Model_For_BarChart
    {
        public string name { get; set; }
        public double y { get; set; } // название участка

        public Model_For_BarChart(string name, double y)
        {
            this.name = name;
            this.y = y;
        }
    }
}
