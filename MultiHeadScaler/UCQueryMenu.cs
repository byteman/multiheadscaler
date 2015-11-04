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
                e.Graphics.DrawString(CurPageList[index], new Font("����", 24, FontStyle.Bold), new SolidBrush(Color.Black), x, y);
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
                case 0: //״̬
                    formFrame.ucListControl.InitData("״̬��ѯ", UCList.UCListType.UCLT_Status, this);
                    formFrame.ShowUC(formFrame.ucListControl);
                    break;
                case 1://����
                    formFrame.ucListControl.InitData("������ѯ", UCList.UCListType.UCLT_Alarm, this);
                    formFrame.ShowUC(formFrame.ucListControl);
                    break;
                case 2://����
                    formFrame.ucListControl.InitData("���ϲ�ѯ", UCList.UCListType.UCLT_Fault, this);
                    formFrame.ShowUC(formFrame.ucListControl);
                    break;
                case 3://��ʷ����
                    break;
                case 4://��־
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
                case 5://�豸��Ϣ
                    Int32 lpdwStorePages, lpdwRamPages, lpdwPageSize;
                    PInvoke.PGetSystemMemoryDivision(out lpdwStorePages, out lpdwRamPages, out lpdwPageSize);
                    string strInfo = string.Format("������:MStar CortexA7  ��   ��:{0}KB              ��   ��:1.0.10", lpdwRamPages * lpdwPageSize / 1024);
                    FormMsgBox.Show(strInfo, "�豸��Ϣ");
                    break;
                case 6://�����޸�
                    FormPwdChange dlg = new FormPwdChange(formFrame);
                    dlg.ShowDialog();
                    dlg.Dispose();
                    break;
                case 7://�û�ע��
                    if (formFrame.userManage.CurPurview == Purview.None)
                    {
                        FormMsgBox.Show("���û���¼��", "��ʾ");
                    } 
                    else
                    {
                        if (DialogResult.OK == FormMsgBox.Show("�Ƿ�ע����ǰ�û���","ע��",FormMsgBox.Buttons.OKCancel))
                        {
                            formFrame.userManage.CurPurview = Purview.None;
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        #region ��ҳ
        private void InitPage()
        {
            TotalPageList.Clear();
            TotalPageList.Add("״̬��ѯ");
            TotalPageList.Add("������ѯ");
            TotalPageList.Add("���ϲ�ѯ");
            TotalPageList.Add("��ʷ����");
            TotalPageList.Add("ͨ�����");
            TotalPageList.Add("�豸��Ϣ");
            TotalPageList.Add("�����޸�");
            TotalPageList.Add("�û�ע��");

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

        #region ����
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
