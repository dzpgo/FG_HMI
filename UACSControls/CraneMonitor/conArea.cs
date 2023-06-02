using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UACSDAL;
using Baosight.iSuperframe.Common;
using Baosight.iSuperframe.Authorization.Interface;
using UACSControls;

namespace UACSControls
{
    public partial class conArea : UserControl
    {
        //跳转画面
        private Baosight.iSuperframe.Authorization.Interface.IAuthorization auth;
        private Label lbl = new Label();
        private Label lbl2 = new Label();
        private AreaInfo areaInfo = new AreaInfo();
        private AreaBase areaBase = new AreaBase();
        private AreaRowInfo rowInfo = new AreaRowInfo();
        private static FrmSaddleShow frmSaddleShow = null;
        private bool isCreateLbl = false;
        private string lblRuler;
        private string lblCol;
        private double Inventory = 57;
        private double TotalInventory = 130;
        private string PercentageName = string.Empty;
        private bool StockPercentage = false;
        public conArea()
        {
            InitializeComponent();
            this.Load += conArea_Load;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var parms = base.CreateParams;
                parms.Style &= ~0x02000000; // Turn off WS_CLIPCHILDREN 
                return parms;
            }
        }
        void conArea_Load(object sender, EventArgs e)
        {
            auth = FrameContext.Instance.GetPlugin<IAuthorization>() as IAuthorization;
        }



        public delegate void areaRefreshInvoke(AreaBase theSaddle, long baySpaceX, long baySpaceY, int panelWidth, int panelHeight, bool xAxisRight, bool yAxisDown, Panel panel, conArea _conArea, bool stockPercentage, DataTable grids);

        public void refreshControl(AreaBase theSaddle, long baySpaceX, long baySpaceY, int panelWidth, int panelHeight, bool xAxisRight, bool yAxisDown, Panel panel, conArea _conArea, bool stockPercentage, DataTable grids)
        {
            try
            {
                areaBase = theSaddle;
                StockPercentage = stockPercentage;
                //计算X方向上的比例关系
                double xScale = Convert.ToDouble(panelWidth) / Convert.ToDouble(baySpaceX);

                double location_X = 0;
                if (xAxisRight == true)
                {
                    location_X = Convert.ToDouble(theSaddle.X_Start) * xScale;
                }
                else
                {
                    location_X = Convert.ToDouble(baySpaceX - (theSaddle.X_End)) * xScale;
                }
                //计算y方向上的比例关系
                double yScale = Convert.ToDouble(panelHeight) / Convert.ToDouble(baySpaceY);

                double location_Y = 0;
                if (yAxisDown == true)
                {
                    location_Y = Convert.ToDouble(theSaddle.Y_Start) * yScale;
                }
                else
                {
                    location_Y = Convert.ToDouble(baySpaceY - (theSaddle.Y_End)) * yScale;
                }
                if (location_Y < 0)
                {
                    location_Y = 0;
                }

                //定位库区的坐标
                this.Location = new Point(Convert.ToInt32(location_X), Convert.ToInt32(location_Y));

                //设置鞍座控件的宽度
                this.Width = Convert.ToInt32((theSaddle.X_End - theSaddle.X_Start) * xScale);
                //设置鞍座控件的高度
                if (theSaddle.AreaNo == "Z73-F" || theSaddle.AreaNo == "Z73-H")
                {
                    this.Height = Convert.ToInt32((theSaddle.Y_End - 0) * yScale);
                }
                else
                {
                    this.Height = Convert.ToInt32((theSaddle.Y_End - theSaddle.Y_Start) * yScale);
                    //if (theSaddle.AreaNo.Equals("A1"))
                    //{
                    //    var height1 = Convert.ToInt32((theSaddle.Y_End - theSaddle.Y_Start) * yScale);
                    //    var Percentage = (int)((Inventory / TotalInventory) * 100);
                    //    var percentageNum = (int)Percentage;
                    //    this.Text = Percentage + "%";
                    //    if (percentageNum >= 90)
                    //    {
                    //        PercentageName = (int)(100 - Percentage) + "%";
                    //    }
                    //    else
                    //    {
                    //        PercentageName = "";
                    //    }
                    //    this.Height = (int)((Percentage * height1) / 100);
                    //}
                }


                //当控件的宽小于1时 不显示控件
                if (this.Width < 1)
                {
                    this.Visible = false;
                }
                if (theSaddle.AreaType == 0)
                {
                    int saddleNum = areaInfo.getAreaSaddleNum(areaBase.AreaNo);
                    int saddleNoCoilNum = areaInfo.getAreaSaddleNoCoilNum(areaBase.AreaNo);
                    int saddleCoilNum = areaInfo.getAreaSaddleCoilNum(areaBase.AreaNo);

                    if (theSaddle.AreaNo.Contains("WJ"))
                    {
                        if (theSaddle.AreaDoorSefeValue == 1 && theSaddle.AreaDoorReserveValue == 0)
                            this.BackColor = System.Drawing.Color.Red;
                        else if (theSaddle.AreaDoorSefeValue == 1 && theSaddle.AreaDoorReserveValue == 1)
                            this.BackColor = System.Drawing.Color.Blue;
                        else if (theSaddle.AreaDoorSefeValue == 0 && theSaddle.AreaDoorReserveValue == 1)
                            this.BackColor = System.Drawing.Color.Yellow;
                        else
                        {
                            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(206)))), ((int)(((byte)(105)))));
                            //this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(171)))), ((int)(((byte)(199)))), ((int)(((byte)(227)))));  //System.Drawing.Color.MediumAquamarine;
                            //this.BackColor = System.Drawing.Color.FromArgb(53, 253, 220);
                        }
                    }
                    else
                    {
                        if (theSaddle.AreaDoorSefeValue == 1 && theSaddle.AreaDoorReserveValue == 0)
                            this.BackColor = System.Drawing.Color.Red;
                        else if (theSaddle.AreaDoorSefeValue == 1 && theSaddle.AreaDoorReserveValue == 1)
                            this.BackColor = System.Drawing.Color.Blue;
                        else if (theSaddle.AreaDoorSefeValue == 0 && theSaddle.AreaDoorReserveValue == 1)
                            this.BackColor = System.Drawing.Color.Yellow;
                        else
                        {
                            if (theSaddle.AreaReserve == 1)
                            {
                                this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(206)))), ((int)(((byte)(105)))));
                            }
                            else
                            {
                                this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(171)))), ((int)(((byte)(199)))), ((int)(((byte)(227)))));  //System.Drawing.Color.MediumAquamarine;
                            }
                            //if (theSaddle.AreaNo.Contains("A"))
                            //{
                            //    if (theSaddle.AreaSafe == 1)
                            //    {
                            //        this.BackColor = System.Drawing.Color.Tomato;
                            //    }
                            //}
                        }
                    }

                    if (!isCreateLbl)
                    {
                        if (!theSaddle.AreaNo.Contains("WJ") && !theSaddle.AreaNo.Contains("D108") && !theSaddle.AreaNo.Contains("Z68-B"))
                        {
                            lbl.Name = theSaddle.AreaNo;
                            //lbl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(206)))), ((int)(((byte)(105)))));
                            lbl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(171)))), ((int)(((byte)(199)))), ((int)(((byte)(227))))); //Color.MediumAquamarine;
                            lbl.Font = new System.Drawing.Font("微软雅黑", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(132)));
                            lbl.Width = 75;
                            lbl.Height = 75;
                            lbl.ForeColor = Color.Black;
                            lbl.Click += conArea_Click;
                            _conArea.Controls.Add(lbl);


                            lbl2.Name = theSaddle.AreaNo;
                            //lbl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(206)))), ((int)(((byte)(105)))));
                            lbl2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(171)))), ((int)(((byte)(199)))), ((int)(((byte)(227))))); //Color.MediumAquamarine;
                            lbl2.Font = new System.Drawing.Font("微软雅黑", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(132)));
                            lbl2.Width = 75;
                            lbl2.Height = 75;
                            lbl2.ForeColor = Color.Black;
                            lbl2.Click += conArea_Click;
                            _conArea.Controls.Add(lbl2);

                            //List<int> list = rowInfo.getAreaNoRow(areaBase.AreaNo) as List<int>;
                            //lblRuler = rowInfo.getAreaRowsInfo(list);

                            isCreateLbl = true;
                        }

                        //lbl.Name = theSaddle.AreaNo;
                        //lbl.BackColor = Color.MediumAquamarine;
                        //lbl.Font = new System.Drawing.Font("微软雅黑", 7F, System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                        ////lbl.Width = 95;
                        ////lbl.Height = 100;
                        //lbl.Width = 90;
                        //lbl.Height = 100;
                        //lbl.ForeColor = Color.Black;                       
                        // _conArea.Controls.Add(lbl); 


                        List<int> list = rowInfo.getAreaNoRow(areaBase.AreaNo) as List<int>;
                        List<int> listCol = rowInfo.getAreaNoCol(areaBase.AreaNo) as List<int>;
                        lblRuler = rowInfo.getAreaRowsInfo(list);

                        //lblCol = rowInfo.getAreaColsInfo(listCol);


                        //foreach (int i in rowInfo.getRowColListByAreaNO(areaBase.AreaNo))
                        //{
                        //    LogManager.WriteProgramLog("RowColList>>>>>>>>" + i);
                        //}

                        isCreateLbl = true;

                    }
                    if (panel.Width < 1400)
                    {
                        lbl.Width = 50;
                        lbl.Height = 20;
                        lbl.Text = lblRuler;
                        //lbl.Location = new Point(this.Width / 2, this.Height / 2);
                        lbl.Location = new Point(this.Width / 2 - 30, this.Height / 2);
                    }
                    else
                    {


                        lbl.Width = Convert.ToInt32((theSaddle.X_End - theSaddle.X_Start) * xScale);
                        //lbl.Height = 20;                       

                        var matWgt = string.Empty;
                        var matWgtTotal = string.Empty;
                        if (grids != null)
                        {
                            foreach (DataRow dr in grids.Rows)
                            {
                                //GRID_NO, GRID_NAME, GRID_DIV, GRID_STATUS, MAT_WGT, MAT_WGTTOTAL
                                var gridNo = dr["GRID_NO"].ToString();
                                if (!string.IsNullOrEmpty(gridNo))
                                {
                                    var gridNoSplit = gridNo.Split('-');
                                    if (gridNoSplit != null && gridNoSplit.Length > 0)
                                    {
                                        var grid = "A" + Convert.ToInt32(gridNoSplit[0]);
                                        if (gridNoSplit[1].ToString().Equals("0") && grid.Equals(theSaddle.AreaNo))
                                        {
                                            Inventory = Convert.ToInt32(dr["MAT_WGT"].ToString());
                                            TotalInventory = Convert.ToInt32(dr["MAT_WGTTOTAL"].ToString());
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        // -------------  1  ---------------
                        lbl.AutoSize = false;
                        var height1 = Convert.ToInt32((theSaddle.Y_End - theSaddle.Y_Start) * yScale);
                        int Percentage = (int)((Inventory / TotalInventory) * 100);
                        var percentageNum = (int)(100 - Percentage);
                        PercentageName = "";
                        if (Percentage >= 10 && Percentage <= 90)
                        {
                            PercentageName = (int)Percentage + "%";
                            lbl.TextAlign = ContentAlignment.TopCenter;
                        }
                        else if (Percentage >= 100)
                        {
                            PercentageName = "100%";
                        }
                        var height = (int)((percentageNum * height1) / 100);
                        var dd = height + (this.Location.Y - 30);
                        this.lbl.Location = new Point(Convert.ToInt32(2), Convert.ToInt32(dd));
                        this.lbl.Height = Convert.ToInt32((Percentage * height1) / 100);
                        this.lbl.Text = PercentageName;
                        this.lbl.BackColor = System.Drawing.Color.FromArgb(25, Color.Blue);
                        // -------------  2  ---------------
                        this.lbl2.AutoSize = false;
                        this.lbl2.Width = Convert.ToInt32((theSaddle.X_End - theSaddle.X_Start) * xScale);
                        var height2 = Convert.ToInt32((theSaddle.Y_End - theSaddle.Y_Start) * yScale);
                        var Percentage2 = (int)((Inventory / TotalInventory) * 100);
                        var percentageNum2 = (int)(100 - Percentage2);
                        var PercentageName2 = "";
                        if (percentageNum2 >= 100)
                        {
                            PercentageName2 = "0%";
                            Percentage2 = (int)(100 - Percentage2);
                            lbl2.TextAlign = ContentAlignment.BottomCenter;
                        }
                        else if (percentageNum2 < 10 && percentageNum2 > 0)
                        {
                            PercentageName2 = (int)Percentage2 + "%";
                            this.lbl2.TextAlign = ContentAlignment.TopCenter;
                        }
                        else if (percentageNum2 <= 0)
                        {
                            PercentageName2 = "0%";
                            this.lbl2.TextAlign = ContentAlignment.BottomCenter;
                        }
                        this.lbl2.Height = (int)((Percentage2 * height2) / 100);
                        this.lbl2.Text = PercentageName2;
                        this.lbl2.BackColor = Color.Transparent;

                        if (areaBase.AreaNo == "Z23-A" || areaBase.AreaNo == "Z23-B" || areaBase.AreaNo == "Z23-C" || areaBase.AreaNo == "Z21-I" || areaBase.AreaNo == "Z21-J")
                        {
                            lbl.Location = new Point(this.Width / 2 - 30, 30);
                        }
                        else if ((areaBase.AreaNo == "Z62-A"))
                        {
                            lbl.Location = new Point(this.Width / 2 - 30, this.Height * 16967 / 39000 + 100);
                        }
                        else if ((areaBase.AreaNo == "Z62-A"))
                        {
                            lbl.Location = new Point(this.Width / 2 - 30, 100);
                        }
                        else if (areaBase.AreaNo.Contains("PR"))
                        {
                            lbl.Location = new Point(2, this.Height / 2 - 30);
                        }
                        else if ((areaBase.AreaNo == "Z01-9"))
                        {
                            lbl.Location = new Point(this.Width / 2 - 30, this.Height / 2 - 20);
                        }
                        else if ((areaBase.AreaNo == "Z01-7"))
                        {
                            lbl.Location = new Point(this.Width / 2 - 30, this.Height / 2);
                        }
                        else
                        {
                            //lbl.Location = new Point(this.Width / 2 - 30, this.Height / 2 - 40);


                            //var x1 = Convert.ToDouble(theTrafficLightBase.X_Start) * xScale;
                            //var x2 = Convert.ToDouble(theTrafficLightBase.X_End) * xScale;
                            //var x3 = x1 + this.Width;
                            //var x4 = x2 - x3;
                            //var x5 = x4 / 2;
                            //location_X = x1 + x5;


                        }

                    }


                    if (StockPercentage)
                    {
                        lbl.Visible = true;
                        lbl2.Visible = true;
                    }
                    else
                    {
                        lbl.Visible = false;
                        lbl2.Visible = false;
                    }

                    if (areaBase.AreaNo == "Z01-1" || areaBase.AreaNo == "Z01-7" || areaBase.AreaNo == "Z01-9" || areaBase.AreaNo == "Z01-TA")
                    {
                        this.SendToBack();
                    }
                }
                else if (theSaddle.AreaType == 3)
                {
                    //this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(206)))), ((int)(((byte)(105)))));
                    this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(171)))), ((int)(((byte)(199)))), ((int)(((byte)(227)))));  //Color.MediumAquamarine;
                    Graphics gr = this.CreateGraphics();
                    if (theSaddle.AreaDoorSefeValue == 1 && theSaddle.AreaDoorReserveValue == 0)
                    {
                        //Rectangle rec = new Rectangle(new Point(0, 0), new Size(this.Width, this.Height));
                        //gr.DrawRectangle(new Pen(Color.Red, 2), rec);              
                        this.BackColor = Color.Red;
                    }
                    else if (theSaddle.AreaDoorSefeValue == 1 && theSaddle.AreaDoorReserveValue == 1)
                    {
                        //Rectangle rec = new Rectangle(new Point(0, 0), new Size(this.Width, this.Height));
                        //gr.DrawRectangle(new Pen(Color.Blue, 2), rec);
                        this.BackColor = Color.Blue;
                    }
                    else if (theSaddle.AreaDoorSefeValue == 0 && theSaddle.AreaDoorReserveValue == 1)
                    {
                        //Rectangle rec = new Rectangle(new Point(0, 0), new Size(this.Width, this.Height));
                        //gr.DrawRectangle(new Pen(Color.Yellow, 2), rec);
                        this.BackColor = Color.Yellow;
                    }
                }
                //出口
                else if (theSaddle.AreaType == 4)
                {
                    this.BackColor = Color.MediumPurple;
                    Graphics gr = this.CreateGraphics();
                    if (theSaddle.AreaNo.Contains("YSL"))
                    {
                        this.BackColor = Color.SpringGreen;
                        if (theSaddle.AreaDoorSefeValue == 1 && theSaddle.AreaDoorReserveValue == 0)
                            this.BackColor = System.Drawing.Color.Red;
                        else if (theSaddle.AreaDoorSefeValue == 1 && theSaddle.AreaDoorReserveValue == 1)
                            this.BackColor = System.Drawing.Color.Blue;
                        else if (theSaddle.AreaDoorSefeValue == 0 && theSaddle.AreaDoorReserveValue == 1)
                            this.BackColor = System.Drawing.Color.Yellow;
                        else
                            this.BackColor = System.Drawing.Color.SpringGreen;
                    }
                    if (theSaddle.AreaNo.Contains("WC"))
                    {
                        if (theSaddle.AreaDoorSefeValue == 1 && theSaddle.AreaDoorReserveValue == 0)
                            this.BackColor = System.Drawing.Color.Red;
                        else if (theSaddle.AreaDoorSefeValue == 1 && theSaddle.AreaDoorReserveValue == 1)
                            this.BackColor = System.Drawing.Color.Blue;
                        else if (theSaddle.AreaDoorSefeValue == 0 && theSaddle.AreaDoorReserveValue == 1)
                            this.BackColor = System.Drawing.Color.Yellow;
                        else
                            this.BackColor = System.Drawing.Color.MediumPurple;
                    }
                    if (areaBase.AreaNo == "D108-WC")
                    {
                        this.SendToBack();
                    }
                }
                //入口
                else if (theSaddle.AreaType == 5)
                {
                    this.BackColor = Color.MediumSlateBlue;
                    Graphics gr = this.CreateGraphics();
                    if (theSaddle.AreaNo.Contains("WR") || theSaddle.AreaNo.Contains("EL"))
                    {
                        if (theSaddle.AreaDoorSefeValue == 1 && theSaddle.AreaDoorReserveValue == 0)
                            this.BackColor = System.Drawing.Color.Red;
                        else if (theSaddle.AreaDoorSefeValue == 1 && theSaddle.AreaDoorReserveValue == 1)
                            this.BackColor = System.Drawing.Color.Blue;
                        else if (theSaddle.AreaDoorSefeValue == 0 && theSaddle.AreaDoorReserveValue == 1)
                            this.BackColor = System.Drawing.Color.Yellow;
                        else
                            this.BackColor = System.Drawing.Color.MediumSlateBlue;
                    }

                }
                else if (theSaddle.AreaType == 1)
                {
                    this.BackColor = Color.CadetBlue;
                    if (theSaddle.AreaNo.Contains("XFQ"))
                    {
                        if (theSaddle.AreaDoorSefeValue == 1 && theSaddle.AreaDoorReserveValue == 0)
                            this.BackColor = System.Drawing.Color.Red;
                        else if (theSaddle.AreaDoorSefeValue == 1 && theSaddle.AreaDoorReserveValue == 1)
                            this.BackColor = System.Drawing.Color.Blue;
                        else if (theSaddle.AreaDoorSefeValue == 0 && theSaddle.AreaDoorReserveValue == 1)
                            this.BackColor = System.Drawing.Color.Yellow;
                        else
                            this.BackColor = System.Drawing.Color.CadetBlue;
                    }
                    if (theSaddle.AreaNo.Contains("WT"))
                    {
                        if (theSaddle.AreaDoorSefeValue == 1 && theSaddle.AreaDoorReserveValue == 0)
                            this.BackColor = System.Drawing.Color.Red;
                        else if (theSaddle.AreaDoorSefeValue == 1 && theSaddle.AreaDoorReserveValue == 1)
                            this.BackColor = System.Drawing.Color.Blue;
                        else if (theSaddle.AreaDoorSefeValue == 0 && theSaddle.AreaDoorReserveValue == 1)
                            this.BackColor = System.Drawing.Color.Yellow;
                        else
                            this.BackColor = System.Drawing.Color.CadetBlue;
                    }

                }
                else if (theSaddle.AreaType == 6)
                {
                    //this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(206)))), ((int)(((byte)(105)))));
                    this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(171)))), ((int)(((byte)(199)))), ((int)(((byte)(227)))));  //Color.MediumAquamarine;
                    Graphics gr = this.CreateGraphics();
                    if (theSaddle.AreaNo.Contains("JQ"))
                    {
                        //if (theSaddle.AreaDoorSefeValue == 1 && theSaddle.AreaDoorReserveValue == 0)
                        //    this.BackColor = System.Drawing.Color.Red;
                        //else if (theSaddle.AreaDoorSefeValue == 1 && theSaddle.AreaDoorReserveValue == 1)
                        //    this.BackColor = System.Drawing.Color.Blue;
                        //else if (theSaddle.AreaDoorSefeValue == 0 && theSaddle.AreaDoorReserveValue == 1)
                        //    this.BackColor = System.Drawing.Color.Yellow;
                        //else
                        //    this.BackColor = System.Drawing.Color.MediumAquamarine;
                        if (theSaddle.AreaDoorSefeValue == 0)
                            this.BackColor = System.Drawing.Color.Red;
                        else
                        {
                            //this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(206)))), ((int)(((byte)(105)))));
                            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(171)))), ((int)(((byte)(199)))), ((int)(((byte)(227))))); //System.Drawing.Color.MediumAquamarine;
                        }
                    }

                }
                else
                {
                    //this.BackColor = Color.Beige;
                    if (theSaddle.AreaNo.Contains("MC"))
                    {
                        this.BackColor = Color.SpringGreen;
                    }
                    else
                    {
                        this.BackColor = Color.MediumSlateBlue;
                    }

                }
            }
            catch (Exception ex)
            {
                //LogManager.WriteProgramLog(ex.Message);
                //LogManager.WriteProgramLog(ex.StackTrace);
            }
        }
        public delegate void EventHandler_Saddle_Selected(AreaBase theSaddleInfo);
        public event EventHandler_Saddle_Selected Saddle_Selected;

        private void conArea_Paint(object sender, PaintEventArgs e)
        {
            try
            {

                Graphics gr = e.Graphics;
                Pen p = new Pen(Color.White, 2);

                if (areaBase.AreaType == 0)
                {

                    if (areaBase.AreaNo == "Z31-8")
                    {
                        gr.DrawString(areaBase.Area_Name,
                               new Font("微软雅黑", 14, FontStyle.Bold), Brushes.Black, new Point(this.Width / 2 - 30, 20));
                        //Z06 - A
                        Point p1 = new Point(0, 0);
                        Point p2 = new Point(this.Width, 0);
                        Point p3 = new Point(this.Width, this.Height * 8 / 13);
                        Point p4 = new Point(this.Width * 8 / 13, this.Height * 8 / 13);
                        Point p5 = new Point(this.Width * 8 / 13, this.Height);
                        Point p6 = new Point(0, this.Height);
                        Point[] points = { p1, p2, p3, p4, p5, p6 };
                        gr.DrawPolygon(p, points);

                        //Brush bush = new SolidBrush(Color.LightSteelBlue);
                        //gr.FillRectangle(bush, 0, this.Height - (this.Height * 2650 / 21615), this.Width * 37870 / 48370, this.Height * 2650 / 21615);                    
                    }
                    else if (areaBase.AreaNo == "Z23-H")
                    {
                        gr.DrawString(areaBase.Area_Name,
                               new Font("微软雅黑", 12, FontStyle.Bold), Brushes.Black, new Point(this.Width / 2 - 30, 20));
                        Point p1 = new Point(0, 0);
                        Point p2 = new Point(this.Width, 0);
                        Point p3 = new Point(this.Width, this.Height);
                        Point p4 = new Point(this.Width * 5 / 8 - 1, this.Height);
                        Point p5 = new Point(this.Width * 5 / 8 - 1, this.Height * 5 / 7 - 2);
                        Point p6 = new Point(this.Width / 2 - 2, this.Height * 5 / 7 - 2);
                        Point p7 = new Point(this.Width / 2 - 2, this.Height);
                        Point p8 = new Point(0, this.Height);
                        Point[] points = { p1, p2, p3, p4, p5, p6, p7, p8 };
                        gr.DrawPolygon(p, points);

                    }
                    else if (areaBase.AreaNo == "Z21-F")
                    {
                        gr.DrawString(areaBase.Area_Name,
                               new Font("微软雅黑", 12, FontStyle.Bold), Brushes.Black, new Point(this.Width / 2 - 30, 20));
                        Point p1 = new Point(0, 0);
                        Point p2 = new Point(this.Width * 2 / 3 - 3, 0);
                        Point p3 = new Point(this.Width * 2 / 3 - 3, this.Height / 4 + 23);
                        Point p4 = new Point(this.Width * 3 / 4 - 2, this.Height / 4 + 23);
                        Point p5 = new Point(this.Width * 3 / 4 - 2, 0);
                        Point p6 = new Point(this.Width, 0);
                        Point p7 = new Point(this.Width, this.Height);
                        Point p8 = new Point(0, this.Height);
                        Point[] points = { p1, p2, p3, p4, p5, p6, p7, p8 };
                        gr.DrawPolygon(p, points);
                    }
                    //else if (areaBase.AreaNo == "Z01-9")
                    //{
                    //    gr.DrawString(areaBase.Area_Name,
                    //           new Font("微软雅黑", 12, FontStyle.Bold), Brushes.Black, new Point(this.Width / 2 - 30, 20));

                    //    //Point p1 = new Point(0, this.Height);
                    //    //Point p2 = new Point(this.Width * 7993 / 26760, this.Height);
                    //    //Point p3 = new Point(this.Width * 7993 / 26760, this.Height * 13747 / 21867);
                    //    //Point p4 = new Point(this.Width, this.Height * 13747 / 21867);
                    //    //Point p5 = new Point(this.Width, 0);
                    //    //Point p6 = new Point(0, 0);
                    //    //Point p7 = new Point(0, this.Height);
                    //    //Point[] points = { p1, p2, p3, p4, p5, p6, p7 };
                    //    //gr.DrawPolygon(p, points);

                    //    //Brush bush = new SolidBrush(Color.LightSteelBlue);
                    //    //gr.FillRectangle(bush, this.Width * 7993 / 26760, this.Height * 13747 / 21867, this.Width * 18767 / 26760 + 1, this.Height * 8120 / 21867 + 1);
                    //}
                    else if (areaBase.AreaNo == "Z01-7")
                    {
                        gr.DrawString(areaBase.Area_Name,
                               new Font("微软雅黑", 12, FontStyle.Bold), Brushes.Black, new Point(this.Width / 2 - 30, this.Height / 2 - 50));
                        Point p1 = new Point(0, this.Height);
                        Point p2 = new Point(this.Width, this.Height);
                        Point p3 = new Point(this.Width, this.Height * 9300 / 28380);
                        Point p4 = new Point(this.Width * 43251 / 79211, this.Height * 9300 / 28380);
                        Point p5 = new Point(this.Width * 43251 / 79211, 0);
                        Point p6 = new Point(0, 0);
                        Point p7 = new Point(0, this.Height);
                        Point[] points = { p1, p2, p3, p4, p5, p6, p7 };
                        gr.DrawPolygon(p, points);

                        Brush bush1 = new SolidBrush(Color.LightSteelBlue);
                        gr.FillRectangle(bush1, this.Width * 43251 / 79211, 0, this.Width * 35960 / 79211 + 1, this.Height * 9300 / 28380 - 1);

                        Brush bush2 = new SolidBrush(Color.LightSteelBlue);
                        gr.FillRectangle(bush2, this.Width * 33366 / 79211, 0, this.Width * 1892 / 79211 + 1, this.Height * 8280 / 28380 - 1);
                    }
                    else if (areaBase.AreaNo == "Z23-A" || areaBase.AreaNo == "Z23-B" || areaBase.AreaNo == "Z23-C" || areaBase.AreaNo == "Z21-I" || areaBase.AreaNo == "Z21-J")
                    {
                        gr.DrawString(areaBase.Area_Name,
                               new Font("微软雅黑", 12, FontStyle.Bold), Brushes.Black, new Point(this.Width / 2 - 30, 0));
                        Rectangle rec = new Rectangle(new Point(0, 0), new Size(this.Width, this.Height));
                        gr.DrawRectangle(p, rec);
                    }
                    else if (areaBase.AreaNo.Contains("WJ"))
                    {
                        gr.DrawString(areaBase.Area_Name,
                           new Font("微软雅黑", 7, FontStyle.Bold), Brushes.Black, new Point(6, 3), new StringFormat(StringFormatFlags.DirectionVertical));
                        Rectangle rec = new Rectangle(new Point(0, 0), new Size(this.Width, this.Height));
                        gr.DrawRectangle(p, rec);
                    }
                    else if (areaBase.AreaNo.Contains("Z68-B"))
                    {
                        gr.DrawString(areaBase.Area_Name,
                           new Font("微软雅黑", 12, FontStyle.Bold), Brushes.Black, new Point(3, 3), new StringFormat(StringFormatFlags.DirectionVertical));
                        Rectangle rec = new Rectangle(new Point(0, 0), new Size(this.Width, this.Height));
                        gr.DrawRectangle(p, rec);
                    }
                    else if (areaBase.AreaNo.Contains("D112"))
                    {
                        //gr.DrawString(areaBase.Area_Name,
                        //   new Font("微软雅黑", 9, FontStyle.Bold), Brushes.Black, new Point(this.Width / 3, 3), new StringFormat(StringFormatFlags.DirectionVertical));
                        //Rectangle rec = new Rectangle(new Point(0, 0), new Size(this.Width, this.Height));
                        //gr.DrawRectangle(p, rec);

                        gr.DrawString(areaBase.Area_Name,
                           new Font("微软雅黑", 9, FontStyle.Bold), Brushes.Black, new Point(2, 3));
                        Rectangle rec = new Rectangle(new Point(0, 0), new Size(this.Width, this.Height));
                        gr.DrawRectangle(p, rec);
                    }
                    else if (areaBase.AreaNo.Contains("D108"))
                    {
                        gr.DrawString(areaBase.Area_Name,
                           new Font("微软雅黑", 9, FontStyle.Bold), Brushes.Black, new Point(this.Width / 3, 3), new StringFormat(StringFormatFlags.DirectionVertical));
                        Rectangle rec = new Rectangle(new Point(0, 0), new Size(this.Width, this.Height));
                        gr.DrawRectangle(p, rec);
                    }
                    else if (areaBase.AreaNo.Contains("D208-PR1"))
                    {
                        gr.DrawString(areaBase.Area_Name,
                           new Font("微软雅黑", 9, FontStyle.Bold), Brushes.Black, new Point(2, 3));
                        Rectangle rec = new Rectangle(new Point(0, 0), new Size(this.Width, this.Height));
                        gr.DrawRectangle(p, rec);
                    }
                    else
                    {
                        gr.DrawString(areaBase.Area_Name,
                               new Font("微软雅黑", 12, FontStyle.Bold), Brushes.Black, new Point(this.Width / 2 - 20, 20));
                        //        //创建矩形对象                左上角度座标                 宽   高  
                        Rectangle rec = new Rectangle(new Point(0, 0), new Size(this.Width, this.Height));
                        gr.DrawRectangle(p, rec);
                    }
                }
                else if (areaBase.AreaType == 1)
                {
                    if (areaBase.AreaNo.Contains("WT"))
                    {
                        e.Graphics.DrawString(areaBase.Area_Name, new Font("微软雅黑", 12, FontStyle.Bold),
                        new SolidBrush(Color.Black), new Point(this.Width / 2 - 20, 0));
                    }
                    else if (areaBase.AreaNo.Contains("XFQ"))
                    {
                        e.Graphics.DrawString(areaBase.Area_Name, new Font("微软雅黑", 12, FontStyle.Bold),
                        new SolidBrush(Color.Black), new Point(0, 0));
                    }
                    else
                    {
                        e.Graphics.DrawString(areaBase.Area_Name, new Font("微软雅黑", 12, FontStyle.Regular), new SolidBrush(Color.Black), 1, this.Height - 70, new StringFormat(StringFormatFlags.DirectionVertical));
                    }
                }
                else if (areaBase.AreaType == 4 || areaBase.AreaType == 5)
                {

                    if (areaBase.AreaNo.Contains("MC1"))
                    {
                        if (areaBase.AreaNo.Contains("Z23-MC1"))
                        {
                            gr.DrawString(areaBase.Area_Name,
                            new Font("微软雅黑", 8F, FontStyle.Bold), Brushes.Black, new Point(0, 10));
                        }
                        else
                        {
                            gr.DrawString(areaBase.Area_Name,
                            new Font("微软雅黑", 8F, FontStyle.Bold), Brushes.Black, new Point(0, 10));
                        }
                    }
                    else if (areaBase.AreaNo.Contains("MC1"))
                    {

                    }
                    else
                    {
                        e.Graphics.DrawString(areaBase.Area_Name, new Font("微软雅黑", 12, FontStyle.Bold),
                        new SolidBrush(Color.Black), new Point(0, 0));
                        Rectangle rec = new Rectangle(new Point(0, 0), new Size(this.Width, this.Height));
                        gr.DrawRectangle(new Pen(Color.White, 2), rec);
                    }

                }
                else if (areaBase.AreaType == 3)
                {
                    e.Graphics.DrawString(areaBase.Area_Name, new Font("微软雅黑", 9, FontStyle.Bold),
                        new SolidBrush(Color.Black), 0, 0, new StringFormat(StringFormatFlags.DirectionVertical));
                    Rectangle rec = new Rectangle(new Point(0, 0), new Size(this.Width, this.Height));
                    gr.DrawRectangle(new Pen(Color.White, 2), rec);
                }
                else if (areaBase.AreaType == 6)
                {
                    e.Graphics.DrawString(areaBase.Area_Name, new Font("微软雅黑", 9, FontStyle.Bold),
                        new SolidBrush(Color.Black), 0, 0, new StringFormat(StringFormatFlags.DirectionVertical));
                    Rectangle rec = new Rectangle(new Point(0, 0), new Size(this.Width, this.Height));
                    gr.DrawRectangle(new Pen(Color.White, 2), rec);
                }
            }
            catch (Exception er)
            {

            }
        }
        private bool WCUnitSaddle(AreaBase areaBase)
        {
            int count = 0;
            string unitNO = areaBase.AreaNo.Substring(0, 4);
            string unitFlag = areaBase.AreaNo.Substring(5, 2);
            string unitStock = unitNO + unitFlag;
            string sqlText = @"SELECT A.STOCK_NO STOCK_NO,B.COIL_NO COIL_NO, C.STOCK_STATUS STOCK_STATUS 
                               FROM UACS_LINE_SADDLE_DEFINE A LEFT JOIN UACS_LINE_EXIT_L2INFO B ON B.UNIT_NO= A.UNIT_NO AND B.SADDLE_L2NAME = A.SADDLE_L2NAME
                               LEFT JOIN UACS_YARDMAP_STOCK_DEFINE C ON C.STOCK_NO = A.STOCK_NO
                               WHERE A.STOCK_NO LIKE '%" + unitStock + "%'";
            using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sqlText))
            {
                while (rdr.Read())
                {
                    if (rdr["COIL_NO"] != System.DBNull.Value && rdr["STOCK_STATUS"].ToString() == "2")
                    {
                        count++;
                    }
                }
            }
            if (count >= 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool WRUnitSaddle(AreaBase areaBase)
        {
            int count = 0;
            string unitNO = areaBase.AreaNo.Substring(0, 4);
            string unitFlag = areaBase.AreaNo.Substring(5, 2);
            string unitStock = unitNO + unitFlag;
            string sqlText = @"SELECT A.STOCK_NO STOCK_NO,B.COIL_NO COIL_NO, C.STOCK_STATUS STOCK_STATUS 
                               FROM UACS_LINE_SADDLE_DEFINE A LEFT JOIN UACS_LINE_ENTRY_L2INFO B ON B.UNIT_NO= A.UNIT_NO AND B.SADDLE_L2NAME = A.SADDLE_L2NAME
                               LEFT JOIN UACS_YARDMAP_STOCK_DEFINE C ON C.STOCK_NO = A.STOCK_NO
                               WHERE A.STOCK_NO LIKE '%" + unitStock + "%'";
            using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sqlText))
            {
                while (rdr.Read())
                {
                    if (rdr["COIL_NO"] != System.DBNull.Value && rdr["STOCK_STATUS"].ToString() == "2")
                    {
                        count++;
                    }
                }
            }
            if (count >= 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private void conArea_Click(object sender, EventArgs e)
        {
            if (areaBase.AreaNo.IndexOf("YSL-WC") > -1 && areaBase.AreaType == 4)
            {
                MessageBoxButtons btn = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("确定要切换到运输链出口画面吗？", "操作提示", btn);
                if (dr == DialogResult.OK)
                {
                    auth.OpenForm("02 运输链出口");
                }
            }
            if (areaBase.AreaNo.IndexOf("D102-WR") > -1 && areaBase.AreaType == 5)
            {
                MessageBoxButtons btn = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("确定要切换到D102机组入口跟踪画面吗？", "操作提示", btn);
                if (dr == DialogResult.OK)
                {
                    auth.OpenForm("03 D102机组入口");
                }
            }
            if (areaBase.AreaNo.IndexOf("Z01-XFQ") > -1 && areaBase.AreaType == 3)
            {
                MessageBoxButtons btn = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("确定要切换到修复区域画面吗？", "操作提示", btn);
                if (dr == DialogResult.OK)
                {
                    auth.OpenForm("04 修复区域");
                }
            }
            if (areaBase.AreaNo.IndexOf("Z01-WT") > -1 && areaBase.AreaType == 1)
            {
                MessageBoxButtons btn = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("确定要切换到冷却水槽画面吗？", "操作提示", btn);
                if (dr == DialogResult.OK)
                {
                    auth.OpenForm("05 冷却水槽");
                }
            }
            if (areaBase.AreaNo.IndexOf("Z01-WT") > -1 && areaBase.AreaType == 3)
            {
                MessageBoxButtons btn = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("确定要切换到冷却水槽画面吗？", "操作提示", btn);
                if (dr == DialogResult.OK)
                {
                    auth.OpenForm("05 冷却水槽");
                }
            }

            if (areaBase.AreaType == 0)
            {
                if (frmSaddleShow == null || frmSaddleShow.IsDisposed)
                {
                    frmSaddleShow = new FrmSaddleShow();
                    frmSaddleShow.AreaBase = areaBase;
                    frmSaddleShow.Show();
                }
                else
                {
                    frmSaddleShow.WindowState = FormWindowState.Normal;
                    frmSaddleShow.Activate();
                }
            }
        }

    }
}
