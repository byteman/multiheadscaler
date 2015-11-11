using System;
using System.Collections.Generic;
using System.Text;

namespace Monitor
{
    public class ParamItem
    {
        public string name;
        public string unit;
        public byte op_write;
        public byte dev_id;
        public byte param_id;
        public byte param_len;
        public byte param_valid;
        public TypeCode param_type;
        public byte param_ret;
        public object param_value;

        public byte permit_read;
        public byte permit_write;
        public bool valid_min_max;
        public int min;
        public int max;
        public string str;

    }
}
