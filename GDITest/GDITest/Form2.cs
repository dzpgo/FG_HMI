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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void cmdDisPlay_Click(object sender, EventArgs e)
        {
            //Graphics g = e.Graphics;
            Graphics g = panelGraphic.CreateGraphics();
            g.Clear(Color.White);


            Bitmap localBitmap = new Bitmap(panelGraphic.Width, panelGraphic.Height);

            Graphics bitmapGraphics = Graphics.FromImage(localBitmap);

            Random pixel = new Random();

            int intXStep = 30;//50mm一条数据线
            int intYStep = 50;//50mm一条数据线
            int intCarLength=12000;//车长12米
            int intCarWidth=3500;//车宽5米

            double scale = Convert.ToDouble(panelGraphic.Width) / Convert.ToDouble(intCarLength);//比列


            Color blue = Color.FromArgb(0, 0, 255);
            SolidBrush myBrush = new SolidBrush(blue); 

            int x = 0;
            double angle=0;
            while (x < intCarLength)
            {
                int y = 0;

                int alpha = 25;
                if (Math.Sin(angle) < 0)
                {
                    alpha = Convert.ToInt32(Math.Abs( 255 * Math.Sin(angle/3.1415926)));
                }
               // alpha = Convert.ToInt32(255+255 * Math.Sin(angle));
                if (alpha <= 25) { alpha = 25; }

                while (y < intCarWidth)
                {
                    //设置 Bitmap 对象中指定像素的颜色,构造函数参数为x,y,color
                    //localBitmap.SetPixel(Convert.ToInt32(x * scale), Convert.ToInt32(y * scale), System.Drawing.Color.FromArgb(255, blue));
                    myBrush.Color = Color.FromArgb(alpha, blue);
                    bitmapGraphics.FillRectangle(myBrush, Convert.ToInt32(x * scale)-2, Convert.ToInt32(y * scale)-2,
                                                          2,  2);
                    y = y + intYStep;
                }

                x = x + intXStep;

                angle=angle+0.1;
                if(angle>=360)
                {
                    angle=0;
                }
            }

            //把在内存里处理的bitmap推向前台并显示

            g.DrawImage(localBitmap, 0, 0);


            myBrush.Dispose();

            bitmapGraphics.Dispose();

            localBitmap.Dispose();

            g.Dispose();
        }
    }
}
