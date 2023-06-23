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
using DocumentFormat.OpenXml.Office2013.Drawing.Chart;
using Microsoft.AspNetCore.Mvc.Rendering;
using DocumentFormat.OpenXml.Office2010.Excel;

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
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Admin()
        {
            var userCount = mesContext1.Users.Count();
            ViewBag.UserCount = userCount;
            var accountCount = mesContext1.TableMasterUsers.Count();
            ViewBag.DataCount = accountCount;
            var stationCount = mesContext1.TableMasterStations.Count();
            ViewBag.StationCount = stationCount;
            var lineCount = mesContext1.TableMasterLines.Count();
            ViewBag.LineCount = lineCount;
            var partCount = mesContext1.TableMasterParts.Count();
            ViewBag.PartCount = partCount;
            var workplanCount = mesContext1.TableMasterWorkplans.Count();
            ViewBag.WorkplanCount = workplanCount;
            var orderCount = mesContext1.TableMasterOrders.Count();
            ViewBag.OrderCount = orderCount;
            var refferenceCount = mesContext1.TableMasterRefferences.Count();
            ViewBag.RefferenceCount = refferenceCount;
            var bomCount = mesContext1.TableMasterMaterials.Count();
            ViewBag.BomCount = bomCount;
            var serialCount = mesContext1.TableMasterSerialCounters.Count();
            ViewBag.SerialCount = serialCount;
            var traceabilityCount = mesContext1.TableRunTraceabilities.Count();
            ViewBag.TraceabilityCount = traceabilityCount;
            
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
                    id = user.Id,
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
            List<TableMasterStation> StationId = mesContext1.TableMasterStations.ToList();
            ViewBag.stationID = new SelectList(StationId, "StationId", "StationId");
            List<User> User = mesContext1.Users.ToList();
            ViewBag.userLevel = new SelectList(User, "Id", "Id");

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
            List<TableMasterStation> StationId = mesContext1.TableMasterStations.ToList();
            ViewBag.stationID = new SelectList(StationId, "StationId", "StationId");

            List<User> User = mesContext1.Users.ToList();
            ViewBag.userLevel = new SelectList(User, "Id", "Id");

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
            return RedirectToAction("masterline", "Admin");
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

            return RedirectToAction("masterline", "Admin");
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

            return RedirectToAction("masterline", "Admin");
        }


        /* part Master*/
        public IActionResult master_part()
        {
            MasterPart masterPart = new MasterPart();
            masterPart.PartList = new List<masterpart>();
            var data = mesContext1.TableMasterParts.ToList();

            foreach (var Masterpart in data)
            {
                masterPart.PartList.Add(new masterpart
                {
                    Material_ID = Masterpart.MaterialId,
                    Material_Name = Masterpart.MaterialName,
                    Vendor = Masterpart.Vendor,
                });
            }

            return View(masterPart);
        }


        [HttpGet]
        public IActionResult Savemasterpart()
        {
            masterpart masterPart = new masterpart();
            List<TableMasterMaterial> MaterialId = mesContext1.TableMasterMaterials.ToList();
            ViewBag.MaterialID = new SelectList(MaterialId, "MaterialName", "MaterialName");

            return View(masterPart);
        }

        [HttpPost]
        public IActionResult Savemasterpart(masterpart masterPart)
        {
            var data = new TableMasterPart()
            {
                MaterialId = masterPart.Material_ID,
                MaterialName = masterPart.Material_Name,
                Vendor = masterPart.Vendor,
            };

            mesContext1.TableMasterParts.Add(data);
            mesContext1.SaveChanges();
            return RedirectToAction("master_part", "Admin");
        }

        [HttpGet]
        public IActionResult Updatemasterpart(int MaterialId = 0)
        {
            masterpart masterPart = new masterpart();
            var data = mesContext1.TableMasterParts.Where(m => m.MaterialId == MaterialId).FirstOrDefault();
            List<TableMasterMaterial> Material_Id = mesContext1.TableMasterMaterials.ToList();
            ViewBag.MaterialID = new SelectList(Material_Id, "MaterialName", "MaterialName");

            if (data != null)
            {
                masterPart.Material_ID = data.MaterialId;
                masterPart.Material_Name = data.MaterialName;
                masterPart.Vendor = data.Vendor;
            }

            return View(masterPart);
        }

        [HttpPost]
        public IActionResult Updatemasterpart(masterpart masterPart)
        {
            var data= mesContext1.TableMasterParts.Where(m => m.MaterialId == masterPart.Material_ID).FirstOrDefault();
            data.MaterialId = masterPart.Material_ID;
            data.MaterialName = masterPart.Material_Name;
            data.Vendor = masterPart.Vendor;

            mesContext1.SaveChanges();

            return RedirectToAction("master_part", "Admin");
        }

        [HttpGet]
        public IActionResult Deletemasterpart(int Materialid)
        {
            var data = mesContext1.TableMasterParts.Where(m => m.MaterialId == Materialid).FirstOrDefault();
            if (data != null)
            {
                mesContext1.TableMasterParts.Remove(data);
                mesContext1.SaveChanges();
            }

            return RedirectToAction("master_part", "Admin");
        }
        /*workplan cntroller*/
        public IActionResult workplan()
        {
            return View();
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
            return RedirectToAction("masterstation", "Admin");
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

            return RedirectToAction("masterstation", "Admin");
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

            return RedirectToAction("masterstation", "Admin");
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
            return RedirectToAction("masterworkplan", "Admin");
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
                workPlan.Refference_Name= data.RefferenceName;
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
            data.LineId= workPlan.Line_ID;
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

            return RedirectToAction("masterworkplan", "Admin");
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

            return RedirectToAction("masterworkplan", "Admin");
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
                //int a = Convert.ToInt32(Masterorder.Status_Order);
                //string Nmmasterorder = "";
                //if (a == 0)
                //{
                //    Nmmasterorder = "-";
                //}
                //else
                //{
                //    var NamaMasterorder = mesContext1.TableMasterStatusOrders.Where(m => m.ID == a).FirstOrDefault();
                //    Nmmasterorder = NamaMasterorder.Status_Order;
                //}
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
            ViewBag.StatusOrder = new SelectList(statusOrder,"StatusOrder", "StatusOrder");

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
            return RedirectToAction("masterorder", "Admin");
        }
        [HttpGet]
        public IActionResult Updatemasterorder(String WorkOrder ) /*Parameter */
        {
            masterorder masterOrder = new masterorder();
            var data = mesContext1.TableMasterOrders.Where(m => m.WorkOrder == WorkOrder).FirstOrDefault();
            List<TableMasterStatusOrder> statusOrder = mesContext1.TableMasterStatusOrders.ToList();
            ViewBag.StatusOrder = new SelectList(statusOrder, "StatusOrder", "StatusOrder");

            //Dropdown Station Id 
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
                masterOrder.Qty_Launching= data.QtyLaunching;
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
            data.Username= masterOrder.Username;
            data.WoComment = masterOrder.WO_Comment;
            data.PriorityWo = masterOrder.Priority_WO;
            data.StationId = masterOrder.Station_ID;
            data.StationSuffix = masterOrder.Station_Suffix;

            

            mesContext1.SaveChanges();

            return RedirectToAction("masterorder", "Admin");
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

            return RedirectToAction("masterorder", "Admin");
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
            return RedirectToAction("master_reference", "Admin");
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

            return RedirectToAction("master_reference", "Admin");
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

            return RedirectToAction("master_reference", "Admin");
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
                    Material_Id = bom.MaterialId,
                    Material_Name = bom.MaterialName,
                    Refference_Name = bom.RefferenceName,
                    Station_Id = bom.StationId,
                    Revision_Number = (int)bom.RevisionNumber,
                    Last_Modify = bom.LastModify,
                    Transact_By = bom.TransactBy,
                    Part_Qty = bom.PartQty,
                    Part_1 = bom.Part1,
                    Part_2 = bom.Part2,
                    Part_3 = bom.Part3,
                    Part_4 = bom.Part4,
                    Part_5 = bom.Part5,
                    Part_6 = bom.Part6,
                    Part_7 = bom.Part7,
                    Part_8 = bom.Part8,
                    Part_9 = bom.Part9,
                    Part_10 = bom.Part10,
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

        [HttpGet]
        public IActionResult Savebom()
        {
            bomeditor bomEditor = new bomeditor();
            List<TableMasterStation> StationId = mesContext1.TableMasterStations.ToList();
            ViewBag.StationID = new SelectList(StationId, "StationId", "StationId");
            

            return View(bomEditor);
        }

        [HttpPost]
        public IActionResult Savebom(bomeditor bomEditor)
        {
            var data = new TableMasterMaterial()
            {
                MaterialId = bomEditor.Material_Id,
                MaterialName = bomEditor.Material_Name,
                RefferenceName = bomEditor.Refference_Name,
                StationId = (int)bomEditor.Station_Id,
                RevisionNumber = bomEditor.Revision_Number,
                LastModify = bomEditor.Last_Modify,
                TransactBy = bomEditor.Transact_By,
                PartQty = bomEditor.Part_Qty,
                Part1 = bomEditor.Part_1,
                Part2 = bomEditor.Part_2,
                Part3 = bomEditor.Part_3,
                Part4 = bomEditor.Part_4,
                Part5 = bomEditor.Part_5,
                Part6 = bomEditor.Part_6,
                Part7 = bomEditor.Part_7,
                Part8 = bomEditor.Part_8,
                Part9 = bomEditor.Part_9,
                Part10 = bomEditor.Part_10,
                Part11 = bomEditor.Part_11,
                Part12 = bomEditor.Part_12,
                Part13 = bomEditor.Part_13,
                Part14 = bomEditor.Part_14,
                Part15 = bomEditor.Part_15,
                Part16 = bomEditor.Part_16,
                Part17 = bomEditor.Part_17,
                Part18 = bomEditor.Part_18,
                Part19 = bomEditor.Part_19,
                Part20 = bomEditor.Part_20,
                Part21 = bomEditor.Part_21,
                Part22 = bomEditor.Part_22,
                Part23 = bomEditor.Part_23,
                Part24 = bomEditor.Part_24,
                Part25 = bomEditor.Part_25,
                Part26 = bomEditor.Part_26,
                Part27 = bomEditor.Part_27,
                Part28 = bomEditor.Part_28,
                Part29 = bomEditor.Part_29,
                Part30 = bomEditor.Part_30,
                Part31 = bomEditor.Part_31,
                Part32 = bomEditor.Part_32,
                Part33 = bomEditor.Part_33,
                Part34 = bomEditor.Part_34,
                Part35 = bomEditor.Part_35,
                Part36 = bomEditor.Part_36,
                Part37 = bomEditor.Part_37,
                Part38 = bomEditor.Part_38,
                Part39 = bomEditor.Part_39,
                Part40 = bomEditor.Part_40,
                Part41 = bomEditor.Part_41,
                Part42 = bomEditor.Part_42,
                Part43 = bomEditor.Part_43,
                Part44 = bomEditor.Part_44,
                Part45 = bomEditor.Part_45,
                Part46 = bomEditor.Part_46,
                Part47 = bomEditor.Part_47,
                Part48 = bomEditor.Part_48,
                Part49 = bomEditor.Part_49,
                Part50 = bomEditor.Part_50,
            };

            mesContext1.TableMasterMaterials.Add(data);
            mesContext1.SaveChanges();
            return RedirectToAction("bom", "Admin");
        }
        [HttpGet]
        public IActionResult Updatebom(int MaterialId = 0)
        {
            bomeditor bomEditor = new bomeditor();
            var data = mesContext1.TableMasterMaterials.Where(m => m.MaterialId == MaterialId).FirstOrDefault();
            List<TableMasterStation> StationId = mesContext1.TableMasterStations.ToList();
            ViewBag.stationID = new SelectList(StationId, "StationId", "StationId");

            if (data != null)
            {
                bomEditor.Material_Id = data.MaterialId;
                bomEditor.Material_Name = data.MaterialName;
                bomEditor.Refference_Name = data.RefferenceName;
                bomEditor.Station_Id= data.StationId;
                bomEditor.Revision_Number = (int)data.RevisionNumber;
                bomEditor.Last_Modify = data.LastModify;
                bomEditor.Transact_By = data.TransactBy;
                bomEditor.Part_Qty = data.PartQty;
                bomEditor.Part_1 = data.Part1;
                bomEditor.Part_2 = data.Part2;
                bomEditor.Part_3 = data.Part3;
                bomEditor.Part_4 = data.Part4;
                bomEditor.Part_5 = data.Part5;
                bomEditor.Part_6 = data.Part6;
                bomEditor.Part_7 = data.Part7;
                bomEditor.Part_8 = data.Part8;
                bomEditor.Part_9 = data.Part9;
                bomEditor.Part_10 = data.Part10;
                bomEditor.Part_11 = data.Part11;
                bomEditor.Part_12 = data.Part12;
                bomEditor.Part_13 = data.Part13;
                bomEditor.Part_14 = data.Part14;
                bomEditor.Part_15 = data.Part15;
                bomEditor.Part_16 = data.Part16;
                bomEditor.Part_17 = data.Part17;
                bomEditor.Part_18 = data.Part18;
                bomEditor.Part_19 = data.Part19;
                bomEditor.Part_20 = data.Part20;
                bomEditor.Part_21 = data.Part21;
                bomEditor.Part_22 = data.Part22;
                bomEditor.Part_23 = data.Part23;
                bomEditor.Part_24 = data.Part24;
                bomEditor.Part_25 = data.Part25;
                bomEditor.Part_26 = data.Part26;
                bomEditor.Part_27 = data.Part27;
                bomEditor.Part_28 = data.Part28;
                bomEditor.Part_29 = data.Part29;
                bomEditor.Part_30 = data.Part30;
                bomEditor.Part_31 = data.Part31;
                bomEditor.Part_32 = data.Part32;
                bomEditor.Part_33 = data.Part33;
                bomEditor.Part_34 = data.Part34;
                bomEditor.Part_35 = data.Part35;
                bomEditor.Part_36 = data.Part36;
                bomEditor.Part_37 = data.Part37;
                bomEditor.Part_38 = data.Part38;
                bomEditor.Part_39 = data.Part39;
                bomEditor.Part_40 = data.Part40;
                bomEditor.Part_41 = data.Part41;
                bomEditor.Part_42 = data.Part42;
                bomEditor.Part_43 = data.Part43;
                bomEditor.Part_44 = data.Part44;
                bomEditor.Part_45 = data.Part45;
                bomEditor.Part_46 = data.Part46;
                bomEditor.Part_47 = data.Part47;
                bomEditor.Part_48 = data.Part48;
                bomEditor.Part_49 = data.Part49;
                bomEditor.Part_50 = data.Part50;

            }
            return View(bomEditor);
        }

        [HttpPost]
        public IActionResult Updatebom(bomeditor bomEditor)
        {
            var data = mesContext1.TableMasterMaterials.Where(m => m.MaterialId == bomEditor.Material_Id).FirstOrDefault();
            data.MaterialId = bomEditor.Material_Id;
            data.MaterialName = bomEditor.Material_Name;
            data.RefferenceName = bomEditor.Refference_Name;
            data.StationId = (int)bomEditor.Station_Id;
            data.RevisionNumber = bomEditor.Revision_Number;
            data.LastModify = bomEditor.Last_Modify;
            data.TransactBy = bomEditor.Transact_By;
            data.PartQty = bomEditor.Part_Qty;
            data.Part1 = bomEditor.Part_1;
            data.Part2 = bomEditor.Part_2;
            data.Part3 = bomEditor.Part_3;
            data.Part4 = bomEditor.Part_4;
            data.Part5 = bomEditor.Part_5;
            data.Part6 = bomEditor.Part_6;
            data.Part7 = bomEditor.Part_7;
            data.Part8 = bomEditor.Part_8;
            data.Part9 = bomEditor.Part_9;
            data.Part10 = bomEditor.Part_10;
            data.Part11 = bomEditor.Part_11;
            data.Part12 = bomEditor.Part_12;
            data.Part13 = bomEditor.Part_13;
            data.Part14 = bomEditor.Part_14;
            data.Part15 = bomEditor.Part_15;
            data.Part16 = bomEditor.Part_16;
            data.Part17 = bomEditor.Part_17;
            data.Part18 = bomEditor.Part_18;
            data.Part19 = bomEditor.Part_19;
            data.Part20 = bomEditor.Part_20;
            data.Part21 = bomEditor.Part_21;
            data.Part22 = bomEditor.Part_22;
            data.Part23 = bomEditor.Part_23;
            data.Part24 = bomEditor.Part_24;
            data.Part25 = bomEditor.Part_25;
            data.Part26 = bomEditor.Part_26;
            data.Part27 = bomEditor.Part_27;
            data.Part28 = bomEditor.Part_28;
            data.Part29 = bomEditor.Part_29;
            data.Part30 = bomEditor.Part_30;
            data.Part31 = bomEditor.Part_31;
            data.Part32 = bomEditor.Part_32;
            data.Part33 = bomEditor.Part_33;
            data.Part34 = bomEditor.Part_34;
            data.Part35 = bomEditor.Part_35;
            data.Part36 = bomEditor.Part_36;
            data.Part37 = bomEditor.Part_37;
            data.Part38 = bomEditor.Part_38;
            data.Part39 = bomEditor.Part_39;
            data.Part40 = bomEditor.Part_40;
            data.Part41 = bomEditor.Part_41;
            data.Part42 = bomEditor.Part_42;
            data.Part43 = bomEditor.Part_43;
            data.Part44 = bomEditor.Part_44;
            data.Part45 = bomEditor.Part_45;
            data.Part46 = bomEditor.Part_46;
            data.Part47 = bomEditor.Part_47;
            data.Part48 = bomEditor.Part_48;
            data.Part49 = bomEditor.Part_49;
            data.Part50 = bomEditor.Part_50;

            mesContext1.SaveChanges();

            return RedirectToAction("bom", "Admin");
        }

        [HttpGet]
        public IActionResult Deletebom(int Materialid = 0)
        {
            var data = mesContext1.TableMasterMaterials.Where(m => m.MaterialId == Materialid).FirstOrDefault();
            if (data != null)
            {
                mesContext1.TableMasterMaterials.Remove(data);
                mesContext1.SaveChanges();
            }

            return RedirectToAction("bom", "Admin");
        }


        public IActionResult serialcounterAd()
        {
            SerialCounter serialCounter = new SerialCounter();
            serialCounter.CounterList = new List<serialcounter>();
            var data = mesContext1.TableMasterSerialCounters.ToList();

            foreach (var Serialcounter in data)
            {
                serialCounter.CounterList.Add(new serialcounter
                {
                    Station_ID = (int)Serialcounter.StationId,
                    Station_Suffix = (int)Serialcounter.StationSuffix,
                    Year_Code = (int)Serialcounter.YearCode,
                    Week_Code = (int)Serialcounter.WeekCode,
                    Counter_Code = (int)Serialcounter.CounterCode,
                });
            }

            return View(serialCounter);
        }

        [HttpGet]
        public IActionResult Saveserialcounter()
        {
            serialcounter serialCounter = new serialcounter();
            List<TableMasterStation> StationId = mesContext1.TableMasterStations.ToList();
            ViewBag.stationID = new SelectList(StationId, "StationId", "StationId");

            return View(serialCounter);
        }

        [HttpPost]
        public IActionResult Saveserialcounter(serialcounter serialCounter)
        {
            var data = new TableMasterSerialCounter()
            {
                StationId = serialCounter.Station_ID,
                StationSuffix = serialCounter.Station_Suffix,
                YearCode = serialCounter.Year_Code,
                WeekCode = serialCounter.Week_Code,
                CounterCode = serialCounter.Counter_Code,
            };

            mesContext1.TableMasterSerialCounters.Add(data);
            mesContext1.SaveChanges();
            return RedirectToAction("serialcounterAd", "Admin");
        }

        public IActionResult traceabillityAd()
        {
            TraceabillityModel traceabillity = new TraceabillityModel();
            traceabillity.Traceabillitylist = new List<traceabillitymodel>();
            var data = mesContext1.TableRunTraceabilities.Take(1000).ToList();

            foreach (var traceabillitymodel in data)
            {
                traceabillity.Traceabillitylist.Add(new traceabillitymodel
                {
                    Serial_Number = traceabillitymodel.SerialNumber,
                    Batch_ID = traceabillitymodel.BatchId,
                    Refference_Name = traceabillitymodel.RefferenceName,
                    Work_Order = traceabillitymodel.WorkOrder,
                    Station_ID = traceabillitymodel.StationId,
                    Station_Suffix = traceabillitymodel.StationSuffix,
                    Station_Name = traceabillitymodel.StationName,
                    User_ID = traceabillitymodel.UserId,
                    Cavity_Number = traceabillitymodel.CavityNumber,
                    Time_In = (DateTime)traceabillitymodel.TimeIn,
                    Time_Out = (DateTime)traceabillitymodel.TimeOut,
                    Status_Result = traceabillitymodel.StatusResult,
                    Status_Running = traceabillitymodel.StatusRunning,
                    Transact_By = traceabillitymodel.StatusRunning,
                    FullRefference = traceabillitymodel.FullRefference,

                });
            }
            return View(traceabillity);
        }

        [HttpGet]
        public IActionResult Savetraceabillity()
        {
            traceabillitymodel traceabillity = new traceabillitymodel();
            List<TableRunTraceability> SerialNumber  = mesContext1.TableRunTraceabilities.ToList();
            ViewBag.serialNumber = new SelectList(SerialNumber, "SerialNumber", "SerialNumber");

            List<TableMasterStation> StationId = mesContext1.TableMasterStations.ToList();
            ViewBag.stationID = new SelectList(StationId, "StationId", "StationId");

            List<TableMasterBatch> BatchId = mesContext1.TableMasterBatches.ToList();
            ViewBag.batchId = new SelectList(BatchId, "BatchId", "BatchId");

            List<TableMasterOrder> WorkOrder = mesContext1.TableMasterOrders.ToList();
            ViewBag.workOrder = new SelectList(WorkOrder, "WorkOrder, WorkOrder");

            List<TableMasterUser> IdUser = mesContext1.TableMasterUsers.ToList();
            ViewBag.idUSer = new SelectList(IdUser, "IdUser", "IdUser");

            List<TableMasterStatusRunning> StatusRunning = mesContext1.TableMasterStatusRunnings.ToList();
            ViewBag.statusRunning = new SelectList(StatusRunning, "StatusRunning", "StatusRunning");

            List<TableMasterStatusResult> StatusResult = mesContext1.TableMasterStatusResults.ToList();
            ViewBag.statusResult = new SelectList(StatusResult, "ResultStatus", "ResultStatus");

            return View(traceabillity);

        }

        [HttpPost]
        public IActionResult Savetraceabillity(traceabillitymodel traceabillity)
        {
            var data = new TableRunTraceability()
            {
                SerialNumber = traceabillity.Serial_Number,
                BatchId = traceabillity.Batch_ID,
                RefferenceName = traceabillity.Refference_Name,
                WorkOrder = traceabillity.Work_Order,
                StationId = traceabillity.Station_ID,
                StationSuffix = traceabillity.Station_Suffix,
                StationName = traceabillity.Station_Name,
                UserId = traceabillity.User_ID,
                CavityNumber = traceabillity.Cavity_Number,
                TimeIn = traceabillity.Time_In,
                TimeOut = traceabillity.Time_Out,
                StatusResult = traceabillity.Status_Result,
                StatusRunning = traceabillity.Status_Running,
                TransactBy = traceabillity.Transact_By,
                FullRefference = traceabillity.FullRefference,

            };

            mesContext1.TableRunTraceabilities.Add(data);
            mesContext1.SaveChanges();
            return RedirectToAction("traceabillityAd", "Admin");

        }

    }
}
