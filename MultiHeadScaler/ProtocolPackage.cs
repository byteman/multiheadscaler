using System;
using System.Collections.Generic;
using System.Text;


namespace Monitor
{
    public class ProtocolPackage
    {
        public class ProtoParamCmd
        {
            public byte dev_id;
            public byte param_id;
        };

        public int ResendCount;                 //�ط�����
        public UInt32 SendTick;                 //����ʱ��(tick��ʾ)
        public byte[] Buffer = null;            //�ط�������
        public int BufferLen;                   //�ط����������ݳ���

        public byte SlaveAddr;
        public byte ParamCount;
        public List<ProtoParamCmd> ListCmd;
    }
}
