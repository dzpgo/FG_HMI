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
    public partial class Z73_library_Monitor : FormBase
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
        private conOffinePackingSaddleModel saddleInStock_Z73;

        private bool isShowCurrentBayXY = false;    //是否显示鼠标位置的XY
        private bool tabActived = true;             //是否在当前画面显示

        private string craneNo_1_1 = "1_1";
        private string craneNo_1_2 = "1_2";
        private string craneNo_1_3 = "1_3";

        private const string D373ENTRY = "D373-WR";
        private const string D374ENTRY = "D374-WR";
        private const string D373EXIT = "D373-WC";
        private const string D374EXIT = "D374-WC";
        private const string D408EXIT = "D408-WC";
        private const string D508EXIT = "D508-WC";
        private const string PA_1 = "PA-1";

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
        public Z73_library_Monitor()
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
            saddleInStock_Z73 = new conOffinePackingSaddleModel();
            AreaInStockZ12 = new conAreaModel();
         
            //btnCrane_5_WaterStatus.Click += btnCrane_1_WaterStatus_Click;
            //btnCrane_4_WaterStatus.Click += btnCrane_1_WaterStatus_Click;

            //行车显示控件配置
            conCrane1_3.InitTagDataProvide(constData.tagServiceName);
            conCrane1_3.CraneNO = craneNo_1_3;
            listConCraneDisplay.Add(conCrane1_3);

            conCrane1_2.InitTagDataProvide(constData.tagServiceName);
            conCrane1_2.CraneNO = craneNo_1_2;
            listConCraneDisplay.Add(conCrane1_2);

            conCrane1_1.InitTagDataProvide(constData.tagServiceName);
            conCrane1_1.CraneNO = craneNo_1_1;
            listConCraneDisplay.Add(conCrane1_1);
            
            //---------------------行车状态控件配置-------------------------------
            conCraneStatus1_3.InitTagDataProvide(constData.tagServiceName);
            conCraneStatus1_3.CraneNO = craneNo_1_3;
            lstConCraneStatusPanel.Add(conCraneStatus1_3);

            conCraneStatus1_2.InitTagDataProvide(constData.tagServiceName);
            conCraneStatus1_2.CraneNO = craneNo_1_2;
            lstConCraneStatusPanel.Add(conCraneStatus1_2);

            conCraneStatus1_1.InitTagDataProvide(constData.tagServiceName);
            conCraneStatus1_1.CraneNO = craneNo_1_1;
            lstConCraneStatusPanel.Add(conCraneStatus1_1);
           

            //---------------------行车tag配置--------------------------------
            craneStatusInBay.InitTagDataProvide(constData.tagServiceName);
            craneStatusInBay.AddCraneNO(craneNo_1_1);
            craneStatusInBay.AddCraneNO(craneNo_1_2);
            craneStatusInBay.AddCraneNO(craneNo_1_3); 
            craneStatusInBay.SetReady();


            this.panelZ73Bay.Paint += panelZ11_Z22Bay_Paint;

            //预先加载
            timer_InitializeLoad.Enabled = true;
            timer_InitializeLoad.Interval = 100;
        }

        private void LoadAreaInfo()
        {
            areaModel.conInit(panelZ73Bay,
                constData.bayNo_Z73,
                constData.tagServiceName,
                constData.Z73BaySpaceX,
                constData.Z73BaySpaceY,
                panelZ73Bay.Width,
                panelZ73Bay.Height,
                constData.xBxisleft,
                constData.yBxisDown,
                false,
                 AreaInfo.AreaType.AllType);
        }

        private void LoadUnitInfo()
        {
            unitSaddleModel.conInit(panelZ73Bay,
                D373ENTRY,
                constData.tagServiceName,
                constData.Z73BaySpaceX,
                constData.Z73BaySpaceY,
                panelZ73Bay.Width,
                panelZ73Bay.Height,
                constData.xBxisleft,
                constData.yBxisDown,
                constData.bayNo_Z73);

            unitSaddleModel.conInit(panelZ73Bay,
                D374ENTRY,
                constData.tagServiceName,
                constData.Z73BaySpaceX,
                constData.Z73BaySpaceY,
                panelZ73Bay.Width,
                panelZ73Bay.Height,
                constData.xBxisleft,
                constData.yBxisDown,
                constData.bayNo_Z73);

            unitSaddleModel.conInit(panelZ73Bay,
                D373EXIT,
                constData.tagServiceName,
                constData.Z73BaySpaceX,
                constData.Z73BaySpaceY,
                panelZ73Bay.Width,
                panelZ73Bay.Height,
                constData.xBxisleft,
                constData.yBxisDown,
                constData.bayNo_Z73);

            unitSaddleModel.conInit(panelZ73Bay,
                D374EXIT,
                constData.tagServiceName,
                constData.Z73BaySpaceX,
                constData.Z73BaySpaceY,
                panelZ73Bay.Width,
                panelZ73Bay.Height,
                constData.xBxisleft,
                constData.yBxisDown,
                constData.bayNo_Z73);

            unitSaddleModel.conInit(panelZ73Bay,
                D408EXIT,
                constData.tagServiceName,
                constData.Z73BaySpaceX,
                constData.Z73BaySpaceY,
                panelZ73Bay.Width,
                panelZ73Bay.Height,
                constData.xBxisleft,
                constData.yBxisDown,
                constData.bayNo_Z73);

            unitSaddleModel.conInit(panelZ73Bay,
                D508EXIT,
                constData.tagServiceName,
                constData.Z73BaySpaceX,
                constData.Z73BaySpaceY,
                panelZ73Bay.Width,
                panelZ73Bay.Height,
                constData.xBxisleft,
                constData.yBxisDown,
                constData.bayNo_Z73);

            unitSaddleModel.conInit(panelZ73Bay,
                PA_1,
                constData.tagServiceName,
                constData.Z73BaySpaceX,
                constData.Z73BaySpaceY,
                panelZ73Bay.Width,
                panelZ73Bay.Height,
                constData.xBxisleft,
                constData.yBxisDown,
                constData.bayNo_Z73);

            saddleInStock_Z73.conInit(panelZ73Bay,
                "Z73-PR1",
                constData.tagServiceName,
                constData.Z73BaySpaceX,
                constData.Z73BaySpaceY,
                constData.xBxisleft,
                constData.yBxisDown);

            //saddleInStock_Z22.conInit(panelZ22Bay,
            //    "D172-PC",
            //    constData.tagServiceName,
            //    constData.Z73BaySpaceX,
            //    constData.Z73BaySpaceY,
            //    constData.xBxisleft,
            //    constData.yBxisDown);

            //unitSaddleModel.conInit(panelZ22Bay,
            //    D112EL,
            //    constData.tagServiceName,
            //    constData.Z73BaySpaceX,
            //    constData.Z73BaySpaceY,
            //    panelZ22Bay.Width,
            //    panelZ22Bay.Height,
            //    constData.xBxisleft,
            //    constData.yBxisDown,
            //    constData.bayNo_Z73);

            //unitSaddleModel.conInit(panelZ22Bay,
            //    D208EL,
            //    constData.tagServiceName,
            //    constData.Z73BaySpaceX,
            //    constData.Z73BaySpaceY,
            //    panelZ22Bay.Width,
            //    panelZ22Bay.Height,
            //    constData.xBxisleft,
            //    constData.yBxisDown,
            //    constData.bayNo_Z73);

            //unitSaddleModel.conInit(panelZ73Bay,
            //    "MC1",
            //    constData.tagServiceName,
            //    constData.Z73BaySpaceX,
            //    constData.Z73BaySpaceY,
            //    panelZ73Bay.Width,
            //    panelZ73Bay.Height,
            //    constData.xBxisleft,
            //    constData.yBxisDown,
            //    constData.bayNo_Z73);
        }

        private void LoadParkingCarInfo()
        {
            parkingCarModel.conInit(panelZ73Bay, 
                constData.bayNo_Z73, 
                constData.tagServiceName,
                constData.Z73BaySpaceX,
                constData.Z73BaySpaceY,
                panelZ73Bay.Width,
                panelZ73Bay.Height,
                constData.xBxisleft,
                constData.yBxisDown);

            parkingCarHeadModel.conInit(panelZ73Bay,
                constData.bayNo_Z73,
                constData.tagServiceName,
                constData.Z73BaySpaceX,
                constData.Z73BaySpaceY,
                panelZ73Bay.Width,
                panelZ73Bay.Height,
                constData.xBxisleft,
                constData.yBxisDown);
        }


        private void timer_InitializeLoad_Tick(object sender, EventArgs e)
        {

            LoadUnitInfo();
            LoadParkingCarInfo();
            LoadAreaInfo();
            //GetStatus();
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
                
                AreaInStockZ12.conInit(panelZ73Bay, constData.bayNo_Z73, SaddleBase.tagServiceName,
                       constData.Z73BaySpaceX, constData.Z73BaySpaceY, panelZ73Bay.Width, panelZ73Bay.Height,
                       constData.xBxisleft, constData.yBxisDown,false, AreaInfo.AreaType.StockArea);
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
                         constData.Z73BaySpaceX,
                         constData.Z73BaySpaceY,
                         panelZ73Bay.Width,
                         panelZ73Bay.Height,
                         constData.xBxisleft,
                         constData.yBxisDown,
                         6160,         
                         panelZ73Bay,
                         false
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
            //GetStatus();
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

            LoadUnitInfo();
            LoadParkingCarInfo();
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
                double xScale = Convert.ToDouble(panelZ73Bay.Width) / Convert.ToDouble(constData.Z73BaySpaceX);
                //计算Y方向的比例关系
                double yScale = Convert.ToDouble(panelZ73Bay.Height) / Convert.ToDouble(constData.Z73BaySpaceY);

                Point p = this.panelZ73Bay.PointToClient(Control.MousePosition);
                if (p.X <= this.panelZ73Bay.Location.X || p.X >= this.panelZ73Bay.Location.X + this.panelZ73Bay.Width ||
                    p.Y < this.panelZ73Bay.Location.Y || p.Y >= this.panelZ73Bay.Location.Y + this.panelZ73Bay.Height)
                {
                    return;
                }
                txtX.Text = Convert.ToString(Convert.ToInt32(Convert.ToDouble(p.X) / xScale));
                txtY.Text = Convert.ToString(Convert.ToInt32(Convert.ToDouble(p.Y) / yScale));

                //txtX.Text = Convert.ToString(Convert.ToInt32((Convert.ToDouble(panelZ73Bay.Width) - Convert.ToDouble(p.X)) / xScale));
                //txtY.Text = Convert.ToString(Convert.ToInt32((Convert.ToDouble(panelZ73Bay.Height) - Convert.ToDouble(p.Y)) / yScale));
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
            double xScale = Convert.ToDouble(panelZ73Bay.Width) / Convert.ToDouble(constData.Z73BaySpaceX);
            //计算Y方向的比例关系
            double yScale = Convert.ToDouble(panelZ73Bay.Height) / Convert.ToDouble(constData.Z73BaySpaceY);
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
                Z73_library_Monitor.SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
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
                conCrane1_3.Visible = false;
                conCrane1_2.Visible = false;
                conCrane1_1.Visible = false;
                btnShowCrane.Text = "显示行车";
            }
            else
            {
                conCrane1_3.Visible = true;
                conCrane1_2.Visible = true;
                conCrane1_1.Visible = true;
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
                frmSeekCoil.Z11_Z12Panel = panelZ73Bay;
                frmSeekCoil.Z11_Z12_Width = panelZ73Bay.Width;
                frmSeekCoil.Z11_Z12_Height = panelZ73Bay.Height;
                frmSeekCoil.BayNo = constData.bayNo_Z73;
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
                form.MyLibrary = "Z73";
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
                //               having count(*)>1) and BAY_NO = 'Z22' order by MAT_NO ";
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
        

        //获取状态
        //private void GetStatus()
        //{
        //    List<string> lstAdress = new List<string>();
        //    lstAdress.Clear();
        //    lstAdress.Add("6_4_MODE");
        //    lstAdress.Add("6_5_MODE");
        //    lstAdress.Add("6_6_MODE");
        //    lstAdress.Add("Z07C_MODE");
        //    lstAdress.Add("Z22A_MODE");
        //    lstAdress.Add("D172PC_MODE");
        //    lstAdress.Add("D172PR_MODE");
        //    lstAdress.Add("D172_EXIT_MODE");
        //    lstAdress.Add("D171_EXIT_MODE");
        //    lstAdress.Add("D112_Z22_MODE");
        //    lstAdress.Add("D208_Z22_MODE");
        //    lstAdress.Add("D172PR_2_YARD");
        //    lstAdress.Add("YARD_2_D172PR");
        //    lstAdress.Add("D112_Z22_UNWRAPPED_MODE");
        //    lstAdress.Add("D208_Z22_UNWRAPPED_MODE");
        //    lstAdress.Add("D172_ENTRY_MODE");
        //    lstAdress.Add("D171_ENTRY_MODE");
        //    arrTagAdress = lstAdress.ToArray<string>();
        //    readTags();
        //    if (get_value("6_4_MODE") == "0")
        //    {
        //        btn6_4.Text = "6_4行车状态：正常";
        //        btn6_4.BackColor = Color.LightSteelBlue;
        //    }
        //    else if (get_value("6_4_MODE") == "1")
        //    {
        //        btn6_4.Text = "6_4行车状态：待业";
        //        btn6_4.BackColor = Color.Yellow;
        //    }

        //    if (get_value("6_5_MODE") == "0")
        //    {
        //        btn6_5.Text = "6_5行车状态：正常";
        //        btn6_5.BackColor = Color.LightSteelBlue;
        //    }
        //    else if (get_value("6_5_MODE") == "1")
        //    {
        //        btn6_5.Text = "6_5行车状态：待业";
        //        btn6_5.BackColor = Color.Yellow;
        //    }

        //    if (get_value("6_6_MODE") == "0")
        //    {
        //        btn6_6.Text = "6_6行车状态：正常";
        //        btn6_6.BackColor = Color.LightSteelBlue;
        //    }
        //    else if (get_value("6_6_MODE") == "1")
        //    {
        //        btn6_6.Text = "6_6行车状态：待业";
        //        btn6_6.BackColor = Color.Yellow;
        //    }

        //    if (get_value("Z07C_MODE") == "0")
        //    {
        //        btnZ07C.Text = "Z07C停车位：正常";
        //        btnZ07C.BackColor = Color.LightSteelBlue;
        //    }
        //    else if (get_value("Z07C_MODE") == "1")
        //    {
        //        btnZ07C.Text = "Z07C停车位：加急";
        //        btnZ07C.BackColor = Color.Yellow;
        //    }

        //    if (get_value("Z22A_MODE") == "0")
        //    {
        //        btnZ22A.Text = "Z22A停车位：正常";
        //        btnZ22A.BackColor = Color.LightSteelBlue;
        //    }
        //    else if (get_value("Z22A_MODE") == "1")
        //    {
        //        btnZ22A.Text = "Z22A停车位：加急";
        //        btnZ22A.BackColor = Color.Yellow;
        //    }

        //    if (get_value("D172PC_MODE") == "0")
        //    {
        //        btnD172PC.Text = "拆包吊入行车：6_5";
        //        btnD172PC.BackColor = Color.LightSteelBlue;
        //    }
        //    else if (get_value("D172PC_MODE") == "1")
        //    {
        //        btnD172PC.Text = "拆包吊入行车：非6_5";
        //        btnD172PC.BackColor = Color.LightSteelBlue;
        //    }

        //    if (get_value("D172PR_MODE") == "0")
        //    {
        //        btnD172PR.Text = "消待包行车：6_6";
        //        btnD172PR.BackColor = Color.LightSteelBlue;
        //    }
        //    else if (get_value("D172PR_MODE") == "1")
        //    {
        //        btnD172PR.Text = "消待包行车：6_5";
        //        btnD172PR.BackColor = Color.LightSteelBlue;
        //    }

        //    if (get_value("D172_EXIT_MODE") == "0")
        //    {
        //        btnD172WC.Text = "172收料模式：正常";
        //        btnD172WC.BackColor = Color.LightSteelBlue;
        //    }
        //    else if (get_value("D172_EXIT_MODE") == "1")
        //    {
        //        btnD172WC.Text = "172收料模式：紧急";
        //        btnD172WC.BackColor = Color.Yellow;
        //    }

        //    if (get_value("D171_EXIT_MODE") == "0")
        //    {
        //        btnD171WC.Text = "171收料模式：正常";
        //        btnD171WC.BackColor = Color.LightSteelBlue;
        //    }
        //    else if (get_value("D171_EXIT_MODE") == "1")
        //    {
        //        btnD171WC.Text = "171收料模式：紧急";
        //        btnD171WC.BackColor = Color.Yellow;
        //    }

        //    if (get_value("D112_Z22_MODE") == "0")
        //    {
        //        btnD112EL.Text = "112中间料入库:关";
        //        btnD112EL.BackColor = Color.LightSteelBlue;
        //    }
        //    else if (get_value("D112_Z22_MODE") == "1")
        //    {
        //        btnD112EL.Text = "112中间料入库:开";
        //        btnD112EL.BackColor = Color.Yellow;
        //    }

        //    if (get_value("D208_Z22_MODE") == "0")
        //    {
        //        btnD208EL.Text = "208中间料入库:关";
        //        btnD208EL.BackColor = Color.LightSteelBlue;
        //    }
        //    else if (get_value("D208_Z22_MODE") == "1")
        //    {
        //        btnD208EL.Text = "208中间料入库:开";
        //        btnD208EL.BackColor = Color.Yellow;
        //    }

        //    if (get_value("D172PR_2_YARD") == "0")
        //    {
        //        btnCP_2_YARD.Text = "包装入库模式：正常";
        //        btnCP_2_YARD.BackColor = Color.LightSteelBlue;
        //    }
        //    else if (get_value("D172PR_2_YARD") == "1")
        //    {
        //        btnCP_2_YARD.Text = "包装入库模式：优先";
        //        btnCP_2_YARD.BackColor = Color.Yellow;
        //    }

        //    if (get_value("YARD_2_D172PR") == "0")
        //    {
        //        btnCoil_2_PR.Text = "消待包模式：正常";
        //        btnCoil_2_PR.BackColor = Color.LightSteelBlue;
        //    }
        //    else if (get_value("YARD_2_D172PR") == "1")
        //    {
        //        btnCoil_2_PR.Text = "消待包模式：优先";
        //        btnCoil_2_PR.BackColor = Color.Yellow;
        //    }

        //    if (get_value("D112_Z22_UNWRAPPED_MODE") == "0")
        //    {
        //        btnD112LightCoil.Text = "112成品入库：关";
        //        btnD112LightCoil.BackColor = Color.LightSteelBlue;
        //    }
        //    else if (get_value("D112_Z22_UNWRAPPED_MODE") == "1")
        //    {
        //        btnD112LightCoil.Text = "112成品入库：开";
        //        btnD112LightCoil.BackColor = Color.Yellow;
        //    }

        //    if (get_value("D208_Z22_UNWRAPPED_MODE") == "0")
        //    {
        //        btnD208LightCoil.Text = "208成品入库：关";
        //        btnD208LightCoil.BackColor = Color.LightSteelBlue;
        //    }
        //    else if (get_value("D208_Z22_UNWRAPPED_MODE") == "1")
        //    {
        //        btnD208LightCoil.Text = "208成品入库：开";
        //        btnD208LightCoil.BackColor = Color.Yellow;
        //    }

        //    if (get_value("D172_ENTRY_MODE") == "0")
        //    {
        //        btnD172WR.Text = "172上料行车：默认";
        //        btnD172WR.BackColor = Color.LightSteelBlue;
        //    }
        //    else if (get_value("D172_ENTRY_MODE") == "1")
        //    {
        //        btnD172WR.Text = "172上料行车：6_4";
        //        btnD172WR.BackColor = Color.Yellow;
        //    }
        //    else if (get_value("D172_ENTRY_MODE") == "2")
        //    {
        //        btnD172WR.Text = "172上料行车：6_5";
        //        btnD172WR.BackColor = Color.Yellow;
        //    }
        //    else if (get_value("D172_ENTRY_MODE") == "3")
        //    {
        //        btnD172WR.Text = "172上料行车：6_6";
        //        btnD172WR.BackColor = Color.Yellow;
        //    }

        //    if (get_value("D171_ENTRY_MODE") == "0")
        //    {
        //        btnD171WR.Text = "171上料行车：默认";
        //        btnD171WR.BackColor = Color.LightSteelBlue;
        //    }
        //    else if (get_value("D171_ENTRY_MODE") == "1")
        //    {
        //        btnD171WR.Text = "171上料行车：6_4";
        //        btnD171WR.BackColor = Color.Yellow;
        //    }
        //    else if (get_value("D171_ENTRY_MODE") == "2")
        //    {
        //        btnD171WR.Text = "171上料行车：6_5";
        //        btnD171WR.BackColor = Color.Yellow;
        //    }
        //    else if (get_value("D171_ENTRY_MODE") == "3")
        //    {
        //        btnD171WR.Text = "171上料行车：6_6";
        //        btnD171WR.BackColor = Color.Yellow;
        //    }
        //}
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
    }
}
