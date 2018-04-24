using System;
using System.Data;
using System.Web.Mvc;
using Mytest0329.Models;
using Mytest0329.App_Code;

namespace Mytest0329.Controllers
{
    public class EasyUIController : Controller
    {
        // GET: EasyUI
        public ActionResult Index()
        {
            return View();
        }

        public String UploadExcel()
        {
            try
            {
                // 存储excel文件，返回excel文件的完整路径
                var file = Request.Files["file1"];//file1就是easyui-filebox的name  
                String fullPath = ExcelHelper.SaveExcel(file, Server.MapPath("~/uploads/"));
                if(fullPath == null) return "{ \"status\": \"error\", \"msg\":\"文件保存失败\"}";
                Session["fullPath"] = fullPath;

                //获取excel文件头
                String headers = ExcelHelper.ReadExcelHeader(fullPath);
                return headers;
            }
            catch (Exception ex)
            {
                return "{ \"status\": \"error\"}";
            }
        }
        public String SaveExcel()
        {
            try
            {
                // 获取前端返回的数据
                String tbName = Request.Form["tablename"].ToString();
                String[] name = Request.Form["name"].ToString().Split(',');
                String[] self_name = Request.Form["self_name"].ToString().Split(',');
                String[] type = Request.Form["type"].ToString().Split(',');
                String fullPath = Session["fullPath"].ToString();
                String[] temp = fullPath.Split('\\');
                Int32 db_id = 1;//从其他位置获取数据库ID,默认放到MVCText中
                temp = temp[temp.Length - 1].Split('.');
                //String oriname = fullPath.Split('');
                // 解析前端返回的数据
                MetaModel mm = JSONHelper.CreateJson(temp[0], tbName, name, self_name, type);
                if (mm == null) return "{\"status\":\"error\", \"msg\":\"数据格式不正确\"}";
                // 用前端传送的数据生成SQL语句
                String sql = SqlHelper.CreateTableSql(mm);
                if (sql == null) return "{\"status\":\"error\", \"msg\":\"数据不正确\"}";

                // 创建数据表
                Int32 intResult = SqlHelper.ExecuteNonQuery(sql, SqlHelper.strConn);
                if (intResult != -1 || MetaHelper.MaintainCommon(mm, db_id) == false)
                {
                    //删表
                    return "{\"status\":\"error\", \"msg\":\"数据表" + SqlHelper.AddHasel(mm.self_name) + "链接出错\"}";
                }
                // 打开事务
                if (SqlHelper.StartTran(SqlHelper.strConn) == false) return "{\"status\":\"error\", \"msg\":\"数据库链接出错\"}";
                // 写入数据
                DataTable dt = ExcelHelper.ReadExcelToDataTable(fullPath);
                bool bAns = SqlHelper.DataTableToSQLServer(dt, mm.self_name, mm.columns);
                if (bAns)
                {
                    // 提交commit
                    SqlHelper.StartCommit(SqlHelper.strConn);
                }
                else
                {
                    // 提交回滚
                    SqlHelper.CollBack(SqlHelper.strConn);
                }
                return "{\"status\":\"succeed\", \"msg\":\"成功创建数据\"}";
            }
            catch
            {
                // 提交回滚
                SqlHelper.CollBack(SqlHelper.strConn);
                return "{\"status\":\"error\", \"msg\":\"数据库链接出错\"}";
            }
        }
        /// <summary>
        /// 返回左侧树对象和字段的方法，未写
        /// </summary>
        /// <returns></returns>
        public static string ShowFields()
        {
            string str = "";

            return str;
        }
    }
}