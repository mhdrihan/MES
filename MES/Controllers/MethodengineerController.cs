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
    public class MethodengineerController : Controller
    {
        private readonly ILogger<MaintenanceController> _logger;
        private MesappContext mesContext1;  /*rubah dari MesappContext ke MesContext begitu sebalik nya*/
        public MethodengineerController(ILogger<MaintenanceController> logger, MesappContext mesContext)
        {
            _logger = logger;
            mesContext1 = mesContext;
        }

        public IActionResult Methodengineer()
        {
            var stationCount = mesContext1.TableMasterStations.Count();
            ViewBag.StationCount = stationCount;
            var workplanCount = mesContext1.TableMasterWorkplans.Count();
            ViewBag.WorkplanCount = workplanCount;
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        //ME FITUR
        public IActionResult workplanME()
        {
            return View();
        }
        public IActionResult masterstationME()
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
            mesContext1.SaveChanges();
            return RedirectToAction("masterstationME", "Methodengineer");
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

            return RedirectToAction("masterstationME", "Methodengineer");
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

            return RedirectToAction("masterstationME", "Methodengineer");
        }
        public IActionResult masterworkplan()
        {
            WorkPlan workPlan = new WorkPlan();
            workPlan.WorkplanList = new List<workplan>();
            var data = mesContext1.TableMasterWorkplans.ToList();

            foreach (var workplan in data)
            {
                workPlan.WorkplanList.Add(new workplan
                {
                    Flow_ID = workplan.FlowId,
                    Flow_Name = workplan.FlowName,
                    Refference_Name = workplan.RefferenceName,
                    Line_ID = workplan.LineId,
                    Valid_Status = workplan.ValidStatus,
                    Revision_Number = workplan.RevisionNumber,
                    Last_Modify = workplan.LastModify,
                    Transact_By = workplan.TransactBy,
                    Flow_Description = workplan.FlowDescription,
                    Process_Qty = workplan.ProcessQty,
                    Process_1 = workplan.Process1,
                    Process_2 = workplan.Process2,
                    Process_3 = workplan.Process3,
                    Process_4 = workplan.Process4,
                    Process_5 = workplan.Process5,
                    Process_6 = workplan.Process6,
                    Process_7 = workplan.Process7,
                    Process_8 = workplan.Process8,
                    Process_9 = workplan.Process9,
                    Process_10 = workplan.Process10,
                    Process_11 = workplan.Process11,
                    Process_12 = workplan.Process12,
                    Process_13 = workplan.Process13,
                    Process_14 = workplan.Process14,
                    Process_15 = workplan.Process15,
                    Process_16 = workplan.Process16,
                    Process_17 = workplan.Process17,
                    Process_18 = workplan.Process18,
                    Process_19 = workplan.Process19,
                    Process_20 = workplan.Process20,
                    Process_21 = workplan.Process21,
                    Process_22 = workplan.Process22,
                    Process_23 = workplan.Process23,
                    Process_24 = workplan.Process24,
                    Process_25 = workplan.Process25,
                    Process_26 = workplan.Process26,
                    Process_27 = workplan.Process27,
                    Process_28 = workplan.Process28,
                    Process_29 = workplan.Process29,
                    Process_30 = workplan.Process30,
                    Process_31 = workplan.Process31,
                    Process_32 = workplan.Process32,
                    Process_33 = workplan.Process33,
                    Process_34 = workplan.Process34,
                    Process_35 = workplan.Process35,
                    Process_36 = workplan.Process36,
                    Process_37 = workplan.Process37,
                    Process_38 = workplan.Process38,
                    Process_39 = workplan.Process39,
                    Process_40 = workplan.Process40,
                    Process_41 = workplan.Process41,
                    Process_42 = workplan.Process42,
                    Process_43 = workplan.Process43,
                    Process_44 = workplan.Process44,
                    Process_45 = workplan.Process45,
                    Process_46 = workplan.Process46,
                    Process_47 = workplan.Process47,
                    Process_48 = workplan.Process48,
                    Process_49 = workplan.Process49,
                    Process_50 = workplan.Process50,
                });
            }
            return View(workPlan);
        }

        [HttpGet]
        public IActionResult Savemasterworkplan()
        {
            workplan workPlan = new workplan();
            List<TableMasterLine> lineId = mesContext1.TableMasterLines.ToList();
            ViewBag.Line_Id = new SelectList(lineId, "LineId", "LineId");
            List<TableMasterStation> StationId = mesContext1.TableMasterStations.ToList();
            ViewBag.StationID = new SelectList(StationId, "StationId", "StationId");

            return View(workPlan);

        }

        [HttpPost]
        public IActionResult Savemasterworkplan(workplan workPlan)
        {
            var data = new TableMasterWorkplan()
            {
                FlowId = workPlan.Flow_ID,
                FlowName = workPlan.Flow_Name,
                RefferenceName = workPlan.Refference_Name,
                LineId = workPlan.Line_ID,
                ValidStatus = workPlan.Valid_Status,
                RevisionNumber = workPlan.Revision_Number,
                LastModify = workPlan.Last_Modify,
                TransactBy = workPlan.Transact_By,
                FlowDescription = workPlan.Flow_Description,
                ProcessQty = workPlan.Process_Qty,
                Process1 = workPlan.Process_1,
                Process2 = workPlan.Process_2,
                Process3 = workPlan.Process_3,
                Process4 = workPlan.Process_4,
                Process5 = workPlan.Process_5,
                Process6 = workPlan.Process_6,
                Process7 = workPlan.Process_7,
                Process8 = workPlan.Process_8,
                Process9 = workPlan.Process_9,
                Process10 = workPlan.Process_10,
                Process11 = workPlan.Process_11,
                Process12 = workPlan.Process_12,
                Process13 = workPlan.Process_13,
                Process14 = workPlan.Process_14,
                Process15 = workPlan.Process_15,
                Process16 = workPlan.Process_16,
                Process17 = workPlan.Process_17,
                Process18 = workPlan.Process_18,
                Process19 = workPlan.Process_19,
                Process20 = workPlan.Process_20,
                Process21 = workPlan.Process_21,
                Process22 = workPlan.Process_22,
                Process23 = workPlan.Process_23,
                Process24 = workPlan.Process_24,
                Process25 = workPlan.Process_25,
                Process26 = workPlan.Process_26,
                Process27 = workPlan.Process_27,
                Process28 = workPlan.Process_28,
                Process29 = workPlan.Process_29,
                Process30 = workPlan.Process_30,
                Process31 = workPlan.Process_31,
                Process32 = workPlan.Process_32,
                Process33 = workPlan.Process_33,
                Process34 = workPlan.Process_34,
                Process35 = workPlan.Process_35,
                Process36 = workPlan.Process_36,
                Process37 = workPlan.Process_37,
                Process38 = workPlan.Process_38,
                Process39 = workPlan.Process_39,
                Process40 = workPlan.Process_40,
                Process41 = workPlan.Process_41,
                Process42 = workPlan.Process_42,
                Process43 = workPlan.Process_43,
                Process44 = workPlan.Process_44,
                Process45 = workPlan.Process_45,
                Process46 = workPlan.Process_46,
                Process47 = workPlan.Process_47,
                Process48 = workPlan.Process_48,
                Process49 = workPlan.Process_49,
                Process50 = workPlan.Process_50,
            };

            mesContext1.TableMasterWorkplans.Add(data);
            mesContext1.SaveChanges();
            return RedirectToAction("masterworkplan", "Methodengineer");
        }
        [HttpGet]
        public IActionResult Updatemasterworkplan(int FlowId = 0)
        {
            workplan workPlan = new workplan();
            var data = mesContext1.TableMasterWorkplans.Where(m => m.FlowId == FlowId).FirstOrDefault();
            List<TableMasterLine> lineId = mesContext1.TableMasterLines.ToList();
            ViewBag.Line_Id = new SelectList(lineId, "LineId", "LineId");
            List<TableMasterStation> StationId = mesContext1.TableMasterStations.ToList();
            ViewBag.StationID = new SelectList(StationId, "StationId", "StationId");

            if (data != null)
            {
                workPlan.Flow_ID = data.FlowId;
                workPlan.Flow_Name = data.FlowName;
                workPlan.Refference_Name = data.RefferenceName;
                workPlan.Line_ID = data.LineId;
                workPlan.Valid_Status = data.ValidStatus;
                workPlan.Revision_Number = data.RevisionNumber;
                workPlan.Last_Modify = data.LastModify;
                workPlan.Transact_By = data.TransactBy;
                workPlan.Flow_Description = data.FlowDescription;
                workPlan.Process_Qty = data.ProcessQty;
                workPlan.Process_1 = data.Process1;
                workPlan.Process_2 = data.Process2;
                workPlan.Process_3 = data.Process3;
                workPlan.Process_4 = data.Process4;
                workPlan.Process_5 = data.Process5;
                workPlan.Process_6 = data.Process6;
                workPlan.Process_7 = data.Process7;
                workPlan.Process_8 = data.Process8;
                workPlan.Process_9 = data.Process9;
                workPlan.Process_10 = data.Process10;
                workPlan.Process_11 = data.Process11;
                workPlan.Process_12 = data.Process12;
                workPlan.Process_13 = data.Process13;
                workPlan.Process_14 = data.Process14;
                workPlan.Process_15 = data.Process15;
                workPlan.Process_16 = data.Process16;
                workPlan.Process_17 = data.Process17;
                workPlan.Process_18 = data.Process18;
                workPlan.Process_19 = data.Process19;
                workPlan.Process_20 = data.Process20;
                workPlan.Process_21 = data.Process21;
                workPlan.Process_22 = data.Process22;
                workPlan.Process_23 = data.Process23;
                workPlan.Process_24 = data.Process24;
                workPlan.Process_25 = data.Process25;
                workPlan.Process_26 = data.Process26;
                workPlan.Process_27 = data.Process27;
                workPlan.Process_28 = data.Process28;
                workPlan.Process_29 = data.Process29;
                workPlan.Process_30 = data.Process30;
                workPlan.Process_31 = data.Process31;
                workPlan.Process_32 = data.Process32;
                workPlan.Process_33 = data.Process33;
                workPlan.Process_34 = data.Process34;
                workPlan.Process_35 = data.Process35;
                workPlan.Process_36 = data.Process36;
                workPlan.Process_37 = data.Process37;
                workPlan.Process_38 = data.Process38;
                workPlan.Process_39 = data.Process39;
                workPlan.Process_40 = data.Process40;
                workPlan.Process_41 = data.Process41;
                workPlan.Process_42 = data.Process42;
                workPlan.Process_43 = data.Process43;
                workPlan.Process_44 = data.Process44;
                workPlan.Process_45 = data.Process45;
                workPlan.Process_46 = data.Process46;
                workPlan.Process_47 = data.Process47;
                workPlan.Process_48 = data.Process48;
                workPlan.Process_49 = data.Process49;
                workPlan.Process_50 = data.Process50;
            }
            return View(workPlan);
        }

        [HttpPost]
        public IActionResult Updatemasterworkplan(workplan workPlan)
        {
            var data = mesContext1.TableMasterWorkplans.Where(m => m.FlowId == workPlan.Flow_ID).FirstOrDefault();
            data.FlowId = workPlan.Flow_ID;
            data.FlowName = workPlan.Flow_Name;
            data.RefferenceName = workPlan.Refference_Name;
            data.LineId = workPlan.Line_ID;
            data.ValidStatus = workPlan.Valid_Status;
            data.RevisionNumber = workPlan.Revision_Number;
            data.LastModify = workPlan.Last_Modify;
            data.TransactBy = workPlan.Transact_By;
            data.FlowDescription = workPlan.Flow_Description;
            data.ProcessQty = workPlan.Process_Qty;
            data.Process1 = workPlan.Process_1;
            data.Process2 = workPlan.Process_2;
            data.Process3 = workPlan.Process_3;
            data.Process4 = workPlan.Process_4;
            data.Process5 = workPlan.Process_5;
            data.Process6 = workPlan.Process_6;
            data.Process7 = workPlan.Process_7;
            data.Process8 = workPlan.Process_8;
            data.Process9 = workPlan.Process_9;
            data.Process10 = workPlan.Process_10;
            data.Process11 = workPlan.Process_11;
            data.Process12 = workPlan.Process_12;
            data.Process13 = workPlan.Process_13;
            data.Process14 = workPlan.Process_14;
            data.Process15 = workPlan.Process_15;
            data.Process16 = workPlan.Process_16;
            data.Process17 = workPlan.Process_17;
            data.Process18 = workPlan.Process_18;
            data.Process19 = workPlan.Process_19;
            data.Process20 = workPlan.Process_20;
            data.Process21 = workPlan.Process_21;
            data.Process22 = workPlan.Process_22;
            data.Process23 = workPlan.Process_23;
            data.Process24 = workPlan.Process_24;
            data.Process25 = workPlan.Process_25;
            data.Process26 = workPlan.Process_26;
            data.Process27 = workPlan.Process_27;
            data.Process28 = workPlan.Process_28;
            data.Process29 = workPlan.Process_29;
            data.Process30 = workPlan.Process_30;
            data.Process31 = workPlan.Process_31;
            data.Process32 = workPlan.Process_32;
            data.Process33 = workPlan.Process_33;
            data.Process34 = workPlan.Process_34;
            data.Process35 = workPlan.Process_35;
            data.Process36 = workPlan.Process_36;
            data.Process37 = workPlan.Process_37;
            data.Process38 = workPlan.Process_38;
            data.Process39 = workPlan.Process_39;
            data.Process40 = workPlan.Process_40;
            data.Process41 = workPlan.Process_41;
            data.Process42 = workPlan.Process_42;
            data.Process43 = workPlan.Process_43;
            data.Process44 = workPlan.Process_44;
            data.Process45 = workPlan.Process_45;
            data.Process46 = workPlan.Process_46;
            data.Process47 = workPlan.Process_47;
            data.Process48 = workPlan.Process_48;
            data.Process49 = workPlan.Process_49;
            data.Process50 = workPlan.Process_50;


            mesContext1.SaveChanges();

            return RedirectToAction("masterworkplan", "Methodengineer");
        }

        [HttpGet]
        public IActionResult Deletemasterworkplan(int Flowid = 0)
        {
            var data = mesContext1.TableMasterWorkplans.Where(m => m.FlowId == Flowid).FirstOrDefault();
            if (data != null)
            {
                mesContext1.TableMasterWorkplans.Remove(data);
                mesContext1.SaveChanges();
            }

            return RedirectToAction("masterworkplan", "Methodengineer");
        }
    }
}
