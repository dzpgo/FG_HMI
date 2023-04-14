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
using Baosight.iSuperframe.TagService.Controls;
using Baosight.iSuperframe.TagService.Interface;
using Baosight.iSuperframe.TagService;
using UACSControls;
using UACSDAL;
using System.Threading;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;
using UACSPopupForm.CraneMonitor;

namespace UACSView.View_CraneMonitor
{
    public partial class Z01_library_Monitor : FormBase
    {
        #region 全局变量
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
        private CraneStatusBase craneStatusBase = new CraneStatusBase();
        private static FrmSeekCoil frmSeekCoil;
        private conStockSaddleModel saddleInStock_Z11_Z12;
        private conOffinePackingSaddleModel saddleInStock_Z21;
        private bool isShowCurrentBayXY = false;    //是否显示鼠标位置的XY
        private bool tabActived = true;             //是否在当前画面显示
        private string craneNo_2_1 = "1";
        private string craneNo_2_2 = "2";
        private string craneNo_2_3 = "3";
        private string craneNo_2_4 = "4";
        private const string D102ENTRY = "D102-WR";
        private const string YSLEXIT = "YSL-WC";
        private bool IsRecondition = false;
        /// <summary>
        /// //清扫按钮背景闪烁
        /// </summary>
        private bool isCubicleClean = false;
        private bool isCubicleCleanRepeat = false;
        /// <summary>
        /// 1号行车X坐标
        /// </summary>
        private string CraneA1_X { get; set; }
        /// <summary>
        /// 2号行车X坐标
        /// </summary>
        private string CraneA2_X { get; set; }
        /// <summary>
        /// 3号行车X坐标
        /// </summary>
        private string CraneA3_X { get; set; }
        /// <summary>
        /// 4号行车X坐标
        /// </summary>
        private string CraneA4_X { get; set; }
        private List<string> listReCoilUnit = new List<string>(); 
        #endregion

        #region TAG配置
        private TagDataProvider tagDataProvider = new TagDataProvider();
        DataCollection<object> inDatas = new DataCollection<object>();

        private IAuthorization auth = null;

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

        public Z01_library_Monitor()
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

        /// <summary>
        /// 初始化加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void A_library_Monitor_Load(object sender, EventArgs e)
        {
            tagDataProvider.ServiceName = "iplature";
            auth = FrameContext.Instance.GetPlugin<IAuthorization>() as IAuthorization;          
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
            CraneA1_X = ""; 
            CraneA2_X = ""; 
            CraneA3_X = ""; 
            CraneA4_X = "";

            //行车显示控件配置

            conCrane2_1.InitTagDataProvide(constData.tagServiceName);
            conCrane2_1.CraneNO = craneNo_2_1;
            listConCraneDisplay.Add(conCrane2_1);

            conCrane2_2.InitTagDataProvide(constData.tagServiceName);
            conCrane2_2.CraneNO = craneNo_2_2;
            listConCraneDisplay.Add(conCrane2_2);

            conCrane2_3.InitTagDataProvide(constData.tagServiceName);
            conCrane2_3.CraneNO = craneNo_2_3;
            listConCraneDisplay.Add(conCrane2_3);

            conCrane2_4.InitTagDataProvide(constData.tagServiceName);
            conCrane2_4.CraneNO = craneNo_2_4;
            listConCraneDisplay.Add(conCrane2_4);



            //---------------------行车状态控件配置-------------------------------
            conCraneStatus2_1.InitTagDataProvide(constData.tagServiceName);
            conCraneStatus2_1.CraneNO = craneNo_2_1;
            lstConCraneStatusPanel.Add(conCraneStatus2_1);

            conCraneStatus2_2.InitTagDataProvide(constData.tagServiceName);
            conCraneStatus2_2.CraneNO = craneNo_2_2;
            lstConCraneStatusPanel.Add(conCraneStatus2_2);

            conCraneStatus2_3.InitTagDataProvide(constData.tagServiceName);
            conCraneStatus2_3.CraneNO = craneNo_2_3;
            lstConCraneStatusPanel.Add(conCraneStatus2_3);

            conCraneStatus2_4.InitTagDataProvide(constData.tagServiceName);
            conCraneStatus2_4.CraneNO = craneNo_2_4;
            lstConCraneStatusPanel.Add(conCraneStatus2_4);


            //---------------------行车tag配置--------------------------------
            craneStatusInBay.InitTagDataProvide(constData.tagServiceName);
            craneStatusInBay.AddCraneNO(craneNo_2_1);
            craneStatusInBay.AddCraneNO(craneNo_2_2);
            craneStatusInBay.AddCraneNO(craneNo_2_3);
            craneStatusInBay.AddCraneNO(craneNo_2_4);
            craneStatusInBay.SetReady();


            this.panelZ61Bay.Paint += panelZ11_Z22Bay_Paint;

            //预先加载
            timer_InitializeLoad.Enabled = true;
            timer_InitializeLoad.Interval = 100;

            //检修状态
            GetOrederTypeStatus();
        }

        private void LoadAreaInfo()
        {
            areaModel.conInit(panelZ61Bay,
                constData.bayNo_Z01,
                constData.tagServiceName,
                constData.Z01BaySpaceX,
                constData.Z01BaySpaceY,
                panelZ61Bay.Width,
                panelZ61Bay.Height,
                constData.xBxisleft,
                constData.yBxisDown,
                 AreaInfo.AreaType.AllType);
        }

        private void LoadUnitInfo()
        {
            unitSaddleModel.conInit(panelZ61Bay,
                D102ENTRY,
                constData.tagServiceName,
                constData.Z01BaySpaceX,
                constData.Z01BaySpaceY,
                panelZ61Bay.Width,
                panelZ61Bay.Height,
                constData.xBxisleft,
                constData.yAxisDown,
                constData.bayNo_Z01);


            unitSaddleModel.conInit(panelZ61Bay,
                YSLEXIT,
                constData.tagServiceName,
                constData.Z01BaySpaceX,
                constData.Z01BaySpaceY,
                panelZ61Bay.Width,
                panelZ61Bay.Height,
                constData.xBxisleft,
                constData.yAxisDown,
                constData.bayNo_Z01);

            saddleInStock_Z21.conInit(panelZ61Bay,
                "Z01-6-15",
                constData.tagServiceName,
                constData.Z01BaySpaceX,
                constData.Z01BaySpaceY,              
                constData.xBxisleft,
                constData.yAxisDown);

            saddleInStock_Z21.conInit(panelZ61Bay,
                "Z01PC",
                constData.tagServiceName,
                constData.Z01BaySpaceX,
                constData.Z01BaySpaceY,
                constData.xBxisleft,
                constData.yAxisDown);
        }

        private void LoadParkingCarInfo()
        {
            parkingCarModel.conInit(panelZ61Bay, 
                constData.bayNo_Z01, 
                constData.tagServiceName,
                constData.Z01BaySpaceX,
                constData.Z01BaySpaceY,
                panelZ61Bay.Width,
                panelZ61Bay.Height,
                constData.xBxisleft,
                constData.yBxisDown);

            parkingCarHeadModel.conInit(panelZ61Bay,
                constData.bayNo_Z01,
                constData.tagServiceName,
                constData.Z01BaySpaceX,
                constData.Z01BaySpaceY,
                panelZ61Bay.Width,
                panelZ61Bay.Height,
                constData.xBxisleft,
                constData.yBxisDown);
        }


        private void timer_InitializeLoad_Tick(object sender, EventArgs e)
        {

            //LoadUnitInfo();
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
                #region 行车、检修、清扫闪烁
                if (Crane_1.Equals(1))
                {
                    conCrane2_1.BackColor = Color.Tomato;
                    //conCrane2_1.BackgroundImage = UACSControls.Resource1.行车_Stop;
                }
                if (Crane_2.Equals(1))
                {
                    conCrane2_2.BackColor = Color.Tomato;
                    //conCrane2_2.BackgroundImage = UACSControls.Resource1.行车_Stop;
                }
                if (Crane_3.Equals(1))
                {
                    conCrane2_3.BackColor = Color.Tomato;
                    //conCrane2_3.BackgroundImage = UACSControls.Resource1.行车_Stop;
                }
                if (Crane_4.Equals(1))
                {
                    conCrane2_4.BackColor = Color.Tomato;
                    //conCrane2_4.BackgroundImage = UACSControls.Resource1.行车_Stop;
                }
                //检修中按钮变红，清扫中按钮变红
                if (Crane_1.Equals(1) || Crane_2.Equals(1) || Crane_3.Equals(1) || Crane_4.Equals(1))
                {
                    if (IsRecondition)
                    {
                        bt_Recondition.BackColor = Color.Red;
                        IsRecondition = false;
                    }
                    else
                    {
                        bt_Recondition.BackColor = Color.LightSteelBlue;
                        IsRecondition = true;
                    }
                }
                else
                {
                    bt_Recondition.BackColor = Color.LightSteelBlue;
                }

                //清扫按钮变红，闪烁
                //if (isCubicleClean)
                //{
                //    if (isCubicleCleanRepeat)
                //    {
                //        bt_CraneClean.BackColor = Color.Red;
                //        isCubicleCleanRepeat = false;
                //    }
                //    else
                //    {
                //        bt_CraneClean.BackColor = Color.LightSteelBlue;
                //        isCubicleCleanRepeat = true;
                //    }
                //}
                //else
                //    bt_CraneClean.BackColor = Color.LightSteelBlue;


                #endregion

                //bt_CraneClean.BackColor = Color.LightSteelBlue;

                //AreaInStockZ12.conInit(panelZ61Bay, constData.bayNo_Z01, SaddleBase.tagServiceName,
                //       constData.Z01BaySpaceX, constData.Z01BaySpaceY, panelZ61Bay.Width, panelZ61Bay.Height,
                //       constData.xAxisRight, constData.yBxisDown, AreaInfo.AreaType.StockArea);
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
                    if (conCraneStatusPanel.CraneNO.Equals("1"))
                    {
                        CraneA1_X = conCraneStatusPanel.AX;
                    }
                    else if (conCraneStatusPanel.CraneNO.Equals("2"))
                    {
                        CraneA2_X = conCraneStatusPanel.AX;
                    }
                    else if (conCraneStatusPanel.CraneNO.Equals("3"))
                    {
                        CraneA3_X = conCraneStatusPanel.AX;
                    }
                    else if (conCraneStatusPanel.CraneNO.Equals("4"))
                    {
                        CraneA4_X = conCraneStatusPanel.AX;
                    }
                }
                //--------------------------行车状态控件刷新-------------------------------------------
                foreach (conCrane conCraneVisual in listConCraneDisplay)
                {
                    conCrane.RefreshControlInvoke ConCraneVisual_Invoke = new conCrane.RefreshControlInvoke(conCraneVisual.RefreshControl);
                    conCraneVisual.BeginInvoke(ConCraneVisual_Invoke, new Object[] 
                    { craneStatusInBay.DicCranePLCStatusBase[conCraneVisual.CraneNO].Clone(), 
                         constData.Z01BaySpaceX,
                         constData.Z01BaySpaceY,
                         panelZ61Bay.Width,
                         panelZ61Bay.Height,
                         constData.xBxisleft,
                         constData.yBxisDown,
                         6160,         
                         panelZ61Bay 
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
            //showMoreStock();
            //showReCoilUnit();
            GetStatus();
            UpdateMessage();
            getMode();
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

            //LoadUnitInfo();
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
                double xScale = Convert.ToDouble(panelZ61Bay.Width) / Convert.ToDouble(constData.Z01BaySpaceX);
                //计算Y方向的比例关系
                double yScale = Convert.ToDouble(panelZ61Bay.Height) / Convert.ToDouble(constData.Z01BaySpaceY);

                Point p = this.panelZ61Bay.PointToClient(Control.MousePosition);
                if (p.X <= this.panelZ61Bay.Location.X || p.X >= this.panelZ61Bay.Location.X + this.panelZ61Bay.Width ||
                    p.Y < this.panelZ61Bay.Location.Y || p.Y >= this.panelZ61Bay.Location.Y + this.panelZ61Bay.Height)
                {
                    return;
                }
                txtX.Text = Convert.ToString(Convert.ToInt32(Convert.ToDouble(p.X) / xScale));
                //txtY.Text = Convert.ToString(Convert.ToInt32((Convert.ToDouble(panelZ62Bay.Height)-Convert.ToDouble(p.Y)) / yScale));
                //txtX.Text = Convert.ToString(Convert.ToInt32((Convert.ToDouble(panelZ61Bay.Width) - Convert.ToDouble(p.X)) / xScale));
                //txtX.Text = Convert.ToString(Convert.ToInt32(Convert.ToDouble(p.X) / xScale));
                txtY.Text = Convert.ToString(Convert.ToInt32((Convert.ToDouble(panelZ61Bay.Height) - Convert.ToDouble(p.Y)) / yScale));
                //txtY.Text = Convert.ToString(Convert.ToInt32(Convert.ToDouble(p.Y) / yScale));
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
            double xScale = Convert.ToDouble(panelZ61Bay.Width) / Convert.ToDouble(constData.Z01BaySpaceX);
            //计算Y方向的比例关系
            double yScale = Convert.ToDouble(panelZ61Bay.Height) / Convert.ToDouble(constData.Z01BaySpaceY);
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
                Z01_library_Monitor.SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
            }
        }
        #endregion


        /// <summary>
        /// 显示行车 / 隐藏行车  点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShowCrane_Click(object sender, EventArgs e)
        {
            if (btnShowCrane.Text == "隐藏行车")
            {
                conCrane2_1.Visible = false;
                conCrane2_2.Visible = false;
                conCrane2_3.Visible = false;
                conCrane2_4.Visible = false;

                btnShowCrane.Text = "显示行车";
            }
            else
            {
                conCrane2_1.Visible = true;
                conCrane2_2.Visible = true;
                conCrane2_3.Visible = true;
                conCrane2_4.Visible = true;

                btnShowCrane.Text = "隐藏行车";
            }
        }
        /// <summary>
        /// 显示XY / 取消显示  点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        #region 无用

        //private void btnSeekCoil_Click(object sender, EventArgs e)
        //{
        //    if (frmSeekCoil == null || frmSeekCoil.IsDisposed)
        //    {
        //        frmSeekCoil = new FrmSeekCoil();
        //        frmSeekCoil.saddleInStock_Z11_Z12 = saddleInStock_Z11_Z12;
        //        frmSeekCoil.Z11_Z12Panel = panelZ61Bay;
        //        frmSeekCoil.Z11_Z12_Width = panelZ61Bay.Width;
        //        frmSeekCoil.Z11_Z12_Height = panelZ61Bay.Height;
        //        frmSeekCoil.BayNo = constData.bayNo_Z01;
        //        frmSeekCoil.Show();
        //    }
        //    else
        //    {
        //        frmSeekCoil.WindowState = FormWindowState.Normal;
        //        frmSeekCoil.Activate();
        //    }
        //}       

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    if (form == null || form.IsDisposed)
        //    {
        //        form = new FrmMoreStock();
        //        form.MyLibrary = "Z01";
        //        form.Show();
        //    }
        //    else
        //    {
        //        form.WindowState = FormWindowState.Normal;
        //        form.Activate();
        //    }
        //}

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
                        //button1.BackColor = Color.Red;
                    }
                    else
                    {
                        //button1.BackColor = Color.LightSteelBlue;
                    }
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, er.StackTrace);
            }
        }

        //private void btnReCoilUnit_Click(object sender, EventArgs e)
        //{
        //    if (frmReCoilUnit == null || frmReCoilUnit.IsDisposed)
        //    {
        //        frmReCoilUnit = new FrmReCoilUnit();
        //        frmReCoilUnit.DicUnit = dicUnit;
        //        frmReCoilUnit.Show();
        //    }
        //    else
        //    {
        //        frmReCoilUnit.WindowState = FormWindowState.Normal;
        //        frmReCoilUnit.Activate();
        //    }
        //}
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
                if (unitShowCount == 0)
                {
                    //btnReCoilUnit.BackColor = Color.LightSteelBlue;
                }
                else if (unitShowCount > 0)
                {
                    //btnReCoilUnit.BackColor = Color.Red;
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



        //private void btnTwoCrane_Click(object sender, EventArgs e)
        //{
        //    List<string> lstAdress = new List<string>();
        //    lstAdress.Clear();
        //    lstAdress.Add("TWO_CRANE_GO_YSL");
        //    arrTagAdress = lstAdress.ToArray<string>();
        //    readTags();
        //    if (get_value("TWO_CRANE_GO_YSL") == "0")
        //    {
        //        if (MessageBox.Show("确定开启运输链双车收料吗？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
        //        {
        //            //开启双车收料
        //            tagDataProvider.SetData("TWO_CRANE_GO_YSL", "1");
        //            UpCraneOrderConfigOpen_EXIT_TO_YARD();

        //            ////关闭双车上料
        //            //tagDataProvider.SetData("TWO_CRANE_GO_D102", "0");

        //            ////关闭双车备料
        //            //tagDataProvider.SetData("TWO_CRANE_GO_YARD", "0");
        //            //UpCraneOrderConfigClose_YARD_2_YARD();

        //            ////关闭双车上水槽
        //            //tagDataProvider.SetData("TWO_CRANE_GO_WATER_TANK", "0");
        //            //UpCraneOrderConfigClose_YARD_2_WATER_TANK();

        //            //关闭倒垛策列配置
        //            UpCraneFlagEnabledByStop1or6or7();
        //            UpCraneFlagEnabledByStopNo1and6and7();
        //            btnTwoCrane.Text = "双车收料：开";
        //            btnTwoCrane.BackColor = Color.Yellow;
        //            ParkClassLibrary.HMILogger.WriteLog(btnTwoCrane.Text, "运输链双车收料：开启", ParkClassLibrary.LogLevel.Info, this.Text);
        //        }
        //    }
        //    else if (get_value("TWO_CRANE_GO_YSL") == "1")
        //    {
        //        if (MessageBox.Show("确定关闭运输链双车收料吗？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
        //        {
        //            tagDataProvider.SetData("TWO_CRANE_GO_YSL", "0");
        //            UpCraneOrderConfigClose_EXIT_TO_YARD();
        //            btnTwoCrane.Text = "双车收料：关";
        //            btnTwoCrane.BackColor = Color.LightSteelBlue;
        //            ParkClassLibrary.HMILogger.WriteLog(btnTwoCrane.Text, "运输链双车收料：关闭", ParkClassLibrary.LogLevel.Info, this.Text);
        //        }
        //    }
        //}

        private void UpCraneOrderConfigClose_EXIT_TO_YARD()
        {
            try
            {
                string sql = @"update CRANE_ORDER_EXCUTE_SEQUENCE set FLAG_ENABLED = 0 where (CRANE_NO = '1_2' or CRANE_NO = '1_3') and TASK_NAME = 'YSL2_EXIT_TO_YARD'";
                DB2Connect.DBHelper.ExecuteNonQuery(sql);
            }
            catch (Exception er)
            {

            }
        }
        private void UpCraneOrderConfigOpen_EXIT_TO_YARD()
        {
            try
            {
                //string sql1 = @"update CRANE_ORDER_EXCUTE_SEQUENCE set FLAG_ENABLED = 0 where (CRANE_NO = '1_2' or CRANE_NO = '1_3') and TASK_NAME != 'YSL2_EXIT_TO_YARD'";
                //DB2Connect.DBHelper.ExecuteNonQuery(sql1);
                string sql2 = @"update CRANE_ORDER_EXCUTE_SEQUENCE set FLAG_ENABLED = 1 where (CRANE_NO = '1_2' or CRANE_NO = '1_3') and TASK_NAME = 'YSL2_EXIT_TO_YARD'";
                DB2Connect.DBHelper.ExecuteNonQuery(sql2);
            }
            catch (Exception er)
            {

            }
        }
        private void UpCraneOrderConfigClose_YARD_2_YARD()
        {
            try
            {
                string sql = @"update CRANE_ORDER_EXCUTE_SEQUENCE set FLAG_ENABLED = 0 where (CRANE_NO = '1_2' or CRANE_NO = '1_3') and TASK_NAME = 'YARD_2_YARD'";
                DB2Connect.DBHelper.ExecuteNonQuery(sql);
            }
            catch (Exception er)
            {

            }
        }

        private void UpCraneOrderConfigOpen_YARD_2_YARD()
        {
            try
            {
                //string sql1 = @"update CRANE_ORDER_EXCUTE_SEQUENCE set FLAG_ENABLED = 0 where (CRANE_NO = '1_2' or CRANE_NO = '1_3') and TASK_NAME != 'YARD_2_YARD'";
                //DB2Connect.DBHelper.ExecuteNonQuery(sql1);
                string sql2 = @"update CRANE_ORDER_EXCUTE_SEQUENCE set FLAG_ENABLED = 1 where (CRANE_NO = '1_2' or CRANE_NO = '1_3') and TASK_NAME = 'YARD_2_YARD'";
                DB2Connect.DBHelper.ExecuteNonQuery(sql2);
            }
            catch (Exception er)
            {

            }
        }

        private void UpCraneOrderConfigClose_YARD_2_WATER_TANK()
        {
            try
            {
                string sql = @"update CRANE_ORDER_EXCUTE_SEQUENCE set FLAG_ENABLED = 0 where (CRANE_NO = '1_2' or CRANE_NO = '1_3') and TASK_NAME = 'YARD_2_WATER_TAN'";
                DB2Connect.DBHelper.ExecuteNonQuery(sql);
            }
            catch (Exception er)
            {

            }
        }

        private void UpCraneOrderConfigOpen_YARD_2_WATER_TANK()
        {
            try
            {
                //string sql1 = @"update CRANE_ORDER_EXCUTE_SEQUENCE set FLAG_ENABLED = 0 where (CRANE_NO = '1_2' or CRANE_NO = '1_3') and TASK_NAME != 'YARD_2_WATER_TANK'";
                //DB2Connect.DBHelper.ExecuteNonQuery(sql1);
                string sql2 = @"update CRANE_ORDER_EXCUTE_SEQUENCE set FLAG_ENABLED = 1 where (CRANE_NO = '1_2' or CRANE_NO = '1_3') and TASK_NAME = 'YARD_2_WATER_TANK'";
                DB2Connect.DBHelper.ExecuteNonQuery(sql2);
            }
            catch (Exception er)
            {

            }
        }

        private void UpCraneFlagEnabledByOpen1or6or7()
        {
            try
            {
                string sql = @"update YARD_TO_YARD_CRANE_STRAEGY set FLAG_ENABLED = 1 where (CRANE_NO = '1_2' or CRANE_NO = '1_3') and  ID in (SELECT ID FROM YARD_TO_YARD_FIND_COIL_STRATEGY WHERE FLAG_FIND_BY_PLAN =1 or FLAG_FIND_BY_PLAN =6 or FLAG_FIND_BY_PLAN =7)";
                DB2Connect.DBHelper.ExecuteNonQuery(sql);
            }
            catch (Exception er)
            {

            }
        }

        private void UpCraneFlagEnabledByStop1or6or7()
        {
            try
            {
                string sql = @"update YARD_TO_YARD_CRANE_STRAEGY set FLAG_ENABLED = 0 where (CRANE_NO = '1_2' or CRANE_NO = '1_3') and  ID in (SELECT ID FROM YARD_TO_YARD_FIND_COIL_STRATEGY WHERE FLAG_FIND_BY_PLAN =1 or FLAG_FIND_BY_PLAN =6 or FLAG_FIND_BY_PLAN =7)";
                DB2Connect.DBHelper.ExecuteNonQuery(sql);
            }
            catch (Exception er)
            {
            }
        }

        private void UpCraneFlagEnabledByOpenNo1and6and7()
        {
            try
            {
                string sql = @"update YARD_TO_YARD_CRANE_STRAEGY set FLAG_ENABLED = 1 where (CRANE_NO = '1_2' or CRANE_NO = '1_3') and ID in (SELECT ID FROM YARD_TO_YARD_FIND_COIL_STRATEGY WHERE FLAG_FIND_BY_PLAN !=1 and FLAG_FIND_BY_PLAN !=6 and FLAG_FIND_BY_PLAN !=7)";
                DB2Connect.DBHelper.ExecuteNonQuery(sql);
            }
            catch (Exception er)
            {
            }
        }

        private void UpCraneFlagEnabledByStopNo1and6and7()
        {
            try
            {
                string sql = @"update YARD_TO_YARD_CRANE_STRAEGY set FLAG_ENABLED = 0 where (CRANE_NO = '1_2' or CRANE_NO = '1_3') and  ID in (SELECT ID FROM YARD_TO_YARD_FIND_COIL_STRATEGY WHERE FLAG_FIND_BY_PLAN !=1 and FLAG_FIND_BY_PLAN !=6 and FLAG_FIND_BY_PLAN !=7)";
                DB2Connect.DBHelper.ExecuteNonQuery(sql);
            }
            catch (Exception er)
            {
            }
        } 
        #endregion

        /// <summary>
        /// 行车检修状态   ORDER_TYPE = 指令类型 11:归堆  21：装车  41：清扫  X1：登车  51：检修  99：空闲
        /// </summary>
        private void GetOrederTypeStatus()
        {
            string sqlText = @"SELECT CRANE_NO,ORDER_TYPE,CMD_STATUS FROM UACS_CRANE_ORDER_CURRENT ORDER BY CRANE_NO ";
            using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sqlText))
            {
                while (rdr.Read())
                {
                    if (rdr["CRANE_NO"] != System.DBNull.Value && rdr["ORDER_TYPE"] != System.DBNull.Value)
                    {
                        if (rdr["ORDER_TYPE"].ToString().Trim().Equals("51"))
                        {
                            if (rdr["CRANE_NO"].ToString().Trim().Equals("1"))
                            {
                                Crane_1 = 1;
                            }
                            else if (rdr["CRANE_NO"].ToString().Trim().Equals("2"))
                            {
                                Crane_2 = 1;
                            }
                            else if (rdr["CRANE_NO"].ToString().Trim().Equals("3"))
                            {
                                Crane_3 = 1;
                            }
                            else if (rdr["CRANE_NO"].ToString().Trim().Equals("4"))
                            {
                                Crane_4 = 1;
                            }
                        }
                        if (rdr["ORDER_TYPE"].ToString().Trim().Equals("41"))
                        {
                            isCubicleClean = true;
                        }
                    }
                    
                }
            }
        }

        private void GetStatus()
        {
            List<string> lstAdress = new List<string>();
            lstAdress.Clear();
            lstAdress.Add("TWO_CRANE_GO_YSL");
            lstAdress.Add("TWO_CRANE_GO_D102");
            lstAdress.Add("TWO_CRANE_GO_YARD");
            lstAdress.Add("TWO_CRANE_GO_WATER_TANK");
            lstAdress.Add("TWO_CRANE_GO_COLDCOIL"); 
            lstAdress.Add("AREA_SAFE_JQ");
            arrTagAdress = lstAdress.ToArray<string>();
            readTags();
            if (get_value("TWO_CRANE_GO_YSL") == "0")
            {
                //btnTwoCrane.Text = "双车收料：关";
                //btnTwoCrane.BackColor = Color.LightSteelBlue;
            }
            else if (get_value("TWO_CRANE_GO_YSL") == "1")
            {
                //btnTwoCrane.Text = "双车收料：开";
                //btnTwoCrane.BackColor = Color.Yellow;
            }
        }

        private void UpdateMessage()
        {
            List<string> lstAdress = new List<string>();
            lstAdress.Clear();
            lstAdress.Add("UPDATE_COILMESSAGE");
            arrTagAdress = lstAdress.ToArray<string>();
            readTags();
            if (get_value("UPDATE_COILMESSAGE") == "0")
            {
                //btnUpdate.BackColor = Color.LightSteelBlue;
            }
            else if (get_value("UPDATE_COILMESSAGE") == "1")
            {
                //btnUpdate.BackColor = Color.Yellow;
            }
        }

        #region MyRegion
        private void getMode()
        {
            //string sql1 = @"SELECT SEQ_VALUE FROM UACS_SEQ_CONFIG WHERE SEQ_NAME = '2_1_CLTS_MODE'";
            //using (IDataReader rdr1 = DB2Connect.DBHelper.ExecuteReader(sql1))
            //{
            //    while (rdr1.Read())
            //    {
            //        if (rdr1["SEQ_VALUE"] != DBNull.Value)
            //        {
            //            string mode1 = rdr1["SEQ_VALUE"].ToString().Trim();
            //            if(mode1 == "1")
            //            {
            //                btnMode1.Text = "2-1行车模式：CLTS";
            //                btnMode1.BackColor = Color.Yellow;
            //            }
            //            else
            //            {
            //                btnMode1.Text = "2-1行车模式：UACS";
            //                btnMode1.BackColor = Color.LightSteelBlue;
            //            }
            //        }
            //    }
            //}

            //string sql2 = @"SELECT SEQ_VALUE FROM UACS_SEQ_CONFIG WHERE SEQ_NAME = '2_2_CLTS_MODE'";
            //using (IDataReader rdr2 = DB2Connect.DBHelper.ExecuteReader(sql2))
            //{
            //    while (rdr2.Read())
            //    {
            //        if (rdr2["SEQ_VALUE"] != DBNull.Value)
            //        {
            //            string mode2 = rdr2["SEQ_VALUE"].ToString().Trim();
            //            if (mode2 == "1")
            //            {
            //                btnMode2.Text = "2-2行车模式：CLTS";
            //                btnMode2.BackColor = Color.Yellow;
            //            }
            //            else
            //            {
            //                btnMode2.Text = "2-2行车模式：UACS";
            //                btnMode2.BackColor = Color.LightSteelBlue;
            //            }
            //        }
            //    }
            //}

            //string sql3 = @"SELECT SEQ_VALUE FROM UACS_SEQ_CONFIG WHERE SEQ_NAME = '2_3_CLTS_MODE'";
            //using (IDataReader rdr3 = DB2Connect.DBHelper.ExecuteReader(sql3))
            //{
            //    while (rdr3.Read())
            //    {
            //        if (rdr3["SEQ_VALUE"] != DBNull.Value)
            //        {
            //            string mode3 = rdr3["SEQ_VALUE"].ToString().Trim();
            //            if (mode3 == "1")
            //            {
            //                btnMode3.Text = "2-3行车模式：CLTS";
            //                btnMode3.BackColor = Color.Yellow;
            //            }
            //            else
            //            {
            //                btnMode3.Text = "2-3行车模式：UACS";
            //                btnMode3.BackColor = Color.LightSteelBlue;
            //            }
            //        }
            //    }
            //}

        } 
        #endregion

        #region 检修时更改行车背景颜色

        private int Crane_1 = 0;
        private int Crane_2 = 0;
        private int Crane_3 = 0;
        private int Crane_4 = 0;
        ///// <summary>
        ///// 检修
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void btnbtnRecondition_Click(object sender, EventArgs e)
        //{
        //    Recondition frm = new Recondition();
        //    frm.CraneNo = craneNO;
        //    frm.BayNO = "A";
        //    frm.ShowDialog();
        //}
        /// <summary>
        /// 更新行车背景颜色
        /// </summary>
        /// <param name="CraneNO">行车号</param>
        public void UpdataCrane(string CraneNO)
        {
            if (CraneNO.Equals("1"))
            {
                this.conCrane2_1.BackColor = System.Drawing.Color.Tomato;
                //this.conCrane2_1.BackgroundImage = UACSControls.Resource1.行车_Stop;
                Crane_1 = 1;
            }
            else if (CraneNO.Equals("2"))
            {
                this.conCrane2_2.BackColor = System.Drawing.Color.Tomato;
                //this.conCrane2_2.BackgroundImage = UACSControls.Resource1.行车_Stop;
                Crane_2 = 1;
            }
            else if (CraneNO.Equals("3"))
            {
                this.conCrane2_3.BackColor = System.Drawing.Color.Tomato;
                //this.conCrane2_3.BackgroundImage = UACSControls.Resource1.行车_Stop;
                Crane_3 = 1;
            }
            else if (CraneNO.Equals("4"))
            {
                this.conCrane2_4.BackColor = System.Drawing.Color.Tomato;
                //this.conCrane2_4.BackgroundImage = UACSControls.Resource1.行车_Stop;
                Crane_4 = 1;
            }
        }
        /// <summary>
        /// 取消行车背景颜色
        /// </summary>
        /// <returns></returns>
        public void OutCrane(string CraneNO)
        {
            if (CraneNO.Equals("1"))
            {
                this.conCrane2_1.BackColor = System.Drawing.SystemColors.Control;
                //this.conCrane2_1.BackgroundImage = UACSControls.Resource1.行车_Run;
                Crane_1 = 0;
            }
            else if (CraneNO.Equals("2"))
            {
                this.conCrane2_2.BackColor = System.Drawing.SystemColors.Control;
                //this.conCrane2_2.BackgroundImage = UACSControls.Resource1.行车_Run;
                Crane_2 = 0;
            }
            else if (CraneNO.Equals("3"))
            {
                this.conCrane2_3.BackColor = System.Drawing.SystemColors.Control;
                //this.conCrane2_3.BackgroundImage = UACSControls.Resource1.行车_Run;
                Crane_3 = 0;
            }
            else if (CraneNO.Equals("4"))
            {
                this.conCrane2_4.BackColor = System.Drawing.SystemColors.Control;
                //this.conCrane2_4.BackgroundImage = UACSControls.Resource1.行车_Run;
                Crane_4 = 0;
            }
        }
        /// <summary>
        /// 清扫按钮背景变色
        /// </summary>
        /// <returns></returns>
        public void UpdaeCubicleClean(string CraneNO)
        {
            isCubicleClean = true;
        }
        /// <summary>
        /// 取消清扫按钮背景变色
        /// </summary>
        /// <returns></returns>
        public void CancelCubicleClean(string CraneNO)
        {
            isCubicleClean = false;
        }

        #endregion

        #region 代码弃用

        //private void button2_Click(object sender, EventArgs e)
        //{
        //    List<string> lstAdress = new List<string>();
        //    lstAdress.Clear();
        //    lstAdress.Add("UPDATE_COILMESSAGE");
        //    arrTagAdress = lstAdress.ToArray<string>();
        //    readTags();
        //    tagDataProvider.SetData("UPDATE_COILMESSAGE", "1");
        //    btnUpdate.BackColor = Color.Yellow;
        //    ParkClassLibrary.HMILogger.WriteLog(btnUpdate.Text, "更新钢卷信息", ParkClassLibrary.LogLevel.Info, this.Text);
        //}

        //private void btnMode1_Click(object sender, EventArgs e)
        //{
        //    FrmYordToYordConfPassword password = new FrmYordToYordConfPassword();
        //    password.PassWords = "UACS1550";
        //    DialogResult retValue = password.ShowDialog();
        //    if (retValue != DialogResult.OK)
        //    {
        //        return;
        //    }

        //    List<string> lstAdress = new List<string>();
        //    lstAdress.Clear();
        //    lstAdress.Add("2_1_zAct");
        //    arrTagAdress = lstAdress.ToArray<string>();
        //    readTags();
        //    int height = Convert.ToInt32(get_value("2_1_zAct"));
        //    if(height <= 4000)
        //    {
        //        MessageBox.Show("行车高度不在4米以上，请在4米以上切换！");
        //        return;
        //    }
        //    else
        //    {
        //        if(btnMode1.Text.Contains("UACS"))
        //        {
        //            string sql = @"UPDATE UACS_SEQ_CONFIG SET SEQ_VALUE = 1 WHERE SEQ_NAME = '2_1_CLTS_MODE'";
        //            DB2Connect.DBHelper.ExecuteNonQuery(sql);
        //            btnMode1.Text = "2-1行车模式：CLTS";
        //            btnMode1.BackColor = Color.Yellow;
        //        }
        //        else
        //        {
        //            string sql = @"UPDATE UACS_SEQ_CONFIG SET SEQ_VALUE = 0 WHERE SEQ_NAME = '2_1_CLTS_MODE'";
        //            DB2Connect.DBHelper.ExecuteNonQuery(sql);
        //            btnMode1.Text = "2-1行车模式：UACS";
        //            btnMode1.BackColor = Color.LightSteelBlue;
        //        }
        //    }
        //}

        //private void btnMode2_Click(object sender, EventArgs e)
        //{
        //    FrmYordToYordConfPassword password = new FrmYordToYordConfPassword();
        //    password.PassWords = "UACS1550";
        //    DialogResult retValue = password.ShowDialog();
        //    if (retValue != DialogResult.OK)
        //    {
        //        return;
        //    }

        //    List<string> lstAdress = new List<string>();
        //    lstAdress.Clear();
        //    lstAdress.Add("2_2_zAct");
        //    arrTagAdress = lstAdress.ToArray<string>();
        //    readTags();
        //    int height = Convert.ToInt32(get_value("2_2_zAct"));          
        //    if (height <= 4000)
        //    {
        //        MessageBox.Show("行车高度不在4米以上，请在4米以上切换！");
        //        return;
        //    }
        //    else
        //    {
        //        if (btnMode2.Text.Contains("UACS"))
        //        {
        //            string sql = @"UPDATE UACS_SEQ_CONFIG SET SEQ_VALUE = 1 WHERE SEQ_NAME = '2_2_CLTS_MODE'";
        //            DB2Connect.DBHelper.ExecuteNonQuery(sql);
        //            btnMode2.Text = "2-2行车模式：CLTS";
        //            btnMode2.BackColor = Color.Yellow;
        //        }
        //        else
        //        {
        //            string sql = @"UPDATE UACS_SEQ_CONFIG SET SEQ_VALUE = 0 WHERE SEQ_NAME = '2_2_CLTS_MODE'";
        //            DB2Connect.DBHelper.ExecuteNonQuery(sql);
        //            btnMode2.Text = "2-2行车模式：UACS";
        //            btnMode2.BackColor = Color.LightSteelBlue;
        //        }
        //    }
        //}

        //private void btnMode3_Click(object sender, EventArgs e)
        //{
        //    FrmYordToYordConfPassword password = new FrmYordToYordConfPassword();
        //    password.PassWords = "UACS1550";
        //    DialogResult retValue = password.ShowDialog();
        //    if (retValue != DialogResult.OK)
        //    {
        //        return;
        //    }

        //    List<string> lstAdress = new List<string>();
        //    lstAdress.Clear();
        //    lstAdress.Add("2_3_zAct");
        //    arrTagAdress = lstAdress.ToArray<string>();
        //    readTags();
        //    int height = Convert.ToInt32(get_value("2_3_zAct"));
        //    if (height <= 4000)
        //    {
        //        MessageBox.Show("行车高度不在4米以上，请在4米以上切换！");
        //        return;
        //    }
        //    else
        //    {
        //        if (btnMode3.Text.Contains("UACS"))
        //        {
        //            string sql = @"UPDATE UACS_SEQ_CONFIG SET SEQ_VALUE = 1 WHERE SEQ_NAME = '2_3_CLTS_MODE'";
        //            DB2Connect.DBHelper.ExecuteNonQuery(sql);
        //            btnMode3.Text = "2-3行车模式：CLTS";
        //            btnMode3.BackColor = Color.Yellow;
        //        }
        //        else
        //        {
        //            string sql = @"UPDATE UACS_SEQ_CONFIG SET SEQ_VALUE = 0 WHERE SEQ_NAME = '2_3_CLTS_MODE'";
        //            DB2Connect.DBHelper.ExecuteNonQuery(sql);
        //            btnMode3.Text = "2-3行车模式：UACS";
        //            btnMode3.BackColor = Color.LightSteelBlue;
        //        }
        //    }
        //}

        //private void button2_Click_1(object sender, EventArgs e)
        //{
        //    List<string> lstAdress = new List<string>();
        //    lstAdress.Clear();
        //    lstAdress.Add("TWO_CRANE_GO_D102");
        //    arrTagAdress = lstAdress.ToArray<string>();
        //    readTags();
        //    if (get_value("TWO_CRANE_GO_D102") == "0")
        //    {
        //        if (MessageBox.Show("确定开启步进梁双车上料吗？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
        //        {
        //            tagDataProvider.SetData("TWO_CRANE_GO_D102", "1");

        //            ////关闭双车收料
        //            //tagDataProvider.SetData("TWO_CRANE_GO_YSL", "0");
        //            ////UpCraneOrderConfigClose_EXIT_TO_YARD();

        //            ////关闭双车备料
        //            //tagDataProvider.SetData("TWO_CRANE_GO_YARD", "0");
        //            ////UpCraneOrderConfigClose_YARD_2_YARD();

        //            ////关闭双车上水槽
        //            //tagDataProvider.SetData("TWO_CRANE_GO_WATER_TANK", "0");
        //            ////UpCraneOrderConfigClose_YARD_2_WATER_TANK();

        //            //关闭倒垛策列配置
        //            //UpCraneFlagEnabledByStop1or6or7();
        //            //UpCraneFlagEnabledByStopNo1and6and7();

        //            button2.Text = "双车上料：开";
        //            button2.BackColor = Color.Yellow;
        //            ParkClassLibrary.HMILogger.WriteLog(button2.Text, "步进梁双车上料：开启", ParkClassLibrary.LogLevel.Info, this.Text);
        //        }
        //    }
        //    else if (get_value("TWO_CRANE_GO_D102") == "1")
        //    {
        //        if (MessageBox.Show("确定关闭步进梁双车上料吗？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
        //        {
        //            tagDataProvider.SetData("TWO_CRANE_GO_D102", "0");
        //            button2.Text = "双车上料：关";
        //            button2.BackColor = Color.LightSteelBlue;
        //            ParkClassLibrary.HMILogger.WriteLog(button2.Text, "步进梁双车上料：关闭", ParkClassLibrary.LogLevel.Info, this.Text);
        //        }
        //    }
        //}

        //private void button3_Click(object sender, EventArgs e)
        //{
        //    List<string> lstAdress = new List<string>();
        //    lstAdress.Clear();
        //    lstAdress.Add("TWO_CRANE_GO_YARD");
        //    arrTagAdress = lstAdress.ToArray<string>();
        //    readTags();
        //    if (get_value("TWO_CRANE_GO_YARD") == "0")
        //    {
        //        if (MessageBox.Show("确定开启步进梁双车备料吗？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
        //        {
        //            tagDataProvider.SetData("TWO_CRANE_GO_YARD", "1");
        //            UpCraneOrderConfigOpen_YARD_2_YARD();
        //            UpCraneFlagEnabledByOpen1or6or7();
        //            UpCraneFlagEnabledByStopNo1and6and7();

        //            ////关闭双车收料
        //            //tagDataProvider.SetData("TWO_CRANE_GO_YSL", "0");
        //            //UpCraneOrderConfigClose_EXIT_TO_YARD();

        //            ////关闭双车上料
        //            //tagDataProvider.SetData("TWO_CRANE_GO_D102", "0");

        //            ////关闭双车上水槽
        //            //tagDataProvider.SetData("TWO_CRANE_GO_WATER_TANK", "0");
        //            //UpCraneOrderConfigClose_YARD_2_WATER_TANK();

        //            button3.Text = "双车备料：开";
        //            button3.BackColor = Color.Yellow;
        //            ParkClassLibrary.HMILogger.WriteLog(button3.Text, "步进梁双车备料：开启", ParkClassLibrary.LogLevel.Info, this.Text);
        //        }
        //    }
        //    else if (get_value("TWO_CRANE_GO_YARD") == "1")
        //    {
        //        if (MessageBox.Show("确定关闭步进梁双车备料吗？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
        //        {
        //            tagDataProvider.SetData("TWO_CRANE_GO_YARD", "0");
        //            UpCraneOrderConfigClose_YARD_2_YARD();
        //            UpCraneFlagEnabledByStop1or6or7();
        //            button3.Text = "双车备料：关";
        //            button3.BackColor = Color.LightSteelBlue;
        //            ParkClassLibrary.HMILogger.WriteLog(button3.Text, "步进梁双车备料：关闭", ParkClassLibrary.LogLevel.Info, this.Text);
        //        }
        //    }
        //}

        //private void button4_Click(object sender, EventArgs e)
        //{
        //    List<string> lstAdress = new List<string>();
        //    lstAdress.Clear();
        //    lstAdress.Add("TWO_CRANE_GO_WATER_TANK");
        //    arrTagAdress = lstAdress.ToArray<string>();
        //    readTags();
        //    if (get_value("TWO_CRANE_GO_WATER_TANK") == "0")
        //    {
        //        if (MessageBox.Show("确定开启双车上料水槽吗？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
        //        {
        //            tagDataProvider.SetData("TWO_CRANE_GO_WATER_TANK", "1");
        //            UpCraneOrderConfigOpen_YARD_2_WATER_TANK();

        //            ////关闭双车收料
        //            //tagDataProvider.SetData("TWO_CRANE_GO_YSL", "0");
        //            //UpCraneOrderConfigClose_EXIT_TO_YARD();

        //            ////关闭双车上料
        //            //tagDataProvider.SetData("TWO_CRANE_GO_D102", "0");

        //            ////关闭双车备料
        //            //tagDataProvider.SetData("TWO_CRANE_GO_YARD", "0");
        //            //UpCraneOrderConfigClose_YARD_2_YARD();

        //            //关闭倒垛策列配置
        //            UpCraneFlagEnabledByStop1or6or7();
        //            UpCraneFlagEnabledByStopNo1and6and7();

        //            button4.Text = "双车上水槽：开";
        //            button4.BackColor = Color.Yellow;
        //            ParkClassLibrary.HMILogger.WriteLog(button4.Text, "双车上水槽：开启", ParkClassLibrary.LogLevel.Info, this.Text);
        //        }
        //    }
        //    else if (get_value("TWO_CRANE_GO_WATER_TANK") == "1")
        //    {
        //        if (MessageBox.Show("确定关闭运输链双车备料吗？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
        //        {
        //            tagDataProvider.SetData("TWO_CRANE_GO_WATER_TANK", "0");
        //            UpCraneOrderConfigClose_YARD_2_WATER_TANK();
        //            //UpCraneOrderConfigClose_YARD_2_YARD();
        //            //UpCraneFlagEnabledByStop1or6or7();
        //            button4.Text = "双车上水槽：关";
        //            button4.BackColor = Color.LightSteelBlue;
        //            ParkClassLibrary.HMILogger.WriteLog(button4.Text, "双车上水槽：关闭", ParkClassLibrary.LogLevel.Info, this.Text);
        //        }
        //    }
        //}

        //private void button5_Click(object sender, EventArgs e)
        //{
        //    List<string> lstAdress = new List<string>();
        //    lstAdress.Clear();
        //    lstAdress.Add("TWO_CRANE_GO_COLDCOIL");
        //    arrTagAdress = lstAdress.ToArray<string>();
        //    readTags();
        //    if (get_value("TWO_CRANE_GO_COLDCOIL") == "0")
        //    {
        //        if (MessageBox.Show("确定开启1-2只收冷卷吗？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
        //        {
        //            tagDataProvider.SetData("TWO_CRANE_GO_COLDCOIL", "1");
        //            button5.Text = "1-2收冷卷：开";
        //            button5.BackColor = Color.Yellow;
        //            ParkClassLibrary.HMILogger.WriteLog(button5.Text, "1-2只收冷卷：开启", ParkClassLibrary.LogLevel.Info, this.Text);
        //        }
        //    }
        //    else if (get_value("TWO_CRANE_GO_COLDCOIL") == "1")
        //    {
        //        if (MessageBox.Show("确定关闭1-2只收冷卷吗？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
        //        {
        //            tagDataProvider.SetData("TWO_CRANE_GO_COLDCOIL", "0");
        //            button5.Text = "1-2收冷卷：关";
        //            button5.BackColor = Color.LightSteelBlue;
        //            ParkClassLibrary.HMILogger.WriteLog(button5.Text, "1-2只收冷卷：关闭", ParkClassLibrary.LogLevel.Info, this.Text);
        //        }
        //    }
        //}

        //private void button6_Click(object sender, EventArgs e)
        //{
        //    List<string> lstAdress = new List<string>();
        //    lstAdress.Clear();
        //    lstAdress.Add("by12");
        //    arrTagAdress = lstAdress.ToArray<string>();
        //    readTags();
        //    if (get_value("by12") == "1")
        //    {
        //        if (MessageBox.Show("确定打开夹钳区域吗？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
        //        {
        //            tagDataProvider.SetData("by12", "0");
        //            button6.Text = "夹钳区域：开";
        //            button6.BackColor = Color.Yellow;
        //            ParkClassLibrary.HMILogger.WriteLog(button6.Text, "夹钳区域：开启", ParkClassLibrary.LogLevel.Info, this.Text);
        //        }
        //    }
        //    else if (get_value("by12") == "0")
        //    {
        //        if (MessageBox.Show("确定关闭夹钳区域吗？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
        //        {
        //            tagDataProvider.SetData("by12", "1");
        //            button6.Text = "夹钳区域：关";
        //            button6.BackColor = Color.LightSteelBlue;
        //            ParkClassLibrary.HMILogger.WriteLog(button6.Text, "夹钳区域：关闭", ParkClassLibrary.LogLevel.Info, this.Text);
        //        }
        //    }
        //} 
        #endregion
        /// <summary>
        /// 检修
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_Recondition_Click(object sender, EventArgs e)
        {
            Recondition frm = new Recondition(this);
            //frm.CraneNo = craneNO;
            frm.BayNO = "A";
            frm.CraneA1_X = CraneA1_X;
            frm.CraneA2_X = CraneA2_X;
            frm.CraneA3_X = CraneA3_X;
            frm.CraneA4_X = CraneA4_X;
            frm.ShowDialog();
        }
        /// <summary>
        /// 清扫
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_CraneClean_Click(object sender, EventArgs e)
        {
            FrmCubicleClean frm = new FrmCubicleClean(this);
            //frm.CraneNo = craneNO;
            frm.ShowDialog();
        }
    }
}
