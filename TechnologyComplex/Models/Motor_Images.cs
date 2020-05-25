using System;
namespace TechnologyComplex.Models
{
    public class Motor_Images
    {
        public int Id_Motor { get; set; }
        public int Number_Of_Image { get; set; }
        public string Name_Of_Image { get; set; }


        public Motor_Images(int Id_Motor, int Number_Of_Image, string Name_Of_Image)
        {
            this.Id_Motor = Id_Motor;
            this.Number_Of_Image = Number_Of_Image;
            this.Name_Of_Image = Name_Of_Image;
        }
    }
}