using System;
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
    public partial class UCCalib : UserControl
    {
        private FormFrame formFrame;
        private Bitmap bmBtnDown = null;
        private Bitmap bmBtnUp = null;
        public Timer timer = new Timer();
        ScalerInfo si = new ScalerInfo();
        public UCCalib(FormFrame f)
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
            pbZero.Image = bmBtnUp;
            pbCalib.Image = bmBtnUp;
            pbExit.Image = bmBtnUp;

            //timer.Interval = configManage.cfg.paramFormWeight.Interval;
            timer.Interval = 200;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Enabled = false;
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
        void timer_Tick(object sender, EventArgs e)
        {
            if (this.Visible == false)
            {
                return;
            }
            read_all_weight();
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

        private void pbClear_Paint(object sender, PaintEventArgs e)
        {
            DrawLabel(sender, e, "清零");
        }

        private void pbZero_Paint(object sender, PaintEventArgs e)
        {
            DrawLabel(sender, e, "零点");
        }

        private void pbCalib_Paint(object sender, PaintEventArgs e)
        {
            DrawLabel(sender, e, "标定");
        }


        private void pbExit_Paint(object sender, PaintEventArgs e)
        {
            DrawLabel(sender, e, "返回");
        }

        private void pbExit_Click(object sender, EventArgs e)
        {
            
        }
        public void init()
        {
            timer.Enabled = true;
        }
     
        private void send()
        {
            Protocol protocol = formFrame.protocol;
            SerialOperate Serial = SerialOperate.instance;
            List<ParamItem> itemList = new List<ParamItem>();
            ParamItem item;
            item = new ParamItem();
            item.dev_id = formFrame.configManage.cfg.paramDeviceId.Ctrl;
            item.param_id = 0;
            item.op_write = 1;
            item.param_type = TypeCode.Byte;
            item.param_len = 1;
            item.param_value = (byte)1;

            itemList.Add(item);
            byte[] buf;
            int len = protocol.Produce(formFrame.configManage.cfg.paramDeviceId.Ctrl, out buf, itemList);
            if (len > 0)
            {
                Serial.Send(buf, len);
            }
        }
        private void pbClear_Click(object sender, EventArgs e)
        {
            send();
        }

        private void tb_number_GotFocus(object sender, EventArgs e)
        {
            banOcxCtl1.Focus();
            ParamItem item = new ParamItem();
            item.name = "设备地址";
            item.unit = "";
            item.param_type = TypeCode.Int32;
            item.param_value = Convert.ToInt32(tb_number.Text);

            FormInput dlg = new FormInput(formFrame);
            dlg.SetValue(item, true);
            dlg.ShowDialog();
            if (dlg.GetAck())
            {
                item = dlg.GetValue();
                tb_number.Text = item.param_value.ToString();
                //ChangeCalibWeight(((CalibInfo)alSource[r]).Addr, Convert.ToInt32(item.param_value));
                
            }
            dlg.Dispose();
            
        }

        private void tb_fama_GotFocus(object sender, EventArgs e)
        {
            banOcxCtl1.Focus();
            ParamItem item = new ParamItem();
            item.name = "砝码重量";
            item.unit = "g";
            item.param_type = TypeCode.Int32;
            item.param_value = Convert.ToInt32(tb_fama.Text);

            FormInput dlg = new FormInput(formFrame);
            dlg.SetValue(item, true);
            dlg.ShowDialog();
            if (dlg.GetAck())
            {
                item = dlg.GetValue();
                tb_fama.Text = item.param_value.ToString();
                //ChangeCalibWeight(((CalibInfo)alSource[r]).Addr, Convert.ToInt32(item.param_value));

            }
            dlg.Dispose();
        }

        private void banOcxCtl1_点击事件(object sender, BanOcx.MyEventArges e)
        {
            tb_number.Text = e.SelNum.ToString();
        }
        private void DrawText(string msg,int x, int y)
        {
            Graphics g =  this.CreateGraphics();

            g.DrawString("标定零点成功", new Font("宋体", 16, FontStyle.Bold), new SolidBrush(Color.Black), x, y);
           
            g.Dispose();
        }
        public void SetReturnValue(List<ParamItem> itemList)
        {
            //
            foreach (ParamItem item in itemList)
            {
                if (item.dev_id <= 32)
                { 
                    //通道板的消息回应 
                    if (item.param_id == 2)
                    {
                        if (item.param_valid == 1)
                        {
                            FormMsgBox.Show("标定零点成功", "标定提示");
                        }
                        else
                        {
                            FormMsgBox.Show("标定零点失败", "标定提示");
                        }
                        
                    }
                    else if (item.param_id == 3) //标定重量
                    {
                        if (item.param_valid == 1)
                        {
                            FormMsgBox.Show("标定重量成功", "标定提示");
                        }
                        else
                        {
                            FormMsgBox.Show("标定重量失败", "标定提示");
                        }

                    }
                }
                else if (item.dev_id == 128) //控制器的命令回应
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
        private void UpdateUI(object obj, System.EventArgs e)
        {
           //更新每个banocx上面的重量.
            
            for (int i = 0; i < 10; i++)
            {
                banOcxCtl1.SetBanWeight(i+1, si.getWeightString(i));
               
                banOcxCtl1.SetBanColor(i  + 1, si.getStatusColor(i));
                banOcxCtl1.SetBanStatus(i + 1, si.getStatusString(i));        
                
            }
            banOcxCtl1.BanRefresh();
        }
        private void pbExit_Click_1(object sender, EventArgs e)
        {
            //read_all_weight();
            timer.Enabled = false;
            formFrame.ShowUC(formFrame.ucMain);
        }

        private void UCCalib_Paint(object sender, PaintEventArgs e)
        {
           // e.Graphics.DrawRectangle(new Pen(Color.Blue), 530, 20, 250, 440);

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
        private void pbZero_Click(object sender, EventArgs e)
        {
            List<ParamItem> itemList = new List<ParamItem>();
            ParamItem item;
            item = new ParamItem();
            int driver_id = int.Parse(tb_number.Text);

            item.dev_id = (byte)driver_id;
            item.param_id = 2; //标定零点
            item.op_write = 1;
            item.param_type = TypeCode.Byte;
            item.param_len = 1;
            item.param_value = (byte)1;
            itemList.Add(item);
            send(itemList);
        }

        private void pbCalib_Click(object sender, EventArgs e)
        {
            List<ParamItem> itemList = new List<ParamItem>();
            ParamItem item;
            item = new ParamItem();
            int driver_id = int.Parse(tb_number.Text);

            item.dev_id = (byte)driver_id;
            item.param_id = 3; //标定重量.
            item.op_write = 1;
            item.param_type = TypeCode.Int32;
            item.param_len = 4;
            item.param_value = int.Parse(tb_fama.Text);
            itemList.Add(item);
            send(itemList);
        }
        //清零发到主控器
        private void pbClear_Click_1(object sender, EventArgs e)
        {
            List<ParamItem> itemList = new List<ParamItem>();
            ParamItem item;
            item = new ParamItem();
            int driver_id = int.Parse(tb_number.Text);

            item.dev_id = (byte)driver_id;
            item.param_id = 11; //清零驱动板
            item.op_write = 1;
            item.param_type = TypeCode.Byte;
            item.param_len = 1;
            item.param_value = int.Parse(tb_number.Text);
            itemList.Add(item);
            send(itemList);
        }

    }
}
