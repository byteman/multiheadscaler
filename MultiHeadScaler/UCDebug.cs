﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
namespace Monitor
{
    public partial class UCDebug : UserControl
    {
        private FormFrame formFrame;
        private Bitmap bmBtnDown = null;
        private Bitmap bmBtnUp = null;
        ScalerInfo si = new ScalerInfo();
        public UCDebug(FormFrame f)
        {
            InitializeComponent();
            formFrame = f;
            string path = formFrame.configManage.FileDir + @"\main_btn_down.png";
            if (File.Exists(path))
            {
                bmBtnDown = new Bitmap(path);
            }
            path = formFrame.configManage.FileDir + @"\main_btn_up.png";
            if (File.Exists(path))
            {
                bmBtnUp = new Bitmap(path);
            }

            pbClear.Image = bmBtnUp;
            pbBan.Image = bmBtnUp;
            //pbStep.Image = bmBtnUp;
            pbShake.Image = bmBtnUp;

            //pbContinuStep.Image = bmBtnUp;
            //pbEmpty.Image = bmBtnUp;
            pbStop.Image = bmBtnUp;
            pbExit.Image = bmBtnUp;
        }

        private void pbBtn_MouseDown(object sender, MouseEventArgs e)
        {
            if (bmBtnDown != null)
            {
                ((PictureBox)sender).Image = bmBtnDown;
            }
        }

        private void pbBtn_MouseUp(object sender, MouseEventArgs e)
        {
            if (bmBtnUp != null)
            {
                ((PictureBox)sender).Image = bmBtnUp;
            }
        }

        private void DrawLabel(object sender, PaintEventArgs e, string str)
        {
            e.Graphics.DrawString(str, new Font("宋体", 16, FontStyle.Bold), new SolidBrush(Color.Black), 40, 20);
        }
        public new void Dispose()
        {
            if (bmBtnDown != null) bmBtnDown.Dispose();
            if (bmBtnUp != null) bmBtnUp.Dispose();
            base.Dispose();
        }
        private void UpdateUI(object obj, System.EventArgs e)
        {
            //更新每个banocx上面的重量.

            for (int i = 0; i < 10; i++)
            {
                banOcxCtl1.SetBanWeight(i + 1, si.getWeightString(i));
                Color color = si.getStatusColor(i);
                //System.Console.WriteLine("color"+ i + color.ToString());
                banOcxCtl1.SetBanColor(i + 1, color);
                banOcxCtl1.SetBanStatus(i + 1, si.getStatusString(i));
                System.Diagnostics.Debug.WriteLine(si.getStatusString(i));
            }
            
            banOcxCtl1.BanRefresh();
        }
        private void pbClear_Paint(object sender, PaintEventArgs e)
        {
            DrawLabel(sender, e, "清零");
        }

        private void pbBan_Paint(object sender, PaintEventArgs e)
        {
            DrawLabel(sender, e, "称重斗");
        }

        private void pbShake_Paint(object sender, PaintEventArgs e)
        {
           DrawLabel(sender, e, "振动");
        }

        private void pbStep_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pbContinuStep_Paint(object sender, PaintEventArgs e)
        {
            //DrawLabel(sender, e, "连续单步");
        }

        private void pbEmpty_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pbStop_Paint(object sender, PaintEventArgs e)
        {
            DrawLabel(sender, e, "全部停止");
        }

        private void pbExit_Paint(object sender, PaintEventArgs e)
        {
            DrawLabel(sender, e, "返回");
        }

        private void pbExit_Click(object sender, EventArgs e)
        {
            formFrame.ShowUC(formFrame.ucMain);
        }
        private void send(List<ParamItem> itemList)
        {
            Protocol protocol = formFrame.protocol;
            SerialOperate Serial = SerialOperate.instance;

            byte[] buf;
            int len = protocol.Produce(formFrame.configManage.cfg.paramDeviceId.Ctrl, out buf, itemList);
            if (len > 0)
            {
                Serial.Send(buf, len);
            }
        }

        private void pbClear_Click(object sender, EventArgs e)
        {
            sendDebugCmd(192);
        }

        private void pbBan_Click(object sender, EventArgs e)
        {
            sendDebugCmd(194);
        }

        private void banOcxCtl1_点击事件(object sender, BanOcx.MyEventArges e)
        {
            
            if (e.SelNum == 0)
                tb_number.Text = "11";
            else tb_number.Text = e.SelNum.ToString();
        }
        private void read_all_weight()
        {
            List<ParamItem> itemList = new List<ParamItem>();
            ParamItem item;
            item = new ParamItem();

            item.dev_id = formFrame.configManage.cfg.paramDeviceId.Ctrl;
            item.param_id = 4; //读取全部驱动板重量
            item.op_write = 0; //读取
            item.param_type = TypeCode.Empty;
            item.param_len = 0;
            item.param_value = 0;
            itemList.Add(item);

            item = new ParamItem();

            item.dev_id = formFrame.configManage.cfg.paramDeviceId.Ctrl;
            item.param_id = 3; //读取全部驱动板重量
            item.op_write = 0; //读取
            item.param_type = TypeCode.Empty;
            item.param_len = 0;
            item.param_value = 0;
            itemList.Add(item);

            send(itemList);

        }
        private void sendDebugCmd(byte cmd)
        {
            List<ParamItem> itemList = new List<ParamItem>();
            ParamItem item;
            item = new ParamItem();

            byte driver_id = byte.Parse(tb_number.Text); ;
            item.dev_id = (byte)formFrame.configManage.cfg.paramDeviceId.Ctrl;
            item.param_id = cmd; //振动一次
            item.op_write = 1;
            item.param_type = TypeCode.Byte;
            item.param_len = 1;
            item.param_value = driver_id;
            itemList.Add(item);
            send(itemList);
        }
        private void pbShake_Click(object sender, EventArgs e)
        {
            
            sendDebugCmd(193);
        }

        private void pbStep_Click(object sender, EventArgs e)
        {
            //sendDebugCmd(195);
        }

        private void pbContinuStep_Click(object sender, EventArgs e)
        {
            //sendDebugCmd(196);
        }

        private void pbEmpty_Click(object sender, EventArgs e)
        {
            //sendDebugCmd(197);
        }

        private void pbStop_Click(object sender, EventArgs e)
        {
            sendDebugCmd(198);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                read_all_weight();
            }
        }
        public void SetReturnValue(List<ParamItem> itemList)
        {
            //
            foreach (ParamItem item in itemList)
            {
                if (item.dev_id == 128) //控制器的命令回应
                {
                    if (item.param_id == 3) //读取所有秤头状态
                    {
                        //item.param_value
                        si.updateStatusObj(item.param_value);
                    }
                    else if (item.param_id == 4) //读取所有秤头重量
                    {
                        //item.param_value
                        si.updateWeightObj(item.param_value);
                    }
                    BeginInvoke(new System.EventHandler(UpdateUI), null);
                }
            }

        }
       

    }
}
