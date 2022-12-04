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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            //Graphics g = e.Graphics;
            Graphics g = this.CreateGraphics();
            g.Clear(Color.White);
            //Pen bluePen = new Pen(Color.Blue);

            Bitmap localBitmap = new Bitmap(ClientRectangle.Width, ClientRectangle.Height);

            Graphics bitmapGraphics = Graphics.FromImage(localBitmap);

            //LineDrawRoutine(bitmapGraphics, bluePen);
            Random pixel = new Random();
            //创建一个随机实例
            for (int i = 0; i < 100000; i++)
            {
                localBitmap.SetPixel(pixel.Next(localBitmap.Width), pixel.Next(localBitmap.Height), Color.Red);
                //设置 Bitmap 对象中指定像素的颜色,构造函数参数为x,y,color
            }

            //把在内存里处理的bitmap推向前台并显示

            g.DrawImage(localBitmap, 0, 0);

            bitmapGraphics.Dispose();

            //bluePen.Dispose();

            localBitmap.Dispose();

            g.Dispose();
        }


    }
}
