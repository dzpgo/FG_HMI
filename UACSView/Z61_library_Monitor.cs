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
    public partial class Z41_library_Monitor : FormBase
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
        private conOffinePackingSaddleModel saddleInStock_Z21;

        private bool isShowCurrentBayXY = false;    //是否显示鼠标位置的XY
        private bool tabActived = true;             //是否在当前画面显示

        private string craneNo_4_1 = "4_1";
        private string craneNo_4_2 = "4_2";
        //private string craneNo_1_3 = "1_3";
       
        private const string D508ENTRY = "D508-WR";
        private const string D122ENTRY = "D122-WR";
        private const string D302EXIT = "D302-WC";

        private List<string> listReCoilUnit = new List<string>();

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

        public Z41_library_Monitor()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
            this.Load += A_library_Monitor_Load;

            //btnCrane_1_WaterStatus.Name = craneNo_4_1;
            //btnCrane_2_WaterStatus.Name = craneNo_4_2;
            //btnCrane_3_WaterStatus.Name = craneNo_1_3;
            
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
        void A_library_Monitor_Load(object sender, EventArgs e)
        {
            tagDataProvider.ServiceName = "iplature";
            auth = FrameContext.Instance.GetPlugin<IAuthorization>() as IAuthorization;
            getUserName();
            areaModel = new conAreaModel();
            unitSaddleModel = new conUnitSaddleModel();
            parkingCarModel = new conParkingCarModel();
            parkingCarHeadModel = new conParkingCarHeadModel();
            craneStatusInBay = new CraneStatusInBay();

            AreaInStockZ12 = new conAreaModel();
            saddleInStock_Z21 = new conOffinePackingSaddleModel();

            //btnCrane_1_WaterStatus.Click += btnCrane_1_WaterStatus_Click;
            //btnCrane_2_WaterStatus.Click += btnCrane_1_WaterStatus_Click;
            //btnCrane_3_WaterStatus.Click += btnCrane_1_WaterStatus_Click;          

            //行车显示控件配置

            conCrane4_1.InitTagDataProvide(constData.tagServiceName);
            conCrane4_1.CraneNO = craneNo_4_1;
            listConCraneDisplay.Add(conCrane4_1);

            conCrane4_2.InitTagDataProvide(constData.tagServiceName);
            conCrane4_2.CraneNO = craneNo_4_2;
            listConCraneDisplay.Add(conCrane4_2);

            //conCrane1_3.InitTagDataProvide(constData.tagServiceName);
            //conCrane1_3.CraneNO = craneNo_1_3;
            //listConCraneDisplay.Add(conCrane1_3);



            //---------------------行车状态控件配置-------------------------------
            conCraneStatus4_1.InitTagDataProvide(constData.tagServiceName);
            conCraneStatus4_1.CraneNO = craneNo_4_1;
            lstConCraneStatusPanel.Add(conCraneStatus4_1);

            conCraneStatus4_2.InitTagDataProvide(constData.tagServiceName);
            conCraneStatus4_2.CraneNO = craneNo_4_2;
            lstConCraneStatusPanel.Add(conCraneStatus4_2);

            //conCraneStatus1_2.InitTagDataProvide(constData.tagServiceName);
            //conCraneStatus1_2.CraneNO = craneNo_1_1;
            //lstConCraneStatusPanel.Add(conCraneStatus1_2);

            //conCraneStatus1_4.InitTagDataProvide(constData.tagServiceName);
            //conCraneStatus1_4.CraneNO = craneNo_1_4;
            //lstConCraneStatusPanel.Add(conCraneStatus1_4);


            //---------------------行车tag配置--------------------------------
            craneStatusInBay.InitTagDataProvide(constData.tagServiceName);
            craneStatusInBay.AddCraneNO(craneNo_4_1);
            craneStatusInBay.AddCraneNO(craneNo_4_2);
            //craneStatusInBay.AddCraneNO(craneNo_1_3);
            craneStatusInBay.SetReady();


            this.panelZ62Bay.Paint += panelZ11_Z22Bay_Paint;

            //预先加载
            timer_InitializeLoad.Enabled = true;
            timer_InitializeLoad.Interval = 100;
        }

        private void LoadAreaInfo()
        {
            areaModel.conInit(panelZ62Bay,
                constData.bayNo_Z41,
                constData.tagServiceName,
                constData.Z41BaySpaceX,
                constData.Z41BaySpaceY,
                panelZ62Bay.Width,
                panelZ62Bay.Height,
                constData.xBxisleft,
                constData.yAxisDown,
                 AreaInfo.AreaType.AllType);
        }

        private void LoadUnitInfo()
        {
            unitSaddleModel.conInit(panelZ62Bay,
                D122ENTRY,
                constData.tagServiceName,
                constData.Z41BaySpaceX,
                constData.Z41BaySpaceY,
                panelZ62Bay.Width,
                panelZ62Bay.Height,
                constData.xBxisleft,
                constData.yAxisDown,
                constData.bayNo_Z41);

            unitSaddleModel.conInit(panelZ62Bay,
                D508ENTRY,
                constData.tagServiceName,
                constData.Z41BaySpaceX,
                constData.Z41BaySpaceY,
                panelZ62Bay.Width,
                panelZ62Bay.Height,
                constData.xBxisleft,
                constData.yAxisDown,
                constData.bayNo_Z41);

            unitSaddleModel.conInit(panelZ62Bay,
                D302EXIT,
                constData.tagServiceName,
                constData.Z41BaySpaceX,
                constData.Z41BaySpaceY,
                panelZ62Bay.Width,
                panelZ62Bay.Height,
                constData.xBxisleft,
                constData.yAxisDown,
                constData.bayNo_Z41);

            //unitSaddleModel.conInit(panelZ62Bay,
            //    "PA-2",
            //    constData.tagServiceName,
            //    constData.Z41BaySpaceX,
            //    constData.Z41BaySpaceY,
            //    panelZ62Bay.Width,
            //    panelZ62Bay.Height,
            //    constData.xBxisleft,
            //    constData.yAxisDown,
            //    constData.bayNo_Z41);

            //saddleInStock_Z21.conInit(panelZ62Bay,
            //    "D173-PR",
            //    constData.tagServiceName,
            //    constData.Z41BaySpaceX,
            //    constData.Z41BaySpaceY,
            //    constData.xBxisleft,
            //    constData.yAxisDown);

            //saddleInStock_Z21.conInit(panelZ62Bay,
            //    "D173-PC",
            //    constData.tagServiceName,
            //    constData.Z41BaySpaceX,
            //    constData.Z41BaySpaceY,
            //    constData.xBxisleft,
            //    constData.yAxisDown);

            unitSaddleModel.conInit(panelZ62Bay,
                "MC1",
                constData.tagServiceName,
                constData.Z41BaySpaceX,
                constData.Z41BaySpaceY,
                panelZ62Bay.Width,
                panelZ62Bay.Height,
                constData.xBxisleft,
                constData.yAxisDown,
                constData.bayNo_Z41);
        }

        private void LoadParkingCarInfo()
        {
            parkingCarModel.conInit(panelZ62Bay, 
                constData.bayNo_Z41, 
                constData.tagServiceName,
                constData.Z41BaySpaceX,
                constData.Z41BaySpaceY,
                panelZ62Bay.Width,
                panelZ62Bay.Height,
                constData.xBxisleft,
                constData.yAxisDown);

            parkingCarHeadModel.conInit(panelZ62Bay,
                constData.bayNo_Z41,
                constData.tagServiceName,
                constData.Z41BaySpaceX,
                constData.Z41BaySpaceY,
                panelZ62Bay.Width,
                panelZ62Bay.Height,
                constData.xBxisleft,
                constData.yAxisDown);
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

                AreaInStockZ12.conInit(panelZ62Bay, constData.bayNo_Z41, SaddleBase.tagServiceName,
                       constData.Z41BaySpaceX, constData.Z41BaySpaceY, panelZ62Bay.Width, panelZ62Bay.Height,
                       constData.xBxisleft, constData.yAxisDown, AreaInfo.AreaType.StockArea);
                //-------------------------行车排水状态------------------------------------------               
                //getWaterStatus("3_1");
                //getWaterStatus("3_2");
                //getWaterStatus("3_3");
                //btnCrane_1_WaterStatus.Text = AreaInStockZ12.updataCraneWaterLevel(craneNo_1_1.Substring(1, 2)) ? "3_1排水中" : "3_1空调水排放";
                //btnCrane_2_WaterStatus.Text = AreaInStockZ12.updataCraneWaterLevel(craneNo_1_2.Substring(1, 2)) ? "3_2排水中" : "3_2空调水排放";
                //btnCrane_3_WaterStatus.Text = AreaInStockZ12.updataCraneWaterLevel(craneNo_1_3.Substring(1, 2)) ? "3_3排水中" : "3_3空调水排放";
                //-------------------------行车水位报警------------------------------------------
                //btnCrane_1_WaterStatus.BackColor = AreaInStockZ12.updataCraneWaterLevel(craneNo_1_1) ? Color.Red : (AreaInStockZ12.updataCraneWaterLevel(craneNo_1_1.Substring(2, 1)) ? SystemColors.Highlight : Color.LightSteelBlue);
                //btnCrane_2_WaterStatus.BackColor = AreaInStockZ12.updataCraneWaterLevel(craneNo_1_2) ? Color.Red : AreaInStockZ12.updataCraneWaterLevel(craneNo_1_2.Substring(2, 1)) ? SystemColors.Highlight : Color.LightSteelBlue;
                //btnCrane_3_WaterStatus.BackColor = AreaInStockZ12.updataCraneWaterLevel(craneNo_1_3) ? Color.Red : AreaInStockZ12.updataCraneWaterLevel(craneNo_1_3.Substring(2, 1)) ? SystemColors.Highlight : Color.LightSteelBlue;
                //btnCrane_1_WaterStatus.BackColor = AreaInStockZ12.updataCraneWaterLevel(craneNo_1_1) ? Color.Red : Color.LightSteelBlue;
                //btnCrane_2_WaterStatus.BackColor = AreaInStockZ12.updataCraneWaterLevel(craneNo_1_2) ? Color.Red : Color.LightSteelBlue;
                //btnCrane_3_WaterStatus.BackColor = AreaInStockZ12.updataCraneWaterLevel(craneNo_1_3) ? Color.Red : Color.LightSteelBlue;

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
                         constData.Z41BaySpaceX,
                         constData.Z41BaySpaceY,
                         panelZ62Bay.Width,
                         panelZ62Bay.Height,
                         constData.xBxisleft,
                         constData.yAxisDown,
                         6160,         
                         panelZ62Bay 
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
                double xScale = Convert.ToDouble(panelZ62Bay.Width) / Convert.ToDouble(constData.Z41BaySpaceX);
                //计算Y方向的比例关系
                double yScale = Convert.ToDouble(panelZ62Bay.Height) / Convert.ToDouble(constData.Z41BaySpaceY);

                Point p = this.panelZ62Bay.PointToClient(Control.MousePosition);
                if (p.X <= this.panelZ62Bay.Location.X || p.X >= this.panelZ62Bay.Location.X + this.panelZ62Bay.Width ||
                    p.Y < this.panelZ62Bay.Location.Y || p.Y >= this.panelZ62Bay.Location.Y + this.panelZ62Bay.Height)
                {
                    return;
                }
                //txtX.Text = Convert.ToString(Convert.ToInt32(Convert.ToDouble(p.X) / xScale));
                //txtY.Text = Convert.ToString(Convert.ToInt32((Convert.ToDouble(panelZ62Bay.Height)-Convert.ToDouble(p.Y)) / yScale));

                txtX.Text = Convert.ToString(Convert.ToInt32((Convert.ToDouble(panelZ62Bay.Width) - Convert.ToDouble(p.X)) / xScale));
                txtY.Text = Convert.ToString(Convert.ToInt32(Convert.ToDouble(p.Y) / yScale));
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
            double xScale = Convert.ToDouble(panelZ62Bay.Width) / Convert.ToDouble(constData.Z41BaySpaceX);
            //计算Y方向的比例关系
            double yScale = Convert.ToDouble(panelZ62Bay.Height) / Convert.ToDouble(constData.Z41BaySpaceY);
            #endregion

            HatchBrush mybrush1 = new HatchBrush(
                          HatchStyle.HorizontalBrick,
                           Color.Black,
                           Color.Silver);
            //gr.FillRectangle(mybrush1, Convert.ToInt32(Convert.ToDouble(265450) * xScale), 0, 
            //    Convert.ToInt32(Convert.ToDouble(268500-265450) * xScale), this.panelZ11_Z12Bay.Height);
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
                Z41_library_Monitor.SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
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
                //conCrane1_1.Visible = false;
                conCrane4_1.Visible = false;
                conCrane4_2.Visible = false;
                
                btnShowCrane.Text = "显示行车";
            }
            else
            {
                //conCrane1_1.Visible = true;
                conCrane4_1.Visible = true;
                conCrane4_2.Visible = true;
                
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
                frmSeekCoil.Z11_Z12Panel = panelZ62Bay;
                frmSeekCoil.Z11_Z12_Width = panelZ62Bay.Width;
                frmSeekCoil.Z11_Z12_Height = panelZ62Bay.Height;
                frmSeekCoil.BayNo = constData.bayNo_Z41;
                frmSeekCoil.Show();
            }
            else
            {
                frmSeekCoil.WindowState = FormWindowState.Normal;
                frmSeekCoil.Activate();
            }
        }       

        private void button1_Click(object sender, EventArgs e)
        {
            if (form == null || form.IsDisposed)
            {
                form = new FrmMoreStock();
                form.MyLibrary = "Z62";
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
                //               having count(*)>1) and BAY_NO = 'Z21' order by BAY_NO ";

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
                //WRUnitSaddle();
                //WCUnitSaddle();
                if(unitShowCount == 0)
                {
                    btnReCoilUnit.BackColor = Color.LightSteelBlue;
                }
                else if(unitShowCount > 0)
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
                            if(get_value(rdr["TAG_ISEMPTY"].ToString().Trim()) == "1")
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
            catch(Exception er)
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

        //获取状态
        //private void GetStatus()
        //{
        //    List<string> lstAdress = new List<string>();
        //    lstAdress.Clear();
        //    lstAdress.Add("D108_EXIT_TO_D208_EXIT");
        //    arrTagAdress = lstAdress.ToArray<string>();
        //    readTags();
        //    if (get_value("D108_EXIT_TO_D208_EXIT") == "0")
        //    {
        //        btnD108ToD208.Text = "108上料208：关";
        //        btnD108ToD208.BackColor = Color.LightSteelBlue;
        //    }
        //    else if (get_value("D108_EXIT_TO_D208_EXIT") == "1")
        //    {
        //        btnD108ToD208.Text = "108上料208：开";
        //        btnD108ToD208.BackColor = Color.Yellow;
        //    }
        //}

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
