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
    public partial class VIEW_EntryLineSaddle : FormBase
    {
        #region -----------------------load加载----------------------------------


        public VIEW_EntryLineSaddle()
        {

            InitializeComponent();

            x = this.Width;
            y = this.Height;
            setTag(this);

            Type dgvEntrySaddleType = this.dataGridViewSaddleMessage.GetType();
            PropertyInfo pi = dgvEntrySaddleType.GetProperty("DoubleBuffered",
                BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(this.dataGridViewSaddleMessage, true, null);

            Type dgvL2PlanType = this.dataGridViewPlanNum.GetType();
            PropertyInfo pi1 = dgvL2PlanType.GetProperty("DoubleBuffered",
                BindingFlags.Instance | BindingFlags.NonPublic);
            pi1.SetValue(this.dataGridViewPlanNum, true, null);
            this.Load += VIEW_EntryLineSaddle_Load;
        }


        #region 控件大小随窗体大小等比例缩放
        private double x;//定义当前窗体的宽度
        private double y;//定义当前窗体的高度
        private void setTag(Control cons)
        {
            foreach (Control con in cons.Controls)
            {
                con.Tag = con.Width + ";" + con.Height + ";" + con.Left + ";" + con.Top + ";" + con.Font.Size;
                if (con.Controls.Count > 0)
                {
                    setTag(con);
                }
            }
        }
        private void setControls(double newx, double newy, Control cons)
        {
            //遍历窗体中的控件，重新设置控件的值
            foreach (Control con in cons.Controls)
            {
                //获取控件的Tag属性值，并分割后存储字符串数组
                if (con.Tag != null)
                {
                    string[] mytag = con.Tag.ToString().Split(new char[] { ';' });
                    //根据窗体缩放的比例确定控件的值
                    con.Width = Convert.ToInt32(System.Convert.ToSingle(mytag[0]) * newx);//宽度
                    con.Height = Convert.ToInt32(System.Convert.ToSingle(mytag[1]) * newy);//高度
                    con.Left = Convert.ToInt32(System.Convert.ToSingle(mytag[2]) * newx);//左边距
                    con.Top = Convert.ToInt32(System.Convert.ToSingle(mytag[3]) * newy);//顶边距                    
                    //Single currentSize = System.Convert.ToSingle(mytag[4]) * newy;//字体大小
                    //con.Font = new Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit);
                    if (con.Controls.Count > 0)
                    {
                        setControls(newx, newy, con);
                    }
                }
            }
        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            double newx = (this.Width) / (1.1 * x);
            double newy = (this.Height) / (1.1 * y) ;
            setControls(newx, newy, this);
        }

        #endregion

        private UnitSaddleTagRead lineSaddleTag = new UnitSaddleTagRead();
        private UnitSaddleMethod saddleMethod = null;
        private UnitEntrySaddleInfo entrySaddleInfo = new UnitEntrySaddleInfo();

        private Dictionary<string, CoilUnitSaddle> dicSaddleControls = new Dictionary<string, CoilUnitSaddle>();
        private bool tabActived = true;

        void VIEW_EntryLineSaddle_Load(object sender, EventArgs e)
        {
            #region 绑定鞍座控件
            dicSaddleControls["D401WR1A01"] = coilUnitSaddle1;
            dicSaddleControls["D401WR1A02"] = coilUnitSaddle2;
            dicSaddleControls["D401WR1A03"] = coilUnitSaddle3;
            dicSaddleControls["D401WR1A04"] = coilUnitSaddle4;
            dicSaddleControls["D401WR1A05"] = coilUnitSaddle5;
            dicSaddleControls["D401WR1A06"] = coilUnitSaddle6;
            dicSaddleControls["D401WR1A07"] = coilUnitSaddle7;
            #endregion

            //实例化机组鞍座处理类
            saddleMethod = new UnitSaddleMethod(constData.UnitNo, constData.EntrySaddleDefine, constData.tagServiceName);
            saddleMethod.ReadDefintion();

            lineSaddleTag.InitTagDataProvider(constData.tagServiceName);

            coilUnitSaddleButton1.MySaddleNo = "D401WR1A01";
            coilUnitSaddleButton2.MySaddleNo = "D401WR1A02";
            coilUnitSaddleButton3.MySaddleNo = "D401WR1A03";
            coilUnitSaddleButton4.MySaddleNo = "D401WR1A04";
            coilUnitSaddleButton5.MySaddleNo = "D401WR1A05";
            coilUnitSaddleButton6.MySaddleNo = "D401WR1A06";
            coilUnitSaddleButton7.MySaddleNo = "D401WR1A07";

            //把表中的tag名称赋值到控件中
            foreach (Control control in panelAutoScroll.Controls)
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
            foreach (Control control in panelAutoScroll.Controls)
            {
                if (control is CoilUnitSaddleButton)
                {
                    CoilUnitSaddleButton t = (CoilUnitSaddleButton)control;
                    t.InitUnitSaddle(lineSaddleTag);
                }
            }

            entrySaddleInfo.getEntrySaddleDt(dataGridViewSaddleMessage, constData.UnitNo);
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

            lineSaddleTag.readTags();

            foreach (Control control in panelAutoScroll.Controls)
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

            entrySaddleInfo.getEntrySaddleDt(dataGridViewSaddleMessage, constData.UnitNo);

            getSaddleMessage();
        }
        /// <summary>
        /// 上料计划
        /// </summary>
        private void CoilPlan()
        {
            entrySaddleInfo.getL2PlanByUnitNo(dataGridViewPlanNum, constData.UnitNo);

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
                    if (theSaddleInfo.TagVal_IsOccupied == 0)
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

                coilEntryMode1.UnitNO = constData.UnitNo;

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

                saddleMethod.GetCoilLabel(constData.UnitNo, lblNextCoil, label2);

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
                VIEW_EntryLineSaddle.SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
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
