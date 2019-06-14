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
        private MotorContext Db_Motor_Context;
        private Technology_Complex_Context Db_Technology_Complex_Context;
        public DateTime Today = DateTime.Today;



        public HomeController(AreaContext context,MotorContext Motor_Context, Technology_Complex_Context technology_Complex_Context)
        {
            db = context;
            Db_Motor_Context = Motor_Context;
            Db_Technology_Complex_Context = technology_Complex_Context;
        }

        [HttpPost]
         public async Task<IActionResult> Motor_Working_Hours_For_Date(string date)
         {
            if (date != null)
            {
                DateTime dateTime = DateTime.Parse(date);
                return View("Automat_Kvadrablock_Working_hours", await Db_Motor_Context.Motor.Where(x => x.Date.Year == dateTime.Year && x.Date.Month == dateTime.Month && x.Date.Day == dateTime.Day).OrderBy(x => x.Date).ToListAsync());
            }
            else
                return View("Automat_Kvadrablock_Working_hours", await Db_Motor_Context.Motor.Where(x => x.Date.Year == Today.Year && x.Date.Month == Today.Month && x.Date.Day == Today.Day).OrderBy(x => x.Date).ToListAsync());
         }

        public async Task<IActionResult> Facility()
        {

            return View(await Db_Technology_Complex_Context.Facility.ToListAsync());
        }

        [HttpPost]
        public IActionResult Work_Flow(int id)
        {
            List<int> Work_Flows = new List<int>();
            foreach (var workflow in Db_Technology_Complex_Context.Facility_WorkFlow)
            {
                if (workflow.Id_Facility == id)
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
            return View(workFlows);
        }

        [HttpPost]
        public IActionResult Area(int id)
        {
            List<int> Areas = new List<int>();
            foreach (var area in Db_Technology_Complex_Context.WorkFlow_Area)
            {
                if (area.Id_WorkFlow == id)
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
            return View(areas);
        }

        public async Task<IActionResult> Index()
        {
            
            return View(await db.Area.ToListAsync());
        }

        public async Task<IActionResult> Automat_Kvadrablock_Working_hours()
        {

            return View(await Db_Motor_Context.Motor.Where(x => x.Date.Year == Today.Year && x.Date.Month == Today.Month && x.Date.Day == Today.Day && x.Name == "Kvadrablock_Work_Hours").OrderBy(x => x.Date).ToListAsync());
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
