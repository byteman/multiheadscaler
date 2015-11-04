using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Monitor
{
    public partial class UCList : UserControl
    {
        public enum UCListType
        {
            UCLT_NONE,
            UCLT_Status,
            UCLT_Alarm,
            UCLT_Fault,
            UCLT_Param,
        }

        public struct SPageItem
        { 
            public byte read;
            public byte write;
            public string str;
        }

        private FormFrame formFrame = null;
        UCButtons ucButtons = null;
        UserControl ucRetControl = null;
        public UCListType uclType = UCListType.UCLT_NONE;
        private int PageTotal, PageIndex;
        private const int PageSize = 5;
        int SelectIndex = -1;
        int CategoryIndex = -1;
        private string strTitle = "";
        private List<SPageItem> CurPageList = new List<SPageItem>(PageSize);
        private List<SPageItem> TotalPageList = new List<SPageItem>();
        List<ParamItem> itemListSend = new List<ParamItem>();
        List<ParamItem> itemListRecv = new List<ParamItem>();
        List<ParamItem> itemListModify = new List<ParamItem>();

        private bool bValidSensor = false;  //true:指传感器操作
        private byte SensorAddr;
        public Timer timer = new Timer();   //自动刷新定时器
        const int MaxItemStringlen = 30;

        public UCList(FormFrame f)
        {
            InitializeComponent();
            formFrame = f;
            ucButtons = new UCButtons(f, this.pnRight);
            ucButtons.RegisterBtnEvent(ClickUp, ClickDown, ClickAck, ClickReturn);
            this.pnRight.Controls.Add(ucButtons);

            timer.Interval = 1000;
            timer.Tick += new EventHandler(timer_Tick);
        }

        //状态、报警、故障列表初始化
        public void InitData(string title, UCListType _uclType, UserControl _ucRetControl)
        {
            ucButtons.SetAckText("刷新");
            ucButtons.SetAckVisible(true);
            bValidSensor = false;
            CategoryIndex = -1;
            strTitle = title;
            uclType = _uclType;
            ucRetControl = _ucRetControl;
            InitPage();
            RefreshData();

            byte refresh = 0;
            switch (uclType)
            {
                case UCListType.UCLT_Status:
                    refresh = formFrame.configManage.cfg.paramFormWeight.RefreshStatus;
                    break;
                case UCListType.UCLT_Alarm:
                    refresh = formFrame.configManage.cfg.paramFormWeight.RefreshAlarm;
                    break;
                case UCListType.UCLT_Fault:
                    refresh = formFrame.configManage.cfg.paramFormWeight.RefreshFault;
                    break;
                default:
                    return;
            }
            RefreshControl(refresh);
        }

        //状态、报警、故障列表刷新
        private void RefreshData()
        {
            System.Diagnostics.Debug.WriteLine(DateTime.Now.ToLongTimeString() + " RefreshData!");
            Protocol protocol = formFrame.protocol;
            SerialOperate Serial = SerialOperate.instance;
            ParamItem item;
            byte[] buf;
            int len;
            switch (uclType)
            {
                case UCListType.UCLT_Status:
                    item = new ParamItem();
                    item.name = "状态";
                    item.dev_id = formFrame.configManage.cfg.paramDeviceId.Ctrl;
                    item.param_id = formFrame.configManage.cfg.paramFormWeight.Status.Id;
                    itemListSend.Clear();
                    itemListSend.Add(item);
                    len = protocol.Produce(formFrame.configManage.cfg.paramDeviceId.Ctrl, out buf, itemListSend);
                    if (len > 0) Serial.Send(buf, len);
                    break;

                case UCListType.UCLT_Alarm:
                    item = new ParamItem();
                    item.name = "报警";
                    item.dev_id = formFrame.configManage.cfg.paramDeviceId.Ctrl;
                    item.param_id = formFrame.configManage.cfg.paramFormWeight.Alarm;
                    itemListSend.Clear();
                    itemListSend.Add(item);
                    len = protocol.Produce(formFrame.configManage.cfg.paramDeviceId.Ctrl, out buf, itemListSend);
                    if (len > 0) Serial.Send(buf, len);
                    break;

                case UCListType.UCLT_Fault:
                    item = new ParamItem();
                    item.name = "故障";
                    item.dev_id = formFrame.configManage.cfg.paramDeviceId.Ctrl;
                    item.param_id = formFrame.configManage.cfg.paramFormWeight.Fault;
                    itemListSend.Clear();
                    itemListSend.Add(item);
                    len = protocol.Produce(formFrame.configManage.cfg.paramDeviceId.Ctrl, out buf, itemListSend);
                    if (len > 0) Serial.Send(buf, len);
                    break;
                default:
                    return;
            }
        }

        //设置菜单列表初始化
        public bool InitData(int _categoryIndex, UCListType _uclType, UserControl _ucRetControl, bool _bValidSensor, byte _SensorAddr)
        {
            bValidSensor = _bValidSensor;
            SensorAddr = _SensorAddr;
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
            if(bValidSensor)    strTitle += " " + SensorAddr + "#";   
            if ((_categoryIndex == formFrame.configManage.cfg.paramFormWeight.OneSensorCateIndex) && (!bValidSensor))
            {
                formFrame.ucRadioOnline.InitData();
                formFrame.ShowUC(formFrame.ucRadioOnline);
                return false;
            }
            else if (_categoryIndex == formFrame.configManage.cfg.paramFormWeight.CalibCateIndex)
            {
                //FormCalib formCalib = new FormCalib(formFrame);
                //formCalib.ShowDialog();
                return false;
            }

            uclType = _uclType;
            switch (uclType)
            {
                case UCListType.UCLT_Param:
                    TotalPageList.Clear();
                    Category cate = null;
                    cate = formFrame.visCateList[CategoryIndex];
                    foreach (ParamDefineItem param in cate.list)
                    {
                        SPageItem spItem;
                        spItem.read = param.read;
                        spItem.write = param.write;
                        spItem.str = param.name;
                        TotalPageList.Add(spItem);
                    }
                    GetPageTotal();
                    ShowPage(0);
                    break;

                default:
                    break;
            }
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

            if (CategoryIndex < 0)
            {
                RefreshData();
            }
            else
            {
                ShowPage(PageIndex);
                //this.pnLeft.Invalidate();
            }
        }

        public void SetReturnValue(List<ParamItem> itemList)
        {
            switch (uclType)
            {
                case UCListType.UCLT_Status:
                case UCListType.UCLT_Alarm:
                case UCListType.UCLT_Fault:
                    if ( (itemList.Count == 1) && (itemList[0].param_type == TypeCode.UInt32))
                    {
                        Resolve(uclType, Convert.ToUInt32(itemList[0].param_value));
                        GetPageTotal();
                        ShowPage(PageIndex);
                        BeginInvoke(new System.EventHandler(UpdateUI), null);
                    }
                    break;
                case UCListType.UCLT_Param:
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
                                itemList[j].permit_read = itemListSend[i].permit_read;
                                itemList[j].permit_write = itemListSend[i].permit_write;
                                bool bRes = Resolve(itemList[j]);
                                if (!bRes)
                                {
                                    formFrame.ucRadioOnline.InitData();
                                    formFrame.ShowUC(formFrame.ucRadioOnline);
                                    return;
                                }
                                
                                break;
                            }
                        }
                        itemListRecv.Add(itemListSend[i]);  //定义itemListRecv是为了当开始收到了控制器回复，然而某次操作没收到回复，显示器任然显示上次收到的回复数据
                        if (!bRet)
                        {
                            SPageItem spItem;
                            spItem.read = itemListSend[i].permit_read;
                            spItem.write = itemListSend[i].permit_write;
                            spItem.str = itemListSend[i].name;
                            CurPageList.Add(spItem);
                        }
                    }
                    BeginInvoke(new System.EventHandler(UpdateUI), null);
                    break;
                default:
                    break;
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
            int title_left = (pnLeft.Width - (strTitle.Length)*36)/2;
            int height = (pnLeft.Height - title_height) / PageSize;

            e.Graphics.FillRectangle(new  SolidBrush(Color.FromArgb(85, 142, 213)), 0, title_height-2, pnLeft.Right - 1, 2);
            e.Graphics.DrawString(strTitle, new Font("宋体", 32, FontStyle.Bold), new SolidBrush(Color.Black), title_left, title_height / 2 - 16);

            for(int i = 1; i<PageSize; i++)
            {
                e.Graphics.DrawLine(new Pen(Color.FromArgb(183,222,232)), 13, title_height + height * i, pnLeft.Right - 44, title_height + height * i);
            }

            for (int i = 0; i < CurPageList.Count; i++)
            {
                if (SelectIndex == i)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(179, 239, 255)), 13, title_height + height * i, pnLeft.Width-26, height);
                }
                string strItem = CurPageList[i].str;
                if (strItem.Length > MaxItemStringlen)
                {
                    strItem = strItem.Substring(0, MaxItemStringlen-2) + "...";
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
            if (timer.Enabled)
            {
                timer.Enabled = false;
            }
            if (CategoryIndex < 0)
            {
                RefreshData();
            } 
            else
            {
                ShowPage(PageIndex);
                //this.pnLeft.Invalidate();
            }
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

        private void Resolve(UCListType uclType, UInt32 data)
        {
            List<StatusAlarmFault> safList = null;
            switch (uclType)
            {
                case UCListType.UCLT_Status:
                    TotalPageList.Clear();
                    safList = formFrame.configManage.cfg.StatusList;
                    foreach (StatusAlarmFault saf in safList)
                    {
                        SPageItem spItem;
                        spItem.read = 1;
                        spItem.write = 0;
                        if ((data & (1 << saf.bit)) != 0)
                        {
                            spItem.str = FormatDisplay(saf.name, "是");
                        }
                        else
                        {
                            spItem.str = FormatDisplay(saf.name, "否");
                        }
                        TotalPageList.Add(spItem);
                    }
                    break;

                case UCListType.UCLT_Alarm:
                    TotalPageList.Clear();
                    safList = formFrame.configManage.cfg.AlarmList;
                    foreach (StatusAlarmFault saf in safList)
                    {
                        SPageItem spItem;
                        spItem.read = 1;
                        spItem.write = 0;
                        if ((data & (1 << saf.bit)) != 0)
                        {
                            spItem.str = FormatDisplay(saf.name, "报警");
                        }
                        else
                        {
                            spItem.str = FormatDisplay(saf.name, "正常");
                        }
                        TotalPageList.Add(spItem);
                    }
                    break;

                case UCListType.UCLT_Fault:
                    TotalPageList.Clear();
                    safList = formFrame.configManage.cfg.FaultList;
                    foreach (StatusAlarmFault saf in safList)
                    {
                        SPageItem spItem;
                        spItem.read = 1;
                        spItem.write = 0;
                        if ((data & (1 << saf.bit)) != 0)
                        {
                            spItem.str = FormatDisplay(saf.name, "故障");
                        }
                        else
                        {
                            spItem.str = FormatDisplay(saf.name, "正常");
                        }
                        TotalPageList.Add(spItem);
                    }
                    break;

                case UCListType.UCLT_Param:

                    break;

                default:
                    break;
            }
        }

        private bool Resolve(ParamItem item)
        {
            string strLine;
            if (item.op_write != 0)
            {
                if (item.param_valid == 1)
                {
                    strLine = item.name + " OK";

                    if ((item.dev_id <= formFrame.configManage.cfg.paramDeviceId.AllSensor) && (item.param_id == Protocol.ParamIdSensorID))
                    {
                       return false;
                    }
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
                        if ( (item.dev_id == formFrame.configManage.cfg.paramDeviceId.Ctrl) && (item.param_id == formFrame.configManage.cfg.paramFormWeight.Ip) )   //IP地址特殊处理
                        {
                            UInt32 uIp = Convert.ToUInt32(item.param_value);
                            strLine = string.Format("{0:D}.{1:D}.{2:D}.{3:D}", (Byte)(uIp >> 24), (Byte)(uIp >> 16), (Byte)(uIp >> 8), (Byte)uIp);
                        } 
                        else
                        {
                            switch (item.param_id)
                            {
                                case Protocol.ParamIdSoftVer:
                                case Protocol.ParamIdHardVer:
                                    UInt32 ver = Convert.ToUInt32(item.param_value);
                                    strLine = string.Format("{0:D}.{1:D}.{2:D}", (Byte)(ver >> 16), (Byte)(ver >> 8), (Byte)(ver));
                                    break;
                                default:
                                    strLine = Convert.ToUInt32(item.param_value).ToString();
                                    break;
                            }
                        }
                        break;
                    case TypeCode.Single:
                        strLine = Convert.ToSingle(item.param_value).ToString();
                        break;
                    case TypeCode.DateTime:
                        strLine = Convert.ToDateTime(item.param_value).ToString("MM-dd HH:mm:ss");
                        break;
                    case TypeCode.String:
                        byte[] v = (byte[])item.param_value;
                        switch(item.param_id)
                        {
                            case Protocol.ParamIdFixAddr:   //安装地址，去除0
                                List<byte> addrList = new List<byte>();
                                for (int i = 0; i < v.Length; i++ )
                                {
                                    if (v[i] != 0)
                                    {
                                        addrList.Add(v[i]);
                                    }
                                }
                                strLine = Util.ByteToStringDec(addrList.ToArray(), addrList.Count, "-");
                                break;
                            case Protocol.ParamIdPlate:     //车牌
                                strLine = formFrame.CodeToPlate( Util.EncodeToString(v) );
                                break;
                            case Protocol.ParamIdSIM:       //SIM
                                strLine = Util.EncodeToString(v);
                                break;
                            case Protocol.ParamIdCpuID:     //CPUID
                                strLine = Util.ByteToStringHex(v, v.Length);
                                break;

                            default:
                                strLine = Util.ByteToStringDec(v, v.Length, "-");
                                break;
                        }
                        break;
                    default:
                        strLine = "null";
                        break;
                }
                //strLine = item.name + " " + strLine;
                strLine = FormatDisplay(item.name, strLine);
            }
            SPageItem spItem;
            spItem.read = item.permit_read;
            spItem.write = item.permit_write;
            spItem.str = strLine;
            CurPageList.Add(spItem);
            return true;
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

            if (CategoryIndex < 0) return;
            if (CategoryIndex == formFrame.configManage.cfg.paramFormWeight.EverySensorCateIndex) return;   //浏览各个传感器的参数
            Category visCate = formFrame.visCateList[CategoryIndex];
            
            itemListSend.Clear();
            for (int i = 0; i < CurPageSize;i++)
            {
                ParamDefineItem defItem = visCate.list[index * PageSize + i];
                if (defItem.visible != 0)
                {
                    ParamItem item = new ParamItem();
                    item.name = defItem.name;
                    item.permit_read = defItem.read;
                    item.permit_write = defItem.write;
                    if (bValidSensor)
                    {
                        item.dev_id = SensorAddr;
                    }
                    else
                    {
                        item.dev_id = defItem.dev_id;
                    }
                    item.param_id = defItem.param_id;
                    itemListSend.Add(item);
                }
            }
            Protocol protocol = formFrame.protocol;
            SerialOperate Serial = SerialOperate.instance;
            byte[] buf;
            byte slaveAddr;
            if (CategoryIndex == formFrame.configManage.cfg.paramFormWeight.WirelessCateIndex)
            {
                slaveAddr = formFrame.configManage.cfg.paramDeviceId.MonitorWireless;
            } 
            else
            {
                slaveAddr = formFrame.configManage.cfg.paramDeviceId.Ctrl;
            }
            int len = protocol.Produce(slaveAddr, out buf, itemListSend);
            if (len > 0)
            {
                Serial.Send(buf, len);
            }
            itemListRecv.Clear();
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
            int index = SelectIndex+PageIndex*PageSize;

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
            else if (CategoryIndex == formFrame.configManage.cfg.paramFormWeight.WirelessCateIndex)
            {
                slaveAddr = formFrame.configManage.cfg.paramDeviceId.MonitorWireless;
            }

            bool bOverLen = (CurPageList[SelectIndex].str.Length > MaxItemStringlen);
            if ( (cate.list[index].write != 0) || bOverLen )
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

                item.permit_read = cate.list[index].read;
                item.permit_write = cate.list[index].write;
                item.valid_min_max = cate.list[index].valid_min_max;
                item.min = cate.list[index].min;
                item.max = cate.list[index].max;


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
                            int len = protocol.Produce(slaveAddr, out buf, itemListModify);
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
                    if (item.param_id == Protocol.ParamIdPlate)
                    {
                        dlg = new FormPlate(this.formFrame);        //车牌输入框
                    }
                    else if (item.param_id == Protocol.ParamIdDateTime)
                    {
                        dlg = new FormDateTime(this.formFrame);        //日期时间输入框
                    }
                    else
                    {
                        dlg = new FormInput(this.formFrame);
                    }

                    dlg.SetValue(item, !bOverLen);
                    dlg.ShowDialog();
                    pnLeft.Invalidate();                                  //处理弹出FormMsgBox对话框消失后，屏幕没刷新的情况
                    if (dlg.GetAck())
                    {
                        //如果是修改无线参数，不需要输入控制器密码
                        //如果不是修改无线参数，则需要输入控制器密码
                        if ( (CategoryIndex == formFrame.configManage.cfg.paramFormWeight.WirelessCateIndex) || JudgePurview())
                        {
                            item = dlg.GetValue();
                            item.op_write = 1;
                            Protocol protocol = formFrame.protocol;
                            SerialOperate Serial = SerialOperate.instance;
                            byte[] buf;
                            itemListModify.Clear();
                            itemListModify.Add(item);
                            int len = protocol.Produce(slaveAddr, out buf, itemListModify);
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
    }
}
