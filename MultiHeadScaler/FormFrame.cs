using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;

namespace Monitor
{
    public partial class FormFrame : Form
    {
        public readonly DateTime AppStartTime;       //应用程序启动时间
        private List<UserControl> ucList = new List<UserControl>();
        private UserControl ucCur = null;
        public UCStatus ucStatus = null;
        public UCMain ucMain = null;
        public UCRun ucRun = null;
        public UCCalib ucCalib = null;
        public UCDebug ucDebug = null;
        public UCQueryMenu ucQueryMenu = null;
        public UCSetMenu ucSetMenu = null;
        public UCList ucListControl = null;
        public UCRadioOnline ucRadioOnline = null;
        public UCCommon ucCommon = null;
        public FormLog formLog = null;

        public ConfigManage configManage = new ConfigManage();
        public List<Category> visCateList = new List<Category>();
        public Timer timer = new Timer();
        public Protocol protocol = new Protocol();
        SerialOperate Serial = SerialOperate.instance;

        public UserManage userManage = new UserManage();
        public bool bQueryStatusOn = true;

        public FormFrame()
        {
            InitializeComponent();

            AppStartTime = DateTime.Now;

            userManage.Init(this);

            if (!configManage.Deserialize()) Application.Exit();

            for (int i = 0; i < configManage.cfg.categoryList.Count; i++)
            {
                Category cate = new Category();
                cate.name = configManage.cfg.categoryList[i].name;
                cate.refresh = configManage.cfg.categoryList[i].refresh;
                cate.list = new List<ParamDefineItem>();
                for (int j = 0; j < configManage.cfg.categoryList[i].list.Count; j++)
                {
                    if (configManage.cfg.categoryList[i].list[j].visible != 0)
                    {
                        cate.list.Add(configManage.cfg.categoryList[i].list[j]);
                    }
                }
                visCateList.Add(cate);
            }

            protocol.InitParam(configManage);

            if (Environment.OSVersion.Platform == PlatformID.WinCE)
            {
                this.FormBorderStyle = FormBorderStyle.None;
            }

            ucStatus = new UCStatus(this);
            //this.Controls.Add(ucStatus);

            ucMain = new UCMain(this);
            this.AddUC(ucMain);
            ucRun = new UCRun(this);
            this.AddUC(ucRun);
            ucCalib = new UCCalib(this);
            this.AddUC(ucCalib);
            ucDebug = new UCDebug(this);
            this.AddUC(ucDebug);
            ucQueryMenu = new UCQueryMenu(this);
            this.AddUC(ucQueryMenu);
            ucSetMenu = new UCSetMenu(this);
            this.AddUC(ucSetMenu);
            ucListControl = new UCList(this);
            this.AddUC(ucListControl);
            ucRadioOnline = new UCRadioOnline(this);
            this.AddUC(ucRadioOnline);
            ucCommon = new UCCommon(this);
            this.AddUC(ucCommon);
            
            Serial.Init();
            Serial.RegisterEvent(sp_DataReceived, sp_Close, LogSend, LogRecv);
            int nOpen = Serial.Open(configManage.cfg.paramSerial.PortName, configManage.cfg.paramSerial.BaudRate, configManage.cfg.paramSerial.ReadWaitMs);
            if (nOpen < 1)
            {
                System.Diagnostics.Debug.WriteLine("open " + configManage.cfg.paramSerial.PortName + " error!");
            }

            //timer.Interval = configManage.cfg.paramFormWeight.Interval;
            timer.Interval = 500;
            timer.Tick += new EventHandler(timer_Tick);
            ShowUC(ucMain);
        }

        void timer_Tick(object sender, EventArgs e)
        {
            List<ParamItem> itemList = new List<ParamItem>();
            ParamItem item;

            item = new ParamItem();
            item.dev_id = configManage.cfg.paramDeviceId.Ctrl;
            item.param_id = configManage.cfg.paramFormWeight.CarryWeight;
            itemList.Add(item);

            item = new ParamItem();
            item.dev_id = configManage.cfg.paramDeviceId.Ctrl;
            item.param_id = configManage.cfg.paramFormWeight.TruckWeight;
            itemList.Add(item);

            item = new ParamItem();
            item.dev_id = configManage.cfg.paramDeviceId.Ctrl;
            item.param_id = configManage.cfg.paramFormWeight.TotalWeight;
            itemList.Add(item);

            item = new ParamItem();
            item.dev_id = configManage.cfg.paramDeviceId.Ctrl;
            item.param_id = configManage.cfg.paramFormWeight.RtWeight;
            itemList.Add(item);

            item = new ParamItem();
            item.dev_id = configManage.cfg.paramDeviceId.Ctrl;
            item.param_id = configManage.cfg.paramFormWeight.Status.Id;
            itemList.Add(item);

            byte[] buf;
            int len = protocol.Produce(configManage.cfg.paramDeviceId.Ctrl, out buf, itemList);
            if (len > 0)
            {
                Serial.Send(buf, len);
            }
        }

        private void FormFrame_Load(object sender, EventArgs e)
        {
            Rectangle rect = new Rectangle();
            PInvoke.SetFullScreen(true, ref rect);//显示
 
        }

        private void FormFrame_Closing(object sender, CancelEventArgs e)
        {
            Serial.Dispose();
            protocol.Dispose();
            ucMain.Dispose();
            ucQueryMenu.Dispose();
            ucSetMenu.Dispose();
            ucListControl.Dispose();
        }

        public void ClickRunBtn()
        {
            ShowUC(ucRun);
        }

        public void ClickSetBtn()
        {
            ucSetMenu.Reset();
            ShowUC(ucSetMenu);
        }

        public void ShowUC(UserControl _uc)
        {
            foreach (UserControl uc in ucList)
            {
                if (_uc == uc)
                {
                    ucCur = _uc;
                    if (ucMain == _uc)  timer.Enabled = true;
                    else    timer.Enabled = false;

                    uc.Show();
                }
                else
                {
                    uc.Hide();
                }
            }
        }

        public UserControl GetUC()
        {
            return ucCur;
        }

        private void AddUC(UserControl _uc)
        {
            _uc.Left = 0;
            _uc.Top = 0;
            _uc.Hide();
            ucList.Add(_uc);
            this.Controls.Add(_uc);
        }

        //车牌转换为编码
        public string PlateToCode(string strPlate)
        {
            string strCode = "";
            if (strPlate.Length < 1) return strCode;

            List<PlateChar> plateCharList = configManage.cfg.plateCharList;
            for (int i = 0; i < plateCharList.Count; i++)
            {
                if (plateCharList[i].display == strPlate[0].ToString())
                {
                    strCode = plateCharList[i].value + strPlate.Remove(0, 1);
                    break;
                }
            }

            return strCode;
        }

        //编码转换为车牌
        public string CodeToPlate(string strCode)
        {
            string strPlate = "";
            if (strCode.Length < 4) return strPlate;
            string plateValue = strCode.Remove(4, strCode.Length - 4);

            List<PlateChar> plateCharList = configManage.cfg.plateCharList;
            for (int i = 0; i < plateCharList.Count; i++)
            {
                if (plateCharList[i].value == plateValue)
                {
                    strPlate = plateCharList[i].display + strCode.Remove(0, 4);
                    break;
                }
            }
            return strPlate;
        }

        #region 串口操作
        void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            byte[] bufRecv;
            int nRecv = Serial.Recv(out bufRecv);
            if (nRecv <= 0) return;
            if (ucStatus != null) ucStatus.ReceiveOnePkg();
            List<ParamItem> itemList;
            int nResolve = protocol.Resolve(bufRecv, nRecv, out itemList);
            if (protocol.bToUserControl == true)
            {
                BeginInvoke(new System.EventHandler(DispenseUsrControl), itemList);
            }
            else
            {
                protocol.SetReturnValue(itemList);
            }
        }

        void sp_Close()
        {
            Application.DoEvents();
        }

        private void DispenseUsrControl(object obj, System.EventArgs e)
        {
            List<ParamItem> itemList = (List<ParamItem>)obj;

            //分发给状态栏
            if (itemList.Count == 1)
            {
                if ((itemList[0].param_id == configManage.cfg.paramFormWeight.Status.Id) &&
                    (itemList[0].dev_id == configManage.cfg.paramDeviceId.Ctrl))
                {
                    ucStatus.SetReturnValue(itemList[0]);       //状态栏处理

                    if ((ucListControl.Visible) && (ucListControl.uclType == UCList.UCListType.UCLT_Status))
                    {
                        ucListControl.SetReturnValue(itemList); //列表刚好是查询状态
                    }
                    return;
                }
            }

            //分发给当前显示的用户控件
            if (ucMain.Visible == true)
            {
                ucMain.SetReturnValue(itemList);
            }
            else if (ucListControl.Visible == true)
            {
                ucListControl.SetReturnValue(itemList);
            }
            else if (ucRadioOnline.Visible == true)
            {
                ucRadioOnline.SetReturnValue(itemList);
            }
            else if (ucCalib.Visible == true)
            {
                ucCalib.SetReturnValue(itemList);
            }
        }
        #endregion

        #region 日志操作
        public void LogSend(byte[] buffer, int len)
        {
            string strLog;
            strLog = "TX(" + len.ToString() + "|" + DateTime.Now.TimeOfDay.TotalMilliseconds.ToString() + "): " + Util.ByteToStringHex(buffer, len);
            System.Diagnostics.Debug.WriteLine(strLog);
            //this.ucDiagnose.AddLog(strLog);
            if (formLog != null)
            {
                formLog.AddLog(strLog);
            }
        }

        public void LogRecv(byte[] buffer, int len)
        {
            string strLog;
            strLog = "RX(" + len.ToString() + "|" + DateTime.Now.TimeOfDay.TotalMilliseconds.ToString() + "): " + Util.ByteToStringHex(buffer, len);
            System.Diagnostics.Debug.WriteLine(strLog);
            //this.ucDiagnose.AddLog(strLog);
            if (formLog != null)
            {
                formLog.AddLog(strLog);
            }
        }
        #endregion
    }
}