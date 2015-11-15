using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Monitor
{
    public partial class FormDateTime : Form, InputInterface
    {
        private FormFrame formFrame = null;
        UCButtons ucButtons = null;
        ParamItem paramItem = null;
        private bool bAck;
        public FormDateTime(FormFrame f)
        {
            InitializeComponent();
            formFrame = f;
            ucButtons = new UCButtons(f, this.pnRight);
            ucButtons.SetAckVisible(true);
            ucButtons.RegisterBtnEvent(ClickUp, ClickDown, ClickAck, ClickReturn,null);
            this.pnRight.Controls.Add(ucButtons);

            this.dateTimePicker.Format = DateTimePickerFormat.Custom;
            this.dateTimePicker.CustomFormat = "yyyyƒÍMM‘¬dd»’";
            for (int i = 0; i < 24; i++ )
            {
                cbHour.Items.Add(i);
            }
            for (int i = 0; i < 60; i++)
            {
                cbMinute.Items.Add(i);
                cbSecond.Items.Add(i);
            }
            cbHour.Text = "0";
            cbMinute.Text = "0";
            cbSecond.Text = "0";
        }

        public void SetValue(ParamItem item, bool bVisible)
        {
            bAck = false;
            if (item.param_value != null)
            {
                DateTime dt = (DateTime)item.param_value;
                dateTimePicker.Value = dt;
                cbHour.Text = Convert.ToString(dt.Hour);
                cbMinute.Text = Convert.ToString(dt.Minute);
                cbSecond.Text = Convert.ToString(dt.Second);
            }
            paramItem = item;
        }

        public ParamItem GetValue() 
        {
            DateTime dt = dateTimePicker.Value;
            paramItem.param_value = new DateTime(dt.Year, dt.Month, dt.Day, Convert.ToInt32(cbHour.Text), Convert.ToInt32(cbMinute.Text), Convert.ToInt32(cbSecond.Text));
            dateTimePicker.Dispose();
            return paramItem;
        }

        public bool GetAck()
        {
            return bAck;
        }

        public new void ShowDialog()
        {
            base.ShowDialog();
        }

        public new void Dispose()
        {
            base.Dispose();
        }

        private void ClickUp()
        {

        }

        private void ClickDown()
        {
            
        }

        private void ClickAck()
        {
            bAck = true;
            paramItem.param_value = Convert.ToDateTime(dateTimePicker.Text);
            this.Close();
        }
            
        private void ClickReturn()
        {
            this.Close();
        }

        private void pnLeft_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(Color.Blue), 0, 0, pnLeft.Width - 1, pnLeft.Height - 1);
        }
    }
}