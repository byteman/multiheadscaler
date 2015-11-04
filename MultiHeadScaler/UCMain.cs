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

            pbHold.Image = bmBtnUp;
            pbSetzero.Image = bmBtnUp;
            pbQuery.Image = bmBtnUp;
            pbSet.Image = bmBtnUp;
            pbExit.Image = bmBtnUp;

            panelBody = new PanelBody();
            //panelBody.Init(formFrame, 12, 22, 606, 387);
            panelBody.Init(formFrame, 12, 22, 604, 385);
            Controls.Add(panelBody);
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
            e.Graphics.DrawString(str, new Font("宋体", 22, FontStyle.Bold), new SolidBrush(Color.Black), 40, 20);
        }

        private void pbHold_Paint(object sender, PaintEventArgs e)
        {
            DrawLabel(sender, e, "保持");
        }

        private void pbSetzero_Paint(object sender, PaintEventArgs e)
        {
            DrawLabel(sender, e, "调零");
        }

        private void pbQuery_Paint(object sender, PaintEventArgs e)
        {
            DrawLabel(sender, e, "系统");
        }

        private void pbSet_Paint(object sender, PaintEventArgs e)
        {
            DrawLabel(sender, e, "参数");
        }

        private void pbExit_Paint(object sender, PaintEventArgs e)
        {
            DrawLabel(sender, e, "退出");
        }

        private void pbBody_Paint(object sender, PaintEventArgs e)
        {
            panelBody.Invalidate();   
        }

        private void pbHold_Click(object sender, EventArgs e)
        {
            bLock = !bLock;
            formFrame.ucStatus.SetLock(bLock);
        }

        private void pbSetzero_Click(object sender, EventArgs e)
        {
            formFrame.timer.Enabled = false;

            DialogResult dlgRes = FormMsgBox.Show("确定要调零？", "询问", FormMsgBox.Buttons.OKCancel);
            if (dlgRes != DialogResult.OK)
            {
                formFrame.timer.Enabled = true;
                return;
            }

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

        private void pbQuery_Click(object sender, EventArgs e)
        {
            formFrame.timer.Enabled = false;
            formFrame.ClickQueryBtn();
        }

        private void pbSet_Click(object sender, EventArgs e)
        {
            formFrame.timer.Enabled = false;
            if (formFrame.userManage.CurPurview == Purview.None)
            {
                FormPwd dlg = new FormPwd(formFrame, Purview.Driver, false); //验证密码模式
                dlg.ShowDialog();
                if (dlg.bAck)
                {
                    //用户点击确认
                    if (formFrame.userManage.CurPurview != Purview.None)
                    {
                        formFrame.ClickSetBtn();
                    }
                }
                else
                {
                    //用户点击返回
                    formFrame.ShowUC(formFrame.ucMain);
                }
                dlg.Dispose();
                formFrame.timer.Enabled = true;
            } 
            else
            {
                formFrame.ClickSetBtn();
            }
        }

        private void pbExit_Click(object sender, EventArgs e)
        {
            if ( formFrame != null )
            {
                if ( DialogResult.OK == FormMsgBox.Show("确定要退出？", "询问", FormMsgBox.Buttons.OKCancel) )
                {
                    formFrame.Close();
                }
            }
        }

        public void SetReturnValue(List<ParamItem> itemList)
        {
            if (!panelBody.bHasReceived)    panelBody.bHasReceived = true;
            if (itemList.Count != 5) return;
            if (itemList[0].dev_id != formFrame.configManage.cfg.paramDeviceId.Ctrl) return;

            for (int j = 0; j < itemList.Count; j++)
            {
                if( itemList[j].param_id == formFrame.configManage.cfg.paramFormWeight.CarryWeight )
                {
                    if (itemList[j].param_type == TypeCode.Int32)
                    {
                        panelBody.CarryWeight = Convert.ToInt32(itemList[j].param_value);
                    }
                }
                else if( itemList[j].param_id == formFrame.configManage.cfg.paramFormWeight.TruckWeight )
                {
                    if (itemList[j].param_type == TypeCode.Int32)
                    {
                        panelBody.BodyWeight = Convert.ToInt32(itemList[j].param_value);
                    }
                }
                else if( itemList[j].param_id == formFrame.configManage.cfg.paramFormWeight.TotalWeight)
                {
                    if (itemList[j].param_type == TypeCode.Int32)
                    {
                        panelBody.TotalWeight = Convert.ToInt32(itemList[j].param_value);
                    }
                }
                else if( itemList[j].param_id == formFrame.configManage.cfg.paramFormWeight.RtWeight)
                {
                    if (itemList[j].param_type == TypeCode.Int32)
                    {
                        panelBody.RealtimeWeight = Convert.ToInt32(itemList[j].param_value);
                    }
                }
                else if( itemList[j].param_id == formFrame.configManage.cfg.paramFormWeight.Status.Id)
                {
                    if (itemList[j].param_type == TypeCode.UInt32)
                    {
                        Status = Convert.ToUInt32(itemList[j].param_value);

                        Byte bit = formFrame.configManage.cfg.paramFormWeight.Status.Alarm;
                        formFrame.ucStatus.SetAlarm(((Status & (1 << bit)) != 0));

                        bit = formFrame.configManage.cfg.paramFormWeight.Status.Fault;
                        formFrame.ucStatus.SetFault(((Status & (1 << bit)) != 0));

                        bit = formFrame.configManage.cfg.paramFormWeight.Status.Gps;
                        formFrame.ucStatus.SetGps(((Status & (1 << bit)) != 0));

                        bit = formFrame.configManage.cfg.paramFormWeight.Status.Gprs;
                        formFrame.ucStatus.SetGprs(((Status & (1 << bit)) != 0));

                        bit = formFrame.configManage.cfg.paramFormWeight.Status.Stable;
                        panelBody.FlagStable = ((Status & (1 << bit)) != 0);

                        bit = formFrame.configManage.cfg.paramFormWeight.Status.Zero;
                        panelBody.FlagZero = ((Status & (1 << bit)) != 0);
                    }
                }
            }
            BeginInvoke(new System.EventHandler(UpdateUI), null);
        }

        private void UpdateUI(object obj, System.EventArgs e)
        {
            if (!bLock)
            {
                panelBody.DisplayWeight = panelBody.CarryWeight;
            }
            panelBody.Invalidate();

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
