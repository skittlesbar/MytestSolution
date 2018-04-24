using System;
using Newtonsoft.Json;
using Mytest0329.Models;
using System.Collections.Generic;

namespace Mytest0329.App_Code
{
    public static class JSONHelper
    {
        public static MetaModel ParseJson(String data)
        {
            try
            {
                MetaModel mm = (MetaModel)JsonConvert.DeserializeObject(data);
                return mm;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static MetaModel CreateJson(String oriName, String tname, String[] name, String[] self_name, String[] type)
        {
            try
            {
                List<MetaColumns> list = new List<MetaColumns>();
                for (int i = 0; i < name.Length; i++)
                {
                    MetaColumns mc = new MetaColumns();
                    mc.name = name[i];
                    mc.self_name = self_name[i];
                    mc.type = type[i];
                    list.Add(mc);
                }
                MetaModel mm = new MetaModel();
                mm.columns = list;
                mm.origin_name = oriName;
                mm.self_name = tname;
                return mm;
            }
            catch
            {
                return null;
            }
        }
    }
}