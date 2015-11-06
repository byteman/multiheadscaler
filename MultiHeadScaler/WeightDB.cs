using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SQLite;
using System.Reflection;

namespace Monitor
{
    class config
    {
        public static string DatabaseFile = "";
        public static string DataSource
        {
            get
            {
                return string.Format("data source={0}", DatabaseFile);
            }
        }
    }
    class SQLiteDBHelper
    {
        //初始化设置connectionString为空，用于打开SQLiteConnection实例链接的参数
        private string connectionString = string.Empty;
        

        public SQLiteDBHelper()
        {
           
        }
        public static void ClearDB()
        {
            using (SQLiteConnection conn = new SQLiteConnection(config.DataSource))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = conn;
                    conn.Open();

                    SQLiteHelper sh = new SQLiteHelper(cmd);

                    sh.DropTable("weight");

                    SQLiteTable tb = new SQLiteTable("weight");
                    //编号
                    tb.Columns.Add(new SQLiteColumn("id", ColType.Integer, true, true, true, null));
                    //重量
                    tb.Columns.Add(new SQLiteColumn("weight", ColType.Decimal));
                    //时间日期
                    tb.Columns.Add(new SQLiteColumn("s_date", ColType.DateTime));
                    //重量偏差
                    tb.Columns.Add(new SQLiteColumn("diff", ColType.Decimal));
                    //组合斗数
                    tb.Columns.Add(new SQLiteColumn("heads"));

                    
                    SQLiteHelper sh2 = new SQLiteHelper(cmd);

                    sh2.CreateTable(tb);

                    conn.Close();
                }
            }
            
        }
        //方法创建一个表
        public static void CreateDB(string dbPath)
        {

            config.DatabaseFile = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\" + dbPath;
            using (SQLiteConnection conn = new SQLiteConnection(config.DataSource))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = conn;
                    conn.Open();

                    SQLiteTable tb = new SQLiteTable("weight");
                    //编号
                    tb.Columns.Add(new SQLiteColumn("id", ColType.Integer, true, true, true, null));
                    //重量
                    tb.Columns.Add(new SQLiteColumn("weight", ColType.Decimal));
                    //时间日期
                    tb.Columns.Add(new SQLiteColumn("s_date", ColType.DateTime));
                    //重量偏差
                    tb.Columns.Add(new SQLiteColumn("diff", ColType.Decimal));
                    //组合斗数
                    tb.Columns.Add(new SQLiteColumn("heads"));

                    SQLiteHelper sh = new SQLiteHelper(cmd);

                    sh.CreateTable(tb);

                    conn.Close();
                }
            }


        }
        public static int addData(WeightData data)
        {
            int count = 0;
            using (SQLiteConnection conn = new SQLiteConnection(config.DataSource))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = conn;
                    conn.Open();

                    SQLiteHelper sh = new SQLiteHelper(cmd);

                    var dic = new Dictionary<string, object>();

                    dic["weight"] = data.weight;
                    dic["s_date"] = System.DateTime.Now; 
                    dic["diff"]   = data.diff;
                    dic["heads"]  = data.getZuheString();

                    sh.Insert("weight", dic);

                    conn.Close();
                }
            }
            return count;
        }
        public static DataTable listData(int page, int page_size)
        {
            using (SQLiteConnection conn = new SQLiteConnection(config.DataSource))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = conn;
                    conn.Open();
                    string sql = String.Format("select * from weight limit {0:d} offset {1:d}", page_size, page * page_size);
                  

                    SQLiteHelper sh = new SQLiteHelper(cmd);
                    DataTable dt = sh.Select(sql);

                    conn.Close();
                    return dt;
                }
            }
           
        }
        public static int DataCount()
        {
            int count = 0;
            using (SQLiteConnection conn = new SQLiteConnection(config.DataSource))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = conn;
                    conn.Open();
                    SQLiteHelper sh = new SQLiteHelper(cmd);

                    count = sh.ExecuteScalar<int>("select count(*) from weight;");

                    conn.Close();
                   
                }
            }
            return count;
        }
    }
}
