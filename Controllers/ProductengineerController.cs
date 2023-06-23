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
    public class ProductengineerController : Controller
    {
        private readonly ILogger<ProductengineerController> _logger;
        private MesappContext mesContext1;  /*rubah dari MesappContext ke MesContext begitu sebalik nya*/
        public ProductengineerController(ILogger<ProductengineerController> logger, MesappContext mesContext)
        {
            _logger = logger;
            mesContext1 = mesContext;
        }
        public IActionResult Productengineer()
        {
            return View();
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
        public IActionResult partmasterPE()
        {
            return View();
        }
    }
}
