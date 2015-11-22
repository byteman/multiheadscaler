using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
namespace Monitor
{
    public partial class FormDataList : Form
    {
        private int page_index = 0;
        private int page_count = 0;
        private int data_total = 0;
        private FormFrame formFrame = null;
        private Bitmap bmBtnDown = null;
        private Bitmap bmBtnUp = null;
        private int w = 0;
        public FormDataList(FormFrame f)
        {
            InitializeComponent();
            formFrame = f;

            string path = formFrame.configManage.FileDir + @"\main_btn_down.png";
            if (File.Exists(path))
            {
                bmBtnDown = new Bitmap(path);
            }
            path = formFrame.configManage.FileDir + @"\main_btn_up.png";
            if (File.Exists(path))
            {
                bmBtnUp = new Bitmap(path);
            }

            pbPrevPage.Image = bmBtnUp;
            pbNextPage.Image = bmBtnUp;
            pbClear.Image = bmBtnUp;
            pbExit.Image = bmBtnUp;
            pbAdd.Image = bmBtnUp;
        }

         private void DrawLabel(object sender, PaintEventArgs e, string str)
        {
            e.Graphics.DrawString(str, new Font("宋体", 16, FontStyle.Bold), new SolidBrush(Color.Black), 20, 15);
        }
        public new void Dispose()
        {
            if (bmBtnDown != null) bmBtnDown.Dispose();
            if (bmBtnUp != null) bmBtnUp.Dispose();
            base.Dispose();
        }
        private void update_index(int num)
        {
            data_total = num;
            page_count = (num + 9) / 10;
        }
        private void Refresh_Data()
        {
            int i = 1;
            listView1.Items.Clear();

            DataTable dt = SQLiteDBHelper.listData(page_index, 10);
            foreach (DataRow dr2 in dt.Rows)
            {
                ListViewItem item = new ListViewItem();
                item.SubItems[0].Text = (page_index * 10 + i++).ToString();
                float v = float.Parse(dr2["weight"].ToString());
                item.SubItems.Add(v.ToString("0.0"));
                v = float.Parse(dr2["diff"].ToString());
                item.SubItems.Add(v.ToString("0.0"));
                item.SubItems.Add(dr2["s_date"].ToString());
                item.SubItems.Add(dr2["heads"].ToString());
                listView1.Items.Add(item);
            }

            
            tbDataTotal.Text = data_total.ToString();
            tbTotalPage.Text = page_count.ToString();
            if (page_count == 0)
            {
                page_index = 0;
                tbCurPage.Text = (page_index).ToString();
            }
            else
            {
                tbCurPage.Text = (page_index+1).ToString();
            }

        }
        private void pbPrevPage_Click(object sender, EventArgs e)
        {
            if (page_index > 0)
            {
                page_index--;
                Refresh_Data();
            }
        }

        private void pbPrevPage_Paint(object sender, PaintEventArgs e)
        {
            DrawLabel(sender, e, "上一页");
        }

        private void pbNextPage_Paint(object sender, PaintEventArgs e)
        {
            DrawLabel(sender, e, "下一页");
        }

        private void pbClear_Paint(object sender, PaintEventArgs e)
        {
            DrawLabel(sender, e, "清除");
        }

        private void pbExit_Paint(object sender, PaintEventArgs e)
        {
            DrawLabel(sender, e, "返回");
        }

        private void pbExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pbNextPage_Click(object sender, EventArgs e)
        {
            if (page_index < page_count - 1)
            {
                page_index++;
                Refresh_Data();
            }
        }

        private void pbClear_Click(object sender, EventArgs e)
        {
            SQLiteDBHelper.ClearDB();
            update_index(SQLiteDBHelper.DataCount());
            page_index = 0;
            Refresh_Data();
            tbCurPage.Text = "0";
        }

        private void FormDataList_Load(object sender, EventArgs e)
        {
            

            update_index(SQLiteDBHelper.DataCount());
            page_index = 0;
            //if (page_count > 0)
                Refresh_Data();
        }
        private void pbBtn_MouseDown(object sender, MouseEventArgs e)
        {
            if (bmBtnDown != null)
            {
                ((PictureBox)sender).Image = bmBtnDown;
            }
        }

        private void pbBtn_MouseUp(object sender, MouseEventArgs e)
        {
            if (bmBtnUp != null)
            {
                ((PictureBox)sender).Image = bmBtnUp;
            }
        }

      
        private void pbAdd_Paint(object sender, PaintEventArgs e)
        {
            DrawLabel(sender, e, "添加");
        }

        private void pbAdd_Click(object sender, EventArgs e)
        {
            WeightData data = new WeightData();
            data.weight = w++;
            data.diff = w++;
            data.addZuhe(1);
            data.addZuhe(4);
            data.addZuhe(5);
            SQLiteDBHelper.addData(data);
            update_index(SQLiteDBHelper.DataCount());
            Refresh_Data();
        }


      
       

       
    }
}