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
            if (HttpContext.Session.GetInt32("User_Level") == 1 )
            {

                return RedirectToAction("Admin", "Admin");
            }
            else if (HttpContext.Session.GetInt32("User_Level") == 2 )
            {
                return RedirectToAction("Production", "Production");
            }
            else if (HttpContext.Session.GetInt32("User_Level") == 3)
            {
                return RedirectToAction("ProductEngineer", "Productengineer");
            }
            else if (HttpContext.Session.GetInt32("User_Level") == 4)
            {
                return RedirectToAction("MethodEngineer", "Methodengineer");
            }
            else if (HttpContext.Session.GetInt32("User_Level") == 5)
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
        public ActionResult Login(TableMasterUser tableMasterUser)
        {
            var data = mesContext1.TableMasterUsers.Where(m=> (m.Username == tableMasterUser.Username) && (m.Password == tableMasterUser.Password)).FirstOrDefault();

            if (data != null)
            {

                //Registrasi variabel session
                HttpContext.Session.SetInt32("ID_User", data.IdUser);
                HttpContext.Session.SetString("Username", value: data.Username);
                //HttpContext.Session.SetString("password", data.Password);
                HttpContext.Session.SetInt32("User_Level", (int)data.UserLevel);

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
        public IActionResult Logout()
        {
            return RedirectToAction("Index", "Home");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}