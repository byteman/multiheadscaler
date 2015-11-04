using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Monitor
{
    interface InputInterface
    {
        void SetValue(ParamItem item, bool bVisible);
        ParamItem GetValue();
        bool GetAck();
        void ShowDialog();
        void Dispose();
    }
}
