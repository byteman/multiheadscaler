using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Monitor
{
    public partial class UCRadioOnline : UserControl
    {
        FormFrame formFrame;
        UCButtons ucButtons = null;
        private string strTitle = "";
        UserControl ucRetControl = null;

        private int PageTotal, PageIndex;
        const int PageSize = 5;
        int SelectIndex = -1;
        private List<string> CurPageList = new List<string>(PageSize);
        private List<string> TotalPageList = new List<string>();
        List<ParamItem> itemListSend = new List<ParamItem>();

        public UCRadioOnline(FormFrame f)
        {
            InitializeComponent();
            formFrame = f;
            ucButtons = new UCButtons(f, this.pnRight);
            ucButtons.RegisterBtnEvent(ClickUp, ClickDown, ClickAck, ClickReturn);
            this.pnRight.Controls.Add(ucButtons);
        }


        //public void InitData(string name, List<string> listOption, UserControl _ucRetControl)
        //{
        //    InitPage();
        //    strTitle = name;
        //    TotalPageList = listOption;
        //    ucRetControl = _ucRetControl;

        //    GetPageTotal();
        //    ShowPage(0);
        //    pnLeft.Invalidate();
        //}

        //public void InitData(ParamItem _paramItem, List<string> listOption, UserControl _ucRetControl)
        //{
        //    bRadioOnline = false;
        //    paramItem = _paramItem;
        //    InitPage();
        //    strTitle = _paramItem.name;
        //    TotalPageList = listOption;
        //    ucRetControl = _ucRetControl;

        //    Protocol protocol = formFrame.protocol;
        //    SerialOperate Serial = SerialOperate.instance;
        //    CurPageList.Clear();
        //    PageTotal = 0;
        //    PageIndex = 0;
        //    SelectIndex = -1;
        //    ucButtons.SetPageCode(PageIndex, PageTotal);

        //    byte[] buf;
        //    int len;
        //    itemListSend.Clear();
        //    itemListSend.Add(paramItem);
        //    len = protocol.Produce(out buf, itemListSend);
        //    Serial.Send(buf, len);
        //}

        public void InitData()
        {
            strTitle = "选择地址：";
            InitPage();
            ucRetControl = formFrame.ucSetMenu;

            Protocol protocol = formFrame.protocol;
            SerialOperate Serial = SerialOperate.instance;
            InitPage();

            ParamItem item;
            byte[] buf;
            int len;
            item = new ParamItem();
            item.name = "在线传感器地址";
            item.dev_id = formFrame.configManage.cfg.paramDeviceId.Ctrl;
            item.param_id = formFrame.configManage.cfg.paramFormWeight.Online;
            itemListSend.Clear();
            itemListSend.Add(item);
            len = protocol.Produce(formFrame.configManage.cfg.paramDeviceId.Ctrl, out buf, itemListSend);
            Serial.Send(buf, len);
        }

        public new void Dispose()
        {
            ucButtons.Dispose();
            base.Dispose();
        }

        public void SetReturnValue(List<ParamItem> itemList)
        {
            if(itemList.Count == 1)
            {
                TotalPageList.Clear();
                if ((itemList[0].dev_id == formFrame.configManage.cfg.paramDeviceId.Ctrl) &&
                    (itemList[0].param_id == formFrame.configManage.cfg.paramFormWeight.Online))
                {
                    if (itemList[0].param_value != null)
                    {
                        byte[] items = (byte[])(itemList[0].param_value);

                        for (byte i = 0; i < items.Length; i++)
                        {
                            TotalPageList.Add(items[i].ToString());
                        }

                        GetPageTotal();
                        ShowPage(0);
                        BeginInvoke(new System.EventHandler(UpdateUI), null);
                    }
                }
            }
        }

        private void UpdateUI(object obj, System.EventArgs e)
        {
            pnLeft.Invalidate();
        }

        private void pnLeft_MouseDown(object sender, MouseEventArgs e)
        {
            int title_height = 100;
            if (Control.MousePosition.Y < (pnLeft.Top + title_height + 48)) return;     //状态栏高度为48pix
            int height = (pnLeft.Height - title_height) / PageSize;
            SelectIndex = (Control.MousePosition.Y - pnLeft.Top - title_height - 48) / height;
            pnLeft.Invalidate();
        }

        private void pnLeft_Click(object sender, EventArgs e)
        {
            int title_height = 100;
            if (Control.MousePosition.Y < (pnLeft.Top + title_height + 48)) return;     //状态栏高度为48pix
            int height = (pnLeft.Height - title_height) / PageSize;
            int tempIndex = (Control.MousePosition.Y - pnLeft.Top - title_height - 48) / height;
            if (tempIndex != SelectIndex)
            {
                SelectIndex = -1;
                pnLeft.Invalidate();
                return;     //用户点选后移动出本条记录
            }

            int index = SelectIndex + PageIndex * PageSize;
            if ((index >= 0) && (index < TotalPageList.Count))
            {
                byte addr = Convert.ToByte(TotalPageList[index]);
                bool bInit = formFrame.ucListControl.InitData(formFrame.configManage.cfg.paramFormWeight.OneSensorCateIndex, UCList.UCListType.UCLT_Param, formFrame.ucRadioOnline, true, addr);
                if (bInit)
                {
                    SelectIndex = -1;
                    formFrame.ShowUC(formFrame.ucListControl);
                }
            }
        }

        private void pnLeft_Paint(object sender, PaintEventArgs e)
        {
            int title_height = 100;
            int title_left = (pnLeft.Width - (strTitle.Length) * 36) / 2;
            int height = (pnLeft.Height - title_height) / PageSize;

            e.Graphics.DrawRectangle(new Pen(Color.Blue), 0, 0, pnLeft.Width - 1, pnLeft.Height - 1);
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(85, 142, 213)), 0, title_height - 2, pnLeft.Right - 1, 2);
            e.Graphics.DrawString(strTitle, new Font("宋体", 32, FontStyle.Bold), new SolidBrush(Color.Black), title_left, title_height / 2 - 16);

            int i;
            for (i = 1; i < PageSize; i++)
            {
                e.Graphics.DrawLine(new Pen(Color.FromArgb(183, 222, 232)), 13, title_height + height * i, pnLeft.Right - 44, title_height + height * i);
            }

            for (i = 0; i < CurPageList.Count; i++)
            {
                if (SelectIndex == i)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(179, 239, 255)), 13, title_height + height * i, pnLeft.Width - 26, height);
                    e.Graphics.FillEllipse(new SolidBrush(Color.FromArgb(255, 165, 5)), 505, title_height + height * i + 25, 10, 10);
                }
                e.Graphics.DrawString(CurPageList[i], new Font("宋体", 24, FontStyle.Bold), new SolidBrush(Color.Black), 100, title_height + height * i + 16);
                e.Graphics.DrawEllipse(new Pen(Color.FromArgb(183, 222, 232)), 500, title_height + height * i + 20, 20, 20);
            }
        }

        #region 分页
        private void InitPage()
        {
            TotalPageList.Clear();
            CurPageList.Clear();
            PageTotal = 0;
            PageIndex = 0;
            SelectIndex = -1;
            ucButtons.SetPageCode(PageIndex, PageTotal);
        }

        private void GetPageTotal()
        {
            PageTotal = TotalPageList.Count / PageSize;
            if ((TotalPageList.Count % PageSize) != 0) PageTotal++;
            PageIndex = 0;
        }

        private void ShowPage(int index)
        {
            int CurPageSize;
            Config cfg = formFrame.configManage.cfg;
            if (PageTotal == 0) return;
            if (index == (PageTotal - 1))
            {
                CurPageSize = TotalPageList.Count - (index * PageSize);
            }
            else
            {
                CurPageSize = PageSize;
            }

            int start = index * PageSize;
            CurPageList.Clear();
            for (int i = 0; i < CurPageSize; i++)
            {
                CurPageList.Add(TotalPageList[start + i]);
            }
            ucButtons.SetPageCode(index, PageTotal);
        }
        #endregion

        private void ClickUp()
        {
            PageIndex--;
            if (PageIndex < 0) PageIndex++;
            ShowPage(PageIndex);
            this.pnLeft.Invalidate();
        }

        private void ClickDown()
        {
            PageIndex++;
            if (PageIndex >= PageTotal) PageIndex--;
            ShowPage(PageIndex);
            this.pnLeft.Invalidate();
        }

        private void ClickAck()
        { }

        private void ClickReturn()
        {
            if (ucRetControl != null) formFrame.ShowUC(ucRetControl);
            else formFrame.ShowUC(formFrame.ucMain);
        }
    }
}
