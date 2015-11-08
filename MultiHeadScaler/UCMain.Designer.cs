namespace Monitor
{
    partial class UCMain
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
            this.pbRun = new System.Windows.Forms.PictureBox();
            this.pbCalib = new System.Windows.Forms.PictureBox();
            this.pbHandDebug = new System.Windows.Forms.PictureBox();
            this.pbTongji = new System.Windows.Forms.PictureBox();
            this.pbParam = new System.Windows.Forms.PictureBox();
            this.banOcxCtl1 = new BanOcx.BanOcxCtl();
            this.pbZero = new System.Windows.Forms.PictureBox();
            this.SuspendLayout();
            // 
            // pbRun
            // 
            this.pbRun.Location = new System.Drawing.Point(624, 37);
            this.pbRun.Name = "pbRun";
            this.pbRun.Size = new System.Drawing.Size(164, 60);
            this.pbRun.Click += new System.EventHandler(this.pbRun_Click);
            this.pbRun.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbBtn_MouseDown);
            this.pbRun.Paint += new System.Windows.Forms.PaintEventHandler(this.pbRun_Paint);
            this.pbRun.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbBtn_MouseUp);
            // 
            // pbCalib
            // 
            this.pbCalib.Location = new System.Drawing.Point(624, 108);
            this.pbCalib.Name = "pbCalib";
            this.pbCalib.Size = new System.Drawing.Size(164, 60);
            this.pbCalib.Click += new System.EventHandler(this.pbCalib_Click);
            this.pbCalib.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbBtn_MouseDown);
            this.pbCalib.Paint += new System.Windows.Forms.PaintEventHandler(this.pbCalib_Paint);
            this.pbCalib.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbBtn_MouseUp);
            // 
            // pbHandDebug
            // 
            this.pbHandDebug.Location = new System.Drawing.Point(624, 179);
            this.pbHandDebug.Name = "pbHandDebug";
            this.pbHandDebug.Size = new System.Drawing.Size(164, 60);
            this.pbHandDebug.Click += new System.EventHandler(this.pbHandDebug_Click);
            this.pbHandDebug.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbBtn_MouseDown);
            this.pbHandDebug.Paint += new System.Windows.Forms.PaintEventHandler(this.pbHandDebug_Paint);
            this.pbHandDebug.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbBtn_MouseUp);
            // 
            // pbTongji
            // 
            this.pbTongji.Location = new System.Drawing.Point(624, 250);
            this.pbTongji.Name = "pbTongji";
            this.pbTongji.Size = new System.Drawing.Size(164, 60);
            this.pbTongji.Click += new System.EventHandler(this.pbTongji_Click);
            this.pbTongji.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbBtn_MouseDown);
            this.pbTongji.Paint += new System.Windows.Forms.PaintEventHandler(this.pbTongji_Paint);
            this.pbTongji.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbBtn_MouseUp);
            // 
            // pbParam
            // 
            this.pbParam.Location = new System.Drawing.Point(624, 321);
            this.pbParam.Name = "pbParam";
            this.pbParam.Size = new System.Drawing.Size(164, 60);
            this.pbParam.Click += new System.EventHandler(this.pbParam_Click);
            this.pbParam.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbBtn_MouseDown);
            this.pbParam.Paint += new System.Windows.Forms.PaintEventHandler(this.pbParam_Paint);
            this.pbParam.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbBtn_MouseUp);
            // 
            // banOcxCtl1
            // 
            this.banOcxCtl1.BackColor = System.Drawing.Color.Black;
            this.banOcxCtl1.Location = new System.Drawing.Point(120, 37);
            this.banOcxCtl1.Name = "banOcxCtl1";
            this.banOcxCtl1.Size = new System.Drawing.Size(486, 417);
            this.banOcxCtl1.TabIndex = 7;
            this.banOcxCtl1.中心点击区半径 = 40;
            this.banOcxCtl1.字母S的大小 = 40F;
            this.banOcxCtl1.字母S的颜色 = System.Drawing.Color.Green;
            this.banOcxCtl1.斗区的颜色 = System.Drawing.Color.YellowGreen;
            this.banOcxCtl1.斗区线条的粗线 = 1F;
            this.banOcxCtl1.斗区线条的颜色 = System.Drawing.Color.White;
            this.banOcxCtl1.磅称中心点坐标 = new System.Drawing.Point(220, 200);
            this.banOcxCtl1.磅称的半径 = 200;
            this.banOcxCtl1.磅称的数量 = 10;
            this.banOcxCtl1.磅称的间隔弧度 = 10;
            this.banOcxCtl1.称状态字体大小 = 15F;
            this.banOcxCtl1.称状态字体颜色 = System.Drawing.Color.Black;
            this.banOcxCtl1.称重量字体大小 = 10F;
            this.banOcxCtl1.称重量字体颜色 = System.Drawing.Color.White;
            this.banOcxCtl1.编号字体大小 = 10F;
            this.banOcxCtl1.编号字体颜色 = System.Drawing.Color.Black;
            this.banOcxCtl1.背景颜色 = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
            // 
            // pbZero
            // 
            this.pbZero.Location = new System.Drawing.Point(624, 390);
            this.pbZero.Name = "pbZero";
            this.pbZero.Size = new System.Drawing.Size(164, 60);
            this.pbZero.Click += new System.EventHandler(this.pbSetzero_Click);
            this.pbZero.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbBtn_MouseDown);
            this.pbZero.Paint += new System.Windows.Forms.PaintEventHandler(this.pbSetzero_Paint);
            this.pbZero.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbBtn_MouseUp);
            // 
            // UCMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.pbZero);
            this.Controls.Add(this.banOcxCtl1);
            this.Controls.Add(this.pbParam);
            this.Controls.Add(this.pbTongji);
            this.Controls.Add(this.pbHandDebug);
            this.Controls.Add(this.pbCalib);
            this.Controls.Add(this.pbRun);
            this.Name = "UCMain";
            this.Size = new System.Drawing.Size(800, 480);
            this.Click += new System.EventHandler(this.UCMain_Click);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbRun;
        private System.Windows.Forms.PictureBox pbCalib;
        private System.Windows.Forms.PictureBox pbHandDebug;
        private System.Windows.Forms.PictureBox pbTongji;
        private System.Windows.Forms.PictureBox pbParam;
        private BanOcx.BanOcxCtl banOcxCtl1;
        private System.Windows.Forms.PictureBox pbZero;
    }
}
