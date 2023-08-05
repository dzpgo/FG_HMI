using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Runtime.InteropServices;
using System.Threading;

using UACSDAL;

namespace UACSControls
{
    /// <summary>
    /// Name:跨别控件
    /// Author:vella
    /// Date:2020/06/05
    /// </summary>
    public partial class UACSYardMonitor : UserControl
    {
        string bayNO = "";

        public string BayNO 
        {
            get { return bayNO; }
            set { bayNO = value; }
        }
        /// <summary>
        /// 行车数量
        /// </summary>
        int craneNums = 0;
        public int CraneNums
        {
            get { return craneNums; }
            set { craneNums = value; }
        }

        string[] craneNOs = {  };

        /// <summary>
        /// 行车号
        /// </summary>
        public string[] CraneNOs
        {
            get { return craneNOs; }
            set { craneNOs = value; }
        }
        /// <summary>
        /// 机组数量
        /// </summary>
        int unitNums = 0;

        public int UnitNums
        {
            get { return unitNums; }
            set { unitNums = value; }
        }

        /// <summary>
        /// 机组名称
        /// </summary>
        string[] unitNames = { };

        public string[] UnitNames
        {
            get { return unitNames; }
            set { unitNames = value; }
        }

        /// <summary>
        /// 停止刷新，终止时所用获取数据的状态
        /// </summary>
        bool stopReflesh = true;

        public bool StopReflesh
        {
            get { return stopReflesh; }
            set { stopReflesh = value; StopRefreshEven(stopReflesh); }
        }
        public enum HMI_MDDE
        { 
            ADMIN = 0,
            UASER = 1,
            GUEST = 2        
        }
        /// <summary>
        /// 用户的模式
        /// </summary>
        HMI_MDDE mode = HMI_MDDE.UASER;

        public HMI_MDDE Mode
        {
            get { return mode; }
            set { mode = value; }
        }

        /// <summary>
        /// X坐标递增
        /// </summary>
        bool xAxisRight = true;
        [Description("X坐标递增"), Category("自定义")]
        public  bool XAxisRight
        {
            get { return xAxisRight;}
            set { xAxisRight = value;}
        }

        /// <summary>
        /// Y坐标递增
        /// </summary>      
        bool yAxisDown = true;
         [Description("Y坐标递增"), Category("自定义")]
        public bool YAxisDown
        {
            get { return yAxisDown; }
            set { yAxisDown = value; }
        }

        private conAreaModel AreaInStockZ12;
        private List<conCraneStatus> lstConCraneStatusPanel = new List<conCraneStatus>();
        private List<conCrane> listConCraneDisplay = new List<conCrane>();
        private List<string> listCraneNo = new List<string>();

        private FrmMoreStock form;
        private conAreaModel areaModel;

        private conUnitSaddleModel unitSaddleModel;
        private conParkingCarModel parkingCarModel;
        private CraneStatusInBay craneStatusInBay;

        private static FrmSeekCoil frmSeekCoil;
        private conStockSaddleModel saddleInStock_Z11_Z12;


        /// <summary>
        /// 初始化线程
        /// </summary>
        Thread thread1 = null;
        bool thread1Run = false;

        private static ManualResetEvent mre = new ManualResetEvent(false);

        /// <summary>
        /// 线程timer1
        /// </summary>
        System.Threading.Timer statusTimer1 = null;

        System.Threading.Timer statusTimer2 = null;

        System.Threading.Timer statusTimer3 = null;

        System.Threading.Timer statusTimer4 = null;

        private static System.Threading.AutoResetEvent autoEvent = new AutoResetEvent(false);

        public UACSYardMonitor()
        {
            InitializeComponent();

            //初始化数据
            //bayNO = "A";
            craneNums = 4;
            //craneNOs = new string[] {"1_1","1_2","1_3","1_4"};
            this.BackColor = ColorTranslator.FromHtml("#FFFFFE");


            this.Load += UACSYardMonitor_Load;
            //this.Paint += UACSYardMonitor_Paint;
            this.pnlYard.Paint += pnlYard_Paint;
            
        }






        #region 事件
        void UACSYardMonitor_Load(object sender, EventArgs e)
        {
            try
            {
                //
                labBayNO.Text = bayNO + labBayNO.Text;

                //
                areaModel = new conAreaModel();
                unitSaddleModel = new conUnitSaddleModel();
                parkingCarModel = new conParkingCarModel();
                craneStatusInBay = new CraneStatusInBay();
                AreaInStockZ12 = new conAreaModel();

                //初始化行车
                for (int i = 1; i <= craneNums; i++)
                {
                    string str1 = "conCrane" + i.ToString();
                    string str2 = "conCraneStatus" + i.ToString();
                    conCrane tmpConCrane = (conCrane)GetControlInstance(pnlYard, str1);
                    if (tmpConCrane != null)
                    {
                        //初始化行车
                        if (craneNOs.Length >= i)
                        {
                            tmpConCrane.Name = craneNOs[i - 1];
                            //行车显示控件配置
                            tmpConCrane.InitTagDataProvide(constData.tagServiceName);
                            tmpConCrane.CraneNO = craneNOs[i - 1];
                            listConCraneDisplay.Add(tmpConCrane);
                        }
                        else
                        {
                            throw new NoNullAllowedException("行车号异常,请检查行车号配置");
                        }
                        //
                        tmpConCrane.Height = pnlYard.Height - 5;
                        tmpConCrane.Visible = true;
                        //设置可用性
                        if (mode == HMI_MDDE.GUEST)
                        {
                            tmpConCrane.Enabled = false;
                        }
                        else
                        {
                            tmpConCrane.Enabled = true;
                        }

                    }
                    //行车控件状态
                    conCraneStatus tmpconCraneStatus = (conCraneStatus)GetControlInstance(tlpnlCrStatus, str2);
                    if (tmpconCraneStatus != null)
                    {
                        tmpconCraneStatus.Visible = true;
                        //---------------------行车状态控件配置-------------------------------
                        tmpconCraneStatus.InitTagDataProvide(constData.tagServiceName);
                        tmpconCraneStatus.CraneNO = tmpConCrane.CraneNO;
                        lstConCraneStatusPanel.Add(tmpconCraneStatus);
                    }
                    //设置可用性
                    if (mode == HMI_MDDE.GUEST)
                    {
                        tmpconCraneStatus.Enabled = false;
                    }
                    else
                    {
                        tmpconCraneStatus.Enabled = true;
                    }

                    //---------------------行车tag配置--------------------------------
                    craneStatusInBay.InitTagDataProvide(constData.tagServiceName);
                    craneStatusInBay.AddCraneNO(tmpConCrane.CraneNO);
                    craneStatusInBay.SetReady();

                }

                //初始化完成，开始加载数据
                Thread t = new Thread(new ParameterizedThreadStart(DataInitLoad));
                t.IsBackground = true;
                t.Start(bayNO);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
            }
            



        }

        
        void pnlYard_Paint(object sender, PaintEventArgs e)
        {
            Panel pnl = (Panel)sender;
            int lineHeght, lineWidth; 
            lineHeght = pnl.Height;
            lineWidth = pnl.Width;
            //int offSet = 3;
            base.OnPaint(e);
            Pen myPen = new Pen(Color.White, 5.0f);
            Rectangle rtg1 = new Rectangle(new Point(0, 0), new Size(lineWidth - 2 , lineHeght - 2 ));
            e.Graphics.DrawRectangle(myPen, rtg1);
        }
        private void btnShow_Click(object sender, EventArgs e)
        {
            if (btnShow.Text == "<")
            {
                panel8.Visible = true;
                //panel4.Visible = true;
                
                btnShow.Text = ">";
            }
            else
            {
                if (btnShowXY.Text == "取消显示")
                {
                    btnShowXY_Click(btnShow, null);            
                }

                panel8.Visible = false;
                btnShow.Text = "<";
            }
        }

        private void btnShowXY_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.Text == "显示XY")
            {
                btnShowXY.Text = "取消显示";
                panel4.Visible = true;
                statusTimer4.Change(500, 3000);
            }
            else
            {
                panel4.Visible = false;
                btnShowXY.Text = "显示XY";
                statusTimer4.Change(-1, -1);

            }

        }

        private void btnShowCrane_Click(object sender, EventArgs e)
        {
            if (btnShowCrane.Text == "隐藏行车")
            {
                foreach (var item in listConCraneDisplay)
                {
                    if (item is conCrane)
                    {
                        ((conCrane)item).Visible = false;
                    }
                }

                btnShowCrane.Text = "显示行车";
                if (statusTimer1 != null)
                {
                    statusTimer1.Change(-1, -1);
                }
            }
            else
            {
                foreach (var item in listConCraneDisplay)
                {
                    if (item is conCrane)
                    {
                        ((conCrane)item).Visible = true;
                    }
                }

                btnShowCrane.Text = "隐藏行车";
                if (statusTimer1 !=null)
                {
                    statusTimer1.Change(1000, 1500);
                }

            }
        }
        #endregion

        #region 方法

        delegate void delegateDataInitLoad(object o);
        /// <summary>
        /// 数据加载
        /// </summary>
        /// <param name="o"></param>
        private void DataInitLoad(object o)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new delegateDataInitLoad(DataInitLoad), new object[] { o });
                    return;
                }

                //TODO
                statusTimer1 = new System.Threading.Timer(queryTimer_Tick1,
                           autoEvent, -1, -1);

                statusTimer2 = new System.Threading.Timer(queryTimer_Tick2,
                            autoEvent, -1, -1);

                statusTimer3 = new System.Threading.Timer(queryTimer_Tick3,
                     autoEvent, -1, -1);

                statusTimer4 = new System.Threading.Timer(queryTimer_Tick4,
                     autoEvent, -1, -1);
                StopRefreshEven(StopReflesh);


                //小区数据加载
                LoadUnitInfo();
                LoadParkingCarInfo();
                LoadAreaInfo();

                // MessageBox.Show("加载完成");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        delegate void queryTimer_TickDelegate(object state);
        /// <summary>
        /// 查询行车状态数据，周期1500毫秒
        /// </summary>
        /// <param name="stateInfo"></param>
        void queryTimer_Tick1(object statusInfo)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new queryTimer_TickDelegate(queryTimer_Tick1), new object[] { statusInfo });
                    return;
                }
                //TODO
                //获取PLC状态
                craneStatusInBay.getAllPLCStatusInBay(craneStatusInBay.lstCraneNO);
                AreaInStockZ12.conInit(pnlYard, bayNO, SaddleBase.tagServiceName,
                   constData.Z42BaySpaceX, constData.Z42BaySpaceY, pnlYard.Width, pnlYard.Height,
                   xAxisRight, yAxisDown,false, AreaInfo.AreaType.StockArea);
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
                         constData.Z42BaySpaceX,
                         constData.Z42BaySpaceY,
                         pnlYard.Width,
                         pnlYard.Height,
                         xAxisRight,
                         yAxisDown,
                         4560,         
                         pnlYard,
                         false
                    });
                }
                //MessageBox.Show("获取行车数据成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        delegate void queryTimer2_TickDelegate(object state);
        /// <summary>
        /// 查询库区状态数据，周期1500毫秒
        /// </summary>
        /// <param name="stateInfo"></param>
        void queryTimer_Tick2(object statusInfo)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new queryTimer2_TickDelegate(queryTimer_Tick2), new object[] { statusInfo });
                    return;
                }

                LoadAreaInfo();

                //MessageBox.Show("获取库区数据成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        delegate void queryTimer3_TickDelegate(object state);
        /// <summary>
        /// 查询机组状态数据，周期3000毫秒
        /// </summary>
        /// <param name="stateInfo"></param>
        void queryTimer_Tick3(object statusInfo)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new queryTimer3_TickDelegate(queryTimer_Tick3), new object[] { statusInfo });
                    return;
                }

                LoadUnitInfo();
                LoadParkingCarInfo();
                //MessageBox.Show("获取机组数据成功");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        delegate void queryTimer4_TickDelegate(object state);
        /// <summary>
        /// 查询坐标数据，周期3000毫秒
        /// </summary>
        /// <param name="stateInfo"></param>
        void queryTimer_Tick4(object statusInfo)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new queryTimer4_TickDelegate(queryTimer_Tick4), new object[] { statusInfo });
                    return;
                }

                //计算X方向上的比例关系
                double xScale = Convert.ToDouble(pnlYard.Width) / Convert.ToDouble(constData.Z42BaySpaceX);
                //计算Y方向的比例关系
                double yScale = Convert.ToDouble(pnlYard.Height) / Convert.ToDouble(constData.Z42BaySpaceY);

                Point p = this.pnlYard.PointToClient(Control.MousePosition);
                if (p.X <= this.pnlYard.Location.X || p.X >= this.pnlYard.Location.X + this.pnlYard.Width ||
                    p.Y < this.pnlYard.Location.Y || p.Y >= this.pnlYard.Location.Y + this.pnlYard.Height)
                {
                    return;
                }
                txtX.Text = Convert.ToString(Convert.ToInt32(Convert.ToDouble(p.X) / xScale));
                txtY.Text = Convert.ToString(Convert.ToInt32((Convert.ToDouble(pnlYard.Height) - Convert.ToDouble(p.Y)) / yScale));

                //MessageBox.Show("获取坐标数据成功");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
            }
        }
        /// <summary>
        /// 获取机组数据
        /// </summary>
        private void LoadUnitInfo()
        {
            for (int i = 0; i < unitNums; i++)
            {
                if (i < unitNames.Length)
                {
                    unitSaddleModel.conInit(pnlYard,
                    unitNames[i],
                    constData.tagServiceName,
                    constData.Z42BaySpaceX,
                    constData.Z42BaySpaceY,
                    pnlYard.Width,
                    pnlYard.Height,
                    xAxisRight,
                    yAxisDown,
                    bayNO);                
                }
            }
        }
        /// <summary>
        /// 获取停车位数据
        /// </summary>
        private void LoadParkingCarInfo()
        {
            parkingCarModel.conInit(pnlYard,
                bayNO,
                constData.tagServiceName,
                constData.Z42BaySpaceX,
                constData.Z42BaySpaceY,
                pnlYard.Width,
                pnlYard.Height,
                xAxisRight,
                yAxisDown);
        }
        /// <summary>
        /// 获取停车位数据
        /// </summary>
        private void LoadAreaInfo()
        {
            areaModel.conInit(pnlYard,
                bayNO,
                constData.tagServiceName,
                constData.Z42BaySpaceX,
                constData.Z42BaySpaceY,
                pnlYard.Width,
                pnlYard.Height,
                xAxisRight,
                yAxisDown,
                false,
                AreaInfo.AreaType.AllType);
        }
        /// <summary>
        /// 根据指定容器和控件名字，获得控件
        /// </summary>
        /// <param name="obj">容器</param>
        /// <param name="strControlName">控件名字</param>
        /// <returns>控件</returns>
        private object GetControlInstance(object obj, string strControlName)
        {
            System.Collections.IEnumerator Controls = null;//所有控件
            Control c = null;//当前控件
            Object cResult = null;//查找结果
            if (obj.GetType() == this.GetType())//窗体
            {
                Controls = this.Controls.GetEnumerator();
            }
            else//控件
            {
                Controls = ((Control)obj).Controls.GetEnumerator();
            }
            while (Controls.MoveNext())//遍历操作
            {
                c = (Control)Controls.Current;//当前控件
                if (c.HasChildren)//当前控件是个容器
                {
                    if (c.Name == strControlName)//是容器，返回
                    {
                        return c;
                    }

                    cResult = GetControlInstance(c, strControlName);//递归查找
                    if (cResult == null)//当前容器中没有，跳出，继续查找
                    {
                        continue;
                    }
                    else//找到控件，返回
                        return cResult;
                }
                else if (c.Name == strControlName)//不是容器，同时找到控件，返回
                {
                    return c;
                }
            }
            return null;//控件不存在
        }

        private void StopRefreshEven(bool flag)
        {
            if (flag)
            {
                if (statusTimer1 !=null)
                {
                    statusTimer1.Change(-1, -1); 
                }
                if (statusTimer2 != null)
                {
                    statusTimer2.Change(-1, -1);
                }
                if (statusTimer3 != null)
                {
                    statusTimer3.Change(-1, -1);
                }
                if (statusTimer4 != null)
                {
                    statusTimer4.Change(-1, -1);
                }            
            }
            else
            {
                if (statusTimer1 != null)
                {
                    statusTimer1.Change(1000, 1500);
                }
                if (statusTimer2 != null)
                {
                    statusTimer2.Change(1500, 3000);
                }
                if (statusTimer3 != null)
                {
                    statusTimer3.Change(1500, 3000);
                }
                if (statusTimer4 != null)
                {
                    statusTimer4.Change(-1, -1);
                }   

            }

        }
        #endregion

        /// <summary>
        /// 资源释放
        /// </summary>
        public void MonitorDispose()
        {
            StopReflesh = true;
            mre.Set();

            if(statusTimer1 != null)
            {
                statusTimer1.Dispose();
            }
            if (statusTimer2 != null)
            {
                statusTimer2.Dispose();
            }
            if (statusTimer3 != null)
            {
                statusTimer3.Dispose();
            }
            if (statusTimer4!= null)
            {
                statusTimer4.Dispose();
            }
        }

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
                UACSYardMonitor.SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
            }
        }
        #endregion

        private void btnSeekCoil_Click(object sender, EventArgs e)
        {
            if (frmSeekCoil == null || frmSeekCoil.IsDisposed)
            {
                frmSeekCoil = new FrmSeekCoil();
                frmSeekCoil.saddleInStock_Z11_Z12 = saddleInStock_Z11_Z12;
                frmSeekCoil.Z11_Z12Panel = pnlYard;
                frmSeekCoil.Z11_Z12_Width = pnlYard.Width;
                frmSeekCoil.Z11_Z12_Height = pnlYard.Height;
                frmSeekCoil.BayNo = bayNO;
                frmSeekCoil.Show();
            }
            else
            {
                frmSeekCoil.WindowState = FormWindowState.Normal;
                frmSeekCoil.Activate();
            }
        }

        

        

    }
}
