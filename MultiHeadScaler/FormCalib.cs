using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Reflection;

namespace Monitor
{
    public partial class FormCalib : Form
    {
        FormFrame formFrame;
        private FormStyle formStyle;
        ArrayList alSource = new ArrayList();
        DataTable dtSource;
        const string cstrCol1 = "地址";
        const string cstrCol2 = "重量";
        const string cstrCol3 = "操作";
        const string cstrCalib = "标定";

        public FormCalib(FormFrame f)
        {
            InitializeComponent();
            formFrame = f;
            formStyle = new FormStyle();
            formStyle.SetStyle(f, this);
            formStyle.ucButtons.SetAckText(cstrCalib);
            Init();
        }

        public void Init()
        {
            this.dgCalib.Parent = formStyle.pnLeft;
            this.dgCalib.Dock = DockStyle.Fill;

            DataGridInit();
            SourceUpdate();
            DataBind();
        }

        private void SourceUpdate()
        {
            for (byte i = 0; i < 10; i++)
            {
                alSource.Add(new CalibInfo(i, i * 1000));
            }
        }

        private void DataGridInit()
        {
            DataTable dt = new DataTable();
            DataColumn dc1 = new DataColumn(cstrCol1);
            dc1.ReadOnly = true;
            dt.Columns.Add(dc1);

            DataColumn dc2 = new DataColumn(cstrCol2);
            dc2.ReadOnly = true;
            dt.Columns.Add(dc2);

            DataColumn dc3 = new DataColumn(cstrCol3);
            dc3.ReadOnly = true;
            dt.Columns.Add(dc3);

            #region 设置行高
            FieldInfo fi = dgCalib.GetType().GetField(  "m_cyRow",
                                                        BindingFlags.NonPublic |
                                                        BindingFlags.Static |
                                                        BindingFlags.Instance);
            fi.SetValue(dgCalib, 35);
            dgCalib.GetType().GetMethod("_DataRebind",
                                         BindingFlags.NonPublic |
                                         BindingFlags.Static |
                                         BindingFlags.Instance).Invoke(dgCalib, new object[] { });
            #endregion

            #region 设置列宽
            DataGridTableStyle ts = new DataGridTableStyle();
            ts.MappingName = dt.TableName;//此处非常关键,数据表的名字不对,将无法映射成功
            
            //定义列样式
            DataGridColumnStyle col1ColStyle = new DataGridTextBoxColumn();
            col1ColStyle.MappingName = cstrCol1;
            col1ColStyle.HeaderText = cstrCol1;
            col1ColStyle.Width = 100;
            ts.GridColumnStyles.Add(col1ColStyle);
            //定义列样式
            DataGridColumnStyle col2ColStyle = new DataGridTextBoxColumn();
            col2ColStyle.MappingName = cstrCol2;
            col2ColStyle.HeaderText = cstrCol2;
            col2ColStyle.Width = 100;
            ts.GridColumnStyles.Add(col2ColStyle);
            //定义列样式
            DataGridColumnStyle col3ColStyle = new DataGridTextBoxColumn();
            col3ColStyle.MappingName = cstrCol3;
            col3ColStyle.HeaderText = cstrCol3;
            col3ColStyle.Width = 100;
            ts.GridColumnStyles.Add(col3ColStyle);

            //将样式和控件绑定到一起
            dgCalib.TableStyles.Add(ts);
            #endregion

            dtSource = dt;
            //this.dataGrid1.DataSource = dt;
        }

        private void DataBind()
        {
            if (dtSource.Columns.Count == 3)
            {
                dtSource.Rows.Clear();
                for (int i = 0; i < alSource.Count; i++)
                {
                    DataRow dr = dtSource.NewRow();
                    dr[0] = ((CalibInfo)alSource[i]).Addr;
                    dr[1] = ((CalibInfo)alSource[i]).Weight;
                    dr[2] = cstrCalib;
                    dtSource.Rows.Add(dr);
                }
                this.dgCalib.DataSource = dtSource;
            }
        }

        private void ChangeCalibWeight(byte addr, int weight)
        {
            for (int i = 0; i < alSource.Count; i++)
            {
                if (((CalibInfo)alSource[i]).Addr == addr)
                {
                    ((CalibInfo)alSource[i]).Weight = weight;
                    break;
                }
            }
        }

        private void dgCalib_CurrentCellChanged(object sender, EventArgs e)
        {
            int r = dgCalib.CurrentCell.RowNumber;
            int c = dgCalib.CurrentCell.ColumnNumber;
            //FormMsgBox.Show(r.ToString() + "|" + c.ToString(), "");
            if (c == 1)
            {
                ParamItem item = new ParamItem();
                item.name = "标定重量";
                item.unit = "kg";
                item.param_type = TypeCode.Int32;
                item.param_value = ((CalibInfo)alSource[r]).Weight;
                FormInput dlg = new FormInput(formFrame);
                dlg.SetValue(item, true);
                dlg.ShowDialog();
                if (dlg.GetAck())
                {
                    item = dlg.GetValue();
                    ChangeCalibWeight(((CalibInfo)alSource[r]).Addr, Convert.ToInt32(item.param_value));
                    DataBind();
                }
                dlg.Dispose();
             }
            else if (c == 2)
            {

            }
        }
    }
}

