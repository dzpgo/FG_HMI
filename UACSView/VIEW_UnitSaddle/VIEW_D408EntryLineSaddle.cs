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
using UACSPopupForm;
using Baosight.iSuperframe.Forms;
using System.Runtime.InteropServices;
using Baosight.iSuperframe.Common;
using Baosight.iSuperframe.Authorization.Interface;
using ParkClassLibrary;

namespace UACSView
{
    public partial class VIEW_D408EntryLineSaddle : FormBase
    {
        private Baosight.iSuperframe.Authorization.Interface.IAuthorization auth;
        #region -----------------------load加载----------------------------------


        public VIEW_D408EntryLineSaddle()
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
            this.Load += VIEW_D173EntryLineSaddle_Load;
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
        private const string AREA_SAFE_D408_ENTRY = "AREA_SAFE_D408_WR";

        public long theValue;
        public long thevalue
        {
            get { return theValue; }
            set { thevalue = value; }
        }

        void VIEW_D173EntryLineSaddle_Load(object sender, EventArgs e)
        {
            auth = FrameContext.Instance.GetPlugin<IAuthorization>() as IAuthorization;
            #region 绑定鞍座控件
            //dicSaddleControls["D208WR1A01"] = coilUnitSaddle1;
            dicSaddleControls["D408WR0003"] = coilUnitSaddle2;
            dicSaddleControls["D408WR0002"] = coilUnitSaddle3;
            dicSaddleControls["D408WR0001"] = coilUnitSaddle4;
            //dicSaddleControls["D208WR1A05"] = coilUnitSaddle5;
            //dicSaddleControls["D208WR1A06"] = coilUnitSaddle6;           
            #endregion

            //实例化机组鞍座处理类
            saddleMethod = new UnitSaddleMethod(constData.UnitNo_D408, constData.EntrySaddleDefine, constData.tagServiceName);
            saddleMethod.ReadDefintion();

            coilUnitStatus1.InitTagDataProvide(constData.tagServiceName);
            coilUnitStatus1.MyStatusTagName = AREA_SAFE_D408_ENTRY;
            coilUnitStatus2.InitTagDataProvide(constData.tagServiceName);
            coilUnitStatus2.MyStatusTagName = "D408_ENTRY_STEP_ENABLE";
            coilUnitStatus3.InitTagDataProvide(constData.tagServiceName);
            coilUnitStatus3.MyStatusTagName = "D408_ENTRY_STEP_1_OP_UNLOCK";

            lineSaddleTag.InitTagDataProvider(constData.tagServiceName);

            coilUnitSaddleButton1.MySaddleNo = "D408WR0001";
            coilUnitSaddleButton2.MySaddleNo = "D408WR0002";
            coilUnitSaddleButton3.MySaddleNo = "D408WR0003";
            //coilUnitSaddleButton6.MySaddleNo = "D208WR1A06";
            
            //conCraneStatus1_2.InitTagDataProvide(constData.tagServiceName);
            //conCraneStatus1_2.CraneNO = "1_2";
            //lstConCraneStatusPanel.Add(conCraneStatus1_2);

            craneStatusInBay.InitTagDataProvide(constData.tagServiceName);
            craneStatusInBay.AddCraneNO("1_2");
            craneStatusInBay.SetReady();

            conEntrySpecAction1.UnitNo = constData.UnitNo_D408;
            conEntrySpecAction1.BayNo = "Z63";

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
            }
            foreach (Control control in panel2.Controls)
            {
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

            entrySaddleInfo.getEntrySaddleDt(dataGridViewSaddleMessage, constData.UnitNo_D408);
            CoilPlan();
            //dgvColor();
            //是否开启定时器
            timer1.Enabled = true;
            //设定刷新时间
            timer1.Interval = 5000;;
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
            foreach (Control control in panel2.Controls)
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
            coilUnitStatus1.Refresh();
            coilUnitStatus2.Refresh();
            coilUnitStatus3.Refresh();
            entrySaddleInfo.getEntrySaddleDt(dataGridViewSaddleMessage, constData.UnitNo_D408);
            getSaddleMessage();
            //dgvColor();
        }
        /// <summary>
        /// 上料计划
        /// </summary>
        private void CoilPlan()
        {
            entrySaddleInfo.getL2PlanByUnitNo(dataGridViewPlanNum, constData.UnitNo_D408);
            dgvCheck(dataGridViewPlanNum);

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
                        conSaddle.CoilBackColor = Color.FromArgb(165, 222, 241);

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

                coilEntryMode1.UnitNO = constData.UnitNo_D408;

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

                saddleMethod.GetCoilLabel(constData.UnitNo_D408, lblNextCoil, label2);

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

        private void dgvColor()
        {        
            foreach (DataGridViewRow dgvRows in dataGridViewPlanNum.Rows)
            {
                string stockNO = dgvRows.Cells["STOCK_NO"].Value.ToString();
                string ststus = dgvRows.Cells["STATUS"].Value.ToString();
                //if (ststus == "库内" && stockNO != "" && (stockNO.Substring(0, 3) == "Z04" || stockNO.Substring(0, 3) == "Z05"))
                //{
                //    if (stockNO.Substring(0, 3) == "Z05")
                //    {
                //        dgvRows.DefaultCellStyle.BackColor = Color.White;
                //    }
                //    else if (stockNO.Substring(0, 3) == "Z04")
                //    {
                //        dgvRows.DefaultCellStyle.BackColor = Color.Green;
                //    }
                //    else
                //    {
                //        dgvRows.DefaultCellStyle.BackColor = Color.Red;
                //    }

                //}
                //else
                //{
                //    dgvRows.DefaultCellStyle.BackColor = Color.Red;
                //}

                if ((stockNO == "" || stockNO.Substring(0, 3) == "Z02" || stockNO.Substring(0, 3) == "Z03") && ststus != "机组鞍座")
                {
                    dgvRows.DefaultCellStyle.BackColor = Color.Red;

                }
                else
                {
                    dgvRows.DefaultCellStyle.BackColor = Color.White;
                }
            }
        }


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
                VIEW_D408EntryLineSaddle.SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
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

        private void button1_Click(object sender, EventArgs e)
        {
            auth.OpenForm("02 Z63跨监控");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FrmUnitRotateRequest unitRotate = new FrmUnitRotateRequest();
            unitRotate.UintNo = "D408";
            unitRotate.ShowDialog();
        }

        private void btnRotate_Click(object sender, EventArgs e)
        {
            int MANU_VALUE = 1;
            btnExecuteNonQuery(MANU_VALUE, dataGridViewPlanNum);
            CoilPlan();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            int MANU_VALUE = 0;
            btnExecuteNonQuery(MANU_VALUE, dataGridViewPlanNum);
            CoilPlan();
        }

        private void btnExecuteNonQuery(int MANU_VALUE, DataGridView dgv)
        {
            try
            {
                List<string> dt = new List<string>();
                List<string> coil = new List<string>();
                string strCoil = String.Empty;
                foreach (DataGridViewRow r in dgv.Rows)
                {
                    if (r.Cells["CHECK_COLUMN"].Value != null && (Int32)r.Cells["CHECK_COLUMN"].Value == 1)
                    {
                        string COIL_NO = r.Cells["COIL_NO"].Value.ToString().Trim();
                        if (COIL_NO != string.Empty)
                        {
                            string sql = string.Format("UPDATE UACS_YARDMAP_COIL SET MANU_VALUE = '{0}' WHERE COIL_NO = '{1}'", MANU_VALUE, COIL_NO);
                            dt.Add(sql);
                            coil.Add(COIL_NO);
                        }
                        else
                        {
                            throw new Exception("字段不能为空");
                        }
                    }
                }
                foreach (string sql in dt)
                {
                    DB2Connect.DBHelper.ExecuteNonQuery(sql);
                }
                MessageBox.Show(string.Format("成功修改{0}条记录！", dt.Count));

                //操作日志
                foreach (string coilNo in coil)
                {
                    if (coilNo != "")
                    {
                        strCoil += coilNo + ",";
                    }
                }
                if (MANU_VALUE == 1)
                {
                    HMILogger.WriteLog("带头旋转", "D408机组入口,钢卷" + strCoil + "设置为旋转180°", LogLevel.Info, this.Text);
                }
                else if (MANU_VALUE == 0)
                {
                    HMILogger.WriteLog("带头旋转", "D408机组入口,钢卷" + strCoil + "设置为不旋转", LogLevel.Info, this.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private Dictionary<string, int> dicCheck = new Dictionary<string, int>();
        private void dataGridViewPlanNum_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                for (int i = 0; i < dataGridViewPlanNum.Rows.Count; i++)
                {
                    string coil_NO = dataGridViewPlanNum.Rows[i].Cells["COIL_NO"].Value.ToString();
                    bool hasChecked = (bool)dataGridViewPlanNum.Rows[i].Cells["CHECK_COLUMN"].EditedFormattedValue;
                    if (hasChecked)
                    {
                        dicCheck[coil_NO] = 1;
                    }
                    else
                    {
                        dicCheck[coil_NO] = 0;

                    }
                    dataGridViewPlanNum.Rows[i].Cells["CHECK_COLUMN"].Value = dicCheck[coil_NO];
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.ToString());
            }
        }

        private void dgvCheck(DataGridView dgv)
        {
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                foreach (string key in dicCheck.Keys)
                {
                    if (dgv.Rows[i].Cells["COIL_NO"].Value.ToString() == key)
                    {
                        dgv.Rows[i].Cells["CHECK_COLUMN"].Value = dicCheck[key];
                    }
                }
            }
        }
    }
}
