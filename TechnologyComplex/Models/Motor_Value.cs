using System;
namespace TechnologyComplex.Models
{
    public class Motor_Value
    {
        public int Id { get; set; }
        public string Name { get; set; } // название тэга
        public string Value { get; set; } // значение тэга
        public DateTime Date { get; set; }
        public Motor_Value()
        {

        }
    }
}
