using System;
namespace TechnologyComplex.Models
{
    public class Service
    {
        public int Id_Motor { get; set; }
        public int Number { get; set; }
        public string Name_Of_Service { get; set; } // название сервиса
        public string Work_Hours { get; set; } // название tag
        public string Commentary { get; set; }
        public string Date { get; set; }
        public Service()
        {
        }
    }
}
