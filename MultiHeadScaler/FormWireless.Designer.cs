namespace Monitor
{
    partial class FormWireless
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
            this.pbReturn = new System.Windows.Forms.PictureBox();
            this.pbAck = new System.Windows.Forms.PictureBox();
            this.SuspendLayout();
            // 
            // pbReturn
            // 
            this.pbReturn.Location = new System.Drawing.Point(173, 267);
            this.pbReturn.Name = "pbReturn";
            this.pbReturn.Size = new System.Drawing.Size(133, 88);
            this.pbReturn.Click += new System.EventHandler(this.pbReturn_Click);
            this.pbReturn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbReturn_MouseDown);
            this.pbReturn.Paint += new System.Windows.Forms.PaintEventHandler(this.pbReturn_Paint);
            this.pbReturn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbReturn_MouseUp);
            // 
            // pbAck
            // 
            this.pbAck.Location = new System.Drawing.Point(497, 267);
            this.pbAck.Name = "pbAck";
            this.pbAck.Size = new System.Drawing.Size(133, 88);
            this.pbAck.Click += new System.EventHandler(this.pbAck_Click);
            this.pbAck.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbAck_MouseDown);
            this.pbAck.Paint += new System.Windows.Forms.PaintEventHandler(this.pbAck_Paint);
            this.pbAck.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbAck_MouseUp);
            // 
            // FormWireless
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(798, 407);
            this.Controls.Add(this.pbReturn);
            this.Controls.Add(this.pbAck);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(0, 48);
            this.Name = "FormWireless";
            this.Text = "FormWireless";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbReturn;
        private System.Windows.Forms.PictureBox pbAck;
    }
}