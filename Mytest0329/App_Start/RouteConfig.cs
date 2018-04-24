using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Mytest0329
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",//配置路由规则 http://localhost/home/Index/参数
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }//直接访问域名时，默认访问home控制器的Index方法
            );
        }
    }
}
