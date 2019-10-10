using System;
using Degage.DataModel.Orm;
namespace Degage.DataModel.Orm.Example
{
    class Program
    {
        static void Main(string[] args)
        {

            //Connection string formats.
            //SQLServer: Data Source = ip; Uid = user; Pwd = password; Initial Catalog = dbname;
            //Oracle: Data Source = (DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = ip)(PORT = port)))(CONNECT_DATA = (SERVICE_NAME = servicename))); User Id = user; Password = password
            //MySql: server = ip; Port = port; Uid = root; Pwd = password; DataBase = dbname; Pooling = true; charset = utf8;
            //SQLite: Data Source = path; UTF8Encoding = True;
            var connStr = "Data Source = test.db; UTF8Encoding = True;";
            SQLiteDbProvider proivder = new SQLiteDbProvider("MyName", connStr);

            var rowCount = 0;
            //-------------------------------------------------get  infos--------------------------------------------------------
            ShowTextInfo(" Select infos:" + Environment.NewLine, ConsoleColor.Yellow);
            var userInfos = proivder.Select<TestUser>().ToList();
            rowCount = userInfos.Count;
            userInfos.ForEach(info =>
            {
                ShowUserInfo(info);
            });
            NewLine();

            ShowTextInfo(" Select infos with where （Id=2）:" + Environment.NewLine, ConsoleColor.Yellow);
            userInfos = proivder.Select<TestUser>().Where(t => t.Id == "2").ToList();
            userInfos.ForEach(info =>
            {
                ShowUserInfo(info);
            });
            NewLine();

            ShowTextInfo(" Select infos by sql:" + Environment.NewLine, ConsoleColor.Yellow);
            userInfos = proivder.Query<TestUser>("select * from test_user where id=1").ToList();
            userInfos.ForEach(info =>
            {
                ShowUserInfo(info);
            });


            //-------------------------------------------------insert info----------------------------------------------------------
            ShowTextInfo(Environment.NewLine + "Insert a user info:", ConsoleColor.Yellow);
            TestUser newUser = new TestUser
            {
                Id = (++rowCount).ToString(),
                Age = 36,
                Born = DateTime.Now.AddYears(-30),
                Name = "John Wang",
                Descrption = "Remark:New User at " + DateTime.Now.ToString()
            };
            ShowUserInfo(newUser, ConsoleColor.White);
            var affect = proivder.Insert(newUser).ExecuteNonQuery(); ;
            ShowTextInfo(" Affect row count is " + affect.ToString(), ConsoleColor.Green);






        }
        private static ConsoleColor _OldForeColor = Console.ForegroundColor;
        public static void SetForeColor(ConsoleColor color)
        {
            _OldForeColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
        }
        public static void RecoverForeColor()
        {

            Console.ForegroundColor = _OldForeColor;
        }
        public static void NewLine()
        {
            Console.WriteLine("-----------------------------------------------------------------------------------");
        }
        public static void ShowTextInfo(String text, ConsoleColor color = ConsoleColor.White)
        {
            SetForeColor(color);
            Console.WriteLine(text);
            RecoverForeColor();
        }
        public static void ShowUserInfo(TestUser info, ConsoleColor color = ConsoleColor.Green)
        {
            var text = $"\tId:{info.Id}, Name:{info.Name} , Age:{info.Age}, Born:{info.Born.ToString("yyyy-MM-dd")}, Desc:{info.Descrption}";
            ShowTextInfo(text, color);
        }
        public class TestUser
        {
            public String Id { get; set; }
            public String Name { get; set; }
            public Int32 Age { get; set; }
            public DateTime Born { get; set; }
            public String Descrption { get; set; }
        }
    }
}
