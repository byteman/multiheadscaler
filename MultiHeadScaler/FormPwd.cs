using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Monitor
{
    public partial class FormPwd : Form
    {
        private FormFrame formFrame = null;
        private bool bInputOrValid;             //true:密码输入对话框   false:密码验证对话框
        UCButtons ucButtons = null;
        public bool bAck;
        string strTitle = "密码输入:";
        const int PageSize = 4;
        bool bKeyDown = false;
        Rectangle rectRefresh = new Rectangle();

        int SelectIndex = -1;
        const int xCount = 7;       //横向按键个数
        const int yCount = 2;       //纵向按键个数
        List<string> listScreen = new List<string>(xCount * yCount);

        public Purview LoginPurview = Purview.None;

        public FormPwd(FormFrame f, Purview pv, bool _bInputOrValid)
        {
            InitializeComponent();
            formFrame = f;
            bInputOrValid = _bInputOrValid;

            LoginPurview = pv;
            ucButtons = new UCButtons(f, this.pnRight);
            ucButtons.SetAckVisible(true);
            if (pv == Purview.None)
            {
                ucButtons.SetPageCode(0, 1);
            }
            else
            {
                ucButtons.SetPageCode(0, 2);
            }
            
            ucButtons.RegisterBtnEvent(ClickUp, ClickDown, ClickAck, ClickReturn);
            this.pnRight.Controls.Add(ucButtons);

            listScreen.Add(" 0 ");
            listScreen.Add(" 1 ");
            listScreen.Add(" 2 ");
            listScreen.Add(" 3 ");
            listScreen.Add(" 4 ");
            listScreen.Add("+/-");
            listScreen.Add("<- ");

            listScreen.Add(" 5 ");
            listScreen.Add(" 6 ");
            listScreen.Add(" 7 ");
            listScreen.Add(" 8 ");
            listScreen.Add(" 9 ");
            listScreen.Add(" . ");
            listScreen.Add("CE");
        }

        public void SetValue(string _pwd)
        {
            bAck = false;
            lbInput.Text = _pwd;
        }

        public string GetValue()
        {
            return lbInput.Text;
        }

        private void ClickUp()
        {
            switch (LoginPurview)
            {
                case Purview.Driver:
                    LoginPurview = Purview.CtrlAdmin;
                    ucButtons.SetPageCode(1, 2);
                    break;
                case Purview.CtrlAdmin:
                    LoginPurview = Purview.Driver;
                    ucButtons.SetPageCode(0, 2);
                    break;
            }
            pnLeft.Invalidate(new Rectangle(420, 26, 200, 40));
        }

        private void ClickDown()
        {
            switch (LoginPurview)
            {
                case Purview.Driver:
                    LoginPurview = Purview.CtrlAdmin;
                    ucButtons.SetPageCode(1, 2);
                    break;
                case Purview.CtrlAdmin:
                    LoginPurview = Purview.Driver;
                    ucButtons.SetPageCode(0, 2);
                    break;
            }
            pnLeft.Invalidate(new Rectangle(420, 26, 200, 40));
        }

        private void ClickAck()
        {
            if (Regex.IsMatch(GetValue().Trim(), "^-?\\d+$|^-{6}$"))
            {
                bAck = true;

                if (bInputOrValid == false)         //如果是验证模式
                {
                    formFrame.userManage.UserInputPwd(this.LoginPurview, this.GetValue());
                    if (formFrame.userManage.CurPurview == this.LoginPurview)
                    {
                        this.Close();
                    }
                    else
                    {
                        //密码错误
                        lbInput.Text = "";
                        FormMsgBox.Show("密码错误!", "警告");
                    }
                }
                else
                { 
                    //密码输入模式
                    this.Close();
                }
            }
            else
            {
                FormMsgBox.Show("请输入整数", "提示");
            }
        }

        private void ClickReturn()
        {
            bAck = false;
            this.Close();
        }

        private void pnLeft_Paint(object sender, PaintEventArgs e)
        {
            int title_height = 80;
            //int title_left = (pnLeft.Width - (strTitle.Length) * 36) / 2;
            int title_left = pnLeft.Left + 10;
            int height = (pnLeft.Height - title_height) / PageSize;

            e.Graphics.DrawRectangle(new Pen(Color.Blue), 0, 0, pnLeft.Width - 1, pnLeft.Height - 1);
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(85, 142, 213)), 0, title_height - 2, pnLeft.Right - 1, 2);
            e.Graphics.DrawString(strTitle, new Font("宋体", 32, FontStyle.Bold), new SolidBrush(Color.Black), title_left, title_height / 2 - 16);

            if (LoginPurview == Purview.Driver)
            {
                e.Graphics.DrawString("驾驶员 >", new Font("宋体", 32, FontStyle.Bold), new SolidBrush(Color.Black), 420, title_height / 2 - 16);
            }
            else if (LoginPurview == Purview.CtrlAdmin)
            {
                e.Graphics.DrawString("管理员 >", new Font("宋体", 32, FontStyle.Bold), new SolidBrush(Color.Black), 420, title_height / 2 - 16);
            }

            e.Graphics.DrawLine(new Pen(Color.FromArgb(183, 222, 232)), 13, title_height + height, pnLeft.Right - 44, title_height + height);

            Rectangle rectBoard = new Rectangle(15, title_height + height * 2, pnLeft.Right - 44 - 12, height * 2);
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(255, 255, 255)), rectBoard.X, rectBoard.Y, rectBoard.Width - 3, rectBoard.Height - 1);
            rectBoard.Width = rectBoard.Width / xCount;
            rectBoard.Height = rectBoard.Height / yCount;
            for (int j = 0; j < yCount; j++)
            {
                for (int i = 0; i < xCount; i++)
                {
                    if ((SelectIndex == (j * xCount + i)) && bKeyDown)
                    {
                        e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(85, 142, 213)), rectBoard.X + i * rectBoard.Width, rectBoard.Y + j * rectBoard.Height, rectBoard.Width - 2, rectBoard.Height - 2);
                    }
                    else
                    {
                        e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(183, 222, 232)), rectBoard.X + i * rectBoard.Width, rectBoard.Y + j * rectBoard.Height, rectBoard.Width - 2, rectBoard.Height - 2);
                    }
                    e.Graphics.DrawString(listScreen[j * xCount + i], new Font("宋体", 32, FontStyle.Bold), new SolidBrush(Color.Black), rectBoard.X + i * rectBoard.Width + 5, rectBoard.Y + j * rectBoard.Height + 20);
                }
            }
        }

        private void ModifyValue()
        {
            if ((SelectIndex < 0) || (SelectIndex > xCount * yCount)) return;
            switch (SelectIndex)
            {
                case 5:
                    Sign();
                    break;
                case 6:
                    BackSpace();
                    break;
                case 13:
                    Clear();
                    break;
                default:
                    Add(listScreen[SelectIndex]);
                    break;
            }
        }

        private void Sign()
        {
            Add("-");
        }

        private void BackSpace()
        {
            if (lbInput.Text.Length > 0)
            {
                int StartIndex = lbInput.Text.Length - 1;

                lbInput.Text = lbInput.Text.Remove(StartIndex, 1);
            }
        }

        private void Clear()
        {
            lbInput.Text = string.Empty;
        }

        private void Add(string strAdd)
        {
            int max = 9;
            if (lbInput.Text.Length < max)
            {
                lbInput.Text += strAdd.Trim();
            }
        }

        private void pnLeft_MouseDown(object sender, MouseEventArgs e)
        {
            bKeyDown = true;

            int title_height = 80;
            int height = (pnLeft.Height - title_height) / PageSize;
            Rectangle rectBoard = new Rectangle(15, title_height + height * 2, pnLeft.Right - 44 - 12, height * 2);
            int wUnit = rectBoard.Width / xCount;
            int hUnit = rectBoard.Height / yCount;
            int x = Control.MousePosition.X - pnLeft.Left;
            int y = Control.MousePosition.Y - pnLeft.Top - 48; //状态栏高度为48pix
            if ((x < rectBoard.X) || (y < rectBoard.Y)) return;
            SelectIndex = ((x - rectBoard.X) / wUnit) + ((y - rectBoard.Y) / hUnit) * xCount;


            rectRefresh.X = rectBoard.X + ((x - rectBoard.X) / wUnit) * wUnit;
            rectRefresh.Y = rectBoard.Y + ((y - rectBoard.Y) / hUnit) * hUnit;
            rectRefresh.Width = wUnit;
            rectRefresh.Height = hUnit;
            pnLeft.Invalidate(rectRefresh);

            ModifyValue();
        }

        private void pnLeft_MouseUp(object sender, MouseEventArgs e)
        {
            bKeyDown = false;

            pnLeft.Invalidate(rectRefresh);
        }

        public new DialogResult ShowDialog()
        {
            return base.ShowDialog();
        }

        public new void Dispose()
        {
            base.Dispose();
        }
    }
}