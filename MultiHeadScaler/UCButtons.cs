using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;

namespace Monitor
{        
    public partial class UCButtons : UserControl
    {
        FormFrame formFrame = null;
        Panel panel = null;

        Bitmap bmUpUp = null;
        Bitmap bmUpDown = null;

        Bitmap bmDownUp = null;
        Bitmap bmDownDown = null;

        Bitmap bmAckUp = null;
        Bitmap bmAckDown = null;

        Bitmap bmReturnUp = null;
        Bitmap bmReturnDown = null;

        string strAckBtn = "确认";
        string strRetBtn = "返回";
        string strExtBtn = "下载";

        public delegate void ButtonClickEvent();
        ButtonClickEvent clickUp = null;
        ButtonClickEvent clickDown = null;
        ButtonClickEvent clickAck = null;
        ButtonClickEvent clickReturn = null;
        ButtonClickEvent clickExt = null;

        public UCButtons(FormFrame _formFrame, Panel _panel)
        {
            InitializeComponent();
            formFrame = _formFrame;
            panel = _panel ;

            bmUpUp = GetBitmap(formFrame.configManage.FileDir + @"\north_btn_up.png");
            bmUpDown = GetBitmap(formFrame.configManage.FileDir + @"\north_btn_down.png");

            bmDownUp = GetBitmap(formFrame.configManage.FileDir + @"\south_btn_up.png");
            bmDownDown = GetBitmap(formFrame.configManage.FileDir + @"\south_btn_down.png");

            bmAckUp = GetBitmap(formFrame.configManage.FileDir + @"\square_btn_up.png");
            bmAckDown = GetBitmap(formFrame.configManage.FileDir + @"\square_btn_down.png");

            bmReturnUp = GetBitmap(formFrame.configManage.FileDir + @"\square_btn_up.png");
            bmReturnDown = GetBitmap(formFrame.configManage.FileDir + @"\square_btn_down.png");

            pbUp.Image = bmUpUp;
            pbDown.Image = bmDownUp;
            pbAck.Image = bmAckUp;
            pbReturn.Image = bmReturnUp;
            pbExt.Image = bmReturnUp;
        }

        public void RegisterBtnEvent(ButtonClickEvent up, ButtonClickEvent down, ButtonClickEvent ack, ButtonClickEvent ret,ButtonClickEvent ext)
        {
            clickUp = up;
            clickDown = down;
            clickAck = ack;
            clickReturn = ret;
            clickExt = ext;
        }

        public new void Dispose()
        {
            if (bmUpUp != null) bmUpUp.Dispose();
            if (bmUpDown != null) bmUpDown.Dispose();

            if (bmDownUp != null) bmDownUp.Dispose();
            if (bmDownDown != null) bmDownDown.Dispose();

            if (bmAckUp != null) bmAckUp.Dispose();
            if (bmAckDown != null) bmAckDown.Dispose();

            if (bmReturnUp != null) bmReturnUp.Dispose();
            if (bmReturnDown != null) bmReturnDown.Dispose();
            
            base.Dispose();
        }

        private Bitmap GetBitmap(string upPath)
        {
            Bitmap bm = null;
            if (File.Exists(upPath))
            {
                bm = new Bitmap(upPath);
            }
            return bm;
        }

        private void pbUp_Click(object sender, EventArgs e)
        {
            if (clickUp != null)  clickUp();
        }

        private void pbUp_MouseDown(object sender, MouseEventArgs e)
        {
            pbUp.Image = bmUpDown;
        }

        private void pbUp_MouseUp(object sender, MouseEventArgs e)
        {
            pbUp.Image = bmUpUp;
        }

        private void pbDown_Click(object sender, EventArgs e)
        {
            if (clickDown != null) clickDown();
        }

        private void pbDown_MouseDown(object sender, MouseEventArgs e)
        {
            pbDown.Image = bmDownDown;
        }

        private void pbDown_MouseUp(object sender, MouseEventArgs e)
        {
            pbDown.Image = bmDownUp;
        }

        private void pbAck_Click(object sender, EventArgs e)
        {
            if (clickAck != null) clickAck();
        }

        private void pbAck_MouseDown(object sender, MouseEventArgs e)
        {
            pbAck.Image = bmAckDown;
        }

        private void pbAck_MouseUp(object sender, MouseEventArgs e)
        {
            pbAck.Image = bmAckUp;
        }

        private void pbAck_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawString(strAckBtn, new Font("宋体", 24, FontStyle.Bold), new SolidBrush(Color.Black), 32, 20);
        }

        private void pbReturn_Click(object sender, EventArgs e)
        {
            if (clickReturn != null) clickReturn();
            else   formFrame.ShowUC(formFrame.ucMain);
        }

        private void pbReturn_MouseDown(object sender, MouseEventArgs e)
        {
            pbReturn.Image = bmReturnDown;
        }

        private void pbReturn_MouseUp(object sender, MouseEventArgs e)
        {
            pbReturn.Image = bmReturnUp;
        }

        private void pbReturn_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawString(strRetBtn, new Font("宋体", 24, FontStyle.Bold), new SolidBrush(Color.Black), 32, 20);
        }

        public void SetPageCode(int cur, int total)
        {
            cur++;      //起始页程序中为0开始，显示从1开始
            if (total < 1) total = 1;
           
            lbPage.Text = cur.ToString() + " / " + total.ToString();
        }

        public void SetAckVisible(bool bVisible)
        {
            pbAck.Visible = bVisible;
        }

        public void SetAckText(string text)
        {
            strAckBtn = text;
        }
        public void SetExtVisible(bool bVisible)
        {
            pbExt.Visible = bVisible;
        }
        private void pbExt_Click(object sender, EventArgs e)
        {
            if (clickExt != null) clickExt();
        }

        private void pbExt_MouseDown(object sender, MouseEventArgs e)
        {
            pbExt.Image = bmReturnDown;
        }

        private void pbExt_MouseUp(object sender, MouseEventArgs e)
        {
            pbExt.Image = bmReturnUp;
        }

        private void pbExt_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawString(strExtBtn, new Font("宋体", 24, FontStyle.Bold), new SolidBrush(Color.Black), 32, 20);
        }
    }
}
