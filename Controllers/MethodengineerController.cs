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
            return View();
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
    }
}
