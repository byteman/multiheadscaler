using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace Monitor
{
    public class Protocol
    {
        private const byte MaxItem = 32;        //每包最多修改16个参数
        private const byte MaxBuffer = 254;     //缓冲区大小

        //这些参数ID需要特殊处理
        public const byte ParamIdFixAddr = 14;
        public const byte ParamIdPlate = 24;
        public const byte ParamIdSIM = 25;
        public const byte ParamIdSoftVer = 26;
        public const byte ParamIdHardVer = 27;
        public const byte ParamIdCpuID = 28;
        public const byte ParamIdArithMode = 37;
        public const byte ParamIdSensorID = 35;
        public const byte ParamIdDateTime = 39;
        public const byte ParamIdCtrlAdmin = 42;

        public bool bToUserControl = true;                      //接收的数据是否分发给用户控件
        ConfigManage configManage;

        public bool bSuccess = false;
        public int nCtrlPwd;
        public const byte DevIdCtrl = 128;

        private object objLockReSend = new object();
        private ProtocolPackage ResendPack = null;              //需要重发的协议包
        Thread ThreadReSend = null;                             //重发线程（处理无线容易掉包）
        bool bReSend = true;

        public Protocol()
        {
            List<ProtocolPackage.ProtoParamCmd> ListCmd = new List<ProtocolPackage.ProtoParamCmd>();
            ResendPack = new ProtocolPackage();
            ResendPack.ResendCount = 0;
            ResendPack.SendTick = 0;
            ResendPack.Buffer = new byte[MaxBuffer];
            ResendPack.BufferLen = 0;
            ResendPack.ListCmd = ListCmd;

            ThreadReSend = new Thread(new ThreadStart(ThreadReSendProc));
            //ThreadReSend.Start();
        }

        public void Dispose()
        {
            if (ThreadReSend != null)
            {
                bReSend = false;
                Thread.Sleep(30);
                ThreadReSend.Abort();
            }
        }

        public void InitParam(ConfigManage cm)
        {
            configManage = cm;
        }

        public void ThreadReSendProc()
        {
            //while (bReSend) { Thread.Sleep(10); }
            int interval;
            SerialOperate Serial = SerialOperate.instance;
            while (bReSend)
            {
                if (ResendPack.ResendCount > 0)
                {
                    interval = (int)PInvoke.PGetTickCount() - (int)ResendPack.SendTick;
                    if (interval > 1)  //重发
                    {
                        Serial.Send(ResendPack.Buffer, ResendPack.BufferLen);
                        ResendPack.ResendCount--;
                    }
                    else
                    {
                        //System.Diagnostics.Debug.WriteLine("send recv interval:" + interval.ToString());
                    }
                }
                Thread.Sleep(10);
            }
        }

        public void AddResendPack(byte SlaveAddr, byte[] Buffer, int BufferLen, List<ParamItem> itemList)
        {
            lock (objLockReSend)
            {
                if((itemList.Count == 1) && (itemList[0].dev_id == configManage.cfg.paramDeviceId.Ctrl) && (itemList[0].param_id == configManage.cfg.paramFormWeight.Status.Id))
                {
                    return;     //查询控制器状态太频繁，不进入重发机制
                }

                ResendPack.ResendCount = 1;// configManage.cfg.paramSerial.ResendCount;
                ResendPack.SlaveAddr = SlaveAddr;
                ResendPack.ParamCount = (byte)itemList.Count;
                ResendPack.ListCmd.Clear();
                for (int i = 0; i < itemList.Count; i++)
                {
                    ProtocolPackage.ProtoParamCmd cmd = new ProtocolPackage.ProtoParamCmd();
                    cmd.dev_id = itemList[i].dev_id;
                    cmd.param_id = itemList[i].param_id;
                    ResendPack.ListCmd.Add(cmd);
                }
                ResendPack.SendTick = PInvoke.PGetTickCount();
                Buffer.CopyTo(ResendPack.Buffer, 0);
                ResendPack.BufferLen = BufferLen;
            }
        }

        public void DelResendPack(byte SlaveAddr, bool bValidParam, List<ParamItem> itemList)
        {
            lock (objLockReSend)
            {
                if (!bValidParam)      //如果返回的参数无效
                {
                    ResendPack.ResendCount = 0;
                }
                else
                {
                    bool bRecvSuccess = true;
                    if (SlaveAddr != ResendPack.SlaveAddr)
                    {
                        bRecvSuccess = false;
                    }
                    else
                    {
                        for (int i = 0; i < itemList.Count; i++)
                        {
                            if ((itemList[i].dev_id != ResendPack.ListCmd[i].dev_id) || (itemList[i].param_id != ResendPack.ListCmd[i].param_id))
                            {
                                bRecvSuccess = false;
                                break;
                            }
                        }
                    }

                    if (bRecvSuccess)
                    {
                        ResendPack.ResendCount = 0;
                    }
                }
            }
        }
      
        public int Produce(byte slaveAddr, out byte[] buffer, List<ParamItem> itemList)
        {
            byte param_count = (byte)itemList.Count;
            if (param_count == 0)
            {
                buffer = null;
                return 0;
            }
            if (param_count > MaxItem) param_count = MaxItem;

            int count = 0;
            buffer = new byte[MaxBuffer];
            buffer[count] = configManage.cfg.paramProtocol.M2S; //M2S;
            count++;

            //buffer[count] = configManage.cfg.paramDeviceId.Ctrl; //Addr_Ctrl;
            buffer[count] = slaveAddr;
            count++;

            buffer[count] = configManage.cfg.paramProtocol.REQ;// REQ;
            count++;

            buffer[count] = itemList[0].op_write;
            count++;

            buffer[count] = param_count;
            count++;

            for (int i = 0; i < param_count; i++)
            {
                buffer[count] = itemList[i].dev_id;
                count++;
                buffer[count] = itemList[i].param_id;
                count++;

                if (itemList[i].op_write == 0x01)
                {
                    buffer[count] = itemList[i].param_len;
                    count++;
                    byte[] bufValue;
                    switch (itemList[i].param_type)
                    {
                        case TypeCode.Empty:
                            break;

                        case TypeCode.Byte:
                            bufValue = new byte[1];

                            bufValue[0] = Convert.ToByte(itemList[i].param_value);// BitConverter.GetBytes();
                            
                            //bufValue[0] = (byte)itemList[i].param_value;
                            Array.Copy(bufValue, 0, buffer, count, 1);
                            count += 1;
                            break;

                        case TypeCode.UInt16:
                            bufValue = BitConverter.GetBytes(Convert.ToUInt16(itemList[i].param_value));
                            Util.SwapBuf(bufValue);
                            Array.Copy(bufValue, 0, buffer, count, 2);
                            count += 2;
                            break;

                        case TypeCode.Int32:
                            bufValue = BitConverter.GetBytes(Convert.ToInt32(itemList[i].param_value));
                            Util.SwapBuf(bufValue);
                            Array.Copy(bufValue, 0, buffer, count, 4);
                            count += 4;
                            break;

                        case TypeCode.UInt32:
                            bufValue = BitConverter.GetBytes(Convert.ToUInt32(itemList[i].param_value));
                            Util.SwapBuf(bufValue);
                            Array.Copy(bufValue, 0, buffer, count, 4);
                            count += 4;
                            break;

                        case TypeCode.Single:
                            bufValue = BitConverter.GetBytes(Convert.ToSingle(itemList[i].param_value));
                            Util.SwapBuf(bufValue);
                            Array.Copy(bufValue, 0, buffer, count, 4);
                            count += 4;
                            break;

                        case TypeCode.DateTime:
                            bufValue = new byte[7];
                            DateTime dt = (DateTime)itemList[i].param_value;
                            byte[] bufYear = BitConverter.GetBytes((ushort)dt.Year);
                            if (bufYear.Length == 2)
                            {
                                bufValue[0] = bufYear[1];
                                bufValue[1] = bufYear[0];
                            }
                            bufValue[2] = (byte)dt.Month;
                            bufValue[3] = (byte)dt.Day;
                            bufValue[4] = (byte)dt.Hour;
                            bufValue[5] = (byte)dt.Minute;
                            bufValue[6] = (byte)dt.Second;
                            Array.Copy(bufValue, 0, buffer, count, 7);
                            count += 7;
                            break;

                        case TypeCode.String:

                            bufValue = System.Text.Encoding.Default.GetBytes(itemList[i].param_value.ToString());
                             
                            Array.Copy(bufValue, 0, buffer, count, bufValue.Length);
                            count += bufValue.Length;
                            break;

                        default:
                            count += itemList[i].param_len;
                            break;
                    }
                }
            }
            ushort uCrc = Util.Crc16(buffer, (ushort)count);
            byte[] byCrc = BitConverter.GetBytes(uCrc);
            Util.SwapBuf(byCrc);
            
            Array.Copy(byCrc, 0, buffer, count, byCrc.Length);
            count += byCrc.Length;

            //AddResendPack(slaveAddr, buffer, count, itemList);
            return count;
        }

        public int Resolve(byte[] buffer, int len, out List<ParamItem> itemList)
        {
            itemList = new List<ParamItem>();
            if(len < 7) return -1;  //协议长度至少为7字节
            if (buffer[0] != configManage.cfg.paramProtocol.S2M) return -2; //只接收从->主的数据
            if ((buffer[1] != configManage.cfg.paramDeviceId.Ctrl) && (buffer[1] != configManage.cfg.paramDeviceId.MonitorWireless)) return -3;//只接收控制器、显示器无线模块传来的数据
            if (buffer[2] != configManage.cfg.paramProtocol.ACK) return -4;//只接收应答数据

            if (buffer[1] != configManage.cfg.paramDeviceId.MonitorWireless)//显示器无线模块不做CRC校验
            {
                if (Util.Crc16(buffer, (ushort)len) != 0) return -5;//校验失败
            }

            byte write = buffer[3];//读命令、写命令判断
            byte count = buffer[4];//本包含多少组数
            if(count > MaxItem) count = MaxItem;

            byte index = 5; //协议偏移
            bool bValidParam = true;         //参数都有效
            for(byte i = 0; i<count; i++)
            {
                ParamItem item = new ParamItem();
                item.op_write = write;
                item.dev_id = buffer[index++];
                item.param_id = buffer[index++];
                item.param_len = buffer[index++];
                item.param_valid = buffer[index++];
                if (item.param_len > 0)
                {
                    byte[] bufValue = new byte[item.param_len];
                    Array.Copy(buffer, index, bufValue, 0, item.param_len);
                    index += item.param_len;
                    item.param_type = GetTypeById(item.dev_id, item.param_id);
                    switch (item.param_type)
                    {
                        case TypeCode.Byte:
                            if (write == 0)
                            {
                                item.param_value = bufValue[0];
                            }
                            break;
                        case TypeCode.UInt16:
                            if (write == 0)
                            {
                                Util.SwapBuf(bufValue);
                                item.param_value = BitConverter.ToUInt16(bufValue, 0);
                            }
                            break;
                        case TypeCode.Int32:
                            if (write == 0)
                            {
                                Util.SwapBuf(bufValue);
                                item.param_value = BitConverter.ToInt32(bufValue, 0);
                            }
                            break;
                        case TypeCode.UInt32:
                            if (write == 0)
                            {
                                Util.SwapBuf(bufValue);
                                item.param_value = BitConverter.ToUInt32(bufValue, 0);
                            }
                            break;
                        case TypeCode.Single:
                            if (write == 0)
                            {
                                Util.SwapBuf(bufValue);
                                item.param_value = BitConverter.ToSingle(bufValue, 0);
                            }
                            break;
                        case TypeCode.DateTime:
                            if ((write == 0) && (bufValue.Length == 7))   //时间的长度必须为7个字节
                            {
                                byte[] bufYear = new byte[2];
                                bufYear[0] = bufValue[1];
                                bufYear[1] = bufValue[0];
                                int Y = BitConverter.ToUInt16(bufYear, 0);
                                DateTime dt = new DateTime(Y, bufValue[2], bufValue[3], bufValue[4], bufValue[5], bufValue[6]);
                                item.param_value = dt;
                            }
                            break;
                        case TypeCode.String:
                            if (write == 0)
                            {
                                item.param_value = bufValue;
                            }
                            break;
                        default:
                            return -2;
                    }
                }
                if (item.param_valid != 0)
                {
                    itemList.Add(item);
                }
                else
                {
                    bValidParam = false;       //有无效参数
                }
            }
            //DelResendPack(buffer[1], bValidParam, itemList);
            return len;
        }

        private TypeCode GetTypeById(byte dev_id, byte param_id)
        {
            if (dev_id < configManage.cfg.paramDeviceId.AllSensor) dev_id = configManage.cfg.paramDeviceId.AllSensor;

            foreach (Category cate in configManage.cfg.categoryList)
            {
                foreach (ParamDefineItem param in cate.list)
                {
                    if ((param.dev_id == dev_id) && (param.param_id == param_id))
                    {
                        return param.param_type;
                    }
                }
            }

            if ( (dev_id == configManage.cfg.paramDeviceId.Ctrl) && (param_id == Protocol.ParamIdCtrlAdmin) )   //控制器密码为Int32没有添加到配置文件
            {
                return TypeCode.Int32;
            }
            return TypeCode.Empty;
        }

        public void SetReturnValue(List<ParamItem> itemList)
        {
            if (itemList.Count != 1) return;
            if (itemList[0].dev_id != DevIdCtrl) return;
            switch (itemList[0].param_id)
            {
                case Protocol.ParamIdCtrlAdmin:
                    if (itemList[0].op_write == 0)
                    {
                        if (itemList[0].param_len == 4)
                        {
                            bSuccess = true;
                            nCtrlPwd = (int)itemList[0].param_value;
                        }
                    }
                    else
                    {
                        if (itemList[0].param_valid == 1)
                        {
                            bSuccess = true;
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
