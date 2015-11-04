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
    public class UCQueryMenu:UCMenu
    {
        private int PageTotal, PageIndex;
        private const int PageSize = 6;
        List<string> TotalPageList = new List<string>();
        List<string> CurPageList = new List<string>(PageSize);
        
        public UCQueryMenu(FormFrame f):base(f)
        {
            ucButtons.RegisterBtnEvent(ClickUp, ClickDown, null, null);
            InitPage();
            GetPageTotal();
            ShowPage(0);
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
                e.Graphics.DrawString(CurPageList[index], new Font("宋体", 24, FontStyle.Bold), new SolidBrush(Color.Black), x, y);
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
            switch (index)
            {
                case 0: //状态
                    formFrame.ucListControl.InitData("状态查询", UCList.UCListType.UCLT_Status, this);
                    formFrame.ShowUC(formFrame.ucListControl);
                    break;
                case 1://报警
                    formFrame.ucListControl.InitData("报警查询", UCList.UCListType.UCLT_Alarm, this);
                    formFrame.ShowUC(formFrame.ucListControl);
                    break;
                case 2://故障
                    formFrame.ucListControl.InitData("故障查询", UCList.UCListType.UCLT_Fault, this);
                    formFrame.ShowUC(formFrame.ucListControl);
                    break;
                case 3://历史数据
                    break;
                case 4://日志
                    //formFrame.ShowUC(formFrame.ucDiagnose);
                    if (formFrame.formLog == null)
                    {
                        formFrame.formLog = new FormLog(formFrame);
                        formFrame.formLog.Left = 200;
                        formFrame.formLog.Top = 200;
                        formFrame.formLog.Show();
                    }
                    else
                    {
                        formFrame.formLog.WindowState = FormWindowState.Normal;
                        formFrame.formLog.TopMost = true;
                    }
                    break;
                case 5://设备信息
                    Int32 lpdwStorePages, lpdwRamPages, lpdwPageSize;
                    PInvoke.PGetSystemMemoryDivision(out lpdwStorePages, out lpdwRamPages, out lpdwPageSize);
                    string strInfo = string.Format("处理器:MStar CortexA7  内   存:{0}KB              版   本:1.0.10", lpdwRamPages * lpdwPageSize / 1024);
                    FormMsgBox.Show(strInfo, "设备信息");
                    break;
                case 6://密码修改
                    FormPwdChange dlg = new FormPwdChange(formFrame);
                    dlg.ShowDialog();
                    dlg.Dispose();
                    break;
                case 7://用户注销
                    if (formFrame.userManage.CurPurview == Purview.None)
                    {
                        FormMsgBox.Show("无用户登录！", "提示");
                    } 
                    else
                    {
                        if (DialogResult.OK == FormMsgBox.Show("是否注销当前用户？","注销",FormMsgBox.Buttons.OKCancel))
                        {
                            formFrame.userManage.CurPurview = Purview.None;
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        #region 分页
        private void InitPage()
        {
            TotalPageList.Clear();
            TotalPageList.Add("状态查询");
            TotalPageList.Add("报警查询");
            TotalPageList.Add("故障查询");
            TotalPageList.Add("历史数据");
            TotalPageList.Add("通信诊断");
            TotalPageList.Add("设备信息");
            TotalPageList.Add("密码修改");
            TotalPageList.Add("用户注销");

            CurPageList.Clear();
            PageTotal = 0;
            PageIndex = 0;
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
