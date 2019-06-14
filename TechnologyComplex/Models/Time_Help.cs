using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TechnologyComplex.Models
{
    public class Time_Help : Controller
    {
            public DateTime Time_Start { get; set; }
            public DateTime Time_End { get; set; }
            public Time_Help(DateTime start, DateTime end)
            {
                Time_Start = start;
                Time_End = end;
            }
        }
        }
