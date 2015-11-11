namespace Monitor
{
    partial class UCDBList
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
            this.pnLeft = new System.Windows.Forms.Panel();
            this.pnRight = new System.Windows.Forms.Panel();
            this.pbLeft = new System.Windows.Forms.PictureBox();
            this.pbRight = new System.Windows.Forms.PictureBox();
            this.pnLeft.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnLeft
            // 
            this.pnLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.pnLeft.Controls.Add(this.pbRight);
            this.pnLeft.Controls.Add(this.pbLeft);
            this.pnLeft.Location = new System.Drawing.Point(28, 57);
            this.pnLeft.Name = "pnLeft";
            this.pnLeft.Size = new System.Drawing.Size(616, 375);
            this.pnLeft.Paint += new System.Windows.Forms.PaintEventHandler(this.pnLeft_Paint);
            this.pnLeft.Click += new System.EventHandler(this.pnLeft_Click);
            this.pnLeft.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnLeft_MouseDown);
            // 
            // pnRight
            // 
            this.pnRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.pnRight.Location = new System.Drawing.Point(650, 0);
            this.pnRight.Name = "pnRight";
            this.pnRight.Size = new System.Drawing.Size(150, 432);
            // 
            // pbLeft
            // 
            this.pbLeft.Location = new System.Drawing.Point(1, 1);
            this.pbLeft.Name = "pbLeft";
            this.pbLeft.Size = new System.Drawing.Size(74, 90);
            this.pbLeft.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbLeft.Click += new System.EventHandler(this.pbLeft_Click);
            this.pbLeft.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbLeft_MouseDown);
            this.pbLeft.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbLeft_MouseUp);
            // 
            // pbRight
            // 
            this.pbRight.Location = new System.Drawing.Point(541, 1);
            this.pbRight.Name = "pbRight";
            this.pbRight.Size = new System.Drawing.Size(74, 90);
            this.pbRight.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbRight.Click += new System.EventHandler(this.pbRight_Click);
            this.pbRight.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbRight_MouseDown);
            this.pbRight.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbRight_MouseUp);
            // 
            // UCDBList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.pnRight);
            this.Controls.Add(this.pnLeft);
            this.Name = "UCDBList";
            this.Size = new System.Drawing.Size(800, 480);
            this.pnLeft.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnLeft;
        private System.Windows.Forms.Panel pnRight;
        private System.Windows.Forms.PictureBox pbLeft;
        private System.Windows.Forms.PictureBox pbRight;
    }
}
