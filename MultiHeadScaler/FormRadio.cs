using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Monitor
{
    public partial class FormRadio : Form
    {
        private FormFrame formFrame = null;
        UCButtons ucButtons = null;
        public bool bAck;
        
        private string strTitle = "";
        private int PageTotal, PageIndex;
        const int PageSize = 5;
        int SelectIndex = -1;       //值在总的list的索引
        int CurSelectIndex = -1;    //用户选择的项目在当前页的索引
        private List<string> CurPageList = new List<string>(PageSize);
        private List<string> TotalPageList = new List<string>();
        List<ParamItem> itemListSend = new List<ParamItem>();

        ParamItem paramItem = null;
        private List<ParamOption> listParamOption = null;

        Bitmap bmCheck = null;
        Bitmap bmUncheck = null;

        public FormRadio(FormFrame f)
        {
            InitializeComponent();
            formFrame = f;
            ucButtons = new UCButtons(f, this.pnRight);
            ucButtons.SetAckVisible(true);
            ucButtons.RegisterBtnEvent(ClickUp, ClickDown, ClickAck, ClickReturn);
            this.pnRight.Controls.Add(ucButtons);

            string path = formFrame.configManage.FileDir + @"\check.png";
            if (File.Exists(path))
            {
                bmCheck = new Bitmap(path);
            }
            path = formFrame.configManage.FileDir + @"\uncheck.png";
            if (File.Exists(path))
            {
                bmUncheck = new Bitmap(path);
            }
        }

        public new void Dispose()
        {
            ucButtons.Dispose();
            if (bmCheck != null) bmCheck.Dispose();
            if (bmUncheck != null) bmUncheck.Dispose();
            base.Dispose();
        }

        private bool Equal(object obj, int n)
        {
            if (obj == null) return false;
            bool bEqual = false;
            try
            {
                if (Convert.ToInt32(obj) == n)
                {
                    bEqual = true;
                }
            }
            catch{}
            return bEqual;
        }

        public void SetValue(ParamItem item, List<ParamOption> _listParamOption)
        {
            bAck = false;
            paramItem = item;
            InitPage();
            strTitle = item.name;
            listParamOption = _listParamOption;
            for (int i = 0; i < listParamOption.Count; i++)
            {
                TotalPageList.Add(listParamOption[i].display);
                if( Equal(item.param_value, listParamOption[i].value) )
                {
                    SelectIndex = i;
                }
            }
            GetPageTotal();
            if (SelectIndex >= 0)
            {
                PageIndex = SelectIndex / PageSize;
                CurSelectIndex = SelectIndex % PageSize;
                ShowPage(PageIndex);
            }
            else
            {
                ShowPage(0);
            }
            BeginInvoke(new System.EventHandler(UpdateUI), null);
        }

        private void UpdateUI(object obj, System.EventArgs e)
        {
            this.pnLeft.Invalidate();
        }

        public ParamItem GetValue()
        {
            return paramItem;
        }

        private void pnLeft_MouseDown(object sender, MouseEventArgs e)
        {
            int title_height = 100;
            if (Control.MousePosition.Y < (pnLeft.Top + title_height + 48)) return;     //状态栏高度为48pix
            int height = (pnLeft.Height - title_height) / PageSize;
            CurSelectIndex = (Control.MousePosition.Y - pnLeft.Top - title_height - 48) / height;
            pnLeft.Invalidate();
        }

        private void pnLeft_Click(object sender, EventArgs e)
        {
            int title_height = 100;
            if (Control.MousePosition.Y < (pnLeft.Top + title_height + 48)) return;     //状态栏高度为48pix
            int height = (pnLeft.Height - title_height) / PageSize;
            int tempIndex = (Control.MousePosition.Y - pnLeft.Top - title_height - 48) / height;
            if (tempIndex != CurSelectIndex)
            {
                CurSelectIndex = -1;
                pnLeft.Invalidate();
                return;                 //用户点选后移动出本条记录
            }

            int index = CurSelectIndex + PageIndex * PageSize;
            if ((index >= 0) && (index < listParamOption.Count))
            {
                SelectIndex = index;
                bAck = true;
                paramItem.param_value = (byte)listParamOption[index].value;
            }
            else
            {
                CurSelectIndex = -1;
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
                //e.Graphics.DrawEllipse(new Pen(Color.FromArgb(183, 222, 232), 2), 500, title_height + height * i + 20, 20, 20);
                if ((SelectIndex / PageSize == PageIndex) && (CurSelectIndex == i))
                {
                    //e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(179, 239, 255)), 13, title_height + height * i, pnLeft.Width - 26, height);
                    //e.Graphics.FillEllipse(new SolidBrush(Color.FromArgb(255, 165, 5)), 503, title_height + height * i + 23, 15, 15);
                    e.Graphics.DrawImage(bmCheck, 503, title_height + height * i + 20);
                }
                else
                {
                    e.Graphics.DrawImage(bmUncheck, 503, title_height + height * i + 20);
                }
                e.Graphics.DrawString(CurPageList[i], new Font("宋体", 24, FontStyle.Bold), new SolidBrush(Color.Black), 100, title_height + height * i + 16);
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
            CurSelectIndex = -1;
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

        #region 按键
        private void ClickUp()
        {
            PageIndex--;
            if (PageIndex < 0)
            {
                PageIndex++;
            }
            ShowPage(PageIndex);
            this.pnLeft.Invalidate();
        }

        private void ClickDown()
        {
            PageIndex++;
            if (PageIndex >= PageTotal)
            {
                PageIndex--;
            }
            ShowPage(PageIndex);
            this.pnLeft.Invalidate();
        }

        private void ClickAck()
        {
            if (CurSelectIndex != -1)
            {
                bAck = true;
                this.Close();
            } 
            else
            {
                FormMsgBox.Show("请选择某一项", "提示");
            }
        }

        private void ClickReturn()
        {
            bAck = false;
            this.Close();
        }
        #endregion
    }
}