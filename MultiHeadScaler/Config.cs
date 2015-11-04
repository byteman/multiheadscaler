using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Monitor
{
    //串口配置
    public class ParamSerial
    {
        public string PortName;
        public int BaudRate;
        public int ReadWaitMs;
        public int ResendCount;
    }

    //协议中使用的字段域
    public class ParamProtocol
    {
        public byte M2S;
        public byte S2M;
        public byte REQ;
        public byte ACK;
    }

    //设备Id
    public class ParamDeviceId
    {
        public byte AllSensor;
        public byte Ctrl;
        public byte Debugger;
        public byte Monitor;
        public byte Other;
        public byte MonitorWireless;
    }

    //状态中的位
    public class ParamStatus
    {
        [XmlAttribute]
        public byte Id;             //查询状态的参数Id

        [XmlAttribute]
        public byte Alarm;          //返回的状态：有报警发生对应的位

        [XmlAttribute]
        public byte Fault;          //返回的状态：有故障发生对应的位

        [XmlAttribute]
        public byte Gps;            //返回的状态：Gps对应的位

        [XmlAttribute]
        public byte Gprs;           //返回的状态：Gprs对应的位

        [XmlAttribute]
        public byte Stable;         //返回的状态：稳定对应的位

        [XmlAttribute]
        public byte Zero;           //返回的状态：零位对应的位
    }

    //主窗体使用
    public class ParamFormWeight
    {
        public int Interval;        //称重界面定时查询间隔

        public int DisplayWeightMax;//最大称量的参数Id
        public int DisplayWeightMin;//最小称量的参数Id
        
        public byte CarryWeight;    //载货重量的参数Id
        public byte TruckWeight;    //车身重量的参数Id
        public byte TotalWeight;    //总重量的参数Id
        public byte RtWeight;       //实时重量的参数Id

        public byte Alarm;          //定时查询报警的参数Id
        public byte Fault;          //定时查询故障的参数Id

        public byte Online;         //查询哪些传感器在线的参数Id
        public byte Ip;             //IP地址的参数Id
        public byte WirelessCateIndex;      //无线参数参数种类索引，默认值为1
        public byte OneSensorCateIndex;     //单个传感器参数种类索引，该索引下的参数为传感器参数，默认值为1
        public byte CalibCateIndex;         //标定参数种类索引，该索引下的参数为传感器参数，默认值为2
        public byte EverySensorCateIndex;   //各个传感器参数种类索引，该索引下的参数为传感器参数，默认值为255
        public ParamStatus Status;  //定时查询状态

        public byte RefreshStatus;  //状态刷新控制  0:不刷新; 1:手动刷新; 2:自动刷新
        public byte RefreshAlarm;   //报警刷新控制  0:不刷新; 1:手动刷新; 2:自动刷新
        public byte RefreshFault;   //故障刷新控制  0:不刷新; 1:手动刷新; 2:自动刷新
    }

    //状态、报警、故障的各名称和对应的bit
    public class StatusAlarmFault
    {
        [XmlAttribute]
        public string name;

        [XmlAttribute]
        public byte bit;
    }

    //选项型参数
    public class ParamOption
    {
        [XmlAttribute]
        public string display;

        [XmlAttribute]
        public int value;
    }

    //设置参数的一项
    public class ParamDefineItem
    {
        [XmlAttribute]
        public byte visible;

        [XmlAttribute]
        public byte read;

        [XmlAttribute]
        public byte write;

        [XmlAttribute]
        public string name;

        [XmlAttribute]
        public string unit;

        [XmlAttribute]
        public byte dev_id;

        [XmlAttribute]
        public byte param_id;

        [XmlAttribute]
        public byte param_len;

        [XmlAttribute]
        public TypeCode param_type;

        [XmlAttribute]
        public bool valid_min_max;

        [XmlAttribute]
        public int min;

        [XmlAttribute]
        public int max;

        public List<ParamOption> listOption;

        /*public object param_value;*/              //此属性，仅为测试使用，终端设备不需要定义。
    }

    //设置参数分类
    public class Category
    {
        [XmlAttribute]
        public string name;         //类别名称

        [XmlAttribute]
        public byte refresh;        //0:不刷新; 1:手动刷新; 2:自动刷新

        public List<ParamDefineItem> list;
    }

    //车牌号中的字符
    public class PlateChar
    {
        [XmlAttribute]
        public string name;         //省的名称，如：重庆

        [XmlAttribute]
        public string display;      //车牌面板的显示，如：渝

        [XmlAttribute]
        public string value;        //该省对应的行政编码，如：5000
    }

    //配置文件
    public class Config
    {
        public Config()
        {

        }

        public string Version;

        //public string Desc;

        public ParamSerial paramSerial;

        public ParamProtocol paramProtocol;

        public ParamDeviceId paramDeviceId;

        public ParamFormWeight paramFormWeight;

        public List<StatusAlarmFault> StatusList, AlarmList, FaultList;

        public List<Category> categoryList;

        public List<PlateChar> plateCharList;
    }
}
