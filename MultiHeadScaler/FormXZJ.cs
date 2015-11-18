using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Monitor
{
    public partial class FormXZJ : Form
    {
        private FormFrame formFrame;
        private int formula_id;
        public FormXZJ(FormFrame f,int id)
        {
            InitializeComponent();
            formula_id = id;
            formFrame = f;
        }

        private void FormXZJ_Load(object sender, EventArgs e)
        {
            //load 
            DataTable dt = SQLiteDBHelper.listFormula(formula_id);
            if (dt.Rows.Count == 0)
            {
                return;
            }
            DataRow dr = dt.Rows[0];
            for (int i = 0; i < 10; i++)
            {
                

                ListViewItem item = new ListViewItem();
                item.SubItems[0].Text = (i+1).ToString();
                String tmp = dr["xzp_strength" + i.ToString()].ToString();

                item.SubItems.Add(tmp.Length == 0 ? "65" : tmp);

                tmp = dr["xzp_time" + i.ToString()].ToString();

                item.SubItems.Add(tmp.Length == 0 ? "40" : tmp);
                
                listView1.Items.Add(item);

            }

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }
    }
}