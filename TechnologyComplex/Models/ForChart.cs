using System;
namespace TechnologyComplex.Models
{
    public class ForChart
    {
        public string name { get; set; }
        public string type { get; set; }
        public double[] data { get; set; }
        public ForChart(string name, string type , double[] data)
        {
            this.name = name;
            this.type = type;
            this.data = data;
        }
    }
}
