﻿using System;
namespace TechnologyComplex.Models
{
    public class Equipment
    {
        public int Id { get; set; }
        public string Name { get; set; } // название Оборудования
        public int Id_Temperature_Sensors { get; set; }
        public int Id_Pressure_Sensors { get; set; }
        public int Id_FlowMeters { get; set; }
        public int Id_Conductometrs { get; set; }
        public int Id_Pumps { get; set; }
        public int Id_Drives { get; set; }
        public int Equipment_Id { get; set; }
        public string Type { get; set; } 
        public Equipment()
        {

        }
    }
}
