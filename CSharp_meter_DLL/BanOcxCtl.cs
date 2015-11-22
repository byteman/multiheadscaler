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

        public event MyEventHandler ����¼�;//��������� �������Դ�����ʾ  
        protected virtual void OnColorChange(MyEventArges e)
        {
            if (����¼� != null)
                ����¼�(this, e);
        }


        private const int totalnum=30;
        private bool first=true;

        private Color bgcolor=Color.Black;//������ɫ
        private int banNum = 10;//�Ƶ�����
        private double banSpace = 10;//�����Ƶļ������,һ����360��
        private Point circlepoint = new Point(170, 170);                             //�����ĵ�����
        private Int32 cpR=40;                      //���ĵ�����뾶
        private Color CircleColor = Color.White; //���ĵ��������ɫ
        private Int32 cpLargR=150;                  //�Ƶİ뾶
        private SolidBrush[] solidbrushs = new SolidBrush[totalnum]; //�洢ÿ���Ƶ���ɫ

        private string[] BanWeight = new string[totalnum]; //�洢ÿ���Ƶ�����
        private string[] BanStatus = new string[totalnum]; //�洢ÿ���Ƶ�״̬��ÿ����ĸ��ʾһ��״̬

        private Color NumColor = Color.Black;//���������ɫ
        private float NumSize = 10;//��������С

        private Color SColor = Color.Green;//��ĸS����ɫ
        private float SSize = 10;//��ĸS�Ĵ�С 

        private Color douColor = Color.YellowGreen;//��������ɫ
        private float douSize = 0;//���������Ĵ��� 
        private Color douLineColor = Color.White;//������������ɫ 

        private Color BanColor = Color.Green;//���Ƶ�Ĭ����ɫ

        private Color WeightColor = Color.White;//������������ɫ
        private float WeightSize = 10;//�����������С

        private Color StatusColor = Color.White;//��״̬������ɫ
        private float StatusSize = 15;//��״̬�����С

        private Int32 cpRNum = 30;                      //��ž����ĵľ���


        public int ��ž����ĵľ���
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


        public Color ��״̬������ɫ
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
        public float ��״̬�����С
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

        public Color ������������ɫ
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
        public float �����������С
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


        public Color ��������ɫ
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
        public float ���������Ĵ���
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
        public Color ������������ɫ
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


        public Color ��ĸS����ɫ
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
        public float ��ĸS�Ĵ�С 
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

        public Color ���������ɫ
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
        public float ��������С
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

        public Color ������ɫ
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

        public int ���Ƶ�����
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

        public double ���Ƶļ������
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

        public Point �������ĵ�����
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

        public int ���ĵ�����뾶
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

        public int ���Ƶİ뾶
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
        /// <param name="num">���</param>
        /// <param name="color">��ɫ</param>
        /// <returns></returns>
        public string SetBanColor(int num,Color color)
        {
            if (num <= 0)
            {
                return "���Ҫ����0";
            }
            if (num > banNum)
            { 
                return "��Ų��ܴ������õĸ���"+banNum.ToString();
            }
            solidbrushs[num - 1] = new SolidBrush(color); ; 
            BufferGraph();


            return "0";
        }


        public string SetBanWeight(int num, string weight)
        {
            if (num <= 0)
            {
                return "���Ҫ����0";
            }
            if (num > banNum)
            {
                return "��Ų��ܴ������õĸ���" + banNum.ToString();
            }
            BanWeight[num - 1] =weight; ;
            BufferGraph();

            return "0";
        }

        public string SetBanStatus(int num, string status)
        {
            if (num <= 0)
            {
                return "���Ҫ����0";
            }
            if (num > banNum)
            {
                return "��Ų��ܴ������õĸ���" + banNum.ToString();
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


            double banPer = (360 - banNum * banSpace) / banNum; //����



            SolidBrush Circle_Brush = new SolidBrush(CircleColor);//�������ĵ㻭����ɫ
            
            Pen pen = new Pen(douLineColor, douSize);//���嶷�����ߵı�

            //S����
            Font s_ft = new Font(FontFamily.GenericSansSerif, SSize, FontStyle.Regular);
            //S��ɫ
            SolidBrush s_Brush = new SolidBrush(SColor);
            //num����
            Font num_ft = new Font(FontFamily.GenericSansSerif,NumSize, FontStyle.Regular);
            //num��ɫ
            SolidBrush num_Brush = new SolidBrush(NumColor);
            //��������ɫ
            SolidBrush doubrushs = new SolidBrush(douColor);


            //����������
            Font Weightft = new Font(FontFamily.GenericSansSerif, WeightSize, FontStyle.Regular);
            //��������ɫ
            SolidBrush WeightBrush = new SolidBrush(WeightColor);


            //��״̬����
            Font Statusft = new Font(FontFamily.GenericSansSerif, StatusSize, FontStyle.Regular);
            //��״̬��ɫ
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



            double l_f���������հ� = Math.PI / 180.0 * banSpace;
            double l_f����������� = Math.PI / 180.0 * banPer;
            double l_f�����������һ�� = Math.PI / 180.0 * banPer / 2;
            double l_f���� = l_f���������հ�;



            for (Int32 i = 0; i < banNum; i++)
            {

                Point point1 = new Point(circlepoint.X, circlepoint.Y);
                Point point2 = GetTop(cpLargR, l_f����);
                Point point11 = GetTop((int)(cpLargR * 0.8), l_f����);

                l_f���� += l_f�����������;
                Point point3 = GetTop(cpLargR, l_f����);
                Point point44 = GetTop((int)(cpLargR * 0.8), l_f����);

                Point[] points1 = new Point[] { point1, point2, point3 };
                //��������ɫ
                g.FillPolygon(solidbrushs[i], points1);
                Point[] points2 = new Point[] { point11, point2, point3, point44 };
                //������
                g.DrawPolygon(pen, points2);
                //��������ɫ
                g.FillPolygon(doubrushs, points2);

                l_f���� += l_f���������հ�;
            }

            //�����ĵ�
            g.FillEllipse(Circle_Brush, circlepoint.X - cpR, circlepoint.Y - cpR, 2 * cpR, 2 * cpR);
            //�����ĵ��е�S
            g.DrawString("S", s_ft, s_Brush, circlepoint.X, circlepoint.Y, L_format);


            l_f���� = l_f���������հ�;
            for (Int32 i = 0; i < banNum; i++)
            {

                Point point1 = new Point(circlepoint.X, circlepoint.Y);
                Point point2 = GetTop(cpLargR, l_f����);
                Point point11 = GetTop((int)(cpLargR * 0.8), l_f����);
                //���Ƶı��
                g.DrawString((i + 1).ToString(), num_ft, num_Brush, GetTop(cpRNum, l_f���� + l_f�����������һ��).X, GetTop(cpRNum, l_f���� + l_f�����������һ��).Y, L_format);

                //���Ƶ�����
                g.DrawString(BanWeight[i], Weightft, WeightBrush, GetTop(cpLargR - 20, l_f���� + l_f�����������һ��).X, GetTop(cpLargR - 20, l_f���� + l_f�����������һ��).Y, L_format);

                //���Ƶ�״̬
                g.DrawString(BanStatus[i], Statusft, StatusBrush, GetTop(cpLargR + 10, l_f���� + l_f�����������һ��).X, GetTop(cpLargR + 10, l_f���� + l_f�����������һ��).Y, L_format);
               
                l_f���� += l_f�����������;

                l_f���� += l_f���������հ�;
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
            //    R = 255 - CircleColor.R;//����
            //    G = 255 - CircleColor.G;//����
            //    B = 255 - CircleColor.B;//����

            //    StringFormat L_format = new StringFormat();
            //    L_format.Alignment = StringAlignment.Center;
            //    L_format.LineAlignment = StringAlignment.Center;
            //    //S����
            //    Font s_ft = new Font(FontFamily.GenericSansSerif, SSize, FontStyle.Regular);
            //    //S��ɫ
            //    SolidBrush s_Brush = new SolidBrush(SColor);

            //    Graphics g = this.CreateGraphics();
            //    SolidBrush Circle_Brush = new SolidBrush(Color.FromArgb(R, G, B));//�������ĵ㻭����ɫ
            //    //�����ĵ�
            //    g.FillEllipse(Circle_Brush, circlepoint.X - cpR, circlepoint.Y - cpR, 2 * cpR, 2 * cpR);
            //    //�����ĵ��е�S
            //    g.DrawString("S", s_ft, s_Brush, circlepoint.X, circlepoint.Y, L_format);
            //    Circle_Brush.Dispose();
            //    s_ft.Dispose();
            //    s_Brush.Dispose();
            //    L_format.Dispose();
            //    g.Dispose();
            ////}
            
            //////regi reg = new Rectangle(���������ʹ�С);
            //////if (rectangle.Contains(MousePosition))
            //////{
            //////}  

            ////Point p = new Point(e.X, e.Y);
            ////Point p2 = circlepoint; 
            ////double value = Math.Sqrt(Math.Abs(p.X - p2.X) * Math.Abs(p.X - p2.X) + Math.Abs(p.Y - p2.Y) * Math.Abs(p.Y - p2.Y));
            ////if (value < cpR)
            ////{
            ////    //Cursor cr = Cursor.Current;
            ////    //Cursor.Current = Cursors.WaitCursor;//�������Ϊ��
            ////    //Cursor.Current = cr;//������û�ԭ��״̬


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
                //S����
                Font s_ft = new Font(FontFamily.GenericSansSerif, SSize, FontStyle.Regular);
                //S��ɫ
                SolidBrush s_Brush = new SolidBrush(SColor);
                SolidBrush Circle_Brush;


                if (step == 1)
                {
                    R = 255 - CircleColor.R;//����
                    G = 255 - CircleColor.G;//����
                    B = 255 - CircleColor.B;//����
                    Circle_Brush = new SolidBrush(Color.FromArgb(R, G, B));//�������ĵ㻭����ɫ

                    //�����ĵ�
                    g.FillEllipse(Circle_Brush, circlepoint.X - cpR, circlepoint.Y - cpR, 2 * cpR, 2 * cpR);
                    //�����ĵ��е�S
                    g.DrawString("S", s_ft, s_Brush, circlepoint.X, circlepoint.Y, L_format);
                    Circle_Brush.Dispose();
                    s_ft.Dispose();
                    s_Brush.Dispose();
                    L_format.Dispose();
                }
                else
                {
                  //  Circle_Brush = new SolidBrush(CircleColor);//�������ĵ㻭����ɫ
                    selnum = 0;

                   // Thread.Sleep(500);
                    refresh();

                    this.OnColorChange(new MyEventArges(selnum));
                }
 
            }
            else
            {
                double banPer = (360 - banNum * banSpace) / banNum; //����
                double l_f���������հ� = Math.PI / 180.0 * banSpace;
                double l_f����������� = Math.PI / 180.0 * banPer;
                double l_f�����������һ�� = Math.PI / 180.0 * banPer / 2;
                double l_f���� = l_f���������հ�;
                //��������ɫ
                SolidBrush doubrushs = new SolidBrush(douColor);

                for (Int32 i = 0; i < banNum; i++)
                {

                    Point point1 = new Point(circlepoint.X, circlepoint.Y);
                    Point point2 = GetTop(cpLargR, l_f����);
                    Point point11 = GetTop((int)(cpLargR * 0.8), l_f����);
                    l_f���� += l_f�����������;
                    Point point3 = GetTop(cpLargR, l_f����);
                    Point point44 = GetTop((int)(cpLargR * 0.8), l_f����);

                    Point[] points1 = new Point[] { point1, point2, point3 };

                    bool isInner = PointInFences(p, points1);
                    if (isInner)
                    {
                        if (step == 1)
                        {
                            R = 255 - solidbrushs[i].Color.R;//����
                            G = 255 - solidbrushs[i].Color.G;//����
                            B = 255 - solidbrushs[i].Color.B;//����

                            g.FillPolygon(new SolidBrush(Color.FromArgb(R, G, B)), points1); //��������ɫ

                            StringFormat L_format = new StringFormat();
                            L_format.Alignment = StringAlignment.Center;
                            L_format.LineAlignment = StringAlignment.Center;
                            //S����
                            Font s_ft = new Font(FontFamily.GenericSansSerif, SSize, FontStyle.Regular);
                            //S��ɫ
                            SolidBrush s_Brush = new SolidBrush(SColor);
                            SolidBrush Circle_Brush = new SolidBrush(CircleColor);//�������ĵ㻭����ɫ

                            //�����ĵ�
                            g.FillEllipse(Circle_Brush, circlepoint.X - cpR, circlepoint.Y - cpR, 2 * cpR, 2 * cpR);
                            //�����ĵ��е�S
                            g.DrawString("S", s_ft, s_Brush, circlepoint.X, circlepoint.Y, L_format);

                            //num����
                            Font num_ft = new Font(FontFamily.GenericSansSerif, NumSize, FontStyle.Regular);
                            //num��ɫ
                            SolidBrush num_Brush = new SolidBrush(NumColor);
                           // //���Ƶı��
                           // g.DrawString((i + 1).ToString(), num_ft, num_Brush, GetTop(cpRNum, l_f���� + l_f�����������һ��).X, GetTop(cpRNum, l_f���� + l_f�����������һ��).Y, L_format);

                            l_f���� = l_f���������հ�;
                            for (Int32 k = 0; k < banNum; k++)
                            {

                                 //point1 = new Point(circlepoint.X, circlepoint.Y);
                                 //point2 = GetTop(cpLargR, l_f����);
                                 //point11 = GetTop((int)(cpLargR * 0.8), l_f����);
                                //���Ƶı��
                                g.DrawString((k + 1).ToString(), num_ft, num_Brush, GetTop(cpRNum, l_f���� + l_f�����������һ��).X, GetTop(cpRNum, l_f���� + l_f�����������һ��).Y, L_format);


                                l_f���� += l_f�����������;

                                l_f���� += l_f���������հ�;
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
                            //g.FillPolygon(solidbrushs[i], points1); //��������ɫ
                            selnum = i+1;

                           // Thread.Sleep(500);
                            refresh();
                        }

                        //Point[] points2 = new Point[] { point11, point2, point3, point44 };
                        ////��������ɫ
                        //g.FillPolygon(doubrushs, points2);



               

         
                        break;
                    }

                    l_f���� += l_f���������հ�;
                }
                doubrushs.Dispose();
            }
            g.Dispose();

            if (selnum>-1)
                this.OnColorChange(new MyEventArges(selnum));
        }


        private bool PointInFences(Point pnt1, Point[] fencePnts)
        {
            int wn = 0, j = 0; //wn ������ j�ڶ�����ָ��
            for (int i = 0; i < fencePnts.Length; i++)
            {//��ʼѭ��
                if (i == fencePnts.Length - 1)
                    j = 0;//��� ѭ�������һ�� �ڶ���ָ��ָ���һ��
                else
                    j = j + 1; //������� ��������һ��


                if (fencePnts[i].Y <= pnt1.Y) // �������εĵ� С�ڵ��� ѡ����� Y ����
                {
                    if (fencePnts[j].Y > pnt1.Y) // �������ε���һ�� ������ ѡ����� Y ����
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