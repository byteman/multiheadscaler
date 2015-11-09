using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Monitor
{
    public partial class UCDBList : UserControl
    {

        public struct SMyItem
        {
            public int write;
            public int read;
            public string col;
            public string str;
        }
        private FormFrame formFrame = null;
        UCButtons ucButtons = null;
        UserControl ucRetControl = null;
     
        private int PageTotal, PageIndex;
        private const int PageSize = 5;
        int SelectIndex = -1;

     

        private string strTitle = "";

        private List<SMyItem> CurPageList = new List<SMyItem>(PageSize);
        private List<SMyItem> TotalPageList = new List<SMyItem>();

        private DataTable curData = null; 
     
        const int MaxItemStringlen = 30;

        public UCDBList(FormFrame f)
        {
            InitializeComponent();
            formFrame = f;
            ucButtons = new UCButtons(f, this.pnRight);
            ucButtons.RegisterBtnEvent(ClickUp, ClickDown, ClickAck, ClickReturn);
            this.pnRight.Controls.Add(ucButtons);

        }

        //状态、报警、故障列表初始化
        public void InitData(string title,  UserControl _ucRetControl)
        {
            ucButtons.SetAckText("刷新");
            ucButtons.SetAckVisible(true);
          
            strTitle = title;
          
            ucRetControl = _ucRetControl;
            InitPage();
            RereshData(0);
            GetPageTotal();
            ShowPage(0);
        }
        private void addTitle()
        {
            SMyItem item = new SMyItem();
            item.col = "目标重量";
            item.str = "目标重量";

            TotalPageList.Add(item);

            item = new SMyItem();
            item.col = "上偏差";
            item.str = "上偏差";

            TotalPageList.Add(item);
        }
        private void RereshData(int id)
        {
            DataTable dt = SQLiteDBHelper.listParam(id);
            TotalPageList.Clear();
            if (dt.Rows.Count == 0)
            {
                addTitle();
                return;
            }
            foreach (DataRow dr in dt.Rows)
            {
                SMyItem item = new SMyItem();
                item.col = "目标重量";
                item.str = dr["target_weight"].ToString();

                TotalPageList.Add(item);

                item = new SMyItem();
                item.col = "上偏差";
                item.str = dr["up_diff"].ToString();

                TotalPageList.Add(item);


            }
            
        }


        public new void Dispose()
        {
            ucButtons.Dispose();
            base.Dispose();
        }

        private void pnLeft_Paint(object sender, PaintEventArgs e)
        {
            int title_height = 100;
            int title_left = (pnLeft.Width - (strTitle.Length) * 36) / 2;
            int height = (pnLeft.Height - title_height) / PageSize;

            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(85, 142, 213)), 0, title_height - 2, pnLeft.Right - 1, 2);
            e.Graphics.DrawString(strTitle, new Font("宋体", 32, FontStyle.Bold), new SolidBrush(Color.Black), title_left, title_height / 2 - 16);

            for (int i = 1; i < PageSize; i++)
            {
                e.Graphics.DrawLine(new Pen(Color.FromArgb(183, 222, 232)), 13, title_height + height * i, pnLeft.Right - 44, title_height + height * i);
            }

            for (int i = 0; i < CurPageList.Count; i++)
            {
                if (SelectIndex == i)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(179, 239, 255)), 13, title_height + height * i, pnLeft.Width - 26, height);
                }
                string strItem = CurPageList[i].str;
                if (strItem.Length > MaxItemStringlen)
                {
                    strItem = strItem.Substring(0, MaxItemStringlen - 2) + "...";
                }
                if (CurPageList[i].write != 0)
                {
                    e.Graphics.DrawString(strItem, new Font("宋体", 24, FontStyle.Bold), new SolidBrush(Color.Black), 30, title_height + height * i + 16);
                }
                else
                {
                    e.Graphics.DrawString(strItem, new Font("宋体", 24, FontStyle.Bold), new SolidBrush(Color.Gray), 30, title_height + height * i + 16);
                }
            }
            e.Graphics.DrawRectangle(new Pen(Color.Blue), 0, 0, pnLeft.Width - 1, pnLeft.Height - 1);
             
          
        }

        private void UpdateUI(object obj, System.EventArgs e)
        {
            this.pnLeft.Invalidate();
        }

        private void ClickUp()
        {
            PageIndex--;
            if (PageIndex < 0) PageIndex++;
            else
            {
                ShowPage(PageIndex);
                this.pnLeft.Invalidate();
            }
        }

        private void ClickDown()
        {
            PageIndex++;
            if (PageIndex >= PageTotal) PageIndex--;
            else
            {
                ShowPage(PageIndex);
                this.pnLeft.Invalidate();
            }
        }

        private void ClickAck()
        {
            ShowPage(PageIndex);     
        }

        private void ClickReturn()
        {
           
            if (ucRetControl != null) formFrame.ShowUC(ucRetControl);
            else formFrame.ShowUC(formFrame.ucMain);
        }

       

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
        }

        private void ShowPage(int index)
        {
            int CurPageSize;
            Config cfg = formFrame.configManage.cfg;
            if (PageTotal == 0) return;
            if ( index == (PageTotal -1) )
            {
                CurPageSize = TotalPageList.Count - (index * PageSize);
            }
            else
            {
                CurPageSize = PageSize;
            }

            int start = index * PageSize;
            if (CurPageList.Count == 0)
            {
                for (int i = 0; i < CurPageSize; i++)
                {
                    CurPageList.Add(TotalPageList[start + i]);
                }
            }
            
            #region  使用此段会导致屏幕闪；但若屏蔽，无线通信掉包时，翻页后的项不变化
            CurPageList.Clear();
            for (int i = 0; i < CurPageSize; i++)
            {
                CurPageList.Add(TotalPageList[start + i]);
            }
            #endregion

            ucButtons.SetPageCode(index, PageTotal);

          
           
        }


        private void pnLeft_MouseDown(object sender, MouseEventArgs e)
        {
            int title_height = 100;
            if (Control.MousePosition.Y < (pnLeft.Top + title_height + 48)) return;     //状态栏高度为48pix
            int height = (pnLeft.Height - title_height) / PageSize;
            SelectIndex = (Control.MousePosition.Y - pnLeft.Top - title_height - 48) / height;
            int index = SelectIndex + PageIndex * PageSize;
            pnLeft.Invalidate();
        }

    

        private string FormatDisplay(string strFirst, string strSecond)
        {
            string strRet;
            int count = 0;

            char[] q = strFirst.ToCharArray();
            for (int i = 0; i < q.Length; i++)
            {
                if ((int)q[i] >= 0x4E00 && (int)q[i] <= 0x9FA5)
                {
                    count += 2;
                }
                else
                {
                    count += 1;
                }
            }
            
            if (count > 20)
            {
                count = 1;
            } 
            else
            {
                count = 20 - count;
            }

            if (strSecond.Length > 20)
            {
                count = 1;
            }

            for (int i = 0; i < count; i++ )
            {
                strFirst += " ";
            }

            strRet = string.Format("{0}{1}", strFirst, strSecond);
            return strRet;
        }

        private void pnLeft_Click(object sender, EventArgs e)
        {
            byte slaveAddr = formFrame.configManage.cfg.paramDeviceId.Ctrl;
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



            //输入框用于用户输入数据
            InputInterface dlg;

            dlg = new FormInput(this.formFrame);
            ParamItem item = new ParamItem();
            dlg.SetValue(
            dlg.ShowDialog();
            pnLeft.Invalidate();                                  //处理弹出FormMsgBox对话框消失后，屏幕没刷新的情况
            if (dlg.GetAck())
            {
                //如果是修改无线参数，不需要输入控制器密码

                ShowPage(PageIndex);
                pnLeft.Invalidate();

            }
            dlg.Dispose();



            SelectIndex = -1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("ww");
        }
    }
}
