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
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MES.Controllers
{
    public class ProductengineerController : Controller
    {
        private readonly ILogger<ProductengineerController> _logger;
        private MesappContext mesContext1;  /*rubah dari MesappContext ke MesContext begitu sebalik nya*/
        public ProductengineerController(ILogger<ProductengineerController> logger, MesappContext mesContext)
        {
            _logger = logger;
            mesContext1 = mesContext;
        }
        public ActionResult Productengineer()
        {
            var partCount = mesContext1.TableMasterParts.Count();
            ViewBag.PartCount = partCount;
            var bomCount = mesContext1.TableMasterMaterials.Count();
            ViewBag.BomCount = bomCount;
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult bomPE()
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
        public IActionResult SavebomPE()
        {
            bomeditor bomEditor = new bomeditor();
            List<TableMasterStation> StationId = mesContext1.TableMasterStations.ToList();
            ViewBag.StationID = new SelectList(StationId, "StationId", "StationId");

            return View(bomEditor);
        }

        [HttpPost]
        public IActionResult SavebomPE(bomeditor bomEditor)
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
            return RedirectToAction("bomPE", "Productengineer");
        }
        [HttpGet]
        public IActionResult Updatebom(int MaterialId = 0)
        {
            bomeditor bomEditor = new bomeditor();
            var data = mesContext1.TableMasterMaterials.Where(m => m.MaterialId == MaterialId).FirstOrDefault();
            List<TableMasterStation> StationId = mesContext1.TableMasterStations.ToList();
            ViewBag.StationID = new SelectList(StationId, "StationId", "StationId");

            if (data != null)
            {
                bomEditor.Material_Id = data.MaterialId;
                bomEditor.Material_Name = data.MaterialName;
                bomEditor.Refference_Name = data.RefferenceName;
                bomEditor.Station_Id = data.StationId;
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

            return RedirectToAction("bomPE", "Productengineer");
        }

        [HttpGet]
        public IActionResult DeletebomPE(int Materialid = 0)
        {
            var data = mesContext1.TableMasterMaterials.Where(m => m.MaterialId == Materialid).FirstOrDefault();
            if (data != null)
            {
                mesContext1.TableMasterMaterials.Remove(data);
                mesContext1.SaveChanges();
            }

            return RedirectToAction("bomPE", "Productengineer");
        }
        public IActionResult master_partPE()
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
            return RedirectToAction("master_partPE", "Productengineer");
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
            var data = mesContext1.TableMasterParts.Where(m => m.MaterialId == masterPart.Material_ID).FirstOrDefault();
            data.MaterialId = masterPart.Material_ID;
            data.MaterialName = masterPart.Material_Name;
            data.Vendor = masterPart.Vendor;

            mesContext1.SaveChanges();

            return RedirectToAction("master_partPE", "Productengineer");
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

            return RedirectToAction("master_partPE", "Productengineer");
        }


    }
}
