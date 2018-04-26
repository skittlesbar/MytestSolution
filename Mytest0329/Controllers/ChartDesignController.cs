using Mytest0329.App_Code;
using System;
using System.Web.Mvc;

namespace Mytest0329.Controllers
{
    public class ChartDesignController : Controller
    {
        // GET: ChartDesign
        [HttpGet]
        public ActionResult Index()
        {
            String ans = DesignHelper.ReadAllTable();
            var res = new JsonResult();
            res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            var tables = ans;
            res.Data = new object[] { new { tables } };
            ViewData["data"] = res;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Index(Int32 tid)
        {
            tid = 2;//从后台获取
            String strColumns = DesignHelper.ReadColumnsByTid(tid);
            var res = new JsonResult();
            var columns = strColumns;
            res.Data = new object[] { new { columns } };
            return res;
        }
        public ActionResult Home()
        {
            return View();
        }
        [HttpGet]
        public JsonResult Left()
        {
            object ans = DesignHelper.ReadSheetLeft();
            var data = new JsonResult();
            data.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            data.Data = ans;
            return data;
        }

        [HttpGet]
        public JsonResult Classify()
        {
            return null;
        }

        //[HttpPost]
        //public JsonResult AddClassify([ModelBinder(typeof JsonBinder<Classify>))]Classify cfy)
        //{
        //    var data = new JsonResult();
        //    try
        //    {
        //        String name = cfy.name;
        //        object ans = DesignHelper.MaintainClassify(name: name);
        //        data.Data = ans;
        //    }
        //    catch
        //    {
        //        data.Data = new object[] { new { status = "0x10" } };
        //    }
        //    return data;
        //}
    }
}