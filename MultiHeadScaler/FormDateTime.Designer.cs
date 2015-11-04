namespace Monitor
{
    partial class FormDateTime
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
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.pnRight = new System.Windows.Forms.Panel();
            this.pnLeft = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.cbSecond = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbMinute = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbHour = new System.Windows.Forms.ComboBox();
            this.pnLeft.SuspendLayout();
            this.SuspendLayout();
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.CalendarFont = new System.Drawing.Font("Tahoma", 26F, System.Drawing.FontStyle.Regular);
            this.dateTimePicker.Font = new System.Drawing.Font("Tahoma", 36F, System.Drawing.FontStyle.Regular);
            this.dateTimePicker.Location = new System.Drawing.Point(57, 89);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(518, 65);
            this.dateTimePicker.TabIndex = 0;
            // 
            // pnRight
            // 
            this.pnRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.pnRight.Location = new System.Drawing.Point(650, 0);
            this.pnRight.Name = "pnRight";
            this.pnRight.Size = new System.Drawing.Size(150, 432);
            // 
            // pnLeft
            // 
            this.pnLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.pnLeft.Controls.Add(this.label3);
            this.pnLeft.Controls.Add(this.cbSecond);
            this.pnLeft.Controls.Add(this.label2);
            this.pnLeft.Controls.Add(this.cbMinute);
            this.pnLeft.Controls.Add(this.label1);
            this.pnLeft.Controls.Add(this.cbHour);
            this.pnLeft.Controls.Add(this.dateTimePicker);
            this.pnLeft.Location = new System.Drawing.Point(28, 34);
            this.pnLeft.Name = "pnLeft";
            this.pnLeft.Size = new System.Drawing.Size(616, 380);
            this.pnLeft.Paint += new System.Windows.Forms.PaintEventHandler(this.pnLeft_Paint);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 36F, System.Drawing.FontStyle.Regular);
            this.label3.Location = new System.Drawing.Point(520, 237);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 64);
            this.label3.Text = "秒";
            // 
            // cbSecond
            // 
            this.cbSecond.Font = new System.Drawing.Font("Tahoma", 36F, System.Drawing.FontStyle.Regular);
            this.cbSecond.Location = new System.Drawing.Point(413, 237);
            this.cbSecond.Name = "cbSecond";
            this.cbSecond.Size = new System.Drawing.Size(100, 64);
            this.cbSecond.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 36F, System.Drawing.FontStyle.Regular);
            this.label2.Location = new System.Drawing.Point(342, 237);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 64);
            this.label2.Text = "分";
            // 
            // cbMinute
            // 
            this.cbMinute.Font = new System.Drawing.Font("Tahoma", 36F, System.Drawing.FontStyle.Regular);
            this.cbMinute.Location = new System.Drawing.Point(235, 237);
            this.cbMinute.Name = "cbMinute";
            this.cbMinute.Size = new System.Drawing.Size(100, 64);
            this.cbMinute.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 36F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(164, 237);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 64);
            this.label1.Text = "时";
            // 
            // cbHour
            // 
            this.cbHour.Font = new System.Drawing.Font("Tahoma", 36F, System.Drawing.FontStyle.Regular);
            this.cbHour.Location = new System.Drawing.Point(57, 237);
            this.cbHour.Name = "cbHour";
            this.cbHour.Size = new System.Drawing.Size(100, 64);
            this.cbHour.TabIndex = 1;
            // 
            // FormDateTime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(798, 407);
            this.Controls.Add(this.pnRight);
            this.Controls.Add(this.pnLeft);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(0, 48);
            this.Name = "FormDateTime";
            this.Text = "FormDateTime";
            this.pnLeft.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePicker;
        private System.Windows.Forms.Panel pnRight;
        private System.Windows.Forms.Panel pnLeft;
        private System.Windows.Forms.ComboBox cbHour;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbSecond;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbMinute;
        private System.Windows.Forms.Label label1;


    }
}