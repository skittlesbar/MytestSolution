using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mytest0329.Controllers
{
    public class UserRegisterController : Controller
    {
        // GET: UserRegister

        //接收form表单返回的数据并存储到数据库中
        public ActionResult Index(FormCollection collection)
        {
            string str = "";
            //if (Request["textname"].Equals(null) || Request["pwname"].Equals(null) || Request["textname"] == "" || Request["pwname"].Equals(""))
            //{
            //     str = "";

            // }
            if (Request["textname"] == null)
            {
                str = "";
            }

            else {
                //声明一个上下文
                MVCTestEntities mVCTestEntities = new MVCTestEntities();

                //创建一个实体
                Mytest0329.users firstuser = new users();
                //firstuser.username = Request["textname"];
                //firstuser.password = Request["pwname"];
                firstuser.username = collection["textname"];
                firstuser.password = collection["pwname"];

                //在上下文中的表的类中添加实体
                mVCTestEntities.users.Add(firstuser);

                //告诉上下文保存更改，即执行SQL语句操作
                mVCTestEntities.SaveChanges();

                ViewData["status"] = "注册成功";
            }
            


            return View();
        }
    }
}