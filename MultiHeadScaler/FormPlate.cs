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
    public partial class FormPlate : Form, InputInterface
    {
        private FormFrame formFrame = null;
        List<PlateChar> plateCharList = null;
        UCButtons ucButtons = null;
        ParamItem paramItem = null;
        private bool bAck;
        string strTitle = "输入框:";
        const int PageSize = 4;
        bool bKeyDown = false;
        Rectangle rectRefresh = new Rectangle();

        int SelectIndex = -1;
        const int xCount = 15;          //横向按键个数
        int yCount = 0;                 //纵向按键个数
        List<string> listScreen = null;

        public FormPlate(FormFrame f)
        {
            InitializeComponent();
            formFrame = f;

            ucButtons = new UCButtons(f, this.pnRight);
            ucButtons.SetAckVisible(true);
            ucButtons.RegisterBtnEvent(ClickUp, ClickDown, ClickAck, ClickReturn,null);
            this.pnRight.Controls.Add(ucButtons);

            plateCharList = new List<PlateChar>();
            plateCharList.AddRange(formFrame.configManage.cfg.plateCharList);

            for (int i = 0; i <= 9; i++)
            {
                PlateChar plateChar = new PlateChar();
                plateChar.display = i.ToString();
                plateChar.value = i.ToString();
                plateCharList.Add(plateChar);
            }
            for (char i = 'A'; i <= 'Z'; i++)
            {
                PlateChar plateChar = new PlateChar();
                plateChar.display = i.ToString();
                plateChar.value = i.ToString();
                plateCharList.Add(plateChar);
            }
            yCount = plateCharList.Count / xCount;
            if (plateCharList.Count % xCount != 0) yCount++;

            listScreen = new List<string>(xCount * yCount);
            for (int i = 0; i < plateCharList.Count; i++)
            {
                listScreen.Add(plateCharList[i].display);
            }
            listScreen.Add("<- ");
            listScreen.Add("CE");
        }

        public void SetValue(ParamItem item, bool bVisible)
        {
            bAck = false;
            paramItem = item;

            if (item.permit_write != 0)
            {
                ucButtons.SetAckVisible(true);      //如果此参数为可写，则显示确认按钮。
            }
            else
            {
                ucButtons.SetAckVisible(bVisible);  //如果此参数为可读，有外部控制是否显示确认按钮。
            }
            strTitle = item.name + ":";
            lbInput.TextAlign = ContentAlignment.TopCenter;

            if ((item.param_value != null) && (item.param_type == TypeCode.String))
            {
                byte[] v = (byte[])item.param_value;
                //SetInputText(Util.EncodeToString(v));
                SetInputText( formFrame.CodeToPlate(Util.EncodeToString(v)) );
            }
            else
            {
                SetInputText("");
            }
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
            base.ShowDialog();
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
                if (paramItem.param_type == TypeCode.String)
                {
                    string pattern = @"^[\u4e00-\u9fa5]{1}[A-Z]{1}[A-Z_0-9]{5}$";
                    //string pattern = @"^[\u4e00-\u9fa5]*[A-Z]*$";
                    string strPlate = GetInputText().Trim();
                    if (Regex.IsMatch(strPlate, pattern))
                    {
                        string strPlateCode = formFrame.PlateToCode(strPlate);
                        byte[] arr = Util.EncodeToByteArr(strPlateCode);
                        paramItem.param_len = (byte)arr.Length;
                        paramItem.param_value = arr;
                        bMatch = true;
                    }
                    else
                    {
                        strInfo = "请输入正确的车牌号!";
                    }
                }
            }
            catch
            {
                bMatch = false;
                strInfo = "车牌号异常!";
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

            //Rectangle rectBoard = new Rectangle(15, title_height + height * 2, pnLeft.Right - 44 - 12, height * 2);
            Rectangle rectBoard = new Rectangle(15, title_height, pnLeft.Right - 44 - 12, height * 4);
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
                    if ( (j * xCount + i) < listScreen.Count )
                    {
                        e.Graphics.DrawString(listScreen[j * xCount + i], new Font("宋体", 24, FontStyle.Bold), new SolidBrush(Color.Black), rectBoard.X + i * rectBoard.Width+2, rectBoard.Y + j * rectBoard.Height+15);
                    }
                }
            }
        }

        private void ModifyValue()
        {
            if ((SelectIndex < 0) || (SelectIndex > xCount * yCount)) return;
            if (SelectIndex >= listScreen.Count) return;

            if (SelectIndex == (listScreen.Count -1))
            {
                Clear();
            }
            else if (SelectIndex == (listScreen.Count - 2))
            {
                BackSpace();
            }
            else
            {
                Add(listScreen[SelectIndex]);
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
            int max = 7;
            if (GetInputText().Length < max)
            {
                SetInputText(GetInputText() + strAdd.Trim());
            }
        }

        private void pnLeft_MouseDown(object sender, MouseEventArgs e)
        {
            bKeyDown = true;

            int title_height = 80;
            int height = (pnLeft.Height - title_height) / PageSize;
            //Rectangle rectBoard = new Rectangle(15, title_height + height * 2, pnLeft.Right - 44 - 12, height * 2);
            Rectangle rectBoard = new Rectangle(15, title_height, pnLeft.Right - 44 - 12, height * 4);
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

        private void SetInputText(string text)
        {
            lbInput.Text = text;
        }

        private string GetInputText()
        {
            return lbInput.Text.Trim();
        }

        //public static byte[] PlateToByteArr(string strPlate)
        //{
        //    return System.Text.Encoding.Default.GetBytes(strPlate);
        //}

        //public static string PlateToString(byte[] arrPlate)
        //{
        //    return System.Text.Encoding.Default.GetString(arrPlate, 0, arrPlate.Length);
        //}
    }
}
