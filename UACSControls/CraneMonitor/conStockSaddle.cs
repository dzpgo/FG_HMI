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
using UACS;

namespace UACSControls
{
    public partial class conStockSaddle : UserControl
    {
        public conStockSaddle()
        {
            InitializeComponent();
            this.SetStyle(
               ControlStyles.OptimizedDoubleBuffer |
               ControlStyles.ResizeRedraw |
               ControlStyles.AllPaintingInWmPaint, true);
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
        private SaddleBase mySaddleInfo = new SaddleBase();
        private Label lblRowNo = new Label();
        private Label lblColNo = new Label();
        private Label lbl = new Label();

        private int Location_X;
        private int Location_Y;
        private int width;
        private int height;

        private Graphics gr;
        public delegate void EventHandler_Saddle_Selected(SaddleBase theSaddleInfo);
        public event EventHandler_Saddle_Selected Saddle_Selected;
        public void conInit()
        {
            try
            {
                this.panel1.MouseDown += new MouseEventHandler(conSaddle_visual_MouseUp);

            }
            catch (Exception ex)
            {
            }
        }

        void conSaddle_visual_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    if (Saddle_Selected != null)
                    {
                        Saddle_Selected(mySaddleInfo.Clone());
                    }
                }
                else
                {
                    FrmSaddleMetail FrmSaddleDetail = new FrmSaddleMetail();
                    FrmSaddleDetail.SaddleInfo = this.mySaddleInfo;
                    FrmSaddleDetail.Show();
                }
            }
            catch (Exception ex)
            {
            }
        }

        public delegate void saddlesRefreshInvoke(SaddleBase theSaddle, double X_Width, double Y_Height, AreaBase theArea, int panelWidth, int panelHeight, bool xAxisRight, bool yAxisDown, Panel panel, List<int> list = null);
        private AreaBase myArea;
        private Panel MyPanel;
        private bool isCreateRowLbl = false;
        private bool isCreateColLbl = false;
        private double location_x;
        private double location_y;
        private double xMax_7_1;

        private bool Coil_PlasticFlag = false;
        private bool Coil_PackFlag = false;
        private void Get_Coil_PlasticFlag(string COIL_NO)
        {
            string sql = @"SELECT COIL_NO FROM UACS_YARDMAP_COIL_PLASTIC WHERE PLASTIC_FLAG = 1";
            using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
            {
                while (rdr.Read())
                {
                    if (COIL_NO == rdr["COIL_NO"].ToString().Trim())
                    {
                        Coil_PlasticFlag = true;
                    }
                }
            }
        }

        private void Get_Coil_PackFlag(string COIL_NO)
        {
            string sql = @"SELECT PACK_FLAG,FORBIDEN_FLAG,NEXT_UNIT_NO,L3_COIL_STATUS FROM UACS_YARDMAP_COIL WHERE COIL_NO = '" + COIL_NO + "'";
            using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
            {
                while (rdr.Read())
                {
                    if (rdr["PACK_FLAG"] != DBNull.Value && rdr["FORBIDEN_FLAG"] != DBNull.Value)
                    {
                        if (Convert.ToInt32(rdr["PACK_FLAG"]) == 0 && Convert.ToInt32(rdr["FORBIDEN_FLAG"]) != 1 && (rdr["NEXT_UNIT_NO"].ToString() == "D000" || rdr["NEXT_UNIT_NO"].ToString().Contains("PA")))
                        {
                            Coil_PackFlag = true;
                        }
                    }
                }
            }
        }
        //public void refreshControl(SaddleBase theSaddle, double X_Width, double Y_Height, AreaBase theArea, int panelWidth, int panelHeight, bool xAxisRight, bool yAxisDown, Panel panel, List<int> list = null)
        //{
        //    try
        //    {
        //        //附对象
        //        mySaddleInfo = theSaddle;

        //        width = panelWidth;
        //        height = panelHeight;

        //        myArea = theArea;
        //        MyPanel = panel;

        //        //钢卷是否套袋
        //        Coil_PlasticFlag = false;

        //        //待包卷标记
        //        Coil_PackFlag = false;

        //        //计算X方向上的比例关系
        //        double xScale = Convert.ToDouble(panelWidth - 40) / Convert.ToDouble(X_Width);
        //        double location_X = 0;
        //        if (xAxisRight == true)
        //        {
        //            location_X = Convert.ToDouble((theSaddle.X_Center) - theArea.X_Start) * xScale;
        //        }
        //        else
        //        {
        //            location_X = (X_Width - ((theSaddle.X_Center + theSaddle.SaddleWidth / 2) - theArea.X_Start)) * xScale;
        //        }

        //        //计算Y方向的比例关系
        //        double yScale = Convert.ToDouble(panelHeight - 40) / Convert.ToDouble(Y_Height);
        //        double location_Y = 0;
        //        if (yAxisDown == true)
        //        {
        //            location_Y = ((theSaddle.Y_Center - theSaddle.SaddleLength / 2) - theArea.Y_Start) * yScale;
        //        }
        //        else
        //        {
        //            location_Y = (Y_Height - ((theSaddle.Y_Center) - theArea.Y_Start)) * yScale;
        //        }

        //        Location_X = (int)location_X;
        //        Location_Y = (int)location_Y;

        //        if (theSaddle.Stock_Status == 0 && theSaddle.Lock_Flag == 0) //无卷可用
        //            this.BackColor = Color.White;
        //        else if (theSaddle.Stock_Status == 2 && theSaddle.Lock_Flag == 0) //有卷可用
        //        {
        //            //区分
        //            if (theSaddle.L3_coil_Status1 == "12")
        //                this.BackColor = Color.Blue;
        //            else if (theSaddle.L3_coil_Status1 == "02")
        //                this.BackColor = Color.Yellow;
        //            else if (theSaddle.L3_coil_Status1 == "10")
        //                this.BackColor = Color.Purple;
        //            else
        //                this.BackColor = Color.Black;

        //            //套袋颜色显示
        //            Get_Coil_PlasticFlag(theSaddle.Mat_No);
        //            if (Coil_PlasticFlag)
        //            {
        //                label2.Visible = true;
        //            }
        //            else
        //            {
        //                label2.Visible = false;
        //            }

        //            //if (Coil_PlasticFlag)
        //            //{
        //            //    label1.Text = "套";
        //            //}
        //            //else
        //            //{
        //            //    label1.Text = "";
        //            //}
        //            //Get_Coil_PackFlag(theSaddle.Mat_No);
        //            //if (Coil_PackFlag && (theArea.AreaNo == "Z07-A" || theArea.AreaNo == "Z07-B" || theArea.AreaNo == "Z08-A" || theArea.AreaNo == "Z08-B"))
        //            //{
        //            //    this.BackColor = Color.Yellow;
        //            //}
        //            //else
        //            //{
        //            //    this.BackColor = Color.Black;
        //            //}
        //        }

        //        else
        //            this.BackColor = Color.Red;



        //        //List<string> repeatStock = new List<string>();
        //        //string sql = string.Format("select stock_no from UACS_YARDMAP_STOCK_DEFINE where MAT_NO in (select MAT_NO from UACS_YARDMAP_STOCK_DEFINE group by UACS_YARDMAP_STOCK_DEFINE.MAT_NO having count(UACS_YARDMAP_STOCK_DEFINE.MAT_NO) > 1)");
        //        //using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
        //        //{
        //        //    while (rdr.Read())
        //        //    {
        //        //        if (rdr["stock_no"] != DBNull.Value)
        //        //        {
        //        //            string rs = rdr["stock_no"].ToString();
        //        //            repeatStock.Add(rs);
        //        //        }
        //        //    }
        //        //}
        //        //foreach(string s in repeatStock)  //一卷多库位
        //        //{
        //        //    if(theSaddle.SaddleNo == s)
        //        //    {
        //        //        this.BackColor = Color.Purple;
        //        //    }
        //        //}

        //        //修改鞍座控件的宽度和高度
        //        this.Width = Convert.ToInt32((theSaddle.SaddleWidth - 700) * xScale);
        //        //this.Width = Convert.ToInt32((theSaddle.SaddleWidth - 40) * xScale);
        //        if (theArea.AreaNo == "Z01-1" || theArea.AreaNo == "Z01-2")
        //        {
        //            this.Height = Convert.ToInt32((theSaddle.SaddleLength - 500) * yScale / 2);
        //        }
        //        else
        //        {
        //            this.Height = Convert.ToInt32((theSaddle.SaddleLength - 500) * yScale / 3);
        //        }


        //        //定位库位鞍座的坐标
        //        //this.Location = new Point(Convert.ToInt32(location_X), Convert.ToInt32(location_Y + 10));
        //        if (theSaddle.Layer_Num == 2)
        //        {
        //            if (theArea.AreaNo == "Z01-1")
        //            {
        //                this.Location = new Point(Convert.ToInt32(location_X), Convert.ToInt32(location_Y) - 7);
        //            }
        //            else
        //            {
        //                this.Location = new Point(Convert.ToInt32(location_X), Convert.ToInt32(location_Y));
        //            }
        //        }
        //        else
        //        {
        //            if (theArea.AreaNo == "Z01-1")
        //            {
        //                this.Location = new Point(Convert.ToInt32(location_X), Convert.ToInt32(location_Y) + 7);
        //            }
        //            else
        //            {
        //                this.Location = new Point(Convert.ToInt32(location_X), Convert.ToInt32(location_Y) + 20);
        //            }

        //        }

        //        this.BringToFront();
        //        //if (theSaddle.SaddleNo.Substring( Convert.ToInt32( theSaddle.SaddleNo.Length.ToString()) - 1,1) == "2")
        //        //    this.BorderStyle = BorderStyle.Fixed3D;
        //        //else
        //        this.BorderStyle = BorderStyle.None;

        //        gr = panel.CreateGraphics();
        //        panel.Paint += panel_Paint;
        //        this.panel1.Paint += StockSaddle_Paint;

        //        toolTip1.IsBalloon = true;
        //        toolTip1.ReshowDelay = 0;
                
        //        toolTip1.SetToolTip(this.panel1, "材料号：" + theSaddle.Mat_No + "\r"
        //                            + "库位：    " + theSaddle.SaddleNo.ToString()
        //                            + "\r" + theSaddle.Row_No.ToString() + "行" + "-" + theSaddle.Col_No.ToString() + "列，" + "\r"
        //                            + "坐标：" + "\r"
        //                            + "X = " + theSaddle.X_Center.ToString() + "\r"
        //                            + "Y = " + theSaddle.Y_Center.ToString() + "\r"
        //        + "Z = " + theSaddle.Z_Center.ToString() + "\r"
        //        + "下道机组： " + theSaddle.Next_Unit_No + "\r"
        //        );



        //        //if (list != null)
        //        //{                 
        //        //    //给每行添加行展示
        //        //       if (theSaddle.SaddleNo.Count() == 10)
        //        //       {
        //        //           bool isIndex = false;
        //        //           bool isSpecialArea = false;
        //        //           string index = theSaddle.SaddleNo.Substring(theSaddle.SaddleNo.Count() - 4, 4);
        //        //           if (index == "0011")
        //        //           {
        //        //               isIndex = true;
        //        //           }

        //        //           if (isIndex || isSpecialArea)
        //        //           {
        //        //                if (theSaddle.Layer_Num == 1)
        //        //                {
        //        //                    if (!isCreateRowLbl)
        //        //                    {
        //        //                        lblRowNo.Name = theSaddle.SaddleNo + "Row";
        //        //                        lblRowNo.Text = theSaddle.Row_No.ToString();
        //        //                        lblRowNo.ForeColor = Color.Red;
        //        //                        lblRowNo.Font = new System.Drawing.Font("微软雅黑", 8F);
        //        //                        panel.Controls.Add(lblRowNo);
        //        //                        isCreateRowLbl = true;
        //        //                    }
        //        //                    //lblColNo.BringToFront();
        //        //                    //lblRowNo.Location = new Point(Convert.ToInt32(location_X + this.Width), Convert.ToInt32(location_Y));
        //        //                    lblRowNo.Location = new Point(Convert.ToInt32(location_X - 20), Convert.ToInt32(location_Y + 10));                                   
        //        //                }
        //        //                else
        //        //                {

        //        //                }
        //        //            }                              

        //        //    }


        //        //if (list[0] == 999)
        //        //{
        //        //    //给每行添加列展示
        //        //    foreach (var item in list)
        //        //    {

        //        //        //if (item == theSaddle.Row_No)
        //        //        //{
        //        //        if (theSaddle.Layer_Num == 1)
        //        //        {
        //        //            if (!isCreateColLbl)
        //        //            {
        //        //                //lblColNo.AutoSize = true;
        //        //                //lblColNo.BackColor = SystemColors.Control;
        //        //                //lblColNo.SendToBack();
        //        //                lblColNo.Size = new System.Drawing.Size(20, 13);
        //        //                lblColNo.Name = theSaddle.SaddleNo + "Col";
        //        //                lblColNo.Text = theSaddle.Col_No.ToString();
        //        //                lblColNo.ForeColor = Color.Blue;
        //        //                lblColNo.Font = new System.Drawing.Font("微软雅黑", 7F);
        //        //                panel.Controls.Add(lblColNo);
        //        //                isCreateColLbl = true;
        //        //            }
        //        //            lblColNo.Location = new Point(Convert.ToInt32(location_X) + this.Width / 3, Convert.ToInt32(location_Y)-5);
        //        //        }
        //        //        else if (theSaddle.Layer_Num == 2)
        //        //        {
        //        //            if (!isCreateColLbl)
        //        //            {
        //        //                //lblColNo.AutoSize = true;
        //        //                //lblColNo.BackColor = SystemColors.Control;
        //        //                //lblColNo.SendToBack();
        //        //                lblColNo.Size = new System.Drawing.Size(20, 13);
        //        //                lblColNo.Name = theSaddle.SaddleNo + "Col";
        //        //                lblColNo.Text = theSaddle.Col_No.ToString();
        //        //                lblColNo.ForeColor = Color.Blue;
        //        //                lblColNo.Font = new System.Drawing.Font("微软雅黑", 7F);
        //        //                panel.Controls.Add(lblColNo);
        //        //                isCreateColLbl = true;
        //        //            }
        //        //            lblColNo.Location = new Point(Convert.ToInt32(location_X) + this.Width / 3, Convert.ToInt32(location_Y)-20);
        //        //        }
        //        //    }
        //        //}
        //        //    else if (list[0] == 999)
        //        //    {
        //        //        gr = panel.CreateGraphics();
        //        //        panel.Paint += panel_Paint;
        //        //    }
        //        //    else
        //        //    { }
        //        //}
        //        //lblColNo.BringToFront();
        //        //this.BringToFront();/

        //        location_x = location_X;
        //        location_y = location_Y;
        //    }
        //    catch (Exception er)
        //    {
        //        throw;
        //    }
        //}

        public void refreshControl(SaddleBase theSaddle, double X_Width, double Y_Height, AreaBase theArea, int panelWidth, int panelHeight, bool xAxisRight, bool yAxisDown, Panel panel, List<int> list = null)
        {
            try
            {
                //panel.BackColor = Color.MediumAquamarine;
                //附对象
                mySaddleInfo = theSaddle;
                width = panelWidth;
                height = panelHeight;
                myArea = theArea;
                MyPanel = panel;
                //钢卷是否套袋
                Coil_PlasticFlag = false;
                //待包卷标记
                Coil_PackFlag = false;
                width = panelWidth;
                height = panelHeight;
                myArea = theArea;
                MyPanel = panel;
                //钢卷是否套袋
                Coil_PlasticFlag = false;
                //待包卷标记
                Coil_PackFlag = false;
                //计算X方向上的比例关系
                double xScale = Convert.ToDouble(panelWidth - 40) / Convert.ToDouble(X_Width);
                double location_X = 0;
                if (xAxisRight == true)
                {
                    //location_X = Convert.ToDouble(theSaddle.X_START - 8000) * xScale;
                    //location_X = Convert.ToDouble(theSaddle.X_START) * xScale;
                    //location_X = Convert.ToDouble( - theSaddle.X_START) * xScale;
                    location_X = Convert.ToDouble((theSaddle.X_START) - theArea.X_Start) * xScale;
                }
                else
                {
                    location_X = (X_Width - (theSaddle.X_CENTER  - theSaddle.X_START)) * xScale;

                }
                //计算Y方向的比例关系
                double yScale = Convert.ToDouble(panelHeight - 20) / Convert.ToDouble(Y_Height);
                double location_Y = 0;
                if (yAxisDown == true)
                {
                    location_Y = (theSaddle.Y_START) * yScale;
                    //location_Y = (theSaddle.Y_CENTER  - theSaddle.X_START) * yScale;
                }
                else
                {
                    location_Y = (theSaddle.Y_START) * yScale;
                    //location_Y = (Y_Height - (theSaddle.Y_CENTER - theSaddle.X_START)) * yScale;
                }

                Location_X = (int)location_X;
                Location_Y = (int)location_Y;

                if (theSaddle.Stock_Status == 0 && theSaddle.Lock_Flag == 0) //无卷可用
                    this.BackColor = Color.White;
                else if (theSaddle.Stock_Status == 2 && theSaddle.Lock_Flag == 0) //有卷可用
                {
                    //区分
                    if (theSaddle.L3_coil_Status1 == "12")
                        this.BackColor = Color.Blue;
                    else if (theSaddle.L3_coil_Status1 == "02")
                        this.BackColor = Color.Yellow;
                    else if (theSaddle.L3_coil_Status1 == "10")
                        this.BackColor = Color.Purple;
                    else
                        this.BackColor = Color.Black;

                    //套袋颜色显示
                    Get_Coil_PlasticFlag(theSaddle.Mat_No);
                    if (Coil_PlasticFlag)
                    {
                        label2.Visible = true;
                    }
                    else
                    {
                        label2.Visible = false;
                    }
                }

                else
                    this.BackColor = Color.Red;

                //修改鞍座控件的宽度和高度
                this.Width = Convert.ToInt32((theSaddle.SaddleWidth - 700) * xScale);
                //this.Width = Convert.ToInt32((theSaddle.SaddleWidth - 40) * xScale);
                if (theArea.AreaNo == "Z01-1" || theArea.AreaNo == "Z01-2")
                {
                    this.Height = Convert.ToInt32((theSaddle.SaddleLength - 500) * yScale / 2);
                }
                else
                {
                    this.Height = Convert.ToInt32((theSaddle.SaddleLength - 500) * yScale / 3);
                }

                //定位库位鞍座的坐标
                //this.Location = new Point(Convert.ToInt32(location_X), Convert.ToInt32(location_Y + 10));
                if (theSaddle.Layer_Num == 2)
                {
                    if (theArea.AreaNo == "Z01-1")
                    {
                        this.Location = new Point(Convert.ToInt32(location_X), Convert.ToInt32(location_Y) - 7);
                    }
                    else
                    {
                        this.Location = new Point(Convert.ToInt32(location_X), Convert.ToInt32(location_Y));
                    }
                }
                else
                {
                    if (theArea.AreaNo == "Z01-1")
                    {
                        this.Location = new Point(Convert.ToInt32(location_X), Convert.ToInt32(location_Y) + 7);
                    }
                    else
                    {
                        this.Location = new Point(Convert.ToInt32(location_X), Convert.ToInt32(location_Y) + 20);
                    }

                }

                //this.BringToFront();
                //this.BorderStyle = BorderStyle.None;
                //gr = panel.CreateGraphics();
                //panel.Paint += panel_Paint;
                //this.panel1.Paint += StockSaddle_Paint;
                //toolTip1.IsBalloon = true;
                //toolTip1.ReshowDelay = 0;


                this.BringToFront();
                //if (theSaddle.SaddleNo.Substring( Convert.ToInt32( theSaddle.SaddleNo.Length.ToString()) - 1,1) == "2")
                //    this.BorderStyle = BorderStyle.Fixed3D;
                //else
                this.BorderStyle = BorderStyle.None;

                gr = panel.CreateGraphics();
                panel.Paint += panel_Paint;
                this.panel1.Paint += StockSaddle_Paint;


                lbl.Name = theSaddle.GRID_NO;
                lbl.BackColor = Color.LightSteelBlue;  //Color.MediumAquamarine;
                lbl.Font = new System.Drawing.Font("微软雅黑", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(132)));
                lbl.Width = 75;
                lbl.Height = 75;
                lbl.ForeColor = Color.Black;
                lbl.Text = "料格号：" + theSaddle.GRID_NO + "\n"
                              + "库存重量：   " + theSaddle.MAT_WGT + "\n";
                //+ "黑库位：   " + saddleCoilNum + "\n"
                //+ "红库位：   " + (saddleNum - saddleNoCoilNum - saddleCoilNum) + "\n"
                //+ lblRuler + "\n"
                //+ lblCol;
                //lbl.Click += conArea_Click;
                //_conArea.Controls.Add(lbl);


                panel.Controls.Add(lbl);
                lbl.Location = new Point(Convert.ToInt32(location_X + 11), Convert.ToInt32(location_Y));

                location_x = location_X;
                location_y = location_Y;
            }
            catch (Exception er)
            {
                throw;
            }
        }


        ///// <summary>
        ///// 写横坐标
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //void panel_Paint(object sender, PaintEventArgs e)
        //{
        //    try
        //    {
        //        if (mySaddleInfo != null)
        //        {
        //            Pen p1 = new Pen(Color.Lime, 3);
        //            string strCol;
        //            int layer;
        //            layer = mySaddleInfo.Layer_Num;
        //            if (mySaddleInfo.SaddleNo.Length > 8)
        //            {
        //                strCol = mySaddleInfo.SaddleNo.Substring(5, 4);
        //            }
        //            else
        //            {
        //                strCol = mySaddleInfo.SaddleNo.Substring(5, 3);
        //            }
        //            if (strCol == "011" || strCol == "131" || strCol == "271" || strCol == "391" || strCol == "491" || strCol == "511" || strCol == "631" || strCol == "751" || strCol == "901")
        //            {
        //                string row;
        //                row = mySaddleInfo.SaddleNo.Substring(3, 2);
        //                if (myArea.AreaNo == "Z01-1")
        //                {
        //                    gr.DrawString(row, new Font("微软雅黑", 12, FontStyle.Bold), Brushes.Green,
        //                    new Point(Convert.ToInt32(location_x) - 25, Convert.ToInt32(location_y)));
        //                    gr.DrawLine(p1, Convert.ToInt32(Location_X) - 25, Convert.ToInt32(Location_Y) + 20, Convert.ToInt32(Location_X), Convert.ToInt32(Location_Y) + 20);
        //                }
        //                else
        //                {
        //                    gr.DrawString(row, new Font("微软雅黑", 12, FontStyle.Bold), Brushes.Green,
        //                    new Point(Convert.ToInt32(location_x - 25), Convert.ToInt32(location_y)));
        //                    gr.DrawLine(p1, Convert.ToInt32(Location_X - 25), Convert.ToInt32(Location_Y) + 33, Convert.ToInt32(Location_X), Convert.ToInt32(Location_Y) + 33);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception er)
        //    {

        //        LogManager.WriteProgramLog(er.Message, er.StackTrace);
        //    }
        //}

        /// <summary>
        /// 写横坐标
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void panel_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                if (mySaddleInfo != null)
                {
                    Pen p1 = new Pen(Color.White, 2);
                    int layer = mySaddleInfo.Layer_Num;
                    string row;

                        row = "料格号：" + mySaddleInfo.GRID_NO + "\r" + " ，库存重量：" + mySaddleInfo.MAT_WGT;
                        if (myArea.AreaNo == "Z01-1")
                        {
                            gr.DrawString(row, new Font("微软雅黑", 12, FontStyle.Bold), Brushes.Green,
                            new Point(Convert.ToInt32(location_x) - 25, Convert.ToInt32(location_y)));
                            gr.DrawLine(p1, Convert.ToInt32(Location_X) - 25, Convert.ToInt32(Location_Y) + 20, Convert.ToInt32(Location_X), Convert.ToInt32(Location_Y) + 20);
                        }                      
                        else
                        {
                        //gr.DrawString(row, new Font("微软雅黑", 12, FontStyle.Bold), Brushes.Green,
                        //new Point(Convert.ToInt32(location_x - 25), Convert.ToInt32(location_y)));
                        //gr.DrawLine(p1, Convert.ToInt32(Location_X - 25), Convert.ToInt32(Location_Y) + 33, Convert.ToInt32(Location_X), Convert.ToInt32(Location_Y) + 33);


                        //gr.DrawString(row, new Font("微软雅黑", 12, FontStyle.Bold), Brushes.Green, new Point(Convert.ToInt32(location_x - 25), Convert.ToInt32(location_y)));
                        //gr.DrawString("TEST", new Font("微软雅黑", 12, FontStyle.Bold), Brushes.Black, new Point(this.Width / 2 - 30, 0));

                        Rectangle rec = new Rectangle(new Point(Convert.ToInt32(location_x) + 10, Location_Y), new Size(lbl.Width + 1, lbl.Height + 1));
                        gr.DrawRectangle(p1, rec);
                    }
             
                }
            }
            catch (Exception er)
            {

                LogManager.WriteProgramLog(er.Message, er.StackTrace);
            }
        }

        //private void conStockSaddle_Paint(object sender, PaintEventArgs e)
        //{
        //    try
        //    {
        //        Graphics gr = e.Graphics;
        //        Pen p = new Pen(Color.White, 2);
        //        Rectangle rec = new Rectangle(new Point(0, 0), new Size(this.Width, this.Height));
        //        gr.DrawRectangle(p, rec);
        //    }
        //    catch (Exception er)
        //    {

        //    }
        //}

        /// <summary>
        /// 写纵坐标
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void StockSaddle_Paint(object sender, PaintEventArgs e)
        //{
        //    try
        //    {
        //        Graphics gr = this.CreateGraphics();
        //        string col;
        //        if (mySaddleInfo.SaddleNo.Length > 8)
        //        {
        //            col = mySaddleInfo.SaddleNo.Substring(5, 3);
        //        }
        //        else
        //        {
        //            col = mySaddleInfo.SaddleNo.Substring(5, 2);
        //        }

        //        StringFormat str = new StringFormat();
        //        str.Alignment = StringAlignment.Center; //居中
        //        Rectangle 矩形 = new Rectangle(0, 0, this.panel1.Width, this.panel1.Height);
        //        Font size = new Font("微软雅黑", 6F, FontStyle.Regular);
        //        Brush br = Brushes.Green;
        //        if (mySaddleInfo.Layer_Num == 2)
        //        {
        //            gr.DrawString(col + "上", size, br, 矩形, str);
        //        }
        //        else
        //        {
        //            gr.DrawString(col + "下", size, br, 矩形, str);
        //        }
        //    }
        //    catch (Exception er)
        //    {
        //        LogManager.WriteProgramLog(er.Message, er.StackTrace);
        //    }

        //}

        /// <summary>
        /// 写纵坐标
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StockSaddle_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                Graphics gr = this.CreateGraphics();
                string col = mySaddleInfo.GRID_NO;
                StringFormat str = new StringFormat();
                str.Alignment = StringAlignment.Center; //居中
                Rectangle 矩形 = new Rectangle(0, 0, this.panel1.Width, this.panel1.Height);
                Font size = new Font("微软雅黑", 6F, FontStyle.Regular);
                Brush br = Brushes.Green;
                if (mySaddleInfo.Layer_Num == 2)
                {
                    gr.DrawString(col + "上", size, br, 矩形, str);
                }
                else
                {
                    gr.DrawString(col + "下", size, br, 矩形, str);
                }
            }
            catch (Exception er)
            {
                LogManager.WriteProgramLog(er.Message, er.StackTrace);
            }

        }
    }
}
