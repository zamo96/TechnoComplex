using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechnologyComplex.Models;

namespace TechnologyComplex.Controllers
{
    public class HomeController : Controller
    {
        private AreaContext db;
        
        private Motor_ValueContext Db_Motor_Context;
        private Technology_Complex_Context Db_Technology_Complex_Context;
        private HistoryValuesContext Db_HistoryValuesContext;
        public DateTime Today = DateTime.Today;



        public HomeController(AreaContext context, Motor_ValueContext Motor_Context, Technology_Complex_Context technology_Complex_Context, HistoryValuesContext historyContext)
        {
            db = context;
            Db_Motor_Context = Motor_Context;
            Db_Technology_Complex_Context = technology_Complex_Context;
            Db_HistoryValuesContext = historyContext;
        }

        [HttpPost]
         public async Task<IActionResult> Motor_Working_Hours_For_Date(string TimeStart,string TimeEnd, int Id_Motor, int Id_Equipment,int Id_Unit, string Name, string Tag)
         {
          
            ViewBag.Id_Equipment = Id_Equipment;
            ViewBag.Id_Unit = Id_Unit;
            ViewBag.Id_Motor = Id_Motor;
            ViewBag.Name = Name;
            ViewBag.Tag = Tag;
            if (TimeStart != null && TimeEnd != null)
            {
                DateTime DateTimeStart = DateTime.Parse(TimeStart);
                DateTime DateTimeEnd = DateTime.Parse(TimeEnd);
                ViewBag.TimeStart = DateTimeStart;
                ViewBag.TimeEnd = DateTimeEnd;
                int days = 1;
                DateTimeEnd += new TimeSpan(days, 0, 0, 0);

                string datetimestart = DateTimeStart.ToString("yyyyMMdd");
                string datetimeend = DateTimeEnd.ToString("yyyyMMdd");
                
                return View("Motor_Working_Hours",await Db_HistoryValuesContext.HistoryValues.FromSql(" SELECT * FROM HistoryValues WHERE HistoryValues.TagName LIKE '%" + Tag + "%' AND HistoryValues.DateTime >= '" + datetimestart + "' and HistoryValues.DateTime <= '" + datetimeend + "' ").ToListAsync());
            
            }
            else
                return View("Motor_Working_Hours", await Db_HistoryValuesContext.HistoryValues.FromSql(" SELECT * FROM HistoryValues WHERE HistoryValues.TagName LIKE '%" + Tag + "%' AND HistoryValues.DateTime between Convert(date,GETDATE()) and  Convert(date,GETDATE()+1) ").ToListAsync());
        }

        public async Task<IActionResult> Facility()
        {

            return View(await Db_Technology_Complex_Context.Facility.ToListAsync());
        }

        [HttpPost]
        public IActionResult Work_Flow(int Id_Facility, string Name_Facility)
        {
            List<int> Work_Flows = new List<int>();
            foreach (var workflow in Db_Technology_Complex_Context.Facility_WorkFlow)
            {
                if (workflow.Id_Facility == Id_Facility)
                {
                    Work_Flows.Add(workflow.Id_WorkFLow);
                }
            }
            List<TechnologyComplex.Models.WorkFlow> workFlows = new List<TechnologyComplex.Models.WorkFlow>();
            foreach (var workflow in Db_Technology_Complex_Context.WorkFlow)
            {
                foreach (var workfl in Work_Flows)
                {
                    if (workflow.Id == workfl)
                    {
                        workFlows.Add(workflow);
                    }
                }
            }
            ViewBag.Name_Facility = Name_Facility;
            return View(workFlows);
        }

        [HttpPost]
        public IActionResult Area(int Id_WorkFlow, string Name_WorkFlow)
        {
            List<int> Areas = new List<int>();
            foreach (var area in Db_Technology_Complex_Context.WorkFlow_Area)
            {
                if (area.Id_WorkFlow == Id_WorkFlow)
                {
                    Areas.Add(area.Id_Area);
                }
            }
            List<TechnologyComplex.Models.Area> areas = new List<TechnologyComplex.Models.Area>();
            foreach (var area in Db_Technology_Complex_Context.Area)
            {
                foreach (var ar in Areas)
                {
                    if (area.Id == ar)
                    {
                        areas.Add(area);
                    }
                }
            }
            ViewBag.Name_WorkFlow = Name_WorkFlow;
            return View(areas);
        }

        [HttpPost]
        public IActionResult Unit(int Id_Area, string Name_Area)
        {
            List<int> Units = new List<int>();
            foreach (var unit in Db_Technology_Complex_Context.Area_Unit)
            {
                if (unit.Id_Area == Id_Area)
                {
                    Units.Add(unit.Id_Unit);
                }
            }
            List<TechnologyComplex.Models.Unit> units = new List<TechnologyComplex.Models.Unit>();
            foreach (var unit in Db_Technology_Complex_Context.Unit)
            {
                foreach (var un in Units)
                {
                    if (unit.Id == un)
                    {
                        units.Add(unit);
                    }
                }
            }
            ViewBag.Name_Area = Name_Area;
            ViewBag.Id_Area = Id_Area;
            return View(units);
        }

        [HttpPost]
        public IActionResult Equipment(int Id_Unit, int Id_Area, string Name_Unit, string Name_Area, string format)
        {
            
            List<int> Equipments = new List<int>();
            foreach (var equipment in Db_Technology_Complex_Context.Unit_Equipment)
            {
                if (equipment.Id_Unit == Id_Unit)
                {
                    Equipments.Add(equipment.Id_Equipment);
                }
            }
            if(format == "Skip")
            {
                ViewBag.Name = Name_Area;
                ViewBag.Id_Area = Id_Area;
                ViewBag.Format = format;
                return View("Control_Module");
            }
              if (Equipments.Count == 0)
              {
                ViewBag.Name = Name_Unit;
                ViewBag.Id_Unit = Id_Unit;
                ViewBag.Id_Equipment = 0;
                return View("Control_Module");       
              }
            List<TechnologyComplex.Models.Equipment> equipments = new List<TechnologyComplex.Models.Equipment>();
            foreach (var equipment in Db_Technology_Complex_Context.Equipment)
            {
                foreach (var Eq in Equipments)
                {
                    if (equipment.Id == Eq)
                    {
                        equipments.Add(equipment);
                    }
                }
            }
            ViewBag.Name_Unit = Name_Unit;

            return View(equipments);
        }

        [HttpPost]
        public IActionResult Control_Module(int Id_Equipment, string Name_Equipment)
        {
            ViewBag.Id_Equipment = Id_Equipment;
            ViewBag.Name = Name_Equipment;
            return View();
        }

        [HttpPost]
        public IActionResult Motor_Panel(int Id_Equipment, int Id_Unit, int Id_Motor, string Name, string Tag)
        {
            ViewBag.Id_Equipment = Id_Equipment;
            ViewBag.Id_Unit = Id_Unit;
            ViewBag.Id_Motor = Id_Motor;
            ViewBag.Name = Name;
            ViewBag.Tag = Tag;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Motor(int Id_Equipment, int Id_Unit, int Id_Area, string Name, string format)
        {

            ViewBag.Name = Name;
            if (format == "Skip")
            {
                ViewBag.Id = Id_Area;
                return View(await Db_Technology_Complex_Context.Motor.Where(x => x.Id_Area == Id_Area).OrderBy(x => x.Id).ToListAsync());

            }
            if (Id_Equipment != 0)
            {
                ViewBag.Id = Id_Equipment;
                return View(await Db_Technology_Complex_Context.Motor.Where(x => x.Id_Equipment == Id_Equipment).OrderBy(x => x.Id).ToListAsync());

            }
            if (Id_Unit != 0)
            {
                ViewBag.Id = Id_Unit;
                return View(await Db_Technology_Complex_Context.Motor.Where(x => x.Id_Unit == Id_Unit).OrderBy(x => x.Id).ToListAsync());

            }
            return View();
        }

        public async Task<IActionResult> Index()
        {
            //await db.Area.ToListAsync()
            
          // string Tag = "C201_M01_Drive_On";
          //  DateTime TimeStart = new DateTime(2019, 7, 9,0,0,0)
          //  string timestart = TimeStart.ToString("yyyyMMdd");
            return View();

        }


        [HttpPost]
        public async Task<IActionResult> Motor_Working_hours(int Id_Motor, int Id_Equipment, string Name, string Tag)
        {
            ViewBag.Id_Equipment = Id_Equipment;
            ViewBag.Id_Motor = Id_Motor;
            ViewBag.Name = Name;
            ViewBag.Tag = Tag;
            ViewBag.TimeStart = DateTime.Today;
            ViewBag.TimeEnd = DateTime.Today;
            return View(await Db_HistoryValuesContext.HistoryValues.FromSql(" SELECT * FROM HistoryValues WHERE HistoryValues.TagName LIKE '%" + Tag + "%' AND HistoryValues.DateTime between Convert(date,GETDATE()) and  Convert(date,GETDATE()+1) ").ToListAsync());
        }

        public async Task<IActionResult> Automat_Kvadrablock_Working_hours()
        {

            return View(await Db_HistoryValuesContext.HistoryValues.Where(x => x.DateTime.Year == Today.Year && x.DateTime.Month == Today.Month && x.DateTime.Day == Today.Day && x.TagName == "Kvadrablock_Work_Hours").OrderBy(x => x.DateTime).ToListAsync());
        }

        public IActionResult Filling_automats()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Working_hours()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public RedirectResult Go_To_Filling_Automats()
        {
            return Redirect("/Home/Filling_automats");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
