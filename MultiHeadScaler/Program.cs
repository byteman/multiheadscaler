using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace Monitor
{
    static class Program
    {
        private const int ERROR_ALREADY_EXISTS = 0183;

        /// <summary>
        /// 应用程序的主入口点
        /// </summary>
        [MTAThread]
        static void Main()
        {
            IntPtr hMutex = PInvoke.PCreateMutex(null, false, "CZ100A5Monitor");
            if (PInvoke.PGetLastError() != ERROR_ALREADY_EXISTS)
            {
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
                //Application.Run(new FormInit());
                Application.Run(new FormFrame());
            }
            else
            {
                PInvoke.PReleaseMutex(hMutex);
            }
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            string strException = string.Format("{0}发生系统异常。\r\n{1}\r\n", DateTime.Now, e.ExceptionObject.ToString());
            StreamWriter sWriter = new StreamWriter(Path.GetDirectoryName(Assembly.GetExecutingAssembly().ManifestModule.FullyQualifiedName) + @"\exception.log");
            sWriter.WriteLine(strException);
            sWriter.Close();
        }
    }  
}