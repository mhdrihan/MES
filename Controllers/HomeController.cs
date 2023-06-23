using MES.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Session;//for session
using Microsoft.AspNetCore.Http; //for session
using Microsoft.AspNetCore.Http.Extensions; //for session
using MES.data;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Antiforgery;

namespace MES.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private MesappContext mesContext1;  /*rubah dari MesappContext ke MesContext begitu sebalik nya*/

        public HomeController(ILogger<HomeController> logger, MesappContext mesContext)
        {
            _logger = logger;
            mesContext1 = mesContext;
        }
        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("role") =="admin")
            {

                return RedirectToAction("Admin", "Admin");
            }
            else if (HttpContext.Session.GetString("role") == "production")
            {
                return RedirectToAction("Production", "Production");
            }
            else if (HttpContext.Session.GetString("role") == "productengineer")
            {
                return RedirectToAction("ProductEngineer", "Productengineer");
            }
            else if (HttpContext.Session.GetString("role") == "methodengineer")
            {
                return RedirectToAction("MethodEngineer", "Methodengineer");
            }
            else if (HttpContext.Session.GetString("role") == "maintenance")
            {
                return RedirectToAction("Maintenance", "Maintenance");
            }
            else
            {
                return View();
                //return RedirectToAction("MenuAdminView", "Home");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User user)
        {
            var data = mesContext1.Users.Where(m=> (m.Username== user.Username) && (m.Password== user.Password)).FirstOrDefault();

            if (data != null)
            {

                //Registrasi variabel session
                HttpContext.Session.SetInt32("id", data.Id);
                HttpContext.Session.SetString("username", value: data.Username);
                //HttpContext.Session.SetString("password", data.Password);
                HttpContext.Session.SetString("role", data.Role);

                return RedirectToAction("Index", "Home");

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}