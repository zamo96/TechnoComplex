using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TechnologyComplex.Models;

namespace TechnologyComplex.Controllers
{
    public class HomeController : Controller
    {
        private const string V = "1";
        private Technology_Complex_Context Db_Technology_Complex_Context;
        private HistoryValuesContext Db_HistoryValuesContext;
        public DateTime Today = DateTime.Today;

      

        // Инициализируем связь с Базами
        public HomeController(Technology_Complex_Context technology_Complex_Context, HistoryValuesContext historyContext)
        {
            Db_Technology_Complex_Context = technology_Complex_Context;
            Db_HistoryValuesContext = historyContext;
        }


        // Возвращает кол-во наработанных часов для выбранный интервал времени
        [HttpPost]
         public async Task<IActionResult> Motor_Working_Hours_For_Date(string TimeStart,string TimeEnd, int Id_Motor, int Id_Equipment,int Id_Unit, string Name, string Tag)
         {
          
            ViewBag.Id_Equipment = Id_Equipment; 
            ViewBag.Id_Unit = Id_Unit;
            ViewBag.Id_Motor = Id_Motor;
            ViewBag.Name = Name; // Название мотора
            ViewBag.Tag = Tag; // Work_Hours / Drive_On


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
            ViewBag.Id_Facility = Id_Facility;
            ViewBag.Name_Facility = Name_Facility;
            return View(workFlows);
        }

        [HttpPost]
        public IActionResult Area(int Id_WorkFlow, string Name_WorkFlow, int Id_Facility, string Name_Facility)
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
            ViewBag.Id_WorkFlow = Id_WorkFlow;
            ViewBag.Name_WorkFlow = Name_WorkFlow;
            ViewBag.Id_Facility = Id_Facility;
            ViewBag.Name_Facility = Name_Facility;
            return View(areas);
        }

        [HttpPost]
        public IActionResult Unit(int Id_Area, string Name_Area,int Id_WorkFlow, string Name_WorkFlow, int Id_Facility, string Name_Facility)
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
            ViewBag.Id_WorkFlow = Id_WorkFlow;
            ViewBag.Name_WorkFlow = Name_WorkFlow;
            ViewBag.Id_Facility = Id_Facility;
            ViewBag.Name_Facility = Name_Facility;
            return View(units);
        }

        [HttpPost]
        public IActionResult Equipment(int Id_Unit, int Id_Area, string Name_Unit, string Name_Area, string format, int Id_WorkFlow, string Name_WorkFlow, int Id_Facility, string Name_Facility)
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
                ViewBag.Name_Area = Name_Area;
                ViewBag.Name_Unit = Name_Unit;
                ViewBag.Format = format;
                ViewBag.Id_WorkFlow = Id_WorkFlow;
                ViewBag.Name_WorkFlow = Name_WorkFlow;
                ViewBag.Id_Facility = Id_Facility;
                ViewBag.Name_Facility = Name_Facility;
                return View("Control_Module");
            }
              if (Equipments.Count == 0)
              {
                ViewBag.Name = Name_Unit;
                ViewBag.Name_Unit = Name_Unit;
                ViewBag.Id_Unit = Id_Unit;
                ViewBag.Id_Equipment = 0;
                ViewBag.Id_WorkFlow = Id_WorkFlow;
                ViewBag.Name_WorkFlow = Name_WorkFlow;
                ViewBag.Id_Facility = Id_Facility;
                ViewBag.Name_Facility = Name_Facility;
                ViewBag.Id_Area = Id_Area;
                ViewBag.Name_Area = Name_Area;
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
            ViewBag.Id_Unit = Id_Unit;
            ViewBag.Id_WorkFlow = Id_WorkFlow;
            ViewBag.Name_WorkFlow = Name_WorkFlow;
            ViewBag.Id_Facility = Id_Facility;
            ViewBag.Name_Facility = Name_Facility;
            ViewBag.Id_Area = Id_Area;
            ViewBag.Name_Area = Name_Area;
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
        public IActionResult Motor_Panel(int Id_Area, string Name_Unit, string Name_Area, string Name_Tanks, string format, int Id_WorkFlow, string Name_WorkFlow, int Id_Facility, string Name_Facility, int Id_Equipment, int Id_Unit, int Id_Motor, string Name, string Tag)
        {
            ViewBag.Name_Unit = Name_Unit;
            ViewBag.Id_WorkFlow = Id_WorkFlow;
            ViewBag.Name_WorkFlow = Name_WorkFlow;
            ViewBag.Id_Facility = Id_Facility;
            ViewBag.Name_Facility = Name_Facility;
            ViewBag.Id_Area = Id_Area;
            ViewBag.Name_Area = Name_Area;
            ViewBag.Name_Tanks = Name_Tanks;
            ViewBag.Format = format;

            ViewBag.Id_Equipment = Id_Equipment;
            ViewBag.Id_Unit = Id_Unit;
            ViewBag.Id_Motor = Id_Motor;
            ViewBag.Name = Name;
            ViewBag.Tag = Tag;
            return View();
        }

        [HttpGet]
        public IActionResult Motor_Panel(int Id_Area,int Id_Equipment, string Name_Unit, string Name_Tanks, string Name_Area, string format, int Id_WorkFlow, string Name_WorkFlow, int Id_Facility, string Name_Facility, int Id_Unit, int Id_Motor, string Name, string Tag)
        {
            ViewBag.Name_Unit = Name_Unit;
            ViewBag.Id_WorkFlow = Id_WorkFlow;
            ViewBag.Name_WorkFlow = Name_WorkFlow;
            ViewBag.Id_Facility = Id_Facility;
            ViewBag.Name_Facility = Name_Facility;
            ViewBag.Id_Area = Id_Area;
            ViewBag.Name_Area = Name_Area;
            ViewBag.Name_Tanks = Name_Tanks;
            ViewBag.Format = format;
            ViewBag.Id_Equipment = Id_Equipment;
            ViewBag.Id_Unit = Id_Unit;
            ViewBag.Id_Motor = Id_Motor;
            ViewBag.Name = Name;
            ViewBag.Tag = Tag;
            return View();
        }



        [HttpPost]                               
        public async Task<IActionResult> Motor(int Id_Equipment, int Id_Unit, int Id_Area, string Name, string format, string Name_Unit, string Name_Area, int Id_WorkFlow, string Name_WorkFlow, int Id_Facility, string Name_Facility)
        {

            ViewBag.Name = Name;
            ViewBag.Name_Unit = Name_Unit;
            ViewBag.Id_Unit = Id_Unit;
            ViewBag.Id_Equipment = Id_Equipment;
            ViewBag.Id_WorkFlow = Id_WorkFlow;
            ViewBag.Name_WorkFlow = Name_WorkFlow;
            ViewBag.Id_Facility = Id_Facility;
            ViewBag.Name_Facility = Name_Facility;
            ViewBag.Id_Area = Id_Area;
            ViewBag.Name_Area = Name_Area;
            ViewBag.Format = format;
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


        public async Task<ActionResult> AddMotor()
        {
           // List<Motor> motors = await Db_Technology_Complex_Context.Motor.ToListAsync();
           

            List<TechnologyComplex.Models.Area> areas = new List<TechnologyComplex.Models.Area>();
            foreach (var area in Db_Technology_Complex_Context.Area)
            {
                areas.Add(area);
            }
            ViewBag.Areas = new SelectList(areas, "Id", "Name");


            List<TechnologyComplex.Models.Equipment> equipments = new List<TechnologyComplex.Models.Equipment>();
            foreach (var equip in Db_Technology_Complex_Context.Equipment)
            {
                equipments.Add(equip);
            }
            ViewBag.Equipments = new SelectList(equipments, "Id", "Name");


            List<TechnologyComplex.Models.Unit> units = new List<TechnologyComplex.Models.Unit>();
            foreach (var unit in Db_Technology_Complex_Context.Unit)
            {
                units.Add(unit);
            }
            ViewBag.Units = new SelectList(units, "Id", "Name");


            return PartialView();
           
        }

        [HttpPost]
        public IActionResult AddMotor(Motor motor)
        {
         //   Db_Technology_Complex_Context.Entry(motor).State = EntityState.Modified;
            Db_Technology_Complex_Context.Motor.Add(motor);
            Db_Technology_Complex_Context.SaveChanges();
            return RedirectToAction("Facility");
          
        }

        [HttpGet]
        public async Task<ActionResult> Motor_Parametrs(int Id_Motor)
        {
            // List<Motor> motors = await Db_Technology_Complex_Context.Motor.ToListAsync();

            return View(Db_Technology_Complex_Context.Motor.Where(x => x.Id == Id_Motor).OrderBy(x => x.Id).FirstOrDefault());

        }

        [HttpPost]
        public IActionResult Motor_Parametrs(Motor motor)
        {
            Db_Technology_Complex_Context.Entry(motor).State = EntityState.Modified;
           // Db_Technology_Complex_Context.Motor.Add(motor);
            Db_Technology_Complex_Context.SaveChanges();
            return RedirectToAction("Facility");
        }

        [HttpPost]
        public async Task<ActionResult> Motor_Document(int Id_Motor)
        {
            // List<Motor> motors = await Db_Technology_Complex_Context.Motor.ToListAsync();

            return View(Db_Technology_Complex_Context.Motor_Documents.Where(x => x.Id_Motor == Id_Motor).OrderBy(x => x.Id_Motor).ToList());

        }
        [HttpGet]
        public async Task<IActionResult> Motor_MultyWorkingHours()
        {


            List<TechnologyComplex.Models.Model_For_Select2> select2s = new List<TechnologyComplex.Models.Model_For_Select2>();
            foreach (var motor in Db_Technology_Complex_Context.Motor)
                select2s.Add(new TechnologyComplex.Models.Model_For_Select2(motor.Id,motor.Name));
            ViewBag.TimeStart = DateTime.Today;
            ViewBag.TimeEnd = DateTime.Today;
            ViewBag.Select2 = JsonConvert.SerializeObject(select2s);
            ViewBag.YearStart = DateTime.Today;
            ViewBag.YearEnd = DateTime.Today;
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Motor_MultyWorkingHours(string TimeStart, string TimeEnd, string[] myselect, string MounthStart, string MounthEnd)
        {
            List<TechnologyComplex.Models.ForBarChart> forbarcharts = new List<TechnologyComplex.Models.ForBarChart>();
            List<TechnologyComplex.Models.HistoryValues> Work_Hours_For_Mounths_Temp = new List<TechnologyComplex.Models.HistoryValues>();
        //    double[] one = { 2, 23 };
        //   double[] two = { 4, 43 };
        //    forbarcharts.Add(new ForBarChart("one", one));
        //    forbarcharts.Add(new ForBarChart("two", two));

            List<TechnologyComplex.Models.Model_For_Select2> select2s = new List<TechnologyComplex.Models.Model_For_Select2>();
           foreach (var motor in Db_Technology_Complex_Context.Motor)
                select2s.Add(new TechnologyComplex.Models.Model_For_Select2(motor.Id, motor.Name));

            ViewBag.Select2 = JsonConvert.SerializeObject(select2s);
        
            ViewBag.PreSelect = JsonConvert.SerializeObject(myselect);

         //   ViewBag.ForBarChart = JsonConvert.SerializeObject(forbarcharts);

            double MotorHoursForDay = 0;

            List<TechnologyComplex.Models.Motor> motors = new List<TechnologyComplex.Models.Motor>();
            List<TechnologyComplex.Models.Model_For_BarChart> barcharts = new List<TechnologyComplex.Models.Model_For_BarChart>();
            foreach (var motor in myselect)
            {
                motors.Add(Db_Technology_Complex_Context.Motor.Where(x => x.Id == Convert.ToInt32(motor)).OrderBy(x => x.Id).FirstOrDefault());
               

            }

          


         
               
            if (TimeStart != null && TimeEnd != null)
            {
                DateTime DateTimeStart = DateTime.Parse(TimeStart);
                DateTime DateTimeEnd = DateTime.Parse(TimeEnd);

                    DateTime YearStart = DateTime.Parse(MounthStart);
                    DateTime YearEnd = DateTime.Parse(MounthEnd);
                    ViewBag.YearStart = YearStart;
                    ViewBag.YearEnd = YearEnd;

                ViewBag.TimeStart = DateTimeStart;
                ViewBag.TimeEnd = DateTimeEnd;
               
                int days = 1;
                DateTimeEnd += new TimeSpan(days, 0, 0, 0);

                string datetimestart = DateTimeStart.ToString("yyyyMMdd");
                string datetimeend = DateTimeEnd.ToString("yyyyMMdd");
               
                foreach (var motor in motors)
                {
                    double[] MotorHoursForMounth = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                    string workhours = motor.Name + "_Work_Hours";
                    List<TechnologyComplex.Models.HistoryValues> Work_Hours_For_Mounths = Db_HistoryValuesContext.HistoryValues.FromSql(" SELECT * FROM HistoryValues WHERE HistoryValues.TagName = '" + workhours + "' AND HistoryValues.DateTime > '" + MounthStart + "' AND HistoryValues.DateTime < '" + MounthEnd + "' ").ToList();
                    List<TechnologyComplex.Models.HistoryValues> Work_Hours =  Db_HistoryValuesContext.HistoryValues.FromSql(" SELECT * FROM HistoryValues WHERE HistoryValues.TagName LIKE '%" + motor.Name + "%' AND HistoryValues.DateTime >= '" + datetimestart + "' AND HistoryValues.DateTime <= '" + datetimeend + "' ").ToList();


                    Work_Hours = Work_Hours.Where(x => x.TagName.Contains("Work_Hours")).OrderBy(x => x.DateTime).ToList();
                   // Work_Hours_For_Mounths = Work_Hours_For_Mounths.Where(x => x.TagName.Contains("Work_Hours")).OrderBy(x => x.DateTime).ToList();
                     if (Work_Hours.Count() != 0 && Work_Hours.Last().Value != null && Work_Hours.First().Value != null)
                    {
                        MotorHoursForDay = (Convert.ToDouble(Work_Hours.Last().Value.ToString()) - Convert.ToDouble(Work_Hours.First().Value.ToString())) / 60;
                        for (int i = 0; i < 12; i++)
                        {
                            Work_Hours_For_Mounths_Temp = Work_Hours_For_Mounths.Where(x => x.DateTime.Month == i+1).OrderBy(x => x.DateTime).ToList();
                            if (Work_Hours_For_Mounths_Temp.Count > 0)
                            {
                                double last = Convert.ToDouble(Work_Hours_For_Mounths_Temp.Last().Value.ToString());
                                double first = Convert.ToDouble(Work_Hours_For_Mounths_Temp.First().Value.ToString());
                                MotorHoursForMounth[i] = (Convert.ToDouble(Work_Hours_For_Mounths_Temp.Last().Value.ToString()) - Convert.ToDouble(Work_Hours_For_Mounths_Temp.First().Value.ToString())) / 60;
                            }
                        }
                    }
                    barcharts.Add(new TechnologyComplex.Models.Model_For_BarChart(motor.Name, MotorHoursForDay));
                    forbarcharts.Add(new ForBarChart(motor.Name, MotorHoursForMounth));
                }
                ViewBag.BarCharts = JsonConvert.SerializeObject(barcharts);
                ViewBag.ForBarChart = JsonConvert.SerializeObject(forbarcharts);
                return View("Motor_MultyWorkingHours");

            }
            else
                ViewBag.BarCharts = JsonConvert.SerializeObject(barcharts);
            return View("Motor_MultyWorkingHours");
        }

    

        public ActionResult Submit(Motor motor)
        {
            return View("Index", motor);
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

        ///////////////////////////////////////////////////////////// PDF_VIEWER
        /// 
       
      

       
    }
}


