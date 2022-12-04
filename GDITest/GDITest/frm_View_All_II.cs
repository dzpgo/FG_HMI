using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GDITest
{
    public partial class frm_View_All_II : Form
    {
        public frm_View_All_II()
        {
            InitializeComponent();
        }


        clsLaserPointsMap LaserPointMap = new clsLaserPointsMap();

        //默认按照1：10的比列去显示
        private int ScaleView = 10;  

        //显示图像的X和Y的起点
        private int XStart_Bmp_Vertical = 0;
        private int XStart_Bmp_Horizontal = 0;
        private int YStart_Bmp = 0;

        //水平和垂直视图的对象
        Bitmap OriginalBitmap_Vertical = new Bitmap(1, 1);
        Bitmap OriginalBitmap_Horizontal = new Bitmap(1, 1);

        string strFileName_Horizontal = "horizontal";
        string strFileName_vertical = "vertical";

        Dictionary<int, Bitmap> DicBitMap_Vertical = new Dictionary<int, Bitmap>();
        Dictionary<int, Bitmap> DicBitMap_Horizontal = new Dictionary<int, Bitmap>();
        
        //窗体load函数
        private void frm_View_All_II_Load(object sender, EventArgs e)
        {
            try
            {
                this.pictureBox_Horizontal.MouseMove += new MouseEventHandler(pictureBox_Horizontal_MouseMove);
                this.pictureBox_Vertical.MouseMove += new MouseEventHandler(pictureBox_Vertical_MouseMove);
            }
            catch (Exception ex)
            {
            }
        }


        //获得激光数据
        private void GetData()
        {
            try
            {
                //拿到数据
                LaserPointMap.GetData_Txt();
            }
            catch (Exception ex)
            {
            }
        }


        //产生垂直视角的原始图片
        private void Create_Original_Bmp_Vertical()
        {
            try
            {
                //得到车长
                long intCarLength = Math.Abs(LaserPointMap.Y_Max - LaserPointMap.Y_Min);

                //得到车宽
                long intCarWidth = Math.Abs(LaserPointMap.X_Max - LaserPointMap.X_Min);

                double scale = 1;

                //获得高度的有效范围
                long intCarHeight = Math.Abs(LaserPointMap.Z_Max - LaserPointMap.Z_Min);

                //按照高度，取得高度上aloha的换算比例
                double scale_Alpha = Convert.ToDouble(intCarHeight) / Convert.ToDouble(255);

                //在内存里面建立一个位图，并且建立该位图的图形对象
                OriginalBitmap_Vertical = new Bitmap(Convert.ToInt32(intCarLength) + 200, Convert.ToInt32(intCarWidth) + 200);

                Graphics bitmapGraphics = Graphics.FromImage(OriginalBitmap_Vertical);

                //定义颜色和画笔
                Color blue = Color.FromArgb(0, 0, 255);
                SolidBrush myBrush = new SolidBrush(Color.Black);

                //打底色
                bitmapGraphics.FillRectangle(myBrush, 0, 0, OriginalBitmap_Vertical.Width, OriginalBitmap_Vertical.Height);

                //绘图
                foreach (clsLaserPoint thePoint in LaserPointMap.LaserPoints)
                {
                    long inZ_Realtive = LaserPointMap.Z_Max - thePoint.Z;
                    int alpha = Convert.ToInt32(inZ_Realtive / scale_Alpha);
                    if (alpha < 0) { alpha = 0; }
                    if (alpha > 255) { alpha = 255; }

                    myBrush.Color = Color.FromArgb(alpha, Color.Red);
                    long Y_Realtive = thePoint.Y - LaserPointMap.Y_Min;
                    long X_Realtive = thePoint.X - LaserPointMap.X_Min;
                    int X_Graphics = Convert.ToInt32(Y_Realtive * scale);
                    int Y_Graphics = Convert.ToInt32(X_Realtive * scale);
                    bitmapGraphics.FillRectangle(myBrush, X_Graphics - 5, Y_Graphics - 5,
                                                          10, 10);

                }

                OriginalBitmap_Vertical.Save("d:\\"+this.strFileName_vertical+"_1"+".bmp");




                //销毁绘图的对象
                myBrush.Dispose();

                bitmapGraphics.Dispose();


                //displayBitmap.Dispose();

                XStart_Bmp_Vertical = Convert.ToInt32(LaserPointMap.X_Min); ;
                YStart_Bmp = Convert.ToInt32(LaserPointMap.Y_Min);
                XStart_Bmp_Horizontal = intZMax_BitMap / 2 + this.conLine_Horizontal_Y.Height / 2 * ScaleView;


                //MessageBox.Show(YStart_Bmp_Vertical.ToString());

            }
            catch (Exception ex)
            {
            }
        }


        private Int32 intZMax_BitMap = 4000;//图片的默认高度为4m

        //产生水平视角的原始图片
        private void Create_Original_Bmp_Horizontal()
        {
            try
            {
                //得到车长
                long intCarLength = Math.Abs(LaserPointMap.Y_Max - LaserPointMap.Y_Min);
                //得到车宽
                long intCarWidth = Math.Abs(LaserPointMap.X_Max - LaserPointMap.X_Min);

                double scale = 1;
                //获得高度的有效范围

                long intCarHeight = Math.Abs(LaserPointMap.Z_Max - LaserPointMap.Z_Min);
                //按照高度，取得高度上aloha的换算比例

                double scale_Alpha = Convert.ToDouble(intCarHeight) / Convert.ToDouble(255);


                //在内存里面建立一个位图，并且建立该位图的图形对象
                //显示的最大高度高定位4M
                OriginalBitmap_Horizontal = new Bitmap(Convert.ToInt32(intCarLength) + 200, Convert.ToInt32(intZMax_BitMap) + 200);

                Graphics bitmapGraphics = Graphics.FromImage(OriginalBitmap_Horizontal);

                //定义颜色和画笔
                Color blue = Color.FromArgb(0, 0, 255);
                SolidBrush myBrush = new SolidBrush(Color.Black);

                //打底色
                bitmapGraphics.FillRectangle(myBrush, 0, 0, OriginalBitmap_Horizontal.Width, OriginalBitmap_Horizontal.Height);

                foreach (clsLaserPoint thePoint in LaserPointMap.LaserPoints)
                {
                    long inZ_Realtive = LaserPointMap.Z_Max - thePoint.Z;
                    int alpha = Convert.ToInt32(inZ_Realtive / scale_Alpha);
                    if (alpha < 0) { alpha = 0; }
                    if (alpha > 255) { alpha = 255; }

                    myBrush.Color = Color.FromArgb(alpha, Color.Red);
                    long Y_Realtive = thePoint.Y - LaserPointMap.Y_Min;
                    int X_Graphics = Convert.ToInt32(Y_Realtive * scale);
                    int Y_Graphics = Convert.ToInt32((intZMax_BitMap - thePoint.Z) * scale);
                    bitmapGraphics.FillRectangle(myBrush, X_Graphics - 5, Y_Graphics - 5,
                                                          10, 10);
                }

                OriginalBitmap_Horizontal.Save("d:\\"+this.strFileName_Horizontal+"_1"+".bmp");


                //销毁绘图的对象
                myBrush.Dispose();

                bitmapGraphics.Dispose();

                //localBitmap.Dispose();

                //displayBitmap.Dispose();

                XStart_Bmp_Vertical = Convert.ToInt32(LaserPointMap.X_Min); ;
                YStart_Bmp = Convert.ToInt32(LaserPointMap.Y_Min);
                XStart_Bmp_Horizontal = intZMax_BitMap / 2 + this.conLine_Horizontal_Y.Height / 2 * ScaleView;



            }
            catch (Exception ex)
            {
            }
        }

        //数据处理按钮按下
        private void cmd_TreatData_Click(object sender, EventArgs e)
        {
            try
            {
                //默认比例为1比10
                ScaleView = 10;

                //获得激光数据
                this.GetData();
                //产生水平原始比例图片
                Create_Original_Bmp_Vertical();

                //获得垂直原始比例图片
                Create_Original_Bmp_Horizontal();



                int X_Locked =0;
                int Y_Locked = 0;
                int Z_Locked = 1600;

                Cal_X_Y_Z(ref X_Locked, ref Y_Locked,ref Z_Locked);





                //将原图按照现在的比例进行缩放
                //NewScalBitamp是按照比例进行缩放的完整的图
                for (int temScaleView = 4; temScaleView <= 40; temScaleView = temScaleView + 2)
                {
                    Bitmap NewScalBitamp = new Bitmap(OriginalBitmap_Vertical, new Size(OriginalBitmap_Vertical.Width / temScaleView, OriginalBitmap_Vertical.Height / temScaleView));
                    //DicBitMap_Vertical[temScaleView] = NewScalBitamp;
                    NewScalBitamp.Save("d:\\" + this.strFileName_vertical + "_" + temScaleView.ToString() + ".bmp");
                }

                for (int temScaleView = 4; temScaleView <= 40; temScaleView = temScaleView + 2)
                {
                    Bitmap NewScalBitamp = new Bitmap(OriginalBitmap_Horizontal, new Size(OriginalBitmap_Horizontal.Width / temScaleView, OriginalBitmap_Horizontal.Height / temScaleView));
                    //DicBitMap_Horizontal[temScaleView] = NewScalBitamp;
                    NewScalBitamp.Save("d:\\" + this.strFileName_Horizontal + "_" + temScaleView.ToString() + ".bmp");
                }


                //显示比例为1：10的左上角坐标为00的图片
                Display_BMP_Vertical(X_Locked, Y_Locked);
                Display_BMP_Horizontal(X_Locked, Y_Locked);

            }
            catch (Exception ex)
            {
            }
        }


        //数据显示按钮按下
        private void cmd_DisplayImage_Click(object sender, EventArgs e)
        {
            try
            {
                int X_Locked = 0;
                int Y_Locked = 0;
                int Z_Locked = 0;
                Cal_X_Y_Z(ref X_Locked, ref Y_Locked, ref Z_Locked);
                Display_BMP_Vertical(X_Locked, Y_Locked);
                Display_BMP_Horizontal(X_Locked, Y_Locked);
            }
            catch (Exception ex)
            {
            }
        }


        //鼠标移动
        void pictureBox_Vertical_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.X > 0 && e.Y > 0 && e.X < this.pictureBox_Vertical.Width && e.Y < this.pictureBox_Vertical.Height)
                {
                    this.conLine_Vertical_Y.Width = 1;
                    this.conLine_Vertical_Y.Top = 0;
                    this.conLine_Vertical_Y.Height = this.pictureBox_Vertical.Height;
                    this.conLine_Vertical_Y.Left = e.X ;
                    try
                    {
                        this.conLine_Vertical_X.Width = this.pictureBox_Vertical.Width;
                        this.conLine_Vertical_X.Height = 1;
                        this.conLine_Vertical_X.Top = e.Y;
                        this.conLine_Vertical_X.Left = 0;
                    }
                    catch (Exception ex)
                    {
                    }

                    // the other

                    this.conLine_Horizontal_Y.Width = 1;
                    this.conLine_Horizontal_Y.Top = 0;
                    this.conLine_Horizontal_Y.Height = this.pictureBox_Horizontal.Height;
                    this.conLine_Horizontal_Y.Left = e.X ;

                    int X = 0;
                    int Y = 0;
                    int Z = 0;
                    Cal_X_Y_Z(ref X, ref Y,ref Z);
                    txt_X.Text = X.ToString();
                    txt_Y.Text = Y.ToString();
                    txt_Z.Text = Z.ToString();

                }
            }
            catch (Exception ex)
            {
            }
        }

        //鼠标移动
        void pictureBox_Horizontal_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.X > 0 && e.Y > 0 && e.X < this.pictureBox_Horizontal.Width && e.Y < this.pictureBox_Horizontal.Height)
                {
                    this.conLine_Horizontal_Y.Width = 1;
                    this.conLine_Horizontal_Y.Top = 0;
                    this.conLine_Horizontal_Y.Height = this.pictureBox_Horizontal.Height;
                    this.conLine_Horizontal_Y.Left = e.X ;

                    //the other

                    this.conLine_Vertical_Y.Width = 1;
                    this.conLine_Vertical_Y.Top = 0;
                    this.conLine_Vertical_Y.Height = this.pictureBox_Vertical.Height;
                    this.conLine_Vertical_Y.Left = e.X;

                    this.conLine_Vertical_X.Width = this.pictureBox_Vertical.Width;
                    this.conLine_Vertical_X.Height = 1;
                    //this.conLine_Vertical_X.Top = e.Y;
                    this.conLine_Vertical_X.Left = 0;

                    int X = 0;
                    int Y = 0;
                    int Z = 0;
                    Cal_X_Y_Z(ref X, ref Y,ref Z);
                    txt_X.Text = X.ToString();
                    txt_Y.Text = Y.ToString();
                    txt_Z.Text = Z.ToString();
                }
            }
            catch (Exception ex)
            {
            }
        }

        //按照输入的XY的相对坐标 计算XY的决定坐标
        private void Cal_X_Y_Z(ref int X, ref int Y,ref int Z)
        {
            try
            {
                //X = Convert.ToInt32(this.LaserPointMap.X_Min + (this.LaserPointMap.X_Max - this.LaserPointMap.X_Min) / 2);
                X = this.XStart_Bmp_Vertical+conLine_Vertical_X.Top * ScaleView;
                Y = Convert.ToInt32(this.YStart_Bmp + conLine_Vertical_Y.Left * ScaleView);
                Z = this.LaserPointMap.GetSampleZ(Y);
            }
            catch (Exception ex)
            {
            }
        }

        //放大
        private void cmd_Zoom_IN_Click(object sender, EventArgs e)
        {
            try
            {
                int X_Locked = 0;
                int Y_Locked = 0;
                int Z_Locked = 0;
                Cal_X_Y_Z(ref X_Locked, ref Y_Locked,ref Z_Locked);

                ScaleView = ScaleView-2;
                if (ScaleView <= 4)
                {
                    ScaleView = 4;
                }
                Display_BMP_Vertical(X_Locked, Y_Locked);
                Display_BMP_Horizontal(X_Locked, Y_Locked);

            }
            catch (Exception ex)
            {
            }
        }

        //缩小
        private void cmd_Zoom_Out_Click(object sender, EventArgs e)
        {
            try
            {
                int X_Locked = 0;
                int Y_Locked = 0;
                int Z_Locked = 0;
                Cal_X_Y_Z(ref X_Locked, ref Y_Locked,ref Z_Locked);

                ScaleView = ScaleView+2;
                if (ScaleView >= 40)
                {
                    ScaleView = 40;
                }
                Display_BMP_Vertical(X_Locked, Y_Locked);
                Display_BMP_Horizontal(X_Locked, Y_Locked);
            }
            catch (Exception ex)
            {
            }
        }

        //显示垂直视角的图片  输入参数为 锁定点的X和Y
        private void Display_BMP_Vertical(int X_Locked, int Y_Locked)
        {
            try
            {
                X_Locked = Convert.ToInt32(this.LaserPointMap.X_Min + (this.LaserPointMap.X_Max - this.LaserPointMap.X_Min) / 2);
                YStart_Bmp = Y_Locked - conLine_Vertical_Y.Left * ScaleView;
                // MessageBox.Show(YStart_Bmp_Vertical.ToString());
                XStart_Bmp_Vertical = X_Locked - Convert.ToInt32(pictureBox_Vertical.Height/2* ScaleView);

                //将原图按照现在的比例进行缩放
                //NewScalBitamp是按照比例进行缩放的完整的图
                //Bitmap NewScalBitamp = new Bitmap(OriginalBitmap_Vertical, new Size(OriginalBitmap_Vertical.Width / ScaleView, OriginalBitmap_Vertical.Height / ScaleView));
                Bitmap NewScalBitamp = new Bitmap("d:\\" + this.strFileName_vertical + "_" + ScaleView.ToString() + ".bmp");
                //Bitmap NewScalBitamp = DicBitMap_Vertical[ScaleView];
                //准备显示的新图
                //DisplayBitmap在按比例缩放的图片的基础上截取的部分图片
                Bitmap DisplayBitmap = new Bitmap(pictureBox_Vertical.Width, pictureBox_Vertical.Height);
                //新图绘图对象
                Graphics g = Graphics.FromImage(DisplayBitmap);

                //Left Top Height Width  均为缩放后图纸上位置和长度长度
                int Left = Convert.ToInt32((YStart_Bmp - LaserPointMap.Y_Min) / ScaleView); ;
                int Top = Convert.ToInt32((XStart_Bmp_Vertical - LaserPointMap.X_Min) / ScaleView);
                int Height = pictureBox_Vertical.Height * ScaleView;
                int Width = pictureBox_Vertical.Width * ScaleView;


                g.DrawImage(NewScalBitamp, 0, 0, new Rectangle(Left, Top, Width, Height), GraphicsUnit.Pixel);

                //DisplayBitmap.Save("d:\\display_vertical.bmp");
                //this.pictureBox_Vertical.Load("d:\\display_vertical.bmp");
                this.pictureBox_Vertical.Image = DisplayBitmap;


                g.Dispose();
                //OriginalBitmap.Dispose();
                NewScalBitamp.Dispose();
                //DisplayBitmap.Dispose();


                int X = 0;
                int Y = 0;
                int Z = 0;
                Cal_X_Y_Z(ref X, ref Y,ref Z);
                txt_X.Text = X.ToString();
                txt_Y.Text = Y.ToString();

            }
            catch (Exception ex)
            {
            }
        }

        private void Display_BMP_Horizontal(int X_Locked, int Y_Locked)
        {
            try
            {
                X_Locked = Convert.ToInt32(this.LaserPointMap.X_Min + (this.LaserPointMap.X_Max - this.LaserPointMap.X_Min) / 2);
                YStart_Bmp = Y_Locked - conLine_Vertical_Y.Left * ScaleView;
                XStart_Bmp_Horizontal = intZMax_BitMap / 2 + this.conLine_Horizontal_Y.Height / 2 * ScaleView;


                //将原图按照现在的比例进行缩放
                //NewScalBitamp是按照比例进行缩放的完整的图
                //Bitmap NewScalBitamp = new Bitmap(OriginalBitmap_Horizontal, new Size(OriginalBitmap_Horizontal.Width / ScaleView, OriginalBitmap_Horizontal.Height / ScaleView));
                Bitmap NewScalBitamp = new Bitmap("d:\\" + this.strFileName_Horizontal + "_" + ScaleView.ToString() + ".bmp");
                //Bitmap NewScalBitamp = DicBitMap_Horizontal[ScaleView];
                //准备显示的新图
                //DisplayBitmap在按比例缩放的图片的基础上截取的部分图片
                Bitmap DisplayBitmap = new Bitmap(pictureBox_Horizontal.Width, pictureBox_Horizontal.Height);
                //新图绘图对象
                Graphics g = Graphics.FromImage(DisplayBitmap);

                //Left Top Height Width  均为缩放后图纸上位置和长度长度
                int Left = Convert.ToInt32((YStart_Bmp - LaserPointMap.Y_Min) / ScaleView); ;
                int Top = Convert.ToInt32((intZMax_BitMap - XStart_Bmp_Horizontal) / ScaleView);
                int Height = pictureBox_Vertical.Height * ScaleView;
                int Width = pictureBox_Vertical.Width * ScaleView;


                g.DrawImage(NewScalBitamp, 0, 0, new Rectangle(Left, Top, Width, Height), GraphicsUnit.Pixel);

                //DisplayBitmap.Save("d:\\display_horizontal.bmp");
                //this.pictureBox_Horizontal.Load("d:\\display_horizontal.bmp");
                this.pictureBox_Horizontal.Image = DisplayBitmap;

                g.Dispose();
                //OriginalBitmap.Dispose();
                NewScalBitamp.Dispose();
                //DisplayBitmap.Dispose();


                int X = 0;
                int Y = 0;
                int Z = 0;
                Cal_X_Y_Z(ref X, ref Y,ref Z);
                txt_X.Text = X.ToString();
                txt_Y.Text = Y.ToString();

            }
            catch (Exception ex)
            {
            }
        }

        private void frm_View_All_II_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                OriginalBitmap_Vertical.Dispose();
                OriginalBitmap_Horizontal.Dispose();
            }
            catch (Exception ex)
            {
            }
        }


    }
}
