using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;

namespace Monitor
{
    class PanelBody : Panel
    {
        private FormFrame formFrame = null;

        public bool bHasReceived = false;
        public int DisplayWeight = 0;
        public int BodyWeight = 0;
        public int TotalWeight = 0;
        public int RealtimeWeight = 0;
        public int CarryWeight = 0;
        public bool FlagStable = false;
        public bool FlagZero = false;
        int DisplayWeightMax = 999000;
        int DisplayWeightMin = -999000;

        Bitmap bmNumbers = null;
        Bitmap bmBody = null;
        Bitmap bmBack = null;
        Bitmap bmCheck = null;
        Graphics pb_Graphics = null;

        public void Init(FormFrame f, int left, int top, int width, int height)
        {
            formFrame = f;
            this.Left = left;       //12
            this.Top = top;         //22
            this.Width = width;     //606
            this.Height = height;   //387

            DisplayWeightMax = formFrame.configManage.cfg.paramFormWeight.DisplayWeightMax;
            DisplayWeightMin = formFrame.configManage.cfg.paramFormWeight.DisplayWeightMin;

            //背景图片
            string path = formFrame.configManage.FileDir + @"\main.png";
            if (File.Exists(path))
            {
                bmBack = new Bitmap(path);
                bmBody = new Bitmap(path);
                pb_Graphics = Graphics.FromImage(bmBody);
            }

            //稳定、零位选中图片
            path = formFrame.configManage.FileDir + @"\main_check.png";
            if (File.Exists(path))
            {
                bmCheck = new Bitmap(path);
            }

            //数字图片
            path = formFrame.configManage.FileDir + @"\numbers_prog.png";
            if (File.Exists(path))
            {
                bmNumbers = new Bitmap(path);
                #region 处理原始图片 注释掉
                //for (int w = 0; w < bmNumbers.Width; w++)
                //{
                //    for (int h = 0; h < bmNumbers.Height; h++)
                //    {
                //        Color backColor = pb_BitmapBack.GetPixel(380, 51+h);
                //        Color pixelColor = bmNumbers.GetPixel(w, h);
                //        if ((pixelColor.R < 200) || (h < 3))
                //        {
                //            bmNumbers.SetPixel(w, h, backColor);
                //        }
                //    }
                //}
                #endregion
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (pb_Graphics != null)    pb_Graphics.Dispose();
            if (bmBack != null)         bmBack.Dispose();
            if (bmBody != null)         bmBody.Dispose();
            if (bmCheck != null)        bmCheck.Dispose();
            if (bmNumbers != null)      bmNumbers.Dispose();
            base.Dispose(disposing);
        }

        protected override void OnPaintBackground(PaintEventArgs paintg)
        {
            //不绘制背景
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                //清除绘制面
                pb_Graphics.Clear(Color.Empty);

                //绘制背景
                pb_Graphics.DrawImage(bmBack, 0, 0);
               
                //绘制数据
                DrawWeight(pb_Graphics);

                e.Graphics.DrawImage((Image)bmBody, 0, 0);

                base.OnPaint(e);
            }
            catch
            {
                throw;
            }
        }

        private void DrawWeight(Graphics g)
        {
            //e.Graphics.DrawImage(bmNumbers, 175, 41, GetNumRect(Weight), GraphicsUnit.Pixel);
            int x = 380;
            int y = 51;
            int ImgLeft, ImgWidth;
            string strWeight;
            if (bHasReceived)
            {
                if (DisplayWeight > DisplayWeightMax)
                {
                    strWeight = "-----";
                }
                else if (DisplayWeight < DisplayWeightMin)
                {
                    strWeight = "-";
                }
                else
                {
                    strWeight = DisplayWeight.ToString();
                }
            }
            else
            {
                strWeight = "";
            }
            int len = strWeight.Length;
            char[] arrWeight = new char[len];
            arrWeight = strWeight.ToCharArray();
            for (int i = 0; i < len; i++)
            {
                char c = arrWeight[len - i - 1];
                GetNumStartPoint(c, out ImgLeft, out ImgWidth);
                if ((c == '-') || (c == '.'))
                {
                    x -= ImgWidth;
                }
                else
                {
                    x -= 45;
                }
                g.DrawImage(bmNumbers, x, y, new Rectangle(ImgLeft, 0, ImgWidth, 78), GraphicsUnit.Pixel);
            }


            if (bHasReceived)
            {
                g.DrawString(BodyWeight.ToString(), new Font("宋体", 22, FontStyle.Bold), new SolidBrush(Color.Black), 177, 185);
                g.DrawString(TotalWeight.ToString(), new Font("宋体", 22, FontStyle.Bold), new SolidBrush(Color.Black), 177, 242);
                g.DrawString(RealtimeWeight.ToString(), new Font("宋体", 22, FontStyle.Bold), new SolidBrush(Color.Black), 177, 299);
            }
            else
            {
                g.DrawString("", new Font("宋体", 22, FontStyle.Bold), new SolidBrush(Color.Black), 177, 185);
                g.DrawString("", new Font("宋体", 22, FontStyle.Bold), new SolidBrush(Color.Black), 177, 242);
                g.DrawString("", new Font("宋体", 22, FontStyle.Bold), new SolidBrush(Color.Black), 177, 299);
            }

            if (FlagStable)
            {
                g.DrawImage(bmCheck, 460, 55);
            }
            if (FlagZero)
            {
                g.DrawImage(bmCheck, 460, 93);
            }
        }

        private void GetNumStartPoint(char data, out int ImgLeft, out int ImgWidth)
        {
            ImgWidth = 45;
            switch (data)
            {
                case '0':
                    ImgLeft = 2;
                    ImgWidth = 47;
                    break;
                case '1':
                    ImgLeft = 52;
                    ImgWidth = 43;
                    break;
                case '2':
                    ImgLeft = 96;
                    ImgWidth = 45;
                    break;
                case '3':
                    ImgLeft = 143;
                    ImgWidth = 42;
                    break;
                case '4':
                    ImgLeft = 185;
                    ImgWidth = 45;
                    break;
                case '5':
                    ImgLeft = 230;
                    ImgWidth = 45;
                    break;
                case '6':
                    ImgLeft = 275;
                    ImgWidth = 45;
                    break;
                case '7':
                    ImgLeft = 321;
                    ImgWidth = 42;
                    break;
                case '8':
                    ImgLeft = 363;
                    ImgWidth = 45;
                    break;
                case '9':
                    ImgLeft = 408;
                    ImgWidth = 45;
                    break;
                case '-':
                    ImgLeft = 453;
                    ImgWidth = 30;
                    break;
                case '.':
                    ImgLeft = 483;
                    ImgWidth = 25;
                    break;
                default:
                    ImgLeft = 452;
                    ImgWidth = 30;
                    break;
            }
        }

    }
}
