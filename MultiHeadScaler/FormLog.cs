using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Monitor
{
    public partial class FormLog : Form
    {
        FormFrame formFrame;
        bool bPause = false;
        int LeftTolerance = 0;
        int TopTolerance = 0;
        Point formLocation = new Point(0, 0);

        public FormLog(FormFrame f)
        {
            InitializeComponent();
            formFrame = f;
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            if (bPause)
            {
                btnPause.Text = "暂停";
                bPause = false;
            } 
            else
            {
                btnPause.Text = "开始";
                bPause = true;
            }

            if (formFrame.bQueryStatusOn)
            {
                btnQueryStatus.Text = "定时开";
            }
            else
            {
                btnQueryStatus.Text = "定时关";
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            tbLog.Text = "";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void AddLog(string strLog)
        {
            BeginInvoke(new System.EventHandler(UpdateUI), strLog + "\r\n");
        }

        private void UpdateUI(object obj, System.EventArgs e)
        {
            if (tbLog.TextLength > 1000) tbLog.Text = "";
            string strLog = obj.ToString() + tbLog.Text;
            if (!bPause)
            {
                tbLog.Text = strLog;
            }
        }

        private void FormLog_MouseDown(object sender, MouseEventArgs e)
        {
            LeftTolerance = MousePosition.X - this.Location.X;
            TopTolerance = MousePosition.Y - this.Location.Y;
            //this.Cursor = Cursors.SizeAll;
        }

        private void FormLog_MouseMove(object sender, MouseEventArgs e)
        {
            //拖动中
            if (e.Button == MouseButtons.Left)
            {
                formLocation.X = MousePosition.X - LeftTolerance;
                formLocation.Y = MousePosition.Y - TopTolerance;
                this.Location = formLocation;
            }
        }

        private void FormLog_MouseUp(object sender, MouseEventArgs e)
        {
            //this.Cursor = Cursors.Default;
        }

        private void FormLog_Closing(object sender, CancelEventArgs e)
        {
            formFrame.formLog = null;
        }

        private void btnQueryStatus_Click(object sender, EventArgs e)
        {
            formFrame.bQueryStatusOn = !formFrame.bQueryStatusOn;
            if (formFrame.bQueryStatusOn)
            {
                btnQueryStatus.Text = "定时开";
            }
            else
            {
                btnQueryStatus.Text = "定时关";
            }
        }

        private void btnAddInfo_Click(object sender, EventArgs e)
        {
            TimeSpan ts = new TimeSpan();
            ts = DateTime.Now - formFrame.AppStartTime;

            Int32 dwAvailPhys, dwTotalPhys;
            //PInvoke.GetMemoryStatus(out dwAvailPhys, out dwTotalPhys);
            //tbInfo.Text = string.Format("{0:0.0}m:{1}/{2}", ts.TotalMinutes, dwAvailPhys, dwTotalPhys);

            //BeginInvoke(new System.EventHandler(UpdateUI), tbInfo.Text.Trim() + "\r\n");
        }
    }
}