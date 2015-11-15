namespace Monitor
{
    partial class FormMyCalib
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.banOcxCtl1 = new BanOcx.BanOcxCtl();
            this.pbClear = new System.Windows.Forms.PictureBox();
            this.pbZero = new System.Windows.Forms.PictureBox();
            this.pbCalib = new System.Windows.Forms.PictureBox();
            this.pbExit = new System.Windows.Forms.PictureBox();
            this.tb_number = new System.Windows.Forms.TextBox();
            this.tb_fama = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // banOcxCtl1
            // 
            this.banOcxCtl1.BackColor = System.Drawing.Color.Black;
            this.banOcxCtl1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular);
            this.banOcxCtl1.Location = new System.Drawing.Point(47, 14);
            this.banOcxCtl1.Name = "banOcxCtl1";
            this.banOcxCtl1.Size = new System.Drawing.Size(442, 409);
            this.banOcxCtl1.TabIndex = 9;
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
            this.banOcxCtl1.称状态字体大小 = 15F;
            this.banOcxCtl1.称状态字体颜色 = System.Drawing.Color.White;
            this.banOcxCtl1.称重量字体大小 = 10F;
            this.banOcxCtl1.称重量字体颜色 = System.Drawing.Color.White;
            this.banOcxCtl1.编号字体大小 = 15F;
            this.banOcxCtl1.编号字体颜色 = System.Drawing.Color.Black;
            this.banOcxCtl1.背景颜色 = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
            this.banOcxCtl1.点击事件 += new BanOcx.BanOcxCtl.MyEventHandler(this.banOcxCtl1_点击事件);
            // 
            // pbClear
            // 
            this.pbClear.Location = new System.Drawing.Point(574, 157);
            this.pbClear.Name = "pbClear";
            this.pbClear.Size = new System.Drawing.Size(164, 70);
            this.pbClear.Click += new System.EventHandler(this.pbClear_Click);
            this.pbClear.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbBtn_MouseDown);
            this.pbClear.Paint += new System.Windows.Forms.PaintEventHandler(this.pbClear_Paint);
            this.pbClear.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbBtn_MouseUp);
            // 
            // pbZero
            // 
            this.pbZero.Location = new System.Drawing.Point(574, 231);
            this.pbZero.Name = "pbZero";
            this.pbZero.Size = new System.Drawing.Size(164, 70);
            this.pbZero.Click += new System.EventHandler(this.pbZero_Click);
            this.pbZero.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbBtn_MouseDown);
            this.pbZero.Paint += new System.Windows.Forms.PaintEventHandler(this.pbZero_Paint);
            this.pbZero.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbBtn_MouseUp);
            // 
            // pbCalib
            // 
            this.pbCalib.Location = new System.Drawing.Point(574, 304);
            this.pbCalib.Name = "pbCalib";
            this.pbCalib.Size = new System.Drawing.Size(164, 70);
            this.pbCalib.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbBtn_MouseDown);
            this.pbCalib.Paint += new System.Windows.Forms.PaintEventHandler(this.pbCalib_Paint);
            this.pbCalib.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbBtn_MouseUp);
            // 
            // pbExit
            // 
            this.pbExit.Location = new System.Drawing.Point(574, 377);
            this.pbExit.Name = "pbExit";
            this.pbExit.Size = new System.Drawing.Size(164, 70);
            this.pbExit.Click += new System.EventHandler(this.pbExit_Click);
            this.pbExit.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbBtn_MouseDown);
            this.pbExit.Paint += new System.Windows.Forms.PaintEventHandler(this.pbExit_Paint);
            this.pbExit.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbBtn_MouseUp);
            // 
            // tb_number
            // 
            this.tb_number.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_number.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Bold);
            this.tb_number.Location = new System.Drawing.Point(582, 51);
            this.tb_number.Name = "tb_number";
            this.tb_number.Size = new System.Drawing.Size(100, 31);
            this.tb_number.TabIndex = 17;
            this.tb_number.Text = "1";
            this.tb_number.TextChanged += new System.EventHandler(this.tb_number_TextChanged);
            this.tb_number.GotFocus += new System.EventHandler(this.tb_number_GotFocus);
            // 
            // tb_fama
            // 
            this.tb_fama.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_fama.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Bold);
            this.tb_fama.Location = new System.Drawing.Point(582, 120);
            this.tb_fama.Name = "tb_fama";
            this.tb_fama.Size = new System.Drawing.Size(100, 31);
            this.tb_fama.TabIndex = 17;
            this.tb_fama.Text = "600";
            this.tb_fama.GotFocus += new System.EventHandler(this.tb_fama_GotFocus);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(578, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 20);
            this.label1.Text = "砝码重量";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(578, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 20);
            this.label2.Text = "选择";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(684, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 20);
            this.label3.Text = "号";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(688, 123);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 28);
            this.label4.Text = "g";
            // 
            // FormMyCalib
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(798, 455);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_fama);
            this.Controls.Add(this.tb_number);
            this.Controls.Add(this.pbExit);
            this.Controls.Add(this.pbCalib);
            this.Controls.Add(this.pbZero);
            this.Controls.Add(this.pbClear);
            this.Controls.Add(this.banOcxCtl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormMyCalib";
            this.Text = "FormMyCalib";
            this.Load += new System.EventHandler(this.FormMyCalib_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FormMyCalib_Paint);
            this.ResumeLayout(false);

        }

        #endregion

        private BanOcx.BanOcxCtl banOcxCtl1;
        private System.Windows.Forms.PictureBox pbClear;
        private System.Windows.Forms.PictureBox pbZero;
        private System.Windows.Forms.PictureBox pbCalib;
        private System.Windows.Forms.PictureBox pbExit;
        private System.Windows.Forms.TextBox tb_number;
        private System.Windows.Forms.TextBox tb_fama;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}