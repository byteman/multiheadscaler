using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Monitor
{
    public partial class FormWireless : Form
    {
        private FormFrame formFrame = null;
        public bool bAck;

        Bitmap bmAckUp = null;
        Bitmap bmAckDown = null;

        Bitmap bmReturnUp = null;
        Bitmap bmReturnDown = null;

        string strAckBtn = "确认";
        string strRetBtn = "取消";

        public FormWireless(FormFrame f)
        {
            InitializeComponent();
            formFrame = f;
            bmAckUp = GetBitmap(formFrame.configManage.FileDir + @"\square_btn_up.png");
            bmAckDown = GetBitmap(formFrame.configManage.FileDir + @"\square_btn_down.png");

            bmReturnUp = GetBitmap(formFrame.configManage.FileDir + @"\square_btn_up.png");
            bmReturnDown = GetBitmap(formFrame.configManage.FileDir + @"\square_btn_down.png");

            pbAck.Image = bmAckUp;
            pbReturn.Image = bmReturnUp;
        }

        public new void Dispose()
        {
            if (bmAckUp != null) bmAckUp.Dispose();
            if (bmAckDown != null) bmAckDown.Dispose();

            if (bmReturnUp != null) bmReturnUp.Dispose();
            if (bmReturnDown != null) bmReturnDown.Dispose();

            base.Dispose();
        }

        private Bitmap GetBitmap(string upPath)
        {
            Bitmap bm = null;
            if (System.IO.File.Exists(upPath))
            {
                bm = new Bitmap(upPath);
            }
            return bm;
        }

        private void pbReturn_MouseUp(object sender, MouseEventArgs e)
        {
            pbReturn.Image = bmReturnUp;
        }

        private void pbReturn_MouseDown(object sender, MouseEventArgs e)
        {
            pbReturn.Image = bmReturnDown;
        }

        private void pbReturn_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawString(strRetBtn, new Font("宋体", 24, FontStyle.Bold), new SolidBrush(Color.Black), 32, 28);
        }

        private void pbReturn_Click(object sender, EventArgs e)
        {
            bAck = false;
            this.Close();
        }

        private void pbAck_MouseUp(object sender, MouseEventArgs e)
        {
            pbAck.Image = bmAckUp;
        }

        private void pbAck_MouseDown(object sender, MouseEventArgs e)
        {
            pbAck.Image = bmAckDown;
        }

        private void pbAck_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawString(strAckBtn, new Font("宋体", 24, FontStyle.Bold), new SolidBrush(Color.Black), 32, 28);
        }

        private void pbAck_Click(object sender, EventArgs e)
        {
            bAck = true;
        }
    }
}