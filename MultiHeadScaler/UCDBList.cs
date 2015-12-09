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
    public partial class UCDBList : UserControl
    {

        public struct SMyItem
        {
            public int write;
            public int read;
            public string col;
            public string str;
        }
        public struct SPageItem
        {
            public byte read;
            public byte write;
            public string str;
        }
        public struct XZJ_Param
        {
            public byte drv_id;
            public byte strength;
            public byte time;
        };
        private Queue<XZJ_Param> xzjpars = new Queue<XZJ_Param>();

        private FormFrame formFrame = null;
        UCButtons ucButtons = null;
        UserControl ucRetControl = null;

        private int PageTotal = 0, PageIndex = 0;
        private const int PageSize = 5;
        private int formula_id = 1;
        private int formula_num = 0;
        private int SelectIndex = -1;
        private int CategoryIndex = -1;
        private int title_height = 92;
        Bitmap bmLeftUp = null;
        Bitmap bmLeftDown = null;

        Bitmap bmRightUp = null;
        Bitmap bmRightDown = null;
        Bitmap bmDownloadUp = null;
        Bitmap bmDownloadDown = null;
        private string strTitle = "";

        private List<ParamItem> CurPageList = new List<ParamItem>(PageSize);
        private List<ParamItem> TotalPageList = new List<ParamItem>();

       // private List<ParamItem> TotalParamList = new List<ParamItem>();
     
        const int MaxItemStringlen = 30;

        public UCDBList(FormFrame f)
        {
            InitializeComponent();
            formFrame = f;
            ucButtons = new UCButtons(f, this.pnRight);
            ucButtons.RegisterBtnEvent(ClickUp, ClickDown, ClickAck, ClickReturn, ClickExt);
            ucButtons.SetExtVisible(true);
            this.pnRight.Controls.Add(ucButtons);

            bmRightUp  = GetBitmap(formFrame.configManage.FileDir + @"\east_btn_up.png");
            bmRightDown = GetBitmap(formFrame.configManage.FileDir + @"\east_btn_down.png");

            bmLeftUp= GetBitmap(formFrame.configManage.FileDir + @"\west_btn_up.png");
            bmLeftDown= GetBitmap(formFrame.configManage.FileDir + @"\west_btn_down.png");

            bmDownloadUp = GetBitmap(formFrame.configManage.FileDir + @"\main_btn_up.png");
            bmDownloadDown = GetBitmap(formFrame.configManage.FileDir + @"\main_btn_down.png");

            pbLeft.Image = bmLeftUp;
            pbRight.Image = bmRightUp;
           
        }
        public new void Dispose()
        {
            if (bmLeftUp != null) bmLeftUp.Dispose();
            if (bmLeftDown != null) bmLeftDown.Dispose();

            if (bmRightUp != null) bmRightUp.Dispose();
            if (bmRightDown != null) bmRightDown.Dispose();

            if (bmDownloadUp != null) bmDownloadUp.Dispose();
            if (bmDownloadDown != null) bmDownloadDown.Dispose();

            ucButtons.Dispose();

            base.Dispose();
        }

        private Bitmap GetBitmap(string upPath)
        {
            Bitmap bm = null;
            if (File.Exists(upPath))
            {
                bm = new Bitmap(upPath);
            }
            return bm;
        }

   

        private void pbLeft_MouseDown(object sender, MouseEventArgs e)
        {
            pbLeft.Image = bmLeftDown;
        }

        private void pbLeft_MouseUp(object sender, MouseEventArgs e)
        {
            pbLeft.Image = bmLeftUp;
        }

        private void pbRight_MouseDown(object sender, MouseEventArgs e)
        {
            pbRight.Image = bmRightDown;
        }

        private void pbRight_MouseUp(object sender, MouseEventArgs e)
        {
            pbRight.Image = bmRightUp;
        }

        int getFormulaNum()
        {
            return SQLiteDBHelper.ParamCount();
        }
        //״̬�������������б��ʼ��
        public void InitData(int _categoryIndex,  string title, UserControl _ucRetControl)
        {
            ucButtons.SetAckText("����");
            ucButtons.SetAckVisible(true);

            if ((_categoryIndex >= 0) && (_categoryIndex < formFrame.visCateList.Count))
            {
                CategoryIndex = _categoryIndex;
            }
            else
            {
                CategoryIndex = 0;
            }
            //��ȡ����������
            strTitle = formFrame.visCateList[CategoryIndex].name; ;
          
            ucRetControl = _ucRetControl;
            InitPage();
            formula_id = formFrame.configManage.cfg.paramFormWeight.FormulaID;
            LoadFormulaFromDB(formula_id);//��ȡ��һ���䷽����
            formula_num = getFormulaNum();
            GetPageTotal();
            ShowPage(0);
            this.pnLeft.Invalidate();
        }
        private string getDBColumName(string title)
        {
            if (title == "Ŀ������") return "target_weight";
            else if (title == "�ٶ�") return "packet_per_minitue";
            else if (title == "��ƫ��") return "up_diff";
            else if (title == "��ƫ��") return "down_diff";
            else if (title == "�ȶ�ʱ��") return "stable_time";
            else if (title == "ȥƤ����") return "tare_count";
            else if (title == "AFC") return "AFC";
            else if (title == "�����") return "no_comb";
            else if (title == "����ģʽ") return "feed_mode";
            else if (title == "���η���") return "feed_in_turn";
            else if (title == "�䷽����") return "formula_name";
            else if (title == "�䷽���") return "id";
            else if (title == "���ģʽ") return "motor_mode";
            else if (title == "��η���") return "multi_feed";
            else if (title == "����ͼƬ") return "pic_id";
            else if (title == "ǿ�����") return "force_comb";
            else if (title == "����ͣ��") return "open_delay";
            return "xx";
        }
       
        private void LoadFormulaFromDB(int id)
        {
            DataTable dt = SQLiteDBHelper.listFormula(id);
            DataRow   dr = null;
            formula_num = getFormulaNum();
            if (dt.Rows.Count != 0)
            {
                //û�����ݾͷ���.
                //return;
               dr = dt.Rows[0]; //ȡ��һ����¼
            }
            SelectIndex = -1;
            PageIndex = 0;
            TotalPageList.Clear();
            
            

            Category cate = null;
            cate = formFrame.visCateList[CategoryIndex];
            foreach (ParamDefineItem param in cate.list)
            {
            
                ParamItem pitem = new ParamItem();
    
                pitem.dev_id = param.dev_id;
                pitem.max = param.max;
                pitem.min = param.min;
                pitem.name = param.name;
                pitem.op_write = param.write;
                pitem.param_id = param.param_id;
              
                pitem.param_len = param.param_len;
               
                pitem.param_type = param.param_type;
                pitem.unit = param.unit;
                pitem.valid_min_max = param.valid_min_max;
                pitem.permit_write = param.write;
                pitem.permit_read = param.read;
                //����һ����ʾ�������ַ���.
                if (dr == null)
                {
                    pitem.param_value = 0;
                    pitem.str = FormatDisplay(pitem.name,"");
                }
                else 
                {
                    if (param.param_id == 48)
                    {
                        pitem.str = FormatDisplay(pitem.name, "�������...");
                    }
                    else
                    {
                        pitem.param_value = dr[getDBColumName(param.name)];
                        pitem.str = FormatDisplay(pitem.name, dr[getDBColumName(pitem.name)].ToString());
                    }
                    
                }

                TotalPageList.Add(pitem);
            }

        }

        private void pnLeft_Paint(object sender, PaintEventArgs e)
        {
         
            //int title_height = 90;
          
            int height = (pnLeft.Height - title_height) / PageSize;
            string sstrTitle = String.Format(strTitle + "{0}/{1}", formula_num > 0 ? formula_id : 0, formula_num);
            //int x_pos = (pnLeft.Right - title.Length*32 - 148)/2;
            int title_left = (pnLeft.Width - (sstrTitle.Length) * 32) / 2;
            //e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(85, 142, 213)), 0, title_height - 2, pnLeft.Right - 1, 2);
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(225, 249, 255)), 0, 1, pnLeft.Right - 1, 100);
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(85, 142, 213)), 0, title_height, pnLeft.Right - 1, 2);

            e.Graphics.DrawString(sstrTitle, new Font("����", 32, FontStyle.Bold), new SolidBrush(Color.Black), title_left, title_height / 2 - 16);

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
                if (CurPageList[i].param_id == 34)
                {
                    int id = int.Parse(CurPageList[i].param_value.ToString());
                    Image bm = GetPicBitmap(id);
                    e.Graphics.DrawString(CurPageList[i].name, new Font("����", 24, FontStyle.Bold), new SolidBrush(Color.Black), 30, title_height + height * i + 16);
                    e.Graphics.DrawImage(bm, new Rectangle(390, title_height + height * i , height, height), new Rectangle(0, 0, bm.Width, bm.Height), GraphicsUnit.Pixel);
                    //e.Graphics.DrawImage(, 0, 0);
                }
                else
                {
                    if (CurPageList[i].permit_write != 0)
                    {
                        //����״̬.
                        e.Graphics.DrawString(strItem, new Font("����", 24, FontStyle.Bold), new SolidBrush(Color.Black), 30, title_height + height * i + 16);
                    }
                    else
                    {
                        e.Graphics.DrawString(strItem, new Font("����", 24, FontStyle.Bold), new SolidBrush(Color.Gray), 30, title_height + height * i + 16);
                    }
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
            //���浽���ݿ�

            var dic = new Dictionary<string, object>();
            foreach (ParamItem i in TotalPageList)
            {
                dic[getDBColumName(i.name)] = i.param_value;
            }
            var cond = new Dictionary<string, object>();
            int id = ParseID(TotalPageList[0].param_value);
            cond["formula_id"] = id;
            DataTable dt = SQLiteDBHelper.listFormula(id);
            if (dt.Rows.Count == 0)
            {
                SQLiteDBHelper.addFormula(dic);
            }
            else
            {
                SQLiteDBHelper.updateFormula(dic, cond);
            }
            
        }

        private void ClickReturn()
        {
           
            if (ucRetControl != null) formFrame.ShowUC(ucRetControl);
            else formFrame.ShowUC(formFrame.ucMain);
        }
        
        private void ClickExt()
        {
            //download to controller

            Protocol protocol = formFrame.protocol;
            SerialOperate Serial = SerialOperate.instance;
            List<ParamItem> itemList = new List<ParamItem>();
            //���ؿ���������.
            foreach (ParamItem item in TotalPageList)
            {
                item.dev_id = formFrame.configManage.cfg.paramDeviceId.Ctrl;
                item.op_write = 1;
                if (item.param_id == 33) //��������
                    item.param_len = (byte)item.param_value.ToString().Length;
                if(item.param_id != 48) //��������
                    itemList.Add(item);
            }
            byte[] buf;
            int len = protocol.Produce(formFrame.configManage.cfg.paramDeviceId.Ctrl, out buf, itemList);
            if (len > 0)
            {
                Serial.Send(buf, len);
            }

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
            
            #region  ʹ�ô˶λᵼ����Ļ�����������Σ�����ͨ�ŵ���ʱ����ҳ�����仯
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
            //int title_height = 90;
            if (Control.MousePosition.Y < (pnLeft.Top + title_height )) return;     //״̬���߶�Ϊ48pix
            int height = (pnLeft.Height - title_height) / PageSize;
            SelectIndex = (Control.MousePosition.Y - pnLeft.Top - title_height) / height;
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
        private int ParseID(object o)
        {
            return int.Parse(o.ToString());
        }
        private void pnLeft_Click(object sender, EventArgs e)
        {
            byte slaveAddr = formFrame.configManage.cfg.paramDeviceId.Ctrl;
            //int title_height = 90;
            if (Control.MousePosition.Y < (pnLeft.Top + title_height)) return;     //״̬���߶�Ϊ48pix
            int height = (pnLeft.Height - title_height) / PageSize;
            int tempIndex = (Control.MousePosition.Y - pnLeft.Top - title_height) / height;
            if (tempIndex != SelectIndex)
            {
                SelectIndex = -1;
                pnLeft.Invalidate();
                return;     //�û���ѡ���ƶ���������¼
            }
            int index = SelectIndex + PageIndex * PageSize;

            ParamItem item;

            item = TotalPageList[index];
            if (item.param_id == 34)
            {
                FormPicture dlg = new FormPicture(formFrame);
                dlg.ShowDialog();
                dlg.Dispose();
                int id = dlg.GetSelectPicID();
                if (id != -1)
                {
                    //MessageBox.Show(id.ToString());
                    TotalPageList[index].param_value = id;
                    ShowPage(PageIndex); //ˢ�µ�ǰҳ������.
                    pnLeft.Invalidate();
                }
            }
            else if (item.param_id == 48)
            {
                int id = ParseID(TotalPageList[0].param_value);
                formFrame.ucXzj.XZJ_Load(id);
                formFrame.ShowUC(formFrame.ucXzj);
                
                //TotalPageList[index].param_value = "�������...";
                //ShowPage(PageIndex); //ˢ�µ�ǰҳ������.
               // pnLeft.Invalidate();
               
            }
            else
            {
                //����������û���������
                InputInterface dlg;

                dlg = new FormInput(this.formFrame);

                dlg.SetValue(item, true);

                dlg.ShowDialog();
                pnLeft.Invalidate();                                  //������FormMsgBox�Ի�����ʧ����Ļûˢ�µ����
                if (dlg.GetAck())
                {
                    //������޸����߲���������Ҫ�������������
                    UpdateTotalList(index, dlg.GetValue());


                    ShowPage(PageIndex); //ˢ�µ�ǰҳ������.
                    pnLeft.Invalidate();

                }
                dlg.Dispose();
            }
           



            SelectIndex = -1;
        }

        private void UpdateTotalList(int index, ParamItem paramItem)
        {

            TotalPageList[index] = paramItem; //�������ڴ��еĲ���.
            TotalPageList[index].str = FormatDisplay(TotalPageList[index].name, TotalPageList[index].param_value.ToString());
        }

        private void pbLeft_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawString("��һ��", new Font("����", 24, FontStyle.Bold), new SolidBrush(Color.Black), 32, 28);
        }

        private void pbRight_Paint(object sender, PaintEventArgs e)
        {
      
            e.Graphics.DrawString("��һ��", new Font("����", 24, FontStyle.Bold), new SolidBrush(Color.Black), 32, 28);   
        }

        private void pbLeft_Click(object sender, EventArgs e)
        {
            formula_id--;
            if (formula_id < 1)
            {
                formula_id = 1;
                return;
            }
         
            LoadFormulaFromDB(formula_id);

            ShowPage(0);
            this.pnLeft.Invalidate();
        }

        private void pbRight_Click(object sender, EventArgs e)
        {

            formula_id++;
            if (formula_id > formula_num)
            {
                formula_id--;
                return;
            }

            LoadFormulaFromDB(formula_id);

            ShowPage(0);
            this.pnLeft.Invalidate();

           
        }
        public Image GetPicBitmap(int id)
        {
            string path = String.Format(formFrame.configManage.FileDir + @"\formula\{0}.jpg", id);
            return GetBitmap(path);
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
           
          

        }

        private void UCDBList_Click(object sender, EventArgs e)
        {

        }

        private void pbDownload_Click(object sender, EventArgs e)
        {

        }

        private void pbDownload_MouseDown(object sender, MouseEventArgs e)
        {
            
        }

        private void pbDownload_MouseUp(object sender, MouseEventArgs e)
        {
            
        }

        private void pbDownload_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawString("����", new Font("����", 24, FontStyle.Bold), new SolidBrush(Color.Black), 32, 28);
        }


        internal void SetReturnValue(List<ParamItem> itemList)
        {
            if (itemList.Count == TotalPageList.Count-1)
            {
                foreach (ParamItem i in itemList)
                {
                    if (i.param_valid != 1)
                    {
                        string msg = String.Format("�䷽����:{0} д��ʧ��", i.name);
                        FormMsgBox.Show(msg,"����");
                        
                        break;

                    }
                }
                //formFrame.configManage.cfg.paramFormWeight.FormulaID = (byte)formula_id;
                //formFrame.configManage.Serialize(); //����.
                //downloadQX();
                FormMsgBox.Show("�䷽�������سɹ�!!!", "��ʾ");
                
            }
           
    
        }

        private void downloadQX()
        {
            throw new NotImplementedException();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
        }
    }
}
