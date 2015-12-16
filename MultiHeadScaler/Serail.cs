using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Text;
namespace NewPortTest
{
    public class CommPort
    {
        ///<summary>
        ///�˿�����(COM1,COM2...COM4...)COM2:
        ///</summary>
        public string Port = "COM1";
        ///<summary>
        ///������9600
        ///</summary>
        public int BaudRate = 115200;
        ///<summary>
        ///����λ4-8
        ///</summary>
        public byte ByteSize = 8; //4-8
        ///<summary>
        ///��żУ��0-4=no,odd,even,mark,space
        ///</summary>
        public byte Parity = 0;   //0-4=no,odd,even,mark,space
        ///<summary>
        ///ֹͣλ
        ///</summary>
        public byte StopBits = 0;   //0,1,2 = 1, 1.5, 2
        ///<summary>
        ///��ʱ��
        ///</summary>
        public int ReadTimeout = 200;
        ///<summary>
        ///�����Ƿ��Ѿ���
        ///</summary>
        public bool Opened = false;
        ///<summary>
        /// COM�ھ��
        ///</summary>
        private int hComm = -1;
        #region "API��ض���"
        private const string DLLPATH = "//windows//coredll.dll"; ////windows//coredll.dll "kernel32";
        ///<summary>
        /// WINAPI����,д��־
        ///</summary>
        private const uint GENERIC_READ = 0x80000000;
        ///<summary>
        /// WINAPI����,����־
        ///</summary>
        private const uint GENERIC_WRITE = 0x40000000;
        ///<summary>
        /// WINAPI����,���Ѵ���
        ///</summary>
        private const int OPEN_EXISTING = 3;
        ///<summary>
        /// WINAPI����,��Ч���
        ///</summary>
        private const int INVALID_HANDLE_VALUE = -1;
        private const int PURGE_RXABORT = 0x2;
        private const int PURGE_RXCLEAR = 0x8;
        private const int PURGE_TXABORT = 0x1;
        private const int PURGE_TXCLEAR = 0x4;
        ///<summary>
        ///�豸���ƿ�ṹ������
        ///</summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct DCB
        {
            ///<summary>
            /// DCB����
            ///</summary>
            public int DCBlength;
            ///<summary>
            ///ָ����ǰ������
            ///</summary>
            public int BaudRate;
            ///<summary>
            ///��־λ
            ///</summary>
            public uint flags;
            ///<summary>
            ///δʹ��,����Ϊ0
            ///</summary>
            public ushort wReserved;
            ///<summary>
            ///ָ����XON�ַ�������ǰ���ջ������п��������С�ֽ���
            ///</summary>
            public ushort XonLim;
            ///<summary>
            ///ָ����XOFF�ַ�������ǰ���ջ������п��������С�ֽ���
            ///</summary>
            public ushort XoffLim;
            ///<summary>
            ///ָ���˿ڵ�ǰʹ�õ�����λ
            ///</summary>
            public byte ByteSize;
            ///<summary>
            ///ָ���˿ڵ�ǰʹ�õ���żУ�鷽��,����Ϊ:EVENPARITY,MARKPARITY,NOPARITY,ODDPARITY 0-4=no,odd,even,mark,space
            ///</summary>
            public byte Parity;
            ///<summary>
            ///ָ���˿ڵ�ǰʹ�õ�ֹͣλ��,����Ϊ:ONESTOPBIT,ONE5STOPBITS,TWOSTOPBITS 0,1,2 = 1, 1.5, 2
            ///</summary>
            public byte StopBits;
            ///<summary>
            ///ָ�����ڷ��ͺͽ����ַ�XON��ֵ Tx and Rx XON character
            ///</summary>
            public byte XonChar;
            ///<summary>
            ///ָ�����ڷ��ͺͽ����ַ�XOFFֵ Tx and Rx XOFF character
            ///</summary>
            public byte XoffChar;
            ///<summary>
            ///���ַ�����������յ�����żУ�鷢������ʱ��ֵ
            ///</summary>
            public byte ErrorChar;
            ///<summary>
            ///��û��ʹ�ö�����ģʽʱ,���ַ�������ָʾ���ݵĽ���
            ///</summary>
            public byte EofChar;
            ///<summary>
            ///�����յ����ַ�ʱ,�����һ���¼�
            ///</summary>
            public byte EvtChar;
            ///<summary>
            ///δʹ��
            ///</summary>
            public ushort wReserved1;
        }
        ///<summary>
        ///���ڳ�ʱʱ��ṹ������
        ///</summary>
        [StructLayout(LayoutKind.Sequential)]
        private struct COMMTIMEOUTS
        {
            public int ReadIntervalTimeout;
            public int ReadTotalTimeoutMultiplier;
            public int ReadTotalTimeoutConstant;
            public int WriteTotalTimeoutMultiplier;
            public int WriteTotalTimeoutConstant;
        }
        ///<summary>
        ///����������ṹ������
        ///</summary>
        [StructLayout(LayoutKind.Sequential)]
        private struct OVERLAPPED
        {
            public int Internal;
            public int InternalHigh;
            public int Offset;
            public int OffsetHigh;
            public int hEvent;
        }
        ///<summary>
        ///�򿪴���
        ///</summary>
        ///<param name="lpFileName">Ҫ�򿪵Ĵ�������</param>
        ///<param name="dwDesiredAccess">ָ�����ڵķ��ʷ�ʽ��һ������Ϊ�ɶ���д��ʽ</param>
        ///<param name="dwShareMode">ָ�����ڵĹ���ģʽ�����ڲ��ܹ�����������Ϊ0</param>
        ///<param name="lpSecurityAttributes">���ô��ڵİ�ȫ���ԣ�WIN9X�²�֧�֣�Ӧ��ΪNULL</param>
        ///<param name="dwCreationDisposition">���ڴ���ͨ�ţ�������ʽֻ��ΪOPEN_EXISTING</param>
        ///<param name="dwFlagsAndAttributes">ָ�������������־������ΪFILE_FLAG_OVERLAPPED(�ص�I/O����)��ָ���������첽��ʽͨ��</param>
        ///<param name="hTemplateFile">���ڴ���ͨ�ű�������ΪNULL</param>
        [DllImport(DLLPATH)]
        private static extern int CreateFile(string lpFileName, uint dwDesiredAccess, int dwShareMode,
        int lpSecurityAttributes, int dwCreationDisposition, int dwFlagsAndAttributes, int hTemplateFile);
        ///<summary>
        ///�õ�����״̬
        ///</summary>
        ///<param name="hFile">ͨ���豸���</param>
        ///<param name="lpDCB">�豸���ƿ�DCB</param>
        [DllImport(DLLPATH)]
        private static extern bool GetCommState(int hFile, ref DCB lpDCB);
        ///<summary>
        ///���������豸���ƿ�(Ƕ���û��)
        ///</summary>
        ///<param name="lpDef">�豸�����ַ���</param>
        ///<param name="lpDCB">�豸���ƿ�</param>
        //[DllImport(DLLPATH)]
        //private static extern bool BuildCommDCB(string lpDef, ref DCB lpDCB);
        ///<summary>
        ///���ô���״̬
        ///</summary>
        ///<param name="hFile">ͨ���豸���</param>
        ///<param name="lpDCB">�豸���ƿ�</param>
        [DllImport(DLLPATH)]
        private static extern bool SetCommState(int hFile, ref DCB lpDCB);
        ///<summary>
        ///��ȡ���ڳ�ʱʱ��
        ///</summary>
        ///<param name="hFile">ͨ���豸���</param>
        ///<param name="lpCommTimeouts">��ʱʱ��</param>
        [DllImport(DLLPATH)]
        private static extern bool GetCommTimeouts(int hFile, ref COMMTIMEOUTS lpCommTimeouts);
        ///<summary>
        ///���ô��ڳ�ʱʱ��
        ///</summary>
        ///<param name="hFile">ͨ���豸���</param>
        ///<param name="lpCommTimeouts">��ʱʱ��</param>
        [DllImport(DLLPATH)]
        private static extern bool SetCommTimeouts(int hFile, ref COMMTIMEOUTS lpCommTimeouts);
        ///<summary>
        ///��ȡ��������
        ///</summary>
        ///<param name="hFile">ͨ���豸���</param>
        ///<param name="lpBuffer">���ݻ�����</param>
        ///<param name="nNumberOfBytesToRead">�����ֽڵȴ���ȡ</param>
        ///<param name="lpNumberOfBytesRead">��ȡ�����ֽ�</param>
        ///<param name="lpOverlapped">���������</param>
        [DllImport(DLLPATH)]
        private static extern bool ReadFile(int hFile, byte[] lpBuffer, int nNumberOfBytesToRead,
        ref int lpNumberOfBytesRead, ref OVERLAPPED lpOverlapped);
        ///<summary>
        ///д��������
        ///</summary>
        ///<param name="hFile">ͨ���豸���</param>
        ///<param name="lpBuffer">���ݻ�����</param>
        ///<param name="nNumberOfBytesToWrite">�����ֽڵȴ�д��</param>
        ///<param name="lpNumberOfBytesWritten">�Ѿ�д������ֽ�</param>
        ///<param name="lpOverlapped">���������</param>
        [DllImport(DLLPATH)]
        private static extern bool WriteFile(int hFile, byte[] lpBuffer, int nNumberOfBytesToWrite,
        ref int lpNumberOfBytesWritten, ref OVERLAPPED lpOverlapped);
        [DllImport(DLLPATH, SetLastError = true)]
        private static extern bool FlushFileBuffers(int hFile);
        [DllImport(DLLPATH, SetLastError = true)]
        private static extern bool PurgeComm(int hFile, uint dwFlags);
        ///<summary>
        ///�رմ���
        ///</summary>
        ///<param name="hObject">ͨ���豸���</param>
        [DllImport(DLLPATH)]
        private static extern bool CloseHandle(int hObject);
        ///<summary>
        ///�õ��������һ�η��صĴ���
        ///</summary>
        [DllImport(DLLPATH)]
        private static extern uint GetLastError();
        #endregion
        ///<summary>
        ///����DCB��־λ
        ///</summary>
        ///<param name="whichFlag"></param>
        ///<param name="setting"></param>
        ///<param name="dcb"></param>
        internal void SetDcbFlag(int whichFlag, int setting, DCB dcb)
        {
            uint num;
            setting = setting << whichFlag;
            if ((whichFlag == 4) || (whichFlag == 12))
            {
                num = 3;
            }
            else if (whichFlag == 15)
            {
                num = 0x1ffff;
            }
            else
            {
                num = 1;
            }
            dcb.flags &= ~(num << whichFlag);
            dcb.flags |= (uint)setting;
        }
        ///<summary>
        ///�����봮�ڵ�����
        ///</summary>
        public int Open()
        {
            DCB dcb = new DCB();
            COMMTIMEOUTS ctoCommPort = new COMMTIMEOUTS();
            // �򿪴���
            hComm = CreateFile(Port, GENERIC_READ | GENERIC_WRITE, 0, 0, OPEN_EXISTING, 0, 0);
            if (hComm == INVALID_HANDLE_VALUE)
            {
                return -1;
            }
            // ����ͨ�ų�ʱʱ��
            GetCommTimeouts(hComm, ref ctoCommPort);
            ctoCommPort.ReadTotalTimeoutConstant = ReadTimeout;
            ctoCommPort.ReadTotalTimeoutMultiplier = 0;
            ctoCommPort.WriteTotalTimeoutMultiplier = 0;
            ctoCommPort.WriteTotalTimeoutConstant = 0;
            SetCommTimeouts(hComm, ref ctoCommPort);
            //���ô��ڲ���
            GetCommState(hComm, ref dcb);
            dcb.DCBlength = Marshal.SizeOf(dcb);
            dcb.BaudRate = BaudRate;
            dcb.flags = 0;
            dcb.ByteSize = (byte)ByteSize;
            dcb.StopBits = StopBits;
            dcb.Parity = (byte)Parity;
            //------------------------------
            SetDcbFlag(0, 1, dcb);            //�����Ʒ�ʽ
            SetDcbFlag(1, (Parity == 0) ? 0 : 1, dcb);
            SetDcbFlag(2, 0, dcb);            //����CTS��ⷢ��������
            SetDcbFlag(3, 0, dcb);            //����DSR��ⷢ��������
            SetDcbFlag(4, 0, dcb);            //��ֹDTR��������
            SetDcbFlag(6, 0, dcb);            //��DTR�ź��߲�����
            SetDcbFlag(9, 1, dcb);            //�����ջ�����
            SetDcbFlag(8, 0, dcb);            //���������ַ�����
            SetDcbFlag(10, 0, dcb);           //�Ƿ���ָ���ַ��滻У�����ַ�
            SetDcbFlag(11, 0, dcb);           //����NULL�ַ�
            SetDcbFlag(12, 0, dcb);           //����RTS��������
            SetDcbFlag(14, 0, dcb);           //���ʹ���󣬼�����������Ķ�д����
            //--------------------------------
            dcb.wReserved = 0;                       //û��ʹ�ã�����Ϊ0      
            dcb.XonLim = 0;                          //ָ����XOFF�ַ�����֮ǰ���յ��������п��������С�ֽ���
            dcb.XoffLim = 0;                         //ָ����XOFF�ַ�����֮ǰ�������п��������С�����ֽ���
            dcb.XonChar = 0;                         //���ͺͽ��յ�XON�ַ�
            dcb.XoffChar = 0;                        //���ͺͽ��յ�XOFF�ַ�
            dcb.ErrorChar = 0;                       //������յ���żУ�������ַ�
            dcb.EofChar = 0;                         //������ʾ���ݵĽ���     
            dcb.EvtChar = 0;                         //�¼��ַ������յ����ַ�ʱ�������һ���¼�       
            dcb.wReserved1 = 0;                      //û��ʹ��
            if (!SetCommState(hComm, ref dcb))
            {
                return -2;
            }
            Opened = true;
            return 0;
        }
        ///<summary>
        ///�رմ���,����ͨѶ
        ///</summary>
        public void Close()
        {
            if (hComm != INVALID_HANDLE_VALUE)
            {
                CloseHandle(hComm);
            }
        }
        ///<summary>
        ///��ȡ���ڷ��ص�����
        ///</summary>
        ///<param name="NumBytes">���ݳ���</param>
        public int Read(ref byte[] bytData, int NumBytes)
        {
            if (hComm != INVALID_HANDLE_VALUE)
            {
                OVERLAPPED ovlCommPort = new OVERLAPPED();
                int BytesRead = 0;
                ReadFile(hComm, bytData, NumBytes, ref BytesRead, ref ovlCommPort);
                return BytesRead;
            }
            else
            {
                return -1;
            }
        }
        public byte[] Readport(int NumBytes)
        {
            byte[] BufBytes;
            byte[] OutBytes;
            BufBytes = new byte[NumBytes];
            if (hComm != INVALID_HANDLE_VALUE)
            {
                OVERLAPPED ovlCommPort = new OVERLAPPED();
                int BytesRead = 0;
                ReadFile(hComm, BufBytes, NumBytes, ref BytesRead, ref ovlCommPort);
                OutBytes = new byte[BytesRead];
                Array.Copy(BufBytes, OutBytes, BytesRead);
            }
            else
            {
                throw (new ApplicationException("����δ�򿪣�"));
            }
            return OutBytes;
        }
        ///<summary>
        ///�򴮿�д����
        ///</summary>
        ///<param name="WriteBytes">��������</param>
        public int Write(byte[] WriteBytes, int intSize)
        {
            if (hComm != INVALID_HANDLE_VALUE)
            {
                OVERLAPPED ovlCommPort = new OVERLAPPED();
                int BytesWritten = 0;
                WriteFile(hComm, WriteBytes, intSize, ref BytesWritten, ref ovlCommPort);
                return BytesWritten;
            }
            else
            {
                return -1;
            }
        }
        ///<summary>
        ///������ջ�����
        ///</summary>
        ///<returns></returns>
        public void ClearReceiveBuf()
        {
            if (hComm != INVALID_HANDLE_VALUE)
            {
                PurgeComm(hComm, PURGE_RXABORT | PURGE_RXCLEAR);
            }
        }
        ///<summary>
        ///������ͻ�����
        ///</summary>
        public void ClearSendBuf()
        {
            if (hComm != INVALID_HANDLE_VALUE)
            {
                PurgeComm(hComm, PURGE_TXABORT | PURGE_TXCLEAR);
            }
        }
    }
   
}
