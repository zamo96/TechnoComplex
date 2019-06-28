using System;
namespace TechnologyComplex.Models
{
    public class Motor
    {
        public int Id { get; set; }
        public int Id_Equipment { get; set; }
        public string Name { get; set; } // название двигателя
        public string Tag { get; set; } // название tag

        public Motor()
        {

        }
    }
}
