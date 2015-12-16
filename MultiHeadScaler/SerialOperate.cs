using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
using System.Threading;

namespace Monitor
{
    class SerialOperate : IDisposable
    {
        public delegate void DataRecvEventDelegate(object sender, SerialDataReceivedEventArgs e);
        public delegate void LogEventDelegate(byte[] buffer, int len);
        public delegate void CloseEventDelegate();

        public static readonly SerialOperate instance = new SerialOperate();
        private SerialPort sp;
        private byte[] RecvBuf;
        private int RecvBufMax = 256;
        private int RecvBufLen = 0;
        private int ReadWaitMs = 150;

        private UInt32 SendInterval = 120;
        private UInt32 PreSendTick;
        private object objLockSend = new object();
        

        private bool Listening = false; //是否没有执行完invoke相关操作
        private bool Closing = false;   //是否正在关闭串口，执行Application.DoEvents，并阻止再次invoke
        DataRecvEventDelegate SEventRecv;
        private LogEventDelegate LogEventSend, LogEventRecv;
        CloseEventDelegate SEventClose = null;

        private SerialOperate()
        {
            
        }

        public void Init()
        {
            sp = new SerialPort();
            RecvBuf = new byte[RecvBufMax];
            sp.DataReceived += new SerialDataReceivedEventHandler(sp_DataReceived);
        }

        public void RegisterEvent(DataRecvEventDelegate _SEventRecv, CloseEventDelegate _SEventClose,
                                  LogEventDelegate _LogEventSend, LogEventDelegate _LogEventRecv)
        {
            SEventRecv = _SEventRecv;
            SEventClose = _SEventClose;
            LogEventSend = _LogEventSend;
            LogEventRecv = _LogEventRecv;
        }

        private void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (Closing)    return;     //如果正在关闭，忽略操作，直接返回，尽快的完成串口监听线程的一次循环  
            try
            {
                Listening = true;       //设置标记，说明我已经开始处理数据，一会儿要使用系统UI的
                ReadWaitMs = 50;
                Thread.Sleep(ReadWaitMs);
                if (sp.BytesToRead > 0)
                {
                    int size = (sp.BytesToRead > RecvBufMax) ? RecvBufMax : sp.BytesToRead;

                    RecvBufLen =  sp.Read(RecvBuf, 0, size/* RecvBufMax */);
                    if (RecvBufLen > 0)
                    {
                        if (SEventRecv != null) SEventRecv(sender, e);
                        if (LogEventRecv != null) LogEventRecv(RecvBuf, RecvBufLen);
                    }
                }
                else
                {
                    //System.Diagnostics.Debug.WriteLine("sp.BytesToRead=" + sp.BytesToRead.ToString());
                }
            }
            catch//(Exception ex)
            {
                //System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                Listening = false;      //使用完后UI可以关闭串口了
            }
        }

        //static int nFlag = 0;      //临时模拟串口丢包
        public int Recv(out byte[] buf)
        {
            //临时模拟串口丢包---------------------------------
            //nFlag++;
            //if ( nFlag >= 3)
            //{
            //    nFlag = 0;
            //    RecvBufLen = 0;
            //}
            //临时模拟串口丢包---------------------------------

            buf = RecvBuf;
            return RecvBufLen;
        }

        public int Open(string PortName, int BaudRate, int _ReadWaitMs)
        {
            ReadWaitMs = _ReadWaitMs;
            sp.PortName = PortName;
            sp.BaudRate = BaudRate;
            sp.DataBits = 8;
            sp.Parity = Parity.None;
            sp.StopBits = StopBits.One;
            sp.ReadTimeout = 200;
            sp.ReadBufferSize = 256;
            try
            {
                if (!sp.IsOpen)
                {
                    sp.Open();
                    return 1;
                }
                return 0;
            }
            catch(Exception)
            {
                return -1;
            }
        }

        public int Send(byte[] buffer, int length)
        {
            if (!sp.IsOpen)
            {
                return -1;
            }
            else
            {
                lock (objLockSend)
                {
                    sp.Write(buffer, 0, length);
                    //#if WINCE
                    //while ((PInvoke.PGetTickCount() - PreSendTick) < SendInterval)
                    //{
                    //    Thread.Sleep(10);
                    //}
                    //PreSendTick = PInvoke.PGetTickCount();
                    //System.Diagnostics.Debug.WriteLine(PreSendTick);
                    //#endif

                    //sp.Write(buffer, 0, length);
                    //if (LogEventSend != null) LogEventSend(buffer, length);
                }
                return length;
            }
        }

        public void Close()
        {
            if (sp.IsOpen)
            {
                Closing = true;
                while (Listening)
                {
                    if (SEventClose != null) SEventClose();
                }
                sp.Close();
                Closing = false;
            }
        }

        public void Dispose()
        {
            Close();
            sp.Dispose();
        }
    }
}
