using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Monitor
{
    class FormStyle
    {
        Form formHost = null;               //引用包含本实例的窗体对象
        public UCButtons ucButtons;
        public Panel pnLeft;
        private Panel pnRight;

        public void SetStyle(FormFrame formFrame, Form form)
        {
            formHost = form;

            //new class
            pnLeft = new System.Windows.Forms.Panel();
            pnRight = new System.Windows.Forms.Panel();
            ucButtons = new UCButtons(formFrame, pnRight);

            //pnLeft
            pnLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            pnLeft.Location = new System.Drawing.Point(28, 24);
            pnLeft.Name = "pnLeft";
            pnLeft.Size = new System.Drawing.Size(616, 380);

            //pnRight
            pnRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            pnRight.Location = new System.Drawing.Point(650, 0);
            pnRight.Name = "pnRight";
            pnRight.Size = new System.Drawing.Size(150, 432);

            //ucButtons
            ucButtons.SetAckVisible(true);
            ucButtons.RegisterBtnEvent(ClickUp, ClickDown, ClickAck, ClickReturn);
            pnRight.Controls.Add(ucButtons);

            //form
            form.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            form.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            form.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
            form.ClientSize = new System.Drawing.Size(798, 407);
            form.Controls.Add(this.pnRight);
            form.Controls.Add(this.pnLeft);
            form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            form.Location = new System.Drawing.Point(0, 48);
        }

        private void ClickUp()
        {
        }

        private void ClickDown()
        {
        }

        private void ClickAck()
        { }

        private void ClickReturn()
        {
            formHost.Close();
        }
    }
}
