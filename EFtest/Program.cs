using System.Data;
namespace EFtest
{
    class Program
    {
        static void testFunction()
        {
            Mytest0329.App_Code.Excel2DT edt = new Mytest0329.App_Code.Excel2DT(@"C:\Users\wushengnan\Desktop\元数据培训\教师任课表.xlsx");
            DataTable dt = edt.ExcelToDataTable("", true);
            //Mytest0329.App_Code.SqlHelper.DataTableToSQLServer(dt, "teacherssession2");
        }
        static void Main(string[] args)
        {
            //MVCTestEntities mVCTestEntities = new MVCTestEntities();

            //studentsInfo studentsinfo = new studentsInfo();
            //studentsinfo.number = "21451";
            //studentsinfo.name = "冯峥";
            //studentsinfo.sex = "男";
            //studentsinfo.age = "21";

            //mVCTestEntities.Entry<studentsInfo>(studentsinfo).State = System.Data.EntityState.Modified;

            //mVCTestEntities.Entry<studentsInfo>(studentsinfo).Property<string>(u => u.name).IsModified = true;
            //mVCTestEntities.Entry<studentsInfo>(studentsinfo).Property("name").IsModified = true;



            //mVCTestEntities.studentsInfo.Add(studentsinfo);

            //mVCTestEntities.SaveChanges();

            //测试C#字符串转换为日期
            //string str = "2018-09-03";
            //string str1 = "2018/09/32";
            //string str2 = "2018/09/02";
            //DateTime dateTime = new DateTime();
            //dateTime = Convert.ToDateTime(str2);
            //DateTime dateTime2 = new DateTime();
            //bool b = false;
            //if (DateTime.TryParse(str, out dateTime2))
            //{

            //    b = true;
            //}
            //else
            //    b = false;
            //List<bool> lst = new List<bool>();
            //lst.Add(Mytest0329.Controllers.EasyUIController.JudgeDataType.IsDate("2018-03-02"));
            //lst.Add(Mytest0329.Controllers.EasyUIController.JudgeDataType.IsDate("2018/03/321"));
            //lst.Add(Mytest0329.Controllers.EasyUIController.JudgeDataType.IsDateTime("2018/02/01 12:43:42"));
            //lst.Add(Mytest0329.Controllers.EasyUIController.JudgeDataType.IsFloat("32.327"));
            //lst.Add(Mytest0329.Controllers.EasyUIController.JudgeDataType.IsInt("-327678"));
            //lst.Add(Mytest0329.Controllers.EasyUIController.JudgeDataType.IsText("d372i"));
            //lst.Add(Mytest0329.Controllers.EasyUIController.JudgeDataType.IsFloat("323"));
            //string type = Mytest0329.Controllers.EasyUIController.JudgeDataType.JudeType("d372i");



            testFunction();
            //Console.WriteLine(b);
            //Console.ReadKey();

        }

    }
}
