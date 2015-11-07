using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.IO;
namespace Monitor
{
    public partial class FormMyCalib : Form
    {
        private FormFrame  formFrame;
        private Bitmap bmBtnDown = null;
        private Bitmap bmBtnUp = null;
        public FormMyCalib(FormFrame f)
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


        }

        private void FormMyCalib_Load(object sender, EventArgs e)
        {

        }
       
       
        private void FormMyCalib_Paint(object sender, PaintEventArgs e)
        {
            //
            //FillRoundRectangle(e.Graphics, Brushes.Plum, new Rectangle(100, 100, 100, 100), 8);
           // DrawRoundRectangle(e.Graphics, Pens.Yellow, new Rectangle(100, 100, 100, 100), 8);
            //e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(85, 142, 213)), 580, 20, 750, 450);
            //e.Graphics.DrawString(strTitle, new Font("宋体", 32, FontStyle.Bold), new SolidBrush(Color.Black), title_left, title_height / 2 - 16);

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

        private void pictureBox2_Click(object sender, EventArgs e)
        {

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
            Close();
        }

        private void banOcxCtl1_点击事件(object sender, BanOcx.MyEventArges e)
        {
            tb_number.Text = e.SelNum.ToString();
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
    }
}