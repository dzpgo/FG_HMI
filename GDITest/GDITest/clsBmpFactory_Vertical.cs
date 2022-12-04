using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace GDITest
{
    public class clsBmpFactory_Vertical
    {
        //缩放比例
        private int scale = 10;
        public int Scale
        {
            get { return scale; }
            set { scale = value; }
        } 
        //比例的最小值，对应ZoonIn的极限
        private  int scaleMin = 8;

        //比例的最大值,对应ZoomOut的极限
        private int scaleMax = 26;

        //调整缩放比例--放大
        public int AdjustScale_ZoomIn()
        {
            try
            {
                scale = scale - 2;
                if (scale <= scaleMin)
                {
                    scale = scaleMin;
                }
            }
            catch (Exception ex)
            {
            }
            return scale;
        }
        //调整缩放比例--缩小
        public int AdjustScale_ZoomOut()
        {
            try
            {
                scale = scale + 2;
                if (scale >= scaleMax)
                {
                    scale = scaleMax;
                }
            }
            catch (Exception ex)
            {
            }
            return scale;
        }
        //图片左上角的实际坐标位置
        private int x_LeftTop = 0;
        public int X_LeftTop
        {
            get { return x_LeftTop; }
            set { x_LeftTop = value; }
        }
        private int y_LeftTop = 0;
        public int Y_LEftTop
        {
            get { return y_LeftTop; }
            set { y_LeftTop = value; }
        }

        //1:1图片的对象
        private Bitmap originalBitmap = new Bitmap(1, 1);

        //文件保存路径
        private string filePath = "z:\\ScanImage\\";
        public string FilePath
        {
            get { return filePath; }
            set { filePath = value; }
        }

        //文件名称
        private string fileName = "vertical";
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        //激光数据的存数对象
        private clsLaserPointsMap laserPointMap = new clsLaserPointsMap();
        public clsLaserPointsMap LaserPointMap
        {
            get { return laserPointMap; }
            set { laserPointMap = value; }
        }

        private int centerParkingX = 0;

        public int CenterParkingX
        {
            get { return centerParkingX; }
            set { centerParkingX = value; }
        }

        private int originalScal = 2;

        //产生原始1：1的图片
        public void Create_Original_Bmp()
        {
            Graphics g = null;
            SolidBrush brush = null;
            originalBitmap = null;
            try
            {
                //得到车长  决定的原始图片的宽度
                long intCarLength = Math.Abs(LaserPointMap.Y_Max - LaserPointMap.Y_Min);

                //得到车宽  决定了原始图片的高度
                long intCarWidth = Math.Abs(LaserPointMap.X_Max - LaserPointMap.X_Min);

                double scale = 2;

                //获得高度的有效范围  为了产生渐变的颜色
                long intCarHeight = Math.Abs(LaserPointMap.Z_Max - LaserPointMap.Z_Min);

                //按照高度，取得高度上aloha的换算比例
                double scale_Alpha = Convert.ToDouble(intCarHeight) / Convert.ToDouble(255);

                //在内存里面建立一个位图，并且建立该位图的图形对象
                //Bitmap tempBitmap = new Bitmap(Convert.ToInt32(intCarLength / scale) + 200, Convert.ToInt32(intCarWidth / scale) + 200);
                originalBitmap = new Bitmap(Convert.ToInt32(intCarLength / originalScal) + 200, Convert.ToInt32(intCarWidth / originalScal) + 200);

                g = Graphics.FromImage(originalBitmap);

                //定义颜色和画笔
                Color blue = Color.FromArgb(0, 0, 255);
                brush = new SolidBrush(Color.White);

                //打底色
                g.FillRectangle(brush, 0, 0, originalBitmap.Width, originalBitmap.Height);

                //绘图
                foreach (clsLaserPoint thePoint in LaserPointMap.LaserPoints)
                {
                    long inZ_Realtive = LaserPointMap.Z_Max -thePoint.Z;//从天花板到数据点的相对高度
                    int alpha = Convert.ToInt32((inZ_Realtive / scale_Alpha)*20);//从天花板向下车高的距离，系数为255，标识颜色最深
                    if (alpha < 0) { alpha = 0; }
                    if (alpha > 255) { alpha = 255; }

                    if (thePoint.Z < 1550)
                    {
                        brush.Color = Color.FromArgb(alpha, Color.Black);

                    }
                    else
                    {
                        brush.Color = Color.FromArgb(alpha, Color.Red);
                    }
                    long X_Realtive = thePoint.Y - LaserPointMap.Y_Min;     //取得本数据点到原点的相对距离
                    long Y_Realtive = thePoint.X - LaserPointMap.X_Min;     //取得本数据点到原点的相对距离
                    int X_Graphics = Convert.ToInt32(X_Realtive / originalScal);   //按照比例折算出图纸上的位置  比例是1：1
                    int Y_Graphics = Convert.ToInt32(Y_Realtive / originalScal);   //按照比例折算出图纸上的位置  比例是1：1
                    g.FillRectangle(brush, Convert.ToInt32(X_Graphics - 6/ originalScal), Convert.ToInt32(Y_Graphics - 6 / originalScal),   //在这个坐标点画一个1cm的颜色块
                                                         Convert.ToInt32(12 / originalScal), Convert.ToInt32(12 / originalScal));

                }
                {
                    brush = new SolidBrush(Color.Blue);
                    long lineWidth = Convert.ToInt32((LaserPointMap.Y_Max - LaserPointMap.Y_Min) / originalScal);
                    long lineHeight = Convert.ToInt32(12 / originalScal);
                    long X_Realtive = this.centerParkingX - LaserPointMap.X_Min;
                    long Y_Realtive = LaserPointMap.Y_Min + (LaserPointMap.Y_Max - LaserPointMap.Y_Min) / 2;
                    int X_Graphics = Convert.ToInt32(Y_Realtive / originalScal);
                    int Y_Graphics = Convert.ToInt32(X_Realtive / originalScal);
                    g.FillRectangle(brush, X_Graphics - lineWidth / 2,
                                            Y_Graphics - lineHeight / 2,
                                            lineWidth,
                                            lineHeight);

                    //originalBitmap = tempBitmap;
                    //MessageBox.Show("line \n" + (X_Graphics - lineHeight / 2).ToString() + "\n" + Y_Graphics.ToString() + "\n"
                    //                + "\n" + lineWidth.ToString() + "\n" + lineHeight.ToString());
                }
                //try to delete
                //try
                //{
                //    if (File.Exists(this.filePath + this.fileName + "_1" + ".bmp"))
                //    {
                //        File.Delete(this.filePath + this.fileName + "_1" + ".bmp");
                //    }
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show("can not delete file" + this.filePath + this.fileName + "_1" + ".bmp");
                //}
                ////try to save
                //try
                //{

                //    originalBitmap.Save(this.filePath + this.fileName + "_1" + ".bmp");
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show("can not save file" + this.filePath + this.fileName + "_1" + ".bmp");
                //}







            }
            catch (Exception ex)
            {
            }
            finally
            {
                //销毁绘图的对象
                brush.Dispose();

                g.Dispose();
            }
        }

        //产生各种比例的图片
        public void Create_DifferentScale_Bmps()
        {

            try
            {
                for (int temScaleView = this.scaleMin; temScaleView < this.scaleMax; temScaleView = temScaleView + 2)
                {
                    CopyImage(temScaleView);
                    //System.Threading.Thread ThreadCopyImage = new System.Threading.Thread(CopyImage);
                    //ThreadCopyImage.Start();
                    //System.Threading.Thread.Sleep(100);
                }
               
            }
            catch (Exception ex)
            {
            }
        }


        public void Create_DifferentScale_Bmps_ExceptCurrentScale()
        {
            try
            {
                for (int temScaleView = this.scaleMin; temScaleView < this.scaleMax; temScaleView = temScaleView + 2)
                {
                    if (this.Scale != temScaleView) { CopyImage(temScaleView); }
                }
            }
            catch (Exception ex)
            {
            }
        }
        public void CopyImage(int temScaleView)
        {
            try
            {
                try
                {
                    if (File.Exists(this.filePath + this.fileName + "_" + temScaleView.ToString() + ".bmp"))
                    {
                        File.Delete(this.filePath + this.fileName + "_" + temScaleView.ToString() + ".bmp");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("can not delete file" + this.filePath + this.fileName + "_" + temScaleView.ToString() + ".bmp");
                }
                //try to save
                try
                {
                    Bitmap NewScalBitamp = null;
                    {
                        NewScalBitamp = new Bitmap(originalBitmap, new Size(originalBitmap.Width / (temScaleView / originalScal), originalBitmap.Height / (temScaleView / originalScal)));
                    }
                    NewScalBitamp.Save(this.filePath + this.fileName + "_" + temScaleView.ToString() + ".bmp");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("can not save file" + this.filePath + this.fileName + "_" + temScaleView.ToString() + ".bmp" + "\n" + ex.Message);
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                
            }
        }


        public void Display_Bmp(PictureBox pictureBox)
        {
            try
            {
                //调出对应比例的图片
                Bitmap NewScalBitamp = new Bitmap(this.filePath + this.fileName + "_" + this.scale.ToString() + ".bmp");
                //准备显示的新图
                Bitmap DisplayBitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
                //新图绘图对象
                Graphics g = Graphics.FromImage(DisplayBitmap);

                //Left Top Height Width  均为缩放后图纸上位置和长度长度
                int Left = Convert.ToInt32((this.y_LeftTop - LaserPointMap.Y_Min) / this.scale); ;
                int Top = Convert.ToInt32((this.x_LeftTop - LaserPointMap.X_Min) / this.scale);
                int Height = pictureBox.Height * this.scale;
                int Width = pictureBox.Width * this.scale;


                g.DrawImage(NewScalBitamp, 0, 0, new Rectangle(Left, Top, Width, Height), GraphicsUnit.Pixel);

                //DisplayBitmap.Save("d:\\display_vertical.bmp");
                //this.pictureBox_Vertical.Load("d:\\display_vertical.bmp");
                pictureBox.Image = DisplayBitmap;


                g.Dispose();
                NewScalBitamp.Dispose();


            }
            catch (Exception ex)
            {
            }
        }


        public void drawPointSelected(int x,int y)
        {
            Graphics g = null;
            SolidBrush brush = null;
            //originalBitmap = null;
            try
            {
                

                g = Graphics.FromImage(originalBitmap);

                //定义颜色和画笔
                Color blue = Color.FromArgb(0, 0, 255);
                brush = new SolidBrush(Color.Black);

         
                {
                    brush = new SolidBrush(Color.Black);
                    
                    long Y_Realtive = x - LaserPointMap.X_Min;
                    long X_Realtive = y - LaserPointMap.Y_Min;
                    int X_Graphics = Convert.ToInt32(X_Realtive / originalScal);
                    int Y_Graphics = Convert.ToInt32(Y_Realtive / originalScal);
                    g.FillRectangle(brush, X_Graphics - 12 ,
                                            Y_Graphics - 12 ,
                                            24,
                                            24);

                    //originalBitmap = tempBitmap;
                    //MessageBox.Show("line \n" + (X_Graphics - lineHeight / 2).ToString() + "\n" + Y_Graphics.ToString() + "\n"
                    //                + "\n" + lineWidth.ToString() + "\n" + lineHeight.ToString());
                }
                //try to delete
                //try
                //{
                //    if (File.Exists(this.filePath + this.fileName + "_1" + ".bmp"))
                //    {
                //        File.Delete(this.filePath + this.fileName + "_1" + ".bmp");
                //    }
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show("can not delete file" + this.filePath + this.fileName + "_1" + ".bmp");
                //}
                ////try to save
                //try
                //{

                //    originalBitmap.Save(this.filePath + this.fileName + "_1" + ".bmp");
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show("can not save file" + this.filePath + this.fileName + "_1" + ".bmp");
                //}







            }
            catch (Exception ex)
            {
            }
            finally
            {
                //销毁绘图的对象
                brush.Dispose();

                g.Dispose();
            }
        }

        public void drawPointSelected_1(int x, int y)
        {
            Graphics g = null;
            SolidBrush brush = null;
            //originalBitmap = null;
            try
            {


                g = Graphics.FromImage(originalBitmap);

                //定义颜色和画笔
                Color blue = Color.FromArgb(0, 0, 255);
                brush = new SolidBrush(Color.Red);


                {
                    brush = new SolidBrush(Color.Red);

                    long X_Realtive = x - LaserPointMap.X_Min;
                    long Y_Realtive = y - LaserPointMap.Y_Min;
                    int X_Graphics = Convert.ToInt32(X_Realtive / originalScal);
                    int Y_Graphics = Convert.ToInt32(Y_Realtive / originalScal);
                    g.FillRectangle(brush, X_Graphics - 12,
                                            Y_Graphics - 12,
                                            24,
                                            24);

                    //originalBitmap = tempBitmap;
                    //MessageBox.Show("line \n" + (X_Graphics - lineHeight / 2).ToString() + "\n" + Y_Graphics.ToString() + "\n"
                    //                + "\n" + lineWidth.ToString() + "\n" + lineHeight.ToString());
                }
                //try to delete
                //try
                //{
                //    if (File.Exists(this.filePath + this.fileName + "_1" + ".bmp"))
                //    {
                //        File.Delete(this.filePath + this.fileName + "_1" + ".bmp");
                //    }
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show("can not delete file" + this.filePath + this.fileName + "_1" + ".bmp");
                //}
                ////try to save
                //try
                //{

                //    originalBitmap.Save(this.filePath + this.fileName + "_1" + ".bmp");
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show("can not save file" + this.filePath + this.fileName + "_1" + ".bmp");
                //}







            }
            catch (Exception ex)
            {
            }
            finally
            {
                //销毁绘图的对象
                brush.Dispose();

                g.Dispose();
            }
        }
    }
}
