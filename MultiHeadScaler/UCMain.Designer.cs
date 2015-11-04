namespace Monitor
{
    partial class UCMain
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
            this.pbHold = new System.Windows.Forms.PictureBox();
            this.pbSetzero = new System.Windows.Forms.PictureBox();
            this.pbQuery = new System.Windows.Forms.PictureBox();
            this.pbSet = new System.Windows.Forms.PictureBox();
            this.pbExit = new System.Windows.Forms.PictureBox();
            this.SuspendLayout();
            // 
            // pbHold
            // 
            this.pbHold.Location = new System.Drawing.Point(622, 28);
            this.pbHold.Name = "pbHold";
            this.pbHold.Size = new System.Drawing.Size(164, 70);
            this.pbHold.Click += new System.EventHandler(this.pbHold_Click);
            this.pbHold.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbBtn_MouseDown);
            this.pbHold.Paint += new System.Windows.Forms.PaintEventHandler(this.pbHold_Paint);
            this.pbHold.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbBtn_MouseUp);
            // 
            // pbSetzero
            // 
            this.pbSetzero.Location = new System.Drawing.Point(622, 104);
            this.pbSetzero.Name = "pbSetzero";
            this.pbSetzero.Size = new System.Drawing.Size(164, 70);
            this.pbSetzero.Click += new System.EventHandler(this.pbSetzero_Click);
            this.pbSetzero.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbBtn_MouseDown);
            this.pbSetzero.Paint += new System.Windows.Forms.PaintEventHandler(this.pbSetzero_Paint);
            this.pbSetzero.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbBtn_MouseUp);
            // 
            // pbQuery
            // 
            this.pbQuery.Location = new System.Drawing.Point(622, 180);
            this.pbQuery.Name = "pbQuery";
            this.pbQuery.Size = new System.Drawing.Size(164, 70);
            this.pbQuery.Click += new System.EventHandler(this.pbQuery_Click);
            this.pbQuery.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbBtn_MouseDown);
            this.pbQuery.Paint += new System.Windows.Forms.PaintEventHandler(this.pbQuery_Paint);
            this.pbQuery.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbBtn_MouseUp);
            // 
            // pbSet
            // 
            this.pbSet.Location = new System.Drawing.Point(622, 256);
            this.pbSet.Name = "pbSet";
            this.pbSet.Size = new System.Drawing.Size(164, 70);
            this.pbSet.Click += new System.EventHandler(this.pbSet_Click);
            this.pbSet.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbBtn_MouseDown);
            this.pbSet.Paint += new System.Windows.Forms.PaintEventHandler(this.pbSet_Paint);
            this.pbSet.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbBtn_MouseUp);
            // 
            // pbExit
            // 
            this.pbExit.Location = new System.Drawing.Point(622, 332);
            this.pbExit.Name = "pbExit";
            this.pbExit.Size = new System.Drawing.Size(164, 70);
            this.pbExit.Click += new System.EventHandler(this.pbExit_Click);
            this.pbExit.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbBtn_MouseDown);
            this.pbExit.Paint += new System.Windows.Forms.PaintEventHandler(this.pbExit_Paint);
            this.pbExit.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbBtn_MouseUp);
            // 
            // UCMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.pbExit);
            this.Controls.Add(this.pbSet);
            this.Controls.Add(this.pbQuery);
            this.Controls.Add(this.pbSetzero);
            this.Controls.Add(this.pbHold);
            this.Name = "UCMain";
            this.Size = new System.Drawing.Size(800, 432);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbHold;
        private System.Windows.Forms.PictureBox pbSetzero;
        private System.Windows.Forms.PictureBox pbQuery;
        private System.Windows.Forms.PictureBox pbSet;
        private System.Windows.Forms.PictureBox pbExit;
    }
}
