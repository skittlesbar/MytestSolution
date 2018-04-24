using System;
using System.Text.RegularExpressions;

namespace Mytest0329.App_Code
{
    /// <summary>
    /// 自定义一个判断数据类型的类，定义每种数据格式对应的方法
    /// </summary>
    public static class JudgeDataType
    {
        //终极方法，判断为哪种类型,给函数一个索引使代码简洁--待做
        public static string JudeType(string targetdata)
        {

            string typestr = null;
            if (IsInt(targetdata))
                typestr = "整数";
            else if (IsFloat(targetdata))
                typestr = "小数";
            else if (IsText(targetdata))
                typestr = "文本";
            else if (IsDate(targetdata))
                typestr = "日期";
            else
                typestr = "日期时间";

            return typestr;
        }
        //整数
        public static bool IsInt(string targetdata)
        {
            bool result = Regex.IsMatch(targetdata, @"^-?\d+$");
            return result;
        }
        //小数
        public static bool IsFloat(string targetdata)
        {
            bool result = Regex.IsMatch(targetdata, @"^-?\d+\.\d+$");
            return result;
        }

        //日期
        public static bool IsDate(string targetdata)
        {
            //判断是否符合****/**/**或****-**-**模式
            if (Regex.IsMatch(targetdata, @"^(\s*\d{4}\s*-\d{1,2}\s*-\d{1,2}\s*)|(\s*\d{4}\s*/\d{1,2}\s*/\d{1,2}\s*)"))
            {
                DateTime dateTime = new DateTime();
                return DateTime.TryParse(targetdata, out dateTime);
            }
            else
                return false;
        }

        //日期时间
        public static bool IsDateTime(string targetdata)
        {
            DateTime dateTime = new DateTime();
            return DateTime.TryParse(targetdata, out dateTime);
        }

        //文本
        public static bool IsText(string targetdata)
        {
            if (!(IsDate(targetdata) || IsDateTime(targetdata) || IsInt(targetdata) || IsFloat(targetdata)))
                return true;
            else
                return false;

        }
    }
}