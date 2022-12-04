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
    public partial class frmView_Vertical : Form
    {
        public frmView_Vertical()
        {
            InitializeComponent();
        }


        clsLaserPointsMap LaserPointMap = new clsLaserPointsMap();

        private void cmdDisplay_Click(object sender, EventArgs e)
        {
            try
            {
                //拿到数据
                LaserPointMap.GetData_Txt();
                //得到车长
                long intCarLength = Math.Abs(LaserPointMap.Y_Max - LaserPointMap.Y_Min);
                //得到车宽
                long intCarWidth = Math.Abs(LaserPointMap.X_Max - LaserPointMap.X_Min);
                //按照车长计算出比列
                double scale = Convert.ToDouble(panelGraphic_Vertical.Width) / Convert.ToDouble(intCarLength);//比列

                //获得高度的有效范围
                long intCarHeight = Math.Abs(LaserPointMap.Z_Max - LaserPointMap.Z_Min);
                //按照高度，取得高度上aloha的换算比例
                double scale_Alpha = Convert.ToDouble(intCarHeight) / Convert.ToDouble(255);

                //按照比例，计算出车宽方向显示区域的大小 调整显示区域的大小
                panelGraphic_Vertical.Height = Convert.ToInt32(intCarWidth * scale);

                //取得绘图区域的图形对象，并且清空该区域
                Graphics g = panelGraphic_Vertical.CreateGraphics();
                g.Clear(Color.White);

                //在内存里面建立一个位图，并且建立该位图的图形对象

                Bitmap localBitmap = new Bitmap(panelGraphic_Vertical.Width + 10, panelGraphic_Vertical.Height+10);

                Graphics bitmapGraphics = Graphics.FromImage(localBitmap);

                //定义颜色和画笔
                Color blue = Color.FromArgb(0, 0, 255);
                SolidBrush myBrush = new SolidBrush(Color.Gray);

                foreach (clsLaserPoint thePoint in LaserPointMap.LaserPoints)
                {
                    long inZ_Realtive = LaserPointMap.Z_Max - thePoint.Z;
                    int alpha = Convert.ToInt32(inZ_Realtive / scale_Alpha);
                    if (alpha < 0) { alpha = 0; }
                    if (alpha > 255) { alpha = 255; }

                    myBrush.Color = Color.FromArgb(alpha, blue);
                    long Y_Realtive = thePoint.Y - LaserPointMap.Y_Min;
                    long X_Realtive = thePoint.X - LaserPointMap.X_Min;
                    int X_Graphics =Convert.ToInt32( Y_Realtive * scale);
                    int Y_Graphics = Convert.ToInt32(X_Realtive * scale);
                    bitmapGraphics.FillRectangle(myBrush, X_Graphics - 2, Y_Graphics - 2,
                                                          2, 2);
                }
                //localBitmap.RotateFlip(RotateFlipType.Rotate180FlipX);

                //把在内存里处理的bitmap推向前台并显示
                g.DrawImage(localBitmap, 0, 0);

                //销毁绘图的对象
                myBrush.Dispose();

                bitmapGraphics.Dispose();

                localBitmap.Dispose();

                g.Dispose();

            }
            catch (Exception ex)
            {
            }
        }

        private void panelGraphic_Vertical_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                return;
                if (e.X > 0 && e.Y > 0 && e.X<panelGraphic_Vertical.Width && e.Y<panelGraphic_Vertical.Height)
                {
                    conLine_Y.Width = 2;
                    conLine_Y.Top = 0;
                    conLine_Y.Height = panelGraphic_Vertical.Height;
                    conLine_Y.Left = e.X - 1;

                    //System.Threading.Thread.Sleep(50);

                    ////得到车长
                    //long intCarLength = Math.Abs(LaserPointMap.Y_Max - LaserPointMap.Y_Min);
                    ////得到车宽
                    //long intCarWidth = Math.Abs(LaserPointMap.X_Max - LaserPointMap.X_Min);
                    ////按照车长计算出比列
                    //double scale = Convert.ToDouble(panelGraphic_Vertical.Width) / Convert.ToDouble(intCarLength);//比列

                    ////获得高度的有效范围
                    //long intCarHeight = Math.Abs(LaserPointMap.Z_Max - LaserPointMap.Z_Min);
                    ////按照高度，取得高度上aloha的换算比例
                    //double scale_Alpha = Convert.ToDouble(intCarHeight) / Convert.ToDouble(255);

                    ////按照比例，计算出车宽方向显示区域的大小 调整显示区域的大小
                    //panelGraphic_Vertical.Height = Convert.ToInt32(intCarWidth * scale);

                    ////取得绘图区域的图形对象，并且清空该区域
                    //Graphics g = panelGraphic_Vertical.CreateGraphics();
                    ////g.Clear(Color.White);

                    ////在内存里面建立一个位图，并且建立该位图的图形对象

                    //Bitmap localBitmap = new Bitmap(panelGraphic_Vertical.Width + 10, panelGraphic_Vertical.Height + 10);

                    //Graphics bitmapGraphics = Graphics.FromImage(localBitmap);

                    ////定义颜色和画笔
                    //Color blue = Color.FromArgb(0, 0, 255);
                    //SolidBrush myBrush = new SolidBrush(blue);

                    //foreach (clsLaserPoint thePoint in LaserPointMap.LaserPoints)
                    //{
                    //    long inZ_Realtive = LaserPointMap.Z_Max - thePoint.Z;
                    //    int alpha = Convert.ToInt32(inZ_Realtive / scale_Alpha);
                    //    if (alpha < 0) { alpha = 0; }
                    //    if (alpha > 255) { alpha = 255; }

                    //    myBrush.Color = Color.FromArgb(alpha, blue);
                    //    long Y_Realtive = thePoint.Y - LaserPointMap.Y_Min;
                    //    long X_Realtive = thePoint.X - LaserPointMap.X_Min;
                    //    int X_Graphics = Convert.ToInt32(Y_Realtive * scale);
                    //    int Y_Graphics = Convert.ToInt32(X_Realtive * scale);
                    //    bitmapGraphics.FillRectangle(myBrush, X_Graphics - 2, Y_Graphics - 2,
                    //                                          2, 2);
                    //}
                    ////localBitmap.RotateFlip(RotateFlipType.Rotate180FlipX);

                    ////把在内存里处理的bitmap推向前台并显示
                    //g.DrawImage(localBitmap, 0, 0);

                    ////销毁绘图的对象
                    //myBrush.Dispose();

                    //bitmapGraphics.Dispose();

                    //localBitmap.Dispose();

                    //g.Dispose();





                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
