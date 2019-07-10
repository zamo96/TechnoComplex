using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechnologyComplex.Models
{
    public class HistoryValues    
    {
        [Key]
        public DateTime DateTime { get; set; }
        public string TagName { get; set; } // название тэга
        public double Value { get; set; } // значение тэга
        public string vValue { get; set; } 
      
        public HistoryValues()
        {

        }
    }
}
