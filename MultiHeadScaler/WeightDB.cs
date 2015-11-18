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
        private static void CreateWeightDataTable()
        {
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
                }
            }
        }
        private static void CreateFormulaTable()
        {
            using (SQLiteConnection conn = new SQLiteConnection(config.DataSource))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = conn;
                    conn.Open();

                    SQLiteTable tb = new SQLiteTable("formula");
                    //编号
                    tb.Columns.Add(new SQLiteColumn("id", ColType.Integer, true, true, true, null));
                    //目标重量
                    tb.Columns.Add(new SQLiteColumn("target_weight", ColType.Decimal));
                    //速度
                    tb.Columns.Add(new SQLiteColumn("packet_per_minitue", ColType.Decimal));
                    //目标重量
                    tb.Columns.Add(new SQLiteColumn("up_diff", ColType.Decimal));
                    //目标重量
                    tb.Columns.Add(new SQLiteColumn("down_diff", ColType.Decimal));
                    //稳定时间
                    tb.Columns.Add(new SQLiteColumn("stable_time", ColType.Integer));
                    //去皮次数
                    tb.Columns.Add(new SQLiteColumn("tare_count", ColType.Integer));
                    //强制组合次数
                    tb.Columns.Add(new SQLiteColumn("force_comb", ColType.Integer));
                    //无组合
                    tb.Columns.Add(new SQLiteColumn("no_comb", ColType.Integer));
                    //振幅自动跟踪模式选择
                    tb.Columns.Add(new SQLiteColumn("AFC", ColType.Integer));
                    //放料模式
                    tb.Columns.Add(new SQLiteColumn("feed_mode", ColType.Integer));
                    //依次放料
                    tb.Columns.Add(new SQLiteColumn("feed_in_turn", ColType.Integer));
                    //电机模式
                    tb.Columns.Add(new SQLiteColumn("motor_mode", ColType.Integer));
                    //多次放料
                    tb.Columns.Add(new SQLiteColumn("multi_feed", ColType.Integer));
                    //配方名称
                    tb.Columns.Add(new SQLiteColumn("formula_name", ColType.Text));
                    //配方编号
                    tb.Columns.Add(new SQLiteColumn("formula_id", ColType.Integer));
                    //开斗停顿
                    tb.Columns.Add(new SQLiteColumn("open_delay", ColType.Integer));
                    
                    
                    for (int i = 0; i < 10; i++)
                    {
                        tb.Columns.Add(new SQLiteColumn("xzp_strength"+i.ToString(), ColType.Integer));
                        tb.Columns.Add(new SQLiteColumn("xzp_time" + i.ToString(), ColType.Integer));
                    }

                    SQLiteHelper sh = new SQLiteHelper(cmd);

                    sh.CreateTable(tb);
                }
            }
        }
        //方法创建一个表
        public static void CreateDB(string dbPath)
        {

            config.DatabaseFile = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\" + dbPath;
            CreateWeightDataTable();
            CreateFormulaTable();
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
        public static DataTable listFormula(int id)
        {
            using (SQLiteConnection conn = new SQLiteConnection(config.DataSource))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = conn;
                    conn.Open();
                    string sql = String.Format("select * from formula where formula_id={0:d}", id);


                    SQLiteHelper sh = new SQLiteHelper(cmd);
                    DataTable dt = sh.Select(sql);

                    conn.Close();
                    return dt;
                }
            }
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
        public static int ParamCount()
        {
            int count = 0;
            using (SQLiteConnection conn = new SQLiteConnection(config.DataSource))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = conn;
                    conn.Open();
                    SQLiteHelper sh = new SQLiteHelper(cmd);

                    count = sh.ExecuteScalar<int>("select count(*) from formula;");

                    conn.Close();

                }
            }
            return count;
        }
        public static bool MofifyFormula(int id,FormulaData data)
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

                    dic["AFC"] = data.AFC;
                }

            }
            return true;
        }
        public static bool updateFormula(Dictionary<string, object> dict, Dictionary<string, object> cond)
        {
            using (SQLiteConnection conn = new SQLiteConnection(config.DataSource))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = conn;
                    conn.Open();

                    SQLiteHelper sh = new SQLiteHelper(cmd);
                    sh.Update("formula",dict,cond);
                    conn.Close();
                }
            }
         
            return true;
        }

        public static bool addFormula(Dictionary<string, object> dic)
        {
            using (SQLiteConnection conn = new SQLiteConnection(config.DataSource))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = conn;
                    conn.Open();

                    SQLiteHelper sh = new SQLiteHelper(cmd);

                    sh.Insert("formula", dic);

                    conn.Close();

                }
            }

            return true;
        
        }
        public static bool addFormula(FormulaData data)
        {
        
            using (SQLiteConnection conn = new SQLiteConnection(config.DataSource))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = conn;
                    conn.Open();

                    SQLiteHelper sh = new SQLiteHelper(cmd);

                    var dic = new Dictionary<string, object>();

                    dic["AFC"] = data.AFC;
                    dic["down_diff"] = data.down_diff;
                    dic["feed_in_turn"] = data.feed_in_turn;
                    dic["feed_mode"] = data.feed_mode;
                    dic["force_comb"] = data.force_comb;
                    dic["formula_id"] = data.formula_id;
                    dic["formula_name"] = data.formula_name;
                    dic["motor_mode"] = data.motor_mode;
                    dic["multi_feed"] = data.multi_feed;
                    dic["no_comb"] = data.no_comb;
                    dic["packet_per_minitue"] = data.packet_per_minitue;
                    dic["stable_time"] = data.stable_time;
                    dic["tare_count"] = data.tare_count;
                    dic["target_weight"] = data.target_weight;
                    dic["up_diff"] = data.up_diff;
                    dic["open_delay"] = data.open_delay;
                    for (int i = 0; i < 10; i++)
                    {
                        dic["xzp_strength" + i.ToString()] = data.xzp_strength[i];
                        dic["xzp_time" + i.ToString()] = data.xzp_time[i];
                    }
                    sh.Insert("formula", dic);

                    conn.Close();
                }
            }
         
            return true;
        }
    }
}
