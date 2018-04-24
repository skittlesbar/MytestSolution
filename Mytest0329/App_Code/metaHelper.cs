using Mytest0329.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Mytest0329.App_Code
{
    public static class MetaHelper
    {
        public static String metaConn = @"data source = @hostname; initial catalog = @db; user id = @id; password = @password";

        /// <summary>
        /// 维护元数据库的数据库表
        /// </summary>
        /// <returns>True:维护成功，请继续;False:维护失败，请回滚</returns>
        public static Boolean MaintainDB(String db_name)
        {
            String sql = "";
            Int32 res = SqlHelper.ExecuteNonQuery(sql, metaConn);
            return (res == 1 ? true : false);
        }
        public static Boolean MaintainCommon(MetaModel dict, Int32 db_id)
        {
            Boolean mt = MaintainTable(dict, db_id);
            if (mt)
            {
                Int32 tb_id = GetTbId(dict.origin_name);
                if (tb_id > 0) return MaintainColmuns(dict, tb_id);
                else return false;
            }
            else
            {
                return false;
            }
        }
        public static Int32 GetTbId(String tb_name)
        {
            String sql = "SELECT id FROM meta_tb WHERE tb_name = @tb_name";
            object tb_id = SqlHelper.ExecuteScalar(sql, metaConn, new SqlParameter("@tb_name", tb_name));
            try
            {
                Int32 tid = Convert.ToInt32(tb_id);
                return tid;
            }
            catch
            {
                return -1;
            }
        }
        public static Boolean MaintainTable(MetaModel dict, Int32 db_id)
        {
            List<MetaColumns> list = dict.columns;
            String strTname = dict.origin_name;
            String strAlias = dict.self_name;
            // 生成维护数据表的语句
            String sql = "INSERT INTO meta_tb(db_id, tb_name, tb_alias, tb_time) VALUES(@db_id, @tb_name, @tb_alias, @tb_time);";
            Int32 ret = SqlHelper.ExecuteNonQuery(sql, metaConn, new SqlParameter("@db_id", db_id), new SqlParameter("@tb_name", strTname), new SqlParameter("@tb_alias", strAlias), new SqlParameter("@tb_time", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")));
            return (ret == 1 ? true : false);
        }
        /// <summary>
        /// 维护列元数据
        /// </summary>
        /// <param name="dict"></param>
        /// <param name="tb_id"></param>
        /// <returns></returns>
        public static Boolean MaintainColmuns(MetaModel dict, Int32 tb_id)
        {
            List<MetaColumns> list = dict.columns;
            List<SqlParameter> ilistStr = new List<SqlParameter>();

            String sql = "INSERT INTO meta_col(tb_id, col_name, col_self_name, col_type) VALUES";
            int i = 0;
            foreach (MetaColumns item in list)
            {
                sql += "(@tb_id"+i+", @col_name"+i+ ", @col_self_name" + i + ", @col_type" + i + ")";
                ilistStr.Add(new SqlParameter("@tb_id"+i, tb_id));
                ilistStr.Add(new SqlParameter("@col_name" + i, item.name));
                ilistStr.Add(new SqlParameter("@col_self_name" + i, item.self_name));
                ilistStr.Add(new SqlParameter("@col_type" + i, item.type));
                i += 1;
                sql += (i == list.Count?"":",");
            }

            SqlParameter[] ps = ilistStr.ToArray();

            Int32 ret = SqlHelper.ExecuteNonQuery(sql, metaConn, ps);
            return (ret > 0 ? true : false);
        }

    }
}