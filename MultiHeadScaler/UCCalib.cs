using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
namespace Monitor
{
    public partial class UCCalib : UserControl
    {
        private FormFrame formFrame;
        private Bitmap bmBtnDown = null;
        private Bitmap bmBtnUp = null;
        
        public UCCalib(FormFrame f)
        {
            InitializeComponent();
            formFrame = f;
            string path = formFrame.configManage.FileDir + @"\main_btn_down.png";
            if (File.Exists(path))
            {
                bmBtnDown = new Bitmap(path);
            }
            path = formFrame.configManage.FileDir + @"\main_btn_up.png";
            if (File.Exists(path))
            {
                bmBtnUp = new Bitmap(path);
            }

            pbClear.Image = bmBtnUp;
            pbZero.Image = bmBtnUp;
            pbCalib.Image = bmBtnUp;
            pbExit.Image = bmBtnUp;
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
        public new void Dispose()
        {
            if (bmBtnDown != null) bmBtnDown.Dispose();
            if (bmBtnUp != null) bmBtnUp.Dispose();
            base.Dispose();
        }

        private void pbClear_Paint(object sender, PaintEventArgs e)
        {
            DrawLabel(sender, e, "清零");
        }

        private void pbZero_Paint(object sender, PaintEventArgs e)
        {
            DrawLabel(sender, e, "零点");
        }

        private void pbCalib_Paint(object sender, PaintEventArgs e)
        {
            DrawLabel(sender, e, "标定");
        }


        private void pbExit_Paint(object sender, PaintEventArgs e)
        {
            DrawLabel(sender, e, "返回");
        }

        private void pbExit_Click(object sender, EventArgs e)
        {
            
        }

     
        private void send()
        {
            Protocol protocol = formFrame.protocol;
            SerialOperate Serial = SerialOperate.instance;
            List<ParamItem> itemList = new List<ParamItem>();
            ParamItem item;
            item = new ParamItem();
            item.dev_id = formFrame.configManage.cfg.paramDeviceId.Ctrl;
            item.param_id = 0;
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
        }
        private void pbClear_Click(object sender, EventArgs e)
        {
            send();
        }

        private void tb_number_GotFocus(object sender, EventArgs e)
        {
            banOcxCtl1.Focus();
            ParamItem item = new ParamItem();
            item.name = "设备地址";
            item.unit = "";
            item.param_type = TypeCode.Int32;
            item.param_value = Convert.ToInt32(tb_number.Text);

            FormInput dlg = new FormInput(formFrame);
            dlg.SetValue(item, true);
            dlg.ShowDialog();
            if (dlg.GetAck())
            {
                item = dlg.GetValue();
                tb_number.Text = item.param_value.ToString();
                //ChangeCalibWeight(((CalibInfo)alSource[r]).Addr, Convert.ToInt32(item.param_value));
                
            }
            dlg.Dispose();
            
        }

        private void tb_fama_GotFocus(object sender, EventArgs e)
        {
            banOcxCtl1.Focus();
            ParamItem item = new ParamItem();
            item.name = "砝码重量";
            item.unit = "g";
            item.param_type = TypeCode.Int32;
            item.param_value = Convert.ToInt32(tb_fama.Text);

            FormInput dlg = new FormInput(formFrame);
            dlg.SetValue(item, true);
            dlg.ShowDialog();
            if (dlg.GetAck())
            {
                item = dlg.GetValue();
                tb_fama.Text = item.param_value.ToString();
                //ChangeCalibWeight(((CalibInfo)alSource[r]).Addr, Convert.ToInt32(item.param_value));

            }
            dlg.Dispose();
        }

        private void banOcxCtl1_点击事件(object sender, BanOcx.MyEventArges e)
        {
            tb_number.Text = e.SelNum.ToString();
        }
        public void SetReturnValue(List<ParamItem> itemList)
        {
            /*
            switch (uclType)
            {
             
                case UCListType.UCLT_Param:
                    
                    bool bRet = false;

                    for (int j = 0; j < itemList.Count; j++)
                    {
                        if ((formFrame.configManage.cfg.paramDeviceId.Ctrl == itemList[j].dev_id) && (123 == itemList[j].param_id))
                        {
                            bRet = true;
                            itemList[j].name = itemListSend[i].name;
                            itemListSend[i].param_value = itemList[j].param_value;
                            itemList[j].permit_read = itemListSend[i].permit_read;
                            itemList[j].permit_write = itemListSend[i].permit_write;

                        }
                    }
                       
                    BeginInvoke(new System.EventHandler(UpdateUI), null);
                    break;
                default:
                    break;
            }
             * */
        }
        private void UpdateUI(object obj, System.EventArgs e)
        {
           //更新每个banocx上面的重量.
            //banOcxCtl1.SetBanColor(
        }
        private void pbExit_Click_1(object sender, EventArgs e)
        {
            formFrame.ShowUC(formFrame.ucMain);
        }

        private void UCCalib_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(Color.Blue), 530, 20, 250, 440);

        }

    }
}
