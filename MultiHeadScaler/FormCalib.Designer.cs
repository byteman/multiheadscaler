namespace Monitor
{
    partial class FormCalib
    {
        /// <summary>
        /// ����������������
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// ������������ʹ�õ���Դ��
        /// </summary>
        /// <param name="disposing">���Ӧ�ͷ��й���Դ��Ϊ true������Ϊ false��</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows ������������ɵĴ���

        /// <summary>
        /// �����֧������ķ��� - ��Ҫ
        /// ʹ�ô���༭���޸Ĵ˷��������ݡ�
        /// </summary>
        private void InitializeComponent()
        {
            this.dgCalib = new System.Windows.Forms.DataGrid();
            this.SuspendLayout();
            // 
            // dgCalib
            // 
            this.dgCalib.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.dgCalib.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular);
            this.dgCalib.HeaderBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.dgCalib.Location = new System.Drawing.Point(31, 43);
            this.dgCalib.Name = "dgCalib";
            this.dgCalib.Size = new System.Drawing.Size(441, 227);
            this.dgCalib.TabIndex = 0;
            this.dgCalib.CurrentCellChanged += new System.EventHandler(this.dgCalib_CurrentCellChanged);
            // 
            // FormCalib
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(503, 293);
            this.Controls.Add(this.dgCalib);
            this.Name = "FormCalib";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGrid dgCalib;

    }
}
