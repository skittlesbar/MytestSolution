using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Mytest0329.App_Code
{
    public static class DesignHelper
    {
        public static String ReadAllTable()
        {
            String sql = "SELECT meta_tb.tb_alias, meta_tb.id FROM metadata.dbo.meta_db RIGHT OUTER JOIN metadata.dbo.meta_tb ON meta_db.id = meta_tb.db_id WHERE meta_db.uid = 1";

            DataTable dt = SqlHelper.ReadTable(sql, MetaHelper.metaConn);
            if (dt == null)
            {
                return "{'code':'0x01', 'msg':'读取源数据出错'}";
            }
            else
            {
                String ans = Json.ToJson(dt);
                return ans;
            }
        }
        public static String ReadColumnsByTid(Int32 tid)
        {
            String sql = "SELECT col_self_name, id FROM metadata.dbo.meta_col WHERE meta_col.tb_id=@tid";
            DataTable dt = SqlHelper.ReadTable(sql, MetaHelper.metaConn, new System.Data.SqlClient.SqlParameter("@tid", tid));
            if (dt == null)
            {
                return "{'code':'0x01', 'msg':'读取源数据出错'}";
            }
            else
            {
                String ans = Json.ToJson(dt);
                return ans;
            }
        }
        public static object ReadSheetLeft()
        {
            String sql = "SELECT a.id, a.name FROM MVCTest.dbo.tb_sheets AS a ORDER BY a.order_id";
            DataTable dt = SqlHelper.ReadTable(sql, SqlHelper.strConn);
            if(dt == null || dt.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                ArrayList list = new ArrayList();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    String pid = dt.Rows[i]["id"].ToString();
                    String pname = dt.Rows[i]["name"].ToString();
                    
                    String sql1 = "SELECT a.id, a.name FROM MVCTest.dbo.tb_sheet AS a WHERE a.ss_id = @sid ORDER BY a.o_id";
                    DataTable dt1 = SqlHelper.ReadTable(sql1, SqlHelper.strConn, new System.Data.SqlClient.SqlParameter("@sid", pid));
                    if (dt1 == null || dt1.Rows.Count == 0)
                    {
                        object data = null;
                        list.Add(new object[] { new { pid, pname, data } });
                    }
                    else
                    {
                        ArrayList aldata = new ArrayList();
                        for (int j = 0; j < dt1.Rows.Count; j++)
                        {
                            String sid = dt1.Rows[j]["id"].ToString();
                            String sname = dt1.Rows[j]["name"].ToString();
                            aldata.Add(new object[] { new { sid, sname } });
                        }
                        list.Add(new object[] { new { pid, pname, data = aldata.ToArray() } });
                    }

                }

                return list.ToArray();
            }
        }
    }
}