using System;
using System.Collections.Generic;
using System.Text;

namespace Monitor
{
    class CalibInfo
    {
        private byte addr;
        private int weight;
        public CalibInfo(byte _addr, int _weight)
        {
            addr = _addr;
            weight = _weight;
        }

        public byte Addr
        {
            get { return addr; }
            set { addr = value; }
        }

        public int Weight
        {
            get { return weight; }
            set { weight = value; }
        }
    }
}
