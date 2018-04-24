using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mytest0329.Controllers
{
    public class HighchartController : Controller
    {
        // GET: Highchart
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult test()
        {
            var ans = readData();
            return Json(ans);
        }

        public JsonResult readData()
        {
            var res = new JsonResult();
            var title = "学生成绩柱形图";
            var subtitle = "2018年第一季度学生成绩信息";
            var ytitle = "成绩";
            var s_name = "S_name";
            res.Data = new object[] { new { title, subtitle, ytitle, s_name } };
            return res;
        }
    }
}