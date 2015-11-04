using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace Monitor
{
    public partial class FormInit : Form
    {
        public FormInit()
        {
            InitializeComponent();
            NotShowInTaskbar();
            EventLoad = new System.Threading.AutoResetEvent(false);
        }

        private void FormInit_Load(object sender, EventArgs e)
        {
            timerCount = 0;
            timer.Interval = 100;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Enabled = true;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (timerCount == 0)
            {
                new System.Threading.Thread((System.Threading.ThreadStart)delegate
                {
                    FormFrame f = new FormFrame();
                    EventLoad.Set();
                    //System.Threading.Thread.Sleep(1000);
                    //EventLoad.WaitOne();
                    Application.Run(f);
                }).Start();
            }  
            else if (timerCount >= 1)
            {
                EventLoad.WaitOne();
                //EventLoad.Set();
                this.Close();
            }
            timerCount++;
        }

        private void FormInit_Paint(object sender, PaintEventArgs e)
        {
        }

 
        void NotShowInTaskbar() 
        {
            Capture = true;
            IntPtr hwnd = PInvoke.PGetCapture(); 
            Capture = false;
            uint style = PInvoke.PGetWindowLong(hwnd, EXSTYLE);
            style |= WS_EX_NOANIMATION; PInvoke.PSetWindowLong(hwnd, EXSTYLE, style); 
        }

        Timer timer = new Timer();
        ushort timerCount;
        System.Threading.AutoResetEvent EventLoad;

        const int EXSTYLE = -20; 
        const int WS_EX_NOANIMATION = 0x04000000;

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}