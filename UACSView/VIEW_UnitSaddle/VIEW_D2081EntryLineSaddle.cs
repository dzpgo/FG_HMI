using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using UACSControls;
using UACSDAL;
using Baosight.iSuperframe.Forms;
using System.Runtime.InteropServices;

namespace UACSView
{
    public partial class VIEW_D2081EntryLineSaddle : FormBase
    {
        #region -----------------------load加载----------------------------------


        public VIEW_D2081EntryLineSaddle()
        {

            InitializeComponent();

            //x = this.Width;
            //y = this.Height;
            //setTag(this);

            Type dgvEntrySaddleType = this.dataGridViewSaddleMessage.GetType();
            PropertyInfo pi = dgvEntrySaddleType.GetProperty("DoubleBuffered",
                BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(this.dataGridViewSaddleMessage, true, null);

            Type dgvL2PlanType = this.dataGridViewPlanNum.GetType();
            PropertyInfo pi1 = dgvL2PlanType.GetProperty("DoubleBuffered",
                BindingFlags.Instance | BindingFlags.NonPublic);
            pi1.SetValue(this.dataGridViewPlanNum, true, null);
            this.Load += VIEW_D2081EntryLineSaddle_Load;
            dataGridViewPlanNum.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            foreach (DataGridViewColumn item in this.dataGridViewPlanNum.Columns)
            {
                item.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                item.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }



        private UnitSaddleTagRead lineSaddleTag = new UnitSaddleTagRead();
        private UnitSaddleMethod saddleMethod = null;
        private UnitEntrySaddleInfo entrySaddleInfo = new UnitEntrySaddleInfo();

        private Dictionary<string, CoilUnitSaddle> dicSaddleControls = new Dictionary<string, CoilUnitSaddle>();
        private bool tabActived = true;
        private const string AREA_SAFE_ENTRY_D401 = "AREA_SAFE_ENTRY_D401";

        public long theValue;
        public long thevalue
        {
            get { return theValue; }
            set { thevalue = value; }
        }

        void VIEW_D2081EntryLineSaddle_Load(object sender, EventArgs e)
        {
            #region 绑定鞍座控件
            dicSaddleControls["D208ENTRY0001"] = coilUnitSaddle1;
            dicSaddleControls["D208ENTRY0002"] = coilUnitSaddle2;
            dicSaddleControls["D208ENTRY0003"] = coilUnitSaddle3;
            //dicSaddleControls["D108ENTRY0004"] = coilUnitSaddle4;
            dicSaddleControls["D208ENTRY0005"] = coilUnitSaddle5;
            dicSaddleControls["D208ENTRY0006"] = coilUnitSaddle6;
            //dicSaddleControls["D108ENTRY0007"] = coilUnitSaddle7;
            //dicSaddleControls["D108ENTRY0101"] = coilUnitSaddle8;
            //dicSaddleControls["D108ENTRY0102"] = coilUnitSaddle9;
            //dicSaddleControls["D108ENTRY0103"] = coilUnitSaddle10;
            //dicSaddleControls["D108ENTRY0201"] = coilUnitSaddle11;
            //dicSaddleControls["D108ENTRY0202"] = coilUnitSaddle12;
            //dicSaddleControls["D108ENTRY0203"] = coilUnitSaddle13;
            #endregion

            //实例化机组鞍座处理类
            saddleMethod = new UnitSaddleMethod(constData.UnitNo_4, constData.EntrySaddleDefine, constData.tagServiceName);
            saddleMethod.ReadDefintion();

            coilUnitStatus1.InitTagDataProvide(constData.tagServiceName);
            coilUnitStatus1.MyStatusTagName = AREA_SAFE_ENTRY_D401;

            lineSaddleTag.InitTagDataProvider(constData.tagServiceName);

            coilUnitSaddleButton1.MySaddleNo = "D208ENTRY0001";
            coilUnitSaddleButton2.MySaddleNo = "D208ENTRY0002";
            coilUnitSaddleButton3.MySaddleNo = "D208ENTRY0003";
            coilUnitSaddleButton6.MySaddleNo = "D208ENTRY0006";

            conCraneStatus1_2.InitTagDataProvide(constData.tagServiceName);
            conCraneStatus1_2.CraneNO = "1_2";
            lstConCraneStatusPanel.Add(conCraneStatus1_2);

            craneStatusInBay.InitTagDataProvide(constData.tagServiceName);
            craneStatusInBay.AddCraneNO("1_2");
            craneStatusInBay.SetReady();

            conEntrySpecAction1.UnitNo = constData.UnitNo_4;
            conEntrySpecAction1.BayNo = "B";

            //把表中的tag名称赋值到控件中
            foreach (Control control in tableLayoutPanel2.Controls)
            {
                //添加解锁鞍座控件
                if (control is CoilUnitSaddleButton)
                {
                    CoilUnitSaddleButton t = (CoilUnitSaddleButton)control;
                    if (saddleMethod.DicSaddles.ContainsKey(t.MySaddleNo))
                    {
                        UnitSaddleBase theSaddleInfo = saddleMethod.DicSaddles[t.MySaddleNo];
                        if (!string.IsNullOrEmpty(theSaddleInfo.TagAdd_LockRequest) && theSaddleInfo.TagAdd_LockRequest != "")
                        {
                            t.MySaddleTagName = theSaddleInfo.TagAdd_LockRequest;
                            lineSaddleTag.AddTagName(theSaddleInfo.TagAdd_LockRequest);
                        }
                    }
                }
                //添加机组状态控件
                if (control is CoilUnitStatus)
                {
                    CoilUnitStatus t = (CoilUnitStatus)control;
                    if (!string.IsNullOrEmpty(t.MyStatusTagName) && t.MyStatusTagName != "")
                    {
                        lineSaddleTag.AddTagName(t.MyStatusTagName);
                    }
                }
            }

            lineSaddleTag.SetReady();
            //把实例化后的机组tag处理类装备每个控件
            foreach (Control control in tableLayoutPanel2.Controls)
            {
                if (control is CoilUnitSaddleButton)
                {
                    CoilUnitSaddleButton t = (CoilUnitSaddleButton)control;
                    t.InitUnitSaddle(lineSaddleTag);
                }
            }

            entrySaddleInfo.getEntrySaddleDt(dataGridViewSaddleMessage, constData.UnitNo_4);
            //是否开启定时器
            timer1.Enabled = true;
            //设定刷新时间
            timer1.Interval = 5000;
        }
        #endregion

        #region -----------------------钢卷信息----------------------------------
        private void timer_LineSaddleControl_Tick(object sender, EventArgs e)
        {
            //不在当前页面停止刷新
            if (tabActived == false)
            {
                return;
            }

            //if (thevalue == 1)
            //{
            //    this.tableLayoutPanel2.BackColor = Color.Transparent;
            //}
            //else
            //{
            //    this.tableLayoutPanel2.BackColor = Color.Red;
            //}

            lineSaddleTag.readTags();

            foreach (Control control in tableLayoutPanel2.Controls)
            {
                if (control is CoilUnitSaddleButton)
                {
                    CoilUnitSaddleButton t = (CoilUnitSaddleButton)control;
                    if (!string.IsNullOrEmpty(t.MySaddleTagName) && t.MySaddleTagName != "")
                    {
                        CoilUnitSaddleButton.delRefresh_Button_Light del = t.refresh_Button_Light;
                        del(lineSaddleTag.getTagValue(t.MySaddleTagName));
                    }
                }
            }
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                if (control is CoilUnitStatus)
                {
                    CoilUnitStatus t = (CoilUnitStatus)control;
                    if (!string.IsNullOrEmpty(t.MyStatusTagName) && t.MyStatusTagName != "")
                    {
                        CoilUnitStatus.delSetColor del = t.SetColor;
                        del(lineSaddleTag.getTagValue(t.MyStatusTagName));
                    }
                }
            }

            entrySaddleInfo.getEntrySaddleDt(dataGridViewSaddleMessage, constData.UnitNo_4);

            getSaddleMessage();
        }
        /// <summary>
        /// 上料计划
        /// </summary>
        private void CoilPlan()
        {
            entrySaddleInfo.getL2PlanByUnitNo(dataGridViewPlanNum, constData.UnitNo_4);

            //dgvColor();
        }
        private void getSaddleMessage()
        {
            saddleMethod.ReadDefintion();
            saddleMethod.getTagNameList();
            saddleMethod.getTagValues();

            foreach (string theL2SaddleName in dicSaddleControls.Keys)
            {
                if (saddleMethod.DicSaddles.ContainsKey(theL2SaddleName))
                {
                    CoilUnitSaddle conSaddle = dicSaddleControls[theL2SaddleName];

                    UnitSaddleBase theSaddleInfo = saddleMethod.DicSaddles[theL2SaddleName];
                    //鞍座反馈
                    if (theSaddleInfo.TagVal_IsLocked == 1)
                        conSaddle.UpVisiable = true;
                    else
                        conSaddle.UpVisiable = false;

                    //鞍座占位
                    if (theSaddleInfo.TagVal_IsOccupied == 1)
                        conSaddle.CoilBackColor = Color.Green;
                    else
                        conSaddle.CoilBackColor = Color.LightGray;

                    //钢卷号
                    if (theSaddleInfo.CoilNO != string.Empty)
                    {
                        conSaddle.CoilId = theSaddleInfo.CoilNO;
                        conSaddle.CoilStatus = 2;
                    }
                    else
                    {
                        conSaddle.CoilId = "";
                        conSaddle.CoilStatus = -10;
                    }
                }

            }
        }
        #endregion

        #region -----------------------刷新事件----------------------------------

        private void timer2_Tick(object sender, EventArgs e)
        {
            try
            {
                if (tabActived == false)
                {
                    return;
                }

                getSaddleMessage();

                if (!isControlPlan)
                {
                    CoilPlan();
                }

                coilEntryMode1.UnitNO = constData.UnitNo_4;
                conEntrySpecAction1.conGetAction();

                //if (conCraneStatus_4_2.ToStockNo.Contains(SaddleNo))
                //{
                //    if (lblNextCoil.Text.Trim() != conCraneStatus_4_2.NextCoilNo.Trim())
                //    {
                //        conCraneStatus_4_2.CheckNextCoil();
                //        lblNextCoil.BackColor = Color.Red;
                //    }
                //    else
                //    {
                //        lblNextCoil.BackColor = System.Drawing.SystemColors.Control;
                //    }
                //}
                //else
                //{
                //    lblNextCoil.BackColor = System.Drawing.SystemColors.Control;
                //}

                ClearMemory();

                //System.GC.Collect();
                //System.GC.WaitForPendingFinalizers();
            }
            catch (Exception er)
            {

            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            try
            {
                if (tabActived == false)
                {
                    return;
                }

                saddleMethod.GetCoilLabel(constData.UnitNo_4, lblNextCoil, label2);

                if (label2.Text.Trim().IndexOf("Z33") > -1)
                {
                    label2.BackColor = Color.Red;
                }
                else
                    label2.BackColor = System.Drawing.SystemColors.Control;

                //coilStatus1.RefreshControl();
                //coilStatus2.RefreshControl();
                //coilStatus3.RefreshControl();
                //coilStatus4.RefreshControl();
                //
                //coilButton1.RefreshControl();
                //coilButton2.RefreshControl();

                craneStatusInBay.getAllPLCStatusInBay(craneStatusInBay.lstCraneNO);

                foreach (conCraneStatus conCraneStatusPanel in lstConCraneStatusPanel)
                {
                    conCraneStatus.RefreshControlInvoke ConCraneStatusPanel_Invoke = new conCraneStatus.RefreshControlInvoke(conCraneStatusPanel.RefreshControl);
                    conCraneStatusPanel.BeginInvoke(ConCraneStatusPanel_Invoke, new Object[] { craneStatusInBay.DicCranePLCStatusBase[conCraneStatusPanel.CraneNO].Clone() });
                }

                System.GC.Collect();
                System.GC.WaitForPendingFinalizers();

            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region  -----------------------内存回收----------------------------------
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
                VIEW_D2081EntryLineSaddle.SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
            }
        }
        #endregion

        #region -----------------------字段变量----------------------------------
        /// <summary>
        /// 公共dataTable
        /// </summary>
        private DataTable SaddleDt = new DataTable();
        /// <summary>
        /// 鞍座控件
        /// </summary>
        //private Dictionary<string, UACS.CoilPicture> dicSaddleControls = new Dictionary<string, CoilPicture>();
        /// <summary>
        /// 吊运控件
        /// </summary>
        //private Dictionary<UACS.CoilPicture, UACS.CoilCranOrder> dicControl = new Dictionary<UACS.CoilPicture, UACS.CoilCranOrder>();
        /// <summary>
        /// 实例化鞍座出口类
        /// </summary>
        //private UACS.SaddleMethod saddleMethod = null;
        /// <summary>
        ///  控件名称
        /// </summary>
        //private string ControlNo;
        /// <summary>
        /// 管理计划顺序是否刷新
        /// </summary>
        private bool isControlPlan = false;


        private CraneStatusInBay craneStatusInBay = new CraneStatusInBay();

        private List<conCraneStatus> lstConCraneStatusPanel = new List<conCraneStatus>();
        /// <summary>
        /// 平台tag配置名称
        /// </summary>
        private const int Flag_Unit_Exit = 0;

        /// <summary>
        /// 根据吊运控件得到指定鞍座号
        /// </summary>
        private Dictionary<string, string> saddleNo = new Dictionary<string, string>();


        private List<string> listSaddleNo = new List<string>();
        #endregion

       
    }
}
