using System;
namespace TechnologyComplex.Models
{
    public class Motor
    {
        public int Id { get; set; }
        public int? Id_Equipment { get; set; }
        public int? Id_Unit { get; set; }
        public int? Id_Area { get; set; }
        public string Name { get; set; } // название двигателя
        public string Tag { get; set; } // название tag
        public string Serial_Number { get; set; } 
        public string Serial_Order { get; set; } 
        public string Company_Name { get; set; } 
        public string Mass { get; set; } 
        public string Year_expl { get; set; }
        public Motor()
        {

        }
    }
}
