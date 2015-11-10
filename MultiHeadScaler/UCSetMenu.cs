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
                e.Graphics.DrawString(CurPageList[index].name, new Font("ËÎÌå", 24, FontStyle.Bold), new SolidBrush(Color.Black), x, y);
            }
            else
            {
                pb.Visible = false;
            }
           
        }

        public override void pictureBox1_Click(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
           int index = PageIndex*PageSize + (int)pb.Tag;
           formFrame.ucDBListControl.InitData(0,"wwww", this);
           formFrame.ShowUC(formFrame.ucDBListControl);
       }

        #region ·ÖÒ³
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

        #region °´¼ü
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
