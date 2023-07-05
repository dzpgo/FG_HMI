using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Baosight.iSuperframe.Forms;
using Baosight.iSuperframe.TagService;
using Baosight.iSuperframe.Authorization.Interface;
using ParkingControlLibrary;
using ParkClassLibrary;
using UACSDAL;
using UACSView.View_Parking;

namespace UACSParking
{
    /// <summary>
    /// 车辆出库作业管理
    /// </summary>
    public partial class PorductMatManage : FormBase
    {
        IAuthorization auth = Baosight.iSuperframe.Common.FrameContext.Instance.GetPlugin<Baosight.iSuperframe.Authorization.Interface.IAuthorization>()
        as Baosight.iSuperframe.Authorization.Interface.IAuthorization;

        #region Tag配置
        Baosight.iSuperframe.TagService.DataCollection<object> inDatas = new Baosight.iSuperframe.TagService.DataCollection<object>();
        private string[] arrTagAdress;
        private void readTags()
        {
            try
            {
                inDatas.Clear();
                tagDP.GetData(arrTagAdress, out inDatas);
            }
            catch (Exception ex)
            {
                return;
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

        #region iPlature配置
        private Baosight.iSuperframe.TagService.Controls.TagDataProvider tagDP = null;
        public Baosight.iSuperframe.TagService.Controls.TagDataProvider TagDP
        {
            get
            {
                if (tagDP == null)
                {
                    try
                    {
                        tagDP = new Baosight.iSuperframe.TagService.Controls.TagDataProvider();
                        tagDP.ServiceName = "iplature";
                        tagDP.AutoRegist = true;
                    }
                    catch (System.Exception er)
                    {
                        MessageBox.Show(er.Message);
                    }
                }
                return tagDP;
            }
            //set { tagDP = value; }
        }
        #endregion

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
                    catch (System.Exception er)
                    {
                        MessageBox.Show(er.Message);
                    }
                }
                return dbHelper;
            }
        }
        #endregion
        //datagridview1
        DataTable dt = new DataTable();
        DataTable dt_selected = new DataTable();
        DataTable dt_Laser = new DataTable();
        DataTable dtNull = new DataTable();
        ToolTip toolTip1 = new ToolTip();
        private Dictionary<string, string> dicParkingNo = new Dictionary<string, string>();
        string carHearDrection = "";

        //

        bool hasSetColumn = false;
        bool hasParkSize = false;
        bool isStowage = false;
        bool hasCar = true;  //车位无车
        Int16 curCarType;  //当前车辆类型

        int coilsWeight = 0;   //添加材料重量
        //当前停车位，画面跳转
        string parkingNO = "";
        //
        string[] dgvColumnsName = { "ORDER_NO", "MAT_CNAME", "FROM_STOCK_NO", "TO_STOCK_NO", "BAY_NO" };
        string[] dgvHeaderText = { "指令号", "物料代码", "取料位", "落料位", "跨别" };
        //配载时间显示
        //private DateTime dtimeLaserEnd;
        bool isReadTime = false;
        string strLaserTime = "";
        Baosight.iSuperframe.TagService.DataCollection<object> TagValues = new DataCollection<object>();
        public PorductMatManage()
        {
            InitializeComponent();
            this.Load += PorductMatManage_Load;
        }
        public PorductMatManage(string parkNO)
        {
            InitializeComponent();
            this.Load += PorductMatManage_Load;
            BindParkingNoAndAreaNo();
            cmbArea.Text = GetOperateAreaByBay(parkNO);
            parkingNO = parkNO;
        }
        private bool isOpenPasswordForm = false;
        public PorductMatManage(bool _isOpenPasswordForm, string _bayNo, string parkNO)
        {
            InitializeComponent();
            this.Load += PorductMatManage_Load;
            isOpenPasswordForm = _isOpenPasswordForm;
            cmbArea.Text = _bayNo;
            parkingNO = parkNO;
        }

        void PorductMatManage_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer1.Enabled = false;
        }
        //tag点事件处理
        void tagDP_DataChangedEvent(object sender, Baosight.iSuperframe.TagService.Interface.DataChangedEventArgs e)
        {
            if (isStowage == true)
            {
                if (cbbPacking.Text.Contains('A'))
                {
                    reflreshParkingCoilstate(cbbPacking.Text.Trim());
                    RefreshOrderDgv(cbbPacking.Text.Trim());
                    return;
                }
            }
            RefreshHMI();
        }

        void PorductMatManage_Load(object sender, EventArgs e)
        {
            this.FormClosed += PorductMatManage_FormClosed;
            //dataGridView2.CellFormatting += dataGridView2_CellFormatting;
            TagDP.DataChangedEvent += tagDP_DataChangedEvent;

            for (int i = 0; i < dataGridView2.Columns.Count; i++)
            {
                dt_selected.Columns.Add(dataGridView2.Columns[i].Name);
            }
            //tagDP.ServiceName = "iplature";
            //tagDP.AutoRegist = true;
            TagValues.Clear();
            TagValues.Add("EV_NEW_PARKING_CARLEAVE", null);
            TagValues.Add("EV_NEW_PARKING_MDL_OUT_CAL_JUDGE", null);
            TagValues.Add("EV_PARKING_LINE_EXIT_TO_CAR_LASEREND_END", null);
            TagDP.Attach(TagValues);

            //初始化dataGridview属性
            ManagerHelper.DataGridViewInit(dataGridView2);
            ManagerHelper.DataGridViewInit(dataGridView_LASER);

            ManagerHelper.DataGridViewInit(dgvOrder);
            CreatDgvHeader(dgvOrder, dgvColumnsName, dgvHeaderText);
            dgvOrder.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            BindParkingNoAndAreaNo();
            Bind_cbbAreaName(cmbArea);
            GetComboxOnParkingByBay(cbbPacking);

            //GetComboxOnParkingByBay();
            cbbPacking.SelectedValue = "A1";
            cbbPacking.SelectedIndexChanged += cbbPacking_SelectedIndexChanged;
            //开启定时器、
            timer1.Enabled = true;
            #region  tooltipshow
            // Create the ToolTip and associate with the Form container.

            // Set up the delays for the ToolTip.
            toolTip1.AutoPopDelay = 10000;
            toolTip1.InitialDelay = 1000;
            toolTip1.ReshowDelay = 500;
            // Force the ToolTip text to be displayed whether or not the form is active.
            toolTip1.ShowAlways = true;
            #endregion

            parkLaserOut1.LabClick += parkLaserOut1_LabClick;
            if (parkingNO != "")//画面跳转
            {
                cmbArea.Text = GetOperateAreaByBay(parkingNO);
                cbbPacking.Text = parkingNO;
            }
            //绑定双击事件
            bindParkControlEvent();
            GetParkInfoByBay();
            RefreshHMI();
        }


        void cbbPacking_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbPacking.Text.Trim() != "请选择" && cbbPacking.Text.Trim() != "" && cbbPacking.Text.Trim().Contains('A'))
            {
                isStowage = false;
                hasCar = true;
                hasParkSize = false;
                RefreshHMI();
                btnOperateStrat.ForeColor = Color.White;  //Color.Black;
                btnOperatePause.ForeColor = Color.White;  //Color.Black;
            }
            //
            isReadTime = false;
        }

        //private string GetOperateArea(string parkNO)
        //{
        //    string area = "";
        //    try
        //    {
        //        if (parkNO.Contains("Z11") && parkNO.Contains("A"))
        //        {
        //            area = "1-6通道";
        //        }
        //        else if (parkNO.Contains("Z11") && parkNO.Contains("B"))
        //        {
        //            area = "1-7通道";
        //        }

        //        else if (parkNO.Contains("Z12") && parkNO.Contains("B"))
        //        {
        //            area = "1-4通道";
        //        }
        //        else if (parkNO.Contains("Z12") && parkNO.Contains("C"))
        //        {
        //            area = "1-5通道";
        //        }
        //        else
        //        {

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(string.Format("{0},{1}", ex.StackTrace.ToString(), ex.Message.ToString()));
        //    }
        //    return area;
        //}


        #region -----------------------------调用方法-------------------------------------
        /// <summary>
        /// 绑定跨号
        /// </summary>
        private void Bind_cbbAreaName(ComboBox comBox)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TypeValue");
            dt.Columns.Add("TypeName");
            //cmbArea.Items.Clear();
            try
            {
                string sqlText = @"SELECT DISTINCT BAY_NO as TypeValue,BAY_NAME as TypeName FROM UACS_YARDMAP_BAY_DEFINE";
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sqlText))
                {
                    while (rdr.Read())
                    {
                        DataRow dr = dt.NewRow();

                        if (rdr["TypeName"].ToString().Trim() != "")
                        {
                            dr["TypeValue"] = rdr["TypeValue"];
                            dr["TypeName"] = rdr["TypeName"];
                            dt.Rows.Add(dr);
                        }
                    }
                }
                //绑定列表下拉框数据
                comBox.DataSource = dt;
                comBox.DisplayMember = "TypeName";
                comBox.ValueMember = "TypeValue";
                comBox.SelectedItem = 1;

            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0},{1}", ex.StackTrace.ToString(), ex.Message.ToString()));
            }
        }
        ///// <summary>
        ///// 绑定停车位与跨号
        ///// </summary>
        //private void BindParkingNoAndAreaNo()
        //{
        //    try
        //    {

        //        string sqlText = @"SELECT B.BAY_NAME BAY_NAME,A.ID ID FROM UACS_YARDMAP_PARKINGSITE A LEFT JOIN UACS_YARDMAP_BAY_DEFINE B ON B.BAY_NO = A.YARD_NO";
        //        using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sqlText))
        //        {
        //            dicParkingNo.Clear();
        //            while (rdr.Read())
        //            {
        //                string AreaName = rdr["BAY_NAME"].ToString().Trim();
        //                dicParkingNo[rdr["ID"].ToString().Trim()] = AreaName.Trim();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(string.Format("{0},{1}", ex.StackTrace.ToString(), ex.Message.ToString()));
        //    }
        //}

        /// <summary>
        /// 绑定停车位与跨号
        /// </summary>
        private void BindParkingNoAndAreaNo()
        {
            try
            {

                string sqlText = @"SELECT B.BAY_NAME BAY_NAME,A.PARKING_NO ID FROM UACS_PARKING_INFO_DEFINE A LEFT JOIN UACS_YARDMAP_BAY_DEFINE B ON B.BAY_NO = A.YARD_NO";
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sqlText))
                {
                    dicParkingNo.Clear();
                    while (rdr.Read())
                    {
                        string AreaName = rdr["BAY_NAME"].ToString().Trim();
                        dicParkingNo[rdr["ID"].ToString().Trim()] = AreaName.Trim();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0},{1}", ex.StackTrace.ToString(), ex.Message.ToString()));
            }
        }
        private void GetComboxOnParkingByBay(ComboBox comBox)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TypeValue");
            dt.Columns.Add("TypeName");
            //准备数据
            try
            {
                string AreaNo = cmbArea.SelectedValue.ToString().Trim();
                string sqlText = @"SELECT DISTINCT PARKING_NO as TypeValue,PAKRING_NAME as TypeName FROM UACS_PARKING_INFO_DEFINE WHERE YARD_NO = '" + AreaNo + "' AND PARKING_TYPE = '3' ";
                //string sqlText = @"SELECT DISTINCT ID as TypeValue,NAME as TypeName FROM UACS_PARKING_INFO_DEFINE WHERE YARD_NO = '" + AreaNo + "'";
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sqlText))
                {
                    while (rdr.Read())
                    {
                        DataRow dr = dt.NewRow();

                        if (rdr["TypeName"].ToString().Trim() != "")
                        {
                            dr["TypeValue"] = rdr["TypeValue"];
                            dr["TypeName"] = rdr["TypeName"];
                            dt.Rows.Add(dr);
                        }
                    }
                }
                //绑定列表下拉框数据
                comBox.DataSource = dt;
                comBox.DisplayMember = "TypeName";
                comBox.ValueMember = "TypeValue";
                comBox.SelectedItem = 0;
                //comBox.Text = "请选择";
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0},{1}", ex.StackTrace.ToString(), ex.Message.ToString()));
            }
        }

        private void GetComboxOnParkingByBay()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TypeValue");
            dt.Columns.Add("TypeName");
            //准备数据
            try
            {
                string str1 = "";

                if (cmbArea.Text.Contains("1550"))
                {
                    str1 = "Z01";
                }
                else
                {

                }
                //string sqlText = @"SELECT DISTINCT ID as TypeValue,NAME as TypeName FROM UACS_PARKING_INFO_DEFINE ";
                string sqlText = @"SELECT DISTINCT PARKING_NO as TypeValue,PAKRING_NAME as TypeName FROM UACS_PARKING_INFO_DEFINE WHERE PARKING_TYPE = '3' ";
                using (IDataReader rdr = DBHelper.ExecuteReader(sqlText))
                {
                    while (rdr.Read())
                    {
                        DataRow dr = dt.NewRow();
                        //if (rdr["TypeName"].ToString().Contains(str1))
                        if (rdr["TypeName"].ToString().Contains(str1) || cmbArea.Text.Trim() == "")
                        {
                            dr["TypeValue"] = rdr["TypeValue"];
                            dr["TypeName"] = rdr["TypeName"];
                            dt.Rows.Add(dr);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0},{1}", ex.StackTrace.ToString(), ex.Message.ToString()));
            }
            //绑定列表下拉框数据
            this.cbbPacking.DataSource = dt;
            this.cbbPacking.DisplayMember = "TypeName";
            this.cbbPacking.ValueMember = "TypeValue";
            cbbPacking.SelectedItem = 0;
            //cbbPacking.Text = "请选择";           //
        }

        private string GetOperateAreaByBay(string parkNO)
        {
            string area = "";
            try
            {
                if (parkNO != "")
                {
                    area = dicParkingNo[parkNO];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0},{1}", ex.StackTrace.ToString(), ex.Message.ToString()));
            }
            return area;
        }
        private string GetAreaByBay()
        {
            string area = "";
            try
            {
                if (cbbPacking.Text.Trim() != "")
                {
                    area = dicParkingNo[cbbPacking.Text.Trim()];
                }
                RefreshHMI();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0},{1}", ex.StackTrace.ToString(), ex.Message.ToString()));
            }
            return area;
        }
        /// <summary>
        /// 绑定停车位信息
        /// </summary>
        //private void GetComboxOnParking()
        //{
        //    DataTable dt = new DataTable();
        //    dt.Columns.Add("TypeValue");
        //    dt.Columns.Add("TypeName");
        //    //准备数据
        //    try
        //    {
        //        string str1 = "";
        //        string str2 = "";

        //        if (cmbArea.Text.Contains("2"))
        //        {
        //            str1 = "Z03";
        //            str2 = "A";
        //        }
        //        else if (cmbArea.Text.Contains("3"))
        //        {
        //            str1 = "Z03";
        //            str2 = "B";
        //        }
        //        else if (cmbArea.Text.Contains("5"))
        //        {
        //            str1 = "Z04";
        //            str2 = "A";
        //        }
        //        else
        //        {

        //        }
        //        string sqlText = @"SELECT DISTINCT ID as TypeValue,NAME as TypeName FROM UACS_YARDMAP_PARKINGSITE ";
        //        using (IDataReader rdr = DBHelper.ExecuteReader(sqlText))
        //        {
        //            while (rdr.Read())
        //            {
        //                DataRow dr = dt.NewRow();

        //                if (rdr["TypeName"].ToString().Contains(str1) && rdr["TypeName"].ToString().Contains(str2) || cmbArea.Text.Trim() == "")
        //                {
        //                    dr["TypeValue"] = rdr["TypeValue"];
        //                    dr["TypeName"] = rdr["TypeName"];
        //                    dt.Rows.Add(dr);
        //                }

        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(string.Format("{0},{1}", ex.StackTrace.ToString(), ex.Message.ToString()));
        //    }
        //    //绑定列表下拉框数据
        //    this.cbbPacking.DataSource = dt;
        //    this.cbbPacking.DisplayMember = "TypeName";
        //    this.cbbPacking.ValueMember = "TypeValue";
        //    cbbPacking.SelectedItem = 0;
        //    //cbbPacking.Text = "请选择";           //
        //}
        //private void GetComboxOnParkingByBay()
        //{
        //    DataTable dt = new DataTable();
        //    dt.Columns.Add("TypeValue");
        //    dt.Columns.Add("TypeName");
        //    //准备数据
        //    try
        //    {
        //        string str1 = "";
        //        string str2 = "";

        //        if (cmbArea.Text.Contains("Z21"))
        //        {
        //            str1 = "Z21";
        //            str2 = "Z08";
        //        }
        //        else if (cmbArea.Text.Contains("Z22"))
        //        {
        //            str1 = "Z22";
        //            str2 = "Z07";
        //        }
        //        else if (cmbArea.Text.Contains("Z23"))
        //        {
        //            str1 = "Z23";
        //            str2 = "23";
        //        }
        //        else
        //        {

        //        }
        //        string sqlText = @"SELECT DISTINCT ID as TypeValue,NAME as TypeName FROM UACS_YARDMAP_PARKINGSITE ";
        //        using (IDataReader rdr = DBHelper.ExecuteReader(sqlText))
        //        {
        //            while (rdr.Read())
        //            {
        //                DataRow dr = dt.NewRow();
        //                //if (rdr["TypeName"].ToString().Contains(str1))
        //                if (rdr["TypeName"].ToString().Contains(str1) || rdr["TypeName"].ToString().Contains(str2) || cmbArea.Text.Trim() == "")
        //                {
        //                    dr["TypeValue"] = rdr["TypeValue"];
        //                    dr["TypeName"] = rdr["TypeName"];
        //                    dt.Rows.Add(dr);
        //                }

        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(string.Format("{0},{1}", ex.StackTrace.ToString(), ex.Message.ToString()));
        //    }
        //    //绑定列表下拉框数据
        //    this.cbbPacking.DataSource = dt;
        //    this.cbbPacking.DisplayMember = "TypeName";
        //    this.cbbPacking.ValueMember = "TypeValue";
        //    cbbPacking.SelectedItem = 0;
        //    //cbbPacking.Text = "请选择";           //
        //}
        /// <summary>
        /// 查询车号
        /// </summary>
        /// <param name="parking">停车位</param>
        private string GetTextOnCar(string parking)
        {
            try
            {
                txt_CENTER_X.Text = "";
                txt_CENTER_Y.Text = "";
                txt_CENTER_Z.Text = "";
                //txtLaserStatus.Text = "";
                txt_POINT_X_MAX.Text = "";
                txt_POINT_X_MIN.Text = "";
                txt_POINT_Y_MAX.Text = "";
                txt_POINT_Y_MIN.Text = "";
                txt_LENGTH.Text = "";
                txt_WIDTH.Text = "";

                string str = "";
                //txtCarNo.Text = "";
                string sql = string.Format("select CAR_NO,HEAD_POSTION ,TREATMENT_NO,STOWAGE_ID ,LASER_ACTION_COUNT from UACS_PARKING_WORK_STATUS where PARKING_NO = '{0}' ", parking);
                //string sql = string.Format("select CAR_NO,HEAD_POSTION ,TREATMENT_NO,STOWAGE_ID ,LASER_ACTION_COUNT from UACS_PARKING_STATUS where PARKING_NO = '{0}' ", parking);
                using (IDataReader rdr = DBHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        txtTeratmentNO.Text = JudgeStrNull(rdr["TREATMENT_NO"]);
                        txtLaserCount.Text = JudgeStrNull(rdr["LASER_ACTION_COUNT"]);
                        txtStowageID.Text = JudgeStrNull(rdr["STOWAGE_ID"]);
                        if (rdr["CAR_NO"] != DBNull.Value)
                        {
                            str = rdr["CAR_NO"].ToString();
                        }
                        else
                        {
                            str = "";
                        }

                        if (rdr["HEAD_POSTION"] != DBNull.Value)
                        {
                            if (rdr["HEAD_POSTION"].ToString() == "E")
                            {
                                txtCarHeadToward.Text = "东";
                            }
                            else if (rdr["HEAD_POSTION"].ToString() == "W")
                            {
                                txtCarHeadToward.Text = "西";
                            }
                            else if (rdr["HEAD_POSTION"].ToString() == "S")
                            {
                                txtCarHeadToward.Text = "南";
                            }
                            else if (rdr["HEAD_POSTION"].ToString() == "N")
                            {
                                txtCarHeadToward.Text = "北";
                            }
                            carHearDrection = rdr["HEAD_POSTION"].ToString();
                        }
                        else
                        {
                            txtCarHeadToward.Text = "";
                            carHearDrection = "";
                        }
                    }
                    txtCarNo.Text = str;
                    
                }

                #region 车实长宽 中心点XYZ 车边框XY

                //车辆激光扫描信息
                string sqlSRSCAR = "SELECT POINT_X_MAX, POINT_X_MIN, POINT_Y_MAX, POINT_Y_MIN, CENTER_X, CENTER_Y, CENTER_Z, LENGTH, WIDTH FROM UACS_PARKING_SRS_CAR_INFO WHERE 1=1  ";
                //停车位处理号
                sqlSRSCAR += " AND TREATMENT_NO = '" + txtTeratmentNO.Text + "' ";
                //扫描结果 1-成功 0-失败
                sqlSRSCAR += " AND SCAN_RESULT = '1' ";
                if (!string.IsNullOrEmpty(txtCarNo.Text.ToString().Trim()))
                {
                    //车号
                    sqlSRSCAR += " AND CAR_NO = '" + txtCarNo.Text.ToString().Trim() + "' ";
                }
                if (!string.IsNullOrEmpty(cbbPacking.Text.Trim()) && cbbPacking.Text.Trim() != "请选择" && cbbPacking.Text.Trim().Contains('A'))
                {
                    //停车位号
                    sqlSRSCAR += " AND PARKING_NO = '" + cbbPacking.Text.Trim() + "' ";
                }
                //if (!string.IsNullOrEmpty(txtLaserCount.Text.Trim()))
                //{
                //    //扫描次数
                //    sqlSRSCAR += " AND SCAN_COUNT = '" + txtLaserCount.Text.Trim() + "' ";
                //}
                sqlSRSCAR += " ORDER BY SCAN_NO, REC_TIME ";

                using (IDataReader rdr = DBHelper.ExecuteReader(sqlSRSCAR))
                {
                    while (rdr.Read())
                    {
                        var data = rdr["POINT_X_MAX"].ToString();
                        if (rdr["POINT_X_MAX"] != DBNull.Value)
                        {
                            txt_POINT_X_MAX.Text = rdr["POINT_X_MAX"].ToString();  //车边框X 最大值
                        }
                        else
                        {
                            txt_POINT_X_MAX.Text = "";
                        }
                        if (rdr["POINT_X_MIN"] != DBNull.Value)
                        {
                            txt_POINT_X_MIN.Text = rdr["POINT_X_MIN"].ToString();  //车边框X 最小值
                        }
                        else
                        {
                            txt_POINT_X_MIN.Text = "";
                        }
                        if (rdr["POINT_Y_MAX"] != DBNull.Value)
                        {
                            txt_POINT_Y_MAX.Text = rdr["POINT_Y_MAX"].ToString();  //车边框Y 最大值
                        }
                        else
                        {
                            txt_POINT_Y_MAX.Text = "";
                        }
                        if (rdr["POINT_Y_MIN"] != DBNull.Value)
                        {
                            txt_POINT_Y_MIN.Text = rdr["POINT_Y_MIN"].ToString();  //车边框Y 最小值
                        }
                        else
                        {
                            txt_POINT_Y_MIN.Text = "";
                        }
                        if (rdr["CENTER_X"] != DBNull.Value)
                        {
                            txt_CENTER_X.Text = rdr["CENTER_X"].ToString();  //中心点X
                        }
                        else
                        {
                            txt_CENTER_X.Text = "";
                        }
                        if (rdr["CENTER_Y"] != DBNull.Value)
                        {
                            txt_CENTER_Y.Text = rdr["CENTER_Y"].ToString();  //中心点Y
                        }
                        else
                        {
                            txt_CENTER_Y.Text = "";
                        }
                        if (rdr["CENTER_Z"] != DBNull.Value)
                        {
                            txt_CENTER_Z.Text = rdr["CENTER_Z"].ToString();  //中心点Z
                        }
                        else
                        {
                            txt_CENTER_Z.Text = "";
                        }
                        if (rdr["LENGTH"] != DBNull.Value)
                        {
                            txt_LENGTH.Text = rdr["LENGTH"].ToString();  //车实宽长
                        }
                        else
                        {
                            txt_LENGTH.Text = "";
                        }
                        if (rdr["WIDTH"] != DBNull.Value)
                        {
                            txt_WIDTH.Text = rdr["WIDTH"].ToString();  //车实宽
                        }
                        else
                        {
                            txt_WIDTH.Text = "";
                        }
                    }
                }

                #endregion

                return str;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0},{1}", ex.StackTrace.ToString(), ex.Message.ToString()));
                return "";
            }
        }

        /// <summary>
        /// 获得配载ID
        /// </summary>
        /// <param name="parking"></param>
        /// <returns></returns>
        private string GetStowageID(string parking, string carNo)
        {
            try
            {
                string str = "";
                string sql = string.Format(" select STOWAGE_ID from UACS_TRUCK_STOWAGE where rownum<=1 And FRAME_LOCATION='{0}' AND FRAME_NO = '{1}' order by STOWAGE_ID desc", parking, carNo);
                using (IDataReader rdr = DBHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        if (rdr["STOWAGE_ID"] != DBNull.Value)
                        {
                            str = rdr["STOWAGE_ID"].ToString();
                        }
                    }
                }
                return str;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0},{1}", ex.StackTrace.ToString(), ex.Message.ToString()));
                return "";
            }
        }
        /// <summary>
        /// 获取配载信息
        /// </summary>
        /// <param name="stowageID"></param>
        /// <returns></returns>
        private bool GetStowageDetail()
        {
            bool retu = false;
            try
            {
                //查询是车位状态
                string carNo = GetTextOnCar(cbbPacking.Text.Trim());
                if (carNo != "" && cbbPacking.Text.Contains('A') && cbbPacking.Text.Trim() != "请选择")
                {
                    string strStowageID = GetStowageID(cbbPacking.Text.Trim(), carNo); //"1451";// 
                    if (strStowageID != "")
                    {
                        string sql = @"select C.GROOVEID,C.MAT_NO as COIL_NO2,A.LOT_NO as LOT_NO,C.X_CENTER as GROOVE_ACT_X ,C.Y_CENTER AS GROOVE_ACT_Y,C.Z_CENTER AS GROOVE_ACT_Z, B.ACT_WEIGHT, B.OUTDIA ,D.STOCK_NO, D.LOCK_FLAG,B.PACK_FLAG  from UACS_TRUCK_STOWAGE_DETAIL C ";
                        sql += " LEFT JOIN  UACS_PLAN_L3PICK A ON C.MAT_NO = A.COIL_NO ";
                        sql += " LEFT JOIN  UACS_YARDMAP_COIL B ON C.MAT_NO = B.COIL_NO ";
                        sql += " LEFT JOIN UACS_YARDMAP_STOCK_DEFINE D ON C.MAT_NO = D.MAT_NO ";
                        //sql += string.Format(" where STOWAGE_ID = '{0}' order by C.GROOVEID", strStowageID);
                        sql += string.Format(" order by C.GROOVEID");
                        DataGridViewBindingSource(dataGridView2, sql);
                        //没找到数据，返回
                        if (((DataTable)dataGridView2.DataSource).Rows.Count == 0)
                        {
                            return retu;
                        }
                        retu = true;
                        return retu;
                    }
                }
                dtNull.Clear();
                dataGridView2.DataSource = dtNull;
                isStowage = true;
                return retu;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0},{1}", ex.StackTrace.ToString(), ex.Message.ToString()));
                return retu;
            }
        }

        /// <summary>
        /// 获取配载信息
        /// </summary>
        /// <param name="stowageID"></param>
        /// <returns></returns>
        private bool GetStowageDetail2()
        {
            bool retu = false;
            try
            {
                //查询是车位状态
                string carNo = GetTextOnCar(cbbPacking.Text.Trim());
                if (!string.IsNullOrEmpty(carNo) && cbbPacking.Text.Contains('A') && cbbPacking.Text.Trim() != "请选择")
                {

                    //对应指令车辆配载明细
                    string sqlText_ORDER = @"SELECT A.ORDER_NO,A.ORDER_GROUP_NO,A.EXE_SEQ,A.CMD_STATUS,A.PLAN_NO,B.MAT_CNAME,A.FROM_STOCK_NO,A.REQ_WEIGHT,A.ACT_WEIGHT FROM UACS_ORDER_QUEUE AS A ";
                    sqlText_ORDER += " LEFT JOIN UACS_L3_MAT_INFO AS B ON A.MAT_CODE = B.MAT_CODE ";
                    sqlText_ORDER += " WHERE A.CMD_STATUS = '0' AND A.CAR_NO = '{0}' AND A.TO_STOCK_NO = '{1}' ";
                    sqlText_ORDER += " ORDER BY A.ORDER_PRIORITY,A.ORDER_NO ";
                    sqlText_ORDER = string.Format(sqlText_ORDER, carNo, cbbPacking.Text.Trim());
                    DataGridViewBindingSource(dataGridView2, sqlText_ORDER);
                    //没找到数据，返回
                    if (((DataTable)dataGridView2.DataSource).Rows.Count == 0)
                    {
                        return retu;
                    }
                    retu = true;
                    return retu;

                }
                dtNull.Clear();
                dataGridView2.DataSource = dtNull;
                isStowage = true;
                return retu;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0},{1}", ex.StackTrace.ToString(), ex.Message.ToString()));
                return retu;
            }
        }

        /// <summary>
        /// 绑定材料位置信息
        /// </summary>
        private void BindMatStock(string packing, string planNo = null)
        {
            if (!packing.Contains('A') || packing.Trim() == "")
            {
                return;
            }
            dt.Clear();

            string sqlText_All = @"  SELECT 0 AS CHECK_COLUMN, C.MAT_NO AS COIL_NO, A.PICK_NO as PLAN_NO,  C.BAY_NO, C.STOCK_NO, B.WEIGHT, B.WIDTH, B.INDIA, B.OUTDIA,";
            sqlText_All += "    D.X_CENTER, D.Y_CENTER, C.Z_CENTER ,";
            sqlText_All += " B.ACT_WEIGHT, B.ACT_WIDTH FROM UACS_YARDMAP_STOCK_DEFINE C ";
            sqlText_All += " LEFT JOIN UACS_YARDMAP_COIL B ON C.MAT_NO = B.COIL_NO ";
            sqlText_All += " LEFT JOIN  UACS_PLAN_L3PICK A ON C.MAT_NO = A.COIL_NO ";
            sqlText_All += " LEFT JOIN  UACS_YARDMAP_SADDLE_STOCK E ON C.STOCK_NO = E.STOCK_NO ";
            sqlText_All += " LEFT JOIN  UACS_YARDMAP_SADDLE_DEFINE D  ON E.SADDLE_NO = D.SADDLE_NO ";

            if (planNo == null)
            {
                sqlText_All += " WHERE  C.BAY_NO  like '" + packing.Substring(0, 3) + "%' ";
                sqlText_All += " AND C.STOCK_STATUS = 2 AND C.LOCK_FLAG = 0 AND C.MAT_NO IS NOT NULL  ";
                sqlText_All += " order by C.STOCK_NO DESC ";
            }
            else if (planNo.Trim().Length > 4)
            {
                sqlText_All += " WHERE  C.BAY_NO  like '" + packing.Substring(0, 3) + "%' ";
                sqlText_All += " AND A.PICK_NO  like '" + "%" + planNo + "%' ";
                sqlText_All += " AND C.STOCK_STATUS = 2 AND C.LOCK_FLAG = 0 AND C.MAT_NO IS NOT NULL  ";
                sqlText_All += " order by C.STOCK_NO DESC ";
            }
            else
            {
                sqlText_All += " WHERE  C.BAY_NO  like '" + packing.Substring(0, 3) + "%' ";
                sqlText_All += " AND C.STOCK_STATUS = 2 AND C.LOCK_FLAG = 0 AND C.MAT_NO IS NOT NULL  ";
                sqlText_All += " order by C.STOCK_NO DESC ";
            }

            using (IDataReader rdr = DBHelper.ExecuteReader(sqlText_All))
            {
                while (rdr.Read())
                {
                    if (!hasSetColumn)
                    {
                        setDataColumn(dt, rdr);
                    }
                    hasSetColumn = true;
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < rdr.FieldCount; i++)
                    {
                        dr[i] = rdr[i];
                    }
                    dt.Rows.Add(dr);
                }
            }

            #region 转库计划
            //转库(根据库位状态和封锁标记只查出可吊的钢卷)
            //sqlText = @"SELECT 0 AS CHECK_COLUMN, A.COIL_NO,A.PLAN_NO, G.BAY_NO, C.STOCK_NO, B.WEIGHT, B.WIDTH, B.INDIA, B.OUTDIA, ";
            //sqlText += "B.PACK_FLAG, B.SLEEVE_WIDTH, B.COIL_OPEN_DIRECTION, B.NEXT_UNIT_NO, B.STEEL_GRANDID, ";
            //sqlText += "B.ACT_WEIGHT, B.ACT_WIDTH, C.X_CENTER, C.Y_CENTER, C.Z_CENTER FROM UACS_PLAN_L3TRANS A ";
            //sqlText += "LEFT JOIN UACS_YARDMAP_COIL B ON A.COIL_NO = B.COIL_NO ";
            //sqlText += "LEFT JOIN UACS_YARDMAP_STOCK_DEFINE C ON C.MAT_NO = A.COIL_NO AND C.STOCK_STATUS = 2 AND C.LOCK_FLAG = 0 ";
            //sqlText += "LEFT JOIN UACS_YARDMAP_SADDLE_STOCK D ON C.STOCK_NO = D.STOCK_NO ";
            //sqlText += "LEFT JOIN UACS_YARDMAP_SADDLE_DEFINE E ON D.SADDLE_NO = E.SADDLE_NO ";
            //sqlText += "LEFT JOIN UACS_YARDMAP_ROWCOL_DEFINE F ON E.COL_ROW_NO = F.COL_ROW_NO ";
            //sqlText += "LEFT JOIN UACS_YARDMAP_AREA_DEFINE G ON F.AREA_NO = G.AREA_NO ";
            //sqlText += "WHERE A.PLAN_NO = '{0}' ";
            //sqlText = string.Format(sqlText, pickNo);
            //using (IDataReader rdr = DBHelper.ExecuteReader(sqlText))
            //{
            //    while (rdr.Read())
            //    {
            //        if (!hasSetColumn)
            //        {
            //            setDataColumn(dt, rdr);
            //        }
            //        hasSetColumn = true;
            //        DataRow dr = dt.NewRow();
            //        for (int i = 0; i < rdr.FieldCount; i++)
            //        {
            //            dr[i] = rdr[i];
            //        }
            //        dt.Rows.Add(dr);
            //    }
            // } 
            #endregion
        }
        /// <summary>
        /// 设置table的列
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="rdr"></param>
        private void setDataColumn(DataTable dt, IDataReader rdr)
        {
            for (int i = 0; i < rdr.FieldCount; i++)
            {
                DataColumn dc = new DataColumn();
                dc.ColumnName = rdr.GetName(i);
                dt.Columns.Add(dc);
            }
            //

        }

        /// <summary>
        /// 激光扫描数据
        /// </summary>
        /// <returns></returns>
        private bool RefreshHMILaserOutData()
        {
            bool bResut = false;
            try
            {
                string parkingNo = "";
                string TREATMENT_NO = "";
                long LASER_ACTION_COUNT = 0;

                // 读取车牌数据
                string truckNo = txtCarNo.Text.Trim();      //车号
                if (string.IsNullOrEmpty(truckNo))
                {
                    return bResut;
                }

                // 车号对应的停车位数据
                string sqlText = @"SELECT PARKING_NO FROM UACS_PARKING_WORK_STATUS WHERE CAR_NO = '{0}'";
                sqlText = string.Format(sqlText, truckNo);
                using (IDataReader rdr = DBHelper.ExecuteReader(sqlText))
                {
                    if (rdr.Read())
                    {
                        parkingNo = rdr["PARKING_NO"].ToString();
                    }
                }


                #region 暂时弃用

                ////先获取车头方向配置表里的车长方向坐标轴和趋势
                //string AXES_CAR_LENGTH = "";
                //string TREND_TO_TAIL = "";
                //string sqlText_head = @"SELECT AXES_CAR_LENGTH, TREND_TO_TAIL FROM UACS_HEAD_POSITION_CONFIG WHERE HEAD_POSTION IN ";
                //sqlText_head += "(SELECT HEAD_POSTION FROM UACS_PARKING_WORK_STATUS WHERE PARKING_NO = '{0}') AND PARKING_NO = '{0}'";
                //sqlText_head = string.Format(sqlText_head, parkingNo);
                //using (IDataReader rdr = DBHelper.ExecuteReader(sqlText_head))
                //{
                //    if (rdr.Read())
                //    {
                //        AXES_CAR_LENGTH = rdr["AXES_CAR_LENGTH"].ToString();
                //        TREND_TO_TAIL = rdr["TREND_TO_TAIL"].ToString();
                //    }
                //}

                //string sqlorder = "";
                //if (AXES_CAR_LENGTH == "X" && TREND_TO_TAIL == "INC")
                //{
                //    sqlorder = "ORDER BY GROOVE_ACT_X ";
                //}
                //else if (AXES_CAR_LENGTH == "X" && TREND_TO_TAIL == "DES")
                //{
                //    sqlorder = "ORDER BY GROOVE_ACT_X DESC";
                //}
                //else if (AXES_CAR_LENGTH == "Y" && TREND_TO_TAIL == "INC")
                //{
                //    sqlorder = "ORDER BY GROOVE_ACT_Y ";
                //}
                //else if (AXES_CAR_LENGTH == "Y" && TREND_TO_TAIL == "DES")
                //{
                //    sqlorder = "ORDER BY GROOVE_ACT_Y DESC";

                //}

                ////从停车位表里取出处理号和激光扫描次数
                //sqlText = @"SELECT TREATMENT_NO, LASER_ACTION_COUNT FROM UACS_PARKING_WORK_STATUS WHERE PARKING_NO='{0}' ";
                //sqlText = string.Format(sqlText, parkingNo);
                //using (IDataReader rdr = DBHelper.ExecuteReader(sqlText))
                //{
                //    while (rdr.Read())
                //    {
                //        TREATMENT_NO = rdr["TREATMENT_NO"].ToString();
                //        LASER_ACTION_COUNT = Convert.ToInt64(rdr["LASER_ACTION_COUNT"].ToString());
                //    }
                //}

                ////string GROOVE_ACT_X = "";
                ////string GROOVE_ACT_Y = "";
                ////string GROOVE_ACT_Z = "";
                ////string GROOVEID = "";
                //dt_selected.Clear();

                ////从出库激光表里取出激光扫描数据
                //sqlText = @"SELECT GROOVE_ACT_X, GROOVE_ACT_Y, GROOVE_ACT_Z, GROOVEID FROM UACS_LASER_OUT ";
                //sqlText += "WHERE TREATMENT_NO = '{0}' AND LASER_ACTION_COUNT = '{1}' ";
                //sqlText += sqlorder;
                //sqlText = string.Format(sqlText, TREATMENT_NO, LASER_ACTION_COUNT);

                //using (IDataReader rdr = DBHelper.ExecuteReader(sqlText))
                //{
                //    //while (rdr.Read())
                //    //{
                //    //    GROOVE_ACT_X = rdr["GROOVE_ACT_X"].ToString();
                //    //    GROOVE_ACT_Y = rdr["GROOVE_ACT_Y"].ToString();
                //    //    GROOVE_ACT_Z = rdr["GROOVE_ACT_Z"].ToString();
                //    //    GROOVEID = rdr["GROOVEID"].ToString();
                //    //    dt_selected.Rows.Add(GROOVEID, "", "", "", "", GROOVE_ACT_X, GROOVE_ACT_Y, GROOVE_ACT_Z);
                //    //}
                //    dt_selected.Load(rdr);
                //} 

                #endregion

                //对应指令车辆配载明细
                string sqlText_ORDER = @"SELECT A.ORDER_NO,A.ORDER_GROUP_NO,A.EXE_SEQ,A.CMD_STATUS,A.PLAN_NO,B.MAT_CNAME,A.FROM_STOCK_NO,A.REQ_WEIGHT,A.ACT_WEIGHT FROM UACS_ORDER_QUEUE AS A ";
                sqlText_ORDER += " LEFT JOIN UACS_L3_MAT_INFO AS B ON A.MAT_CODE = B.MAT_CODE ";
                sqlText_ORDER += " WHERE A.CMD_STATUS = '0' AND A.CAR_NO = '{0}' AND A.TO_STOCK_NO = '{1}' ";

                sqlText_ORDER = string.Format(sqlText_ORDER, truckNo, parkingNo);
                using (IDataReader rdr = DBHelper.ExecuteReader(sqlText_ORDER))
                {
                    dt_selected.Load(rdr);
                }

                this.dataGridView2.DataSource = dt_selected;

                bResut = true;
            }
            catch (Exception er)
            {
                MessageBox.Show(string.Format("{0} {1}", er.TargetSite, er.ToString()));
            }

            return bResut;
        }

        /// <summary>
        /// 与配载信息匹配
        /// </summary>
        /// <param name="treatmentNo"></param>
        /// <param name="LASER_ACTION_COUNT"></param>
        /// <returns></returns>
        private bool CheckWithLaserOutData(string treatmentNo, long LASER_ACTION_COUNT)
        {
            bool bResult = false;
            string sqlText;

            try
            {
                // 获取最新激光扫描数据（从出库激光表里取出激光扫描数据）
                Dictionary<string, LASER_OUT_DATA> dictGrooveIDLaserOut = new Dictionary<string, LASER_OUT_DATA>();
                sqlText = @"SELECT GROOVE_ACT_X, GROOVE_ACT_Y, GROOVE_ACT_Z, GROOVEID FROM UACS_LASER_OUT ";
                sqlText += "WHERE TREATMENT_NO = '{0}' AND LASER_ACTION_COUNT = '{1}' ";
                sqlText = string.Format(sqlText, treatmentNo, LASER_ACTION_COUNT);
                using (IDataReader rdr = DBHelper.ExecuteReader(sqlText))
                {
                    while (rdr.Read())
                    {
                        LASER_OUT_DATA laseroutData;
                        laseroutData.GROOVE_ACT_X = rdr["GROOVE_ACT_X"].ToString();
                        laseroutData.GROOVE_ACT_Y = rdr["GROOVE_ACT_Y"].ToString();
                        laseroutData.GROOVE_ACT_Z = rdr["GROOVE_ACT_Z"].ToString();
                        laseroutData.GROOVEID = rdr["GROOVEID"].ToString();

                        dictGrooveIDLaserOut[laseroutData.GROOVEID] = laseroutData;
                    }
                }

                // 与画面选定的配载信息比对
                int nCountCoil = 0;
                int nCountChecked = 0;
                for (int i = 0; i < dt_selected.Rows.Count; i++)
                {
                    if (i < 30)
                    {
                        string coilNO = dt_selected.Rows[i]["COIL_NO2"].ToString().Trim();
                        if (coilNO.Length != 0)
                        {
                            nCountCoil++;

                            // 配过卷的
                            string GROOVEID = dt_selected.Rows[i]["GROOVEID"].ToString();
                            string GROOVE_X = dt_selected.Rows[i]["GROOVE_ACT_X"].ToString();
                            string GROOVE_Y = dt_selected.Rows[i]["GROOVE_ACT_Y"].ToString();
                            string GROOVE_Z = dt_selected.Rows[i]["GROOVE_ACT_Z"].ToString();

                            if (dictGrooveIDLaserOut.ContainsKey(GROOVEID))
                            {
                                LASER_OUT_DATA laserout = dictGrooveIDLaserOut[GROOVEID];

                                // 画面数据与选择数据匹配
                                if (laserout.GROOVE_ACT_X == GROOVE_X &&
                                    laserout.GROOVE_ACT_Y == GROOVE_Y &&
                                    laserout.GROOVE_ACT_Z == GROOVE_Z)
                                {
                                    nCountChecked++;
                                }
                            }
                        }
                    }
                }
                // 数据与后台均匹配
                if (nCountChecked == nCountCoil && nCountCoil != 0)
                    bResult = true;
            }
            catch (Exception er)
            {
                MessageBox.Show(string.Format("{0} {1}", er.TargetSite, er.ToString()));
            }

            return bResult;
        }
        #endregion



        #region -----------------------------控件事件-------------------------------------




        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            isStowage = false;
            hasCar = true;
            hasParkSize = false;
            RefreshHMI();
            //Stowage_intercept(cmbArea.Text.Trim(), cbbPacking.Text.Trim(), txtCarNo.Text.Trim(), dataGridView2);
            ParkClassLibrary.HMILogger.WriteLog(btnRefresh.Text, "刷新", ParkClassLibrary.LogLevel.Info, this.Text);
        }
        /// <summary>
        /// 刷新画面
        /// </summary>
        private void RefreshHMI()
        {
            btnOperateStrat.Enabled = true;
            //panel_Tip.Visible = false;
            bool stopRefresh = false;
            if (isStowage)
            {
                return;
            }
            if (cbbPacking.Text.Trim().Contains('A'))
            {
                //刷新车位信息
                string temp = GetTextOnCar(cbbPacking.Text.Trim());
                if (temp != "")
                {
                    GetParkStatus(cbbPacking.Text, out curCarType);
                }
                //框架车获取车身信息
                //if (curCarType == 100 || curCarType == 102) //车辆类型（100：框架 101：社会车辆 102：大头娃娃车 103：17m社会车辆）
                //{
                    //getCarBorderSize(txtLaserCount.Text, txtTeratmentNO.Text);
                    GetCurrentCarInfo(cbbPacking.Text);
                //}
                //查询车位状态,是否是暂停状态
                GetOperateStatus(cbbPacking.Text.Trim());
                //string strPacking = cbbPacking.Text.Trim().Substring(0, 3);
                //StringBuilder sbb = new StringBuilder(strPacking);
                //sbb.Append("-1");
                //BindMatStock(sbb.ToString());

                //刷新通道全部车位
                // GetParkInfo();
                GetParkInfoByBay();
                if (!hasCar)
                {
                    stopRefresh = txtTeratmentNO.Text.Contains('A');
                    hasCar = stopRefresh;
                    return;
                }
                //刷新指令表
                RefreshOrderDgv(cbbPacking.Text.Trim());
                //刷新激光图像配载数据
                //LoadLaserInfo(cbbPacking.Text.Trim(), parkLaserOut1);
                //刷新激光数据
                Inq_Laser(txtTeratmentNO.Text, txtLaserCount.Text);

                //获得配载数据
                GetStowageDetail2();

                //计算重量
                CalculteWeight();
                //刷新车上卷状态
                reflreshParkingCoilstate(cbbPacking.Text.Trim());

                stopRefresh = txtTeratmentNO.Text.Contains('A');
                hasCar = stopRefresh;
                //txtDebug.Text = hasCar.ToString();//没车只刷一次
                setCurParkStatus(cbbPacking.Text.Trim());

                displayStowageTime(labTime, txtStowageID.Text.Trim()); // //显示配载生成后的时间差

                Stowage_intercept(cmbArea.Text.Trim(), cbbPacking.Text.Trim(), txtCarNo.Text.Trim(), dataGridView2);
            }
            else
            {
                txtCarNo.Text = "";
                txtCarHeadToward.Text = "";
                txtLaserCount.Text = "";
                txtStowageID.Text = "";
                txtTeratmentNO.Text = "";
                txtLaserStatus.Text = "";
                txt_CENTER_X.Text = "";
                txt_CENTER_Y.Text = "";
                txt_CENTER_Z.Text = "";
                txt_POINT_X_MAX.Text = "";
                txt_POINT_X_MIN.Text = "";
                txt_POINT_Y_MAX.Text = "";
                txt_POINT_Y_MIN.Text = "";
                txt_LENGTH.Text = "";
                txt_WIDTH.Text = "";
            }
        }
        private void CalculteWeight()
        {
            txtCoilsWeight.Text = "";
            if (txtCoilsWeight.Text == "" && dataGridView2.Rows.Count != 0)
            {
                int n1 = 0;
                foreach (DataGridViewRow item in dataGridView2.Rows)
                {
                    //if (item.Cells["WEIGHT2"].Value != null)
                    //{
                    //    n1 += JudgeIntNull(item.Cells["WEIGHT2"].Value);
                    //}
                    if (item.Cells["REQ_WEIGHT"].Value != null)
                    {
                        n1 += JudgeIntNull(item.Cells["REQ_WEIGHT"].Value);
                    }
                }
                txtCoilsWeight.Text = string.Format("{0} /公斤", n1.ToString());
                if (n1 > 500000)
                    txtCoilsWeight.BackColor = Color.Red;
                else
                    txtCoilsWeight.BackColor = Color.White;
            }

        }

        /// <summary>
        /// 定位到指定的行
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="searchString"></param>
        /// <param name="columnName"></param>
        private void SelectDataGridViewRow(DataGridView dgv, string searchString, string columnName)
        {
            try
            {
                foreach (DataGridViewRow dgvRow in dgv.Rows)
                {
                    if (dgvRow.Cells[columnName].Value != null)
                    {
                        if (dgvRow.Cells[columnName].Value.ToString() == searchString)
                        {
                            dgv.FirstDisplayedScrollingRowIndex = dgvRow.Index;
                            dgvRow.Cells[columnName].Selected = true;
                            dgv.CurrentCell = dgvRow.Cells[columnName];
                            return;
                        }
                    }
                }
                MessageBox.Show(string.Format("没有找到指定的钢卷：{0}", searchString));
            }

            catch (Exception er)
            {
                MessageBox.Show(string.Format("{0} {1}", er.TargetSite, er.ToString()));
            }
        }





        /// <summary>
        /// 停车位变化刷新激光数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCarNo_TextChanged(object sender, EventArgs e)
        {
            //改在车位刷新
            if (txtCarNo.Text.Trim().Length > 2)
            {
                //判断是否已经配载
                if (cbbPacking.Text.Trim().Contains('A'))
                {
                    if (!GetStowageDetail2())
                    {
                        RefreshHMILaserOutData();
                    }
                }
            }

        }

        #region 车到达
        /// <summary>
        /// 车到达
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCarEnter_Click(object sender, EventArgs e)
        {

            //if (!cbbPacking.Text.Trim().Contains('Z'))
            //{
            //    MessageBox.Show("请选择停车位！", "提示");
            //    return;
            //}
            if (!cbbPacking.Text.Trim().Contains('A'))
            {
                MessageBox.Show("请选择停车位！", "提示");
                return;
            }
            else if (txtTeratmentNO.Text.Trim() != "")
            {
                MessageBox.Show("车位已经有车！", "警告");
            }
            //if (GetTextOnCar(cbbPacking.Text.Trim()) == "")
            else
            {
                FrmCarEntry frm = new FrmCarEntry();
                frm.PackingNo = cbbPacking.Text.Trim();
                frm.CarNo = txtCarNo.Text.Trim();
                //frm.CarType = "社会车"; 
                frm.CarType = "ALL";
                frm.ShowDialog();
                curCarType = frm.CarTypeValue1550 != 0 ? frm.CarTypeValue1550 : curCarType;
                if (curCarType == 100 || curCarType == 101 || curCarType == 102 || curCarType == 103 || curCarType == 200 || curCarType == 106)
                {
                    btnSeleceByMat.Enabled = true;
                }
                else
                {
                    btnSeleceByMat.Enabled = false;
                }
            }
        }
        #endregion

        #region 车离

        /// <summary>
        /// 车离开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCarFrom_Click(object sender, EventArgs e)
        {
            if (!cbbPacking.Text.Contains('A'))
            {
                MessageBox.Show("请选择停车位。");
                return;
            }
            if (GetParkStatus(cbbPacking.Text.Trim()) == "5")
            {
                MessageBox.Show("车位无车，不需要车离。");
                btnRefresh_Click(null, null);
                return;
            }
            if (!JudgeCraneOrder(cbbPacking.Text))
            {
                MessageBox.Show("行车已生成指令，不能车离！");
                return;
            }
            int coilsNotComplete = 0;
            if (!string.IsNullOrEmpty(txtStowageID.Text.ToString()))
            {
                coilsNotComplete = checkParkingIsWorking(int.Parse(txtStowageID.Text));
            }

            if (txtStowageID.Text != "" && coilsNotComplete > 0)
            {
                MessageBox.Show("车位还有（" + coilsNotComplete + " )个物料没有完成!", "提示");
            }
            if (cbbPacking.Text.Contains('A'))
            {
                MessageBoxButtons btn = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("确定要对" + cbbPacking.Text.Trim() + "进行车离位吗？", "操作提示", btn, MessageBoxIcon.Asterisk);
                if (dr == DialogResult.OK)
                {
                    TagDP.SetData("EV_PARKING_CARLEAVE", cbbPacking.Text.Trim());
                    //画面清空
                    dt_selected.Clear();
                    dataGridView2.DataSource = dt_selected;
                    DataTable dtOrder = new DataTable();
                    dtOrder.Clear();
                    dgvOrder.DataSource = dtOrder;
                    //重量清空
                    coilsWeight = 0;
                    txtCoilsWeight.Text = string.Format("{0}/吨", coilsWeight);
                    txtCoilsWeight.BackColor = Color.White;
                    //txtSelectGoove.Text = "";
                    //panel_Tip.Visible = false;
                    btnOperateStrat.Enabled = true;
                    ParkClassLibrary.HMILogger.WriteLog(btnCarFrom.Text, "车离：" + cbbPacking.Text, ParkClassLibrary.LogLevel.Info, this.Text);
                }
                isStowage = false;
                hasParkSize = false;
            }

        }
        private string GetParkStatus(string parkingNO)
        {
            string ret = "";
            if (!parkingNO.Contains('A'))
            {
                return ret;
            }
            try
            {
                string sqlText = @"SELECT WORK_STATUS FROM UACS_PARKING_WORK_STATUS WHERE PARKING_NO = '" + parkingNO + "'";
                using (IDataReader rdr = DBHelper.ExecuteReader(sqlText))
                {
                    while (rdr.Read())
                    {
                        ret = ParkClassLibrary.ManagerHelper.JudgeStrNull(rdr["WORK_STATUS"]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0},{1}", ex.StackTrace.ToString(), ex.Message.ToString()));
            }
            return ret;
        }
        #endregion
        #endregion



        #region 车位状态显示

        /// <summary>
        /// 读取停车位状态
        /// </summary>      
        private bool GetParkInfoByBay()
        {
            try
            {
                //string sql = @"select A.PARKING_NO,A.ISLOADED,A.PARKING_STATUS,A.CAR_NO , B.CAR_TYPE, B.ISWOODENCAR from UACS_PARKING_STATUS A
                //                LEFT JOIN UACS_TRUCK_STOWAGE B ON A.STOWAGE_ID = B.STOWAGE_ID ";
                string sql = @"select A.PARKING_NO,A.ISLOADED,A.WORK_STATUS,A.CAR_NO , B.CAR_TYPE from UACS_PARKING_WORK_STATUS A
                                LEFT JOIN UACS_TRUCK_STOWAGE B ON A.STOWAGE_ID = B.STOWAGE_ID ";
                bool ret = true;
                using (IDataReader rdr = DBHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {

                        //if (cmbArea.Text.Contains("2030"))
                        //{
                        if (rdr["PARKING_NO"].ToString().Trim() == "A1")
                        {
                            //parkZ21B6.SetPark(JudgeStrNull(rdr["PARKING_NO"]), JudgeStrNull(rdr["ISLOADED"]), JudgeStrNull(rdr["PARKING_STATUS"]), JudgeStrNull(rdr["CAR_NO"]), JudgeStrNull(rdr["CAR_TYPE"]), JudgeStrNull(rdr["ISWOODENCAR"]));
                            parkZ21B6.SetPark(JudgeStrNull(rdr["PARKING_NO"]), JudgeStrNull(rdr["ISLOADED"]), JudgeStrNull(rdr["WORK_STATUS"]), JudgeStrNull(rdr["CAR_NO"]), JudgeStrNull(rdr["CAR_TYPE"]), "");
                        }
                        else if (rdr["PARKING_NO"].ToString().Trim() == "A2")
                        {
                            //parkZ21B5.SetPark(JudgeStrNull(rdr["PARKING_NO"]), JudgeStrNull(rdr["ISLOADED"]), JudgeStrNull(rdr["PARKING_STATUS"]), JudgeStrNull(rdr["CAR_NO"]), JudgeStrNull(rdr["CAR_TYPE"]), JudgeStrNull(rdr["ISWOODENCAR"]));
                            parkZ21B5.SetPark(JudgeStrNull(rdr["PARKING_NO"]), JudgeStrNull(rdr["ISLOADED"]), JudgeStrNull(rdr["WORK_STATUS"]), JudgeStrNull(rdr["CAR_NO"]), JudgeStrNull(rdr["CAR_TYPE"]), "");
                        }
                        else if (rdr["PARKING_NO"].ToString().Trim() == "A3")
                        {
                            parkZ21B4.SetPark(JudgeStrNull(rdr["PARKING_NO"]), JudgeStrNull(rdr["ISLOADED"]), JudgeStrNull(rdr["WORK_STATUS"]), JudgeStrNull(rdr["CAR_NO"]), JudgeStrNull(rdr["CAR_TYPE"]), "");
                        }
                        else if (rdr["PARKING_NO"].ToString().Trim() == "A4")
                        {
                            parkZ21B3.SetPark(JudgeStrNull(rdr["PARKING_NO"]), JudgeStrNull(rdr["ISLOADED"]), JudgeStrNull(rdr["WORK_STATUS"]), JudgeStrNull(rdr["CAR_NO"]), JudgeStrNull(rdr["CAR_TYPE"]), "");
                        }
                        //}
                    }
                }

                //string treatmentNO = txtTeratmentNO.Text;
                //string laserCount = txtLaserCount.Text;
                //string sqlTextTotal = @"SELECT COUNT(distinct(LASER_ID)) AS IDTOTAL  FROM UACS_LASER_OUT WHERE 1=1 ";
                //sqlTextTotal += " AND LASER_ACTION_COUNT = '" + laserCount + "' AND TREATMENT_NO = '" + treatmentNO + "' FETCH FIRST 1 ROWS ONLY ";

                //using (IDataReader rdr = DBHelper.ExecuteReader(sqlTextTotal))
                //{
                //    while (rdr.Read())
                //    {
                //        if (rdr["IDTOTAL"] != System.DBNull.Value)
                //        {
                //            //txtSelectGoove.Text = Convert.ToString(rdr["IDTOTAL"]);
                //            if (Convert.ToInt16(rdr["IDTOTAL"]) == 0 && txtCarNo.Text.Equals(""))
                //            {
                //                txtSelectGoove.Text = "";
                //                txtSelectGoove.BackColor = SystemColors.Control;
                //            }
                //            else if (curCarType == 100)
                //            {
                //                txtSelectGoove.Text = Convert.ToString(rdr["IDTOTAL"]);
                //                txtSelectGoove.BackColor = txtSelectGoove.Text == txtGrooveNum.Text ? SystemColors.Control : txtSelectGoove.BackColor = Color.Red; ;
                //            }
                //            else
                //            {
                //                txtSelectGoove.Text = Convert.ToString(rdr["IDTOTAL"]);
                //                txtSelectGoove.BackColor = SystemColors.Control;
                //            }
                //        }
                //    }
                //}
                return ret;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0},{1}", ex.StackTrace.ToString(), ex.Message.ToString()));
                return false;
            }
        }
        #endregion

        #region 方法
        private string JudgeStrNull(object item)
        {
            string str = "";
            if (item == DBNull.Value)
            {
                return str;
            }
            else
            {
                str = item.ToString();
            }
            return str;
        }
        private int JudgeIntNull(object item)
        {
            int ret = 0;
            if (item == DBNull.Value)
            {
                return ret;
            }
            else
            {
                ret = Convert.ToInt32(item);
            }
            return ret;
        }

        public static bool DataGridViewBindingSource(DataGridView dataGridView, string sql)
        {
            DataTable dt = new DataTable();
            using (IDataReader odrIn = DBHelper.ExecuteReader(sql))
            {
                try
                {
                    dt.Load(odrIn);
                    dataGridView.DataSource = dt;
                    foreach (DataGridViewColumn item in dataGridView.Columns)
                    {
                        if (item.Name == "Index")
                        {
                            for (int y = 0; y < dataGridView.Rows.Count - 1; y++)
                            {
                                dataGridView.Rows[y].Cells["Index"].Value = y;
                            }
                            break;
                        }
                    }

                }
                catch (Exception meg)
                {
                    MessageBox.Show(string.Format("调用函数DataGridViewBindingSource出错：{0}", meg));
                }
                odrIn.Close();
                return true;
            }
        }
        private bool CreatDgvHeader(DataGridView dataGridView, string[] columnsName, string[] headerText)
        {
            bool isFirst = false;
            if (!isFirst)
            {
                //dataGridView.Columns.Add("Index", "序号");
                //DataGridViewColumn columnIndex = new DataGridViewTextBoxColumn();
                //columnIndex.Width = 50;
                //columnIndex.DataPropertyName = "Index";
                //columnIndex.Name = "Index";
                //columnIndex.HeaderText = "序号";
                //dataGridView.Columns.Add(columnIndex);
                for (int i = 0; i < headerText.Count(); i++)
                {
                    DataGridViewColumn column = new DataGridViewTextBoxColumn();
                    column.DataPropertyName = columnsName[i];
                    column.Name = columnsName[i];
                    column.HeaderText = headerText[i];
                    if (i > 0)
                    {
                        column.Width = 150;
                    }

                    int index = dataGridView.Columns.Add(column);
                    dataGridView.Columns[index].SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                isFirst = true;
                return isFirst;
            }
            else
                return isFirst;
        }
        void dataGridView2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if ((e.RowIndex >= 0) && (e.ColumnIndex >= 0)
                                && dataGridView2.Rows[e.RowIndex].Cells["COIL_NO2"].Value != null
                                 && dataGridView2.Rows[e.RowIndex].Cells["COIL_NO2"].Value.ToString() != "")
                {
                    if (dataGridView2.Columns[e.ColumnIndex].Name.Equals("LOCK_FLAG")
                        || dataGridView2.Columns[e.ColumnIndex].Name.Equals("STOCK_NO"))
                    {
                        if (e.Value == null || e.Value.ToString() == "")
                        {
                            if (dataGridView2.Rows[e.RowIndex].Cells["COIL_NO2"].Value.ToString().Contains("待装钢卷"))
                            {
                                e.Value = "";
                                return;
                            }
                            else
                            {
                                e.Value = "";
                                e.CellStyle.BackColor = Color.Red;
                                return;
                            }
                        }
                        if (e.Value.Equals(0))
                            e.Value = "可用";
                        else if (e.Value.Equals(1))
                        {
                            e.Value = "待判";
                            e.CellStyle.BackColor = Color.Yellow;
                        }
                        else if (e.Value.Equals(2))
                        {
                            e.Value = "封锁";
                            e.CellStyle.BackColor = Color.Red;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0},{1}", ex.StackTrace.ToString(), ex.Message.ToString()));
            }
        }

        #endregion

        #region 指令信息显示

        /// <summary>
        /// 刷新指令表
        /// </summary>
        private void RefreshOrderDgv(string parkNO)
        {
            DataTable dtOrder = new DataTable();
            dtOrder.Clear();
            try
            {
                //查询是车位状态
                string carNo = GetTextOnCar(parkNO);
                if (!string.IsNullOrEmpty(parkNO) && !string.IsNullOrEmpty(carNo))
                {
                    //对应指令车辆配载明细
                    string SQLOder = @"SELECT A.ORDER_NO,B.MAT_CNAME,A.FROM_STOCK_NO,A.TO_STOCK_NO,A.BAY_NO FROM UACS_ORDER_QUEUE AS A ";
                    SQLOder += " LEFT JOIN UACS_L3_MAT_INFO AS B ON A.MAT_CODE = B.MAT_CODE ";
                    SQLOder += " WHERE A.CMD_STATUS = '0' AND A.CAR_NO = '{0}' AND A.TO_STOCK_NO = '{1}' ";
                    SQLOder += " ORDER BY A.ORDER_PRIORITY,A.ORDER_NO ";
                    SQLOder = string.Format(SQLOder, carNo, parkNO);
                    using (IDataReader odrIn = DBHelper.ExecuteReader(SQLOder))
                    {
                        dtOrder.Load(odrIn);
                    }
                    dgvOrder.DataSource = dtOrder;
                }
                else
                {
                    dtNull.Clear();
                    dgvOrder.DataSource = dtNull;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0},{1}", ex.StackTrace.ToString(), ex.Message.ToString()));
            }

        }
        #endregion

        private void CreatCarSize(string strParkNo, ParkLaserOut park)
        {
            DataTable dtLaserData = new DataTable();
            dtLaserData.Clear();
            try
            {
                //string sqlLaser = @"SELECT CAR_X_BORDER_MAX,CAR_X_BORDER_MIN,CAR_Y_BORDER_MAX,CAR_Y_BORDER_MIN,GROOVE_ACT_X,GROOVE_ACT_Y,GROOVE_ACT_Z,GROOVEID FROM UACS_LASER_OUT  ";
                //sqlLaser = string.Format("{0}  WHERE TREATMENT_NO  IN (SELECT TREATMENT_NO FROM UACS_PARKING_STATUS WHERE PARKING_NO = '{1}')", sqlLaser, strParkNo); //strParkNo:Z53A1
                string sqlLaser = @"SELECT CAR_X_BORDER_MAX,CAR_X_BORDER_MIN,CAR_Y_BORDER_MAX,CAR_Y_BORDER_MIN,GROOVE_ACT_X,GROOVE_ACT_Y,GROOVE_ACT_Z,GROOVEID FROM UACS_LASER_OUT  ";
                sqlLaser = string.Format("{0}  WHERE TREATMENT_NO  IN (SELECT TREATMENT_NO FROM UACS_PARKING_WORK_STATUS WHERE PARKING_NO = '{1}')", sqlLaser, strParkNo); //strParkNo:Z53A1
                using (IDataReader rdr = DBHelper.ExecuteReader(sqlLaser))
                {
                    dtLaserData.Load(rdr);
                }
                //生成车位置
                ////获取车位坐标XMin, XMax, YMin, YMax
                int XMin, XMax, YMin, YMax;
                int XVehicleLength, YVehicleLength;
                XMin = XMax = YMin = YMax = XVehicleLength = YVehicleLength = 0;
                //SELECT CAR_X_BORDER_MAX,CAR_X_BORDER_MIN,CAR_Y_BORDER_MAX,CAR_Y_BORDER_MIN,GROOVE_ACT_X,GROOVE_ACT_Y,GROOVE_ACT_Z,GROOVEID FROM UACS_LASER_OUT 
                if (dtLaserData.Rows.Count != 0)
                {
                    XMin = Convert.ToInt32(dtLaserData.Rows[0]["CAR_X_BORDER_MIN"]);
                    XMax = Convert.ToInt32(dtLaserData.Rows[0]["CAR_X_BORDER_MAX"]);
                    YMin = Convert.ToInt32(dtLaserData.Rows[0]["CAR_Y_BORDER_MIN"]);
                    YMax = Convert.ToInt32(dtLaserData.Rows[0]["CAR_Y_BORDER_MAX"]);
                    XVehicleLength = XMax - XMin;
                    YVehicleLength = YMax - YMin;
                    //画面显示
                    park.CreateCarSize(XMin, YMax, XVehicleLength, YVehicleLength, 0, "E");
                }
                //生成钢卷位置
                foreach (DataRow item in dtLaserData.Rows)
                {
                    int n1 = JudgeIntNull(item["GROOVE_ACT_X"]);
                    int n2 = JudgeIntNull(item["GROOVE_ACT_Y"]);
                    string strID = JudgeStrNull(item["GROOVEID"]);
                    if (n1 != 0 && n2 != 0)
                    {
                        park.CreateCoilSize(n1, n2, 1200, 1200, strID, false);
                    }
                }
            }
            catch (Exception er)
            {

                MessageBox.Show(string.Format("{0} {1}", er.TargetSite, er.ToString()));
            }

        }
        #region 配载图显示
        /// <summary>
        /// 车位绑定激光数据
        /// </summary>
        /// <param name="strParkNo"></param>
        private void LoadLaserInfo(string strParkNo, ParkLaserOut park)
        {
            if (!strParkNo.Contains('A'))
            {
                return;
            }
            DataTable dtLaserData = new DataTable();
            DataTable dtParksize = new DataTable();
            DataTable dtStowageData = new DataTable();
            dtParksize.Clear();
            dtLaserData.Clear();
            dtStowageData.Clear();
            park.ClearbitM();
            try
            {
                //string sqlLaser = @"SELECT CAR_X_BORDER_MAX,CAR_X_BORDER_MIN,CAR_Y_BORDER_MAX,CAR_Y_BORDER_MIN,GROOVE_ACT_X,GROOVE_ACT_Y,GROOVE_ACT_Z,GROOVEID,TIME_CREATED FROM UACS_LASER_OUT  ";
                //sqlLaser = string.Format("{0}  WHERE TREATMENT_NO  IN (SELECT TREATMENT_NO FROM UACS_PARKING_STATUS WHERE PARKING_NO = '{1}')", sqlLaser, strParkNo); //strParkNo:Z53A1
                string sqlLaser = @"SELECT CAR_X_BORDER_MAX,CAR_X_BORDER_MIN,CAR_Y_BORDER_MAX,CAR_Y_BORDER_MIN,GROOVE_ACT_X,GROOVE_ACT_Y,GROOVE_ACT_Z,GROOVEID,TIME_CREATED FROM UACS_LASER_OUT  ";
                sqlLaser = string.Format("{0}  WHERE TREATMENT_NO  IN (SELECT TREATMENT_NO FROM UACS_PARKING_WORK_STATUS WHERE PARKING_NO = '{1}')", sqlLaser, strParkNo); //strParkNo:Z53A1
                using (IDataReader rdr = DBHelper.ExecuteReader(sqlLaser))
                {
                    dtLaserData.Load(rdr);
                }
                //string sqlPark = string.Format("SELECT * FROM UACS_YARDMAP_PARKINGSITE WHERE NAME ='{0}' AND YARD_NO = '{1}'", strParkNo,cmbArea.Text.Substring(0,1));
                //string sqlPark = string.Format("SELECT * FROM UACS_YARDMAP_PARKINGSITE WHERE NAME ='{0}' ", strParkNo);
                string sqlPark = string.Format("SELECT * FROM UACS_PARKING_INFO_DEFINE WHERE PAKRING_NAME ='{0}' ", strParkNo);
                using (IDataReader rdr = DBHelper.ExecuteReader(sqlPark))
                {
                    dtParksize.Load(rdr);
                }
                string sqlStowage = @" SELECT C.MAT_NO,C.X_CENTER,C.Y_CENTER ,C.GROOVEID, B.OUTDIA ,B.WIDTH ,A.STOCK_NO,A.LOCK_FLAG  FROM UACS_TRUCK_STOWAGE_DETAIL C ";
                sqlStowage += " LEFT JOIN UACS_YARDMAP_COIL B ON C.MAT_NO = B.COIL_NO ";
                sqlStowage += " LEFT JOIN UACS_YARDMAP_STOCK_DEFINE A ON C.MAT_NO = A.MAT_NO ";
                sqlStowage += " WHERE C.STOWAGE_ID IN (SELECT STOWAGE_ID FROM UACS_PARKING_WORK_STATUS  ";
                sqlStowage += " WHERE PARKING_NO ='{0}')  ORDER BY GROOVEID ASC ";
                sqlStowage = string.Format(sqlStowage, cbbPacking.Text.Trim());
                using (IDataReader rdr = DBHelper.ExecuteReader(sqlStowage))
                {
                    dtStowageData.Load(rdr);
                }
                //初始化停车位信息
                int XStart, XEnd, YStart, YEnd, XLength, YLength;
                XStart = JudgeIntNull(dtParksize.Rows[0]["X_START"]);
                XEnd = JudgeIntNull(dtParksize.Rows[0]["X_END"]);
                YStart = JudgeIntNull(dtParksize.Rows[0]["Y_START"]);
                YEnd = JudgeIntNull(dtParksize.Rows[0]["Y_END"]);
                XLength = JudgeIntNull(dtParksize.Rows[0]["X_LENGTH"]);
                YLength = JudgeIntNull(dtParksize.Rows[0]["Y_LENGTH"]);
                //画面显示
                park.InitializeXY(park.Size.Width - 10, park.Size.Height - 10);

                //生成车位
                //宽增加0.5m、长度增加1.5m
                park.CreatePassageWayArea(XStart - 500, YEnd + 1500, 7000, 17000);

                if (curCarType == 100 || curCarType == 102 || curCarType == 106) //有车边框
                {
                    //park.DrawParksize(XStart, YEnd, XEnd - XStart, YEnd - YStart);
                    if (strParkNo.Contains("Z62"))
                    {
                        park.DrawParksize(XStart, YEnd, XEnd - XStart, YEnd - YStart, carHearDrection, "INC");
                    }
                    else
                    {
                        park.DrawParksize(XStart, YEnd, XEnd - XStart, YEnd - YStart, carHearDrection);
                    }
                    //生成车位置
                    ////获取车位坐标XMin, XMax, YMin, YMax
                    int XMin, XMax, YMin, YMax;
                    int XVehicleLength, YVehicleLength;
                    // int XView, YView;
                    XMin = XMax = YMin = YMax = XVehicleLength = YVehicleLength = 0;
                    if (dtLaserData.Rows.Count != 0)
                    {
                        XMin = JudgeIntNull(dtLaserData.Rows[0]["CAR_X_BORDER_MIN"]);
                        XMax = JudgeIntNull(dtLaserData.Rows[0]["CAR_X_BORDER_MAX"]);
                        YMin = JudgeIntNull(dtLaserData.Rows[0]["CAR_Y_BORDER_MIN"]);
                        YMax = JudgeIntNull(dtLaserData.Rows[0]["CAR_Y_BORDER_MAX"]);
                        XVehicleLength = XMax - XMin;
                        YVehicleLength = YMax - YMin;
                        //画面显示
                        //if (txtCarHeadToward.Text == "西")
                        //{
                        //    park.CreateCarSize(XMin, YMax, XVehicleLength, YVehicleLength, 1, "W"); //框架车类型不一样1，社会车0
                        //}
                        //else
                        park.CreateCarSize(XMin, YMax, XVehicleLength, YVehicleLength, 1, ""); //框架车类型不一样1，社会车0
                        //
                    }
                    else
                    {
                        park.ClearbitM();
                        return;
                    }
                }
                else  //无车边框
                {
                    if (!hasParkSize)
                    {
                        //park.DrawParksize(XStart, YEnd, XEnd - XStart, YEnd - YStart);
                        if (strParkNo.Contains("Z62"))
                        {
                            park.DrawParksize(XStart, YEnd, XEnd - XStart, YEnd - YStart, carHearDrection, "INC");
                        }
                        else
                        {
                            park.DrawParksize(XStart, YEnd, XEnd - XStart, YEnd - YStart, carHearDrection);
                        }
                        if (carHearDrection != "")
                        {
                            hasParkSize = true;
                        }
                    }
                    park.HasCarSize = false;
                }
                //生成钢卷位置
                foreach (DataRow item in dtStowageData.Rows)
                {
                    int n1 = JudgeIntNull(item["X_CENTER"]);
                    int n2 = JudgeIntNull(item["Y_CENTER"]);
                    int n3 = JudgeIntNull(item["OUTDIA"]);  //外径
                    int n4 = JudgeIntNull(item["WIDTH"]);  //宽度
                    string strID = JudgeStrNull(item["GROOVEID"]);
                    string strMat = JudgeStrNull(item["MAT_NO"]);
                    int flag1 = 3;  //封锁标记(0:可用 1:待判 2:封锁)
                    if (item["LOCK_FLAG"] == DBNull.Value)
                    {
                        flag1 = 3;
                    }
                    else
                    {
                        flag1 = Convert.ToInt32(item["LOCK_FLAG"]);
                    }
                    if (n1 != 0 && n2 != 0)
                    {
                        //park.CreateCoilSize(n1, n2, n4, n3, strID, true);
                        //park.CreateCoilSize(n1, n2, n4, n3, strID, true,strMat, toolTip1 );
                        park.CreateCoilSize(n1, n2, n4, n3, strID, false, strMat, toolTip1, flag1);
                        //park.CreateLaserLocation(n1, n2, 4000, 120);
                    }
                }
                //生成激光位置
                foreach (DataRow item in dtLaserData.Rows)
                {
                    int n1 = JudgeIntNull(item["GROOVE_ACT_X"]);
                    int n2 = JudgeIntNull(item["GROOVE_ACT_Y"]);
                    string strID = JudgeStrNull(item["GROOVEID"]);
                    if (n1 != 0 && n2 != 0)
                    {
                        //park.CreateCoilSize(n1, n2, 4000, 120,strID,false);
                        park.CreateLaserLocation(n1, n2, 4000, 120);
                        //有卷测试
                        //park.CreateCoilSize(n1, n2, 1200, 1200, strID, true);
                    }
                }

            }
            catch (Exception er)
            {

                MessageBox.Show(string.Format("{0} {1}", er.TargetSite, er.ToString()));
            }



        }
        void parkLaserOut1_LabClick(string matNO)
        {
            SelectDataGridViewRow(dataGridView2, matNO, "COIL_NO2");
        }
        private bool reflreshParkingCoilstate(string parkNO)
        {
            DataTable dtStowage = new DataTable();
            bool ret = false;
            string matNO;
            string coilStatus;
            string stowageID = txtStowageID.Text.Trim();
            if (!parkNO.Contains('A') || stowageID.Length < 2)
            {
                return ret;
            }
            try
            {
                string sqlStowage = @" SELECT C.MAT_NO,C.STATUS FROM UACS_TRUCK_STOWAGE_DETAIL C ";
                sqlStowage += " WHERE C.STOWAGE_ID = " + stowageID + " ORDER BY GROOVEID ";
                //sqlStowage += " WHERE C.STOWAGE_ID IN (SELECT STOWAGE_ID FROM UACS_PARKING_STATUS  ";
                //sqlStowage += " WHERE  CAR_NO IN ( SELECT CAR_NO FROM UACS_PARKING_STATUS WHERE PARKING_NO ='{0}')) ORDER BY GROOVEID ";
                //sqlStowage = string.Format(sqlStowage, parkNO);
                using (IDataReader rdr = DBHelper.ExecuteReader(sqlStowage))
                {
                    dtStowage.Load(rdr);
                }
                if (dtStowage.Rows.Count > 0)
                {
                    foreach (DataRow item in dtStowage.Rows)
                    {
                        matNO = JudgeStrNull(item["MAT_NO"]);
                        coilStatus = JudgeStrNull(item["STATUS"]);
                        if (curCarType == 100 || curCarType == 102)
                        {
                            parkLaserOut1.ChangeCoilState(matNO, coilStatus, "1");
                        }
                        else
                        {
                            parkLaserOut1.ChangeCoilState(matNO, coilStatus, "0");
                        }

                    }
                }

                return ret;
            }
            catch (Exception er)
            {
                MessageBox.Show(string.Format("{0} {1}", er.TargetSite, er.ToString()));
                return ret;
            }

        }
        #endregion



        #region 激光数据显示
        /// <summary>
        /// 激光数据
        /// </summary>
        /// <param name="theTreatmentNO">停车位处理号</param>
        /// <param name="theCount">扫描次数</param>
        private void Inq_Laser(string theTreatmentNO, string theCount)
        {
            try
            {
                #region 20230302 0945
                if (theTreatmentNO == "" || theCount == "")
                {
                    dt_Laser.Clear();
                    dataGridView_LASER.DataSource = dt_Laser;
                    return;
                }
                //出库激光扫描信息
                string sqlText_Laser = @"SELECT POINT_X_1,POINT_Y_1,POINT_Z_1,POINT_X_2,POINT_Y_2,POINT_Z_2,POINT_X_3,POINT_Y_3,POINT_Z_3,POINT_X_4,POINT_Y_4,POINT_Z_4 FROM UACS_PARKING_SRS_CAR_INFO WHERE 1=1 ";
                //停车位处理号
                sqlText_Laser += " AND  TREATMENT_NO = '" + theTreatmentNO + "' ";
                //扫描结果 1-成功 0-失败
                sqlText_Laser += " AND  SCAN_RESULT = '1' ";
                if (!string.IsNullOrEmpty(txtCarNo.Text.ToString().Trim()))
                {
                    //车号
                    sqlText_Laser += " AND  CAR_NO = '" + txtCarNo.Text.ToString().Trim() + "' ";
                }
                if (!string.IsNullOrEmpty(cbbPacking.Text.Trim()) && cbbPacking.Text.Trim() != "请选择" && cbbPacking.Text.Trim().Contains('A'))
                {
                    //停车位号
                    sqlText_Laser += " AND  PARKING_NO = '" + cbbPacking.Text.Trim() + "' ";
                }
                //if (!string.IsNullOrEmpty(theCount))
                //{
                //    //扫描次数
                //    sqlText_Laser += " AND  SCAN_COUNT = '" + theCount + "' ";
                //}
                sqlText_Laser += " ORDER BY SCAN_NO, REC_TIME ";

                //初始化grid
                if (dataGridView_LASER.DataSource != null && dataGridView_LASER.Rows.Count > 0)
                {
                    dt_Laser.Clear();
                }
                else
                {
                    if (!dt_Laser.Columns.Contains("ROW_INDEX"))
                    {
                        dt_Laser.Columns.Add("ROW_INDEX");
                        dt_Laser.Columns.Add("POINT_X");
                        dt_Laser.Columns.Add("POINT_Y");
                        dt_Laser.Columns.Add("POINT_Z");
                    }
                }

                using (IDataReader rdr = DBHelper.ExecuteReader(sqlText_Laser))
                {
                    int index = 1;
                    while (rdr.Read())
                    {
                        DataRow dr = dt_Laser.NewRow();
                        dr["ROW_INDEX"] = Convert.ToString(index++);
                        dr["POINT_X"] = rdr["POINT_X_1"].ToString();
                        dr["POINT_Y"] = rdr["POINT_Y_1"].ToString();
                        dr["POINT_Z"] = rdr["POINT_Z_1"].ToString();
                        dt_Laser.Rows.Add(dr);

                        dr = dt_Laser.NewRow();
                        dr["ROW_INDEX"] = Convert.ToString(index++);
                        dr["POINT_X"] = rdr["POINT_X_2"].ToString();
                        dr["POINT_Y"] = rdr["POINT_Y_2"].ToString();
                        dr["POINT_Z"] = rdr["POINT_Z_2"].ToString();
                        dt_Laser.Rows.Add(dr);

                        dr = dt_Laser.NewRow();
                        dr["ROW_INDEX"] = Convert.ToString(index++);
                        dr["POINT_X"] = rdr["POINT_X_3"].ToString();
                        dr["POINT_Y"] = rdr["POINT_Y_3"].ToString();
                        dr["POINT_Z"] = rdr["POINT_Z_3"].ToString();
                        dt_Laser.Rows.Add(dr);

                        dr = dt_Laser.NewRow();
                        dr["ROW_INDEX"] = Convert.ToString(index++);
                        dr["POINT_X"] = rdr["POINT_X_4"].ToString();
                        dr["POINT_Y"] = rdr["POINT_Y_4"].ToString();
                        dr["POINT_Z"] = rdr["POINT_Z_4"].ToString();
                        dt_Laser.Rows.Add(dr);

                    }
                    //dt_Laser.Load(dt);
                }
                dataGridView_LASER.DataSource = dt_Laser;
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message + "\r\n" + er.StackTrace);
            }
            #endregion
        }
        #endregion
        /// <summary>
        /// 每10秒刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            //labTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //DateTime myDateTime = DateTime.ParseExact(laserTime, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);

            if (isStowage == true)
            {
                if (cbbPacking.Text.Contains('A'))
                {
                    reflreshParkingCoilstate(cbbPacking.Text.Trim());
                    RefreshOrderDgv(cbbPacking.Text.Trim());
                    setCurParkStatus(cbbPacking.Text.Trim());
                    displayStowageTime(labTime, txtStowageID.Text.Trim()); // //显示配载生成后的时间差
                }
            }

            if (dataGridView2.DataSource != null && ((DataTable)dataGridView2.DataSource).Rows.Count > 0)
            {
                isStowage = true;
                return;
            }
            isStowage = false;
            if (!cbbPacking.Text.Contains('A'))
            {
                //GetParkInfo();
                GetParkInfoByBay();
                return;
            }
            RefreshHMI();
            //Stowage_intercept(cmbArea.Text.Trim(), cbbPacking.Text.Trim(), txtCarNo.Text.Trim(), dataGridView2);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (GetStowageDetail2())
            {
                MessageBox.Show("车辆已经配载材料，请先做车离。");
                return;
            }
            if (curCarType == 100)
            {
                goto kuangjiache;
            }
            SelectCoilForm selectCoilF = new SelectCoilForm();
            selectCoilF.TransferValue += selectCoilF_TransferValue;
            selectCoilF.CarType = "社会车";
            if (cbbPacking.Text.Trim() != "请选择" && cbbPacking.Text.Trim() != "" && cbbPacking.Text.Trim().Contains('A'))
            {
                selectCoilF.ParkNO = cbbPacking.Text.Trim();
                if (txtCarNo.Text.Trim().Length > 3)
                    selectCoilF.CarNO = txtCarNo.Text.Trim();
                else
                {
                    MessageBox.Show("车牌号信息不正确！");
                    return;
                }
                selectCoilF.ShowDialog();
            }
            else
            {
                MessageBox.Show("车位信息不正确！");
            }
            return;
        kuangjiache:
            {
                if (cbbPacking.Text.Trim() != "请选择" && cbbPacking.Text.Trim() != "" && cbbPacking.Text.Trim().Contains('A'))
                {
                    string parkNO = cbbPacking.Text;
                    //string GrooveNum = txtSelectGoove.Text;
                    string GrooveNum = "";
                    if (auth.IsOpen("框架车出库材料选择"))
                    {
                        auth.CloseForm("框架车出库材料选择");
                        auth.OpenForm("框架车出库材料选择", parkNO, GrooveNum);
                    }
                    else
                        auth.OpenForm("框架车出库材料选择", parkNO, GrooveNum);
                }
                else
                {
                    MessageBox.Show("车位号信息不正确！");
                }
            }


        }

        void selectCoilF_TransferValue(string weight, bool isLoad)
        {
            if (weight != "")
            {
                if (Convert.ToInt32(weight) > 50000)
                    txtCoilsWeight.BackColor = Color.Red;
                else
                    txtCoilsWeight.BackColor = Color.White;
            }
            RefreshHMI();
            txtCoilsWeight.Text = string.Format("{0} /公斤", weight);
            isStowage = isLoad;
        }

        #region 作业开始
        private void btnOperateStrat_Click(object sender, EventArgs e)
        {
            string parkNO = cbbPacking.Text.Trim();
            try
            {
                if (parkNO == "" || !parkNO.Contains('A'))
                {
                    MessageBox.Show("请选择停车位。");
                    return;
                }
                if (txtCarNo.Text.Equals(""))
                {
                    MessageBox.Show("车位无车，不能开始。");
                    return;
                }

                if (parkNO != "请选择" || parkNO != "")
                {
                    MessageBoxButtons btn = MessageBoxButtons.OKCancel;
                    DialogResult dr = MessageBox.Show("是否对" + parkNO + "作业开始？", "提示", btn, MessageBoxIcon.Asterisk);
                    if (dr == DialogResult.OK)
                    {
                        TagDP.SetData("EV_NEW_PARKING_JOB_RESUME", parkNO);
                        ParkClassLibrary.HMILogger.WriteLog(btnOperateStrat.Text, "作业开始：" + cbbPacking.Text, ParkClassLibrary.LogLevel.Info, this.Text);
                        btnOperateStrat.ForeColor = Color.Green;
                        btnOperatePause.ForeColor = Color.White;   //Color.Black;
                    }

                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message + "\r\n" + er.StackTrace);
            }
        }

        /// <summary>
        /// 获取车位是否是暂停状态
        /// </summary>
        /// <param name="parkNO"></param>
        /// <returns></returns>
        private bool GetOperateStatus(string parkNO)
        {
            bool ret = false;
            try
            {
                string SQLOder = "  SELECT WORK_STATUS FROM UACS_PARKING_WORK_STATUS WHERE PARKING_NO ='{0}' ";
                SQLOder = string.Format(SQLOder, parkNO);
                using (IDataReader rdr = DBHelper.ExecuteReader(SQLOder))
                {
                    while (rdr.Read())
                    {
                        int status = 0;
                        status = JudgeIntNull(rdr["WORK_STATUS"]);
                        if (status == 270) //暂停
                        {
                            ret = true;
                            break;
                        }
                    }
                }
                if (ret) //开始
                {
                    btnOperateStrat.ForeColor = Color.White; //Color.Black;
                    btnOperatePause.ForeColor = Color.Orange;
                }
                else
                {
                    btnOperatePause.ForeColor = Color.White; //Color.Black;
                    btnOperateStrat.ForeColor = Color.White; //Color.Black;
                }
                return ret;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0},{1}", ex.StackTrace.ToString(), ex.Message.ToString()));
                return ret;
            }
            //
        }
        #endregion

        #region 作业暂停

        private void btnOperatePause_Click(object sender, EventArgs e)
        {
            string parkNO = cbbPacking.Text.Trim();
            try
            {
                if (parkNO == "" || !parkNO.Contains('A'))
                {
                    MessageBox.Show("请选择停车位。");
                    return;
                }
                if (txtCarNo.Text.Equals(""))
                {
                    MessageBox.Show("车位无车，不能开始。");
                    return;
                }

                if (parkNO != "请选择" || parkNO != "")
                {
                    MessageBoxButtons btn = MessageBoxButtons.OKCancel;
                    DialogResult dr = MessageBox.Show("是否对" + parkNO + "作业暂停？", "提示", btn, MessageBoxIcon.Asterisk);
                    if (dr == DialogResult.OK)
                    {
                        TagDP.SetData("EV_NEW_PARKING_JOB_PAUSE", parkNO);
                        //TagDP.SetData("EV_NEW_PARKING_Z32_CRANE_ALLOW", parkNO);
                        ParkClassLibrary.HMILogger.WriteLog(btnOperatePause.Text, "作业暂停：" + parkNO, ParkClassLibrary.LogLevel.Info, this.Text);
                        btnOperateStrat.ForeColor = Color.White; //Color.Black;
                        btnOperatePause.ForeColor = Color.Orange;
                    }
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message + "\r\n" + er.StackTrace);
            }
        }
        #endregion

        #region 画面跳转
        private bool JumpToOtherForm(string currPark)
        {
            string cmbarea = cmbArea.Text;
            bool ret = false;
            Int16 carType = -1;
            string strStatus = GetParkStatus(currPark, out carType);
            if (strStatus == "")
            {
                return ret;
            }
            if (strStatus.Substring(0, 1) == "1" && strStatus.Length == 3) //入库
            {
                if (auth.IsOpen("01 车辆入库"))
                {
                    auth.CloseForm("01 车辆入库");
                }
                //auth.OpenForm("01 车辆入库", true, cmbarea, currPark);
                auth.OpenForm("01 车辆入库", currPark);
                ret = true;
            }
            //else if (strStatus.Substring(0, 1) == "2" && (carType != 101 && carType != 103)) //框架出库
            //{
            //    if (auth.IsOpen("成品库框架车出库"))
            //    {
            //        auth.CloseForm("成品库框架车出库");
            //    }
            //    auth.OpenForm("成品库框架车出库", currPark);
            //    ret = true;
            //}
            //刷新画面
            RefreshHMI();
            return ret;
        }
        /// <summary>
        /// 返回车位状态
        /// </summary>
        /// <param name="parkingNO"></param>
        /// <returns></returns>
        private string GetParkStatus(string parkingNO, out Int16 carType)
        {
            string ret = "";
            int isWoodenCar = 0;
            carType = -1;
            if (!parkingNO.Contains('A'))
            {
                return ret;
            }
            try
            {
                string sqlText = " SELECT C.WORK_STATUS,A.CAR_TYPE FROM UACS_PARKING_WORK_STATUS  C ";
                sqlText += " LEFT JOIN UACS_TRUCK_STOWAGE A ON C.STOWAGE_ID = A.STOWAGE_ID ";
                sqlText += " WHERE PARKING_NO = '" + parkingNO + "'";
                //string sqlText = " SELECT C.PARKING_STATUS,A.CAR_TYPE,A.ISWOODENCAR FROM UACS_PARKING_STATUS  C ";
                //sqlText += " LEFT JOIN UACS_TRUCK_STOWAGE A ON C.STOWAGE_ID = A.STOWAGE_ID ";
                //sqlText += " WHERE PARKING_NO = '" + parkingNO + "'";
                using (IDataReader rdr = DBHelper.ExecuteReader(sqlText))
                {
                    while (rdr.Read())
                    {
                        //if (rdr["ISWOODENCAR"] != DBNull.Value)
                        //{
                        //    isWoodenCar = (Int16)ManagerHelper.JudgeIntNull(rdr["ISWOODENCAR"]);
                        //}
                        ret = ManagerHelper.JudgeStrNull(rdr["WORK_STATUS"]);
                        carType = (Int16)ManagerHelper.JudgeIntNull(rdr["CAR_TYPE"]);
                    }
                    if (carType == 100 || carType == 101 || carType == 102 || carType == 103 || carType == 106)
                    {
                        //if (carType == 101 && isWoodenCar == 1)
                        if (carType == 101)
                        {
                            btnUnitToCar.Enabled = false;
                            button_L3Stowage.Enabled = false;
                            btnSeleceByMat.Enabled = false;
                            btnWoodenCar.Enabled = true;
                        }
                        else
                        {
                            btnUnitToCar.Enabled = true;
                            button_L3Stowage.Enabled = true;
                            btnSeleceByMat.Enabled = true;
                            btnWoodenCar.Enabled = false;
                        }
                    }
                    else if (carType == 200)
                    {
                        btnUnitToCar.Enabled = false;
                        button_L3Stowage.Enabled = false;
                        btnSeleceByMat.Enabled = false;
                        btnWoodenCar.Enabled = true;
                    }
                    else
                    {
                        btnSeleceByMat.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0},{1}", ex.StackTrace.ToString(), ex.Message.ToString()));
            }
            return ret;
        }
        #endregion
        private void cmbArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetComboxOnParkingByBay(cbbPacking);
            //GetComboxOnParking();
            //GetComboxOnParkingByBay();
            if (cmbArea.Text.Contains("后"))
            {
                labTips.Text = "";
            }
            else
            {
                labTips.Text = "  -->\r\n库后区\r\n方向";
            }

        }

        /// <summary>
        /// getCarBorderSize
        /// </summary>
        /// <param name="laserCount">激光扫描次数</param>
        /// <param name="treatmentNO">处理号</param>
        private void getCarBorderSize(string laserCount, string treatmentNO)
        {
            //出库激光车边框信息
            string sqlText_Border = @"SELECT CAR_X_BORDER_MAX, CAR_X_BORDER_MIN, CAR_Y_BORDER_MAX, CAR_Y_BORDER_MIN FROM UACS_LASER_OUT WHERE 1=1 ";

            sqlText_Border += " AND LASER_ACTION_COUNT = '" + laserCount + "' AND TREATMENT_NO = '" + treatmentNO + "' FETCH FIRST 1 ROWS ONLY ";

            using (IDataReader rdr = DBHelper.ExecuteReader(sqlText_Border))
            {
                if (rdr.Read())
                {
                    if (rdr["CAR_X_BORDER_MAX"] != System.DBNull.Value)
                    {
                        txt_POINT_X_MAX.Text = Convert.ToString(rdr["CAR_X_BORDER_MAX"]);
                    }
                    if (rdr["CAR_X_BORDER_MIN"] != System.DBNull.Value)
                    {
                        txt_POINT_X_MIN.Text = Convert.ToString(rdr["CAR_X_BORDER_MIN"]);
                    }
                    if (rdr["CAR_Y_BORDER_MAX"] != System.DBNull.Value)
                    {
                        txt_POINT_Y_MAX.Text = Convert.ToString(rdr["CAR_Y_BORDER_MAX"]);
                    }
                    if (rdr["CAR_Y_BORDER_MIN"] != System.DBNull.Value)
                    {
                        txt_POINT_Y_MIN.Text = Convert.ToString(rdr["CAR_Y_BORDER_MIN"]);
                    }
                }
                else
                {
                    txt_POINT_X_MAX.Text = "";
                    txt_POINT_X_MIN.Text = "";
                    txt_POINT_Y_MAX.Text = "";
                    txt_POINT_Y_MIN.Text = "";
                }
            }
        }

        /// <summary>
        /// 获得当前车信息
        /// </summary>
        /// <param name="parkNO">停车位号</param>
        private void GetCurrentCarInfo(string parkNO)
        {
            DataTable dt = new DataTable();
            dt.Clear();
            try
            {
                //string SQLOder = " SELECT A.LENGTH,A.WIDTH,A.HEIGHT,A.LOAD_CAPACITY,A.SADDLE_NUM,A.SADDLE_INTERVAL,A.DISTANCE_HEAD,A.DISTANCE_LEFT,A.DISTANCE_RIGHT";
                //SQLOder += " FROM UACS_TRUCK_FRAME_DEFINE A WHERE FRAME_TYPE_NO IN ( SELECT  CAR_NO FROM UACS_PARKING_STATUS B WHERE B.PARKING_NO ='{0}') ";
                string SQLOder = " SELECT A.LENGTH,A.WIDTH,A.HEIGHT,A.LOAD_CAPACITY,A.SADDLE_NUM,A.SADDLE_INTERVAL,A.DISTANCE_HEAD,A.DISTANCE_LEFT,A.DISTANCE_RIGHT";
                SQLOder += " FROM UACS_TRUCK_FRAME_DEFINE A WHERE FRAME_TYPE_NO IN ( SELECT  CAR_NO FROM UACS_PARKING_WORK_STATUS B WHERE B.PARKING_NO ='{0}') ";
                SQLOder = string.Format(SQLOder, parkNO);
                using (IDataReader odrIn = DBHelper.ExecuteReader(SQLOder))
                {
                    dt.Load(odrIn);
                }
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        //txt_LENGTH.Text = JudgeStrNull(item["LENGTH"]);
                        //txt_WIDTH.Text = JudgeStrNull(item["WIDTH"]);
                        //txt_CENTER_X.Text = JudgeStrNull(item["HEIGHT"]);
                        txtGrooveDist.Text = JudgeStrNull(item["SADDLE_INTERVAL"]);
                        txtGrooveNum.Text = JudgeStrNull(item["SADDLE_NUM"]);
                        txtHeadDist.Text = JudgeStrNull(item["DISTANCE_HEAD"]);
                        txtsideDist.Text = JudgeStrNull(item["DISTANCE_LEFT"]);
                    }
                }
                else
                {
                    //txt_LENGTH.Text = "";
                    //txt_WIDTH.Text = "";                    
                    txtGrooveDist.Text = "";
                    txtGrooveNum.Text = "";
                    txtHeadDist.Text = "";
                    txtsideDist.Text = "";                    
                }
                txtLaserStatus.Text = "";
                //添加激光状态显示
                SQLOder = "";
                SQLOder = " SELECT WORK_STATUS FROM UACS_PARKING_WORK_STATUS WHERE PARKING_NO = '" + parkNO + "'";
                string laserStatus = "";
                using (IDataReader rdr = DBHelper.ExecuteReader(SQLOder))
                {
                    if (rdr.Read())
                    {
                        laserStatus = ManagerHelper.JudgeStrNull(rdr["WORK_STATUS"]);
                        int index = laserStatus.IndexOf("::");
                        if (index != -1)
                        {
                            laserStatus = laserStatus.Substring(index + 2);
                        }
                        txtLaserStatus.Text = laserStatus;
                        if (laserStatus.Equals("5"))
                        {
                            txtLaserStatus.Text = "无车";
                        }
                        else if (laserStatus.Equals("10"))
                        {
                            txtLaserStatus.Text = "有车到达";
                        }
                        else if (laserStatus.Equals("110"))
                        {
                            txtLaserStatus.Text = "激光扫描开始";
                        }
                        else if (laserStatus.Equals("120"))
                        {
                            txtLaserStatus.Text = "入库激光扫描完成";
                        }
                        else if (laserStatus.Equals("130"))
                        {
                            txtLaserStatus.Text = "入库手持扫描完成";
                        }
                        else if (laserStatus.Equals("210"))
                        {
                            txtLaserStatus.Text = "出库激光扫描开始";
                        }
                        else if (laserStatus.Equals("220"))
                        {
                            txtLaserStatus.Text = "出库激光扫描完成";
                        }
                    }

                }
                //if (laserStatus.Contains("start"))
                //{
                //    txtLaserStatus.Text = "激光没开始！";
                //}
                //else if (laserStatus.Contains("send data begin"))
                //{
                //    txtLaserStatus.Text = "激光发送数据！";
                //}
                //else if (laserStatus.Contains("scan strat"))
                //{
                //    txtLaserStatus.Text = "扫描开始"; 
                //}
                //else if (laserStatus.Contains("scan end"))
                //{

                //}
                //else if (laserStatus.Contains("receive scan event"))
                //{

                //}


            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0},{1}", ex.StackTrace.ToString(), ex.Message.ToString()));
            }
        }

        private void btnSeleceByMat_Click(object sender, EventArgs e)
        {
            if (GetStowageDetail2())
            {
                MessageBox.Show("车辆已经配载材料，请先做车离。");
                return;
            }
            SelectCoilForm selectCoilF = new SelectCoilForm();
            //selectCoilF.CarType = curCarType.ToString();
            string sqltext = @"select CAR_TYPE from UACS_TRUCK_STOWAGE where STOWAGE_ID = '" + txtStowageID.Text.Trim() + "'";
            using (IDataReader rdr = DBHelper.ExecuteReader(sqltext))
            {
                if (rdr.Read())
                {
                    selectCoilF.CarType = rdr["CAR_TYPE"].ToString();
                }
            }

            if (cbbPacking.Text.Trim() != "请选择" && cbbPacking.Text.Trim() != "" && cbbPacking.Text.Trim().Contains('A'))
            {
                selectCoilF.ParkNO = cbbPacking.Text.Trim();
                selectCoilF.TransferValue += selectCoilF_TransferValue;
                selectCoilF.GrooveNum = txtGrooveNum.Text.Trim();
                //selectCoilF.GrooveTotal = txtSelectGoove.Text.Trim();

                if (txtCarNo.Text.Trim().Length > 3)
                    selectCoilF.CarNO = txtCarNo.Text.Trim();
                else
                {
                    MessageBox.Show("车牌号信息不正确！");
                    return;
                }
                selectCoilF.ShowDialog();
            }
            else
            {
                MessageBox.Show("车位号信息不正确！");
            }
        }
        private int checkParkingIsWorking(int stowageID)
        {
            int count = -1;
            try
            {
                string sqlText = " SELECT COUNT (*) COUNT FROM  UACS_TRUCK_STOWAGE_DETAIL WHERE STOWAGE_ID ='" + stowageID + "'   AND STATUS IN(0,30)";

                using (IDataReader rdr = DBHelper.ExecuteReader(sqlText))
                {
                    if (rdr.Read())
                    {
                        count = ManagerHelper.JudgeIntNull(rdr["COUNT"]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0},{1}", ex.StackTrace.ToString(), ex.Message.ToString()));
            }
            return count;
        }
        private void bindParkControlEvent()
        {
            foreach (var item in tabPanelParkArea.Controls)
            {
                if (item is ParkingState)
                {
                    ((ParkingState)item).sendMember += PorductMatManage_sendMember;
                }
            }
        }

        void PorductMatManage_sendMember(string parkName)
        {
            if (!JumpToOtherForm(parkName))
            {
                cbbPacking.Text = parkName;
                //cbbPacking_SelectedIndexChanged(null, null);
                setCurParkStatus(parkName);
            }
        }

        private void setCurParkStatus(string parkName)
        {
            foreach (var item in tabPanelParkArea.Controls)
            {
                if (item is ParkingState)
                {
                    if (((ParkingState)item).ParkName == parkName)
                    {
                        ((ParkingState)item).setControilBackColor(Color.LightBlue);
                    }
                    else
                    {
                        ((ParkingState)item).setControilBackColor(SystemColors.InactiveCaption);
                    }
                }
            }
        }

        #region 配载时间显示

        private void displayStowageTime(Label labTime, string stowageID)
        {
            if (stowageID == "")
            {
                return;
            }
            //显示配载生成后的时间差
            strLaserTime = getStowageCreatTime(stowageID);
            //if (!isReadTime)
            //{
            //    strLaserTime = getStowageCreatTime(stowageID);
            //    isReadTime = true;
            //}
            if (strLaserTime.Length >= 15)
            {
                DateTime dtimeLaserEnd = DateTime.Parse(strLaserTime); //DateTime.ParseExact(strLaserTime, "yyyy/MM/ddHH:mm:ss", System.Globalization.CultureInfo.CurrentCulture);
                labTime.Text = "配载已生成：" + caculateTimeExpend(dtimeLaserEnd);
            }
            else
                labTime.Text = "---";
        }
        private string getStowageCreatTime(string stowageID)
        {
            string retStr = "";
            try
            {
                string SQLOder = "  SELECT REC_TIME FROM UACS_TRUCK_STOWAGE WHERE STOWAGE_ID ='" + stowageID + "' ";
                using (IDataReader rdr = DBHelper.ExecuteReader(SQLOder))
                {
                    if (rdr.Read())
                    {
                        retStr = JudgeStrNull(rdr["REC_TIME"]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0},{1}", ex.StackTrace.ToString(), ex.Message.ToString()));
            }
            return retStr;
        }


        private string caculateTimeExpend(DateTime timeStart)
        {
            string strTime = "";
            try
            {
                TimeSpan timeExpend = TimeSpan.MinValue;
                timeExpend = DateTime.Now - timeStart;
                strTime = timeExpend.Hours.ToString() + "时" + timeExpend.Minutes.ToString() + "分" + timeExpend.Seconds.ToString() + "秒";
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
            }
            return strTime;
        }
        #endregion

        #region 物流提升，重L3获取配载
        private void btnGetStowage_Click(object sender, EventArgs e)
        {

        }
        #endregion


        #region 车到位和装车配载

        /// <summary>
        /// 装车配载 点击事件（新）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_L3Stowage_Click(object sender, EventArgs e)
        {
            //if (GetStowageDetail())
            //{
            //    MessageBox.Show("车辆已经配载材料，请先做车离。");
            //    return;
            //}
            SelectCoilByL3FormNew selectCoilF = new SelectCoilByL3FormNew();
            selectCoilF.CarType = curCarType.ToString().Trim();
            if (cbbPacking.Text.Trim() != "请选择" && cbbPacking.Text.Trim() != "" && cbbPacking.Text.Trim().Contains('A'))
            {
                selectCoilF.ParkNO = cbbPacking.Text.Trim();
                selectCoilF.TransferValue += selectCoilF_TransferValue;
                selectCoilF.GrooveNum = txtGrooveNum.Text.Trim();
                selectCoilF.CarNO = txtCarNo.Text.Trim();
                //selectCoilF.Size = new Size(2155, 1400);
                selectCoilF.ShowDialog();
            }
            else
            {
                MessageBox.Show("车位号信息不正确！");
            }

        }
        #region 装车配载（旧）

        ///// <summary>
        ///// 装车配载 点击事件（旧）
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void button_L3Stowage_Click(object sender, EventArgs e)
        //{
        //    if (GetStowageDetail())
        //    {
        //        MessageBox.Show("车辆已经配载材料，请先做车离。");
        //        return;
        //    }
        //    #region 旧窗口

        //    //SelectCoilByL3Form selectCoilF = new SelectCoilByL3Form();
        //    //selectCoilF.CarType = curCarType.ToString().Trim();
        //    //if (cbbPacking.Text.Trim() != "请选择" && cbbPacking.Text.Trim() != "" && cbbPacking.Text.Trim().Contains('A'))
        //    //{
        //    //    selectCoilF.ParkNO = cbbPacking.Text.Trim();
        //    //    selectCoilF.TransferValue += selectCoilF_TransferValue;
        //    //    selectCoilF.GrooveNum = txtGrooveNum.Text.Trim();
        //    //    //selectCoilF.GrooveTotal = txtSelectGoove.Text.Trim();

        //    //    if (txtCarNo.Text.Trim().Length > 3)
        //    //        selectCoilF.CarNO = txtCarNo.Text.Trim();
        //    //    else
        //    //    {
        //    //        MessageBox.Show("车牌号信息不正确！");
        //    //        return;
        //    //    }
        //    //    selectCoilF.ShowDialog();
        //    //}
        //    //else
        //    //{
        //    //    MessageBox.Show("车位号信息不正确！");
        //    //} 
        //    #endregion

        //    SelectCoilByL3FormNew selectCoilF = new SelectCoilByL3FormNew();
        //    selectCoilF.CarType = curCarType.ToString().Trim();
        //    if (cbbPacking.Text.Trim() != "请选择" && cbbPacking.Text.Trim() != "" && cbbPacking.Text.Trim().Contains('A'))
        //    {
        //        selectCoilF.ParkNO = cbbPacking.Text.Trim();
        //        selectCoilF.TransferValue += selectCoilF_TransferValue;
        //        selectCoilF.GrooveNum = txtGrooveNum.Text.Trim();
        //        //selectCoilF.GrooveTotal = txtSelectGoove.Text.Trim();

        //        if (txtCarNo.Text.Trim().Length > 3)
        //            selectCoilF.CarNO = txtCarNo.Text.Trim();
        //        else
        //        {
        //            MessageBox.Show("车牌号信息不正确！");
        //            return;
        //        }
        //        selectCoilF.ShowDialog();
        //    }
        //    else
        //    {
        //        MessageBox.Show("车位号信息不正确！");
        //    }

        //}

        #endregion

        #endregion

        private void cbbPacking_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            JumpToOtherForm(cbbPacking.Text);
        }

        //行车指令判断
        private bool JudgeCraneOrder(string ParkingNo)
        {
            int count = 0;
            string fromStockNo = String.Empty;
            string toStockNo = String.Empty;
            try
            {
                string sql = "select * from UACS_CRANE_ORDER_CURRENT ";
                using (IDataReader rdr = DBHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        if (rdr["FROM_STOCK_NO"] != DBNull.Value && rdr["TO_STOCK_NO"] != DBNull.Value)
                        {
                            fromStockNo = rdr["FROM_STOCK_NO"].ToString();
                            toStockNo = rdr["TO_STOCK_NO"].ToString();
                            if (fromStockNo.Contains(ParkingNo) || toStockNo.Contains(ParkingNo))
                            {
                                count++;
                            }
                        }
                    }
                }
            }
            catch (Exception er)
            {

                MessageBox.Show(er.Message);
            }
            if (count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void btnUnitToCar_Click(object sender, EventArgs e)
        {
            int flag = 0;
            try
            {
                //if (!cbbPacking.Text.Contains('A'))
                //{
                //    MessageBox.Show("请选择停车位。");
                //    return;
                //}
                //if (cbbPacking.Text != "Z23C1" && cbbPacking.Text != "Z23C2" && cbbPacking.Text != "Z08C1")
                //{
                //    MessageBox.Show("该停车位不是Z23C1/Z23C2/Z08C1。");
                //    return;
                //}
                //if (GetParkStatus(cbbPacking.Text.Trim()) != "220")
                //{
                //    MessageBox.Show("该停车位不是激光扫描完成状态。");
                //    return;
                //}
                //if (curCarType != 101 && curCarType != 103)
                //{
                //    MessageBox.Show("该车类型不是社会车。");
                //    return;
                //}

                //string sqlText = " SELECT IS_LINE_EXIT_TO_CAR FROM  UACS_PARKING_STATUS WHERE PARKING_NO ='" + cbbPacking.Text.Trim() + "'";

                //using (IDataReader rdr = DBHelper.ExecuteReader(sqlText))
                //{
                //    if (rdr.Read())
                //    {
                //        if (rdr["IS_LINE_EXIT_TO_CAR"] != DBNull.Value)
                //        {
                //            flag = Convert.ToInt32(rdr["IS_LINE_EXIT_TO_CAR"]);
                //        }
                //    }
                //}
                //if (flag == 1)
                //{
                //    MessageBoxButtons btn = MessageBoxButtons.OKCancel;
                //    DialogResult dr = MessageBox.Show("确定要对" + cbbPacking.Text.Trim() + "进行直上社会车吗？", "操作提示", btn, MessageBoxIcon.Asterisk);
                //    if (dr == DialogResult.OK)
                //    {
                //        TagDP.SetData("EV_PARKING_LINE_EXIT_TO_CAR_LASEREND_END", cbbPacking.Text.Trim());
                //        ParkClassLibrary.HMILogger.WriteLog(btnUnitToCar.Text, "直上社会车：" + cbbPacking.Text, ParkClassLibrary.LogLevel.Info, this.Text);
                //    }
                //}
                //else
                //{
                //    MessageBox.Show("直上社会车标记不为1。");
                //    return;
                //}

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //木架卷选卷
        private void btnWoodenCar_Click(object sender, EventArgs e)
        {
            string carType;
            string isWoodenCar;
            if (GetStowageDetail2())
            {
                MessageBox.Show("车辆已经配载材料，请先做车离。");
                return;
            }
            SelectCoilForm selectCoilF = new SelectCoilForm();
            selectCoilF.CarType = "200";
            //string sqltext = @"select CAR_TYPE,ISWOODENCAR from UACS_TRUCK_STOWAGE where STOWAGE_ID = '" + txtStowageID.Text.Trim() + "'";
            //using (IDataReader rdr = DBHelper.ExecuteReader(sqltext))
            //{
            //    if (rdr.Read())
            //    {
            //        carType = rdr["CAR_TYPE"].ToString();
            //        isWoodenCar = rdr["ISWOODENCAR"].ToString();
            //        selectCoilF.CarType = carType;
            //        if (carType=="101"&& isWoodenCar=="1")
            //        {
            //            selectCoilF.CarType = "200";
            //        }
            //    }
            //}

            if (cbbPacking.Text.Trim() != "请选择" && cbbPacking.Text.Trim() != "" && cbbPacking.Text.Trim().Contains('A'))
            {
                selectCoilF.ParkNO = cbbPacking.Text.Trim();
                selectCoilF.TransferValue += selectCoilF_TransferValue;
                selectCoilF.GrooveNum = txtGrooveNum.Text.Trim();
                //selectCoilF.GrooveTotal = txtSelectGoove.Text.Trim();

                if (txtCarNo.Text.Trim().Length > 3)
                    selectCoilF.CarNO = txtCarNo.Text.Trim();
                else
                {
                    MessageBox.Show("车牌号信息不正确！");
                    return;
                }
                selectCoilF.ShowDialog();
            }
            else
            {
                MessageBox.Show("车位号信息不正确！");
            }
        }

        //作业暂停
        private void WorkStop(string parkNO, string CarNo)
        {
            try
            {
                if (parkNO == "")
                {
                    MessageBox.Show("请选择停车位。");
                    return;
                }
                if (CarNo.Equals(""))
                {
                    MessageBox.Show("车位无车，不能开始。");
                    return;
                }

                if (parkNO != "请选择" || parkNO != "")
                {
                    TagDP.SetData("EV_NEW_PARKING_JOB_PAUSE", parkNO);
                    HMILogger.WriteLog(btnOperatePause.Text, "作业暂停：" + parkNO, LogLevel.Info, this.Text);
                    btnOperateStrat.Enabled = false;
                    //label_Tip.Text = "框架车配载偏差值过大，配载拦截，请做车离！";
                    //panel_Tip.Visible = true;
                    //}
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message + "\r\n" + er.StackTrace);
            }
        }

        /// <summary>
        /// 判断配载模型是否为双排卷配载
        /// </summary>
        private bool JudgeDoubleRow(string AreaNo, DataGridView dgv)
        {
            int LastValue = 0;
            int CurrentValue = 0;
            if (AreaNo.Trim() == "T4" || AreaNo.Trim() == "T5")
            {
                if ((string)dgv.Rows[0].Cells["GROOVE_ACT_Y"].FormattedValue != "")
                {
                    LastValue = Convert.ToInt32((string)dgv.Rows[0].Cells["GROOVE_ACT_Y"].FormattedValue);
                }
                foreach (DataGridViewRow r in dgv.Rows)
                {
                    if ((string)r.Cells["GROOVE_ACT_Y"].FormattedValue != "")
                    {
                        CurrentValue = Convert.ToInt32((string)r.Cells["GROOVE_ACT_Y"].FormattedValue);
                        if (CurrentValue != LastValue)
                        {
                            return true;
                        }
                        LastValue = CurrentValue;
                    }
                }
                return false;
            }
            else
            {
                if ((string)dgv.Rows[0].Cells["GROOVE_ACT_X"].FormattedValue != "")
                {
                    LastValue = Convert.ToInt32((string)dgv.Rows[0].Cells["GROOVE_ACT_X"].FormattedValue);
                }
                foreach (DataGridViewRow r in dgv.Rows)
                {
                    if ((string)r.Cells["GROOVE_ACT_X"].FormattedValue != "")
                    {
                        CurrentValue = Convert.ToInt32((string)r.Cells["GROOVE_ACT_X"].FormattedValue);
                        if (CurrentValue != LastValue)
                        {
                            return true;
                        }
                        LastValue = CurrentValue;
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// 配载拦截
        /// </summary>
        private void Stowage_intercept(string AreaNo, string ParkingNO, string CarkNO, DataGridView DGV)
        {
            try
            {
                btnOperateStrat.Enabled = true;
                //panel_Tip.Visible = false;
                if (curCarType == 100 && txtCarNo.Text.Trim() != "")
                {
                    int Intercept_Value = 0;
                    string Intercept_PackingNo = String.Empty;
                    string Intercept_Tag = "CenterDev_" + ParkingNO;
                    List<string> lstAdress = new List<string>();
                    lstAdress.Clear();
                    lstAdress.Add(Intercept_Tag);
                    arrTagAdress = lstAdress.ToArray<string>();
                    readTags();
                    Intercept_PackingNo = get_value(Intercept_Tag);
                    if (Intercept_PackingNo.Trim() != "")
                    {
                        Intercept_Value = Convert.ToInt32(Intercept_PackingNo.Trim());
                        //GetOperateStatus(cbbPacking.Text.Trim());
                        if (Intercept_Value > 150)
                        {
                            WorkStop(ParkingNO, CarkNO);

                        }
                        else if (Intercept_Value > 130 && Intercept_Value <= 150)
                        {
                            if (JudgeDoubleRow(AreaNo, DGV))
                            {
                                WorkStop(ParkingNO, CarkNO);
                            }
                        }
                    }
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void cbbPacking_TextChanged(object sender, EventArgs e)
        {
            cmbArea.Text = GetAreaByBay();
        }

        private void btnReissue_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(DialogResult.OK == MessageBox.Show("确定需要装满补发吗？", "操作提示", MessageBoxButtons.OKCancel)))
                {
                    return;
                }
                string isLoaded = "";
                string sqlText = @"SELECT ISLOADED FROM UACS_PARKING_WORK_STATUS WHERE PARKING_NO = '" + cbbPacking.Text.Trim() + "'";
                using (IDataReader rdr = DBHelper.ExecuteReader(sqlText))
                {
                    while (rdr.Read())
                    {
                        if (rdr["ISLOADED"] != DBNull.Value)
                        {
                            isLoaded = rdr["ISLOADED"].ToString();
                        }
                    }
                }
                if (cbbPacking.Text.Trim() != "请选择" && cbbPacking.Text.Trim() != "" && cbbPacking.Text.Trim().Contains('A') && txtStowageID.Text.Trim() != "" && isLoaded == "0")
                {
                    string tagReissue = cbbPacking.Text.Trim() + "|" + txtStowageID.Text.Trim() + "|1|O|";
                    TagDP.SetData("EV_NEW_PARKING_SEND_STOWAGE_TO_MES", tagReissue);
                    HMILogger.WriteLog(btnReissue.Text, "补发：" + cbbPacking.Text.Trim(), LogLevel.Info, this.Text);
                    MessageBox.Show("装满补发完成！");
                }
                else
                {
                    MessageBox.Show("该停车位未空车车到位或者未配载，不能补发");
                    return;
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message + "\r\n" + er.StackTrace);
            }
        }
    }
}
