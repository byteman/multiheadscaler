namespace Monitor
{
    partial class UCDebug
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
            this.pbClear = new System.Windows.Forms.PictureBox();
            this.pbBan = new System.Windows.Forms.PictureBox();
            this.pbStep = new System.Windows.Forms.PictureBox();
            this.pbShake = new System.Windows.Forms.PictureBox();
            this.pbEmpty = new System.Windows.Forms.PictureBox();
            this.pbContinuStep = new System.Windows.Forms.PictureBox();
            this.pbStop = new System.Windows.Forms.PictureBox();
            this.pbExit = new System.Windows.Forms.PictureBox();
            this.tb_number = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // banOcxCtl1
            // 
            this.banOcxCtl1.BackColor = System.Drawing.Color.Black;
            this.banOcxCtl1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular);
            this.banOcxCtl1.Location = new System.Drawing.Point(35, 38);
            this.banOcxCtl1.Name = "banOcxCtl1";
            this.banOcxCtl1.Size = new System.Drawing.Size(442, 409);
            this.banOcxCtl1.TabIndex = 27;
            this.banOcxCtl1.中心点击区半径 = 40;
            this.banOcxCtl1.字母S的大小 = 40F;
            this.banOcxCtl1.字母S的颜色 = System.Drawing.Color.Green;
            this.banOcxCtl1.斗区的颜色 = System.Drawing.Color.YellowGreen;
            this.banOcxCtl1.斗区线条的粗线 = 1F;
            this.banOcxCtl1.斗区线条的颜色 = System.Drawing.Color.White;
            this.banOcxCtl1.磅称中心点坐标 = new System.Drawing.Point(220, 200);
            this.banOcxCtl1.磅称的半径 = 180;
            this.banOcxCtl1.磅称的数量 = 10;
            this.banOcxCtl1.磅称的间隔弧度 = 5;
            this.banOcxCtl1.称状态字体大小 = 15F;
            this.banOcxCtl1.称状态字体颜色 = System.Drawing.Color.Red;
            this.banOcxCtl1.称重量字体大小 = 10F;
            this.banOcxCtl1.称重量字体颜色 = System.Drawing.Color.White;
            this.banOcxCtl1.编号字体大小 = 10F;
            this.banOcxCtl1.编号字体颜色 = System.Drawing.Color.Black;
            this.banOcxCtl1.背景颜色 = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
            // 
            // pbClear
            // 
            this.pbClear.Location = new System.Drawing.Point(483, 119);
            this.pbClear.Name = "pbClear";
            this.pbClear.Size = new System.Drawing.Size(150, 50);
            this.pbClear.Click += new System.EventHandler(this.pbClear_Click);
            this.pbClear.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbBtn_MouseDown);
            this.pbClear.Paint += new System.Windows.Forms.PaintEventHandler(this.pbClear_Paint);
            this.pbClear.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbBtn_MouseUp);
            // 
            // pbBan
            // 
            this.pbBan.Location = new System.Drawing.Point(639, 119);
            this.pbBan.Name = "pbBan";
            this.pbBan.Size = new System.Drawing.Size(150, 50);
            this.pbBan.Click += new System.EventHandler(this.pbBan_Click);
            this.pbBan.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbBtn_MouseDown);
            this.pbBan.Paint += new System.Windows.Forms.PaintEventHandler(this.pbBan_Paint);
            this.pbBan.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbBtn_MouseUp);
            // 
            // pbStep
            // 
            this.pbStep.Location = new System.Drawing.Point(639, 196);
            this.pbStep.Name = "pbStep";
            this.pbStep.Size = new System.Drawing.Size(150, 50);
            this.pbStep.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbBtn_MouseDown);
            this.pbStep.Paint += new System.Windows.Forms.PaintEventHandler(this.pbStep_Paint);
            this.pbStep.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbBtn_MouseUp);
            // 
            // pbShake
            // 
            this.pbShake.Location = new System.Drawing.Point(483, 196);
            this.pbShake.Name = "pbShake";
            this.pbShake.Size = new System.Drawing.Size(150, 50);
            this.pbShake.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbBtn_MouseDown);
            this.pbShake.Paint += new System.Windows.Forms.PaintEventHandler(this.pbShake_Paint);
            this.pbShake.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbBtn_MouseUp);
            // 
            // pbEmpty
            // 
            this.pbEmpty.Location = new System.Drawing.Point(639, 268);
            this.pbEmpty.Name = "pbEmpty";
            this.pbEmpty.Size = new System.Drawing.Size(150, 50);
            this.pbEmpty.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbBtn_MouseDown);
            this.pbEmpty.Paint += new System.Windows.Forms.PaintEventHandler(this.pbEmpty_Paint);
            this.pbEmpty.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbBtn_MouseUp);
            // 
            // pbContinuStep
            // 
            this.pbContinuStep.Location = new System.Drawing.Point(483, 268);
            this.pbContinuStep.Name = "pbContinuStep";
            this.pbContinuStep.Size = new System.Drawing.Size(150, 50);
            this.pbContinuStep.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbBtn_MouseDown);
            this.pbContinuStep.Paint += new System.Windows.Forms.PaintEventHandler(this.pbContinuStep_Paint);
            this.pbContinuStep.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbBtn_MouseUp);
            // 
            // pbStop
            // 
            this.pbStop.Location = new System.Drawing.Point(483, 339);
            this.pbStop.Name = "pbStop";
            this.pbStop.Size = new System.Drawing.Size(150, 50);
            this.pbStop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbBtn_MouseDown);
            this.pbStop.Paint += new System.Windows.Forms.PaintEventHandler(this.pbStop_Paint);
            this.pbStop.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbBtn_MouseUp);
            // 
            // pbExit
            // 
            this.pbExit.Location = new System.Drawing.Point(639, 339);
            this.pbExit.Name = "pbExit";
            this.pbExit.Size = new System.Drawing.Size(150, 50);
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
            this.tb_number.Location = new System.Drawing.Point(525, 58);
            this.tb_number.Name = "tb_number";
            this.tb_number.Size = new System.Drawing.Size(100, 31);
            this.tb_number.TabIndex = 46;
            this.tb_number.Text = "1";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(661, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 20);
            this.label3.Text = "号斗";
            // 
            // UCDebug
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tb_number);
            this.Controls.Add(this.pbExit);
            this.Controls.Add(this.pbStop);
            this.Controls.Add(this.pbEmpty);
            this.Controls.Add(this.pbContinuStep);
            this.Controls.Add(this.pbStep);
            this.Controls.Add(this.pbShake);
            this.Controls.Add(this.pbBan);
            this.Controls.Add(this.pbClear);
            this.Controls.Add(this.banOcxCtl1);
            this.Name = "UCDebug";
            this.Size = new System.Drawing.Size(800, 480);
            this.ResumeLayout(false);

        }

        #endregion

        private BanOcx.BanOcxCtl banOcxCtl1;
        private System.Windows.Forms.PictureBox pbClear;
        private System.Windows.Forms.PictureBox pbBan;
        private System.Windows.Forms.PictureBox pbStep;
        private System.Windows.Forms.PictureBox pbShake;
        private System.Windows.Forms.PictureBox pbEmpty;
        private System.Windows.Forms.PictureBox pbContinuStep;
        private System.Windows.Forms.PictureBox pbStop;
        private System.Windows.Forms.PictureBox pbExit;
        private System.Windows.Forms.TextBox tb_number;
        private System.Windows.Forms.Label label3;
    }
}
