using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace Monitor
{
    public partial class UCMain : UserControl
    {
        private FormFrame formFrame = null;
        PanelBody panelBody = null;
        private Bitmap bmBtnDown = null;
        private Bitmap bmBtnUp = null;

        uint Status;
        bool bLock = false;
        const byte ParamIdSetZero = 36;

        public UCMain(FormFrame f)
        {
            InitializeComponent();
            formFrame = f;

            string path = formFrame.configManage.FileDir + @"\main_btn_down.png";
            if(File.Exists(path))
            {
                bmBtnDown = new Bitmap(path);
            }
            path = formFrame.configManage.FileDir + @"\main_btn_up.png";
            if(File.Exists(path))
            {
                bmBtnUp = new Bitmap(path);
            }

            pbRun.Image = bmBtnUp;
            pbCalib.Image = bmBtnUp;
            pbHandDebug.Image = bmBtnUp;
            pbTongji.Image = bmBtnUp;
            pbParam.Image = bmBtnUp;
            pbZero.Image = bmBtnUp;
            //panelBody = new PanelBody();
            //panelBody.Init(formFrame, 12, 22, 606, 387);
            //panelBody.Init(formFrame, 12, 22, 604, 385);
            //Controls.Add(panelBody);
        }

        public new void Dispose()
        {
            if (bmBtnDown != null) bmBtnDown.Dispose();
            if (bmBtnUp != null) bmBtnUp.Dispose();
            base.Dispose();
        }

        private void pbBtn_MouseDown(object sender, MouseEventArgs e)
        {
            if (bmBtnDown != null)
            {   
                ((PictureBox)sender).Image = bmBtnDown;
            }
        }

        private void pbBtn_MouseUp(object sender, MouseEventArgs e)
        {
            if (bmBtnUp != null)
            {
                ((PictureBox)sender).Image = bmBtnUp;
            }
        }

        private void DrawLabel(object sender, PaintEventArgs e, string str)
        {
            e.Graphics.DrawString(str, new Font("宋体", 16, FontStyle.Bold), new SolidBrush(Color.Black), 40, 20);
        }

        private void pbRun_Paint(object sender, PaintEventArgs e)
        {
            DrawLabel(sender, e, "运行");
        }

        private void pbSetzero_Paint(object sender, PaintEventArgs e)
        {
            DrawLabel(sender, e, "调零");
        }

        private void pbCalib_Paint(object sender, PaintEventArgs e)
        {
            DrawLabel(sender, e, "标定");
        }

        private void pbHandDebug_Paint(object sender, PaintEventArgs e)
        {
            DrawLabel(sender, e, "手动调试");
        }

        private void pbTongji_Paint(object sender, PaintEventArgs e)
        {
            DrawLabel(sender, e, "统计");
        }

        private void pbParam_Paint(object sender, PaintEventArgs e)
        {
            DrawLabel(sender, e, "参数");
        }
        private void pbBody_Paint(object sender, PaintEventArgs e)
        {
            //panelBody.Invalidate();   
        }

        private void pbRun_Click(object sender, EventArgs e)
        {
            bLock = !bLock;
            formFrame.ucStatus.SetLock(bLock);
        }

        private void pbSetzero_Click(object sender, EventArgs e)
        {
           

            Protocol protocol = formFrame.protocol;
            SerialOperate Serial = SerialOperate.instance;
            List<ParamItem> itemList = new List<ParamItem>();
            ParamItem item;
            item = new ParamItem();
            item.dev_id = formFrame.configManage.cfg.paramDeviceId.Ctrl;
            item.param_id = ParamIdSetZero;
            item.op_write = 1;
            item.param_type = TypeCode.Byte;
            item.param_len = 1;
            item.param_value = (byte)1;
            itemList.Add(item);
            byte[] buf;
            int len = protocol.Produce(formFrame.configManage.cfg.paramDeviceId.Ctrl, out buf, itemList);
            if (len > 0)
            {
                Serial.Send(buf, len);
            }
            formFrame.timer.Enabled = true;
        }


        public void SetReturnValue(List<ParamItem> itemList)
        {
            
        }

        private void UpdateUI(object obj, System.EventArgs e)
        {
         
        }

        private void banOcxCtl1_点击事件(object sender, BanOcx.MyEventArges e)
        {

        }

        private void UCMain_Click(object sender, EventArgs e)
        {

        }

        private void pbCalib_Click(object sender, EventArgs e)
        {

        }

        private void pbHandDebug_Click(object sender, EventArgs e)
        {

        }

        private void pbParam_Click(object sender, EventArgs e)
        {

        }

       

     
       
        #region 测试代码

        //pbBody.Invalidate(new Rectangle(64, 37, 325, 92));     //更新载货重量大字
        //pbBody.Invalidate(new Rectangle(177, 185, 183, 32));   //更新车身重量
        //pbBody.Invalidate(new Rectangle(177, 244, 183, 32));   //更新总重量
        //pbBody.Invalidate(new Rectangle(177, 303, 183, 32));   //更新实时重量
        //pbBody.Invalidate();

        //private void pbBody_Click(object sender, EventArgs e)
        //{
        //    string str = string.Format("pos:{0:D}.{1:D}.{2:D}.{3:D}", Control.MousePosition.X, Control.MousePosition.Y,
        //                                    Control.MousePosition.X - this.Left - pbBody.Left, Control.MousePosition.Y - this.Top - pbBody.Top);
        //    System.Diagnostics.Debug.WriteLine(str);
        //}
        #endregion
    }
}
