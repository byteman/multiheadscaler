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

        private void pbBan_Click(object sender, EventArgs e)
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

       

    }
}
