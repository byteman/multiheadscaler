using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Threading;

namespace BanOcx
{
    public partial class BanOcxCtl : UserControl
    {
        public delegate void MyEventHandler(object sender, MyEventArges e);

        public event MyEventHandler 点击事件;//中心区点击 会在属性窗口显示  
        protected virtual void OnColorChange(MyEventArges e)
        {
            if (点击事件 != null)
                点击事件(this, e);
        }


        private const int totalnum=30;
        private bool first=true;

        private Color bgcolor=Color.Black;//背景颜色
        private int banNum = 10;//称的数量
        private double banSpace = 10;//各个称的间隔度数,一周是360度
        private Point circlepoint = new Point(170, 170);                             //称中心点坐标
        private Int32 cpR=40;                      //中心点击区半径
        private Color CircleColor = Color.White; //中心点击区的颜色
        private Int32 cpLargR=150;                  //称的半径
        private SolidBrush[] solidbrushs = new SolidBrush[totalnum]; //存储每个称的颜色

        private string[] BanWeight = new string[totalnum]; //存储每个称的重量
        private string[] BanStatus = new string[totalnum]; //存储每个称的状态，每个字母表示一个状态

        private Color NumColor = Color.Black;//编号字体颜色
        private float NumSize = 10;//编号字体大小

        private Color SColor = Color.Green;//字母S的颜色
        private float SSize = 10;//字母S的大小 

        private Color douColor = Color.YellowGreen;//斗区的颜色
        private float douSize = 0;//斗区线条的粗线 
        private Color douLineColor = Color.White;//斗区线条的颜色 

        private Color BanColor = Color.Green;//磅称的默认颜色

        private Color WeightColor = Color.White;//称重量字体颜色
        private float WeightSize = 10;//称重量字体大小

        private Color StatusColor = Color.White;//称状态字体颜色
        private float StatusSize = 15;//称状态字体大小

        private Int32 cpRNum = 30;                      //编号距中心的距离


        public int 编号距中心的距离
        {
            get
            {
                return cpRNum;
            }
            set
            {
                cpRNum = value;
                BufferGraph();

            }
        }


        public Color 称状态字体颜色
        {
            get
            {
                return StatusColor;
            }
            set
            {
                StatusColor = value;
                BufferGraph();
            }
        }
        public float 称状态字体大小
        {
            get
            {
                return StatusSize;
            }
            set
            {
                StatusSize = value;
                BufferGraph();

            }
        }

        public Color 称重量字体颜色
        {
            get
            {
                return WeightColor;
            }
            set
            {
                WeightColor = value;
                BufferGraph();
            }
        }
        public float 称重量字体大小
        {
            get
            {
                return WeightSize;
            }
            set
            {
                WeightSize = value;
                BufferGraph();

            }
        }


        public Color 斗区的颜色
        {
            get
            {
                return douColor;
            }
            set
            {
                douColor = value;
                BufferGraph();
            }
        }
        public float 斗区线条的粗线
        {
            get
            {
                return douSize;
            }
            set
            {
                douSize = value;
                BufferGraph();

            }
        }
        public Color 斗区线条的颜色
        {
            get
            {
                return douLineColor;
            }
            set
            {
                douLineColor = value;
                BufferGraph();

            }
        }


        public Color 字母S的颜色
        {
            get
            {
                return SColor;
            }
            set
            {
                SColor = value;
                BufferGraph();
            }
        }
        public float 字母S的大小 
        {
            get
            {
                return SSize;
            }
            set
            {
                SSize = value;
                BufferGraph();

            }
        }

        public Color 编号字体颜色
        {
            get
            {
                return NumColor;
            }
            set
            {
                NumColor = value;
                BufferGraph();
            }
        }
        public float 编号字体大小
        {
            get
            {
                return NumSize;
            }
            set
            {
                NumSize = value;
                BufferGraph();

            }
        }

        public Color 背景颜色
        {
            get
            {
                return bgcolor;
            }
            set
            {
                bgcolor = value;
                BufferGraph();
            }
        }

        public int 磅称的数量
        {
            get
            {
                return banNum;
            }
            set
            {
                banNum = value;
                BufferGraph();

            }
        }

        public double 磅称的间隔弧度
        {
            get
            {
                return banSpace;
            }
            set
            {
                banSpace = value;
                BufferGraph();

            }
        }

        public Point 磅称中心点坐标
        {
            get
            {
                return circlepoint;
            }
            set
            {
                circlepoint = value;
                BufferGraph();

            }
        }

        public int 中心点击区半径
        {
            get
            {
                return cpR;
            }
            set
            {
                cpR = value;
                BufferGraph();

            }
        }

        public int 磅称的半径
        {
            get
            {
                return cpLargR;
            }
            set
            {
                cpLargR = value;
                BufferGraph();

            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="num">编号</param>
        /// <param name="color">颜色</param>
        /// <returns></returns>
        public string SetBanColor(int num,Color color)
        {
            if (num <= 0)
            {
                return "编号要大于0";
            }
            if (num > banNum)
            { 
                return "编号不能大于设置的个数"+banNum.ToString();
            }
            solidbrushs[num - 1] = new SolidBrush(color); ; 
            BufferGraph();


            return "0";
        }


        public string SetBanWeight(int num, string weight)
        {
            if (num <= 0)
            {
                return "编号要大于0";
            }
            if (num > banNum)
            {
                return "编号不能大于设置的个数" + banNum.ToString();
            }
            BanWeight[num - 1] =weight; ;
            BufferGraph();

            return "0";
        }

        public string SetBanStatus(int num, string status)
        {
            if (num <= 0)
            {
                return "编号要大于0";
            }
            if (num > banNum)
            {
                return "编号不能大于设置的个数" + banNum.ToString();
            }
            BanStatus[num - 1] = status; ;
            BufferGraph();

            return "0";
        }


        
        public BanOcxCtl()
        {
            InitializeComponent();

        }

        private void UC_Ctrl_Paint(object sender, PaintEventArgs e)
        {

            refresh();
            
        }
        private Point GetTop(Int32 pRadius, double pRadian)
        {
            Int32 X;
            Int32 Y;
            X = circlepoint.X + (int)(pRadius * Math.Cos(pRadian));
            Y = circlepoint.Y - (int)(pRadius * Math.Sin(pRadian));
            return new Point(X, Y);
        }
        private void BufferGraph()
        {
            Bitmap Bufferimage = new Bitmap(this.Width, this.Height);
            Graphics g = Graphics.FromImage(Bufferimage);
            g.Clear(bgcolor);  
            //-----------------------------------------------------


            double banPer = (360 - banNum * banSpace) / banNum; //度数



            SolidBrush Circle_Brush = new SolidBrush(CircleColor);//定义中心点画区的色
            
            Pen pen = new Pen(douLineColor, douSize);//定义斗区画线的笔

            //S字体
            Font s_ft = new Font(FontFamily.GenericSansSerif, SSize, FontStyle.Regular);
            //S颜色
            SolidBrush s_Brush = new SolidBrush(SColor);
            //num字体
            Font num_ft = new Font(FontFamily.GenericSansSerif,NumSize, FontStyle.Regular);
            //num颜色
            SolidBrush num_Brush = new SolidBrush(NumColor);
            //画斗区颜色
            SolidBrush doubrushs = new SolidBrush(douColor);


            //称重量字体
            Font Weightft = new Font(FontFamily.GenericSansSerif, WeightSize, FontStyle.Regular);
            //称重量颜色
            SolidBrush WeightBrush = new SolidBrush(WeightColor);


            //称状态字体
            Font Statusft = new Font(FontFamily.GenericSansSerif, StatusSize, FontStyle.Regular);
            //称状态颜色
            SolidBrush StatusBrush = new SolidBrush(StatusColor);




            StringFormat L_format = new StringFormat();
            L_format.Alignment = StringAlignment.Center;
            L_format.LineAlignment = StringAlignment.Center;

            if (first)
            {
                for (int i = 0; i < 30; i++)
                {
                    solidbrushs[i] = new SolidBrush(BanColor);
                    BanWeight[i] = "0";
                    BanStatus[i] = "P";
                }
                first = false;
            }



            double l_f弧度增量空白 = Math.PI / 180.0 * banSpace;
            double l_f弧度增量填充 = Math.PI / 180.0 * banPer;
            double l_f弧度增量填充一半 = Math.PI / 180.0 * banPer / 2;
            double l_f弧度 = l_f弧度增量空白;



            for (Int32 i = 0; i < banNum; i++)
            {

                Point point1 = new Point(circlepoint.X, circlepoint.Y);
                Point point2 = GetTop(cpLargR, l_f弧度);
                Point point11 = GetTop((int)(cpLargR * 0.8), l_f弧度);

                l_f弧度 += l_f弧度增量填充;
                Point point3 = GetTop(cpLargR, l_f弧度);
                Point point44 = GetTop((int)(cpLargR * 0.8), l_f弧度);

                Point[] points1 = new Point[] { point1, point2, point3 };
                //画称区颜色
                g.FillPolygon(solidbrushs[i], points1);
                Point[] points2 = new Point[] { point11, point2, point3, point44 };
                //画斗区
                g.DrawPolygon(pen, points2);
                //画斗区颜色
                g.FillPolygon(doubrushs, points2);

                l_f弧度 += l_f弧度增量空白;
            }

            //画中心点
            g.FillEllipse(Circle_Brush, circlepoint.X - cpR, circlepoint.Y - cpR, 2 * cpR, 2 * cpR);
            //画中心点中的S
            g.DrawString("S", s_ft, s_Brush, circlepoint.X, circlepoint.Y, L_format);


            l_f弧度 = l_f弧度增量空白;
            for (Int32 i = 0; i < banNum; i++)
            {

                Point point1 = new Point(circlepoint.X, circlepoint.Y);
                Point point2 = GetTop(cpLargR, l_f弧度);
                Point point11 = GetTop((int)(cpLargR * 0.8), l_f弧度);
                //画称的编号
                g.DrawString((i + 1).ToString(), num_ft, num_Brush, GetTop(cpRNum, l_f弧度 + l_f弧度增量填充一半).X, GetTop(cpRNum, l_f弧度 + l_f弧度增量填充一半).Y, L_format);

                //画称的重量
                g.DrawString(BanWeight[i], Weightft, WeightBrush, GetTop(cpLargR - 20, l_f弧度 + l_f弧度增量填充一半).X, GetTop(cpLargR - 20, l_f弧度 + l_f弧度增量填充一半).Y, L_format);

                //画称的状态
                g.DrawString(BanStatus[i], Statusft, StatusBrush, GetTop(cpLargR + 10, l_f弧度 + l_f弧度增量填充一半).X, GetTop(cpLargR + 10, l_f弧度 + l_f弧度增量填充一半).Y, L_format);
               
                l_f弧度 += l_f弧度增量填充;

                l_f弧度 += l_f弧度增量空白;
            }





            Weightft.Dispose();
            WeightBrush.Dispose();
            Statusft.Dispose();
            StatusBrush.Dispose();

            s_ft.Dispose();
            num_ft.Dispose();
            L_format.Dispose();

            s_Brush.Dispose();
            num_Brush.Dispose();
            Circle_Brush.Dispose();
            doubrushs.Dispose();

            pen.Dispose();

            //-----------------------------------------------------
            this.CreateGraphics().DrawImage(Bufferimage,0, 0 );
            Bufferimage.Dispose();
            g.Dispose();
        }
        public void refresh()
        {
            BufferGraph();
        }

        private void BanOcxCtl_MouseMove(object sender, MouseEventArgs e)
        {
            //Point p = new Point(e.X, e.Y);
            //Point p2 = circlepoint;
            //double value = Math.Sqrt(Math.Abs(p.X - p2.X) * Math.Abs(p.X - p2.X) + Math.Abs(p.Y - p2.Y) * Math.Abs(p.Y - p2.Y));
            ////if (value < cpR)
            ////{
            //    int R, G, B;
            //    R = 255 - CircleColor.R;//反红
            //    G = 255 - CircleColor.G;//反绿
            //    B = 255 - CircleColor.B;//反蓝

            //    StringFormat L_format = new StringFormat();
            //    L_format.Alignment = StringAlignment.Center;
            //    L_format.LineAlignment = StringAlignment.Center;
            //    //S字体
            //    Font s_ft = new Font(FontFamily.GenericSansSerif, SSize, FontStyle.Regular);
            //    //S颜色
            //    SolidBrush s_Brush = new SolidBrush(SColor);

            //    Graphics g = this.CreateGraphics();
            //    SolidBrush Circle_Brush = new SolidBrush(Color.FromArgb(R, G, B));//定义中心点画区的色
            //    //画中心点
            //    g.FillEllipse(Circle_Brush, circlepoint.X - cpR, circlepoint.Y - cpR, 2 * cpR, 2 * cpR);
            //    //画中心点中的S
            //    g.DrawString("S", s_ft, s_Brush, circlepoint.X, circlepoint.Y, L_format);
            //    Circle_Brush.Dispose();
            //    s_ft.Dispose();
            //    s_Brush.Dispose();
            //    L_format.Dispose();
            //    g.Dispose();
            ////}
            
            //////regi reg = new Rectangle(区域的坐标和大小);
            //////if (rectangle.Contains(MousePosition))
            //////{
            //////}  

            ////Point p = new Point(e.X, e.Y);
            ////Point p2 = circlepoint; 
            ////double value = Math.Sqrt(Math.Abs(p.X - p2.X) * Math.Abs(p.X - p2.X) + Math.Abs(p.Y - p2.Y) * Math.Abs(p.Y - p2.Y));
            ////if (value < cpR)
            ////{
            ////    //Cursor cr = Cursor.Current;
            ////    //Cursor.Current = Cursors.WaitCursor;//将光标置为手
            ////    //Cursor.Current = cr;//将光标置回原来状态


            ////    Graphics g = this.CreateGraphics();

            ////    StringFormat L_format = new StringFormat();
            ////    L_format.Alignment = StringAlignment.Center;
            ////    L_format.LineAlignment = StringAlignment.Center;

            ////    SolidBrush s_Brush = new SolidBrush(SColor);
            ////    Font s_ft = new Font(FontFamily.GenericSansSerif, SSize, FontStyle.Regular);
            ////    g.DrawString(value.ToString(), s_ft, s_Brush, circlepoint.X, circlepoint.Y, L_format);

            ////    s_Brush.Dispose();
            ////    s_ft.Dispose();
            ////    g.Dispose();
            ////}
            ////else
            ////{ 
            
            ////}
        }

 
        private void BanOcxCtl_MouseDown(object sender, MouseEventArgs e)
        {
            Point p = new Point(e.X, e.Y);
            ckcolor(p, 1);
        }

        private void BanOcxCtl_MouseUp(object sender, MouseEventArgs e)
        {
            Point p = new Point(e.X, e.Y);
            ckcolor(p, 2);
        }
        private void ckcolor(Point p,int step) 
        {
            int selnum = -1;
            int R, G, B;



            Graphics g = this.CreateGraphics();
            Point p2 = circlepoint;
            double value = Math.Sqrt(Math.Abs(p.X - p2.X) * Math.Abs(p.X - p2.X) + Math.Abs(p.Y - p2.Y) * Math.Abs(p.Y - p2.Y));
            if (value < cpR)
            {


                StringFormat L_format = new StringFormat();
                L_format.Alignment = StringAlignment.Center;
                L_format.LineAlignment = StringAlignment.Center;
                //S字体
                Font s_ft = new Font(FontFamily.GenericSansSerif, SSize, FontStyle.Regular);
                //S颜色
                SolidBrush s_Brush = new SolidBrush(SColor);
                SolidBrush Circle_Brush;


                if (step == 1)
                {
                    R = 255 - CircleColor.R;//反红
                    G = 255 - CircleColor.G;//反绿
                    B = 255 - CircleColor.B;//反蓝
                    Circle_Brush = new SolidBrush(Color.FromArgb(R, G, B));//定义中心点画区的色

                    //画中心点
                    g.FillEllipse(Circle_Brush, circlepoint.X - cpR, circlepoint.Y - cpR, 2 * cpR, 2 * cpR);
                    //画中心点中的S
                    g.DrawString("S", s_ft, s_Brush, circlepoint.X, circlepoint.Y, L_format);
                    Circle_Brush.Dispose();
                    s_ft.Dispose();
                    s_Brush.Dispose();
                    L_format.Dispose();
                }
                else
                {
                  //  Circle_Brush = new SolidBrush(CircleColor);//定义中心点画区的色
                    selnum = 0;

                   // Thread.Sleep(500);
                    refresh();

                    this.OnColorChange(new MyEventArges(selnum));
                }
 
            }
            else
            {
                double banPer = (360 - banNum * banSpace) / banNum; //度数
                double l_f弧度增量空白 = Math.PI / 180.0 * banSpace;
                double l_f弧度增量填充 = Math.PI / 180.0 * banPer;
                double l_f弧度增量填充一半 = Math.PI / 180.0 * banPer / 2;
                double l_f弧度 = l_f弧度增量空白;
                //画斗区颜色
                SolidBrush doubrushs = new SolidBrush(douColor);

                for (Int32 i = 0; i < banNum; i++)
                {

                    Point point1 = new Point(circlepoint.X, circlepoint.Y);
                    Point point2 = GetTop(cpLargR, l_f弧度);
                    Point point11 = GetTop((int)(cpLargR * 0.8), l_f弧度);
                    l_f弧度 += l_f弧度增量填充;
                    Point point3 = GetTop(cpLargR, l_f弧度);
                    Point point44 = GetTop((int)(cpLargR * 0.8), l_f弧度);

                    Point[] points1 = new Point[] { point1, point2, point3 };

                    bool isInner = PointInFences(p, points1);
                    if (isInner)
                    {
                        if (step == 1)
                        {
                            R = 255 - solidbrushs[i].Color.R;//反红
                            G = 255 - solidbrushs[i].Color.G;//反绿
                            B = 255 - solidbrushs[i].Color.B;//反蓝

                            g.FillPolygon(new SolidBrush(Color.FromArgb(R, G, B)), points1); //画称区颜色

                            StringFormat L_format = new StringFormat();
                            L_format.Alignment = StringAlignment.Center;
                            L_format.LineAlignment = StringAlignment.Center;
                            //S字体
                            Font s_ft = new Font(FontFamily.GenericSansSerif, SSize, FontStyle.Regular);
                            //S颜色
                            SolidBrush s_Brush = new SolidBrush(SColor);
                            SolidBrush Circle_Brush = new SolidBrush(CircleColor);//定义中心点画区的色

                            //画中心点
                            g.FillEllipse(Circle_Brush, circlepoint.X - cpR, circlepoint.Y - cpR, 2 * cpR, 2 * cpR);
                            //画中心点中的S
                            g.DrawString("S", s_ft, s_Brush, circlepoint.X, circlepoint.Y, L_format);

                            //num字体
                            Font num_ft = new Font(FontFamily.GenericSansSerif, NumSize, FontStyle.Regular);
                            //num颜色
                            SolidBrush num_Brush = new SolidBrush(NumColor);
                           // //画称的编号
                           // g.DrawString((i + 1).ToString(), num_ft, num_Brush, GetTop(cpRNum, l_f弧度 + l_f弧度增量填充一半).X, GetTop(cpRNum, l_f弧度 + l_f弧度增量填充一半).Y, L_format);

                            l_f弧度 = l_f弧度增量空白;
                            for (Int32 k = 0; k < banNum; k++)
                            {

                                 //point1 = new Point(circlepoint.X, circlepoint.Y);
                                 //point2 = GetTop(cpLargR, l_f弧度);
                                 //point11 = GetTop((int)(cpLargR * 0.8), l_f弧度);
                                //画称的编号
                                g.DrawString((k + 1).ToString(), num_ft, num_Brush, GetTop(cpRNum, l_f弧度 + l_f弧度增量填充一半).X, GetTop(cpRNum, l_f弧度 + l_f弧度增量填充一半).Y, L_format);


                                l_f弧度 += l_f弧度增量填充;

                                l_f弧度 += l_f弧度增量空白;
                            }

   

                            num_ft.Dispose();

                             num_Brush.Dispose();

                            Circle_Brush.Dispose();
                            s_ft.Dispose();
                            s_Brush.Dispose();
                            L_format.Dispose();
                        }
                        else
                        {
                            //g.FillPolygon(solidbrushs[i], points1); //画称区颜色
                            selnum = i+1;

                           // Thread.Sleep(500);
                            refresh();
                        }

                        //Point[] points2 = new Point[] { point11, point2, point3, point44 };
                        ////画斗区颜色
                        //g.FillPolygon(doubrushs, points2);



               

         
                        break;
                    }

                    l_f弧度 += l_f弧度增量空白;
                }
                doubrushs.Dispose();
            }
            g.Dispose();

            if (selnum>-1)
                this.OnColorChange(new MyEventArges(selnum));
        }


        private bool PointInFences(Point pnt1, Point[] fencePnts)
        {
            int wn = 0, j = 0; //wn 计数器 j第二个点指针
            for (int i = 0; i < fencePnts.Length; i++)
            {//开始循环
                if (i == fencePnts.Length - 1)
                    j = 0;//如果 循环到最后一点 第二个指针指向第一点
                else
                    j = j + 1; //如果不是 ，则找下一点


                if (fencePnts[i].Y <= pnt1.Y) // 如果多边形的点 小于等于 选定点的 Y 坐标
                {
                    if (fencePnts[j].Y > pnt1.Y) // 如果多边形的下一点 大于于 选定点的 Y 坐标
                    {
                        if (isLeft(fencePnts[i], fencePnts[j], pnt1) > 0)
                        {
                            wn++;
                        }
                    }
                }
                else
                {
                    if (fencePnts[j].Y <= pnt1.Y)
                    {
                        if (isLeft(fencePnts[i], fencePnts[j], pnt1) < 0)
                        {
                            wn--;
                        }
                    }
                }
            }
            if (wn == 0)
                return false;
            else
                return true;
        }

        private int isLeft(Point P0, Point P1, Point P2)
        {
            int abc = ((P1.X - P0.X) * (P2.Y - P0.Y) - (P2.X - P0.X) * (P1.Y - P0.Y));
            return abc;
        }
    }
    public class MyEventArges : EventArgs
    {
        private int selNum;

        public int SelNum
        {
            get { return selNum; }
            set { selNum = value; }
        }

        public MyEventArges(int sel)
        {
            this.selNum = sel;
        }
    }
}