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

        public int ResendCount;                 //重发次数
        public UInt32 SendTick;                 //发送时间(tick表示)
        public byte[] Buffer = null;            //重发缓冲区
        public int BufferLen;                   //重发缓冲区数据长度

        public byte SlaveAddr;
        public byte ParamCount;
        public List<ProtoParamCmd> ListCmd;
    }
}
