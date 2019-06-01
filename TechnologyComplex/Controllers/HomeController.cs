using System;
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
        public DateTime Today = DateTime.Today;


        public HomeController(AreaContext context,MotorContext Motor_Context)
        {
            db = context;
            Db_Motor_Context = Motor_Context;
        }
        [HttpPost]
         public async Task<IActionResult> Motor_Working_Hours_For_Date(string date)
         {
            DateTime dateTime = DateTime.Parse(date);
            dateTime = new DateTime(2019 - 31 - 05);
            return View("Automat_Kvadrablock_Working_hours", await Db_Motor_Context.Motor.Where(x => x.Date.Year == dateTime.Year && x.Date.Month == dateTime.Month && x.Date.Day == dateTime.Day && x.Name == "Kvadrablock_Work_Hours").OrderBy(x => x.Date).ToListAsync());
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
