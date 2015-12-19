using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Monitor
{
    public partial class UCXZJ : UserControl
    {
        private FormFrame formFrame = null;
        int formula_id = 0;
        public UCXZJ(FormFrame f)
        {
            InitializeComponent();
            formFrame = f;
        }


        public void XZJ_Load(int _formula_id)
        {
            //load 
            formula_id = _formula_id;
            DataTable dt = SQLiteDBHelper.listFormula(formula_id);
            if (dt.Rows.Count == 0)
            {
                return;
            }
            listView1.Items.Clear();
            DataRow dr = dt.Rows[0];
            for (int i = 0; i < 10; i++)
            {


                ListViewItem item = new ListViewItem();
                item.SubItems[0].Text = (i + 1).ToString();
                String tmp = dr["xzp_strength" + i.ToString()].ToString();

                item.SubItems.Add(tmp.Length == 0 ? "65" : tmp);

                tmp = dr["xzp_time" + i.ToString()].ToString();

                item.SubItems.Add(tmp.Length == 0 ? "40" : tmp);

                listView1.Items.Add(item);

            }

        }
        private void start_download_driver_param(Dictionary<string, object> dic)
        {
            Protocol protocol = formFrame.protocol;
            SerialOperate Serial = SerialOperate.instance;
            List<ParamItem> itemList = new List<ParamItem>();
            int size = dic.Count / 2;
            for (byte i = 0; i < size; i++)
            {

                ParamItem item = new ParamItem();

                item.dev_id = (byte)(i + 1);
                item.op_write = 1;
                item.param_id = 6;
                string key = "xzp_strength" + i.ToString();
                UInt16 v = byte.Parse(dic[key].ToString());

                item.param_value = v;
                item.param_len = 2;
                item.param_type = TypeCode.UInt16;
                itemList.Add(item);

                item = new ParamItem();
                item.dev_id = (byte)(i + 1);
                item.op_write = 1;
                item.param_id = 7;
                key = "xzp_time" + i.ToString();
                v = byte.Parse(dic[key].ToString());
                item.param_type = TypeCode.UInt16;
                item.param_value = v;
                item.param_len = 2;
                itemList.Add(item);

            }
            byte[] buf;
            int len = protocol.Produce(formFrame.configManage.cfg.paramDeviceId.Ctrl, out buf, itemList);
            if (len > 0)
            {
                Serial.Send(buf, len);
            }

        }
  

        private void listView1_ItemActivate_1(object sender, EventArgs e)
        {
            if (listView1.FocusedItem != null)
            {
                FormXZJParam.Show(formFrame, listView1.FocusedItem.SubItems[1].Text, listView1.FocusedItem.SubItems[2].Text);

                listView1.FocusedItem.SubItems[1].Text = FormXZJParam.strength;
                listView1.FocusedItem.SubItems[2].Text = FormXZJParam.time;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView1.Items)
            {
                int s = int.Parse(item.SubItems[2].Text);
                if (s < 255)
                {
                    s++;
                    item.SubItems[2].Text = s.ToString();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView1.Items)
            {
                int s = int.Parse(item.SubItems[2].Text);
                if (s > 0)
                {
                    s--;
                    item.SubItems[2].Text = s.ToString();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();


            for (int i = 0; i < 10; i++)
            {
                dic["xzp_strength" + i.ToString()] = listView1.Items[i].SubItems[1].Text;
                dic["xzp_time" + i.ToString()] = listView1.Items[i].SubItems[2].Text;
            }
            Dictionary<string, object> cond = new Dictionary<string, object>();
            cond["formula_id"] = formula_id;
            SQLiteDBHelper.updateFormula(dic, cond);
            start_download_driver_param(dic);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            formFrame.ShowUC(formFrame.ucDBListControl);
        }

        internal void SetReturnValue(List<ParamItem> itemList)
        {
            foreach (ParamItem item in itemList)
            { 
                
            }
        }
    }
}
