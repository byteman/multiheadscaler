using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Monitor
{
    public partial class FormPwdChange : Form
    {
        private FormFrame formFrame = null;
        public bool bAck;

        Bitmap bmAckUp = null;
        Bitmap bmAckDown = null;

        Bitmap bmReturnUp = null;
        Bitmap bmReturnDown = null;

        string strAckBtn = "确认";
        string strRetBtn = "取消";

        public FormPwdChange(FormFrame f)
        {
            InitializeComponent();
            formFrame = f;

            bmAckUp = GetBitmap(formFrame.configManage.FileDir + @"\square_btn_up.png");
            bmAckDown = GetBitmap(formFrame.configManage.FileDir + @"\square_btn_down.png");

            bmReturnUp = GetBitmap(formFrame.configManage.FileDir + @"\square_btn_up.png");
            bmReturnDown = GetBitmap(formFrame.configManage.FileDir + @"\square_btn_down.png");

            pbAck.Image = bmAckUp;
            pbReturn.Image = bmReturnUp;
        }

        public new void Dispose()
        {
            if (bmAckUp != null) bmAckUp.Dispose();
            if (bmAckDown != null) bmAckDown.Dispose();

            if (bmReturnUp != null) bmReturnUp.Dispose();
            if (bmReturnDown != null) bmReturnDown.Dispose();

            base.Dispose();
        }

        private Bitmap GetBitmap(string upPath)
        {
            Bitmap bm = null;
            if (System.IO.File.Exists(upPath))
            {
                bm = new Bitmap(upPath);
            }
            return bm;
        }

        private void pbReturn_MouseUp(object sender, MouseEventArgs e)
        {
            pbReturn.Image = bmReturnUp;
        }

        private void pbReturn_MouseDown(object sender, MouseEventArgs e)
        {
            pbReturn.Image = bmReturnDown;
        }

        private void pbReturn_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawString(strRetBtn, new Font("宋体", 24, FontStyle.Bold), new SolidBrush(Color.Black), 32, 28);
        }

        private void pbReturn_Click(object sender, EventArgs e)
        {
            bAck = false;
            this.Close();
        }

        private void pbAck_MouseUp(object sender, MouseEventArgs e)
        {
            pbAck.Image = bmAckUp;
        }

        private void pbAck_MouseDown(object sender, MouseEventArgs e)
        {
            pbAck.Image = bmAckDown;
        }

        private void pbAck_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawString(strAckBtn, new Font("宋体", 24, FontStyle.Bold), new SolidBrush(Color.Black), 32, 28);
        }

        private void pbAck_Click(object sender, EventArgs e)
        {
            if (tbRpeat.Text.Trim() != tbNew.Text.Trim())
            {
                FormMsgBox.Show("新密码输入不一致", "密码");
                return;
            }
            bAck = true;
            Purview pv;
            if (rbAdmin.Checked)
            {
                pv = Purview.CtrlAdmin;
            }
            else
            {
                pv = Purview.Driver;
            }
            string strPwd;
            bool bSuccess = formFrame.userManage.GetPwd(pv, out strPwd);
            if ( (bSuccess && (tbOld.Text == strPwd)) || (tbOld.Text == "------") ) 
            {
                formFrame.userManage.ChangePwd(pv, tbNew.Text.Trim());
                FormMsgBox.Show("新密码设置成功", "密码");
                this.Close();
            }
            else
            {
                FormMsgBox.Show("旧密码输入错误", "密码");
            }
        }

        private void btnOld_Click(object sender, EventArgs e)
        {
            FormPwd dlg = new FormPwd(formFrame, Purview.None, true);         //输入密码模式
            dlg.SetValue(tbOld.Text.Trim());
            dlg.ShowDialog();
            if (dlg.bAck)
            {
                //用户点击确认
                this.tbOld.Text = dlg.GetValue();
            }
            else
            {
                //用户点击返回
            }
            dlg.Dispose();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            FormPwd dlg = new FormPwd(formFrame, Purview.None, true);   //输入密码模式
            dlg.SetValue(tbNew.Text.Trim());
            dlg.ShowDialog();
            if (dlg.bAck)
            {
                //用户点击确认
                this.tbNew.Text = dlg.GetValue(); ;
            }
            else
            {
                //用户点击返回
            }
            dlg.Dispose();
        }

        private void btnRepeat_Click(object sender, EventArgs e)
        {
            FormPwd dlg = new FormPwd(formFrame, Purview.None, true);       //输入密码模式
            dlg.SetValue(tbRpeat.Text.Trim());
            dlg.ShowDialog();
            if (dlg.bAck)
            {
                //用户点击确认
                this.tbRpeat.Text = dlg.GetValue(); ;
            }
            else
            {
                //用户点击返回
            }
            dlg.Dispose();
        }
    }
}