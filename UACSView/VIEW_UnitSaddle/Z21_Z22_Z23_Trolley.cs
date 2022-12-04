using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Baosight.iSuperframe.Forms;
using UACS;
using UACSControls;
using UACSPopupForm;
using Baosight.iSuperframe.TagService;

namespace UACSview
{
    public partial class Z21_Z22_Z23_Trolley : FormBase
    {
        Baosight.iSuperframe.TagService.DataCollection<object> TagValues = new DataCollection<object>();
        private Baosight.iSuperframe.TagService.Controls.TagDataProvider tagDP = new Baosight.iSuperframe.TagService.Controls.TagDataProvider();
        #region 数据库连接
        private static Baosight.iSuperframe.Common.IDBHelper dbHelper = null;
        //连接数据库
        private static Baosight.iSuperframe.Common.IDBHelper DBHelper
        {
            get
            {
                if (dbHelper == null)
                {
                    try
                    {
                        dbHelper = Baosight.iSuperframe.Common.DataBase.DBFactory.GetHelper("ZJDB0");//平台连接数据库的Text
                    }
                    catch (System.Exception e)
                    {
                        //throw e;
                    }
                }
                return dbHelper;
            }
        }
        #endregion

        private const string tagServiceName = "iplature";
        private const string Z21BayNo = "Z21";
        private const string Z22BayNo = "Z22";
        private const string Z23BayNo = "Z23";
        private const string SaddleNo = "MC1";
        private const int Flag_Unit_Exit = 1;
        /// <summary>
        /// 公共dataTable
        /// </summary>
        private DataTable SaddleDt = new DataTable();
        /// <summary>
        /// 鞍座控件
        /// </summary>
        private Dictionary<string, UACS.CoilPicture> dicSaddleControls = new Dictionary<string, CoilPicture>();
        /// <summary>
        /// 吊运控件
        /// </summary>
        private Dictionary<UACS.CoilPicture, UACS.CoilCranOrder> dicControl = new Dictionary<UACS.CoilPicture, UACS.CoilCranOrder>();
        /// <summary>
        /// 实例化鞍座出口类
        /// </summary>
        private UACS.SaddleMethod saddleMethod = new SaddleMethod();
        /// <summary>
        /// 根据吊运控件得到指定鞍座号
        /// </summary>
        private Dictionary<string, string> saddleNo = new Dictionary<string, string>();
        /// <summary>
        /// 台车故障
        /// </summary>
        private const string MC_MALFUNCTION = "MC_FAULT";
        /// <summary>
        /// 台车运行
        /// </summary>
        private const string MC_RUNNING_STATE = "MC_RUNNING_STATE";
        /// <summary>
        /// 安全门
        /// </summary>
        private const string MC_SAFE_DOOR = "MC_SAFE_DOOR";
        /// <summary>
        /// 作业模式
        /// </summary>
        private const string MC_AUTO_MODE = "MC_AUTO_MODE";


        public Z21_Z22_Z23_Trolley()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
            this.Load += Z21_Z22_Z23_Trolley_Load;
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
        void Z21_Z22_Z23_Trolley_Load(object sender, EventArgs e)
        {
            // 根据指定值附鞍座控件
            dicSaddleControls["Z21MC10001"] = coilPictureZ21;
            dicSaddleControls["Z22MC10001"] = coilPictureZ22;
            dicSaddleControls["Z23MC10001"] = coilPictureZ23;
          
            // 根据鞍座附吊运控件
            dicControl[coilPictureZ23] = coilCranOrderZ23;
            dicControl[coilPictureZ22] = coilCranOrderZ22;
            dicControl[coilPictureZ21] = coilCranOrderZ21;
            // 加载初始化
            saddleMethod = new UACS.SaddleMethod(SaddleNo, Flag_Unit_Exit, tagServiceName);

            CoilMessage();

            //-----------------------------故障------------------------------
            coilUnitStatus1.InitTagDataProvide(tagServiceName);
            coilUnitStatus1.MyStatusTagName = MC_MALFUNCTION;

            //-----------------------------运行信号------------------------------
            coilUnitStatus6.InitTagDataProvide(tagServiceName);
            coilUnitStatus6.MyStatusTagName = MC_RUNNING_STATE;

            //-----------------------------安全门------------------------------
            coilUnitStatus2.InitTagDataProvide(tagServiceName);
            coilUnitStatus2.MyStatusTagName = "MC_SAFE_DOOR_Z21";
            coilUnitStatus4.InitTagDataProvide(tagServiceName);
            coilUnitStatus4.MyStatusTagName = "MC_SAFE_DOOR_Z22";
            coilUnitStatus5.InitTagDataProvide(tagServiceName);
            coilUnitStatus5.MyStatusTagName = "MC_SAFE_DOOR_Z23";
            //-----------------------------作业模式------------------------------
            coilUnitStatus3.InitTagDataProvide(tagServiceName);
            coilUnitStatus3.MyStatusTagName = MC_AUTO_MODE;

            coilPictureZ21.ContextMenuStrip.Enabled = false;
            coilPictureZ22.ContextMenuStrip.Enabled = false;

            //
            conTrolleyTagZ21.BayNO = Z21BayNo;
            conTrolleyTagZ21.InitTagDataProvide(tagServiceName);


            conTrolleyTagZ22.BayNO = Z22BayNo;
            conTrolleyTagZ22.InitTagDataProvide(tagServiceName);

            conTrolleyTagZ23.BayNO = Z23BayNo;
            conTrolleyTagZ23.InitTagDataProvide(tagServiceName);
            //conEntrySpecActionZ21.UnitNo = "MC1";
            //conEntrySpecActionZ21.BayNo = "A";

            //conEntrySpecActionB.UnitNo = "MC1";
            //conEntrySpecActionB.BayNo = "B";
            button7.Click += button4_Click;
            button9.Click += button4_Click;

            timer1.Enabled = true;
            timer2.Enabled = true;
        }
        /// <summary>
        /// 查询鞍座信息
        /// </summary>
        private void CoilMessage()
        {
            System.GC.Collect();

            saddleMethod.ReadDefintion();
            saddleMethod.getTagNameList();
            saddleMethod.getTagValues();

            // 鞍座信息
            //dataGridViewSaddleMessage.DataSource = saddleMethod.getExitSaddleDt(SaddleDt, SaddleNo);

            foreach (string theL2SaddleName in dicSaddleControls.Keys)
            {
                if (saddleMethod.DicSaddles.ContainsKey(theL2SaddleName))
                {
                    // 找到指定的鞍座
                    UACS.CoilPicture conSaddle = dicSaddleControls[theL2SaddleName];
                    // 获取钢卷号
                    Saddle theSaddleInfo = saddleMethod.DicSaddles[theL2SaddleName];
                    
                    // 给吊运控件赋值
                    UACS.CoilCranOrder coil = dicControl[conSaddle];

                    // 锁定反馈
                    if (theSaddleInfo.TagVal_IsLocked == 1)
                    {
                        conSaddle.UpVisiable = true;
                    }
                    else
                    {
                        conSaddle.UpVisiable = false;
                    }


                    if (theL2SaddleName == "Z21MC10001")
                    {
                        // 占位
                        if (theSaddleInfo.TagVal_IsOccupied == 1 && conTrolleyTagZ21.pTrolleyATInt == 1)
                            conSaddle.CoilBackColor = Color.Green;
                        else
                            conSaddle.CoilBackColor = Color.LightGray;
                    }
                    else if (theL2SaddleName == "Z22MC10001")
                    {
                        if (theSaddleInfo.TagVal_IsOccupied == 1 && conTrolleyTagZ22.pTrolleyATInt == 1)
                            conSaddle.CoilBackColor = Color.Green;
                        else
                            conSaddle.CoilBackColor = Color.LightGray;
                    }
                    else if (theL2SaddleName == "Z23MC10001")
                    {
                        if (theSaddleInfo.TagVal_IsOccupied == 1 && conTrolleyTagZ23.pTrolleyATInt == 1)
                            conSaddle.CoilBackColor = Color.Green;
                        else
                            conSaddle.CoilBackColor = Color.LightGray;
                    }
                    else
                    {
                        //if (theSaddleInfo.TagVal_IsOccupied == 0)
                        //    conSaddle.CoilBackColor = Color.Green;
                        //else
                        //    conSaddle.CoilBackColor = Color.LightGray;
                    }

                    // 有无钢卷号
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

                    CoilCranOrder.CoilNoDelegate del = new CoilCranOrder.CoilNoDelegate(coil.EntryStatusOrCoil);
                    del(theSaddleInfo.CoilNO);
                }
            }
            this.Refresh();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            long a, b;
            try
            {
                if (tabActived == false)
                {
                    return;
                }
                 conTrolleyTagZ21.SetReadyA_B();
                 conTrolleyTagZ21.RefreshControlA_B(out a, out b);

                 conTrolleyTagZ22.SetReadyA_B();
                 conTrolleyTagZ22.RefreshControlA_B(out a, out b);

                 conTrolleyTagZ23.SetReadyA_B();
                 conTrolleyTagZ23.RefreshControlA_B(out a, out b);

                 coilUnitStatus1.RefreshControl();
                 coilUnitStatus2.RefreshControl();
                 coilUnitStatus3.RefreshControl();
                 coilUnitStatus4.RefreshControl();
                 coilUnitStatus5.RefreshControl();
                 coilUnitStatus6.RefreshControl();

                System.GC.Collect();
                 System.GC.WaitForPendingFinalizers();
                 
            }
            catch (Exception er)
            {
                timer1.Enabled = false;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            try
            {
                if (tabActived == false)
                {
                    return;
                }
                CoilMessage();
                //conEntrySpecActionZ21.conGetAction("A");
                //conEntrySpecActionB.conGetAction("B");

                System.GC.Collect();
                System.GC.WaitForPendingFinalizers();

            }
            catch (Exception er)
            {

                this.timer2.Enabled = false;
            }
           
        }

        #region --------------------------画面切换-------------------------------
        bool tabActived = true;
        void MyTabActivated(object sender, EventArgs e)
        {
            tabActived = true;
        }
        void MyTabDeactivated(object sender, EventArgs e)
        {
            tabActived = false;
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            long arrival, occupied;
            conTrolleyTagZ21.SetReadyA_B();
            conTrolleyTagZ21.RefreshControlA_B(out arrival, out occupied);
            if(arrival != 1 || occupied == 1)
            {
                MessageBox.Show("台车未到位或台车上有卷！");
                return;
            }
            else
            {
                TrolleyFrmYardToYardRequest frm = new TrolleyFrmYardToYardRequest();
                frm.BayNo = "Z21";
                frm.Show();
            }
            
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            long arrival, occupied;
            conTrolleyTagZ22.SetReadyA_B();
            conTrolleyTagZ22.RefreshControlA_B(out arrival, out occupied);
            if (arrival != 1 || occupied == 1)
            {
                MessageBox.Show("台车未到位或台车上有卷！");
                return;
            }
            else
            {
                TrolleyFrmYardToYardRequest frm = new TrolleyFrmYardToYardRequest();
                frm.BayNo = "Z22";
                frm.Show();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            long arrival, occupied;
            conTrolleyTagZ23.SetReadyA_B();
            conTrolleyTagZ23.RefreshControlA_B(out arrival, out occupied);
            if (arrival != 1 || occupied == 1)
            {
                MessageBox.Show("台车未到位或台车上有卷！");
                return;
            }
            else
            {
                TrolleyFrmYardToYardRequest frm = new TrolleyFrmYardToYardRequest();
                frm.BayNo = "Z23";
                frm.Show();
            }
        }

        private string strStockZ21 = string.Empty;
        private string strSaddle_L2NameZ21 = string.Empty;
        private string strStockZ22 = string.Empty;
        private string strSaddle_L2NameZ22 = string.Empty;
        private string strStockZ23 = string.Empty;
        private string strSaddle_L2NameZ23 = string.Empty;
        private void btnInsertZ21_Click(object sender, EventArgs e)
        {
            strStockZ21 = "Z21MC10001";
            strSaddle_L2NameZ21 = "Z21MC10001";
            strStockZ22 = "Z22MC10001";
            strSaddle_L2NameZ22 = "Z22MC10001";
            strStockZ23 = "Z23MC10001";
            strSaddle_L2NameZ23 = "Z23MC10001";

            string sql1 = string.Format("UPDATE UACS_YARDMAP_STOCK_DEFINE SET STOCK_STATUS = 2,MAT_NO = '{0}'WHERE STOCK_NO = '{1}'", txtCoilNoZ21.Text.Trim(), strStockZ21);
            string sql2 = string.Format("UPDATE UACS_LINE_EXIT_L2INFO SET HAS_COIL = 1,COIL_NO = '{0}'WHERE SADDLE_L2NAME = '{1}'", txtCoilNoZ21.Text.Trim(), strSaddle_L2NameZ21);
            string sql3 = string.Format("UPDATE UACS_YARDMAP_STOCK_DEFINE SET STOCK_STATUS = 0,LOCK_FLAG = 2,MAT_NO = ''WHERE STOCK_NO = '{0}'", strStockZ22);
            string sql4 = string.Format("UPDATE UACS_LINE_EXIT_L2INFO SET HAS_COIL = 0,COIL_NO = '' WHERE SADDLE_L2NAME = '{0}'", strSaddle_L2NameZ22);
            string sql5 = string.Format("UPDATE UACS_YARDMAP_STOCK_DEFINE SET STOCK_STATUS = 0,LOCK_FLAG = 2,MAT_NO = ''WHERE STOCK_NO = '{0}'", strStockZ23);
            string sql6 = string.Format("UPDATE UACS_LINE_EXIT_L2INFO SET HAS_COIL = 0,COIL_NO = '' WHERE SADDLE_L2NAME = '{0}'", strSaddle_L2NameZ23);
            try
            {
                if (txtCoilNoZ21.Text.Trim().Length != 11)
                {
                    MessageBox.Show("输入的卷号不正确，请重新输入！");
                    txtCoilNoZ21.Clear();
                    txtCoilNoZ21.Focus();
                    return;
                }
                if (MessageBox.Show("确定是否修改台车鞍座状态？", "修改提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    DBHelper.ExecuteNonQuery(sql1);
                    DBHelper.ExecuteNonQuery(sql2);
                    DBHelper.ExecuteNonQuery(sql3);
                    DBHelper.ExecuteNonQuery(sql4);
                    DBHelper.ExecuteNonQuery(sql5);
                    DBHelper.ExecuteNonQuery(sql6);
                    MessageBox.Show("修改成功", "修改提示");
                    txtCoilNoZ21.Text = "";
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void btnInsertZ22_Click(object sender, EventArgs e)
        {
            strStockZ21 = "Z21MC10001";
            strSaddle_L2NameZ21 = "Z21MC10001";
            strStockZ22 = "Z22MC10001";
            strSaddle_L2NameZ22 = "Z22MC10001";
            strStockZ23 = "Z23MC10001";
            strSaddle_L2NameZ23 = "Z23MC10001";

            string sql1 = string.Format("UPDATE UACS_YARDMAP_STOCK_DEFINE SET STOCK_STATUS = 2,MAT_NO = '{0}' WHERE STOCK_NO = '{1}'", txtCoilNoZ22.Text.Trim(), strStockZ22);
            string sql2 = string.Format("UPDATE UACS_LINE_EXIT_L2INFO SET HAS_COIL = 1,COIL_NO = '{0}' WHERE SADDLE_L2NAME = '{1}'", txtCoilNoZ22.Text.Trim(), strSaddle_L2NameZ22);
            string sql3 = string.Format("UPDATE UACS_YARDMAP_STOCK_DEFINE SET STOCK_STATUS = 0,LOCK_FLAG = 2,MAT_NO = '' WHERE STOCK_NO = '{0}'", strStockZ21);
            string sql4 = string.Format("UPDATE UACS_LINE_EXIT_L2INFO SET HAS_COIL = 0,COIL_NO = '' WHERE SADDLE_L2NAME = '{0}'", strSaddle_L2NameZ21);
            string sql5 = string.Format("UPDATE UACS_YARDMAP_STOCK_DEFINE SET STOCK_STATUS = 0,LOCK_FLAG = 2,MAT_NO = '' WHERE STOCK_NO = '{0}'", strStockZ23);
            string sql6 = string.Format("UPDATE UACS_LINE_EXIT_L2INFO SET HAS_COIL = 0,COIL_NO = '' WHERE SADDLE_L2NAME = '{0}'", strSaddle_L2NameZ23);

            try
            {
                if (txtCoilNoZ22.Text.Trim().Length != 11)
                {
                    MessageBox.Show("输入的卷号不正确，请重新输入！");
                    txtCoilNoZ22.Clear();
                    txtCoilNoZ22.Focus();
                    return;
                }
                if (MessageBox.Show("确定是否修改台车鞍座状态？", "修改提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    DBHelper.ExecuteNonQuery(sql1);
                    DBHelper.ExecuteNonQuery(sql2);
                    DBHelper.ExecuteNonQuery(sql3);
                    DBHelper.ExecuteNonQuery(sql4);
                    DBHelper.ExecuteNonQuery(sql5);
                    DBHelper.ExecuteNonQuery(sql6);
                    MessageBox.Show("修改成功","修改提示");
                    txtCoilNoZ22.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnInsertZ23_Click(object sender, EventArgs e)
        {
            strStockZ21 = "Z21MC10001";
            strSaddle_L2NameZ21 = "Z21MC10001";
            strStockZ22 = "Z22MC10001";
            strSaddle_L2NameZ22 = "Z22MC10001";
            strStockZ23 = "Z23MC10001";
            strSaddle_L2NameZ23 = "Z23MC10001";

            string sql1 = string.Format("UPDATE UACS_YARDMAP_STOCK_DEFINE SET STOCK_STATUS = 2,MAT_NO = '{0}' WHERE STOCK_NO = '{1}'", txtCoilNoZ23.Text.Trim(), strStockZ23);
            string sql2 = string.Format("UPDATE UACS_LINE_EXIT_L2INFO SET HAS_COIL = 1,COIL_NO = '{0}' WHERE SADDLE_L2NAME = '{1}'", txtCoilNoZ23.Text.Trim(), strSaddle_L2NameZ23);
            string sql3 = string.Format("UPDATE UACS_YARDMAP_STOCK_DEFINE SET STOCK_STATUS = 0,LOCK_FLAG = 2,MAT_NO = '' WHERE STOCK_NO = '{0}'", strStockZ21);
            string sql4 = string.Format("UPDATE UACS_LINE_EXIT_L2INFO SET HAS_COIL = 0,COIL_NO = '' WHERE SADDLE_L2NAME = '{0}'", strSaddle_L2NameZ21);
            string sql5 = string.Format("UPDATE UACS_YARDMAP_STOCK_DEFINE SET STOCK_STATUS = 0,LOCK_FLAG = 2,MAT_NO = '' WHERE STOCK_NO = '{0}'", strStockZ22);
            string sql6 = string.Format("UPDATE UACS_LINE_EXIT_L2INFO SET HAS_COIL = 0,COIL_NO = '' WHERE SADDLE_L2NAME = '{0}'", strSaddle_L2NameZ22);

            try
            {
                if (txtCoilNoZ23.Text.Trim().Length != 11)
                {
                    MessageBox.Show("输入的卷号不正确，请重新输入！");
                    txtCoilNoZ23.Clear();
                    txtCoilNoZ23.Focus();
                    return;
                }
                if (MessageBox.Show("确定是否修改台车鞍座状态？", "修改提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    DBHelper.ExecuteNonQuery(sql1);
                    DBHelper.ExecuteNonQuery(sql2);
                    DBHelper.ExecuteNonQuery(sql3);
                    DBHelper.ExecuteNonQuery(sql4);
                    DBHelper.ExecuteNonQuery(sql5);
                    DBHelper.ExecuteNonQuery(sql6);
                    MessageBox.Show("修改成功", "修改提示");
                    txtCoilNoZ23.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            strStockZ21 = "Z21MC10001";
            strSaddle_L2NameZ21 = "Z21MC10001";
            strStockZ22 = "Z22MC10001";
            strSaddle_L2NameZ22 = "Z22MC10001";
            strStockZ23 = "Z23MC10001";
            strSaddle_L2NameZ23 = "Z23MC10001";
            string sql1 = string.Format("UPDATE UACS_YARDMAP_STOCK_DEFINE SET STOCK_STATUS = 0,MAT_NO = '' WHERE STOCK_NO = '{0}'", strStockZ21);
            string sql2 = string.Format("UPDATE UACS_LINE_EXIT_L2INFO SET HAS_COIL = 0,COIL_NO = '' WHERE SADDLE_L2NAME = '{0}'", strSaddle_L2NameZ21);
            try
            {

                if (MessageBox.Show("确定是否修改台车鞍座状态？", "清除提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    DBHelper.ExecuteNonQuery(sql1);
                    DBHelper.ExecuteNonQuery(sql2);
                    MessageBox.Show("清除成功", "清除提示");
                    txtCoilNoZ21.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            strStockZ21 = "Z21MC10001";
            strSaddle_L2NameZ21 = "Z21MC10001";
            strStockZ22 = "Z22MC10001";
            strSaddle_L2NameZ22 = "Z22MC10001";
            strStockZ23 = "Z23MC10001";
            strSaddle_L2NameZ23 = "Z23MC10001";
            string sql1 = string.Format("UPDATE UACS_YARDMAP_STOCK_DEFINE SET STOCK_STATUS = 0,MAT_NO = ''WHERE STOCK_NO = '{0}'", strStockZ22);
            string sql2 = string.Format("UPDATE UACS_LINE_EXIT_L2INFO SET HAS_COIL = 0,COIL_NO = '' WHERE SADDLE_L2NAME = '{0}'", strSaddle_L2NameZ22);
            try
            {

                if (MessageBox.Show("确定是否修改台车鞍座状态？", "清除提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    DBHelper.ExecuteNonQuery(sql1);
                    DBHelper.ExecuteNonQuery(sql2);
                    MessageBox.Show("清除成功", "清除提示");
                    txtCoilNoZ22.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            strStockZ23 = "Z23MC10001";
            strSaddle_L2NameZ23 = "Z23MC10001";
            string sql1 = string.Format("UPDATE UACS_YARDMAP_STOCK_DEFINE SET STOCK_STATUS = 0,MAT_NO = ''WHERE STOCK_NO = '{0}'", strStockZ23);
            string sql2 = string.Format("UPDATE UACS_LINE_EXIT_L2INFO SET HAS_COIL = 0,COIL_NO = '' WHERE SADDLE_L2NAME = '{0}'", strSaddle_L2NameZ23);
            try
            {

                if (MessageBox.Show("确定是否修改台车鞍座状态？", "清除提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    DBHelper.ExecuteNonQuery(sql1);
                    DBHelper.ExecuteNonQuery(sql2);
                    MessageBox.Show("清除成功", "清除提示");
                    txtCoilNoZ23.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            tagDP.ServiceName = "iplature";
            tagDP.AutoRegist = true;
            TagValues.Clear();
            TagValues.Add("MC_STOP_REQUEST_SET", null);
            tagDP.Attach(TagValues);

            if(button4.Text == "紧停")
            {
                DialogResult ret = MessageBox.Show("确定要台车停车吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (ret == DialogResult.Cancel)
                {
                    return;
                }               
                tagDP.SetData("MC_STOP_REQUEST_SET", "0");
                button4.Text = "复位";
                button7.Text = "复位";
                button9.Text = "复位";
                button4.BackColor = Color.Green;
                button7.BackColor = Color.Green;
                button9.BackColor = Color.Green;
            }
            else if (button4.Text == "复位")
            {
                DialogResult ret = MessageBox.Show("确定要台车复位吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (ret == DialogResult.Cancel)
                {
                    return;
                }
                tagDP.SetData("MC_STOP_REQUEST_SET", "1");
                button4.Text = "紧停";
                button7.Text = "紧停";
                button9.Text = "紧停";
                button4.BackColor = Color.Tomato;
                button7.BackColor = Color.Tomato;
                button9.BackColor = Color.Tomato;
            }
                   
        }
    }
}
