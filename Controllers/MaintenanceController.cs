using Microsoft.AspNetCore.Session;//for session
using Microsoft.AspNetCore.Http; //for session
using Microsoft.AspNetCore.Http.Extensions;
using MES.data;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Antiforgery;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using MES.Models;

namespace MES.Controllers
{
    public class MaintenanceController : Controller
    {
        private readonly ILogger<MaintenanceController> _logger;
        private MesappContext mesContext1;  /*rubah dari MesappContext ke MesContext begitu sebalik nya*/
        public MaintenanceController(ILogger<MaintenanceController> logger, MesappContext mesContext)
        {
            _logger = logger;
            mesContext1 = mesContext;
        }
        public IActionResult Maintenance()
        {
            return View();
        }
        //Maintenance FITUR
        public IActionResult machineM()
        {
            return View();
        }
        public IActionResult masterstationM()
        {
            MasterStation masterStation = new MasterStation();
            masterStation.StationList = new List<masterstation>();
            var data = mesContext1.TableMasterStations.ToList();

            foreach (var masterstation in data)
            {
                masterStation.StationList.Add(new masterstation
                {
                    Station_ID = masterstation.StationId,
                    Station_Suffix = masterstation.StationSuffix,
                    Station_Name = masterstation.StationName,
                    Process_ID = masterstation.ProcessId,
                    Line_ID = masterstation.LineId,
                    Target_Output = masterstation.TargetOutput,
                    Target_Yield = masterstation.TargetYield,
                    Last_Modify = masterstation.LastModify,
                    Transact_By = masterstation.TransactBy,
                    Tester_Name = masterstation.TesterName,
                });
            }
            return View(masterStation);
        }
        public IActionResult masterlineM()
        {
            MasterLine masterLine = new MasterLine();
            masterLine.LineList = new List<masterline>();
            var data = mesContext1.TableMasterLines.ToList();

            foreach (var masterline in data)
            {
                masterLine.LineList.Add(new masterline
                {
                    Line_ID = masterline.LineId,
                    Line_Name = masterline.LineName,
                    Line_Location = masterline.LineLocation,
                    Line_Description = masterline.LineDescription,
                    Last_Modify = masterline.LastModify,
                    Transact_By = masterline.TransactBy,
                });
            }

            return View(masterLine);
        }
    }
}
