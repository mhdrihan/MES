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

namespace MES.Controllers
{
    public class ProductionController : Controller
    {
        private readonly ILogger<ProductionController> _logger;
        private MesappContext mesContext1;  /*rubah dari MesappContext ke MesContext begitu sebalik nya*/
        public ProductionController(ILogger<ProductionController> logger, MesappContext mesContext)
        {
            _logger = logger;
            mesContext1 = mesContext;
        }
        public ActionResult Production()
        {
            var workplanCount = mesContext1.TableMasterWorkplans.Count();
            ViewBag.WorkplanCount = workplanCount;
            var orderCount = mesContext1.TableMasterOrders.Count();
            ViewBag.OrderCount = orderCount;
            var refferenceCount = mesContext1.TableMasterRefferences.Count();
            ViewBag.RefferenceCount = refferenceCount;
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        //Production FITUR
        public IActionResult orderOP()
        {
            return View();
        }
        public IActionResult master_referenceOP()
        {
            MasterRefference masterRefference = new MasterRefference();
            masterRefference.RefferenceList = new List<master_refference>();
            var data = mesContext1.TableMasterRefferences.ToList();

            foreach (var Masterrefference in data)
            {
                masterRefference.RefferenceList.Add(new master_refference
                {
                    Refference_ID = Masterrefference.RefferenceId,
                    Refference_Name = Masterrefference.RefferenceName,
                    Refference_Description = Masterrefference.RefferenceDescription,
                    Last_Modify = (DateTime)Masterrefference.LastModify,
                    Transact_By = Masterrefference.TransactBy,
                });
            }
            return View(masterRefference);
        }

        [HttpGet]
        public IActionResult Savemasterreference()
        {
            master_refference masterRefference = new master_refference();

            return View(masterRefference);
            
        }

        [HttpPost]
        public IActionResult Savemasterreference(master_refference masterRefference)
        {
            var data = new TableMasterRefference()
            {
                RefferenceId = masterRefference.Refference_ID,
                RefferenceName = masterRefference.Refference_Name,
                RefferenceDescription = masterRefference.Refference_Description,
                LastModify = masterRefference.Last_Modify,
                TransactBy = masterRefference.Transact_By,
            };

            mesContext1.TableMasterRefferences.Add(data);
            mesContext1.SaveChanges();
            return RedirectToAction("master_referenceOP", "Production");
        }
        [HttpGet]
        public IActionResult Updatemasterreference(int RefferenceId = 0)
        {
            master_refference masterRefference = new master_refference();
            var data = mesContext1.TableMasterRefferences.Where(m => m.RefferenceId == RefferenceId).FirstOrDefault();

            if (data != null)
            {
                masterRefference.Refference_ID = data.RefferenceId;
                masterRefference.Refference_Name = data.RefferenceName;
                masterRefference.Refference_Description = data.RefferenceDescription;
                masterRefference.Last_Modify = (DateTime)data.LastModify;
                masterRefference.Transact_By = data.TransactBy;

            }
            return View(masterRefference);
        }

        [HttpPost]
        public IActionResult Updatemasterreference(master_refference masterRefference)
        {
            var data = mesContext1.TableMasterRefferences.Where(m => m.RefferenceId == masterRefference.Refference_ID).FirstOrDefault();
            data.RefferenceId = masterRefference.Refference_ID;
            data.RefferenceName = masterRefference.Refference_Name;
            data.RefferenceDescription = masterRefference.Refference_Description;
            data.LastModify = (DateTime)masterRefference.Last_Modify;
            data.TransactBy = masterRefference.Transact_By;

            mesContext1.SaveChanges();

            return RedirectToAction("master_referenceOP", "Production");
        }

        [HttpGet]
        public IActionResult Deletemasterreference(int Refferenceid = 0)
        {
            var data = mesContext1.TableMasterRefferences.Where(m => m.RefferenceId == Refferenceid).FirstOrDefault();
            if (data != null)
            {
                mesContext1.TableMasterRefferences.Remove(data);
                mesContext1.SaveChanges();
            }

            return RedirectToAction("master_referenceOP", "Production");
        }


        public IActionResult masterorderOP()
        {
            MasterOrder masterOrder = new MasterOrder();
            masterOrder.OrderList = new List<masterorder>();
            var data = mesContext1.TableMasterOrders.ToList();

            foreach (var Masterorder in data)
            {
                masterOrder.OrderList.Add(new masterorder
                {
                    Work_Order = Masterorder.WorkOrder,
                    Refference_Name = Masterorder.RefferenceName,
                    Work_Plan = Masterorder.WorkPlan,
                    Qty_Order = Masterorder.QtyOrder,
                    Qty_Launching = Masterorder.QtyLaunching,
                    Status_Order = Masterorder.StatusOrder,
                    Date_Order = Masterorder.DateOrder,
                    Date_Complete = Masterorder.DateComplete,
                    Transact_By = Masterorder.TransactBy,
                    Username = Masterorder.Username,
                    WO_Comment = Masterorder.WoComment,
                    Priority_WO = Masterorder.PriorityWo,
                    Station_ID = Masterorder.StationId,
                    Station_Suffix = Masterorder.StationSuffix,
                });
            }
            return View(masterOrder);
        }

        [HttpGet]
        public IActionResult Savemasterorder()
        {
            masterorder masterOrder = new masterorder();
            List<TableMasterStatusOrder> statusOrder = mesContext1.TableMasterStatusOrders.ToList();
            ViewBag.StatusOrder = new SelectList(statusOrder, "StatusOrder", "StatusOrder");

            //Dropdown List Station ID

            List<TableMasterStation> StationId = mesContext1.TableMasterStations.ToList();
            ViewBag.stationID = new SelectList(StationId, "StationId", "StationId");

            List<TableMasterRefference> refferenceName = mesContext1.TableMasterRefferences.ToList();
            ViewBag.RefferenceName = new SelectList(refferenceName, "RefferenceName", "RefferenceName");

            List<TableMasterWorkplan> workplan = mesContext1.TableMasterWorkplans.ToList();
            ViewBag.WorkPlan = new SelectList(workplan, "FlowId", "FlowId");

            return View(masterOrder);
        }

        [HttpPost]
        public IActionResult Savemasterorder(masterorder masterOrder)
        {
            var data = new TableMasterOrder()
            {
                WorkOrder = masterOrder.Work_Order,
                RefferenceName = masterOrder.Refference_Name,
                WorkPlan = masterOrder.Work_Plan,
                QtyOrder = (int)masterOrder.Qty_Order,
                QtyLaunching = masterOrder.Qty_Launching,
                StatusOrder = masterOrder.Status_Order,
                DateOrder = masterOrder.Date_Order,
                DateComplete = masterOrder.Date_Complete,
                TransactBy = masterOrder.Transact_By,
                Username = masterOrder.Username,
                WoComment = masterOrder.WO_Comment,
                PriorityWo = masterOrder.Priority_WO,
                StationId = masterOrder.Station_ID,
                StationSuffix = masterOrder.Station_Suffix,
            };

            mesContext1.TableMasterOrders.Add(data);
            mesContext1.SaveChanges();
            return RedirectToAction("masterorderOP", "Production");
        }
        [HttpGet]
        public IActionResult Updatemasterorder(String WorkOrder) /*Parameter */
        {
            masterorder masterOrder = new masterorder();
            var data = mesContext1.TableMasterOrders.Where(m => m.WorkOrder == WorkOrder).FirstOrDefault();
            List<TableMasterStatusOrder> statusOrder = mesContext1.TableMasterStatusOrders.ToList();
            ViewBag.StatusOrder = new SelectList(statusOrder, "StatusOrder", "StatusOrder");

            //Dropdown List Station ID

            List<TableMasterStation> StationId = mesContext1.TableMasterStations.ToList();
            ViewBag.stationID = new SelectList(StationId, "StationId", "StationId");

            List<TableMasterRefference> refferenceName = mesContext1.TableMasterRefferences.ToList();
            ViewBag.RefferenceName = new SelectList(refferenceName, "RefferenceName", "RefferenceName");

            List<TableMasterWorkplan> workplan = mesContext1.TableMasterWorkplans.ToList();
            ViewBag.WorkPlan = new SelectList(workplan, "FlowId", "FlowId");

            if (data != null)
            {
                masterOrder.Work_Order = data.WorkOrder;
                masterOrder.Refference_Name = data.RefferenceName;
                masterOrder.Work_Plan = data.WorkPlan;
                masterOrder.Qty_Order = data.QtyOrder;
                masterOrder.Qty_Launching = data.QtyLaunching;
                masterOrder.Status_Order = data.StatusOrder;
                masterOrder.Date_Order = data.DateOrder;
                masterOrder.Date_Complete = data.DateComplete;
                masterOrder.Transact_By = data.TransactBy;
                masterOrder.Username = data.Username;
                masterOrder.WO_Comment = data.WoComment;
                masterOrder.Priority_WO = data.PriorityWo;
                masterOrder.Station_ID = data.StationId;
                masterOrder.Station_Suffix = data.StationSuffix;

            }
            return View(masterOrder);
        }

        [HttpPost]
        public IActionResult Updatemasterorder(masterorder masterOrder)
        {
            var data = mesContext1.TableMasterOrders.Where(m => m.WorkOrder == masterOrder.Work_Order).FirstOrDefault();
            data.WorkOrder = masterOrder.Work_Order;
            data.RefferenceName = masterOrder.Refference_Name;
            data.WorkPlan = masterOrder.Work_Plan;
            data.QtyOrder = (int)masterOrder.Qty_Order;
            data.QtyLaunching = masterOrder.Qty_Launching;
            data.StatusOrder = masterOrder.Status_Order;
            data.DateOrder = masterOrder.Date_Order;
            data.DateComplete = masterOrder.Date_Complete;
            data.TransactBy = masterOrder.Transact_By;
            data.Username = masterOrder.Username;
            data.WoComment = masterOrder.WO_Comment;
            data.PriorityWo = masterOrder.Priority_WO;
            data.StationId = masterOrder.Station_ID;
            data.StationSuffix = masterOrder.Station_Suffix;



            mesContext1.SaveChanges();

            return RedirectToAction("masterorderOP", "Production");
        }

        [HttpGet]
        public IActionResult Deletemasterorder(string Workorder = "0")
        {
            var data = mesContext1.TableMasterOrders.FirstOrDefault(m => m.WorkOrder == Workorder);
            if (data != null)
            {
                mesContext1.TableMasterOrders.Remove(data);
                mesContext1.SaveChanges();
            }

            return RedirectToAction("masterorderOP", "Production");
        }

        public IActionResult master_workplan()
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
            return RedirectToAction("master_workplan", "Production");
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

            return RedirectToAction("master_workplan", "Production");
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

            return RedirectToAction("master_workplan", "Production");
        }
    }
}
