using System;
namespace TechnologyComplex.Models
{
    public class Motor_Documents
    {
        public int Id_Motor { get; set; }
        public int Number_Of_Document { get; set; }
        public string Name_Of_Document { get; set; }
        public string PDF { get; set; }

        public Motor_Documents(int Id_Motor, int Number_Of_Document, string Name_Of_Document, string PDF)
        {
            this.Id_Motor = Id_Motor;
            this.Number_Of_Document = Number_Of_Document;
            this.Name_Of_Document = Name_Of_Document;
            this.PDF = PDF;
        }
    }
}
