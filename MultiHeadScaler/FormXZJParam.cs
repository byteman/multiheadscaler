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
    public partial class FormXZJParam : Form
    {
        static private FormXZJParam _msgBox=null;
        private static DialogResult _buttonResult = new DialogResult();
        static public string strength;
        static public string time;
        private FormFrame formFrame = null;
        public FormXZJParam(FormFrame f)
        {
            formFrame = f;
            InitializeComponent();
        }
        public static DialogResult Show(FormFrame f,string _str, string _time)
        {
            _msgBox = new FormXZJParam(f);
            _msgBox.button1.Focus();
            _msgBox.textBox1.Text = _str;
            _msgBox.textBox2.Text = _time;

            _buttonResult = DialogResult.Cancel;
            _msgBox.Left = (800 - _msgBox.Width) / 2;
            _msgBox.Top = (600 - _msgBox.Height) / 2;

         
            _msgBox.ShowDialog();
            return _buttonResult;
        }

        private void textBox1_GotFocus(object sender, EventArgs e)
        {
            //
            button1.Focus();
            FormInput input = new FormInput(formFrame);
            ParamItem item = new ParamItem();
            item.param_value = byte.Parse(textBox1.Text);
            item.unit = "";
            item.param_type = TypeCode.Byte;
            item.name = "线振机强度";
            input.SetValue(item, true);
            input.ShowDialog();
            textBox1.Text = input.GetValue().param_value.ToString();

        }

        private void textBox2_GotFocus(object sender, EventArgs e)
        {
            button1.Focus();
            FormInput input = new FormInput(formFrame);
            ParamItem item = new ParamItem();
            item.param_value = byte.Parse(textBox2.Text);
            item.unit = "";
            item.param_type = TypeCode.Byte;
            item.name = "线振机时间";
            input.SetValue(item, true);
            input.ShowDialog();
            textBox2.Text = input.GetValue().param_value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            strength = textBox1.Text;
            time = textBox2.Text;
            this.Close();

        }
    }
}