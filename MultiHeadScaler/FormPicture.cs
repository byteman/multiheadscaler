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
        private int pic_page = 0;
        private int pic_total = 0;
        private int pic_total_page = 0;
        private int select_pic = -1;
        private FormFrame formFrame = null;
        private List<PictureBox> pbx = new List<PictureBox>();
        Bitmap bmUp = null;
        Bitmap bmDown = null;
        
        // 这里写你的目录
        private int getFileCount(string path)
        {
            int FileCount = 0;
            DirectoryInfo Dir = new DirectoryInfo(path);
            foreach (FileInfo FI in Dir.GetFiles())
            {
                // 这里写文件格式
                if (System.IO.Path.GetExtension(FI.Name) == ".jpg")
                {
                    FileCount++;
                }
            }
            return FileCount;
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
        public string GetSelectPicture()
        { 
            if(select_pic!=-1)
            {
               return String.Format(formFrame.configManage.FileDir + @"\formula\{0}.jpg", select_pic);
            }
            return null;
        }
        private void clear_select()
        {
            Graphics g = this.CreateGraphics();
            Pen myPen = new Pen(Color.FromArgb(205, 244, 255), 4);
            foreach (PictureBox pb in pbx)
            {
                g.DrawRectangle(myPen, pb.Left - 2, pb.Top - 2, pb.Width + 4, pb.Height + 4);
            }
            g.Dispose();
            select_pic = -1;
        }
        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            Graphics g = this.CreateGraphics();
            
            clear_select();
            PictureBox pb2 = (PictureBox)sender;
            Pen myPen = new Pen(Color.Red,  4);

            g.DrawRectangle(myPen, pb2.Left - 2, pb2.Top - 2, pb2.Width + 4, pb2.Height + 4);
            g.Dispose();
            select_pic = (int)pb2.Tag + pic_page * pbx_number;

            //MessageBox.Show(select_pic.ToString());

        }
        private void  load_pic(int page)
        {
            int start = page*pbx_number+1;
         
            foreach (PictureBox pb in pbx)
            {
                if (start <= pic_total)
                {
                    string path = String.Format(formFrame.configManage.FileDir + @"\formula\{0}.jpg", start++);
                    pb.Image = GetBitmap(path);
                }
                else
                    pb.Image = null;
            }
          
        }
        public FormPicture(FormFrame f)
        {
            InitializeComponent();
            formFrame = f;

            
            pic_total = getFileCount(formFrame.configManage.FileDir + @"\formula\");
            pic_total_page = (pic_total + pbx_number - 1) / pbx_number;

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
                //pb.Image = bmp;
                pb.Tag = i+1;
                pbx.Add(pb);
                this.Controls.Add(pb);
            }
            load_pic(0);

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
            e.Graphics.DrawString(str, new Font("宋体", 16, FontStyle.Bold), new SolidBrush(Color.Black), 45, 35);
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
            pic_page--;
            if (pic_page < 0)
            {
                pic_page++;
                return;
            }
            clear_select();
            load_pic(pic_page);
        }

        private void pbAck_Click(object sender, EventArgs e)
        {
            Close();
        }
        public int GetSelectPicID()
        {
            return select_pic;
        }
        private void pbExit_Click(object sender, EventArgs e)
        {
            select_pic = -1;
            Close();
        }

        private void pbNext_Click(object sender, EventArgs e)
        {
            pic_page++;
            if (pic_page >= pic_total_page)
            {
                pic_page--;
                return;
            }
            clear_select();
            load_pic(pic_page);
        
        }


    }
}