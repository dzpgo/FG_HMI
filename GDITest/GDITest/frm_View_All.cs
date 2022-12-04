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
    public partial class frm_View_All : Form
    {
        public frm_View_All()
        {
            InitializeComponent();
        }


        clsLaserPointsMap LaserPointMap = new clsLaserPointsMap();
        private int ScaleView = 10;  //默认按照1：10的比列去显示

        private int XStart_Bmp_Vertical = 0;
        private int YStart_Bmp_Vertical = 0;

        private int XStart_Bmp_Horizontal = 0;
        private int YStart_Bmp_Horizontal = 0;


        Bitmap OriginalBitmap_Vertical = new Bitmap(1,1);
        Bitmap OriginalBitmap_Horizontal = new Bitmap(1,1);

        private void frm_View_All_Load(object sender, EventArgs e)
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

                OriginalBitmap_Vertical.Save("d:\\original_vertical.bmp");

             


                //销毁绘图的对象
                myBrush.Dispose();

                bitmapGraphics.Dispose();

             
                //displayBitmap.Dispose();

                XStart_Bmp_Vertical =Convert.ToInt32( LaserPointMap.X_Min); ;
                YStart_Bmp_Vertical =Convert.ToInt32( LaserPointMap.Y_Min);

                XStart_Bmp_Horizontal =Convert.ToInt32( LaserPointMap.X_Min);
                YStart_Bmp_Horizontal =Convert.ToInt32( LaserPointMap.Y_Min);

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

                OriginalBitmap_Horizontal.Save("d:\\original_horizontal.bmp");

             
                //销毁绘图的对象
                myBrush.Dispose();

                bitmapGraphics.Dispose();

                //localBitmap.Dispose();

                //displayBitmap.Dispose();


            }
            catch (Exception ex)
            {
            }
        }

        private void cmd_TreatData_Click(object sender, EventArgs e)
        {
            try
            {
                ScaleView = 10;

                this.GetData();
                Create_Original_Bmp_Vertical();
                Create_Original_Bmp_Horizontal();


                int X_Locked = 0;
                int Y_Locked = 0;
                Cal_X_Y(ref X_Locked, ref Y_Locked);
                Display_BMP_Vertical(X_Locked, Y_Locked);
                Display_BMP_Horizontal(X_Locked, Y_Locked);



                //MessageBox.Show("数据处理完成");
            }
            catch (Exception ex)
            {
            }
        }



        private void cmd_DisplayImage_Click(object sender, EventArgs e)
        {
            try
            {
                this.pictureBox_Vertical.Load("d:\\display_vertical.bmp");
            }
            catch (Exception ex)
            {
            }
        }



        void pictureBox_Vertical_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.X > 0 && e.Y > 0 && e.X < this.pictureBox_Vertical.Width && e.Y < this.pictureBox_Vertical.Height)
                {
                    this.conLine_Vertical.Width = 2;
                    this.conLine_Vertical.Top = 0;
                    this.conLine_Vertical.Height = this.pictureBox_Vertical.Height;
                    this.conLine_Vertical.Left = e.X - 1;

                    // the other

                    this.conLine_Horizontal.Width = 2;
                    this.conLine_Horizontal.Top = 0;
                    this.conLine_Horizontal.Height = this.pictureBox_Horizontal.Height;
                    this.conLine_Horizontal.Left = e.X - 1;

                    int X = 0;
                    int Y = 0;
                    Cal_X_Y(ref X,ref Y);
                    txt_X.Text = X.ToString();
                    txt_Y.Text = Y.ToString();

                }
            }
            catch (Exception ex)
            {
            }
        }

        void pictureBox_Horizontal_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.X > 0 && e.Y > 0 && e.X < this.pictureBox_Horizontal.Width && e.Y < this.pictureBox_Horizontal.Height)
                {
                    this.conLine_Horizontal.Width = 2;
                    this.conLine_Horizontal.Top = 0;
                    this.conLine_Horizontal.Height = this.pictureBox_Horizontal.Height;
                    this.conLine_Horizontal.Left = e.X - 1;

                    //the other

                    this.conLine_Vertical.Width = 2;
                    this.conLine_Vertical.Top = 0;
                    this.conLine_Vertical.Height = this.pictureBox_Vertical.Height;
                    this.conLine_Vertical.Left = e.X - 1;

                    int X = 0;
                    int Y = 0;
                    Cal_X_Y(ref X, ref Y);
                    txt_X.Text = X.ToString();
                    txt_Y.Text = Y.ToString();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void Cal_X_Y(ref int X,ref int Y)
        {
            try
            {
                X = Convert.ToInt32(this.LaserPointMap.X_Min+(this.LaserPointMap.X_Max-this.LaserPointMap.X_Min)/2);
                Y = Convert.ToInt32(this.YStart_Bmp_Vertical + conLine_Vertical.Left * ScaleView);
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
                Cal_X_Y(ref X_Locked, ref Y_Locked);

                ScaleView--;
                if (ScaleView <= 1)
                {
                    ScaleView = 1;
                }
                Display_BMP_Vertical(X_Locked, Y_Locked);
                Display_BMP_Horizontal(X_Locked, Y_Locked);

            }
            catch (Exception ex)
            {
            }
        }

        private void cmd_Zoom_Out_Click(object sender, EventArgs e)
        {
            try
            {
                int X_Locked = 0;
                int Y_Locked = 0;
                Cal_X_Y(ref X_Locked, ref Y_Locked);

                ScaleView++;
                if (ScaleView <= 1)
                {
                    ScaleView = 1;
                }
                Display_BMP_Vertical(X_Locked, Y_Locked);
                Display_BMP_Horizontal(X_Locked, Y_Locked);
            }
            catch (Exception ex)
            {
            }
        }

        private void Display_BMP_Vertical(int X_Locked, int Y_Locked)
        {
            try
            {

                YStart_Bmp_Vertical = Y_Locked - conLine_Vertical.Left * ScaleView;
               // MessageBox.Show(YStart_Bmp_Vertical.ToString());
                XStart_Bmp_Vertical = X_Locked - conLine_Vertical.Height / 2 * ScaleView;

                //将原图按照现在的比例进行缩放
                Bitmap NewScalBitamp = new Bitmap(OriginalBitmap_Vertical, new Size(OriginalBitmap_Vertical.Width / ScaleView, OriginalBitmap_Vertical.Height / ScaleView));
                //准备显示的新图
                Bitmap DisplayBitmap = new Bitmap(pictureBox_Vertical.Width, pictureBox_Vertical.Height);
                //新图绘图对象
                Graphics g = Graphics.FromImage(DisplayBitmap);

                //Left Top Height Width  均为缩放后图纸上位置和长度长度
                int Left = Convert.ToInt32((YStart_Bmp_Vertical - LaserPointMap.Y_Min) / ScaleView); ;
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
                Cal_X_Y(ref X, ref Y);
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

                YStart_Bmp_Vertical = Y_Locked - conLine_Vertical.Left * ScaleView;
                XStart_Bmp_Vertical = intZMax_BitMap/2 + conLine_Vertical.Height / 2 * ScaleView;


                //将原图按照现在的比例进行缩放
                Bitmap NewScalBitamp = new Bitmap(OriginalBitmap_Horizontal, new Size(OriginalBitmap_Horizontal.Width / ScaleView, OriginalBitmap_Horizontal.Height / ScaleView));
                //准备显示的新图
                Bitmap DisplayBitmap = new Bitmap(pictureBox_Horizontal.Width, pictureBox_Horizontal.Height);
                //新图绘图对象
                Graphics g = Graphics.FromImage(DisplayBitmap);

                //Left Top Height Width  均为缩放后图纸上位置和长度长度
                int Left = Convert.ToInt32((YStart_Bmp_Vertical - LaserPointMap.Y_Min) / ScaleView); ;
                int Top = Convert.ToInt32((intZMax_BitMap-XStart_Bmp_Vertical) / ScaleView);
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
                Cal_X_Y(ref X, ref Y);
                txt_X.Text = X.ToString();
                txt_Y.Text = Y.ToString();

            }
            catch (Exception ex)
            {
            }
        }

        private void frm_View_All_FormClosing(object sender, FormClosingEventArgs e)
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
