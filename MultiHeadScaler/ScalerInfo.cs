using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Monitor
{
    
  
    public class ScalerInfo
    {
        public ScalerInfo()
        {
            weight = new List<float>();
            status = new List<int>();
            for (int i = 0; i < 10; i++)
            {
                weight.Add(0);
                status.Add(0);
            }

           
        }
        bool updateWeight(int index, int w)
        {
            return true;
        }
        bool updateWeightList(int index, byte[] wl)
        {
            return true;
        }
        bool updateStatusList(int index, byte[] sl)
        {
            return true;
        }
        public bool updateWeightObj(object wl)
        {
            byte[] arr = (byte[])wl;
            byte[] tmp = new byte[4];
            for (int i = 0; i < 10; i++)
            {
                tmp[0] = arr[i * 4 + 3];
                tmp[1] = arr[i * 4 + 2];
                tmp[2] = arr[i * 4 + 1];
                tmp[3] = arr[i * 4 + 0];

                weight[i] = BitConverter.ToSingle(tmp, 0);
            }
            return true;
        }
        public bool updateStatusObj(object sl)
        {
            byte[] arr = (byte[])sl;
            for (int i = 0; i < 10; i++)
            {
                status[i] = arr[i];
            }
            return true;
        }
        bool updateStatus(int index, byte s)
        {
            return true;
        }
        bool update(int index, int w, byte s)
        {
            updateWeight(index, w);
            updateStatus(index, s);
            return true;
        }

        public string getWeightString(int index)
        {
            if ((index < 0) || index >= 10) return "";
            string str = weight[index].ToString("0.0");
            return str;
        }
        public string getStatusString(int index)
        {
            int sta = status[index];

            return "R";
        }
        public int getStatus(int index)
        {

            if ((index < 0) || index >= 10) return 0; 
            return status[index];
        }
        public Color getStatusColor(int index)
        {
            index = getStatus(index);
            if (index == 1) return Color.Green;
            if (index == 2) return Color.Red;
            if (index >= 3 && index <= 7) return Color.Black;
            if (index >= 8 && index <= 10) return Color.Blue;

            return Color.Gray;

         
        }
        private List<float> weight;
        private List<int> status;
    }
}
