using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;

namespace Monitor
{
    public partial class UCRun : UserControl
    {
        public class HeadUIInfo
        {
            public HeadUIInfo(byte _i, Color _c, String _t)
            {
                state = _i;
                color = _c;
                title = _t;
            }
            public byte state;
            public Color color;
            public String title;

        }
        ScalerInfo si = new ScalerInfo();
        private List<HeadUIInfo> headInfos = new List<HeadUIInfo>();
        private byte session_id = 0;
        private FormFrame formFrame = null;
        private Bitmap bmBtnDown = null;
        private Bitmap bmBtnUp = null;
        private List<TextBox> focusBox;
        
        private TextBox curTxtBox = null;
        private byte start = 2;
        private byte session_timeout = 0;
        public UCRun(FormFrame f)
        {
            InitializeComponent();
            formFrame = f;
            if (focusBox == null)
            {
                focusBox = new List<TextBox>();
                focusBox.Add(textBox5);
                focusBox.Add(textBox6);
                focusBox.Add(textBox7);
                focusBox.Add(textBox8);
                focusBox.Add(textBox9);
                focusBox.Add(textBox10);
                focusBox.Add(textBox11);
                focusBox.Add(textBox12);
                focusBox.Add(textBox13);
                focusBox.Add(textBox14);


            }
            headInfos.Add(new HeadUIInfo(1, Color.Green, "M")); //参与组合
            headInfos.Add(new HeadUIInfo(2, Color.Red, "R")); //等待组合
            headInfos.Add(new HeadUIInfo(3, Color.Black, "U")); //进料斗故障
            headInfos.Add(new HeadUIInfo(4, Color.Black, "D")); //称重斗故障
            headInfos.Add(new HeadUIInfo(5, Color.Black, "S")); //模块故障
            headInfos.Add(new HeadUIInfo(6, Color.Black, "B")); //驱动卡故障
            headInfos.Add(new HeadUIInfo(7, Color.Black, "I")); //斗禁用
            headInfos.Add(new HeadUIInfo(8, Color.Blue, "Y")); //超重强排
            headInfos.Add(new HeadUIInfo(9, Color.Blue, "L")); //多次不参与组合强排
            headInfos.Add(new HeadUIInfo(10, Color.Blue, "P")); //开机自检通过


            string path = formFrame.configManage.FileDir + @"\main_btn_down.png";
            if (File.Exists(path))
            {
                bmBtnDown = new Bitmap(path);
            }
            path = formFrame.configManage.FileDir + @"\main_btn_up.png";
            if (File.Exists(path))
            {
                bmBtnUp = new Bitmap(path);
            }

            pbStart.Image = bmBtnUp;
            pbStop.Image  = bmBtnUp;
            pbExit.Image = bmBtnUp;
            pbSimu.Image = bmBtnUp;
            //timer1.Interval = 1000;
            //timer1.Tick += new EventHandler(timer1_Tick);
            //timer1.Enabled = true;
        }
        private void pbBtn_MouseDown(object sender, MouseEventArgs e)
        {
            if (bmBtnDown != null)
            {
                ((PictureBox)sender).Image = bmBtnDown;
            }
        }

        private void pbBtn_MouseUp(object sender, MouseEventArgs e)
        {
            if (bmBtnUp != null)
            {
                ((PictureBox)sender).Image = bmBtnUp;
            }
        }

        private void DrawLabel(object sender, PaintEventArgs e, string str)
        {
            e.Graphics.DrawString(str, new Font("宋体", 16, FontStyle.Bold), new SolidBrush(Color.Black), 40, 20);
        }
        public new void Dispose()
        {
            if (bmBtnDown != null) bmBtnDown.Dispose();
            if (bmBtnUp != null) bmBtnUp.Dispose();
            base.Dispose();
        }

        private void pbStart_Paint(object sender, PaintEventArgs e)
        {
            DrawLabel(sender, e, "启动");
        }

        private void pbStop_Paint(object sender, PaintEventArgs e)
        {
            DrawLabel(sender, e, "停止");
        }

        private void pbStop_Click(object sender, EventArgs e)
        {
            writeCmd(1, 2);
        }

        private void pbExit_Paint(object sender, PaintEventArgs e)
        {
            DrawLabel(sender, e, "退出");
        }

        private void pbExit_Click(object sender, EventArgs e)
        {
            writeCmd(1, 2);
           
            writeCmd(1, 2);
            formFrame.ShowUC(formFrame.ucMain);
        }

        private void label4_ParentChanged(object sender, EventArgs e)
        {

        }

        private void pbSimu_Paint(object sender, PaintEventArgs e)
        {
            DrawLabel(sender, e, "模拟运行");
        }
        private void read_all_weight()
        {
            List<ParamItem> itemList = new List<ParamItem>();
            ParamItem item;
            item = new ParamItem();

            item.dev_id = formFrame.configManage.cfg.paramDeviceId.Ctrl;
            item.param_id = 4; //读取全部驱动板重量
            item.op_write = 0; //读取
            item.param_type = TypeCode.Empty;
            item.param_len = 0;
            item.param_value = 0;
            itemList.Add(item);

            item = new ParamItem();

            item.dev_id = formFrame.configManage.cfg.paramDeviceId.Ctrl;
            item.param_id = 3; //读取全部驱动板重量
            item.op_write = 0; //读取
            item.param_type = TypeCode.Empty;
            item.param_len = 0;
            item.param_value = 0;
            itemList.Add(item);

            item = new ParamItem();

            item.dev_id = (byte)formFrame.configManage.cfg.paramDeviceId.Ctrl;
            item.param_id = 1; //获取状态.
            item.op_write = 0;
            item.param_type = TypeCode.Byte;
            item.param_len = 1;
            item.param_value = 0;
            itemList.Add(item);

            send(itemList);

        }
      
        private void timer1_Tick(object sender, EventArgs e)
        {
          
        }
        private void UpdateUI(object obj, System.EventArgs e)
        {
            if (start == 0)
                lblStatus.Text = "停止";
            else if (start == 1)
                lblStatus.Text = "运行";
            else if(start == 2)
                lblStatus.Text = "停止中";

            //更新每个banocx上面的重量.

            for (int i = 0; i < 10; i++)
            {
                banOcxCtl1.SetBanWeight(i + 1, si.getWeightString(i));
                banOcxCtl1.SetBanColor(i + 1, si.getStatusColor(i));
                banOcxCtl1.SetBanStatus(i + 1, si.getStatusString(i));

            }
            banOcxCtl1.BanRefresh();
        }
        private void pbStart_Click(object sender, EventArgs e)
        {
            writeCmd(1, 1);
            
        }

        private void textBox5_GotFocus(object sender, EventArgs e)
        {
            SetFocusTextBox();
        }
        private TextBox getFocusTextBox()
        {
            foreach (TextBox b in focusBox)
            {
                if (b.Focused) return b;
            }
            return null;

        }
        private void SetFocusTextBox()
        {
            foreach (TextBox b in focusBox)
            {
                if (b.Focused)
                {
                    b.BackColor = Color.Yellow;
                    curTxtBox = b;
                }
                else b.BackColor = Color.White;
            }
        }
        private void send(List<ParamItem> itemList)
        {
            Protocol protocol = formFrame.protocol;
            SerialOperate Serial = SerialOperate.instance;

            byte[] buf;
            int len = protocol.Produce(formFrame.configManage.cfg.paramDeviceId.Ctrl, out buf, itemList);
            if (len > 0)
            {
                Serial.Send(buf, len);
            }
        }
       
        private void writeCmd(byte cmd,byte v)
        {
            List<ParamItem> itemList = new List<ParamItem>();
            ParamItem item;
            item = new ParamItem();


            item.dev_id = (byte)formFrame.configManage.cfg.paramDeviceId.Ctrl;
            item.param_id = cmd; //振动一次
            item.op_write = 1;
            item.param_type = TypeCode.Byte;
            item.param_len = 1;
            item.param_value = v;
            itemList.Add(item);
            send(itemList);
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            TextBox box = curTxtBox;
            if (box != null)
            { 
                Int32 v = Convert.ToInt32(box.Text);

                box.Text = (v + 1).ToString();
            }
        }

        private void btnSub_Click(object sender, EventArgs e)
        {
            TextBox box = curTxtBox;
            if (box != null)
            {
                Int32 v = Convert.ToInt32(box.Text);

                box.Text = (v - 1).ToString();
            }
            

        }
        /*
         
        #define	SCALE_NUM_MAX			10
        typedef struct {
            u8		scale_num;	//总的秤个数
            u8		qualified;	//本次组合结果 合格与否 1:合格 0: 不合格[可能是强排之类]
            u8		comb_heads[SCALE_NUM_MAX]; //组合斗编号，表示参与组合的斗数，没有参与组合的斗数，我自己来计算
            u8		state[SCALE_NUM_MAX];	// 秤头状态,具体主动发送时个数由设置的秤台数量决定
            float	wet[SCALE_NUM_MAX];		// 各个秤头的当前重量
            u8		quali;					// 合格数
            u8		unquali;				// 不合格数
            float	quali_wet;				// 本次合格的总重量,不管合格与否，都有一个重量

        }resultDef;
         * 
         */

        

//注意这个属性不能少
        [StructLayoutAttribute(LayoutKind.Explicit, CharSet = CharSet.Auto,Size=68)]
    struct TestStruct
    {
        [FieldOffset(0)]
        [MarshalAs(UnmanagedType.U1, SizeConst = 1)]
        public byte		scale_num;	//总的秤个数
        [FieldOffset(1)]
        [MarshalAs(UnmanagedType.U1, SizeConst = 1)]
        public byte		qualified;	//本次组合结果 合格与否 1:合格 0: 不合格[可能是强排之类]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        [FieldOffset(2)]
        byte[]		comb_heads; //组合斗编号，表示参与组合的斗数，没有参与组合的斗数，我自己来计算
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        [FieldOffset(12)]
        byte[]		state;	// 秤头状态,具体主动发送时个数由设置的秤台数量决定
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        [FieldOffset(22)]
        float[]	wet;		// 各个秤头的当前重量
        [FieldOffset(62)]
        [MarshalAs(UnmanagedType.U1, SizeConst = 1)]
        byte		quali;					// 合格数
        [FieldOffset(63)]
        [MarshalAs(UnmanagedType.U1, SizeConst = 1)]
        byte unquali;				// 不合格数
        [FieldOffset(64)]
        [MarshalAs(UnmanagedType.R4, SizeConst = 4)]
        float	quali_wet;				// 本次合格的总重量,不管合格与否，都有一个重量
    }

        class MultiScalerInfo
        {
            public MultiScalerInfo()
            { 
            
            }
            public byte scale_num = 10;
            public byte qualified = 0;
            public byte[] comb_heads = new byte[10];
            public byte[] state = new byte[10];
            public float[] wet = new float[10];
            public byte quali;
            public byte unquali;
            public float  qual_wet; 
        }
        
        public static object BytesToStuct(byte[] bytes, Type type)
        {
            //得到结构体的大小
            int size = Marshal.SizeOf(type);
            //byte数组长度小于结构体的大小
            if (size > bytes.Length)
            {
                //返回空
                //return null;
            }
            //分配结构体大小的内存空间
            IntPtr structPtr = Marshal.AllocHGlobal(size);
            //将byte数组拷到分配好的内存空间
            Marshal.Copy(bytes, 0, structPtr, size);
            //将内存空间转换为目标结构体
            object obj = Marshal.PtrToStructure(structPtr, type);
            //释放内存空间
            Marshal.FreeHGlobal(structPtr);
            //返回结构体
            return obj;
        }
        public HeadUIInfo FindHeadByStatus(byte status)
        {
            //异常的返回称重斗故障
            if (status < 1) return headInfos[4];
            if (status > 10) return headInfos[4];
            return headInfos[status - 1];
        }
        private void parseInfo(object o)
        {
            MultiScalerInfo info = new MultiScalerInfo();
            WeightData wd = new WeightData();
            byte[] arr = (byte[])o;
            if (session_id == arr[0])
            {
                return ;
            }
            //TestStruct ss = (TestStruct)BytesToStuct(arr, System.Type.GetType("TestStruct", true));
            info.scale_num = arr[1];
            info.qualified = arr[2];
            for (int i = 0; i < 10; i++)
            {
                byte addr = arr[3 + i];
                info.comb_heads[i] = addr;

                info.state[i] = arr[13 + i];
                if (info.state[i] == 1) //参与组合
                {
                    //添加参与组合的编号.
                    wd.addZuhe(addr);
                }
                info.wet[i] = BitConverter.ToSingle(arr, 23 + i * 4);
                banOcxCtl1.SetBanWeight(addr, info.wet[i].ToString());
                banOcxCtl1.SetBanColor(addr,  FindHeadByStatus(info.state[i]).color);
                banOcxCtl1.SetBanStatus(addr, FindHeadByStatus(info.state[i]).title);
            }
            banOcxCtl1.BanRefresh();
            info.qualified = arr[63];
            info.unquali = arr[64];
            info.qual_wet = BitConverter.ToSingle(arr,65);
            
            lbl_hege.Text = "合格数: " + info.qualified.ToString();
            lbl_unhege.Text = "不合格数: " + info.unquali.ToString();
            txb_wet.Text = info.qual_wet.ToString("0.0");

            wd.diff = info.qual_wet - formFrame.curFormula.target_weight;
            wd.weight = info.qual_wet;
            //添加一条组合记录.
            SQLiteDBHelper.addData(wd);
            session_id = arr[0];
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
        public Image GetPicBitmap(int id)
        {
            string path = String.Format(formFrame.configManage.FileDir + @"\formula\{0}.jpg", id);
            return GetBitmap(path);
        }
      
        internal void SetReturnValue(List<ParamItem> itemList)
        {
           
            foreach (ParamItem item in itemList)
            {
                if (item.dev_id == 128) //控制器的命令回应
                {
                    if (item.param_id == 2) //组合结果
                    {
                        parseInfo(item.param_value);
                        ackData(session_id);
                        return;
                        //item.param_value;
                    }
                    else if ( (item.param_id == 1) && (item.op_write == 0)) //启动停止反馈.
                    {
                        byte s = (byte)item.param_value;
                        if (s == 0)
                        {
                            // 停止成功

                        }
                        else if (s == 1)
                        { 
                            //正在运行
                        }
                        else if (s == 2)
                        {
                            // 停止
                        }
                        start = s;
                      
                    }
                    else if (item.param_id == 3) //读取所有秤头状态
                    {
                        //item.param_value
                        si.updateStatusObj(item.param_value);
                    }
                    else if (item.param_id == 4) //读取所有秤头重量
                    {
                        //item.param_value
                        si.updateWeightObj(item.param_value);
                    }
                    else
                    {
                        return;
                    }
                    BeginInvoke(new System.EventHandler(UpdateUI), null);
                   
                }
            }
        }

        private void ackData(byte session_id)
        {
            Protocol protocol = formFrame.protocol;
            SerialOperate Serial = SerialOperate.instance;
            byte[] ack = new byte[] { 0x69, 0x80, 0xc2, 0x01, 0x01, 0x80, 0x02, 0x01, session_id, 0x0, 0x0 };
            ushort uCrc = Util.Crc16(ack, (ushort)9);
            byte[] byCrc = BitConverter.GetBytes(uCrc);
            Util.SwapBuf(byCrc);

           
            ack[9] = byCrc[0];
            ack[10] = byCrc[1];
            Serial.Send(ack, 11);
        }

        private void banOcxCtl1_Validated(object sender, EventArgs e)
        { 
            //MessageBox.Show("banOcxCtl1_Validated");
        }

        private void banOcxCtl1_Resize(object sender, EventArgs e)
        {
            //
            
        }

        private void UCRun_Resize(object sender, EventArgs e)
        {
            
        }

        private void UCRun_GotFocus(object sender, EventArgs e)
        {
            
          
        }

        public void Init()
        {
            formFrame.load_formula_data();
            pbName.Image = GetPicBitmap(formFrame.FormulaID);
            textBox1.Text = formFrame.curFormula.target_weight.ToString();
            textBox2.Text = formFrame.curFormula.up_diff.ToString();
            textBox3.Text = formFrame.curFormula.down_diff.ToString();

        }

        private void label5_ParentChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_LostFocus(object sender, EventArgs e)
        {
            if (getFocusTextBox() == null)
            {
               // curTxtBox = null;
            }
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            if (session_timeout++ > 25)
            {
                session_id = 0;
                session_timeout = 0;
            }
            
            if (this.Visible)
            {
                read_all_weight();
            }
        }
    }
}
