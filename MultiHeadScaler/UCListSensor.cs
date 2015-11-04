using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Monitor
{
    public partial class UCListSensor : UserControl
    {
        private FormFrame formFrame = null;
        UCButtons ucButtons = null;
        UserControl ucRetControl = null;
        private int PageTotal, PageIndex;
        private const int PageSize = 5;
        int SelectIndex = -1;
        int CategoryIndex = -1;
        private string strTitle = "";
        private List<string> CurPageList = new List<string>(PageSize);
        private List<string> TotalPageList = new List<string>();
        List<ParamItem> itemListSend = new List<ParamItem>();
        List<ParamItem> itemListRecv = new List<ParamItem>();
        List<ParamItem> itemListModify = new List<ParamItem>();

        public Timer timer = new Timer();   //自动刷新定时器
        const int MaxItemStringlen = 30;

        public UCListSensor(FormFrame f)
        {
            InitializeComponent();
            formFrame = f;
            ucButtons = new UCButtons(f, this.pnRight);
            ucButtons.RegisterBtnEvent(ClickUp, ClickDown, ClickAck, ClickReturn);
            this.pnRight.Controls.Add(ucButtons);

            timer.Interval = 1000;
            timer.Tick += new EventHandler(timer_Tick);
        }

        //设置菜单列表初始化
        public bool InitData(int _categoryIndex, UserControl _ucRetControl, bool _bValidSensor, byte _SensorAddr)
        {
            ucRetControl = _ucRetControl;
            Protocol protocol = formFrame.protocol;
            SerialOperate Serial = SerialOperate.instance;
            InitPage();

            if ((_categoryIndex >= 0) && (_categoryIndex < formFrame.visCateList.Count))
            {
                CategoryIndex = _categoryIndex;
            }
            else
            {
                CategoryIndex = 0;
            }
            byte refresh = formFrame.visCateList[CategoryIndex].refresh;
            RefreshControl(refresh);

            strTitle = formFrame.visCateList[CategoryIndex].name;

            TotalPageList.Clear();
            Category cate = null;
            cate = formFrame.visCateList[CategoryIndex];
            foreach (ParamDefineItem param in cate.list)
            {
                TotalPageList.Add(param.name);
            }
            GetPageTotal();
            ShowPage(0);
            return true;
        }

        void RefreshControl(byte refresh)
        {
            if (refresh == 0)               //不刷新
            {
                timer.Enabled = false;
                ucButtons.SetAckVisible(false);
            }
            else if (refresh == 1)          //手动刷新
            {
                timer.Enabled = false;
                ucButtons.SetAckText("刷新");
                ucButtons.SetAckVisible(true);
            }
            else                            //自动刷新
            {
                ucButtons.SetAckVisible(false);
                timer.Enabled = true;
            }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(DateTime.Now.ToLocalTime().ToString() + "UCList timer run!");
        }

        public void SetReturnValue(List<ParamItem> itemList)
        {
            CurPageList.Clear();
            itemListRecv.Clear();
            bool bRet;
            for (int i = 0; i < itemListSend.Count; i++)
            {
                bRet = false;
                for (int j = 0; j < itemList.Count; j++)
                {
                    if ((itemListSend[i].dev_id == itemList[j].dev_id) && (itemListSend[i].param_id == itemList[j].param_id))
                    {
                        bRet = true;
                        itemList[j].name = itemListSend[i].name;
                        itemListSend[i].param_value = itemList[j].param_value;
                        Resolve(itemList[j]);
                        break;
                    }
                }
                itemListRecv.Add(itemListSend[i]);  //定义itemListRecv是为了当开始收到了控制器回复，然而某次操作没收到回复，显示器任然显示上次收到的回复数据
                if (!bRet) CurPageList.Add(itemListSend[i].name);
            }
            BeginInvoke(new System.EventHandler(UpdateUI), null);
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
                string strItem = CurPageList[i];
                if (strItem.Length > MaxItemStringlen)
                {
                    strItem = strItem.Substring(0, MaxItemStringlen - 2) + "...";
                }
                e.Graphics.DrawString(strItem, new Font("宋体", 24, FontStyle.Bold), new SolidBrush(Color.Black), 30, title_height + height * i + 16);
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
            if (timer.Enabled)
            {
                timer.Enabled = false;
            }
             ShowPage(PageIndex);
        }

        private void ClickReturn()
        {
            if (timer.Enabled)
            {
                timer.Enabled = false;
            }
            if (ucRetControl != null) formFrame.ShowUC(ucRetControl);
            else formFrame.ShowUC(formFrame.ucMain);
        }

        private void Resolve(ParamItem item)
        {
            string strLine;
            if (item.op_write != 0)
            {
                if (item.param_valid == 1)
                {
                    strLine = item.name + " OK";
                }
                else
                {
                    strLine = item.name + " ERROR";
                }
            }
            else
            {
                /*if (item.param_value != null)
                {
                    strLine = Convert.ChangeType(item.param_value, item.param_type, null).ToString();
                } 
                else
                {
                    strLine = "null";
                }*/
                switch (item.param_type)
                {
                    case TypeCode.Byte:
                        switch (item.param_id)
                        {
                            default:
                                strLine = Convert.ToByte(item.param_value).ToString();
                                break;
                        }
                        break;
                    case TypeCode.UInt16:
                        strLine = Convert.ToUInt16(item.param_value).ToString();
                        break;
                    case TypeCode.Int32:
                        strLine = Convert.ToInt32(item.param_value).ToString();
                        break;
                    case TypeCode.UInt32:
                        if ((item.dev_id == formFrame.configManage.cfg.paramDeviceId.Ctrl) && (item.param_id == formFrame.configManage.cfg.paramFormWeight.Ip))   //IP地址特殊处理
                        {
                            UInt32 uIp = Convert.ToUInt32(item.param_value);
                            strLine = string.Format("{0:D}.{1:D}.{2:D}.{3:D}", (Byte)(uIp >> 24), (Byte)(uIp >> 16), (Byte)(uIp >> 8), (Byte)uIp);
                        }
                        else
                        {
                            strLine = Convert.ToUInt32(item.param_value).ToString();
                        }
                        break;
                    case TypeCode.Single:
                        strLine = Convert.ToSingle(item.param_value).ToString();
                        break;
                    case TypeCode.String:
                        byte[] v = (byte[])item.param_value;
                        switch (item.param_id)
                        {
                            default:
                                strLine = Util.ByteToStringDec(v, v.Length, "-");
                                break;
                        }
                        break;
                    default:
                        strLine = "null";
                        break;
                }
                strLine = item.name + " " + strLine;
            }
            CurPageList.Add(strLine);
        }

        private void InitPage()
        {
            TotalPageList.Clear();
            CurPageList.Clear();
            itemListRecv.Clear();
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
            if (index == (PageTotal - 1))
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

            if (CategoryIndex < 0) return;
            if (CategoryIndex == formFrame.configManage.cfg.paramFormWeight.EverySensorCateIndex) return;   //浏览各个传感器的参数
            Category cate = formFrame.visCateList[CategoryIndex];
            itemListSend.Clear();
            for (int i = 0; i < CurPageSize; i++)
            {
                ParamDefineItem defItem = cate.list[index * PageSize + i];
                ParamItem item = new ParamItem();
                item.name = defItem.name;

                item.dev_id = defItem.dev_id;

                item.param_id = defItem.param_id;
                itemListSend.Add(item);
            }
            Protocol protocol = formFrame.protocol;
            SerialOperate Serial = SerialOperate.instance;
            byte[] buf;
            int len = protocol.Produce(formFrame.configManage.cfg.paramDeviceId.Ctrl, out buf, itemListSend);
            if (len > 0)
            {
                Serial.Send(buf, len);
            }
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

            if (CategoryIndex < 0) return;
            Category cate = formFrame.visCateList[CategoryIndex];
            if (index >= cate.list.Count) return;

            if ((SelectIndex < 0) || (SelectIndex >= CurPageList.Count))
            {
                return;
            }

            if (CategoryIndex == formFrame.configManage.cfg.paramFormWeight.EverySensorCateIndex)    //浏览各个传感器的参数
            {
                FormMsgBox.Show("index:" + index.ToString() + ",SelectIndex:" + SelectIndex.ToString(), "tip");
                return;
            }

            bool bOverLen = (CurPageList[SelectIndex].Length > MaxItemStringlen);
            if ((cate.list[index].write != 0) || bOverLen)
            {
                if (timer.Enabled)
                {
                    timer.Enabled = false;
                }
                ParamItem item;
                if ((SelectIndex >= 0) && (SelectIndex < itemListRecv.Count))
                {
                    item = itemListRecv[SelectIndex];
                }
                else
                {
                    item = itemListSend[SelectIndex];
                }
                item.param_type = cate.list[index].param_type;
                item.param_len = cate.list[index].param_len;
                item.unit = cate.list[index].unit;
                item.op_write = cate.list[index].write;


                if ((cate.list[index].listOption != null) && (cate.list[index].listOption.Count > 0))
                {
                    //选择框用于用户选择数据
                    FormRadio dlg = new FormRadio(this.formFrame);
                    dlg.SetValue(item, cate.list[index].listOption);
                    dlg.ShowDialog();
                    if (dlg.bAck)
                    {
                        if (JudgePurview())
                        {
                            item = dlg.GetValue();
                            item.op_write = 1;
                            Protocol protocol = formFrame.protocol;
                            SerialOperate Serial = SerialOperate.instance;
                            byte[] buf;
                            itemListModify.Clear();
                            itemListModify.Add(item);
                            int len = protocol.Produce(formFrame.configManage.cfg.paramDeviceId.Ctrl, out buf, itemListModify);
                            if (len > 0) Serial.Send(buf, len);

                            System.Threading.Thread.Sleep(100);
                            ShowPage(PageIndex);
                            pnLeft.Invalidate();
                        }
                    }
                    dlg.Dispose();
                }
                else
                {
                    //输入框用于用户输入数据
                    InputInterface dlg;
                    dlg = new FormInput(this.formFrame);

                    dlg.SetValue(item, !bOverLen);
                    dlg.ShowDialog();
                    if (dlg.GetAck())
                    {
                        if (JudgePurview())
                        {
                            item = dlg.GetValue();
                            item.op_write = 1;
                            Protocol protocol = formFrame.protocol;
                            SerialOperate Serial = SerialOperate.instance;
                            byte[] buf;
                            itemListModify.Clear();
                            itemListModify.Add(item);
                            int len = protocol.Produce(formFrame.configManage.cfg.paramDeviceId.Ctrl, out buf, itemListModify);
                            if (len > 0) Serial.Send(buf, len);

                            System.Threading.Thread.Sleep(100);
                            ShowPage(PageIndex);
                            pnLeft.Invalidate();
                        }
                    }
                    dlg.Dispose();
                }


                if (formFrame.visCateList[CategoryIndex].refresh == 2)
                {
                    timer.Enabled = true;
                }
            }
            SelectIndex = -1;
        }

        private bool JudgePurview()
        {
            //弹出对话框，让用户以管理员登陆
            if (formFrame.userManage.CurPurview != Purview.CtrlAdmin)
            {
                FormPwd dlg = new FormPwd(formFrame, Purview.CtrlAdmin, false); //验证密码模式
                dlg.ShowDialog();
                dlg.Dispose();
            }
            return (formFrame.userManage.CurPurview == Purview.CtrlAdmin);       //如果用户以管理员身份登录了
        }
    }
}
