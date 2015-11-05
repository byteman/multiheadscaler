//#define WINCE

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Globalization;
using System.Drawing;
using System.Windows.Forms;

namespace Monitor
{
    class PInvoke
    {
        [StructLayout(LayoutKind.Sequential)]
        public class SECURITY_ATTRIBUTES
        { 
            public int nLength;
            public int lpSecurityDescriptor;
            public int bInheritHandle;
        }

        public struct MEMORYSTATUS
        {
            public int dwLength;
            public int dwMemoryLoad;
            public int dwTotalPhys;
            public int dwAvailPhys;
            public int dwTotalPageFile;
            public int dwAvailPageFile;
            public int dwTotalVirtual;
            public int dwAvailVirtual;
        };

#if WINCE
        [DllImport("coredll.Dll")]
        private static extern int GetLastError();
        
        [DllImport("coredll.Dll")]
        private static extern int ReleaseMutex(IntPtr hMutex);
        
        [DllImport("coredll.Dll")]
        private static extern IntPtr CreateMutex(SECURITY_ATTRIBUTES lpMutexAttributes, bool bInitialOwner, string lpName);

        [DllImport("coredll.Dll")]
        private static extern UInt32 GetTickCount();

        [DllImport("coredll.dll", SetLastError = true)]
        private static extern void SetWindowLong(IntPtr hWnd, int GetWindowLongParam, uint nValue);

        [DllImport("coredll.dll", SetLastError = true)]
        private static extern uint GetWindowLong(IntPtr hWnd, int nItem);

        [DllImport("coredll.dll")]
        private static extern IntPtr GetCapture();

        [DllImport("coredll.Dll")]
        public static extern bool GetSystemMemoryDivision(out Int32 lpdwStorePages, out Int32 lpdwRamPages, out Int32 lpdwPageSize);

        [DllImport("coredll.dll", EntryPoint = "KernelIoControl", SetLastError = true)]
        private extern static bool KernelIoControl(Int32 IoControlCode, IntPtr InputBuffer, Int32 InputBufferSize, byte[] OutputBuffer, Int32 OutputBufferSize, ref Int32 BytesReturned);

        [DllImport("coredll.dll")]
        public static extern void GlobalMemoryStatus(ref MEMORYSTATUS lpBuffer);


        public const int SPI_SETWORKAREA = 47;
        public const int SPI_GETWORKAREA = 48;
        public const int SW_HIDE = 0x00;
        public const int SW_SHOW = 0x0001;
        public const int SPIF_UPDATEINIFILE = 0x01;
        [DllImport("coredll.dll", EntryPoint = "FindWindow")]
        private static extern IntPtr FindWindow(string lpWindowName, string lpClassName);
        [DllImport("coredll.dll", EntryPoint = "ShowWindow")]
        private static extern bool ShowWindow(IntPtr hwnd, int nCmdShow);
        [DllImport("coredll.dll", EntryPoint = "SystemParametersInfo")]
        private static extern int SystemParametersInfo(int uAction, int uParam, ref Rectangle lpvParam, int fuWinIni);
          
        //#endregion

        public static bool SetFullScreen(bool fullscreen, ref Rectangle rectOld)
        {
            IntPtr Hwnd = FindWindow("HHTaskBar", null);
            if (Hwnd == IntPtr.Zero) return false;
            if (fullscreen)
            {
                ShowWindow(Hwnd, SW_HIDE);
                Rectangle rectFull = Screen.PrimaryScreen.Bounds;
                SystemParametersInfo(SPI_GETWORKAREA, 0, ref rectOld, SPIF_UPDATEINIFILE);//get
                SystemParametersInfo(SPI_SETWORKAREA, 0, ref rectFull, SPIF_UPDATEINIFILE);//set
            }
            else
            {
                ShowWindow(Hwnd, SW_SHOW);
                SystemParametersInfo(SPI_SETWORKAREA, 0, ref rectOld, SPIF_UPDATEINIFILE);
            }
            return true;
   
        }
        /*
        public static bool PShowTaskBar(bool show)
        {
            IntPtr trayHwnd = FindWindowEx(IntPtr.Zero, IntPtr.Zero, "Shell_TrayWnd", null);
            IntPtr hStar = FindWindowEx(IntPtr.Zero, IntPtr.Zero, "Button", null);
            if (trayHwnd != IntPtr.Zero && hStar != IntPtr.Zero)
            {
                //ShowWindow(FindWindow("progman", null), 0);
                if (!show)
                {
                    ShowWindow(trayHwnd, SW_HIDE);
                    ShowWindow(hStar, SW_HIDE);
                }
                else
                {
                    ShowWindow(trayHwnd, SW_SHOW);
                    ShowWindow(hStar, SW_SHOW);
                
                }
                
            }
            return show;
        }*/
        public static int PGetLastError()
        {
            return GetLastError();
        }

        public static int PReleaseMutex(IntPtr hMutex)
        {
            return ReleaseMutex(hMutex);
        }

        public static IntPtr PCreateMutex(SECURITY_ATTRIBUTES lpMutexAttributes, bool bInitialOwner, string lpName)
        { 
            return CreateMutex(lpMutexAttributes, bInitialOwner, lpName);
        }

        public static UInt32 PGetTickCount()
        {
            return GetTickCount();
        }

        public static void PSetWindowLong(IntPtr hWnd, int GetWindowLongParam, uint nValue)
        {
            SetWindowLong(hWnd, GetWindowLongParam, nValue);
        }

        public static uint PGetWindowLong(IntPtr hWnd, int nItem)
        {
            return GetWindowLong(hWnd, nItem);
        }

        public static IntPtr PGetCapture()
        {
            return GetCapture();
        }

        public static bool PGetSystemMemoryDivision(out Int32 lpdwStorePages, out Int32 lpdwRamPages, out Int32 lpdwPageSize)
        { 
            return GetSystemMemoryDivision(out lpdwStorePages, out lpdwRamPages, out lpdwPageSize);
        }

        public static void GetMemoryStatus(out Int32 dwAvailPhys, out Int32 dwTotalPhys)
        {
            MEMORYSTATUS ms = new MEMORYSTATUS();
            ms.dwLength = Marshal.SizeOf(ms);
            GlobalMemoryStatus(ref ms);
            dwAvailPhys = ms.dwAvailPhys;
            dwTotalPhys = ms.dwTotalPhys;
        }

        #region Serial Number
        private static Int32 FILE_DEVICE_HAL = 0x00000101;
        private static Int32 IOCTL_HAL_GET_DEVICEID =((FILE_DEVICE_HAL) << 16) | ((0x0) << 14) | ((21) << 2) | (0x0);
        public static string GetPDASerialNumber()
        {
            byte[] outputBuffer = new byte[256];
            Int32 outputBufferSize = outputBuffer.Length;
            Int32 bytesReturned = 0;
            bool retVal = KernelIoControl(IOCTL_HAL_GET_DEVICEID, IntPtr.Zero, 0, outputBuffer, outputBufferSize, ref bytesReturned);
            // If the request failed, exit the method now
            if (retVal == false)
            {
                return String.Empty;
            }
            Int32 presetIdOffset = BitConverter.ToInt32(outputBuffer, 4);
            Int32 platformIdOffset = BitConverter.ToInt32(outputBuffer, 0xc);
            Int32 platformIdSize = BitConverter.ToInt32(outputBuffer, 0x10);
            StringBuilder sb = new StringBuilder();
            sb.Append(String.Format(CultureInfo.InvariantCulture, "{0:X8}-{1:X4}-{2:X4}-{3:X4}-",
                        BitConverter.ToInt32(outputBuffer, presetIdOffset),
                        BitConverter.ToInt16(outputBuffer, presetIdOffset + 4),
                        BitConverter.ToInt16(outputBuffer, presetIdOffset + 6),
                        BitConverter.ToInt16(outputBuffer, presetIdOffset + 8)));
            for (int i = platformIdOffset; i < platformIdOffset + platformIdSize; i++)
            {
                sb.Append(String.Format(CultureInfo.InvariantCulture, "{0:X2}", outputBuffer[i]));
            }
            // return the device id string
            return sb.ToString();
        }
        #endregion

        #region Device ID
        [DllImport("coredll.dll")]
        private extern static int GetDeviceUniqueID([In, Out] byte[] appdata,
                                                    int cbApplictionData,
                                                    int dwDeviceIDVersion,
                                                    [In, Out] byte[] deviceIDOuput,
                                                    out uint pcbDeviceIDOutput);
        public static byte[] GetDeviceID(string AppString)
        {
            // Call the GetDeviceUniqueID
            byte[] AppData = new byte[AppString.Length];
            for (int count = 0; count < AppString.Length; count++)
                AppData[count] = (byte)AppString[count];
            int appDataSize = AppData.Length;
            byte[] DeviceOutput = new byte[20];
            uint SizeOut = 20;
            GetDeviceUniqueID(AppData, appDataSize, 1, DeviceOutput, out SizeOut);
            return DeviceOutput;
        }
        #endregion
#else
        private static UInt32 SysRunTick = 0;

        public static int PGetLastError()
        {
            return 0;
        }

        public static int PReleaseMutex(IntPtr hMutex)
        {
            return 0;
        }

        public static IntPtr PCreateMutex(SECURITY_ATTRIBUTES lpMutexAttributes, bool bInitialOwner, string lpName)
        {
            return IntPtr.Zero;
        }

        public static UInt32 PGetTickCount()
        {
            return (SysRunTick += 100);
        }

        public static void PSetWindowLong(IntPtr hWnd, int GetWindowLongParam, uint nValue)
        {
        }

        public static uint PGetWindowLong(IntPtr hWnd, int nItem)
        {
            return 0;
        }

        public static IntPtr PGetCapture()
        {
            return IntPtr.Zero;
        }

        public static bool PGetSystemMemoryDivision(out Int32 lpdwStorePages, out Int32 lpdwRamPages, out Int32 lpdwPageSize)
        {
            lpdwStorePages = lpdwRamPages = lpdwPageSize = 0;
            return true;
        }
#endif
    }
}
