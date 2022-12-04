using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UACSDAL;

namespace UACSControls
{
    public partial class conTruckStowage : UserControl
    {
        public conTruckStowage()
        {
            InitializeComponent();
            //this.SetStyle(
            //    ControlStyles.OptimizedDoubleBuffer |
            //    ControlStyles.ResizeRedraw |
            //    ControlStyles.AllPaintingInWmPaint, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_carDirection">车头方向</param>
        /// <param name="_listTruck">配载图信息</param>
        public delegate void dgeLoadTruckStowage(string _carDirection, int _CarType, List<TruckStowageClass> _listTruck);

        public void DrawTruckStowage(string carDirection, int _CarType, List<TruckStowageClass> _listTruck)
        {
            //控件清除不干净
            ClearCarSaddle();
            ClearCarSaddle();
            ClearCarSaddle();
            ClearCarSaddle();

            //ClearControl();
            this.Paint += conTruckStowage_Paint;


            //车辆类型不是框架类型不显示
            if (_CarType != 100)
                return;

            _carDirection = carDirection;
                     
            panel1.Paint += panel1_Paint;


            //判断装卷方式(成品库)
             List<int> XArray = _listTruck.Select(a => a.XCenter).ToList();
            //XArray = XArray.Distinct().ToArray();
            XArray = BubbleSort(XArray);
            int count = 1;
            if (XArray.Count() > 1)
            {
                if (JugdeSaddleDiff(XArray))//单排装
                {
                    foreach (var item in _listTruck)
                    {
                        //坐标 --- X      dicCarX[item.GrooveId] - 10
                        //     --- Y      (panel1.Height - (panel1.Height / 10 * 2)) / 3 + (panel1.Height / 10)

                        //大小 --- width  Convert.ToInt32(mean) + 20
                        //     --- height (panel1.Height - (panel1.Height / 10 * 2)) / 3 - (panel1.Height / 10)

                        //Rectangle rec = new Rectangle(new Point(dicCarX[item.GrooveId] - 10, (panel1.Height - (panel1.Height / 10 * 2)) / 3 + (panel1.Height / 10)),
                        //    new Size(Convert.ToInt32(mean) + 20, (panel1.Height - (panel1.Height / 10 * 2)) / 3 - (panel1.Height / 10)));
                        //gr.FillRectangle(Brushes.Black, rec);

                        if (dicCarX.ContainsKey(item.GrooveId))
                        {
                            conCarSaddle theSaddleVisual = new conCarSaddle();
                            theSaddleVisual.Name = item.CoilNo;
                            theSaddleVisual.Click += new EventHandler(conCtrl_click);
                            panel1.Controls.Add(theSaddleVisual);
                            //传入参数 -- 装卷方式、车上鞍座的位置
                            if (isCarDir)
                            {
                                //theSaddleVisual.refreshControl(dicCarX[item.GrooveId] - 10, (panel1.Height - (panel1.Height / 10 * 2)) / 3 + (panel1.Height / 10),
                                //Convert.ToInt32(mean) + 20, (panel1.Height - (panel1.Height / 10 * 2)) / 3 - (panel1.Height / 10), item);
                                theSaddleVisual.refreshControl(dicCarX[item.GrooveId] - 10, (panel1.Height - (panel1.Height / 10 * 2)) / 3 + (panel1.Height / 10),
                                Convert.ToInt32(mean) + 20, (panel1.Height - (panel1.Height / 10 * 2)) / 3 - (panel1.Height / 10), item);   
                            }
                            else
                            {

                                theSaddleVisual.refreshControl(dicCarX[ShiftNum(item.GrooveId)] - 10, (panel1.Height - (panel1.Height / 10 * 2)) / 3 + (panel1.Height / 10),
                                Convert.ToInt32(mean) + 20, (panel1.Height - (panel1.Height / 10 * 2)) / 3 - (panel1.Height / 10), item);
                            }
                        }            
                        count++;
                    }
                }
                else   //双排装
                {
                    foreach (var item in _listTruck)
                    {
                        conCarSaddle theSaddleVisual = new conCarSaddle();
                        theSaddleVisual.Name = item.CoilNo;
                        theSaddleVisual.Click += new EventHandler(conCtrl_click);
                        panel1.Controls.Add(theSaddleVisual);
                        //传入参数 -- 装卷方式、车上鞍座的位置
                        if (isCarDir)
                        {
                            //怎么判断双排卷
                            //1 必须出来中心线 -- 用最大的Y减去最小 加上最小  
                            //2 判断X大还是小，判断y是否相同

                            if (true)
                            {
                                theSaddleVisual.refreshControl(dicCarX[item.GrooveId] - 10, (panel1.Height - (panel1.Height / 10 * 2)) / 5 + (panel1.Height / 10),
                                Convert.ToInt32(mean) + 20, (panel1.Height - (panel1.Height / 10 * 2)) / 5 - (panel1.Height / 10), item);   
                            }
                            
                            theSaddleVisual.refreshControl(dicCarX[item.GrooveId] - 10, (panel1.Height - (panel1.Height / 10 * 2)) / 3 + (panel1.Height / 10),
                           Convert.ToInt32(mean) + 20, (panel1.Height - (panel1.Height / 10 * 2)) / 3 - (panel1.Height / 10), item);    
                       
                        }
                        else
                        {
                            theSaddleVisual.refreshControl(dicCarX[ShiftNum(item.GrooveId)] - 10, (panel1.Height - (panel1.Height / 10 * 2)) / 3 + (panel1.Height / 10),
                            Convert.ToInt32(mean) + 20, (panel1.Height - (panel1.Height / 10 * 2)) / 3 - (panel1.Height / 10), item);
                        }
                        count++;
                    }
                }
            }
           
        }

        public delegate void BtnClickHandle(object sender, EventArgs e);
        public event BtnClickHandle UserControlBtnClicked;
        private bool isCarDir = false;

        private void conCtrl_click(object sender, EventArgs e)
        {
            if (UserControlBtnClicked != null)
                UserControlBtnClicked(sender, new EventArgs());//把控件自身作为参数传递
        }


        void conTruckStowage_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = this.CreateGraphics();
            //step 1 判断车头方向
            if (_carDirection == "N" || _carDirection == "S")
            {
                g.DrawString("北", new Font("微软雅黑", 12, FontStyle.Regular), Brushes.Red, new Point(1, this.Height - 20));
                g.DrawString("南", new Font("微软雅黑", 12, FontStyle.Regular), Brushes.Red, new Point(this.Width - 20, this.Height - 20));
                if (_carDirection == "N")
                    g.DrawString("头", new Font("微软雅黑", 12, FontStyle.Regular), Brushes.Red, new Point(1, this.Height / 2));
                if (_carDirection == "S")
                    g.DrawString("头", new Font("微软雅黑", 12, FontStyle.Regular), Brushes.Red, new Point(this.Width - 20, this.Height / 2));
            }
            if (_carDirection == "W" || _carDirection == "E")
            {
                g.DrawString("西", new Font("微软雅黑", 12, FontStyle.Regular), Brushes.Red, new Point(1, this.Height - 20));
                g.DrawString("东", new Font("微软雅黑", 12, FontStyle.Regular), Brushes.Red, new Point(this.Width - 20, this.Height - 20));
                if (_carDirection == "W")
                {
                    g.DrawString("头", new Font("微软雅黑", 12, FontStyle.Regular), Brushes.Red, new Point(1, this.Height / 2));
                    isCarDir = false;
                }
                if (_carDirection == "E")
                {
                    g.DrawString("头", new Font("微软雅黑", 12, FontStyle.Regular), Brushes.Red, new Point(this.Width - 20, this.Height / 2));
                    isCarDir = true;
                }
            }
        }

        void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics gr = panel1.CreateGraphics();
            mean = panel1.Width / 16;
            int X = Convert.ToInt32(mean / 2);
            int grooveId = 8;
            dicCarX.Clear();
            do
            {
                dicCarX[grooveId] = X;
                Rectangle rec = new Rectangle(new Point(X, panel1.Height / 10), 
                    new Size(Convert.ToInt32(mean), panel1.Height - (panel1.Height / 10 * 2)));
                gr.FillRectangle(Brushes.White, rec);
                X += Convert.ToInt32(mean) * 2;
                grooveId--;

            } while (panel1.Width > X);
        }


        Dictionary<int, int> dicCarX = new Dictionary<int, int>();
        double mean;
        string _carDirection;
        private void ClearControl()
        {
            using (Graphics g = this.CreateGraphics())
            {
                g.Clear(this.BackColor);
            }
            using (Graphics gr = panel1.CreateGraphics())
            {
                gr.Clear(this.BackColor);
            }
        }

        private void ClearCarSaddle()
        {
            foreach (Control ctl in this.panel1.Controls)
            {
                if (ctl is conCarSaddle)
                {
                    this.panel1.Controls.Remove(ctl);
                }
            }
        }

        static List<int> BubbleSort(List<int> list)
        {
            int temp;
            //第一层循环： 表明要比较的次数，比如list.count个数，肯定要比较count-1次
            for (int i = 0; i < list.Count - 1; i++)
            {
                //list.count-1：取数据最后一个数下标，
                //j>i: 从后往前的的下标一定大于从前往后的下标，否则就超越了。
                for (int j = list.Count - 1; j > i; j--)
                {
                    //如果前面一个数大于后面一个数则交换
                    if (list[j - 1] > list[j])
                    {
                        temp = list[j - 1];
                        list[j - 1] = list[j];
                        list[j] = temp;
                    }
                }
            }
            return list;
        }

        static bool JugdeSaddleDiff(List<int> list)
        {
            int min = list[0];
            int max = list[list.Count - 1];
            if (max - min < 400)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static int ShiftNum(int index)
        {
            if (index == 1)
                return 8;
            else if (index == 2)
                return 7;
            else if (index == 3)
                return 6;
            else if (index == 4)
                return 5;
            else if (index == 5)
                return 4;
            else if (index == 6)
                return 3;
            else if (index == 7)
                return 2;
            else if (index == 8)
                return 1;
            else
                return index;
        }
    }
}
