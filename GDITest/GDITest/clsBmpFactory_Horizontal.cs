using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace GDITest
{
    public class clsBmpFactory_Horizontal
    {
        //缩放比例
        private int scale = 10;
        public int Scale
        {
            get { return scale; }
            set { scale = value; }
        }
        //比例的最小值，对应ZoonIn的极限
        private int scaleMin = 8;

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
        private int z_LeftTop = 0;
        public int Z_LeftTop
        {
            get { return z_LeftTop; }
            set { z_LeftTop = value; }
        }
        private int y_LeftTop = 0;
        public int Y_LEftTop
        {
            get { return y_LeftTop; }
            set { y_LeftTop = value; }
        }
        private int x_LeftTop = 0;
        public int X_LeftTop
        {
            get { return x_LeftTop; }
            set { x_LeftTop = value; }
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
        private string fileName = "horizontal";
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

        private Int32 zMax = 4000;//图片的默认高度为4m
        public Int32 ZMax
        {
            get { return zMax; }
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
                //得到车长
                long intCarLength = Math.Abs(LaserPointMap.Y_Max - LaserPointMap.Y_Min);
                //得到车宽
                long intCarWidth = Math.Abs(LaserPointMap.X_Max - LaserPointMap.X_Min);

                //获得高度的有效范围

                long intCarHeight = Math.Abs(LaserPointMap.Z_Max - LaserPointMap.Z_Min);
                //按照高度，取得高度上aloha的换算比例

                double scale_Alpha = Convert.ToDouble(intCarHeight) / Convert.ToDouble(255);


                //在内存里面建立一个位图，并且建立该位图的图形对象
                //显示的最大高度高定位4M
                //Bitmap tempBitmap=new Bitmap( Convert.ToInt32(intCarLength) + 200, Convert.ToInt32(zMax) + 200);
                originalBitmap = new Bitmap(Convert.ToInt32(intCarLength / originalScal) + 200, Convert.ToInt32(zMax / originalScal) + 200);

                g = Graphics.FromImage(originalBitmap);

                //定义颜色和画笔
                Color blue = Color.FromArgb(0, 0, 255);
                brush = new SolidBrush(Color.White);

                //打底色
                g.FillRectangle(brush, 0, 0, originalBitmap.Width, originalBitmap.Height);

                foreach (clsLaserPoint thePoint in LaserPointMap.LaserPoints)
                {
                    long inZ_Realtive = LaserPointMap.Z_Max - thePoint.Z;
                    int alpha = Convert.ToInt32((inZ_Realtive / scale_Alpha)*10);
                    if (alpha < 0) { alpha = 0; }
                    if (alpha > 255) { alpha = 255; }

                    brush.Color = Color.FromArgb(alpha, Color.Gray);
                    long X_Realtive = thePoint.Y - LaserPointMap.Y_Min;
                    int X_Graphics = Convert.ToInt32(X_Realtive / originalScal);
                    int Y_Graphics = Convert.ToInt32((zMax - thePoint.Z) / originalScal);
                    g.FillRectangle(brush, Convert.ToInt32(X_Graphics - 6 / originalScal), Convert.ToInt32(Y_Graphics - 6 / originalScal),
                                                          Convert.ToInt32(12 / originalScal), Convert.ToInt32(12 / originalScal));
                }
                //originalBitmap = tempBitmap;
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

                ////try to save file
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

        private int temScaleView = 0;


     
        //产生各种比例的图片
        public void Create_DifferentScale_Bmps()
        {
            try
            {
                for (temScaleView = this.scaleMin; temScaleView < this.scaleMax; temScaleView = temScaleView + 2)
                {
                    CopyImage();
                    //System.Threading.Thread ThreadCopyImage = new System.Threading.Thread(CopyImage);
                    //ThreadCopyImage.Start();
                    //System.Threading.Thread.Sleep(100);
                }

              
            }
            catch (Exception ex)
            {
            }
            
        }

   

        private void CopyImage()
        {
            
            try
            {

                //try to delete
                try
                {
                    if (File.Exists(this.filePath + this.fileName + "_" + this.temScaleView.ToString() + ".bmp"))
                    {
                        File.Delete(this.filePath + this.fileName + "_" + this.temScaleView.ToString() + ".bmp");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("can not delete file" + this.filePath + this.fileName + "_" + this.temScaleView.ToString() + ".bmp");
                }
                //try to save
                try
                {
                    Bitmap NewScalBitamp = null;
                    
                    {
                        NewScalBitamp = new Bitmap(originalBitmap, new Size(originalBitmap.Width / (this.temScaleView/2), originalBitmap.Height / (this.temScaleView/2) ) );
                    }
                    NewScalBitamp.Save(this.filePath + this.fileName + "_" + this.temScaleView.ToString() + ".bmp");

                }
                catch (Exception ex)
                {
                    MessageBox.Show("can not save file" + this.filePath + this.fileName + "_" + this.temScaleView.ToString() + ".bmp" + "\n" + ex.Message);
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
                int Top = Convert.ToInt32((zMax-this.z_LeftTop) / this.scale);
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


       



    }
}
