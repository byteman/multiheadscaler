namespace Monitor
{
    partial class FormDataList
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
            this.pbExit = new System.Windows.Forms.PictureBox();
            this.ch_zuhe = new System.Windows.Forms.ColumnHeader();
            this.ch_index = new System.Windows.Forms.ColumnHeader();
            this.pbClear = new System.Windows.Forms.PictureBox();
            this.ch_datetime = new System.Windows.Forms.ColumnHeader();
            this.ch_weight = new System.Windows.Forms.ColumnHeader();
            this.pbNextPage = new System.Windows.Forms.PictureBox();
            this.ch_diff = new System.Windows.Forms.ColumnHeader();
            this.ch_result = new System.Windows.Forms.ColumnHeader();
            this.pbPrevPage = new System.Windows.Forms.PictureBox();
            this.tbTotalPage = new System.Windows.Forms.TextBox();
            this.tbDataTotal = new System.Windows.Forms.TextBox();
            this.tbCurPage = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // pbExit
            // 
            this.pbExit.Location = new System.Drawing.Point(584, 377);
            this.pbExit.Name = "pbExit";
            this.pbExit.Size = new System.Drawing.Size(164, 60);
            this.pbExit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbExit.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbBtn_MouseDown);
            this.pbExit.Paint += new System.Windows.Forms.PaintEventHandler(this.pbExit_Paint);
            this.pbExit.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbBtn_MouseUp);
            // 
            // ch_zuhe
            // 
            this.ch_zuhe.Text = "组合斗";
            this.ch_zuhe.Width = 150;
            // 
            // ch_index
            // 
            this.ch_index.Text = "序号";
            this.ch_index.Width = 60;
            // 
            // pbClear
            // 
            this.pbClear.Location = new System.Drawing.Point(399, 377);
            this.pbClear.Name = "pbClear";
            this.pbClear.Size = new System.Drawing.Size(164, 60);
            this.pbClear.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbClear.Click += new System.EventHandler(this.pbClear_Click);
            this.pbClear.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbBtn_MouseDown);
            this.pbClear.Paint += new System.Windows.Forms.PaintEventHandler(this.pbClear_Paint);
            this.pbClear.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbBtn_MouseUp);
            // 
            // ch_datetime
            // 
            this.ch_datetime.Text = "日期";
            this.ch_datetime.Width = 150;
            // 
            // ch_weight
            // 
            this.ch_weight.Text = "重量";
            this.ch_weight.Width = 60;
            // 
            // pbNextPage
            // 
            this.pbNextPage.Location = new System.Drawing.Point(212, 377);
            this.pbNextPage.Name = "pbNextPage";
            this.pbNextPage.Size = new System.Drawing.Size(164, 60);
            this.pbNextPage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbNextPage.Click += new System.EventHandler(this.pbNextPage_Click);
            this.pbNextPage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbBtn_MouseDown);
            this.pbNextPage.Paint += new System.Windows.Forms.PaintEventHandler(this.pbNextPage_Paint);
            this.pbNextPage.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbBtn_MouseUp);
            // 
            // ch_diff
            // 
            this.ch_diff.Text = "偏差";
            this.ch_diff.Width = 60;
            // 
            // ch_result
            // 
            this.ch_result.Text = "结果";
            this.ch_result.Width = 60;
            // 
            // pbPrevPage
            // 
            this.pbPrevPage.Location = new System.Drawing.Point(29, 377);
            this.pbPrevPage.Name = "pbPrevPage";
            this.pbPrevPage.Size = new System.Drawing.Size(164, 60);
            this.pbPrevPage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbPrevPage.Click += new System.EventHandler(this.pbPrevPage_Click);
            this.pbPrevPage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbBtn_MouseDown);
            this.pbPrevPage.Paint += new System.Windows.Forms.PaintEventHandler(this.pbPrevPage_Paint);
            this.pbPrevPage.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbBtn_MouseUp);
            // 
            // tbTotalPage
            // 
            this.tbTotalPage.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Bold);
            this.tbTotalPage.Location = new System.Drawing.Point(700, 293);
            this.tbTotalPage.Name = "tbTotalPage";
            this.tbTotalPage.Size = new System.Drawing.Size(69, 31);
            this.tbTotalPage.TabIndex = 15;
            // 
            // tbDataTotal
            // 
            this.tbDataTotal.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Bold);
            this.tbDataTotal.Location = new System.Drawing.Point(700, 186);
            this.tbDataTotal.Name = "tbDataTotal";
            this.tbDataTotal.Size = new System.Drawing.Size(68, 31);
            this.tbDataTotal.TabIndex = 13;
            // 
            // tbCurPage
            // 
            this.tbCurPage.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Bold);
            this.tbCurPage.Location = new System.Drawing.Point(700, 75);
            this.tbCurPage.Name = "tbCurPage";
            this.tbCurPage.Size = new System.Drawing.Size(68, 31);
            this.tbCurPage.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(639, 302);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 20);
            this.label3.Text = "总页面";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(637, 194);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 20);
            this.label2.Text = "记录数";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(637, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 20);
            this.label1.Text = "当前页";
            // 
            // listView1
            // 
            this.listView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.listView1.Columns.Add(this.ch_index);
            this.listView1.Columns.Add(this.ch_weight);
            this.listView1.Columns.Add(this.ch_diff);
            this.listView1.Columns.Add(this.ch_datetime);
            this.listView1.Columns.Add(this.ch_zuhe);
            this.listView1.Columns.Add(this.ch_result);
            this.listView1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular);
            this.listView1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.listView1.Location = new System.Drawing.Point(29, 21);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(602, 350);
            this.listView1.TabIndex = 8;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // FormDataList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(798, 455);
            this.Controls.Add(this.pbExit);
            this.Controls.Add(this.pbClear);
            this.Controls.Add(this.pbNextPage);
            this.Controls.Add(this.pbPrevPage);
            this.Controls.Add(this.tbTotalPage);
            this.Controls.Add(this.tbDataTotal);
            this.Controls.Add(this.tbCurPage);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormDataList";
            this.Text = "FormDataList";
            this.Load += new System.EventHandler(this.FormDataList_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbExit;
        private System.Windows.Forms.ColumnHeader ch_zuhe;
        private System.Windows.Forms.ColumnHeader ch_index;
        private System.Windows.Forms.PictureBox pbClear;
        private System.Windows.Forms.ColumnHeader ch_datetime;
        private System.Windows.Forms.ColumnHeader ch_weight;
        private System.Windows.Forms.PictureBox pbNextPage;
        private System.Windows.Forms.ColumnHeader ch_diff;
        private System.Windows.Forms.ColumnHeader ch_result;
        private System.Windows.Forms.PictureBox pbPrevPage;
        private System.Windows.Forms.TextBox tbTotalPage;
        private System.Windows.Forms.TextBox tbDataTotal;
        private System.Windows.Forms.TextBox tbCurPage;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView listView1;
    }
}