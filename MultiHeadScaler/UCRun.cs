using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace Monitor
{
    public partial class UCRun : UserControl
    {
        private FormFrame formFrame = null;
        private Bitmap bmBtnDown = null;
        private Bitmap bmBtnUp = null;
        private List<TextBox> focusBox;
        private int index = 0;
        public UCRun(FormFrame f)
        {
            InitializeComponent();
            formFrame = f;
            if (focusBox == null)
            {
                focusBox = new List<TextBox>();
                focusBox.Add(textBox5);
                focusBox.Add(textBox6);
                focusBox.Add(textBox7);
                focusBox.Add(textBox8);
                focusBox.Add(textBox9);
                focusBox.Add(textBox10);
                focusBox.Add(textBox11);
                focusBox.Add(textBox12);
                focusBox.Add(textBox13);
                focusBox.Add(textBox14);


            }
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

            pbStart.Image = bmBtnUp;
            pbStop.Image  = bmBtnUp;
            pbExit.Image = bmBtnUp;
            pbSimu.Image = bmBtnUp;


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

        private void pbStart_Paint(object sender, PaintEventArgs e)
        {
            DrawLabel(sender, e, "启动");
        }

        private void pbStop_Paint(object sender, PaintEventArgs e)
        {
            DrawLabel(sender, e, "停止");
        }

        private void pbStop_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

        private void pbExit_Paint(object sender, PaintEventArgs e)
        {
            DrawLabel(sender, e, "退出");
        }

        private void pbExit_Click(object sender, EventArgs e)
        {
            formFrame.ShowUC(formFrame.ucMain);
        }

        private void label4_ParentChanged(object sender, EventArgs e)
        {

        }

        private void pbSimu_Paint(object sender, PaintEventArgs e)
        {
            DrawLabel(sender, e, "模拟运行");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            banOcxCtl1.SetBanColor(index++ % banOcxCtl1.磅称的数量, Color.Red);
        }

        private void pbStart_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void textBox5_GotFocus(object sender, EventArgs e)
        {
            if (sender == textBox1)
            { 
                
            }
            else if (sender == textBox2)
            {

            }
        }
        private TextBox getFocusTextBox()
        {
            foreach (TextBox b in focusBox)
            {
                if (b.Focused) return b;
            }
            return null;

        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            TextBox box = getFocusTextBox();
            if (box != null)
            { 
                Int32 v = Convert.ToInt32(box.Text);

                box.Text = (v + 1).ToString();
            }
        }

        private void btnSub_Click(object sender, EventArgs e)
        {
            TextBox box = getFocusTextBox();
            if (box != null)
            {
                Int32 v = Convert.ToInt32(box.Text);

                box.Text = (v - 1).ToString();
            }
            FormMsgBox.Show("我的老婆是李晓荣", "tip");

        }
    }
}
