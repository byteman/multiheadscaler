namespace Monitor
{
    partial class FormInput
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
            this.lbInput = new System.Windows.Forms.Label();
            this.pnRight = new System.Windows.Forms.Panel();
            this.pnLeft.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnLeft
            // 
            this.pnLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.pnLeft.Controls.Add(this.lbInput);
            this.pnLeft.Location = new System.Drawing.Point(28, 24);
            this.pnLeft.Name = "pnLeft";
            this.pnLeft.Size = new System.Drawing.Size(616, 380);
            this.pnLeft.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnLeft_MouseDown);
            this.pnLeft.Paint += new System.Windows.Forms.PaintEventHandler(this.pnLeft_Paint);
            this.pnLeft.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnLeft_MouseUp);
            // 
            // lbInput
            // 
            this.lbInput.Font = new System.Drawing.Font("Tahoma", 28F, System.Drawing.FontStyle.Regular);
            this.lbInput.Location = new System.Drawing.Point(3, 105);
            this.lbInput.Name = "lbInput";
            this.lbInput.Size = new System.Drawing.Size(610, 92);
            this.lbInput.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pnRight
            // 
            this.pnRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.pnRight.Location = new System.Drawing.Point(650, 0);
            this.pnRight.Name = "pnRight";
            this.pnRight.Size = new System.Drawing.Size(150, 432);
            // 
            // FormInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(798, 407);
            this.Controls.Add(this.pnRight);
            this.Controls.Add(this.pnLeft);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(0, 48);
            this.Name = "FormInput";
            this.pnLeft.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnLeft;
        private System.Windows.Forms.Panel pnRight;
        private System.Windows.Forms.Label lbInput;
    }
}
