using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Monitor
{
    public class UCSetMenu : UCMenu
    {
        private int PageTotal, PageIndex;
        private const int PageSize = 6;
        List<Category> CurPageList = new List<Category>(PageSize);

        public UCSetMenu(FormFrame f)
            : base(f)
        {
            ucButtons.RegisterBtnEvent(ClickUp, ClickDown, null, null);
            InitPage();
            GetPageTotal();
            ShowPage(0);
        }

        public void Reset()
        {
            InitPage();
            GetPageTotal();
            ShowPage(0);
            PictrueBoxShow();
        }

        public override void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            float x = 50;
            float y = 28;
            int index = (int)pb.Tag;
            if ((index >= 0) && (index < CurPageList.Count))
            {
                pb.Visible = true;
                e.Graphics.DrawString(CurPageList[index].name, new Font("宋体", 24, FontStyle.Bold), new SolidBrush(Color.Black), x, y);
            }
            else
            {
                pb.Visible = false;
            }
            //switch ((int)pb.Tag)
            //{
            //    case 1:
            //        //str = "系统参数";
            //        break;
            //    case 2:
            //        str = "称重设置";
            //        break;
            //    case 3:
            //        str = "服务器";
            //        x = 70;
            //        break;
            //    case 4:
            //        str = "传感器";
            //        x = 70;
            //        break;
            //    case 5:
            //        str = "零位跟踪";
            //        break;
            //    case 6:
            //        str = "标定";
            //        x = 80;
            //        break;
            //    default:
            //        break;
            //}
        }

        public override void pictureBox1_Click(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
           int index = PageIndex*PageSize + (int)pb.Tag;
           bool bInit = formFrame.ucListControl.InitData(index, UCList.UCListType.UCLT_Param, this, false, 0);
           if(bInit)    formFrame.ShowUC(formFrame.ucListControl);
       }

        #region 分页
        private void InitPage()
        {
            CurPageList.Clear();
            PageTotal = 0;
            PageIndex = 0;
            ucButtons.SetPageCode(PageIndex, PageTotal);
        }

        private void GetPageTotal()
        {
            PageTotal = formFrame.visCateList.Count / PageSize;
            if ((formFrame.visCateList.Count % PageSize) != 0) PageTotal++;
            PageIndex = 0;
        }

        private void ShowPage(int index)
        {
            int CurPageSize;
            Config cfg = formFrame.configManage.cfg;
            if (PageTotal == 0) return;
            if (index == (PageTotal - 1))
            {
                CurPageSize = cfg.categoryList.Count - (index * PageSize);
            }
            else
            {
                CurPageSize = PageSize;
            }

            int start = index * PageSize;
            CurPageList.Clear();
            for (int i = 0; i < CurPageSize; i++)
            {
                CurPageList.Add(cfg.categoryList[start + i]);
            }

            ucButtons.SetPageCode(index, PageTotal);
        }
        #endregion

        #region 按键
        private void ClickUp()
        {
            PageIndex--;
            if (PageIndex < 0) PageIndex++;
            ShowPage(PageIndex);
            PictrueBoxShow();
        }

        private void ClickDown()
        {
            PageIndex++;
            if (PageIndex >= PageTotal)
            {
                PageIndex--;
            }
            else
            {
                ShowPage(PageIndex);
                PictrueBoxShow();
            }
        }
        #endregion

        private void PictrueBoxShow()
        {
            pictureBox1.Visible = true;
            pictureBox1.Invalidate();
            pictureBox2.Visible = true;
            pictureBox2.Invalidate();
            pictureBox3.Visible = true;
            pictureBox3.Invalidate();
            pictureBox4.Visible = true;
            pictureBox4.Invalidate();
            pictureBox5.Visible = true;
            pictureBox5.Invalidate();
            pictureBox6.Visible = true;
            pictureBox6.Invalidate();
        }
    }
}
