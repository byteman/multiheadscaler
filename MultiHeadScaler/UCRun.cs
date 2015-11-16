﻿using System;
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
            

        }
        /*
         
        #define	SCALE_NUM_MAX			10
        typedef struct {
            u8		scale_num;	//总的秤个数
            u8		qualified;	//本次组合结果 合格与否 1:合格 0: 不合格[可能是强排之类]
            u8		comb_heads[SCALE_NUM_MAX]; //组合斗编号，表示参与组合的斗数，没有参与组合的斗数，我自己来计算
            u8		state[SCALE_NUM_MAX];	// 秤头状态,具体主动发送时个数由设置的秤台数量决定
            float	wet[SCALE_NUM_MAX];		// 各个秤头的当前重量
            u32		quali;					// 合格数
            u32		unquali;				// 不合格数
            float	quali_wet;				// 本次合格的总重量,不管合格与否，都有一个重量

        }resultDef;
         * 
         */
        class MultiScalerInfo
        {
            public MultiScalerInfo()
            { 
            
            }
            public byte scale_num = 10;
            public byte qualified = 0;
            public byte[] comb_heads = new byte[10];
            public byte[] state = new byte[10];
            public float[] wet = new float[10];
            public UInt32 quali;
            public UInt32 unquali;
            public float  qual_wet; 
        }
        private void parseInfo(object o)
        {
            MultiScalerInfo info = new MultiScalerInfo();

            lbl_hege.Text = "合格数: " + info.quali.ToString();
            lbl_unhege.Text = "不合格数: " + info.unquali.ToString();
            txb_wet.Text = info.qual_wet.ToString("0.0");

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
        public Image GetPicBitmap(int id)
        {
            string path = String.Format(formFrame.configManage.FileDir + @"\formula\{0}.jpg", id);
            return GetBitmap(path);
        }
        internal void SetReturnValue(List<ParamItem> itemList)
        {
            foreach (ParamItem item in itemList)
            {
                if (item.dev_id == 128) //控制器的命令回应
                {
                    if (item.param_id == 2) //组合结果
                    {
                        parseInfo(item.param_value);
                        
                        //item.param_value;
                    }
                    else if (item.param_id == 1) //启动停止反馈.
                    {

                    }
                }
            }
        }

        private void banOcxCtl1_Validated(object sender, EventArgs e)
        { 
            //MessageBox.Show("banOcxCtl1_Validated");
        }

        private void banOcxCtl1_Resize(object sender, EventArgs e)
        {
            //
            
        }

        private void UCRun_Resize(object sender, EventArgs e)
        {
            
        }

        private void UCRun_GotFocus(object sender, EventArgs e)
        {
            
          
        }

        internal void Init()
        {
            pbName.Image = GetPicBitmap(formFrame.FormulaID);
            textBox1.Text = formFrame.curFormula.target_weight.ToString();
            textBox2.Text = formFrame.curFormula.up_diff.ToString();
            textBox3.Text = formFrame.curFormula.down_diff.ToString();
        }
    }
}
