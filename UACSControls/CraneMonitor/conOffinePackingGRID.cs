using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UACSDAL;
using UACSPopupForm;

namespace UACSControls
{
    public partial class conOffinePackingGRID : UserControl
    {
        public conOffinePackingGRID()
        {
            InitializeComponent();
        }


        private GRIDBase myGRIDInfo = new GRIDBase();
        public delegate void EventHandler_GRID_Selected(GRIDBase theGRIDInfo);
        public event EventHandler_GRID_Selected GRID_Selected;
        public void conInit()
        {
            try
            {
                this.panel1.MouseDown += new MouseEventHandler(conGRID_visual_MouseUp);

            }
            catch (Exception ex)
            {
            }
        }

        void conGRID_visual_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    if (GRID_Selected != null)
                    {
                        GRID_Selected(myGRIDInfo.Clone());
                    }
                }
                else
                {
           
                        //FrmGRIDMetail FrmGRIDDetail = new FrmGRIDMetail();
                        //FrmGRIDDetail.GRIDInfo = this.myGRIDInfo;
                        //FrmGRIDDetail.Show();
                }
            }
            catch (Exception ex)
            {
            }
        }

        public delegate void GRIDsRefreshInvoke(GRIDBase theGRID, long baySpaceX, long baySpaceY, int panelWidth, int panelHeight, bool xAxisRight, bool yAxisDown);

        public void refreshControl(GRIDBase theGRID, long baySpaceX, long baySpaceY, int panelWidth, int panelHeight, bool xAxisRight, bool yAxisDown)
        {
            myGRIDInfo = theGRID;
            try
            {
                this.Visible = true;
                this.Enabled = true;
                //计算X方向上的比例关系
                double xScale = Convert.ToDouble(panelWidth) / Convert.ToDouble(baySpaceX);

                //计算控件行车中心X，区分为X坐标轴向左或者向右
                double location_X = 0;
                if (xAxisRight == true)
                    location_X = Convert.ToDouble(theGRID.X_CENTER  / 2) * xScale;
                else
                    location_X = Convert.ToDouble(baySpaceX - (theGRID.X_CENTER / 2)) * xScale;


                //计算Y方向的比例关系
                double yScale = Convert.ToDouble(panelHeight) / Convert.ToDouble(baySpaceY);

                //计算行车中心Y 区分Y坐标轴向上或者向下
                double location_Y = 0;
                if (yAxisDown == true)
                    location_Y = (theGRID.Y_CENTER / 2) * yScale;
                else
                    location_Y = (baySpaceY - (theGRID.Y_CENTER / 2)) * yScale;

                //修改鞍座控件的宽度和高度
               
                    //this.Width = Convert.ToInt32(theGRID.GRIDWidth / 2 * xScale);
                    //this.Height = Convert.ToInt32(theGRID.GRIDLength / 2 * yScale);

                    //if (theGRID.Stock_Status == 0 && theGRID.Lock_Flag == 0) //无卷可用
                    //    this.BackColor = Color.White;
                    //else if (theGRID.Stock_Status == 2 && theGRID.Lock_Flag == 0) //有卷可用                
                    //    this.BackColor = Color.Black;
                    //else
                    //    this.BackColor = Color.Red;
               


                //定位库位鞍座的坐标
                this.Location = new Point(Convert.ToInt32(location_X), Convert.ToInt32(location_Y));

                toolTip1.IsBalloon = true;
                toolTip1.ReshowDelay = 0;
                toolTip1.SetToolTip(this, "料格号：" + theGRID.GRID_NO + "\r"
                                    + "料格名：    " + theGRID.GRID_NAME
                                    + "\r" + "物料代码：" + theGRID.MAT_CODE + "，钢制代码：" + theGRID.COMP_CODE + "，库存重量 单位-KG" + theGRID.MAT_WGT + "\r");
                                    //+ "坐标：" + "\r"
                                    //+ "X = " + theGRID.X_Center + "\r"
                                    //+ "Y = " + theGRID.Y_Center + "\r"
                                    //+ "下道机组： " + theGRID.Next_Unit_No + "\r");
            }
            catch (Exception ex)
            {
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                Graphics gr = e.Graphics;
                Pen p = new Pen(Color.White, 2);
                Rectangle rec = new Rectangle(new Point(0, 0), new Size(this.Width, this.Height));
                gr.DrawRectangle(p, rec);
            }
            catch (Exception er)
            {

            }
        }
    }
}
