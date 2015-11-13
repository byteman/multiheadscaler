namespace Monitor
{
    partial class FormPicture
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
            this.pbNext = new System.Windows.Forms.PictureBox();
            this.pbExit = new System.Windows.Forms.PictureBox();
            this.pbAck = new System.Windows.Forms.PictureBox();
            this.pbPrev = new System.Windows.Forms.PictureBox();
            this.SuspendLayout();
            // 
            // pbNext
            // 
            this.pbNext.Location = new System.Drawing.Point(219, 364);
            this.pbNext.Name = "pbNext";
            this.pbNext.Size = new System.Drawing.Size(133, 88);
            this.pbNext.Click += new System.EventHandler(this.pbNext_Click);
            this.pbNext.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pbNext.Paint += new System.Windows.Forms.PaintEventHandler(this.pbNext_Paint);
            this.pbNext.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // pbExit
            // 
            this.pbExit.Location = new System.Drawing.Point(610, 364);
            this.pbExit.Name = "pbExit";
            this.pbExit.Size = new System.Drawing.Size(133, 88);
            this.pbExit.Click += new System.EventHandler(this.pbExit_Click);
            this.pbExit.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pbExit.Paint += new System.Windows.Forms.PaintEventHandler(this.pbExit_Paint);
            this.pbExit.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // pbAck
            // 
            this.pbAck.Location = new System.Drawing.Point(411, 364);
            this.pbAck.Name = "pbAck";
            this.pbAck.Size = new System.Drawing.Size(133, 88);
            this.pbAck.Click += new System.EventHandler(this.pbAck_Click);
            this.pbAck.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pbAck.Paint += new System.Windows.Forms.PaintEventHandler(this.pbAck_Paint);
            this.pbAck.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // pbPrev
            // 
            this.pbPrev.Location = new System.Drawing.Point(29, 364);
            this.pbPrev.Name = "pbPrev";
            this.pbPrev.Size = new System.Drawing.Size(133, 88);
            this.pbPrev.Click += new System.EventHandler(this.pbPrev_Click);
            this.pbPrev.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pbPrev.Paint += new System.Windows.Forms.PaintEventHandler(this.pbPrev_Paint);
            this.pbPrev.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // FormPicture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(798, 455);
            this.Controls.Add(this.pbPrev);
            this.Controls.Add(this.pbAck);
            this.Controls.Add(this.pbExit);
            this.Controls.Add(this.pbNext);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormPicture";
            this.Text = "FormPicture";
            this.Load += new System.EventHandler(this.FormPicture_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbNext;
        private System.Windows.Forms.PictureBox pbExit;
        private System.Windows.Forms.PictureBox pbAck;
        private System.Windows.Forms.PictureBox pbPrev;
    }
}