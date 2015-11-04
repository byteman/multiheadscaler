namespace Monitor
{
    partial class FormPwdChange
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbOld = new System.Windows.Forms.TextBox();
            this.tbNew = new System.Windows.Forms.TextBox();
            this.rbDriver = new System.Windows.Forms.RadioButton();
            this.rbAdmin = new System.Windows.Forms.RadioButton();
            this.btnOld = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnRepeat = new System.Windows.Forms.Button();
            this.tbRpeat = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // pbReturn
            // 
            this.pbReturn.Location = new System.Drawing.Point(460, 314);
            this.pbReturn.Name = "pbReturn";
            this.pbReturn.Size = new System.Drawing.Size(133, 88);
            this.pbReturn.Click += new System.EventHandler(this.pbReturn_Click);
            this.pbReturn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbReturn_MouseDown);
            this.pbReturn.Paint += new System.Windows.Forms.PaintEventHandler(this.pbReturn_Paint);
            this.pbReturn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbReturn_MouseUp);
            // 
            // pbAck
            // 
            this.pbAck.Location = new System.Drawing.Point(194, 314);
            this.pbAck.Name = "pbAck";
            this.pbAck.Size = new System.Drawing.Size(133, 88);
            this.pbAck.Click += new System.EventHandler(this.pbAck_Click);
            this.pbAck.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbAck_MouseDown);
            this.pbAck.Paint += new System.Windows.Forms.PaintEventHandler(this.pbAck_Paint);
            this.pbAck.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbAck_MouseUp);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(18, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 36);
            this.label1.Text = "旧密码：";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(18, 171);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 36);
            this.label2.Text = "新密码：";
            // 
            // tbOld
            // 
            this.tbOld.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Bold);
            this.tbOld.Location = new System.Drawing.Point(194, 82);
            this.tbOld.Name = "tbOld";
            this.tbOld.Size = new System.Drawing.Size(399, 45);
            this.tbOld.TabIndex = 2;
            this.tbOld.Text = "0";
            // 
            // tbNew
            // 
            this.tbNew.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Bold);
            this.tbNew.Location = new System.Drawing.Point(194, 162);
            this.tbNew.Name = "tbNew";
            this.tbNew.Size = new System.Drawing.Size(399, 45);
            this.tbNew.TabIndex = 4;
            this.tbNew.Text = "0";
            // 
            // rbDriver
            // 
            this.rbDriver.Checked = true;
            this.rbDriver.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Bold);
            this.rbDriver.Location = new System.Drawing.Point(194, 18);
            this.rbDriver.Name = "rbDriver";
            this.rbDriver.Size = new System.Drawing.Size(172, 40);
            this.rbDriver.TabIndex = 0;
            this.rbDriver.Text = "驾驶员";
            // 
            // rbAdmin
            // 
            this.rbAdmin.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Bold);
            this.rbAdmin.Location = new System.Drawing.Point(440, 18);
            this.rbAdmin.Name = "rbAdmin";
            this.rbAdmin.Size = new System.Drawing.Size(153, 40);
            this.rbAdmin.TabIndex = 1;
            this.rbAdmin.TabStop = false;
            this.rbAdmin.Text = "管理员";
            // 
            // btnOld
            // 
            this.btnOld.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.btnOld.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Bold);
            this.btnOld.Location = new System.Drawing.Point(627, 87);
            this.btnOld.Name = "btnOld";
            this.btnOld.Size = new System.Drawing.Size(80, 40);
            this.btnOld.TabIndex = 3;
            this.btnOld.Text = "...";
            this.btnOld.Click += new System.EventHandler(this.btnOld_Click);
            // 
            // btnNew
            // 
            this.btnNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.btnNew.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Bold);
            this.btnNew.Location = new System.Drawing.Point(627, 167);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(80, 40);
            this.btnNew.TabIndex = 5;
            this.btnNew.Text = "...";
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnRepeat
            // 
            this.btnRepeat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.btnRepeat.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Bold);
            this.btnRepeat.Location = new System.Drawing.Point(627, 247);
            this.btnRepeat.Name = "btnRepeat";
            this.btnRepeat.Size = new System.Drawing.Size(80, 40);
            this.btnRepeat.TabIndex = 7;
            this.btnRepeat.Text = "...";
            this.btnRepeat.Click += new System.EventHandler(this.btnRepeat_Click);
            // 
            // tbRpeat
            // 
            this.tbRpeat.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Bold);
            this.tbRpeat.Location = new System.Drawing.Point(194, 242);
            this.tbRpeat.Name = "tbRpeat";
            this.tbRpeat.Size = new System.Drawing.Size(399, 45);
            this.tbRpeat.TabIndex = 6;
            this.tbRpeat.Text = "0";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(18, 251);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(192, 36);
            this.label3.Text = "重复新密码：";
            // 
            // FormPwdChange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(798, 407);
            this.Controls.Add(this.btnRepeat);
            this.Controls.Add(this.tbRpeat);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.btnOld);
            this.Controls.Add(this.rbAdmin);
            this.Controls.Add(this.rbDriver);
            this.Controls.Add(this.tbNew);
            this.Controls.Add(this.tbOld);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pbReturn);
            this.Controls.Add(this.pbAck);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(0, 48);
            this.Name = "FormPwdChange";
            this.Text = "FormPwdChange";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbReturn;
        private System.Windows.Forms.PictureBox pbAck;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbOld;
        private System.Windows.Forms.TextBox tbNew;
        private System.Windows.Forms.RadioButton rbDriver;
        private System.Windows.Forms.RadioButton rbAdmin;
        private System.Windows.Forms.Button btnOld;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnRepeat;
        private System.Windows.Forms.TextBox tbRpeat;
        private System.Windows.Forms.Label label3;
    }
}