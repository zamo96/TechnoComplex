using System;
namespace TechnologyComplex.Models
{
    public class Pressure_Sensor
    {
        public int Id { get; set; }
        public int Id_Group { get; set; }
        public string Name { get; set; } // название тэга
        public string Value { get; set; } // значение тэга
        public DateTime Date { get; set; }
        public Pressure_Sensor()
        {

        }
    }
}
