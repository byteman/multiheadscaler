using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Monitor
{
    
  
    public class ScalerInfo
    {
        public ScalerInfo()
        {
            weight = new List<int>();
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
            
            return true;
        }
        public bool updateStatusObj(object sl)
        {
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
            string str = weight[index].ToString();
            if (str.Length == 1)
            {
                str = "0." + str;
            }
            else
                str.Insert(str.Length - 1, ".");
            return str;
        }
        public string getStatusString(int index)
        {
            int sta = status[index];

            return "R";
        }
        private List<int> weight;
        private List<int> status;
    }
}
