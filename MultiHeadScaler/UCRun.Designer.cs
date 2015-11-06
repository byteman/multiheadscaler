namespace Monitor
{
    partial class UCRun
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.banOcxCtl1 = new BanOcx.BanOcxCtl();
            this.pbStart = new System.Windows.Forms.PictureBox();
            this.pbStop = new System.Windows.Forms.PictureBox();
            this.pbExit = new System.Windows.Forms.PictureBox();
            this.pbName = new System.Windows.Forms.PictureBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.pbSimu = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer();
            this.SuspendLayout();
            // 
            // banOcxCtl1
            // 
            this.banOcxCtl1.BackColor = System.Drawing.Color.Black;
            this.banOcxCtl1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular);
            this.banOcxCtl1.Location = new System.Drawing.Point(196, 49);
            this.banOcxCtl1.Name = "banOcxCtl1";
            this.banOcxCtl1.Size = new System.Drawing.Size(442, 409);
            this.banOcxCtl1.TabIndex = 8;
            this.banOcxCtl1.中心点击区半径 = 40;
            this.banOcxCtl1.字母S的大小 = 40F;
            this.banOcxCtl1.字母S的颜色 = System.Drawing.Color.Green;
            this.banOcxCtl1.斗区的颜色 = System.Drawing.Color.YellowGreen;
            this.banOcxCtl1.斗区线条的粗线 = 1F;
            this.banOcxCtl1.斗区线条的颜色 = System.Drawing.Color.White;
            this.banOcxCtl1.磅称中心点坐标 = new System.Drawing.Point(220, 200);
            this.banOcxCtl1.磅称的半径 = 180;
            this.banOcxCtl1.磅称的数量 = 10;
            this.banOcxCtl1.磅称的间隔弧度 = 10;
            this.banOcxCtl1.编号字体大小 = 15F;
            this.banOcxCtl1.编号字体颜色 = System.Drawing.Color.Black;
            this.banOcxCtl1.背景颜色 = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
            // 
            // pbStart
            // 
            this.pbStart.Location = new System.Drawing.Point(644, 13);
            this.pbStart.Name = "pbStart";
            this.pbStart.Size = new System.Drawing.Size(141, 60);
            this.pbStart.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbStart.Click += new System.EventHandler(this.pbStart_Click);
            this.pbStart.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbBtn_MouseDown);
            this.pbStart.Paint += new System.Windows.Forms.PaintEventHandler(this.pbStart_Paint);
            this.pbStart.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbBtn_MouseUp);
            // 
            // pbStop
            // 
            this.pbStop.Location = new System.Drawing.Point(644, 79);
            this.pbStop.Name = "pbStop";
            this.pbStop.Size = new System.Drawing.Size(141, 60);
            this.pbStop.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbStop.Click += new System.EventHandler(this.pbStop_Click);
            this.pbStop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbBtn_MouseDown);
            this.pbStop.Paint += new System.Windows.Forms.PaintEventHandler(this.pbStop_Paint);
            this.pbStop.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbBtn_MouseUp);
            // 
            // pbExit
            // 
            this.pbExit.Location = new System.Drawing.Point(644, 411);
            this.pbExit.Name = "pbExit";
            this.pbExit.Size = new System.Drawing.Size(141, 60);
            this.pbExit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbExit.Click += new System.EventHandler(this.pbExit_Click);
            this.pbExit.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbBtn_MouseDown);
            this.pbExit.Paint += new System.Windows.Forms.PaintEventHandler(this.pbExit_Paint);
            this.pbExit.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbBtn_MouseUp);
            // 
            // pbName
            // 
            this.pbName.Location = new System.Drawing.Point(3, 13);
            this.pbName.Name = "pbName";
            this.pbName.Size = new System.Drawing.Size(108, 60);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Bold);
            this.textBox1.ForeColor = System.Drawing.Color.Blue;
            this.textBox1.Location = new System.Drawing.Point(83, 95);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(83, 31);
            this.textBox1.TabIndex = 11;
            this.textBox1.Text = "123";
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Bold);
            this.textBox2.ForeColor = System.Drawing.Color.Blue;
            this.textBox2.Location = new System.Drawing.Point(83, 140);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(83, 31);
            this.textBox2.TabIndex = 11;
            this.textBox2.Text = "0.1";
            // 
            // textBox3
            // 
            this.textBox3.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Bold);
            this.textBox3.ForeColor = System.Drawing.Color.Blue;
            this.textBox3.Location = new System.Drawing.Point(83, 182);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(83, 31);
            this.textBox3.TabIndex = 11;
            this.textBox3.Text = "0";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(0, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 20);
            this.label1.Text = "目标重量";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(0, 140);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 20);
            this.label2.Text = "上偏差";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(0, 185);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 20);
            this.label3.Text = "下偏差";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(3, 225);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 20);
            this.label4.Text = "状态";
            this.label4.ParentChanged += new System.EventHandler(this.label4_ParentChanged);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(0, 260);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 20);
            this.label5.Text = "合格";
            this.label5.ParentChanged += new System.EventHandler(this.label4_ParentChanged);
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(0, 293);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 20);
            this.label6.Text = "不合格";
            this.label6.ParentChanged += new System.EventHandler(this.label4_ParentChanged);
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(0, 331);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 20);
            this.label7.Text = "平均组合";
            this.label7.ParentChanged += new System.EventHandler(this.label4_ParentChanged);
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(0, 371);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 20);
            this.label8.Text = "运行速度";
            this.label8.ParentChanged += new System.EventHandler(this.label4_ParentChanged);
            // 
            // pbSimu
            // 
            this.pbSimu.Location = new System.Drawing.Point(7, 411);
            this.pbSimu.Name = "pbSimu";
            this.pbSimu.Size = new System.Drawing.Size(148, 56);
            this.pbSimu.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbSimu.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbBtn_MouseDown);
            this.pbSimu.Paint += new System.Windows.Forms.PaintEventHandler(this.pbSimu_Paint);
            this.pbSimu.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbBtn_MouseUp);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // UCRun
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.pbName);
            this.Controls.Add(this.pbSimu);
            this.Controls.Add(this.pbExit);
            this.Controls.Add(this.pbStop);
            this.Controls.Add(this.pbStart);
            this.Controls.Add(this.banOcxCtl1);
            this.Name = "UCRun";
            this.Size = new System.Drawing.Size(800, 480);
            this.ResumeLayout(false);

        }

        #endregion

        private BanOcx.BanOcxCtl banOcxCtl1;
        private System.Windows.Forms.PictureBox pbStart;
        private System.Windows.Forms.PictureBox pbStop;
        private System.Windows.Forms.PictureBox pbExit;
        private System.Windows.Forms.PictureBox pbName;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.PictureBox pbSimu;
        private System.Windows.Forms.Timer timer1;
    }
}
