using MES.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Session;//for session
using Microsoft.AspNetCore.Http; //for session
using Microsoft.AspNetCore.Http.Extensions; //for session
using MES.data;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Antiforgery;
using System.Data;

namespace MES.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private MesappContext mesContext1;  /*rubah dari MesappContext ke MesContext begitu sebalik nya*/
        public AdminController(ILogger<AdminController> logger, MesappContext mesContext)
        {
            _logger = logger;
            mesContext1 = mesContext;
        }
        public ActionResult Admin()
        {
            return View();
        }
        public IActionResult user()
        {
            UserModel userModel = new UserModel();
            userModel.UserList = new List<user>();
            var data = mesContext1.Users.ToList();

            foreach (var user in data) 
            {
                userModel.UserList.Add(new user
                {
                    id= user.Id,
                    username = user.Username,
                    password = user.Password,
                    role = user.Role
                });
            }
            return View(userModel);
        }

        [HttpGet]
        public IActionResult SaveaccountMES()
        {
            masteruser masterUser = new masteruser();

            return View(masterUser);
        }

        [HttpPost]
        public IActionResult SaveaccountMES(masteruser masterUser)
        {
            var data = new TableMasterUser()
            {
                IdUser = masterUser.ID_User,
                Username = masterUser.Username,
                Password = masterUser.Password,
                StationId = masterUser.Station_ID,
                UserLevel = masterUser.User_Level
            };

            mesContext1.TableMasterUsers.Add(data);
            mesContext1.SaveChanges();
            return RedirectToAction("accountMES", "Admin");
        }
        [HttpGet]
        public IActionResult UpdateaccountMES(int Iduser = 0)
        {
            masteruser masterUser = new masteruser();
            var data = mesContext1.TableMasterUsers.Where(m => m.IdUser == Iduser).FirstOrDefault();

            if (data != null)
            {
                masterUser.ID_User = data.IdUser;
                masterUser.Username = data.Username;
                masterUser.Password = data.Password;
                masterUser.Station_ID = (int)data.StationId;
                masterUser.User_Level = (int)data.UserLevel;
            }
            return View(masterUser);
        }

        [HttpPost]
        public IActionResult UpdateaccountMES(masteruser masterUser)
        {
            var data = mesContext1.TableMasterUsers.Where(m => m.IdUser == masterUser.ID_User).FirstOrDefault();
            data.IdUser = masterUser.ID_User;
            data.Username = masterUser.Username;
            data.Password = masterUser.Password;
            data.StationId = masterUser.Station_ID;
            data.UserLevel = masterUser.User_Level;

            mesContext1.SaveChanges();

            return RedirectToAction("accountMES", "Admin");
        }

        [HttpGet]
        public IActionResult DeleteaccountMES(int Iduser = 0)
        {
            var data = mesContext1.TableMasterUsers.Where(m => m.IdUser == Iduser).FirstOrDefault();
            if (data != null)
            {
                mesContext1.TableMasterUsers.Remove(data);
                mesContext1.SaveChanges();
            }

            return RedirectToAction("accountMES", "Admin");
            
        }
        public IActionResult accountMES()
        {
            MasterUser masterUser = new MasterUser();
            masterUser.MasterList = new List<masteruser>();
            var data = mesContext1.TableMasterUsers.ToList();

            foreach (var masteruser in data)
            {
                masterUser.MasterList.Add(new masteruser
                {
                    ID_User = masteruser.IdUser,
                    Username = masteruser.Username,
                    Password = masteruser.Password,
                    Station_ID = (int)masteruser.StationId,
                    User_Level = (int)masteruser.UserLevel,
                });
            }
            return View(masterUser);
        }
        /* Machine controller*/
        public IActionResult machine()
        {
            return View();
        }
        public IActionResult masterline()
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
        /* part Master*/
        public IActionResult partmaster()
        {
            return View();
        }
        /*workplan cntroller*/
        public IActionResult workplan()
        {
            return View();
        }
        public IActionResult masterstation() 
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
        /*Order Controller*/
        public IActionResult order()
        {
            return View();
        }
        public IActionResult masterorder()
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
        public IActionResult master_reference()
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
        /*BOM Controller*/
        public IActionResult bom()
        {
            BomEditorModel bomEditor = new BomEditorModel();
            bomEditor.BomList = new List<bomeditor>();
            var data = mesContext1.TableMasterMaterials.ToList();

            foreach (var bom in data)
            {
                bomEditor.BomList.Add(new bomeditor
                {
                    Material_Id= bom.MaterialId,
                    Material_Name= bom.MaterialName,
                    Refference_Name= bom.RefferenceName,
                    Station_Id= bom.StationId,
                    Revision_Number = (int)bom.RevisionNumber,
                    Last_Modify= bom.LastModify,
                    Transact_By= bom.TransactBy,
                    Part_Qty= bom.PartQty,
                    Part_1= bom.Part1,
                    Part_2= bom.Part2,
                    Part_3= bom.Part3,
                    Part_4= bom.Part4,
                    Part_5= bom.Part5,
                    Part_6= bom.Part6,
                    Part_7 = bom.Part7,
                    Part_8 = bom.Part8,
                    Part_9= bom.Part9,
                    Part_10= bom.Part10,
                    Part_11 = bom.Part11,
                    Part_12 = bom.Part12,
                    Part_13 = bom.Part13,
                    Part_14 = bom.Part14,
                    Part_15 = bom.Part15,
                    Part_16 = bom.Part16,
                    Part_17 = bom.Part17,
                    Part_18 = bom.Part18,
                    Part_19 = bom.Part19,
                    Part_20 = bom.Part20,
                    Part_21 = bom.Part21,
                    Part_22 = bom.Part22,
                    Part_23 = bom.Part23,
                    Part_24 = bom.Part24,
                    Part_25 = bom.Part25,
                    Part_26 = bom.Part26,
                    Part_27 = bom.Part27,
                    Part_28 = bom.Part28,
                    Part_29 = bom.Part29,
                    Part_30 = bom.Part30,
                    Part_31 = bom.Part31,
                    Part_32 = bom.Part32,
                    Part_33 = bom.Part33,
                    Part_34 = bom.Part34,
                    Part_35 = bom.Part35,
                    Part_36 = bom.Part36,
                    Part_37 = bom.Part37,
                    Part_38 = bom.Part38,
                    Part_39 = bom.Part39,
                    Part_40 = bom.Part40,
                    Part_41 = bom.Part41,
                    Part_42 = bom.Part42,
                    Part_43 = bom.Part43,
                    Part_44 = bom.Part44,
                    Part_45 = bom.Part45,
                    Part_46 = bom.Part46,
                    Part_47 = bom.Part47,
                    Part_48 = bom.Part48,
                    Part_49 = bom.Part49,
                    Part_50 = bom.Part50,
                    
                });
            }
            return View(bomEditor);
        }
    }
}
