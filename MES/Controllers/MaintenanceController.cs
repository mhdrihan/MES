using Microsoft.AspNetCore.Session;//for session
using Microsoft.AspNetCore.Http; //for session
using Microsoft.AspNetCore.Http.Extensions;
using MES.data;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Antiforgery;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using MES.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using DocumentFormat.OpenXml.Office2010.Excel;

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
            var stationCount = mesContext1.TableMasterStations.Count();
            ViewBag.StationCount = stationCount;
            var lineCount = mesContext1.TableMasterLines.Count();
            ViewBag.LineCount = lineCount;

            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
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
                    Id = masterstation.Id,
                    Station_ID = (int)masterstation.StationId,
                    Station_Suffix = (int)masterstation.StationSuffix,
                    Station_Name = masterstation.StationName,
                    Process_ID = (int)masterstation.ProcessId,
                    Line_ID = (int)masterstation.LineId,
                    Target_Output = (int)masterstation.TargetOutput,
                    Target_Yield = (int)masterstation.TargetYield,
                    Last_Modify = masterstation.LastModify,
                    Transact_By = masterstation.TransactBy,
                    Tester_Name = masterstation.TesterName,
                });
            }
            return View(masterStation);
        }

        [HttpGet]
        public IActionResult Savemasterstation()
        {
            masterstation masterStation = new masterstation();
            List<TableMasterLine> lineId = mesContext1.TableMasterLines.ToList();
            ViewBag.Line_Id = new SelectList(lineId, "LineId", "LineId");

            return View(masterStation);
        }

        [HttpPost]
        public IActionResult Savemasterstation(masterstation masterStation)
        {
            var data = new TableMasterStation()
            {
                Id = masterStation.Id,
                StationId = masterStation.Station_ID,
                StationSuffix = masterStation.Station_Suffix,
                StationName = masterStation.Station_Name,
                ProcessId = masterStation.Process_ID,
                LineId = masterStation.Line_ID,
                TargetOutput = masterStation.Target_Output,
                TargetYield = masterStation.Target_Yield,
                LastModify = masterStation.Last_Modify,
                TransactBy = masterStation.Transact_By,
                TesterName = masterStation.Tester_Name,
            };

            mesContext1.TableMasterStations.Add(data);
            mesContext1.SaveChanges(true);
            return RedirectToAction("masterstationM", "Maintenance");
        }
        [HttpGet]
        public IActionResult Updatemasterstation(int id = 0)
        {
            masterstation masterStation = new masterstation();
            var data = mesContext1.TableMasterStations.Where(m => m.Id == id).FirstOrDefault();
            List<TableMasterLine> lineId = mesContext1.TableMasterLines.ToList();
            ViewBag.Line_Id = new SelectList(lineId, "LineId", "LineId");

            if (data != null)
            {
                masterStation.Id = data.Id;
                masterStation.Station_ID = (int)data.StationId;
                masterStation.Station_Suffix = (int)data.StationSuffix;
                masterStation.Station_Name = data.StationName;
                masterStation.Process_ID = (int)data.ProcessId;
                masterStation.Line_ID = (int)data.LineId;
                masterStation.Target_Output = (int)data.TargetOutput;
                masterStation.Target_Yield = (int)data.TargetYield;
                masterStation.Last_Modify = data.LastModify;
                masterStation.Transact_By = data.TransactBy;
                masterStation.Tester_Name = data.TesterName;

            }
            return View(masterStation);
        }

        [HttpPost]
        public IActionResult Updatemasterstation(masterstation masterStation)
        {
            var data = mesContext1.TableMasterStations.Where(m => m.Id == masterStation.Id).FirstOrDefault();
            data.StationId = masterStation.Station_ID;
            data.StationSuffix = masterStation.Station_Suffix;
            data.StationName = masterStation.Station_Name;
            data.ProcessId = masterStation.Process_ID;
            data.LineId = masterStation.Line_ID;
            data.TargetOutput = masterStation.Target_Output;
            data.TargetYield = masterStation.Target_Yield;
            data.LastModify = masterStation.Last_Modify;
            data.TransactBy = masterStation.Transact_By;
            data.TesterName = masterStation.Tester_Name;

            mesContext1.SaveChanges();

            return RedirectToAction("masterstationM", "Maintenance");
        }

        [HttpGet]
        public IActionResult Deletemasterstation(int id)
        {
            var data = mesContext1.TableMasterStations.Where(m => m.Id == id).FirstOrDefault();
            if (data != null)
            {
                mesContext1.TableMasterStations.Remove(data);
                mesContext1.SaveChanges();
            }

            return RedirectToAction("masterstationM", "Maintenance");
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

        [HttpGet]
        public IActionResult Savemasterline()
        {
            masterline masterLine = new masterline();

            return View(masterLine);
        }

        [HttpPost]
        public IActionResult Savemasterline(masterline masterLine)
        {
            var data = new TableMasterLine()
            {
                LineId = masterLine.Line_ID,
                LineName = masterLine.Line_Name,
                LineLocation = masterLine.Line_Location,
                LineDescription = masterLine.Line_Description,
                LastModify = masterLine.Last_Modify,
                TransactBy = masterLine.Transact_By,
            };

            mesContext1.TableMasterLines.Add(data);
            mesContext1.SaveChanges();
            return RedirectToAction("masterlineM", "Maintenance");
        }
        [HttpGet]
        public IActionResult Updatemasterline(int LineId = 0)
        {
            masterline masterLine = new masterline();
            var data = mesContext1.TableMasterLines.Where(m => m.LineId == LineId).FirstOrDefault();

            if (data != null)
            {
                masterLine.Line_ID = data.LineId;
                masterLine.Line_Name = data.LineName;
                masterLine.Line_Location = data.LineLocation;
                masterLine.Line_Description = data.LineDescription;
                masterLine.Last_Modify = data.LastModify;
                masterLine.Transact_By = data.TransactBy;

            }
            return View(masterLine);
        }

        [HttpPost]
        public IActionResult Updatemasterline(masterline masterLine)
        {
            var data = mesContext1.TableMasterLines.Where(m => m.LineId == masterLine.Line_ID).FirstOrDefault();
            data.LineId = masterLine.Line_ID;
            data.LineName = masterLine.Line_Name;
            data.LineLocation = masterLine.Line_Location;
            data.LineDescription = masterLine.Line_Description;
            data.LastModify = masterLine.Last_Modify;
            data.TransactBy = masterLine.Transact_By;

            mesContext1.SaveChanges();

            return RedirectToAction("masterlineM", "Maintenance");
        }

        [HttpGet]
        public IActionResult Deletemasterline(int Lineid)
        {
            var data = mesContext1.TableMasterLines.Where(m => m.LineId == Lineid).FirstOrDefault();
            if (data != null)
            {
                mesContext1.TableMasterLines.Remove(data);
                mesContext1.SaveChanges();
            }

            return RedirectToAction("masterlineM", "Maintenance");
        }
    }
}
