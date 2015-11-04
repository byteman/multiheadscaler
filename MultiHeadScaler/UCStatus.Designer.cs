namespace Monitor
{
    partial class UCStatus
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCStatus));
            this.pbBackground = new System.Windows.Forms.PictureBox();
            this.pbLock = new System.Windows.Forms.PictureBox();
            this.pbAlarm = new System.Windows.Forms.PictureBox();
            this.pbConnect = new System.Windows.Forms.PictureBox();
            this.pbFault = new System.Windows.Forms.PictureBox();
            this.pbGprs = new System.Windows.Forms.PictureBox();
            this.pbGps = new System.Windows.Forms.PictureBox();
            this.SuspendLayout();
            // 
            // pbBackground
            // 
            this.pbBackground.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbBackground.Image = ((System.Drawing.Image)(resources.GetObject("pbBackground.Image")));
            this.pbBackground.Location = new System.Drawing.Point(0, 0);
            this.pbBackground.Name = "pbBackground";
            this.pbBackground.Size = new System.Drawing.Size(800, 48);
            this.pbBackground.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbBackground.Click += new System.EventHandler(this.pbBackground_Click);
            this.pbBackground.Paint += new System.Windows.Forms.PaintEventHandler(this.pbBackground_Paint);
            // 
            // pbLock
            // 
            this.pbLock.BackColor = System.Drawing.Color.Transparent;
            this.pbLock.Image = ((System.Drawing.Image)(resources.GetObject("pbLock.Image")));
            this.pbLock.Location = new System.Drawing.Point(272, 1);
            this.pbLock.Name = "pbLock";
            this.pbLock.Size = new System.Drawing.Size(39, 42);
            // 
            // pbAlarm
            // 
            this.pbAlarm.BackColor = System.Drawing.Color.Transparent;
            this.pbAlarm.Image = ((System.Drawing.Image)(resources.GetObject("pbAlarm.Image")));
            this.pbAlarm.Location = new System.Drawing.Point(10, 1);
            this.pbAlarm.Name = "pbAlarm";
            this.pbAlarm.Size = new System.Drawing.Size(39, 42);
            // 
            // pbConnect
            // 
            this.pbConnect.BackColor = System.Drawing.Color.Transparent;
            this.pbConnect.Image = ((System.Drawing.Image)(resources.GetObject("pbConnect.Image")));
            this.pbConnect.Location = new System.Drawing.Point(317, 3);
            this.pbConnect.Name = "pbConnect";
            this.pbConnect.Size = new System.Drawing.Size(39, 39);
            // 
            // pbFault
            // 
            this.pbFault.BackColor = System.Drawing.Color.Transparent;
            this.pbFault.Image = ((System.Drawing.Image)(resources.GetObject("pbFault.Image")));
            this.pbFault.Location = new System.Drawing.Point(59, 1);
            this.pbFault.Name = "pbFault";
            this.pbFault.Size = new System.Drawing.Size(39, 42);
            // 
            // pbGprs
            // 
            this.pbGprs.BackColor = System.Drawing.Color.Transparent;
            this.pbGprs.Image = ((System.Drawing.Image)(resources.GetObject("pbGprs.Image")));
            this.pbGprs.Location = new System.Drawing.Point(362, 3);
            this.pbGprs.Name = "pbGprs";
            this.pbGprs.Size = new System.Drawing.Size(39, 39);
            // 
            // pbGps
            // 
            this.pbGps.Image = ((System.Drawing.Image)(resources.GetObject("pbGps.Image")));
            this.pbGps.Location = new System.Drawing.Point(407, 3);
            this.pbGps.Name = "pbGps";
            this.pbGps.Size = new System.Drawing.Size(39, 39);
            // 
            // UCStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.pbGps);
            this.Controls.Add(this.pbGprs);
            this.Controls.Add(this.pbFault);
            this.Controls.Add(this.pbConnect);
            this.Controls.Add(this.pbAlarm);
            this.Controls.Add(this.pbLock);
            this.Controls.Add(this.pbBackground);
            this.Name = "UCStatus";
            this.Size = new System.Drawing.Size(800, 48);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbBackground;
        private System.Windows.Forms.PictureBox pbLock;
        private System.Windows.Forms.PictureBox pbAlarm;
        private System.Windows.Forms.PictureBox pbConnect;
        private System.Windows.Forms.PictureBox pbFault;
        private System.Windows.Forms.PictureBox pbGprs;
        private System.Windows.Forms.PictureBox pbGps;
    }
}
