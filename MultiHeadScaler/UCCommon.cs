using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Monitor
{
    public partial class UCCommon : UserControl
    {
        public enum EnumCtrlPwd
        {
            EnumCtrlPwdRead,
            EnumCtrlPwdWrite
        }

        FormFrame formFrame = null;

        

        public UCCommon(FormFrame f)
        {
            InitializeComponent();
            formFrame = f;
        }

        public void SetReturnValue(List<ParamItem> itemList)
        {

        }

        private void UpdateUI(object obj, System.EventArgs e)
        {

        }

        public void SendCtrlPwd(EnumCtrlPwd enumCtrlPwd, int pwd)
        {
            Protocol protocol = formFrame.protocol;
            protocol.bSuccess = false;
            SerialOperate Serial = SerialOperate.instance;
            List<ParamItem> itemList = new List<ParamItem>();
            ParamItem item;
            item = new ParamItem();
            item.dev_id = formFrame.configManage.cfg.paramDeviceId.Ctrl;
            item.param_id = Protocol.ParamIdCtrlAdmin;
            if (enumCtrlPwd == EnumCtrlPwd.EnumCtrlPwdRead)
            {
                item.op_write = 0;
                item.param_type = TypeCode.Int32;
            } 
            else if (enumCtrlPwd == EnumCtrlPwd.EnumCtrlPwdWrite)
            {
                item.op_write = 1;
                item.param_type = TypeCode.Int32;
                item.param_len = 4;
                item.param_value = pwd;
            }
            else
            {
                return;
            }

            itemList.Add(item);
            byte[] buf;
            int len = protocol.Produce(formFrame.configManage.cfg.paramDeviceId.Ctrl, out buf, itemList);
            if (len > 0)
            {
                Serial.Send(buf, len);
            }
        }

        public bool GetCtrlPwd(out int pwd)
        {
            formFrame.protocol.bToUserControl = false;
            SendCtrlPwd(EnumCtrlPwd.EnumCtrlPwdRead, 0);
            System.Threading.Thread.Sleep(200);
            pwd = formFrame.protocol.nCtrlPwd;
            formFrame.protocol.bToUserControl = true;
            return formFrame.protocol.bSuccess;
        }

        public bool ChangeCtrlPwd(int pwd)
        {
            formFrame.protocol.bToUserControl = false;
            SendCtrlPwd(EnumCtrlPwd.EnumCtrlPwdWrite, pwd);
            System.Threading.Thread.Sleep(200);
            formFrame.protocol.bToUserControl = true;
            return formFrame.protocol.bSuccess;
        }
    }
}
