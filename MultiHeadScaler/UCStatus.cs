using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace Monitor
{
    public partial class UCStatus : UserControl
    {
        const int nMaxRecvTimeout = 3;
        const int nMaxRefreshTime = 2;

        int nRecvTimeout = nMaxRecvTimeout;
        int nRefreshTime = 0;
        FormFrame formFrame = null;

        public UCStatus(FormFrame f)
        {
            InitializeComponent();
            formFrame = f;
            pbAlarm.Visible = false;
            pbFault.Visible = false;
            pbLock.Visible = false;
            pbConnect.Visible = false;
            pbGprs.Visible = false;
            pbGps.Visible = false;

            Timer tm = new Timer();
            tm.Interval = 1000;
            tm.Tick += new EventHandler(tm_Tick);
            tm.Enabled = true;

            #region 处理原始图片 注释掉
            ////GPRS图片生成
            //Bitmap bmGprs = (Bitmap)this.pbGprs.Image;
            //for (int w = 0; w < bmGprs.Width; w++)
            //{
            //    for (int h = 0; h < bmGprs.Height; h++)
            //    {
            //        Color backColor = ((Bitmap)this.pbBackground.Image).GetPixel(500, 3 + h);
            //        Color pixelColor = bmGprs.GetPixel(w, h);

            //        //if ((pixelColor.R < 50) && (pixelColor.G < 50) && (pixelColor.B < 50))
            //        if (pixelColor.R == 0)
            //        {
            //            bmGprs.SetPixel(w, h, Color.FromArgb(0, 0, 0));
            //        }
            //        else
            //        {
            //            bmGprs.SetPixel(w, h, backColor);
            //        }
            //    }
            //}
            //this.pbGprs.Image = bmGprs;
            ////connect图片生成
            //Bitmap bmConnect = (Bitmap)this.pbConnect.Image;
            //for (int w = 0; w < bmConnect.Width; w++)
            //{
            //    for (int h = 0; h < bmConnect.Height; h++)
            //    {
            //        Color backColor = ((Bitmap)this.pbBackground.Image).GetPixel(500, 3 + h);
            //        Color pixelColor = bmConnect.GetPixel(w, h);

            //        if ((pixelColor.R > 220) && (pixelColor.G > 220) && (pixelColor.B > 220))
            //        {
            //            bmConnect.SetPixel(w, h, backColor);
            //        }
            //    }
            //}
            //this.pbConnect.Image = bmConnect;
            #endregion
        }

        void tm_Tick(object sender, EventArgs e)
        {
            if (nRecvTimeout >= nMaxRecvTimeout)
            {
                pbConnect.Visible = false;
            }
            else
            {
                pbConnect.Visible = true;
                nRecvTimeout++;
            }
            this.pbBackground.Invalidate(new Rectangle(470, 10, 330, 38));

            
            if (formFrame.bQueryStatusOn)
            {
                nRefreshTime++;
                if (nRefreshTime >= nMaxRefreshTime)
                {
                    QueryStatus();
                    nRefreshTime = 0;
                }
            }
        }

        private void pbBackground_Click(object sender, EventArgs e)
        {
            
        }

        private void pbBackground_Paint(object sender, PaintEventArgs e)
        {
            switch (formFrame.userManage.CurPurview)
            {
                case Purview.Driver:
                    e.Graphics.DrawString("驾驶员", new Font("宋体", 20, FontStyle.Bold), new SolidBrush(Color.Black), 470, 10);
                    break;
                case Purview.CtrlAdmin:
                    e.Graphics.DrawString("管理员", new Font("宋体", 20, FontStyle.Bold), new SolidBrush(Color.Black), 470, 10);
                    break;
                default:
                    break;
            }
            string strTime = DateTime.Now.ToString("MM-dd HH:mm:ss");
            e.Graphics.DrawString(strTime, new Font("宋体", 20, FontStyle.Bold), new SolidBrush(Color.Black), 580, 10);
        }

        public void SetLock(bool b)
        {
            pbLock.Visible = b;
        }

        public void SetAlarm(bool b)
        {
            pbAlarm.Visible = b;
        }

        public void SetFault(bool b)
        {
            pbFault.Visible = b;
        }

        public void SetGprs(bool b)
        {
            pbGprs.Visible = b;
        }

        public void SetGps(bool b)
        {
            pbGps.Visible = b;
        }

        public void ReceiveOnePkg()
        {
            nRecvTimeout = 0;
        }

        private void QueryStatus()
        {
            Protocol protocol = formFrame.protocol;
            SerialOperate Serial = SerialOperate.instance;

            List<ParamItem> itemList = new List<ParamItem>();
            ParamItem item;
            item = new ParamItem();
            item.dev_id = formFrame.configManage.cfg.paramDeviceId.Ctrl;
            item.param_id = formFrame.configManage.cfg.paramFormWeight.Status.Id;
            itemList.Add(item);

            byte[] buf;
            int len = protocol.Produce(formFrame.configManage.cfg.paramDeviceId.Ctrl, out buf, itemList);
            if (len > 0)
            {
                Serial.Send(buf, len);
            }
        }

        public void SetReturnValue(ParamItem item)
        {
            //if ( (item.param_id == formFrame.configManage.cfg.paramFormWeight.Status.Id) &&
            //     (item.dev_id == formFrame.configManage.cfg.paramDeviceId.Ctrl))
            {
                if (item.param_type == TypeCode.UInt32)
                {
                    UInt32 Status;
                    Status = Convert.ToUInt32(item.param_value);

                    Byte bit = formFrame.configManage.cfg.paramFormWeight.Status.Alarm;
                    SetAlarm(((Status & (1 << bit)) != 0));

                    bit = formFrame.configManage.cfg.paramFormWeight.Status.Fault;
                    formFrame.ucStatus.SetFault(((Status & (1 << bit)) != 0));

                    bit = formFrame.configManage.cfg.paramFormWeight.Status.Gps;
                    formFrame.ucStatus.SetGps(((Status & (1 << bit)) != 0));

                    bit = formFrame.configManage.cfg.paramFormWeight.Status.Gprs;
                    formFrame.ucStatus.SetGprs(((Status & (1 << bit)) != 0));
                }
            }
        }
    }
}
