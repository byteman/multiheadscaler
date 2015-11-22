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
    public partial class UCDebug : UserControl
    {
        private FormFrame formFrame;
        private Bitmap bmBtnDown = null;
        private Bitmap bmBtnUp = null;
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
            pbStep.Image = bmBtnUp;
            pbShake.Image = bmBtnUp;

            pbContinuStep.Image = bmBtnUp;
            pbEmpty.Image = bmBtnUp;
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
            DrawLabel(sender, e, "单步");
        }

        private void pbContinuStep_Paint(object sender, PaintEventArgs e)
        {
            DrawLabel(sender, e, "连续单步");
        }

        private void pbEmpty_Paint(object sender, PaintEventArgs e)
        {
            DrawLabel(sender, e, "清空");
        }

        private void pbStop_Paint(object sender, PaintEventArgs e)
        {
            DrawLabel(sender, e, "停止");
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
            tb_number.Text = e.SelNum.ToString();
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
            sendDebugCmd(195);
        }

        private void pbContinuStep_Click(object sender, EventArgs e)
        {
            sendDebugCmd(196);
        }

        private void pbEmpty_Click(object sender, EventArgs e)
        {
            sendDebugCmd(197);
        }

        private void pbStop_Click(object sender, EventArgs e)
        {
            sendDebugCmd(198);
        }

       

    }
}
