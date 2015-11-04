using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Monitor
{
    public partial class FormInput : Form, InputInterface
    {
        private FormFrame formFrame = null;
        UCButtons ucButtons = null;
        ParamItem paramItem = null;
        private bool bAck;
        string strTitle = "输入框:";
        const int PageSize = 4;
        bool bKeyDown = false;
        Rectangle rectRefresh = new Rectangle();

        int SelectIndex = -1;
        const int xCount = 7;       //横向按键个数
        const int yCount = 2;       //纵向按键个数
        List<string> listScreen = new List<string>(xCount * yCount);
        bool bIpAddr = false;

        public FormInput(FormFrame f)
        {
            InitializeComponent();
            formFrame = f;
            ucButtons = new UCButtons(f, this.pnRight);
            ucButtons.SetAckVisible(true);
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

        public void SetValue(ParamItem item, bool bVisible)
        {
            bAck = false;
            if (item.permit_write != 0)
            {
                ucButtons.SetAckVisible(true);      //如果此参数为可写，则显示确认按钮。
            }
            else
            {
                ucButtons.SetAckVisible(bVisible);  //如果此参数为可读，有外部控制是否显示确认按钮。
            }
            if (item.unit.Length > 0)
            {
                strTitle = item.name + "(" + item.unit + "):";
            }
            else
            {
                strTitle = item.name + ":";
            }

            if (item.param_type == TypeCode.String)
            {
                lbInput.TextAlign = ContentAlignment.TopLeft;
            }
            else
            {
                lbInput.TextAlign = ContentAlignment.TopCenter;
            }

            if ((item.dev_id == formFrame.configManage.cfg.paramDeviceId.Ctrl) && (item.param_id == formFrame.configManage.cfg.paramFormWeight.Ip))
            {
                bIpAddr = true;
                bAck = false;
                if ((item.param_value != null) && (item.param_type == TypeCode.UInt32))
                {
                    UInt32 uIp = Convert.ToUInt32(item.param_value);
                    SetInputText(string.Format("{0:D}.{1:D}.{2:D}.{3:D}", (Byte)(uIp >> 24), (Byte)(uIp >> 16), (Byte)(uIp >> 8), (Byte)uIp));
                }
                else
                {
                    SetInputText("");
                }
            }
            else
            { 
                bIpAddr = false;
                if (item.param_value != null)
                {
                    switch (item.param_type)
                    {
                        case TypeCode.String:
                            byte[] v = (byte[])item.param_value;
                            switch (item.param_id)
                            {
                                case Protocol.ParamIdFixAddr:   //安装地址，去除0
                                    List<byte> addrList = new List<byte>();
                                    for (int i = 0; i < v.Length; i++)
                                    {
                                        if (v[i] != 0)
                                        {
                                            addrList.Add(v[i]);
                                        }
                                    }
                                    SetInputText(Util.ByteToStringDec(addrList.ToArray(), addrList.Count, "-"));
                                    break;

                                case Protocol.ParamIdSIM:    //SIM
                                    SetInputText(Util.EncodeToString(v));
                                    break;
                                default:
                                    SetInputText(Util.ByteToStringDec(v, v.Length, "-"));
                                    break;
                            }
                            break;
                        default:
                            SetInputText(item.param_value.ToString());
                            break;
                    }
                }
                else
                {
                    SetInputText("0");
                }
            }
            paramItem = item;

        }

        public ParamItem GetValue()
        {
            return paramItem;
        }

        public bool GetAck()
        {
            return bAck;
        }

        public new void ShowDialog()
        {
            if (paramItem.param_type == TypeCode.Empty)
            {
                if (DialogResult.OK == FormMsgBox.Show("是否" + paramItem.name + "？", "执行", FormMsgBox.Buttons.OKCancel))
                {
                    bAck = true;
                }
                else
                {
                    bAck = false;
                }
            }
            else
            {
                base.ShowDialog();
            }
        }

        public new void Dispose()
        {
            base.Dispose();
        }

        private void ClickUp()
        {
        }

        private void ClickDown()
        {
        }

        private void ClickAck()
        {
            bool bMatch = false;
            string strInfo = "";
            try
            {
                switch (paramItem.param_type)
                { 
                    case TypeCode.Empty:
                        bMatch = true;
                        break;

                    case TypeCode.Byte:
                        if (Regex.IsMatch(GetInputText().Trim(), @"^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$"))
                        {
                            byte temp = Convert.ToByte(GetInputText());

                            if (paramItem.valid_min_max)
                            {
                                if ((temp >= paramItem.min) && (temp <= paramItem.max))
                                {
                                    paramItem.param_value = temp;
                                    bMatch = true;
                                }
                            }
                            else
                            {
                                paramItem.param_value = temp;
                                bMatch = true;
                            }
                        }
                        
                        if(!bMatch)
                        {
                            if (paramItem.valid_min_max)
                            {
                                strInfo = string.Format("请输入{0}到{1}的整数", paramItem.min, paramItem.max);
                            }
                            else
                            {
                                strInfo = "请输入0到255的整数";
                            }
                        }
                        break;
                    case TypeCode.UInt16:
                        if (Regex.IsMatch(GetInputText().Trim(), @"^(\d{1,4}|[0-5]\d{4}|6[0-4]\d{3}|65[0-4]\d{2}|655[0-2]\d|6552\d|6553[0-5])$"))
                        {
                            paramItem.param_value = Convert.ToUInt16(GetInputText());
                            bMatch = true;
                        }
                        else
                        {
                            strInfo = "请输入0到65535的整数";
                        }
                        break;
                    case TypeCode.UInt32:
                        if (bIpAddr)
                        {
                            string pattern = @"^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$";
                            if (Regex.IsMatch(GetInputText().Trim(), pattern))
                            {
                                UInt32 uIp = 0;
                                string strIp = GetInputText().Trim();
                                string[] arrIp = strIp.Split('.');
                                for (int i = 0; i < arrIp.Length; i++)
                                {
                                    uIp |= Convert.ToByte(arrIp[i]);
                                    if (i >= 3) break;
                                    uIp = uIp << 8;
                                }
                                paramItem.param_value = uIp;
                                bMatch = true;
                            }
                            else
                            {
                                strInfo = "请输入正确IP地址";
                            }
                        }
                        else
                        {
                            if (Regex.IsMatch(GetInputText().Trim(), "^\\d+$"))
                            {
                                paramItem.param_value = Convert.ToUInt32(GetInputText());
                                bMatch = true;
                            }
                            else
                            {
                                strInfo = "请输入非负整数";
                            }
                        }
                        break;
                    case TypeCode.Int32:
                        //if (Regex.IsMatch(GetInputText().Trim(), "^-?\\d+$"))
                        //{
                        //    paramItem.param_value = Convert.ToInt32(GetInputText());
                        //    bMatch = true;
                        //}
                        //else
                        //{
                        //    strInfo = "请输入整数";
                        //}

                        if (Regex.IsMatch(GetInputText().Trim(), "^-?\\d+$"))
                        {
                            int temp = Convert.ToInt32(GetInputText());

                            if (paramItem.valid_min_max)
                            {
                                if ((temp >= paramItem.min) && (temp <= paramItem.max))
                                {
                                    paramItem.param_value = temp;
                                    bMatch = true;
                                }
                            }
                            else
                            {
                                paramItem.param_value = temp;
                                bMatch = true;
                            }
                        }

                        if (!bMatch)
                        {
                            if (paramItem.valid_min_max)
                            {
                                strInfo = string.Format("请输入{0}到{1}的整数", paramItem.min, paramItem.max);
                            }
                            else
                            {
                                strInfo = "请输入整数";
                            }
                        }
                        break;
                    case TypeCode.Single:
                        if (Regex.IsMatch(GetInputText().Trim(), @"^(-?\d+)(\.\d+)?$"))
                        {
                            paramItem.param_value = Convert.ToSingle(GetInputText());
                            bMatch = true;
                        }
                        else
                        {
                            strInfo = "请输入浮点数";
                        }
                        break;
                    case TypeCode.String:
                        byte[] byteArr;
                        switch(paramItem.param_id)
                        {
                            case Protocol.ParamIdSIM:    //SIM
                                byteArr = Util.CharArryToByte(GetInputText());
                                break;
                            default:
                                byteArr = Util.StringDecToByte(GetInputText(), '-');
                                break;
                        }
                        if (byteArr.Length <= paramItem.param_len)
                        {
                            paramItem.param_value = byteArr;
                            if (paramItem.param_value != null)
                            {
                                paramItem.param_len = (byte)byteArr.Length;
                            }
                            bMatch = true;
                        }
                        else
                        {
                            strInfo = "输入数据过长，最大长度：" + paramItem.param_len.ToString();
                        }
                        break;
                    default:
                        strInfo = "输入的类型未知";
                        break;
                }
            }
            catch
            {
                bMatch = false;
                if (paramItem.param_id == Protocol.ParamIdFixAddr)
                {
                    strInfo = "请输入的正确的安装地址(如：2-3-4)";
                }
                else
                {
                    strInfo = "输入的类型匹配错误";
                }
            }

            if (bMatch)
            {
                bAck = true;
                this.Close();
            }
            else
            {
                FormMsgBox.Show(strInfo, "提示");
            }
        }

        private void ClickReturn()
        {
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
            e.Graphics.DrawLine(new Pen(Color.FromArgb(183, 222, 232)), 13, title_height + height, pnLeft.Right - 44, title_height + height);

            Rectangle rectBoard = new Rectangle(15, title_height + height * 2, pnLeft.Right - 44 - 12, height * 2);
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(255, 255, 255)), rectBoard.X, rectBoard.Y, rectBoard.Width-3, rectBoard.Height-1);
            rectBoard.Width = rectBoard.Width / xCount;
            rectBoard.Height = rectBoard.Height / yCount;
            for (int j = 0; j < yCount; j++)
            {
                for (int i = 0; i < xCount; i++)
                {
                    if( (SelectIndex == (j*xCount+i)) && bKeyDown )
                    {
                        e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(85, 142, 213)), rectBoard.X+i*rectBoard.Width, rectBoard.Y+j*rectBoard.Height, rectBoard.Width - 2, rectBoard.Height - 2);
                    }
                    else
                    {
                        e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(183, 222, 232)), rectBoard.X+i*rectBoard.Width, rectBoard.Y+j*rectBoard.Height, rectBoard.Width - 2, rectBoard.Height - 2);
                    }
                    e.Graphics.DrawString(listScreen[j * xCount + i], new Font("宋体", 32, FontStyle.Bold), new SolidBrush(Color.Black), rectBoard.X + i * rectBoard.Width + 5, rectBoard.Y + j * rectBoard.Height + 20);
                }
            }
        }

        private void ModifyValue()
        {
            if ((SelectIndex < 0) || (SelectIndex > xCount*yCount)) return;
            if (SelectIndex >= listScreen.Count) return;
            switch(SelectIndex)
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
            if ((paramItem != null) && (paramItem.param_type == TypeCode.String))
            {
                //u8(byte)数组
                SetInputText(GetInputText() + "-");
            }
            else
            {
                if (GetInputText().Length == 0)
                {
                    SetInputText("-");
                }
                else
                {
                    if (GetInputText()[0] == '-')
                    {
                        SetInputText(GetInputText().Remove(0, 1));
                    }
                    else
                    {
                        SetInputText("-" + GetInputText());
                    }
                }
            }
        }

        private void BackSpace()
        {
            if (GetInputText().Length > 0)
            {
                int StartIndex = GetInputText().Length - 1;

                SetInputText(GetInputText().Remove(StartIndex, 1));
            }
        }

        private void Clear()
        {
            SetInputText(string.Empty);
        }

        private void Add(string strAdd)
        {

            if ( (paramItem != null) && (paramItem.param_type == TypeCode.String) )
            {
                //u8(byte)数组
                SetInputText(GetInputText() + strAdd.Trim());
            }
            else
            {
                int max = 9;
                if (bIpAddr) max = 15;
                if (GetInputText().Length < max)
                {
                    SetInputText(GetInputText() + strAdd.Trim());
                }
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
            if( (x < rectBoard.X) || (y < rectBoard.Y) )    return;
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

        private void SetInputText(string text)
        {
            lbInput.Text = text;
        }

        private string GetInputText()
        {
            return lbInput.Text.Trim();
        }
    }
}
