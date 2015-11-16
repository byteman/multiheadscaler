using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Collections;


namespace Monitor
{
    public class FormulaData
    {
        public FormulaData()
        { 
            xzp_strength = new int[10];
            for (int i = 0; i < 10; i++)
            {
                xzp_strength[i] = 65;
            }
            
           

            xzp_time = new int[10];
            for (int i = 0; i < 10; i++)
            {
                xzp_time[i] = 40;
            }


        }
        void set_strength(int index, int value)
        {
            if (index < 10) xzp_strength[index] = value;
        }
        void set_time(int index, int value)
        {
            if (index < 10) xzp_time[index] = value;
        }
        public double   target_weight { set; get; } //目标重量
        public int      packet_per_minitue { set; get; } //速度 包/分钟
        public double   up_diff { set; get; } //上偏差
        public double   down_diff { set; get; } //下偏差
        public int      stable_time { set; get; } //稳定时间 x0.01s
        public int      tare_count { set; get; } //去皮次数 
        public int      force_comb { set; get; } //强制组合 次数
        public int      no_comb { set; get; } //无组合
        public int      AFC { set; get; } //振幅自动跟踪模式选择
        public int      formula_pic { set; get; }
        public int      feed_mode { set; get; } //放料模式
        public int      feed_in_turn { set; get; } //依次放料
        public int      motor_mode { set; get; } //电机模式
        public int      multi_feed { set; get; } //多次放料
        public string   formula_name { set; get; } //配方名称
        public int      formula_id { set; get; } //配方编号
        public int      open_delay { set; get; } //开斗停顿
        public int[]    xzp_strength { set; get; }   //线振盘强度
        public int[]    xzp_time { set; get; }   //线振盘时间
    }
}
