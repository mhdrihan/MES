//using MES.data;
//using MES.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using System.Diagnostics;

//namespace MES.Controllers
//{
//    public class WorkplanController : Controller
//    {
//        private readonly ILogger<WorkplanController> logger;
//        private MesappContext _mesappContext;
//        private ILogger<WorkplanController> _logger;
//        private MesappContext mesappContext;

//        public WorkplanController(ILogger<WorkplanController> logger, MesappContext _mesappContext)
//        {
//            _logger = logger;
//            _mesappContext = mesappContext;
//        }
//        // GET: WorkplanController
//        public ActionResult Index()
//        {
//            WorkplanModel workplanModel = new WorkplanModel();
//            workplanModel.WorkplanList = new List<WorkplanModel>();
//            var data = _mesappContext.TableMasterWorkplans.ToList();
//            foreach(var item in data)
//            {
//                workplanModel.WorkplanList.Add(new Workplan 
//                {
//                    FlowId = item.FlowId,
//                    FlowName = item.FlowName,
//                    RefferenceName = item.RefferenceName,
//                    LineID = item.LineId,
//                    ValidStatus = item.ValidStatus,
//                    RevisionNumber = item.RevisionNumber,
//                    LastModify = item.LastModify,
//                    TransactBy = item.TransactBy,
//                    FlowDescription = item.FlowDescription,
//                    ProcessQty = item.ProcessQty,
//                    Process1 = item.Process1,
//                    Process2 = item.Process2,
//                    Process3 = item.Process3,
//                    Process4 = item.Process4,
//                    Process5 = item.Process5,
//                    Process6 = item.Process6,
//                    Process7 = item.Process7,
//                    Process8 = item.Process8,
//                    Process9 = item.Process9,
//                    Process10 = item.Process10,
//                    Process11 = item.Process11,
//                    Process12 = item.Process12
//                });
//            }
//            return View(workplanModel);
//        }

//        // GET: WorkplanController/Details/5
//        public ActionResult Details(int id)
//        {
//            return View();
//        }

//        // GET: WorkplanController/Create
//        public ActionResult Create()
//        {
//            return View();
//        }

//        // POST: WorkplanController/Create
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Create(IFormCollection collection)
//        {
//            try
//            {
//                return RedirectToAction(nameof(Index));
//            }
//            catch
//            {
//                return View();
//            }
//        }

//        // GET: WorkplanController/Edit/5
//        public ActionResult Edit(int id)
//        {
//            return View();
//        }

//        // POST: WorkplanController/Edit/5
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit(int id, IFormCollection collection)
//        {
//            try
//            {
//                return RedirectToAction(nameof(Index));
//            }
//            catch
//            {
//                return View();
//            }
//        }

//        // GET: WorkplanController/Delete/5
//        public ActionResult Delete(int id)
//        {
//            return View();
//        }

//        // POST: WorkplanController/Delete/5
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Delete(int id, IFormCollection collection)
//        {
//            try
//            {
//                return RedirectToAction(nameof(Index));
//            }
//            catch
//            {
//                return View();
//            }
//        }
//    }
//}
