using Mytest0329.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Mytest0329.App_Code
{
    public static class SqlHelper
    {
        public static string strConn = @"data source = WINDOWS-PIU20LO\SQLEXPRESS; initial catalog = MVCTest; user id = wsn; password = Wsn@2145127";

        public static bool StartTran(String sqlStr)
        {
            String sql = "BEGIN TRAN";
            try
            {
                ExecuteNonQuery(sql, sqlStr);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool CollBack(String sqlStr)
        {
            String sql = "ROLLBACK TRAN";
            try
            {
                ExecuteNonQuery(sql, sqlStr);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool StartCommit(String sqlStr)
        {
            String sql = "COMMIT TRAN";
            try
            {
                ExecuteNonQuery(sql, sqlStr);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 用前端数据生成创建数据表的字符串
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        public static String CreateTableSql(MetaModel dict)
        {
            try
            {
                List<MetaColumns> list = dict.columns;
                String strTname = dict.self_name;
                // 生成创建表的语句
                String sql = "CREATE TABLE " + AddHasel(strTname) + "(ID int identity(1,2) primary key,";
                foreach (MetaColumns mc in list)
                {
                    sql += (AddHasel(mc.name) + @" " + AddHasel(mc.type)) + ",";
                }
                sql = sql.Substring(0, sql.Length - 1) + ")";
                return sql;
            }
            catch (Exception)
            {
                return null;
            }

        }
        /// <summary>
        /// 执行sql语句，返回影响的行数
        /// </summary>
        /// <param name="sql">执行的sql语句</param>
        /// <returns>-1:执行SQL出错;其他:执行SQL影响的行数</returns>
        public static Int32 ExecuteNonQuery(String sql, String strConn, params SqlParameter[] ps)
        {
            using (SqlConnection sqlConn = new SqlConnection(strConn))
            {
                using (SqlCommand sqlCmd = new SqlCommand(sql, sqlConn))
                {
                    try
                    {
                        sqlCmd.Parameters.AddRange(ps);
                        sqlConn.Open();
                        return sqlCmd.ExecuteNonQuery();
                    }
                    catch
                    {
                        return -2;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="strConn"></param>
        /// <param name="ps"></param>
        /// <returns></returns>
        public static object ExecuteScalar(String sql, String strConn, params SqlParameter[] ps)
        {
            using (SqlConnection sqlConn = new SqlConnection(strConn))
            {
                using (SqlCommand sqlCmd = new SqlCommand(sql, sqlConn))
                {
                    try
                    {
                        sqlCmd.Parameters.AddRange(ps);
                        sqlConn.Open();
                        return sqlCmd.ExecuteScalar();
                    }
                    catch
                    {
                        return -1;
                    }
                }
            }
        }

        public static DataTable ReadTable(String sql, String conn, params SqlParameter[] ps)
        {
            DataTable dt = null;
            using (SqlConnection sqlConn = new SqlConnection(strConn))
            {
                using (SqlCommand sqlCmd = new SqlCommand(sql, sqlConn))
                {
                    try
                    {
                        sqlCmd.Parameters.AddRange(ps);
                        sqlConn.Open();
                        //强大的SqlDataAdapter 
                        SqlDataAdapter adapter = new SqlDataAdapter(sqlCmd);

                        DataSet ds = new DataSet();
                        //Fill 方法会执行一系列操作 connection.open command.reader 等等
                        //反正到最后就把 sql语句执行一遍,然后把结果集插入到 ds 里.
                        adapter.Fill(ds);

                        dt = ds.Tables[0];
                    }
                    catch
                    {
                        dt = null;
                    }
                }
            }
            return dt;
        }

        /// <summary>
        /// 使用SqlBulkCopy将datatable中的数据存储到数据库中，有问题待处理--列名数据重复存储
        /// </summary>
        /// <param name="dt">参数1 datatable</param>
        /// <param name="connectString">参数2  数据库链接字符串</param>
        /// <returns>返回成功与否的提示语</returns>
        public static Boolean DataTableToSQLServer(DataTable dt, String tablename, List<MetaColumns> mc)
        {
            using (SqlConnection destinationConnection = new SqlConnection(strConn))
            {
                destinationConnection.Open();
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(destinationConnection))
                {
                    try
                    {
                        bulkCopy.DestinationTableName = tablename;//要插入的表的表名
                        bulkCopy.BatchSize = dt.Rows.Count;

                        //将数据从datatable中导入
                        foreach (MetaColumns item in mc)
                        {
                            //映射字段名 DataTable列名 ,数据库 对应的列名  
                            bulkCopy.ColumnMappings.Add(item.name, item.name);
                        }
                        bulkCopy.WriteToServer(dt);
                        return true;
                    }
                    catch (Exception ex)
                    {

                        return false;
                    }
                    finally
                    {

                    }
                }
            }
        }

        /// <summary>
        /// 将导入数据库中的表信息存储到系统元数据中，未完待续
        /// </summary>
        /// <param name="connectString"></param>
        /// <returns></returns>
        public static string SaveSQLServerMetaData(string connectString)
        {
            string connectionString = connectString;
            using (SqlConnection destinationConnection = new SqlConnection(connectionString))
            {
                destinationConnection.Open();
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(destinationConnection))
                {
                    try
                    {
                        SqlCommand command = new SqlCommand();
                        command.Connection = destinationConnection;
                        string datetime = DateTime.Now.ToString();
                        command.CommandText = "INSERT INTO fieldsTable (tableId , tableName ,objectId ,objectName ,fieldId ,fieldName ,fieldType ,fieldLength ,createDate ) VALUES ('0001', 'teacherssession2', '0001','教师任课表','0001001','number','nvarchar','50','" + datetime + "')," + "('0001', 'teacherssession2', '0001','教师任课表','0001002','major','nvarchar','50','" + datetime + "')," +
                            "('0001', 'teacherssession2', '0001','教师任课表','0001003','session','nvarchar','50','" + datetime + "')," +
                            "('0001', 'teacherssession2', '0001','教师任课表','0001004','class','nvarchar','50','" + datetime + "')," +
                            "('0001', 'teacherssession2', '0001','教师任课表', '0001005', 'teachername', 'nvarchar', '50', '" + datetime + "')";
                        command.CommandType = CommandType.Text;
                        int j = Convert.ToInt32(command.ExecuteNonQuery());
                        return "保存元数据成功";
                    }
                    catch (Exception ex)
                    {
                        return "保存元数据失败" + "\n" + ex;
                    }
                    finally
                    {

                    }
                }
            }
        }

        /// <summary>
        /// 过滤SQL注入、XSS等内容
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static String AddHasel(String str)
        {
            return str;
        }

        /// <summary>
        /// 检测数据库是否正常连接的方法
        /// </summary>
        /// <returns>返回成功与否的提示语</returns>
        public static String TestSQLseverConnection()
        {
            try
            {
                //声明一个字符串，用于存储连接数据库字符串
                string constring = @"Data Source = WINDOWS-PIU20LO\SQLEXPRESS; Initial Catalog = MVCTest;  Integrated Security = True";
                //Data Source = china - PC\SQLEXPRESS; Initial Catalog = getsinablog; Integrated Security = True
                //创建一个Sqlconnection对象
                SqlConnection con = new SqlConnection(constring);
                con.Open();
                if (con.State == ConnectionState.Open)
                {
                    return "数据库已经连接并打开";
                }

                else
                    return "数据库连接失败else";
            }
            catch (Exception e)
            {
                return "数据库连接失败catch" + e;
            }


        }
    }
}