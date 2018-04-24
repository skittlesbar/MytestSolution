using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mytest0329.Controllers
{
    public class test1Controller : Controller
    {
        // GET: test1
        public ActionResult Index()
        {
            ViewData["a"] = "董志勇是条狗，哈巴狗！";
            return View();
        }
    }
}