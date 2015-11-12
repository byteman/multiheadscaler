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
    public partial class FormPicture : Form
    {
        private const int pbx_number = 6;
        private const int pbx_row_number = 3;
        private const int pbx_col_number = 3;
        private const int pb_width = 160;
        private const int pb_height = 160;
        private FormFrame formFrame = null;
        private List<PictureBox> pbx = new List<PictureBox>();
        Bitmap bmUp = null;
        Bitmap bmDown = null;
        private Bitmap GetBitmap(string upPath)
        {
            Bitmap bm = null;
            if (File.Exists(upPath))
            {
                bm = new Bitmap(upPath);
            }
            return bm;
        }
        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            Graphics g = this.CreateGraphics();
            Pen myPen = new Pen(Color.FromArgb(205, 244, 255), 2);
            foreach (PictureBox pb in pbx)
            { 
                
                g.DrawRectangle(myPen, pb.Left - 2, pb.Top - 2, pb.Width + 4, pb.Height + 4);
            }
            PictureBox pb2 = (PictureBox)sender;
            myPen.Color = Color.Red;
            g.DrawRectangle(myPen, pb2.Left - 2, pb2.Top - 2, pb2.Width + 4, pb2.Height + 4);
            g.Dispose();
        }
        public FormPicture(FormFrame f)
        {
            InitializeComponent();
            formFrame = f;
            int left = (Width - pbx_row_number * pb_width + 5 - 100) / 2;
            int top = 10;// (Height - pbx_col_number * pb_height + 5 - 100) / 2;
            Bitmap bmp = GetBitmap(formFrame.configManage.FileDir + @"\north_btn_up.png");
            for (int i = 0; i < pbx_number; i++)
            {
                PictureBox pb = new PictureBox();
                pb.Width = pb_width;
                pb.Height = pb_height;

                pb.Left = left + (i % pbx_row_number) * (pb.Width + 5);
                pb.Top  = top + (i / pbx_col_number) * (pb.Height + 5);

                pb.SizeMode = PictureBoxSizeMode.StretchImage;
                pb.MouseDown += pictureBox_MouseDown;
                pb.Image = bmp;
                pbx.Add(pb);
                this.Controls.Add(pb);
            }
            string path = formFrame.configManage.FileDir + @"\square_btn_up.png";
            if (File.Exists(path))
            {
                bmUp = new Bitmap(path);
            }
            path = formFrame.configManage.FileDir + @"\square_btn_down.png";
            if (File.Exists(path))
            {
                bmDown = new Bitmap(path);
            }

            pbPrev.Image = bmUp;
            pbNext.Image = bmUp;
            pbAck.Image = bmUp;
            pbExit.Image = bmUp;
            
        }

        private void FormPicture_Load(object sender, EventArgs e)
        {

        }

        public new void Dispose()
        {
           
            if (bmUp != null) bmUp.Dispose();
            if (bmDown != null) bmDown.Dispose();
            base.Dispose();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            ((PictureBox)sender).Image = bmDown;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            ((PictureBox)sender).Image = bmUp;
        }
        private void DrawLabel(object sender, PaintEventArgs e, string str)
        {
            e.Graphics.DrawString(str, new Font("宋体", 16, FontStyle.Bold), new SolidBrush(Color.Black), 50, 35);
        }
        private void pbPrev_Paint(object sender, PaintEventArgs e)
        {
            DrawLabel(sender,  e,"上一页");
        }

        private void pbNext_Paint(object sender, PaintEventArgs e)
        {
            DrawLabel(sender, e, "下一页");
        }

        private void pbAck_Paint(object sender, PaintEventArgs e)
        {
            DrawLabel(sender, e, "确定");
        }

        private void pbExit_Paint(object sender, PaintEventArgs e)
        {
            DrawLabel(sender, e, "取消");
        }

        private void pbPrev_Click(object sender, EventArgs e)
        {
            //
        }


    }
}