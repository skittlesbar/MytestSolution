using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mytest0329.Models
{
    public class MetaModel
    {
        public String origin_name;//原始表名
        public String self_name;//用户自定义表明
        public List<MetaColumns> columns;//列的集合
    }

    public class MetaColumns
    {
        public String name;//原始列名
        public String self_name;//自定义列名
        public String type;//列类型
        public Int32 length;//列类型长度
    }
}