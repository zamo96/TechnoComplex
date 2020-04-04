using System;
namespace TechnologyComplex.Models
{
	public class Model_For_Select2
	{
		public int id { get; set; }
		public string text { get; set; }

		public Model_For_Select2(int id, string text)
		{
			this.id = id;
			this.text = text;
		}
	}
}
