using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Monitor
{
    public class WeightData
    {
        public WeightData()
        { 
            headers = new List<int>();
        }
        WeightData(double w, double d, List<int> h)
        {
            weight = w;
            diff = d;
            headers = h;
        }
        public string getZuheString()
        {
            StringBuilder str = new StringBuilder();
            
            foreach (int i in headers)
            {
                str.Append(i.ToString() + ",");
            }
            if(str.Length > 0)
                str.Remove(str.Length-1, 1);
            return str.ToString();
        }
        public double weight {get;set;}
        public double diff {get;set;}
        public List<int> headers { get; set; }
    };
}
