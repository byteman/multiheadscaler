using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Monitor
{
    //��������
    public class ParamSerial
    {
        public string PortName;
        public int BaudRate;
        public int ReadWaitMs;
        public int ResendCount;
    }

    //Э����ʹ�õ��ֶ���
    public class ParamProtocol
    {
        public byte M2S;
        public byte S2M;
        public byte REQ;
        public byte ACK;
    }

    //�豸Id
    public class ParamDeviceId
    {
        public byte AllSensor;
        public byte Ctrl;
        public byte Debugger;
        public byte Monitor;
        public byte Other;
        public byte MonitorWireless;
    }

    //״̬�е�λ
    public class ParamStatus
    {
        [XmlAttribute]
        public byte Id;             //��ѯ״̬�Ĳ���Id

        [XmlAttribute]
        public byte Alarm;          //���ص�״̬���б���������Ӧ��λ

        [XmlAttribute]
        public byte Fault;          //���ص�״̬���й��Ϸ�����Ӧ��λ

        [XmlAttribute]
        public byte Gps;            //���ص�״̬��Gps��Ӧ��λ

        [XmlAttribute]
        public byte Gprs;           //���ص�״̬��Gprs��Ӧ��λ

        [XmlAttribute]
        public byte Stable;         //���ص�״̬���ȶ���Ӧ��λ

        [XmlAttribute]
        public byte Zero;           //���ص�״̬����λ��Ӧ��λ
    }

    //������ʹ��
    public class ParamFormWeight
    {
        public int Interval;        //���ؽ��涨ʱ��ѯ���

        public int DisplayWeightMax;//�������Ĳ���Id
        public int DisplayWeightMin;//��С�����Ĳ���Id
        
        public byte CarryWeight;    //�ػ������Ĳ���Id
        public byte TruckWeight;    //���������Ĳ���Id
        public byte TotalWeight;    //�������Ĳ���Id
        public byte RtWeight;       //ʵʱ�����Ĳ���Id

        public byte Alarm;          //��ʱ��ѯ�����Ĳ���Id
        public byte Fault;          //��ʱ��ѯ���ϵĲ���Id

        public byte Online;         //��ѯ��Щ���������ߵĲ���Id
        public byte Ip;             //IP��ַ�Ĳ���Id
        public byte WirelessCateIndex;      //���߲�����������������Ĭ��ֵΪ1
        public byte OneSensorCateIndex;     //�������������������������������µĲ���Ϊ������������Ĭ��ֵΪ1
        public byte CalibCateIndex;         //�궨���������������������µĲ���Ϊ������������Ĭ��ֵΪ2
        public byte EverySensorCateIndex;   //�������������������������������µĲ���Ϊ������������Ĭ��ֵΪ255
        public ParamStatus Status;  //��ʱ��ѯ״̬

        public byte RefreshStatus;  //״̬ˢ�¿���  0:��ˢ��; 1:�ֶ�ˢ��; 2:�Զ�ˢ��
        public byte RefreshAlarm;   //����ˢ�¿���  0:��ˢ��; 1:�ֶ�ˢ��; 2:�Զ�ˢ��
        public byte RefreshFault;   //����ˢ�¿���  0:��ˢ��; 1:�ֶ�ˢ��; 2:�Զ�ˢ��
    }

    //״̬�����������ϵĸ����ƺͶ�Ӧ��bit
    public class StatusAlarmFault
    {
        [XmlAttribute]
        public string name;

        [XmlAttribute]
        public byte bit;
    }

    //ѡ���Ͳ���
    public class ParamOption
    {
        [XmlAttribute]
        public string display;

        [XmlAttribute]
        public int value;
    }

    //���ò�����һ��
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

        /*public object param_value;*/              //�����ԣ���Ϊ����ʹ�ã��ն��豸����Ҫ���塣
    }

    //���ò�������
    public class Category
    {
        [XmlAttribute]
        public string name;         //�������

        [XmlAttribute]
        public byte refresh;        //0:��ˢ��; 1:�ֶ�ˢ��; 2:�Զ�ˢ��

        public List<ParamDefineItem> list;
    }

    //���ƺ��е��ַ�
    public class PlateChar
    {
        [XmlAttribute]
        public string name;         //ʡ�����ƣ��磺����

        [XmlAttribute]
        public string display;      //����������ʾ���磺��

        [XmlAttribute]
        public string value;        //��ʡ��Ӧ���������룬�磺5000
    }

    //�����ļ�
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
