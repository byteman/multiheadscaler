using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Monitor
{
    public partial class UCMenu : UserControl
    {
        protected FormFrame formFrame = null;
        protected UCButtons ucButtons = null;
        Bitmap bmUp = null;
        Bitmap bmDown = null;

        public UCMenu(FormFrame f)
        {
            InitializeComponent();
            formFrame = f;
            ucButtons = new UCButtons(f, this.pnRight);
            this.pnRight.Controls.Add(ucButtons);

            string path = formFrame.configManage.FileDir + @"\rect_btn_up.png";
            if (File.Exists(path))
            {
                bmUp = new Bitmap(path);
            }
            path = formFrame.configManage.FileDir + @"\rect_btn_down.png";
            if (File.Exists(path))
            {
                bmDown = new Bitmap(path);
            }

            pictureBox1.Image = bmUp;
            pictureBox2.Image = bmUp;
            pictureBox3.Image = bmUp;
            pictureBox4.Image = bmUp;
            pictureBox5.Image = bmUp;
            pictureBox6.Image = bmUp;

            pictureBox1.Tag = 0;
            pictureBox2.Tag = 1;
            pictureBox3.Tag = 2;
            pictureBox4.Tag = 3;
            pictureBox5.Tag = 4;
            pictureBox6.Tag = 5;
        }

        public new void Dispose()
        {
            ucButtons.Dispose();
            if (bmUp != null) bmUp.Dispose();
            if (bmDown != null) bmDown.Dispose();
            base.Dispose();
        }

        private void pnLeft_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(Color.Blue), 0, 0, pnLeft.Width - 1, pnLeft.Height - 1);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            ((PictureBox)sender).Image = bmDown;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            ((PictureBox)sender).Image = bmUp;
        }

        public virtual void pictureBox1_Paint(object sender, PaintEventArgs e)
        {

        }

        public virtual void pictureBox1_Click(object sender, EventArgs e)
        {
        
        }
    }
}
