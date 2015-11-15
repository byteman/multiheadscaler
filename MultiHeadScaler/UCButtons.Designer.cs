namespace Monitor
{
    partial class UCButtons
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
            this.lbPage = new System.Windows.Forms.Label();
            this.pbUp = new System.Windows.Forms.PictureBox();
            this.pbDown = new System.Windows.Forms.PictureBox();
            this.pbAck = new System.Windows.Forms.PictureBox();
            this.pbReturn = new System.Windows.Forms.PictureBox();
            this.pbExt = new System.Windows.Forms.PictureBox();
            this.SuspendLayout();
            // 
            // lbPage
            // 
            this.lbPage.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular);
            this.lbPage.Location = new System.Drawing.Point(15, 82);
            this.lbPage.Name = "lbPage";
            this.lbPage.Size = new System.Drawing.Size(112, 31);
            this.lbPage.Text = "1/1";
            this.lbPage.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pbUp
            // 
            this.pbUp.Location = new System.Drawing.Point(7, 8);
            this.pbUp.Name = "pbUp";
            this.pbUp.Size = new System.Drawing.Size(133, 69);
            this.pbUp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbUp.Click += new System.EventHandler(this.pbUp_Click);
            this.pbUp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbUp_MouseDown);
            this.pbUp.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbUp_MouseUp);
            // 
            // pbDown
            // 
            this.pbDown.Location = new System.Drawing.Point(7, 121);
            this.pbDown.Name = "pbDown";
            this.pbDown.Size = new System.Drawing.Size(133, 71);
            this.pbDown.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbDown.Click += new System.EventHandler(this.pbDown_Click);
            this.pbDown.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbDown_MouseDown);
            this.pbDown.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbDown_MouseUp);
            // 
            // pbAck
            // 
            this.pbAck.Location = new System.Drawing.Point(7, 206);
            this.pbAck.Name = "pbAck";
            this.pbAck.Size = new System.Drawing.Size(133, 72);
            this.pbAck.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbAck.Visible = false;
            this.pbAck.Click += new System.EventHandler(this.pbAck_Click);
            this.pbAck.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbAck_MouseDown);
            this.pbAck.Paint += new System.Windows.Forms.PaintEventHandler(this.pbAck_Paint);
            this.pbAck.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbAck_MouseUp);
            // 
            // pbReturn
            // 
            this.pbReturn.Location = new System.Drawing.Point(7, 295);
            this.pbReturn.Name = "pbReturn";
            this.pbReturn.Size = new System.Drawing.Size(133, 69);
            this.pbReturn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbReturn.Click += new System.EventHandler(this.pbReturn_Click);
            this.pbReturn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbReturn_MouseDown);
            this.pbReturn.Paint += new System.Windows.Forms.PaintEventHandler(this.pbReturn_Paint);
            this.pbReturn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbReturn_MouseUp);
            // 
            // pbExt
            // 
            this.pbExt.Location = new System.Drawing.Point(7, 384);
            this.pbExt.Name = "pbExt";
            this.pbExt.Size = new System.Drawing.Size(133, 69);
            this.pbExt.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbExt.Visible = false;
            this.pbExt.Click += new System.EventHandler(this.pbExt_Click);
            this.pbExt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbExt_MouseDown);
            this.pbExt.Paint += new System.Windows.Forms.PaintEventHandler(this.pbExt_Paint);
            this.pbExt.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbExt_MouseUp);
            // 
            // UCButtons
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.pbExt);
            this.Controls.Add(this.pbReturn);
            this.Controls.Add(this.pbAck);
            this.Controls.Add(this.pbDown);
            this.Controls.Add(this.pbUp);
            this.Controls.Add(this.lbPage);
            this.Name = "UCButtons";
            this.Size = new System.Drawing.Size(150, 480);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbPage;
        private System.Windows.Forms.PictureBox pbUp;
        private System.Windows.Forms.PictureBox pbDown;
        private System.Windows.Forms.PictureBox pbAck;
        private System.Windows.Forms.PictureBox pbReturn;
        private System.Windows.Forms.PictureBox pbExt;
    }
}
