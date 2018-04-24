using System;
using System.Data;
using System.IO;
using System.Web;

namespace Mytest0329.App_Code
{
    public static class ExcelHelper
    {
        public static String ReadExcelHeader(String fullPath)
        {
            Excel2DT excelHelper = new Excel2DT(fullPath);
            DataTable dt = excelHelper.ExcelToDataTable("", false);
            string ans = "{";

            if (dt.Rows.Count > 0)
            {
                try
                {
                    DataRow item = dt.Rows[0];
                    for (int j = 0; j < item.ItemArray.Length; j++)
                    {
                        String key = dt.Rows[0][j].ToString();
                        String type = JudgeDataType.JudeType(dt.Rows[1][j].ToString());
                        ans += ("\"" + key + "\":\"" + type + ((j + 1 == item.ItemArray.Length) ? "\"" : "\","));
                    }
                }
                catch
                {
                    ;
                }
            }
            ans += "}";
            return ans;
        }

        /// <summary>
        /// 读取上传的Excel文件并转换成datatable
        /// </summary>
        /// <param name="fullPath">文件路径</param>
        /// <returns>返回datatable</returns>
        public static DataTable ReadExcelToDataTable(String fullPath)
        {
            Excel2DT excel_helper = new Excel2DT(fullPath);
            DataTable dt = excel_helper.ExcelToDataTable("", true);

            //List<string> tableList = GetColumnsByDataTable(dt);
            //Int32 count = tableList.Count; 
            String data = null;
            foreach (DataRow item in dt.Rows)
            {
                try
                {
                    for (int j = 0; j < item.ItemArray.Length; j++)
                    {
                        data += (j != item.ItemArray.Length - 1 ? item[j].ToString() + "-" : item[j].ToString());
                    }
                    data += "\n";
                }
                catch (Exception ex)
                { }
            }
            return dt;
            // 返回数据
        }

        public static String SaveExcel(HttpPostedFileBase file, String StorageRoot)
        {
            try
            {
                var fullPath = Path.Combine(StorageRoot, Path.GetFileName(file.FileName));
                file.SaveAs(fullPath);
                return fullPath;
            }
            catch
            {
                return null;
            }

        }
    }
}