using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Monitor
{
    public partial class FormMsgBox : Form
    {
        public enum Buttons
        {
            OK = 1,
            OKCancel,
        }

        private static FormMsgBox _msgBox;
        private static DialogResult _buttonResult = new DialogResult();

        public FormMsgBox()
        {
            InitializeComponent();
            label1.Visible = true;
        }

        public void SetInfo(string str)
        {
            if(str.Length < 15)
            {
                panel1.Top = 50;
                panel1.Height = 60;
                if (str.Length < 8)
                {
                    panel1.Left = 45;
                    panel1.Width = 290;
                }
                else
                {
                    panel1.Left = 15;
                    panel1.Width = 320;
                }
            }
            else
            {
                panel1.Top = 20;
                panel1.Height = 100;
            }
            this.label1.Text = str;
            this.button1.Focus();
        }

        private void button1_Click(object sender, EventArgs e)  //确定
        {
            this.Close();
            _buttonResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)  //取消
        {
            this.Close();
        }

        private static void InitButtons(Buttons buttons)
        {
            if (buttons == Buttons.OK)
            {
                _msgBox.button2.Visible = false;

                _msgBox.button1.Left = (_msgBox.Width - _msgBox.button1.Width) / 2;
            }
        }

        public static DialogResult Show(string message, string title)
        {
            _msgBox = new FormMsgBox();
            _buttonResult = DialogResult.Cancel;
            _msgBox.Left = (800 - _msgBox.Width) / 2;
            _msgBox.Top = (480 - _msgBox.Height) / 2;
            _msgBox.Text = title;
            _msgBox.SetInfo(message);
            FormMsgBox.InitButtons(Buttons.OK);
            _msgBox.ShowDialog();
            return _buttonResult;
        }

        public static DialogResult Show(string message, string title, Buttons buttons)
        {
            _msgBox = new FormMsgBox();
            _buttonResult = DialogResult.Cancel;
            _msgBox.Left = (800 - _msgBox.Width) / 2;
            _msgBox.Top = (480 - _msgBox.Height) / 2;
            _msgBox.Text = title;
            _msgBox.SetInfo(message);
            FormMsgBox.InitButtons(buttons);
            _msgBox.ShowDialog();
            return _buttonResult;
        }
    }
}