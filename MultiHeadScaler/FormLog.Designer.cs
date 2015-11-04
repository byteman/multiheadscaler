namespace Monitor
{
    partial class FormLog
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
            this.tbLog = new System.Windows.Forms.TextBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnQueryStatus = new System.Windows.Forms.Button();
            this.tbInfo = new System.Windows.Forms.TextBox();
            this.btnAddInfo = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbLog
            // 
            this.tbLog.Location = new System.Drawing.Point(4, 30);
            this.tbLog.Multiline = true;
            this.tbLog.Name = "tbLog";
            this.tbLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbLog.Size = new System.Drawing.Size(331, 182);
            this.tbLog.TabIndex = 0;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(341, 139);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(52, 33);
            this.btnClear.TabIndex = 1;
            this.btnClear.Text = "清空";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(341, 177);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(52, 33);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "关闭";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnPause
            // 
            this.btnPause.Location = new System.Drawing.Point(341, 99);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(52, 33);
            this.btnPause.TabIndex = 3;
            this.btnPause.Text = "暂停";
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnQueryStatus
            // 
            this.btnQueryStatus.ForeColor = System.Drawing.Color.Green;
            this.btnQueryStatus.Location = new System.Drawing.Point(341, 59);
            this.btnQueryStatus.Name = "btnQueryStatus";
            this.btnQueryStatus.Size = new System.Drawing.Size(52, 33);
            this.btnQueryStatus.TabIndex = 4;
            this.btnQueryStatus.Text = "定时开";
            this.btnQueryStatus.Click += new System.EventHandler(this.btnQueryStatus_Click);
            // 
            // tbInfo
            // 
            this.tbInfo.Location = new System.Drawing.Point(4, 3);
            this.tbInfo.Name = "tbInfo";
            this.tbInfo.ReadOnly = true;
            this.tbInfo.Size = new System.Drawing.Size(258, 23);
            this.tbInfo.TabIndex = 5;
            // 
            // btnAddInfo
            // 
            this.btnAddInfo.Location = new System.Drawing.Point(269, 1);
            this.btnAddInfo.Name = "btnAddInfo";
            this.btnAddInfo.Size = new System.Drawing.Size(66, 27);
            this.btnAddInfo.TabIndex = 6;
            this.btnAddInfo.Text = "内存";
            this.btnAddInfo.Click += new System.EventHandler(this.btnAddInfo_Click);
            // 
            // FormLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(398, 215);
            this.Controls.Add(this.btnAddInfo);
            this.Controls.Add(this.tbInfo);
            this.Controls.Add(this.btnQueryStatus);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.tbLog);
            this.MaximizeBox = false;
            this.Name = "FormLog";
            this.Text = "通信诊断";
            this.TopMost = true;
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormLog_MouseUp);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormLog_MouseDown);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.FormLog_Closing);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormLog_MouseMove);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox tbLog;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnQueryStatus;
        private System.Windows.Forms.TextBox tbInfo;
        private System.Windows.Forms.Button btnAddInfo;
    }
}