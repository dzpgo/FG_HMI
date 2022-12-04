using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Baosight.iSuperframe.Forms;
using Baosight.iSuperframe.Common;
using Baosight.iSuperframe.Authorization.Interface;
using UACSControls;
using UACSDAL;
using System.Threading;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;


namespace UACSView.View_CraneMonitor
{
    public partial class Z23_library_Monitor : FormBase
    {
        private conAreaModel AreaInStockZ12;
        private List<conCraneStatus> lstConCraneStatusPanel = new List<conCraneStatus>();
        private List<conCrane> listConCraneDisplay = new List<conCrane>();
        private List<string> listCraneNo = new List<string>();

        private FrmReCoilUnit frmReCoilUnit;
        private FrmMoreStock form;
        private conAreaModel areaModel;
        private conUnitSaddleModel unitSaddleModel;
        private conParkingCarModel parkingCarModel;
        private conParkingCarHeadModel parkingCarHeadModel;
        private CraneStatusInBay craneStatusInBay;

        private static FrmSeekCoil frmSeekCoil;
        private conStockSaddleModel saddleInStock_Z11_Z12;
        private conOffinePackingSaddleModel saddleInStock_Z23;
        private bool isShowCurrentBayXY = false;    //是否显示鼠标位置的XY
        private bool tabActived = true;             //是否在当前画面显示

        private string craneNo_6_7 = "6_7";
        private string craneNo_6_8 = "6_8";
        private string craneNo_6_9 = "6_9";
        private string craneNo_6_10 = "6_10";
              
        private const string D174ENTRY = "D174-WR";
        private const string D112EXIT = "D112-WC";
        private const string D174EXIT = "D174-WC";

        #region TAG配置
        private Baosight.iSuperframe.TagService.Controls.TagDataProvider tagDataProvider = new Baosight.iSuperframe.TagService.Controls.TagDataProvider();
        Baosight.iSuperframe.TagService.DataCollection<object> inDatas = new Baosight.iSuperframe.TagService.DataCollection<object>();

        private Baosight.iSuperframe.Authorization.Interface.IAuthorization auth = null;

        private string[] arrTagAdress;
        private void readTags()
        {
            try
            {
                inDatas.Clear();
                tagDataProvider.GetData(arrTagAdress, out inDatas);
            }
            catch (Exception ex)
            {
            }
        }
        private string get_value(string tagName)
        {
            string theValue = string.Empty;
            object valueObject = null;
            try
            {
                valueObject = inDatas[tagName];
                theValue = Convert.ToString(valueObject);
            }
            catch
            {
                valueObject = null;
            }
            return theValue; ;
        }
        #endregion

        public Z23_library_Monitor()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
            this.Load += B_library_Monitor_Load;
         
           //btnCrane_5_WaterStatus.Name = craneNo_6_5;
           //btnCrane_4_WaterStatus.Name = craneNo_6_4;
        }

        void btnCrane_1_WaterStatus_Click(object sender, EventArgs e)
        {
            //检查
            Button btn = (Button)sender;
            //if (!AreaInStockZ12.updataCraneWaterLevel(btn.Name))
            //{
            //    MessageBoxButtons btn1 = MessageBoxButtons.OKCancel;
            //    DialogResult dr = MessageBox.Show(btn.Name + "#行车水位没有到达报警值，不能进行放水!", "提示", btn1, MessageBoxIcon.Asterisk);
            //    return;
            //}
            SubFrmLetOutWater frm = new SubFrmLetOutWater(btn.Name);
            frm.ShowDialog();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // Turn on WS_EX_COMPOSITED 
                return cp;
            }
        }
        void B_library_Monitor_Load(object sender, EventArgs e)
        {
            tagDataProvider.ServiceName = "iplature";
            auth = FrameContext.Instance.GetPlugin<IAuthorization>() as IAuthorization;
            getUserName();
            areaModel = new conAreaModel();
            unitSaddleModel = new conUnitSaddleModel();
            parkingCarModel = new conParkingCarModel();
            parkingCarHeadModel = new conParkingCarHeadModel();
            craneStatusInBay = new CraneStatusInBay();
            saddleInStock_Z23 = new conOffinePackingSaddleModel();
            AreaInStockZ12 = new conAreaModel();
         
            //btnCrane_5_WaterStatus.Click += btnCrane_1_WaterStatus_Click;
            //btnCrane_4_WaterStatus.Click += btnCrane_1_WaterStatus_Click;

            //行车显示控件配置
            conCrane6_7.InitTagDataProvide(constData.tagServiceName);
            conCrane6_7.CraneNO = craneNo_6_7;
            listConCraneDisplay.Add(conCrane6_7);

            conCrane6_8.InitTagDataProvide(constData.tagServiceName);
            conCrane6_8.CraneNO = craneNo_6_8;
            listConCraneDisplay.Add(conCrane6_8);

            conCrane6_9.InitTagDataProvide(constData.tagServiceName);
            conCrane6_9.CraneNO = craneNo_6_9;
            listConCraneDisplay.Add(conCrane6_9);

            conCrane6_10.InitTagDataProvide(constData.tagServiceName);
            conCrane6_10.CraneNO = craneNo_6_10;
            listConCraneDisplay.Add(conCrane6_10);
            
            //---------------------行车状态控件配置-------------------------------
            conCraneStatus6_7.InitTagDataProvide(constData.tagServiceName);
            conCraneStatus6_7.CraneNO = craneNo_6_7;
            lstConCraneStatusPanel.Add(conCraneStatus6_7);

            conCraneStatus6_8.InitTagDataProvide(constData.tagServiceName);
            conCraneStatus6_8.CraneNO = craneNo_6_8;
            lstConCraneStatusPanel.Add(conCraneStatus6_8);

            conCraneStatus6_9.InitTagDataProvide(constData.tagServiceName);
            conCraneStatus6_9.CraneNO = craneNo_6_9;
            lstConCraneStatusPanel.Add(conCraneStatus6_9);

            conCraneStatus6_10.InitTagDataProvide(constData.tagServiceName);
            conCraneStatus6_10.CraneNO = craneNo_6_10;
            lstConCraneStatusPanel.Add(conCraneStatus6_10);
           

            //---------------------行车tag配置--------------------------------
            craneStatusInBay.InitTagDataProvide(constData.tagServiceName);
            craneStatusInBay.AddCraneNO(craneNo_6_7);
            craneStatusInBay.AddCraneNO(craneNo_6_8);
            craneStatusInBay.AddCraneNO(craneNo_6_9);
            craneStatusInBay.AddCraneNO(craneNo_6_10); 
            craneStatusInBay.SetReady();


            this.panelZ23Bay.Paint += panelZ11_Z22Bay_Paint;

            //预先加载
            timer_InitializeLoad.Enabled = true;
            timer_InitializeLoad.Interval = 100;
        }

        private void LoadAreaInfo()
        {
            areaModel.conInit(panelZ23Bay,
                constData.bayNo_C,
                constData.tagServiceName,
                constData.Z23BaySpaceX,
                constData.Z23BaySpaceY,
                panelZ23Bay.Width,
                panelZ23Bay.Height,
                constData.xAxisRight,
                constData.yAxisDown,
                 AreaInfo.AreaType.AllType);
        }

        private void LoadUnitInfo()
        {
            unitSaddleModel.conInit(panelZ23Bay,
                D174ENTRY, 
                constData.tagServiceName,
                constData.Z23BaySpaceX,
                constData.Z23BaySpaceY,
                panelZ23Bay.Width,
                panelZ23Bay.Height,
                constData.xAxisRight,
                constData.yAxisDown,
                constData.bayNo_C);

            unitSaddleModel.conInit(panelZ23Bay,
                D112EXIT,
                constData.tagServiceName,
                constData.Z23BaySpaceX,
                constData.Z23BaySpaceY,
                panelZ23Bay.Width,
                panelZ23Bay.Height,
                constData.xAxisRight,
                constData.yAxisDown,
                constData.bayNo_C);

            unitSaddleModel.conInit(panelZ23Bay,
                D174EXIT,
                constData.tagServiceName,
                constData.Z23BaySpaceX,
                constData.Z23BaySpaceY,
                panelZ23Bay.Width,
                panelZ23Bay.Height,
                constData.xAxisRight,
                constData.yAxisDown,
                constData.bayNo_C);

            saddleInStock_Z23.conInit(panelZ23Bay,
                "D112-PR2",
                constData.tagServiceName,
                constData.Z23BaySpaceX,
                constData.Z23BaySpaceY,
                constData.xAxisRight,
                constData.yAxisDown);

            saddleInStock_Z23.conInit(panelZ23Bay,
                "D174-PC",
                constData.tagServiceName,
                constData.Z23BaySpaceX,
                constData.Z23BaySpaceY,
                constData.xAxisRight,
                constData.yAxisDown);

            unitSaddleModel.conInit(panelZ23Bay,
                "PA-1",
                constData.tagServiceName,
                constData.Z23BaySpaceX,
                constData.Z23BaySpaceY,
                panelZ23Bay.Width,
                panelZ23Bay.Height,
                constData.xAxisRight,
                constData.yAxisDown,
                constData.bayNo_C);

            unitSaddleModel.conInit(panelZ23Bay,
                "PA-6",
                constData.tagServiceName,
                constData.Z23BaySpaceX,
                constData.Z23BaySpaceY,
                panelZ23Bay.Width,
                panelZ23Bay.Height,
                constData.xAxisRight,
                constData.yAxisDown,
                constData.bayNo_C);

            unitSaddleModel.conInit(panelZ23Bay,
                "MC1",
                constData.tagServiceName,
                constData.Z23BaySpaceX,
                constData.Z23BaySpaceY,
                panelZ23Bay.Width,
                panelZ23Bay.Height,
                constData.xAxisRight,
                constData.yAxisDown,
                constData.bayNo_C);         
        }

        private void LoadParkingCarInfo()
        {
            parkingCarModel.conInit(panelZ23Bay, 
                constData.bayNo_C, 
                constData.tagServiceName,
                constData.Z23BaySpaceX,
                constData.Z23BaySpaceY,
                panelZ23Bay.Width,
                panelZ23Bay.Height,
                constData.xAxisRight,
                constData.yAxisDown);

            parkingCarHeadModel.conInit(panelZ23Bay,
                constData.bayNo_C,
                constData.tagServiceName,
                constData.Z23BaySpaceX,
                constData.Z23BaySpaceY,
                panelZ23Bay.Width,
                panelZ23Bay.Height,
                constData.xAxisRight,
                constData.yAxisDown);
        }


        private void timer_InitializeLoad_Tick(object sender, EventArgs e)
        {

            LoadUnitInfo();
            LoadParkingCarInfo();
            LoadAreaInfo();
            GetStatus();

            Thread.Sleep(500);
            timerCrane.Enabled = true;
            timerArea.Enabled = true;
            timerUnit.Enabled = true;

            timerCrane.Interval = 1000;
            timerArea.Interval = 10000;
            timerUnit.Interval = 10000;

            timer_InitializeLoad.Enabled = false;

        }
        private void timerCrane_Tick(object sender, EventArgs e)
        {
            if (tabActived == false)
            {
                return;
            }

            craneStatusInBay.getAllPLCStatusInBay(craneStatusInBay.lstCraneNO);

            if (this.Height < 10)
            {
                return;
            }

            try
            {
                
                AreaInStockZ12.conInit(panelZ23Bay, constData.bayNo_C, SaddleBase.tagServiceName,
                       constData.Z23BaySpaceX, constData.Z23BaySpaceY, panelZ23Bay.Width, panelZ23Bay.Height,
                       constData.xAxisRight, constData.yAxisDown, AreaInfo.AreaType.StockArea);
                //-------------------------行车排水状态------------------------------------------
                //getWaterStatus("3_4");
                //getWaterStatus("3_5");
                //btnCrane_4_WaterStatus.Text = AreaInStockZ12.updataCraneWaterLevel(craneNo_6_4.Substring(1, 2)) ? "3_4排水中" : "3_4空调水排放";
                //btnCrane_5_WaterStatus.Text = AreaInStockZ12.updataCraneWaterLevel(craneNo_6_5.Substring(1, 2)) ? "3_5排水中" : "3_5空调水排放";
                //-------------------------行车水位报警------------------------------------------
                //btnCrane_4_WaterStatus.BackColor = AreaInStockZ12.updataCraneWaterLevel(craneNo_6_4) ? Color.Red : (AreaInStockZ12.updataCraneWaterLevel(craneNo_6_4.Substring(2, 1)) ? SystemColors.Highlight : Color.LightSteelBlue);
                //btnCrane_5_WaterStatus.BackColor = AreaInStockZ12.updataCraneWaterLevel(craneNo_6_5) ? Color.Red : (AreaInStockZ12.updataCraneWaterLevel(craneNo_6_5.Substring(2, 1)) ? SystemColors.Highlight : Color.LightSteelBlue);
                //btnCrane_4_WaterStatus.BackColor = AreaInStockZ12.updataCraneWaterLevel(craneNo_6_4) ? Color.Red : Color.LightSteelBlue;
                //btnCrane_5_WaterStatus.BackColor = AreaInStockZ12.updataCraneWaterLevel(craneNo_6_5) ? Color.Red : Color.LightSteelBlue;
                //--------------------------行车指令控件刷新------------------------------------------
                foreach (conCraneStatus conCraneStatusPanel in lstConCraneStatusPanel)
                {
                    conCraneStatus.RefreshControlInvoke ConCraneStatusPanel_Invoke = new conCraneStatus.RefreshControlInvoke(conCraneStatusPanel.RefreshControl);
                    conCraneStatusPanel.BeginInvoke(ConCraneStatusPanel_Invoke, new Object[] { craneStatusInBay.DicCranePLCStatusBase[conCraneStatusPanel.CraneNO].Clone() });
                }
                //--------------------------行车状态控件刷新-------------------------------------------
                foreach (conCrane conCraneVisual in listConCraneDisplay)
                {
                    conCrane.RefreshControlInvoke ConCraneVisual_Invoke = new conCrane.RefreshControlInvoke(conCraneVisual.RefreshControl);
                    conCraneVisual.BeginInvoke(ConCraneVisual_Invoke, new Object[] 
                    { craneStatusInBay.DicCranePLCStatusBase[conCraneVisual.CraneNO].Clone(), 
                         constData.Z23BaySpaceX,
                         constData.Z23BaySpaceY,
                         panelZ23Bay.Width,
                         panelZ23Bay.Height,
                         constData.xAxisRight,
                         constData.yAxisDown,
                         6912,         
                         panelZ23Bay 
                    });
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(string.Format("{0},{1}", er.Message, er.Source));
                timerCrane.Enabled = false;
            }

        }

        private void timerArea_Tick(object sender, EventArgs e)
        {
            if (tabActived == false)
            {
                return;
            }
            if (this.Height < 10)
            {
                return;
            }

            LoadAreaInfo();
            showMoreStock();
            showReCoilUnit();
            GetStatus();
        }

        private void timerUnit_Tick(object sender, EventArgs e)
        {
            if (tabActived == false)
            {
                return;
            }
            if (this.Height < 10)
            {
                return;
            }
            LoadParkingCarInfo();
            LoadUnitInfo();

        }

        private void timerClear_Tick(object sender, EventArgs e)
        {
            if (tabActived == false)
            {
                return;
            }
            try
            {
                ClearMemory();
            }
            catch (Exception er)
            {
            }
        }

        private void timer_ShowXY_Tick(object sender, EventArgs e)
        {
            if (tabActived == false)
            {
                return;
            }
            if (this.Height < 10)
            {
                return;
            }
            if (isShowCurrentBayXY)
            {
                //计算X方向上的比例关系
                double xScale = Convert.ToDouble(panelZ23Bay.Width) / Convert.ToDouble(constData.Z23BaySpaceX);
                //计算Y方向的比例关系
                double yScale = Convert.ToDouble(panelZ23Bay.Height) / Convert.ToDouble(constData.Z23BaySpaceY);

                Point p = this.panelZ23Bay.PointToClient(Control.MousePosition);
                if (p.X <= this.panelZ23Bay.Location.X || p.X >= this.panelZ23Bay.Location.X + this.panelZ23Bay.Width ||
                    p.Y < this.panelZ23Bay.Location.Y || p.Y >= this.panelZ23Bay.Location.Y + this.panelZ23Bay.Height)
                {
                    return;
                }
                txtX.Text = Convert.ToString(Convert.ToInt32(Convert.ToDouble(p.X) / xScale));
                txtY.Text = Convert.ToString(Convert.ToInt32(Convert.ToDouble(p.Y)  / yScale));
            }
        }

        //绘图
        void panelZ11_Z22Bay_Paint(object sender, PaintEventArgs e)
        {
            #region 引用对象
            Graphics gr = e.Graphics;
            #endregion

            #region 比例换算
            //计算X方向上的比例关系
            double xScale = Convert.ToDouble(panelZ23Bay.Width) / Convert.ToDouble(constData.Z23BaySpaceX);
            //计算Y方向的比例关系
            double yScale = Convert.ToDouble(panelZ23Bay.Height) / Convert.ToDouble(constData.Z23BaySpaceY);
            #endregion

            HatchBrush mybrush1 = new HatchBrush(
                          HatchStyle.HorizontalBrick,
                           Color.Black,
                           Color.Red);
            //gr.FillRectangle(mybrush1, Convert.ToInt32(Convert.ToDouble(265450) * xScale), 0, 
            //    Convert.ToInt32(Convert.ToDouble(268500-265450) * xScale), this.panelBBay.Height);
            //gr.DrawString("9999", new Font("微软雅黑", 10, FontStyle.Bold), Brushes.White, new Point(Convert.ToInt32(Convert.ToDouble(265450) * xScale), 10));
        } 

      

        #region -----------------------------画面切换--------------------------------
        void MyTabActivated(object sender, EventArgs e)
        {
            tabActived = true;
        }
        void MyTabDeactivated(object sender, EventArgs e)
        {
            tabActived = false;
        }
        #endregion


        #region  -----------------------------内存回收--------------------------------
        [DllImport("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize")]
        public static extern int SetProcessWorkingSetSize(IntPtr process, int minSize, int maxSize);
        /// <summary>
        /// 释放内存
        /// </summary>
        public static void ClearMemory()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                Z23_library_Monitor.SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
            }
        }
        #endregion

        private void btnShowXY_Click(object sender, EventArgs e)
        {
            if (!isShowCurrentBayXY)
            {
                isShowCurrentBayXY = true;
                btnShowXY.Text = "取消显示";

                timer_ShowXY.Enabled = true;
                timer_ShowXY.Interval = 1000;
            }
            else
            {
                isShowCurrentBayXY = false;
                btnShowXY.Text = "显示XY";
                timer_ShowXY.Enabled = false;

            }
        }

        private void btnShowCrane_Click(object sender, EventArgs e)
        {
            if (btnShowCrane.Text == "隐藏行车")
            {
                conCrane6_7.Visible = false;
                conCrane6_8.Visible = false;
                conCrane6_9.Visible = false;
                conCrane6_10.Visible = false;
                btnShowCrane.Text = "显示行车";
            }
            else
            {
                conCrane6_7.Visible = true;
                conCrane6_8.Visible = true;
                conCrane6_9.Visible = true;
                conCrane6_10.Visible = true;
                btnShowCrane.Text = "隐藏行车";
            }
        }

        private void panelZ11_Z22Bay_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void btnSeekCoil_Click(object sender, EventArgs e)
        {
            if (frmSeekCoil == null || frmSeekCoil.IsDisposed)
            {
                frmSeekCoil = new FrmSeekCoil();
                frmSeekCoil.saddleInStock_Z11_Z12 = saddleInStock_Z11_Z12;
                frmSeekCoil.Z11_Z12Panel = panelZ23Bay;
                frmSeekCoil.Z11_Z12_Width = panelZ23Bay.Width;
                frmSeekCoil.Z11_Z12_Height = panelZ23Bay.Height;
                frmSeekCoil.BayNo = constData.bayNo_C;
                frmSeekCoil.Show();
            }
            else
            {
                frmSeekCoil.WindowState = FormWindowState.Normal;
                frmSeekCoil.Activate();
            }
        }

        private void getWaterStatus(string craneNo)
        {
            string status;
            string sql = @"select STATUS from CRANE_PIPI where CRANE_NO =  '" + craneNo + "'";
            using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
            {
                while (rdr.Read())
                {
                    if (rdr["STATUS"] != System.DBNull.Value)
                    {
                        status = rdr["STATUS"].ToString();
                        
                        if (status == "TO_BE_START")
                        {
                            switch(craneNo)
                            {
                                case "3_4":
                                    btnCrane_4_WaterStatus.Text = "3_4准备排水";
                                    break;
                                case "3_5":
                                    btnCrane_5_WaterStatus.Text = "3_5准备排水";
                                    break;                               
                                default:
                                    break;
                            }                             
                        }
                        else if (status == "STARTED")
                        {
                            switch (craneNo)
                            {
                                case "3_4":
                                    btnCrane_4_WaterStatus.Text = "3_4排水中";
                                    break;
                                case "3_5":
                                    btnCrane_5_WaterStatus.Text = "3_5排水中";
                                    break;                               
                                default:
                                    break;
                            }
                        }
                        else if (status == "TO_BE_END")
                        {
                            switch (craneNo)
                            {
                                case "3_4":
                                    btnCrane_4_WaterStatus.Text = "3_4结束排水";
                                    break;
                                case "3_5":
                                    btnCrane_5_WaterStatus.Text = "3_5结束排水";
                                    break;                             
                                default:
                                    break;
                            }
                        }
                        else
                        {
                            switch (craneNo)
                            {
                                case "3_4":
                                    btnCrane_4_WaterStatus.Text = "3_4空调水排放";
                                    break;
                                case "3_5":
                                    btnCrane_5_WaterStatus.Text = "3_5空调水排放";
                                    break;                               
                                default:
                                    break;
                            }
                        }
                    }

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (form == null || form.IsDisposed)
            {
                form = new FrmMoreStock();
                form.MyLibrary = "Z23";
                form.Show();
            }
            else
            {
                form.WindowState = FormWindowState.Normal;
                form.Activate();
            }
        }

        //多库位按钮提示
        private void showMoreStock()
        {
            if (tabActived == false)
            {
                return;
            }
            try
            {

                //string sql = @" select STOCK_NO,MAT_NO,BAY_NO from UACS_YARDMAP_STOCK_DEFINE where MAT_NO in (select MAT_NO from UACS_YARDMAP_STOCK_DEFINE group by MAT_NO
                //               having count(*)>1) and BAY_NO = 'Z23' order by MAT_NO ";

                string sql = @" select STOCK_NO,MAT_NO,BAY_NO from UACS_YARDMAP_STOCK_DEFINE where MAT_NO in (select MAT_NO from UACS_YARDMAP_STOCK_DEFINE group by MAT_NO
                               having count(*)>1) order by MAT_NO ";
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        button1.BackColor = Color.Red;
                    }
                    else
                    {
                        button1.BackColor = Color.LightSteelBlue;
                    }
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, er.StackTrace);
            }
        }

        private void btnReCoilUnit_Click(object sender, EventArgs e)
        {
            if (frmReCoilUnit == null || frmReCoilUnit.IsDisposed)
            {
                frmReCoilUnit = new FrmReCoilUnit();
                frmReCoilUnit.DicUnit = dicUnit;
                frmReCoilUnit.Show();
            }
            else
            {
                frmReCoilUnit.WindowState = FormWindowState.Normal;
                frmReCoilUnit.Activate();
            }
        }
        private int unitShowCount = 0;
        private Dictionary<string, bool> dicUnit = new Dictionary<string, bool>();
        //重卷机组按钮提示
        private void showReCoilUnit()
        {
            unitShowCount = 0;
            if (tabActived == false)
            {
                return;
            }
            try
            {
                WRUnitSaddle();
                WCUnitSaddle();
                if (unitShowCount == 0)
                {
                    btnReCoilUnit.BackColor = Color.LightSteelBlue;
                }
                else if (unitShowCount > 0)
                {
                    btnReCoilUnit.BackColor = Color.Red;
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, er.StackTrace);
            }
        }

        private void WRUnitSaddle()
        {
            List<string> listReCoilUnit = new List<string>();
            listReCoilUnit.Clear();
            listReCoilUnit.Add("D171WR");
            listReCoilUnit.Add("D172WR");
            listReCoilUnit.Add("D173WR");
            listReCoilUnit.Add("D174WR");
            try
            {
                foreach (string unitStock in listReCoilUnit)
                {
                    List<string> lstAdress = new List<string>();
                    int count = 0;
                    string sqlText = @"SELECT A.STOCK_NO STOCK_NO,A.TAG_ISEMPTY TAG_ISEMPTY FROM UACS_LINE_SADDLE_DEFINE A ";
                    //string sqlText = @"SELECT A.STOCK_NO STOCK_NO,A.TAG_ISEMPTY TAG_ISEMPTY,B.COIL_NO COIL_NO, C.STOCK_STATUS STOCK_STATUS 
                    //           FROM UACS_LINE_SADDLE_DEFINE A ";
                    //LEFT JOIN UACS_LINE_ENTRY_L2INFO B ON B.UNIT_NO= A.UNIT_NO AND B.SADDLE_L2NAME = A.SADDLE_L2NAME
                    //LEFT JOIN UACS_YARDMAP_STOCK_DEFINE C ON C.STOCK_NO = A.STOCK_NO
                    sqlText += "WHERE A.STOCK_NO LIKE '%" + unitStock + "%'";
                    using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sqlText))
                    {
                        while (rdr.Read())
                        {
                            lstAdress.Clear();
                            lstAdress.Add(rdr["TAG_ISEMPTY"].ToString().Trim());
                            arrTagAdress = lstAdress.ToArray<string>();
                            readTags();
                            if (get_value(rdr["TAG_ISEMPTY"].ToString().Trim()) == "1")
                            //if (rdr["COIL_NO"] != System.DBNull.Value && rdr["STOCK_STATUS"].ToString() == "2")
                            {
                                count++;
                            }
                        }
                    }
                    if (count < 1)
                    {
                        unitShowCount++;
                        dicUnit[unitStock] = true;
                    }
                    else
                    {
                        dicUnit[unitStock] = false;
                    }
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.ToString());
            }
        }

        private void WCUnitSaddle()
        {
            List<string> listReCoilUnit = new List<string>();
            listReCoilUnit.Clear();
            listReCoilUnit.Add("D171WC");
            listReCoilUnit.Add("D172WC");
            listReCoilUnit.Add("D173WC");
            listReCoilUnit.Add("D174WC");
            try
            {
                foreach (string unitStock in listReCoilUnit)
                {
                    List<string> lstAdress = new List<string>();
                    int count = 0;
                    string sqlText = @"SELECT A.STOCK_NO STOCK_NO,A.TAG_ISEMPTY TAG_ISEMPTY FROM UACS_LINE_SADDLE_DEFINE A ";
                    //string sqlText = @"SELECT A.STOCK_NO STOCK_NO,A.TAG_ISEMPTY TAG_ISEMPTY,B.COIL_NO COIL_NO, C.STOCK_STATUS STOCK_STATUS 
                    //FROM UACS_LINE_SADDLE_DEFINE A ";
                    //LEFT JOIN UACS_LINE_EXIT_L2INFO B ON B.UNIT_NO= A.UNIT_NO AND B.SADDLE_L2NAME = A.SADDLE_L2NAME
                    //LEFT JOIN UACS_YARDMAP_STOCK_DEFINE C ON C.STOCK_NO = A.STOCK_NO
                    sqlText += "WHERE A.STOCK_NO LIKE '%" + unitStock + "%'";
                    using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sqlText))
                    {
                        while (rdr.Read())
                        {
                            lstAdress.Clear();
                            lstAdress.Add(rdr["TAG_ISEMPTY"].ToString().Trim());
                            arrTagAdress = lstAdress.ToArray<string>();
                            readTags();
                            if (get_value(rdr["TAG_ISEMPTY"].ToString().Trim()) == "1")
                            //if (rdr["COIL_NO"] != System.DBNull.Value && rdr["STOCK_STATUS"].ToString() == "2")
                            {
                                count++;
                            }
                        }
                    }
                    if (count >= 2)
                    {
                        unitShowCount++;
                        dicUnit[unitStock] = true;
                    }
                    else
                    {
                        dicUnit[unitStock] = false;
                    }
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.ToString());
            }
        }

        //用户功能限制
        private void getUserName()
        {
            if (!auth.GetUserName().Contains("admin") && !auth.GetUserName().Contains("ZYBG"))
            {
                btnShow.Visible = false;
                panelShow.Visible = false;
            }
        }

        //获取状态
        private void GetStatus()
        {
            List<string> lstAdress = new List<string>();
            lstAdress.Clear();
            lstAdress.Add("PA-1_IS_BUSY");
            lstAdress.Add("PA-1_READY");
            lstAdress.Add("PA-6_READY");
            lstAdress.Add("PA-1_MID_COIL_TO_YARD");
            arrTagAdress = lstAdress.ToArray<string>();
            readTags();
            if (get_value("PA-1_IS_BUSY") == "0")
            {
                btnD112PRStatus.Text = "D112包装：正常";
                btnD112PRStatus.BackColor = Color.LightSteelBlue;
            }
            else if (get_value("PA-1_IS_BUSY") == "1")
            {
                btnD112PRStatus.Text = "D112包装：繁忙";
                btnD112PRStatus.BackColor = Color.Yellow;
            }
            if (get_value("PA-1_READY") == "0")
            {
                btnD112LightCoil.Text = "D112光卷：关闭";
                btnD112LightCoil.BackColor = Color.LightSteelBlue;
            }
            else if (get_value("PA-1_READY") == "1")
            {
                btnD112LightCoil.Text = "D112光卷：打开";
                btnD112LightCoil.BackColor = Color.Yellow;
            }
            if (get_value("PA-6_READY") == "0")
            {
                btnD174LightCoil.Text = "D174光卷：关闭";
                btnD174LightCoil.BackColor = Color.LightSteelBlue;
            }
            else if (get_value("PA-6_READY") == "1")
            {
                btnD174LightCoil.Text = "D174光卷：打开";
                btnD174LightCoil.BackColor = Color.Yellow;
            }
            if (get_value("PA-1_MID_COIL_TO_YARD") == "0")
            {
                btnMidCoilToYard.Text = "中间料入库：关闭";
                btnMidCoilToYard.BackColor = Color.LightSteelBlue;
            }
            else if (get_value("PA-1_MID_COIL_TO_YARD") == "1")
            {
                btnMidCoilToYard.Text = "中间料入库：打开";
                btnMidCoilToYard.BackColor = Color.Yellow;
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            if (btnShow.Text == "<")
            {
                panelShow.Visible = true;
                btnShow.Text = ">";
            }
            else
            {
                panelShow.Visible = false;
                btnShow.Text = "<";
            }
        }

        private void btnD112PRStatus_Click(object sender, EventArgs e)
        {
            try
            {
                if (DialogResult.OK == MessageBox.Show("确定要修改D112包装状态吗？", "Warning", MessageBoxButtons.OKCancel))
                {
                    if (btnD112PRStatus.Text.Contains("正常"))
                    {
                        string value = "1";
                        inDatas["PA-1_IS_BUSY"] = value;
                        tagDataProvider.Write2Level1(inDatas, 1);
                        MessageBox.Show("已打开D112包装繁忙状态！");
                        btnD112PRStatus.Text = "D112包装：繁忙";
                        btnD112PRStatus.BackColor = Color.Yellow;
                    }
                    else if (btnD112PRStatus.Text.Contains("繁忙"))
                    {
                        string value = "0";
                        inDatas["PA-1_IS_BUSY"] = value;
                        tagDataProvider.Write2Level1(inDatas, 1);
                        MessageBox.Show("已恢复D112包装正常状态！");
                        btnD112PRStatus.Text = "D112包装：正常";
                        btnD112PRStatus.BackColor = Color.LightSteelBlue;
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }
        }

        private void btnD112LightCoil_Click(object sender, EventArgs e)
        {
            try
            {
                if (DialogResult.OK == MessageBox.Show("确定要修改D112光卷入库状态吗？", "Warning", MessageBoxButtons.OKCancel))
                {
                    if (btnD112LightCoil.Text.Contains("关闭"))
                    {
                        string value = "1";
                        inDatas["PA-1_READY"] = value;
                        tagDataProvider.Write2Level1(inDatas, 1);
                        MessageBox.Show("已打开光卷入库吊运！");
                        btnD112LightCoil.Text = "D112光卷：打开";
                        btnD112LightCoil.BackColor = Color.Yellow;
                    }
                    else if (btnD112LightCoil.Text.Contains("打开"))
                    {
                        string value = "0";
                        inDatas["PA-1_READY"] = value;
                        tagDataProvider.Write2Level1(inDatas, 1);
                        MessageBox.Show("已关闭光卷入库吊运！");
                        btnD112LightCoil.Text = "D112光卷：关闭";
                        btnD112LightCoil.BackColor = Color.LightSteelBlue;
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }
        }

        private void btnD174LightCoil_Click(object sender, EventArgs e)
        {
            try
            {
                if (DialogResult.OK == MessageBox.Show("确定要修改D174光卷入库状态吗？", "Warning", MessageBoxButtons.OKCancel))
                {
                    if (btnD174LightCoil.Text.Contains("关闭"))
                    {
                        string value = "1";
                        inDatas["PA-6_READY"] = value;
                        tagDataProvider.Write2Level1(inDatas, 1);
                        MessageBox.Show("已打开光卷入库吊运！");
                        btnD174LightCoil.Text = "D174光卷：打开";
                        btnD174LightCoil.BackColor = Color.Yellow;
                    }
                    else if (btnD174LightCoil.Text.Contains("打开"))
                    {
                        string value = "0";
                        inDatas["PA-6_READY"] = value;
                        tagDataProvider.Write2Level1(inDatas, 1);
                        MessageBox.Show("已关闭光卷入库吊运！");
                        btnD174LightCoil.Text = "D174光卷：关闭";
                        btnD174LightCoil.BackColor = Color.LightSteelBlue;
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }
        }

        private void btnMidCoilToYard_Click(object sender, EventArgs e)
        {
            try
            {
                if (DialogResult.OK == MessageBox.Show("确定要修改中间料入库状态吗？", "Warning", MessageBoxButtons.OKCancel))
                {
                    if (btnMidCoilToYard.Text.Contains("关闭"))
                    {
                        string value = "1";
                        inDatas["PA-1_MID_COIL_TO_YARD"] = value;
                        tagDataProvider.Write2Level1(inDatas, 1);
                        MessageBox.Show("已打开中间料入库吊运！");
                        btnMidCoilToYard.Text = "中间料入库：打开";
                        btnMidCoilToYard.BackColor = Color.Yellow;
                    }
                    else if (btnMidCoilToYard.Text.Contains("打开"))
                    {
                        string value = "0";
                        inDatas["PA-1_MID_COIL_TO_YARD"] = value;
                        tagDataProvider.Write2Level1(inDatas, 1);
                        MessageBox.Show("已关闭中间料入库吊运！");
                        btnMidCoilToYard.Text = "中间料入库：关闭";
                        btnMidCoilToYard.BackColor = Color.LightSteelBlue;
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }
        }
    }
}
