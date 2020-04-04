using System;
namespace TechnologyComplex.Models
{
    public class ForBarChart
    {
        public string name { get; set; }
        public double[] data { get; set; }
        public ForBarChart(string name, double[] data)
		{
			this.name = name;
			this.data = data;
		}
	}
}
