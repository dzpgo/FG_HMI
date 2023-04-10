using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Baosight.iSuperframe.Forms;
using Baosight.iSuperframe.TagService;

using ParkingControlLibrary;
using ParkClassLibrary;
using UACSParking;
using IBM.Data.DB2;
using UACSDAL;
using System.Xml;

namespace UACSView.View_Parking
{
    /// <summary>
    /// 装车配载（新）
    /// </summary>
    public partial class SelectCoilByL3FormNew : Form
    {
        #region 变量

        public event TransferValue TransferValue;
        private static Baosight.iSuperframe.Common.IDBHelper DBHelper = null;
        DataTable dt_selected = new DataTable();
        CheckBox checkbox;

        Baosight.iSuperframe.TagService.DataCollection<object> TagValues = new DataCollection<object>();
        private Baosight.iSuperframe.TagService.Controls.TagDataProvider tagDP = new Baosight.iSuperframe.TagService.Controls.TagDataProvider();

        #region 模型
        private string bayNO;
        private string parkNO;
        private string carNO;
        private string carType;
        private string grooveNum;
        private string grooveTotal;

        /// <summary>
        /// 车辆类型
        /// </summary>
        public string CarType
        {
            get { return carType; }
            set
            {
                carType = value;
            }
        }
        /// <summary>
        /// 车号
        /// </summary>
        public string CarNO
        {
            get { return carNO; }
            set { carNO = value; }
        }
        /// <summary>
        /// 停车位号
        /// </summary>
        public string ParkNO
        {
            get { return parkNO; }
            set { parkNO = value; }
        }
        /// <summary>
        /// 凹槽编号
        /// </summary>
        public string GrooveNum
        {
            get { return grooveNum; }
            set { grooveNum = value; }
        }
        /// <summary>
        /// 凹槽总计
        /// </summary>
        public string GrooveTotal
        {
            get { return grooveTotal; }
            set { grooveTotal = value; }
        }
        #endregion

        #endregion

        #region 初始化

        /// <summary>
        /// 构造函数
        /// </summary>
        public SelectCoilByL3FormNew()
        {
            InitializeComponent();
            DBHelper = Baosight.iSuperframe.Common.DataBase.DBFactory.GetHelper("ZJDB0");

            tagDP.ServiceName = "iplature";
            tagDP.AutoRegist = true;
            TagValues.Clear();
            //TagValues.Add("EV_NEW_PARKING_CARLEAVE", null);
            //社会车出库
            TagValues.Add("EV_NEW_PARKING_MDL_OUT_CAL_JUDGE", null);
            //框架车出库
            TagValues.Add("EV_PARKING_MDL_OUT_CAL_START", null);
            tagDP.Attach(TagValues);
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectCoilByL3FormNew_Load(object sender, EventArgs e)
        {
            //加载扫描数据
            RefreshHMILaserOutData();
            //加载L3装车要求
            InitialL3StowagePlan(carNO);
            //绑定停车位
            BindCmbParKing(this.ParkNO);
            //绑定车号
            BindCmbCar(this.ParkNO);
            //绑定车辆类型
            BindCmbCarType();
            //绑定车头方向
            BindCarDrection();            
            //绑定装料模式
            BindCmbWordMode();
            //绑定扫描行车
            BindCmbScanCar();

            //窗体大小
            this.Size = new Size(1440, 950);
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="strCarNo"></param>
        private void InitialL3StowagePlan(string strCarNo)
        {
            // 查询L3装车要求
            string strPlanNo = "";
            string strTaskNo = "";
            if (QueryL3StowagePlan(strCarNo, out strPlanNo, out strTaskNo))
            {
                tbCAR_NO.Text = strCarNo;
                tbPLAN_NO.Text = strPlanNo;
                tbL3_TASK_NO.Text = strTaskNo;

                // 根据L3配载计划，查询装车材料信息
                DataTable dataTable = BindMatStockByL3Stowage2(false, strCarNo, strPlanNo, strTaskNo);
                this.dataGridView1.DataSource = dataTable;
            }
            else
            {
                tbCAR_NO.Text = "无";
                tbPLAN_NO.Text = "无";
                tbL3_TASK_NO.Text = "无";
                // 初始数据
                DataTable dataTable = BindMatStockByL3Stowage2(true, "", "", "");
                this.dataGridView1.DataSource = dataTable;
            }
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="datagridView"></param>
        /// <returns></returns>
        private DataTable InitDataTable(DataGridView datagridView)
        {
            DataTable dataTable = new DataTable();
            foreach (DataGridViewColumn dgvColumn in datagridView.Columns)
            {
                DataColumn dtColumn = new DataColumn();
                if (!dgvColumn.GetType().Equals(typeof(DataGridViewCheckBoxColumn)))
                {
                    dtColumn.ColumnName = dgvColumn.Name.ToUpper();
                    dtColumn.DataType = typeof(String);
                    dataTable.Columns.Add(dtColumn);
                }
                else
                {
                    dtColumn.ColumnName = dgvColumn.Name.ToUpper();
                    //dtColumn.DataType = typeof(bool);
                    dataTable.Columns.Add(dtColumn);
                }
            }

            return dataTable;
        }


        #endregion

        #region 控件点击

        /// <summary>
        /// 点击查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            UpdateDgvRow(false);
            this.dataGridView1.DataSource = BindMatStockByL3Stowage2(false, tbCAR_NO.Text, tbPLAN_NO.Text, tbL3_TASK_NO.Text);
        }

        /// <summary>
        /// 提交Tag
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            var parkingNo = parkNO;   //停车位号
            var truckNo = carNO;     //车号
            var planN0 = "";      //计划号
            try
            {
                //框架车号不能为空
                if (truckNo == "")
                {
                    MessageBox.Show("车号不能为空");
                    return;
                }
                //车位号不能为空
                if (parkingNo == "" || parkingNo == "请选择")
                {
                    MessageBox.Show("该车找不到对应的停车位号");
                    return;
                }

                //停车位号|CaoNO|处理号|模型计算次数|配载图ID-卷|卷
                string treatmentNo = "";
                string stowageNo = "";
                int currengMdlCalId = 0;
                long LASER_ACTION_COUNT = 0;
                //string sqlText = @"SELECT TREATMENT_NO, STOWAGE_ID, MDL_CAL_ID, LASER_ACTION_COUNT FROM UACS_PARKING_WORK_STATUS where PARKING_NO = '{0}'";
                string sqlText = @"SELECT TREATMENT_NO, STOWAGE_ID, CAR_NO, CAR_TYPE, LASER_ACTION_COUNT FROM UACS_PARKING_WORK_STATUS where PARKING_NO = '{0}'";
                sqlText = string.Format(sqlText, parkingNo);
                using (IDataReader rdr = DBHelper.ExecuteReader(sqlText))
                {
                    if (rdr.Read())
                    {
                        treatmentNo = rdr["TREATMENT_NO"].ToString();
                        LASER_ACTION_COUNT = Convert.ToInt64(string.IsNullOrEmpty(rdr["LASER_ACTION_COUNT"].ToString()) ? "0" : rdr["LASER_ACTION_COUNT"].ToString());
                        stowageNo = rdr["STOWAGE_ID"].ToString();
                        currengMdlCalId = Convert.ToInt32(LASER_ACTION_COUNT);
                        if (!string.IsNullOrEmpty(rdr["CAR_TYPE"].ToString()))
                        {
                            carType = rdr["CAR_TYPE"].ToString();  //车辆类型
                        }
                    }
                }

                //var myValue = string.Format("{0}|{1}|{2}", parkingNo, truckNo, planN0);
                string myValue = "";
                //模型计算次数
                int mdlCalId = currengMdlCalId + 1;

                foreach (DataGridViewRow dgvRow in dataGridView1.Rows)
                {
                    if (!string.IsNullOrEmpty(dgvRow.Cells["cbChoice"].Value.ToString()))
                    {
                        if (dgvRow.Cells["cbChoice"].Value.ToString().Equals("1"))
                        {
                            planN0 = dgvRow.Cells["GPlanNO"].Value.ToString();
                            break;
                        }
                    }
                }


                //更新社会车辆中间选卷数据到配载图的选卷完成中间数据里
                string shehuicheValue = "";
                for (int i = 0; i < dt_selected.Rows.Count; i++)
                {
                    if (i < 30)
                    {
                        string coilNO = dt_selected.Rows[i]["COIL_NO2"].ToString().Trim();
                        if (coilNO.Length != 0)
                        {
                            shehuicheValue += dt_selected.Rows[i]["GROOVE_ACT_X"].ToString();
                            shehuicheValue += "|";
                            shehuicheValue += dt_selected.Rows[i]["GROOVE_ACT_Y"].ToString();
                            shehuicheValue += "|";
                            shehuicheValue += dt_selected.Rows[i]["COIL_NO2"].ToString();
                            shehuicheValue += "|";
                            shehuicheValue += dt_selected.Rows[i]["GROOVE_ACT_Z"].ToString();
                            shehuicheValue += "|";
                            if (dt_selected.Rows[i]["GROOVEID"].ToString() == "")
                            {
                                shehuicheValue += i + 1;
                            }
                            else
                            {
                                shehuicheValue += dt_selected.Rows[i]["GROOVEID"].ToString();
                            }
                            shehuicheValue += "-";
                        }
                    }
                }

                //sqlText = @"UPDATE UACS_TRUCK_STOWAGE SET MD_COIL_READY = '{0}' WHERE STOWAGE_ID = {1} ";
                //sqlText = string.Format(sqlText, shehuicheValue, stowageNo);
                //DBHelper.ExecuteNonQuery(sqlText);                

                //更新模型计算次数
                sqlText = @"UPDATE UACS_PARKING_WORK_STATUS SET LASER_ACTION_COUNT = {0} where PARKING_NO = '{1}'";
                sqlText = string.Format(sqlText, mdlCalId, parkingNo);
                DBHelper.ExecuteNonQuery(sqlText);


                myValue = string.Format("{0}|{1}|{2}", parkingNo, truckNo, planN0);
                if (carType == "1" || carType == "2")
                {
                    tagDP.SetData("EV_PARKING_MDL_OUT_CAL_START", myValue);
                }
                MessageBox.Show("材料选择成功，自动行车准备作业，请注意安全！");
                this.Close();
            }
            catch (Exception er)
            {
                MessageBox.Show(string.Format("{0} {1}", er.TargetSite, er.ToString()));
                MessageBox.Show("车辆选择材料失败！");
            }


        }

        /// <summary>
        /// 单击选中dataGridView行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedCells.Count != 0)
            {
                //得到选中行的索引
                int intRow = dataGridView1.SelectedCells[0].RowIndex;
                //得到列的索引
                //int intColumn = dataGridView1.SelectedCells[0].ColumnIndex;
                //var sss = dataGridView1.Rows[intRow].Cells["cbChoice"].Value.ToString();
                //dataGridView1.Rows[intRow].Cells["cbChoice"].Value = 1;
                //var dddd = dataGridView1.Rows[intRow].Cells["cbChoice"].Value.ToString();
                ////得到选中行某列的值
                //string str = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                //MessageBox.Show(str);

                // 选中行
                foreach (DataGridViewRow dgvRow in dataGridView1.Rows)
                {
                    if (!string.IsNullOrEmpty(dgvRow.Cells["cbChoice"].Value.ToString()))
                    {
                        if (dgvRow.Index.Equals(intRow))
                        {
                            var isChoice = dgvRow.Cells["cbChoice"].Value.ToString();
                            if (isChoice.Equals("0"))
                            {
                                dgvRow.Cells["cbChoice"].Value = 1; //选中

                                UpdateDgvRow(
                                    true,
                                    dgvRow.Cells["GMatCode_1"].Value.ToString(),
                                    dgvRow.Cells["GMatCode_2"].Value.ToString(),
                                    dgvRow.Cells["GMatCode_3"].Value.ToString(),
                                    dgvRow.Cells["GMatCode_4"].Value.ToString(),
                                    dgvRow.Cells["GMatCode_5"].Value.ToString(),
                                    dgvRow.Cells["GMatCode_6"].Value.ToString(),
                                    dgvRow.Cells["GMatCode_7"].Value.ToString(),
                                    dgvRow.Cells["GMatCode_8"].Value.ToString(),
                                    dgvRow.Cells["GMatCode_9"].Value.ToString(),
                                    dgvRow.Cells["GMatCode_10"].Value.ToString(),
                                    dgvRow.Cells["GWeight_1"].Value.ToString(),
                                    dgvRow.Cells["GWeight_2"].Value.ToString(),
                                    dgvRow.Cells["GWeight_3"].Value.ToString(),
                                    dgvRow.Cells["GWeight_4"].Value.ToString(),
                                    dgvRow.Cells["GWeight_5"].Value.ToString(),
                                    dgvRow.Cells["GWeight_6"].Value.ToString(),
                                    dgvRow.Cells["GWeight_7"].Value.ToString(),
                                    dgvRow.Cells["GWeight_8"].Value.ToString(),
                                    dgvRow.Cells["GWeight_9"].Value.ToString(),
                                    dgvRow.Cells["GWeight_10"].Value.ToString()
                                    );
                            }
                            else if (isChoice.Equals("1"))
                            {
                                dgvRow.Cells["cbChoice"].Value = 0; //未选中
                                //UpdateDgvRow(false);
                            }
                            else
                            {
                                dgvRow.Cells["cbChoice"].Value = 0; //未选中
                            }
                        }
                        else
                        {
                            if (dgvRow.Cells["cbChoice"].Value.ToString().Equals("1"))
                            {
                                dgvRow.Cells["cbChoice"].Value = 0; //未选中
                            }

                        }
                    }
                }

                bool IsNoChoice = true;
                foreach (DataGridViewRow dgvRow in dataGridView1.Rows)
                {
                    if (!string.IsNullOrEmpty(dgvRow.Cells["cbChoice"].Value.ToString()))
                    {
                        if (dgvRow.Cells["cbChoice"].Value.ToString().Equals("1"))
                        {
                            IsNoChoice = false;
                            break;
                        }
                    }
                }
                if (IsNoChoice)
                {
                    UpdateDgvRow(false);
                }

            }
        }

        #endregion

        #region 数据处理

        /// <summary>
        /// 查询车号对应的L3装车要求号
        /// </summary>
        /// <param name="strCarNo">车号</param>
        /// <param name="strPlanNo">计划号</param>
        /// <param name="strTaskNo">任务号</param>
        /// <returns></returns>
        private bool QueryL3StowagePlan(string strCarNo, out string strPlanNo, out string strTaskNo)
        {
            bool bFounded = false;

            strPlanNo = "";
            strTaskNo = "";

            // 查询车号对应的L3装车要求号
            string sqlText = @"SELECT * FROM UACS_TRUCK_STOWAGE_L3 where TRUCK_NO like '{0}'";
            sqlText = string.Format(sqlText, strCarNo);
            using (IDataReader rdr = DBHelper.ExecuteReader(sqlText))
            {
                if (rdr.Read())
                {
                    strPlanNo = rdr["L3_PLAN_NO"].ToString();
                    strTaskNo = rdr["L3_TASK_NO"].ToString();

                    bFounded = true;
                }
            }

            return bFounded;
        }

        /// <summary>
        /// 绑定材料位置信息
        /// </summary>
        /// <param name="isLoad">是否是初始化 true=是初始化，false=不是初始化</param>
        /// <param name="strCarNo">配置车号</param>
        /// <param name="strPlanNo">配载计划号</param>
        /// <param name="strTaskNo">配载任务号</param>
        /// <returns></returns>
        private DataTable BindMatStockByL3Stowage2(bool isLoad, string strCarNo, string strPlanNo, string strTaskNo)
        {
            DataTable dtResult = InitDataTable(dataGridView1);
            //bool bAddedWhere = false;
            //string subSql = "SELECT MAT_NO FROM UACS_TRUCK_STOWAGE_L3";

            // 转换
            if (strPlanNo == "无")
                strPlanNo = "";
            if (strTaskNo == "无")
                strTaskNo = "";
            if (strCarNo == "无")
                strCarNo = "";

            //if (strCarNo.Length == 0 && strPlanNo.Length == 0 && strTaskNo.Length == 0)
            //    return dtResult;

            DataTable tbL3_MAT_OUT_INFO = new DataTable("UACS_L3_MAT_OUT_INFO");
            string sqlText_All = @" SELECT 0 AS CHECK_COLUMN, WORK_SEQ_NO, OPER_FLAG, PLAN_NO, BOF_NO, CAR_NO, MAT_CODE_1, WEIGHT_1, MAT_CODE_2, WEIGHT_2, MAT_CODE_3, WEIGHT_3,
            MAT_CODE_4, WEIGHT_4, MAT_CODE_5, WEIGHT_5, MAT_CODE_6, WEIGHT_6, MAT_CODE_7, WEIGHT_7, MAT_CODE_8, WEIGHT_8, MAT_CODE_9, WEIGHT_9, MAT_CODE_10, 
            WEIGHT_10, PLAN_STATUS, REC_TIME, UPD_TIME, CYCLE_COUNT, MAT_NET_WT, WT_TIME FROM UACSAPP.UACS_L3_MAT_OUT_INFO 
            WHERE 1 = 1 ";
            if (!isLoad)
            {
                if (!string.IsNullOrEmpty(strCarNo))
                {
                    sqlText_All += "AND CAR_NO ='" + strCarNo + "'";
                }
                if (!string.IsNullOrEmpty(strPlanNo))
                {
                    sqlText_All += "AND PLAN_NO ='" + strPlanNo + "'";
                }
                if (!string.IsNullOrEmpty(strTaskNo))
                {
                    //sqlText_All += "AND L3_PLAN_NO like '%" + strPlanNo + "%'";
                }

                //按 计划号>流水号>记录时间>更新时间 降序
                sqlText_All += " order by WORK_SEQ_NO DESC,PLAN_NO DESC,REC_TIME DESC,UPD_TIME DESC ";
            }
            else
            {
                //初次加载时默认查询倒序30条数据（仅初始化时用）
                sqlText_All = @"SELECT 0 AS CHECK_COLUMN, WORK_SEQ_NO, OPER_FLAG, PLAN_NO, BOF_NO, CAR_NO, MAT_CODE_1, WEIGHT_1, MAT_CODE_2, WEIGHT_2, MAT_CODE_3, WEIGHT_3,
                                MAT_CODE_4, WEIGHT_4, MAT_CODE_5, WEIGHT_5, MAT_CODE_6, WEIGHT_6, MAT_CODE_7, WEIGHT_7, MAT_CODE_8, WEIGHT_8, MAT_CODE_9, WEIGHT_9, MAT_CODE_10, 
                                WEIGHT_10, PLAN_STATUS, REC_TIME, UPD_TIME, CYCLE_COUNT, MAT_NET_WT, WT_TIME
                                FROM (
                                SELECT ROW_NUMBER() OVER(ORDER BY WORK_SEQ_NO DESC,PLAN_NO DESC,REC_TIME DESC,UPD_TIME DESC) AS ROWNUM,
                                0 AS CHECK_COLUMN, WORK_SEQ_NO, OPER_FLAG, PLAN_NO, BOF_NO, CAR_NO, MAT_CODE_1, WEIGHT_1, MAT_CODE_2, WEIGHT_2, MAT_CODE_3, WEIGHT_3,
                                MAT_CODE_4, WEIGHT_4, MAT_CODE_5, WEIGHT_5, MAT_CODE_6, WEIGHT_6, MAT_CODE_7, WEIGHT_7, MAT_CODE_8, WEIGHT_8, MAT_CODE_9, WEIGHT_9, MAT_CODE_10, 
                                WEIGHT_10, PLAN_STATUS, REC_TIME, UPD_TIME, CYCLE_COUNT, MAT_NET_WT, WT_TIME FROM UACSAPP.UACS_L3_MAT_OUT_INFO 
                                ) a 
                                WHERE ROWNUM > 0 and ROWNUM <=30";
            }




            // 执行
            using (IDataReader rdr = DBHelper.ExecuteReader(sqlText_All))
            {
                DataColumn col;
                DataRow row;
                for (int i = 0; i < rdr.FieldCount; i++)
                {
                    col = new DataColumn();
                    col.ColumnName = rdr.GetName(i);
                    col.DataType = rdr.GetFieldType(i);
                    tbL3_MAT_OUT_INFO.Columns.Add(col);
                }

                while (rdr.Read())
                {

                    row = tbL3_MAT_OUT_INFO.NewRow();
                    for (int i = 0; i < rdr.FieldCount; i++)
                    {
                        row[i] = rdr[i];
                    }
                    tbL3_MAT_OUT_INFO.Rows.Add(row);
                }
            }



            foreach (DataRow dataRow in tbL3_MAT_OUT_INFO.Rows)
            {
                dtResult.Rows.Add(
                    0, /*cbChoice*/
                    dataRow["PLAN_NO"].ToString(),
                    dataRow["CAR_NO"].ToString(),
                    dataRow["MAT_CODE_1"].ToString(),
                    dataRow["WEIGHT_1"].ToString(),
                    dataRow["MAT_CODE_2"].ToString(),
                    dataRow["WEIGHT_2"].ToString(),
                    dataRow["MAT_CODE_3"].ToString(),
                    dataRow["WEIGHT_3"].ToString(),
                    dataRow["MAT_CODE_4"].ToString(),
                    dataRow["WEIGHT_4"].ToString(),
                    dataRow["MAT_CODE_5"].ToString(),
                    dataRow["WEIGHT_5"].ToString(),
                    dataRow["MAT_CODE_6"].ToString(),
                    dataRow["WEIGHT_6"].ToString(),
                    dataRow["MAT_CODE_7"].ToString(),
                    dataRow["WEIGHT_7"].ToString(),
                    dataRow["MAT_CODE_8"].ToString(),
                    dataRow["WEIGHT_8"].ToString(),
                    dataRow["MAT_CODE_9"].ToString(),
                    dataRow["WEIGHT_9"].ToString(),
                    dataRow["MAT_CODE_10"].ToString(),
                    dataRow["WEIGHT_10"].ToString()
                    );
            }

            return dtResult;
        }

        /// <summary>
        /// 更新物料与总量
        /// </summary>
        /// <param name="isNulls">是否更新，true更新 or false设置空</param>
        /// <param name="GMatCode_1">物料1</param>
        /// <param name="GMatCode_2">物料2</param>
        /// <param name="GMatCode_3">物料3</param>
        /// <param name="GMatCode_4">物料4</param>
        /// <param name="GMatCode_5">物料5</param>
        /// <param name="GMatCode_6">物料6</param>
        /// <param name="GMatCode_7">物料7</param>
        /// <param name="GMatCode_8">物料8</param>
        /// <param name="GMatCode_9">物料9</param>
        /// <param name="GMatCode_10">物料10</param>
        /// <param name="GWeight_1">重量1</param>
        /// <param name="GWeight_2">重量2</param>
        /// <param name="GWeight_3">重量3</param>
        /// <param name="GWeight_4">重量4</param>
        /// <param name="GWeight_5">重量5</param>
        /// <param name="GWeight_6">重量6</param>
        /// <param name="GWeight_7">重量7</param>
        /// <param name="GWeight_8">重量8</param>
        /// <param name="GWeight_9">重量9</param>
        /// <param name="GWeight_10">重量10</param>
        private void UpdateDgvRow(bool isNulls, string GMatCode_1, string GMatCode_2, string GMatCode_3, string GMatCode_4, string GMatCode_5, string GMatCode_6, string GMatCode_7, string GMatCode_8, string GMatCode_9, string GMatCode_10, string GWeight_1, string GWeight_2, string GWeight_3, string GWeight_4, string GWeight_5, string GWeight_6, string GWeight_7, string GWeight_8, string GWeight_9, string GWeight_10)
        {
            if (isNulls)
            {
                #region 查询物料名


                DataTable tbL3_MAT_INFO = new DataTable("UACS_L3_MAT_INFO");

                string sqlText = @" SELECT MAT_CODE, BASE_RESOURCE, MAT_CNAME, MAT_TYPE, REC_TIME FROM UACSAPP.UACS_L3_MAT_INFO ";
                // 执行
                using (IDataReader rdr = DBHelper.ExecuteReader(sqlText))
                {
                    DataColumn col;
                    DataRow row;
                    for (int i = 0; i < rdr.FieldCount; i++)
                    {
                        col = new DataColumn();
                        col.ColumnName = rdr.GetName(i);
                        col.DataType = rdr.GetFieldType(i);
                        tbL3_MAT_INFO.Columns.Add(col);
                    }

                    while (rdr.Read())
                    {

                        row = tbL3_MAT_INFO.NewRow();
                        for (int i = 0; i < rdr.FieldCount; i++)
                        {
                            row[i] = rdr[i];
                        }
                        tbL3_MAT_INFO.Rows.Add(row);
                    }
                }

                #region 更新物料名

                foreach (DataRow L3Row in tbL3_MAT_INFO.Rows)
                {
                    if (!string.IsNullOrEmpty(GMatCode_1) && L3Row["MAT_CODE"].ToString().Equals(GMatCode_1))
                    {
                        tbMatCode_1.Text = L3Row["MAT_CNAME"].ToString();
                    }
                    if (!string.IsNullOrEmpty(GMatCode_2) && L3Row["MAT_CODE"].ToString().Equals(GMatCode_2))
                    {
                        tbMatCode_2.Text = L3Row["MAT_CNAME"].ToString();
                    }
                    if (!string.IsNullOrEmpty(GMatCode_3) && L3Row["MAT_CODE"].ToString().Equals(GMatCode_3))
                    {
                        tbMatCode_3.Text = L3Row["MAT_CNAME"].ToString();
                    }
                    if (!string.IsNullOrEmpty(GMatCode_4) && L3Row["MAT_CODE"].ToString().Equals(GMatCode_4))
                    {
                        tbMatCode_4.Text = L3Row["MAT_CNAME"].ToString();
                    }
                    if (!string.IsNullOrEmpty(GMatCode_5) && L3Row["MAT_CODE"].ToString().Equals(GMatCode_5))
                    {
                        tbMatCode_5.Text = L3Row["MAT_CNAME"].ToString();
                    }
                    if (!string.IsNullOrEmpty(GMatCode_6) && L3Row["MAT_CODE"].ToString().Equals(GMatCode_6))
                    {
                        tbMatCode_6.Text = L3Row["MAT_CNAME"].ToString();
                    }
                    if (!string.IsNullOrEmpty(GMatCode_7) && L3Row["MAT_CODE"].ToString().Equals(GMatCode_7))
                    {
                        tbMatCode_7.Text = L3Row["MAT_CNAME"].ToString();
                    }
                    if (!string.IsNullOrEmpty(GMatCode_8) && L3Row["MAT_CODE"].ToString().Equals(GMatCode_8))
                    {
                        tbMatCode_8.Text = L3Row["MAT_CNAME"].ToString();
                    }
                    if (!string.IsNullOrEmpty(GMatCode_9) && L3Row["MAT_CODE"].ToString().Equals(GMatCode_9))
                    {
                        tbMatCode_9.Text = L3Row["MAT_CNAME"].ToString();
                    }
                    if (!string.IsNullOrEmpty(GMatCode_10) && L3Row["MAT_CODE"].ToString().Equals(GMatCode_10))
                    {
                        tbMatCode_10.Text = L3Row["MAT_CNAME"].ToString();
                    }
                }

                #endregion

                #region 物料名称清空
                if (string.IsNullOrEmpty(GMatCode_1))
                    tbMatCode_1.Text = "";
                if (string.IsNullOrEmpty(GMatCode_2))
                    tbMatCode_2.Text = "";
                if (string.IsNullOrEmpty(GMatCode_3))
                    tbMatCode_3.Text = "";
                if (string.IsNullOrEmpty(GMatCode_4))
                    tbMatCode_4.Text = "";
                if (string.IsNullOrEmpty(GMatCode_5))
                    tbMatCode_5.Text = "";
                if (string.IsNullOrEmpty(GMatCode_6))
                    tbMatCode_6.Text = "";
                if (string.IsNullOrEmpty(GMatCode_7))
                    tbMatCode_7.Text = "";
                if (string.IsNullOrEmpty(GMatCode_8))
                    tbMatCode_8.Text = "";
                if (string.IsNullOrEmpty(GMatCode_9))
                    tbMatCode_9.Text = "";
                if (string.IsNullOrEmpty(GMatCode_10))
                    tbMatCode_10.Text = ""; 
                #endregion

                #endregion

                #region 更新重量
                if (!string.IsNullOrEmpty(GWeight_1))
                {
                    tbWeight_1.Text = GWeight_1;
                }
                else
                {
                    tbWeight_1.Text = "";
                }
                if (!string.IsNullOrEmpty(GWeight_2))
                {
                    tbWeight_2.Text = GWeight_2;
                }
                else
                {
                    tbWeight_2.Text = "";
                }
                if (!string.IsNullOrEmpty(GWeight_3))
                {
                    tbWeight_3.Text = GWeight_3;
                }
                else
                {
                    tbWeight_3.Text = "";
                }
                if (!string.IsNullOrEmpty(GWeight_4))
                {
                    tbWeight_4.Text = GWeight_4;
                }
                else
                {
                    tbWeight_4.Text = "";
                }
                if (!string.IsNullOrEmpty(GWeight_5))
                {
                    tbWeight_5.Text = GWeight_5;
                }
                else
                {
                    tbWeight_5.Text = "";
                }
                if (!string.IsNullOrEmpty(GWeight_6))
                {
                    tbWeight_6.Text = GWeight_6;
                }
                else
                {
                    tbWeight_6.Text = "";
                }
                if (!string.IsNullOrEmpty(GWeight_7))
                {
                    tbWeight_7.Text = GWeight_7;
                }
                else
                {
                    tbWeight_7.Text = "";
                }
                if (!string.IsNullOrEmpty(GWeight_8))
                {
                    tbWeight_8.Text = GWeight_8;
                }
                else
                {
                    tbWeight_8.Text = "";
                }
                if (!string.IsNullOrEmpty(GWeight_9))
                {
                    tbWeight_9.Text = GWeight_9;
                }
                else
                {
                    tbWeight_9.Text = "";
                }
                if (!string.IsNullOrEmpty(GWeight_10))
                {
                    tbWeight_10.Text = GWeight_10;
                }
                else
                {
                    tbWeight_10.Text = "";
                }

                #endregion

                #region 计算总重量

                tbWeight_Sum.Text = "";

                var w_1 = Convert.ToInt32(!string.IsNullOrEmpty(GWeight_1) ? GWeight_1 : "0");
                var w_2 = Convert.ToInt32(!string.IsNullOrEmpty(GWeight_2) ? GWeight_2 : "0");
                var w_3 = Convert.ToInt32(!string.IsNullOrEmpty(GWeight_3) ? GWeight_3 : "0");
                var w_4 = Convert.ToInt32(!string.IsNullOrEmpty(GWeight_4) ? GWeight_4 : "0");
                var w_5 = Convert.ToInt32(!string.IsNullOrEmpty(GWeight_5) ? GWeight_5 : "0");
                var w_6 = Convert.ToInt32(!string.IsNullOrEmpty(GWeight_6) ? GWeight_6 : "0");
                var w_7 = Convert.ToInt32(!string.IsNullOrEmpty(GWeight_7) ? GWeight_7 : "0");
                var w_8 = Convert.ToInt32(!string.IsNullOrEmpty(GWeight_8) ? GWeight_8 : "0");
                var w_9 = Convert.ToInt32(!string.IsNullOrEmpty(GWeight_9) ? GWeight_9 : "0");
                var w_10 = Convert.ToInt32(!string.IsNullOrEmpty(GWeight_10) ? GWeight_10 : "0");

                var sum = Convert.ToString(Convert.ToInt32(!string.IsNullOrEmpty(tbWeight_Sum.Text) ? tbWeight_Sum.Text : "0") + w_1 + w_2 + w_3 + w_4 + w_5 + w_6 + w_7 + w_8 + w_9 + w_10);
                tbWeight_Sum.Text = sum;

                #endregion
            }
            else
            {
                tbMatCode_1.Text = "";
                tbMatCode_2.Text = "";
                tbMatCode_3.Text = "";
                tbMatCode_4.Text = "";
                tbMatCode_5.Text = "";
                tbMatCode_6.Text = "";
                tbMatCode_7.Text = "";
                tbMatCode_8.Text = "";
                tbMatCode_9.Text = "";
                tbMatCode_10.Text = "";
                tbWeight_1.Text = "";
                tbWeight_2.Text = "";
                tbWeight_3.Text = "";
                tbWeight_4.Text = "";
                tbWeight_5.Text = "";
                tbWeight_6.Text = "";
                tbWeight_7.Text = "";
                tbWeight_8.Text = "";
                tbWeight_9.Text = "";
                tbWeight_10.Text = "";
                tbWeight_Sum.Text = "";
            }



        }

        /// <summary>
        /// 更新物料控件和总量控件为空
        /// </summary>
        /// <param name="isNulls">false设置空</param>
        private void UpdateDgvRow(bool isNulls = false)
        {
            UpdateDgvRow(isNulls, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);
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
                string truckNo = carNO;      //车号
                if (truckNo == "")
                {
                    return bResut;
                }

                parkingNo = parkNO;
                //先获取车头方向配置表里的车长方向坐标轴和趋势
                string AXES_CAR_LENGTH = "";
                string TREND_TO_TAIL = "";
                string sqlText_head = @"SELECT AXES_CAR_LENGTH, TREND_TO_TAIL FROM UACS_HEAD_POSITION_CONFIG WHERE HEAD_POSTION IN ";
                sqlText_head += "(SELECT HEAD_POSTION FROM UACS_PARKING_WORK_STATUS WHERE PARKING_NO = '{0}') AND PARKING_NO = '{0}'";
                sqlText_head = string.Format(sqlText_head, parkingNo);
                using (IDataReader rdr = DBHelper.ExecuteReader(sqlText_head))
                {
                    if (rdr.Read())
                    {
                        AXES_CAR_LENGTH = rdr["AXES_CAR_LENGTH"].ToString();
                        TREND_TO_TAIL = rdr["TREND_TO_TAIL"].ToString();
                    }
                }

                string sqlorder = "";
                if (AXES_CAR_LENGTH == "X" && TREND_TO_TAIL == "INC")
                {
                    sqlorder = "ORDER BY GROOVE_ACT_X ";
                }
                else if (AXES_CAR_LENGTH == "X" && TREND_TO_TAIL == "DES")
                {
                    sqlorder = "ORDER BY GROOVE_ACT_X DESC";
                }
                else if (AXES_CAR_LENGTH == "Y" && TREND_TO_TAIL == "INC")
                {
                    sqlorder = "ORDER BY GROOVE_ACT_Y ";
                }
                else if (AXES_CAR_LENGTH == "Y" && TREND_TO_TAIL == "DES")
                {
                    sqlorder = "ORDER BY GROOVE_ACT_Y DESC";
                }

                //从停车位表里取出处理号和激光扫描次数
                string sqlText = @"SELECT TREATMENT_NO, LASER_ACTION_COUNT FROM UACS_PARKING_WORK_STATUS WHERE PARKING_NO='{0}' ";
                sqlText = string.Format(sqlText, parkingNo);
                using (IDataReader rdr = DBHelper.ExecuteReader(sqlText))
                {
                    while (rdr.Read())
                    {
                        TREATMENT_NO = rdr["TREATMENT_NO"].ToString();
                        if (!string.IsNullOrEmpty(rdr["LASER_ACTION_COUNT"].ToString()))
                            LASER_ACTION_COUNT = Convert.ToInt64(rdr["LASER_ACTION_COUNT"].ToString());
                    }
                }

                string GROOVE_ACT_X = "";
                string GROOVE_ACT_Y = "";
                string GROOVE_ACT_Z = "";
                string GROOVEID = "";
                dt_selected.Clear();

                //从出库激光表里取出激光扫描数据
                sqlText = @"SELECT GROOVE_ACT_X, GROOVE_ACT_Y, GROOVE_ACT_Z, GROOVEID FROM UACS_LASER_OUT ";
                sqlText += "WHERE TREATMENT_NO = '{0}' AND LASER_ACTION_COUNT = '{1}' ";
                sqlText += sqlorder;
                sqlText = string.Format(sqlText, TREATMENT_NO, LASER_ACTION_COUNT);

                using (IDataReader rdr = DBHelper.ExecuteReader(sqlText))
                {
                    while (rdr.Read())
                    {
                        GROOVE_ACT_X = rdr["GROOVE_ACT_X"].ToString();
                        GROOVE_ACT_Y = rdr["GROOVE_ACT_Y"].ToString();
                        GROOVE_ACT_Z = rdr["GROOVE_ACT_Z"].ToString();
                        GROOVEID = rdr["GROOVEID"].ToString();
                        dt_selected.Rows.Add("0", GROOVEID, "", "", "", "", GROOVE_ACT_X, GROOVE_ACT_Y, GROOVE_ACT_Z);
                    }

                    if ((carType == "100" || carType == "102" || carType == "106") && Convert.ToInt32(grooveTotal) >= 1)
                    {
                        for (int i = 1; i <= Convert.ToInt32(grooveNum) - Convert.ToInt32(grooveTotal); i++)
                        {

                            dt_selected.Rows.Add("0", "", "", "", "", "", 999999, 999999, 999999);
                        }
                    }
                }
                //this.dataGridView2.DataSource = dt_selected;

                bResut = true;
            }
            catch (Exception er)
            {
                MessageBox.Show(string.Format("{0} {1}", er.TargetSite, er.ToString()));
            }

            return bResut;
        }

        #endregion


        #region 绑定下拉框

        /// <summary>
        /// 绑定停车位
        /// </summary>
        /// <param name="ParkNO">停车位号</param>
        private void BindCmbParKing(string ParkNO)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("NAME");
            DataRow dr = dt.NewRow();
            dr = dt.NewRow();
            dr["ID"] = "1";
            dr["NAME"] = "A1";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["ID"] = "2";
            dr["NAME"] = "A2";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["ID"] = "3";
            dr["NAME"] = "A3";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["ID"] = "4";
            dr["NAME"] = "A4";
            dt.Rows.Add(dr);
            //绑定数据
            cmb_ParKingNO.ValueMember = "ID";
            cmb_ParKingNO.DisplayMember = "NAME";
            cmb_ParKingNO.DataSource = dt;

            int selectedIndex = 0;
            //设置默认值
            if (ParkNO.Equals("A1"))
                selectedIndex = 0;
            if (ParkNO.Equals("A2"))
                selectedIndex = 1;
            if (ParkNO.Equals("A3"))
                selectedIndex = 2;
            if (ParkNO.Equals("A4"))
                selectedIndex = 3;

            this.cmb_ParKingNO.SelectedIndex = selectedIndex;
        }

        /// <summary>
        /// 绑定车辆
        /// </summary>
        /// <param name="ParkNO">停车位号</param>
        private void BindCmbCar(string ParkNO)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("ID");
                dt.Columns.Add("NAME");
                DataRow dr = dt.NewRow();
                int IndexID = 0;
                var HopperNo = ""; //料槽号（车号）
                if (!string.IsNullOrEmpty(ParkNO))
                {
                    //L3配料间空料斗与工位对照
                    string sqlText0 = @"SELECT ARRIVE_FLAG, HOPPER_NO,POSITION_NO,LOAD_FLAG,STATUS FROM UACS_L3_MAT_CRA_RELATION WHERE POSITION_NO='{0}' ";
                    //sqlText0 += " AND LOAD_FLAG='1' ";  //1:满斗/0:空斗
                    sqlText0 = string.Format(sqlText0, ParkNO);
                    using (IDataReader rdr = DBHelper.ExecuteReader(sqlText0))
                    {
                        //只取匹配到的第一个
                        if (rdr.Read())
                        {
                            if (!string.IsNullOrEmpty(rdr["HOPPER_NO"].ToString()))
                            {
                                HopperNo = rdr["HOPPER_NO"].ToString();//料槽号（车号）
                            }
                        }
                    }
                }
                //查车辆号
                string sqlText = @"SELECT FRAME_TYPE_NO FROM UACS_TRUCK_FRAME_DEFINE ";
                using (IDataReader rdr = DBHelper.ExecuteReader(sqlText))
                {
                    while (rdr.Read())
                    {
                        dr = dt.NewRow();
                        dr["ID"] = rdr["FRAME_TYPE_NO"].ToString();
                        dr["NAME"] = rdr["FRAME_TYPE_NO"].ToString();
                        dt.Rows.Add(dr);
                    }
                }
                dr = dt.NewRow();
                dr["ID"] = "SG99";
                dr["NAME"] = "SG99";
                dt.Rows.Add(dr);
                //绑定数据
                cmb_CarNo.ValueMember = "ID";
                cmb_CarNo.DisplayMember = "NAME";
                cmb_CarNo.DataSource = dt;
                //根据text值选中项
                this.cmb_CarNo.SelectedIndex = string.IsNullOrEmpty(HopperNo) ? 0 : this.cmb_CarNo.FindString(HopperNo) >= 0 ? this.cmb_CarNo.FindString(HopperNo) : 0;

            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 绑定车辆类型
        /// </summary>
        private void BindCmbCarType()
        {
            DataTable dt = new DataTable();
            DataColumn dc1 = new DataColumn("ID");
            DataColumn dc2 = new DataColumn("NAME");
            dt.Columns.Add(dc1);
            dt.Columns.Add(dc2);
            DataRow dr1 = dt.NewRow();
            //dr1["ID"] = "1";
            //dr1["NAME"] = "社会车";
            DataRow dr2 = dt.NewRow();
            dr2["ID"] = "2";
            dr2["NAME"] = "装料车";
            //dt.Rows.Add(dr1);
            dt.Rows.Add(dr2);
            //绑定数据
            cmbCarType.ValueMember = "ID";
            cmbCarType.DisplayMember = "NAME";
            cmbCarType.DataSource = dt;
            //设置默认值
            this.cmbCarType.SelectedIndex = 0;
        }

        /// <summary>
        /// 绑定车头方向
        /// </summary>
        private void BindCarDrection()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TypeValue");
            dt.Columns.Add("TypeName");
            DataRow dr = dt.NewRow();
            dr = dt.NewRow();
            dr["TypeValue"] = "E";
            dr["TypeName"] = "东";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["TypeValue"] = "W";
            dr["TypeName"] = "西";
            dt.Rows.Add(dr);

            //dr = dt.NewRow();
            //dr["TypeValue"] = "S";
            //dr["TypeName"] = "南";
            //dt.Rows.Add(dr);
            //dr = dt.NewRow();
            //dr["TypeValue"] = "N";
            //dr["TypeName"] = "北";
            //dt.Rows.Add(dr);

            this.txtDirection.DisplayMember = "TypeName";
            this.txtDirection.ValueMember = "TypeValue";
            //绑定列表下拉框数据
            this.txtDirection.DataSource = dt;
            //设置默认值
            this.txtDirection.SelectedIndex = 1;
        }

        /// <summary>
        /// 绑定装料模式
        /// </summary>
        private void BindCmbWordMode()
        {
            DataTable dt = new DataTable();
            DataColumn dc1 = new DataColumn("ID");
            DataColumn dc2 = new DataColumn("NAME");
            dt.Columns.Add(dc1);
            dt.Columns.Add(dc2);
            DataRow dr1 = dt.NewRow();
            dr1["ID"] = "1";
            dr1["NAME"] = "模式一";
            DataRow dr2 = dt.NewRow();
            dr2["ID"] = "2";
            dr2["NAME"] = "模式二";
            DataRow dr3 = dt.NewRow();
            dr3["ID"] = "3";
            dr3["NAME"] = "模式三";
            DataRow dr4 = dt.NewRow();
            dr4["ID"] = "4";
            dr4["NAME"] = "模式四";
            dt.Rows.Add(dr1);
            dt.Rows.Add(dr2);
            dt.Rows.Add(dr3);
            dt.Rows.Add(dr4);

            cmbWordMode.ValueMember = "ID";
            cmbWordMode.DisplayMember = "NAME";
            //绑定数据
            cmbWordMode.DataSource = dt;
            //设置默认值
            this.cmbWordMode.SelectedIndex = 0;
        }


        /// <summary>
        /// 绑定扫描行车
        /// </summary>
        private void BindCmbScanCar()
        {
            DataTable dt = new DataTable();
            DataColumn dc1 = new DataColumn("ID");
            DataColumn dc2 = new DataColumn("NAME");
            dt.Columns.Add(dc1);
            dt.Columns.Add(dc2);
            DataRow dr1 = dt.NewRow();
            dr1["ID"] = "1";
            dr1["NAME"] = "1号行车";
            DataRow dr2 = dt.NewRow();
            dr2["ID"] = "2";
            dr2["NAME"] = "2号行车";
            DataRow dr3 = dt.NewRow();
            dr3["ID"] = "3";
            dr3["NAME"] = "3号行车";
            DataRow dr4 = dt.NewRow();
            dr4["ID"] = "4";
            dr4["NAME"] = "4号行车";
            dt.Rows.Add(dr1);
            dt.Rows.Add(dr2);
            dt.Rows.Add(dr3);
            dt.Rows.Add(dr4);

            cmbScanCar.ValueMember = "ID";
            cmbScanCar.DisplayMember = "NAME";
            //绑定数据
            cmbScanCar.DataSource = dt;
            var index = 0;
            if (!string.IsNullOrEmpty(ParkNO))
            {
                if (ParkNO.Equals("A1"))
                    index = 0;
                if (ParkNO.Equals("A2"))
                    index = 1;
                if (ParkNO.Equals("A3"))
                    index = 2;
                if (ParkNO.Equals("A4"))
                    index = 3;
            }            
            //设置默认值
            this.cmbScanCar.SelectedIndex = index;
        }

        #endregion

        #region 值发生更改时触发事件

        /// <summary>
        /// 车辆类型 值发生更改时触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbCarType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCarType.SelectedValue != null && (int.Parse(cmbCarType.SelectedValue.ToString()) == 100 || int.Parse(cmbCarType.SelectedValue.ToString()) == 106))
            {
                txtDirection.Enabled = true;
                //txtDirection.Text = "南";
                carType = "框架车";
                //button1.Enabled = true;
            }
            else if (cmbCarType.SelectedValue != null && int.Parse(cmbCarType.SelectedValue.ToString()) == 102)
            {
                txtDirection.Enabled = true;
                //txtDirection.Text = "南";
                carType = "ALL";
                //button1.Enabled = true;
            }
            else if (cmbCarType.SelectedValue != null && int.Parse(cmbCarType.SelectedValue.ToString()) == 2)
            {
                txtDirection.Enabled = true;
                //txtDirection.Text = "南";
                carType = "装料车";
                //button1.Enabled = true;
            }
            else
            {
                txtDirection.Enabled = true;
                txtDirection.Text = "";
                carType = "社会车";
                //button1.Enabled = true;
            }
        }
        /// <summary>
        /// 车号 值发生更改时触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmb_CarNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            var dd = this.cmb_CarNo.Text;
            if (this.cmb_CarNo.FindString(this.cmb_CarNo.Text) < 0)
            {
                MessageBox.Show("车号不存在！");
            }
            this.cmb_CarNo.SelectedIndex = string.IsNullOrEmpty(this.cmb_CarNo.Text) ? 0 : this.cmb_CarNo.FindString(this.cmb_CarNo.Text) >= 0 ? this.cmb_CarNo.FindString(this.cmb_CarNo.Text) : 0;
        }

        #endregion

        /// <summary>
        /// 车到位和装车配载合二为一测试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTestSubmit_Click(object sender, EventArgs e)
        {
            #region 条件
            if (!cmb_ParKingNO.Text.ToString().Contains('A') || cmb_ParKingNO.Text.ToString().Trim() == "请选择")
            {
                MessageBox.Show("请先选择停车位！！", "提示");
                this.Close();
                return;
            }
            if (!string.IsNullOrEmpty(ParkNO) && !string.IsNullOrEmpty(CarNO))
            {
                MessageBox.Show("车位已经有车！！", "提示");
                return;
            }
            if (!txtDirection.Text.Trim().Equals("东") && !txtDirection.Text.Trim().Equals("西"))
            {
                MessageBox.Show("请输入车头方向！", "提示");
                return;
            }
            if (cmb_CarNo.Text.Length < 4)
            {
                MessageBox.Show("请输入四位以上的车牌号！", "提示");
                return;
            }
            var cbChoiceData = string.Empty;
            if (dataGridView1.SelectedCells.Count <= 0)
            {
                MessageBox.Show("请先选择计划！！", "提示");
                this.Close();
                return;
            }
            else
            {
                // 选中行
                foreach (DataGridViewRow dgvRow in dataGridView1.Rows)
                {
                    if (!string.IsNullOrEmpty(dgvRow.Cells["cbChoice"].Value.ToString()))
                    {
                        var isChoice = dgvRow.Cells["cbChoice"].Value.ToString();
                        if (isChoice.Equals("1"))
                        {
                            cbChoiceData = dgvRow.Cells["GPlanNO"].Value.ToString();
                        }
                    }
                }

                if (string.IsNullOrEmpty(cbChoiceData))
                {
                    MessageBox.Show("请先选择计划！！", "提示");
                    return;
                }
            } 
            #endregion

            //确认提示
            MessageBoxButtons btn = MessageBoxButtons.OKCancel;
            DialogResult drmsg = MessageBox.Show("确认是否装车配载？", "提示", btn, MessageBoxIcon.Asterisk);
            if (drmsg == DialogResult.OK)
            {
                int currengMdlCalId = 0;
                long LASER_ACTION_COUNT = 0;
                string sqlText = @"SELECT TREATMENT_NO, STOWAGE_ID, CAR_NO, CAR_TYPE, LASER_ACTION_COUNT FROM UACS_PARKING_WORK_STATUS where PARKING_NO = '{0}'";
                sqlText = string.Format(sqlText, cmb_ParKingNO.Text.ToString());
                using (IDataReader rdr = DBHelper.ExecuteReader(sqlText))
                {
                    if (rdr.Read())
                    {
                        LASER_ACTION_COUNT = Convert.ToInt64(string.IsNullOrEmpty(rdr["LASER_ACTION_COUNT"].ToString()) ? "0" : rdr["LASER_ACTION_COUNT"].ToString());
                        currengMdlCalId = Convert.ToInt32(LASER_ACTION_COUNT);
                    }
                }
                //模型计算次数
                int mdlCalId = currengMdlCalId + 1;
                //更新模型计算次数
                sqlText = @"UPDATE UACS_PARKING_WORK_STATUS SET LASER_ACTION_COUNT = {0} where PARKING_NO = '{1}'";
                sqlText = string.Format(sqlText, mdlCalId, cmb_ParKingNO.Text.ToString());
                DBHelper.ExecuteNonQuery(sqlText);

                //框架车
                if (carType == "框架车" || carType == "装料车" || carType == "2")
                {
                    if (cmb_CarNo.Text.Trim() != "" || txtDirection.Text.Trim() != "" || txtFlag.Text.Trim() != "" || cmb_ParKingNO.Text.Trim() != "")
                    {
                        //操作人 | 日期 | 班次 | 班组 | 停车位 | 车号 | 空重标记 | 车头朝向 | 装料模式 | 扫描行车 | 车辆类型 | 停车位类型
                        //车头位置(东：E 西：W 南：S 北：N)
                        StringBuilder sb = new StringBuilder("HMI");  //操作人
                        sb.Append("|");
                        sb.Append(DateTime.Now.ToString("yyyyMMddHHmmss")); //日期
                        sb.Append("|");
                        sb.Append("1");  //班次
                        sb.Append("|");
                        sb.Append("1");  //班组
                        sb.Append("|");
                        sb.Append(cmb_ParKingNO.Text.Trim());   //停车位
                        sb.Append("|");
                        sb.Append(cmb_CarNo.Text.ToUpper().Trim());   //车号
                        sb.Append("|");
                        if (txtFlag.Text.Trim().Equals("空"))
                        {
                            sb.Append("0");  //空重标记 0：空
                        }
                        else
                        {
                            sb.Append("1");  //空重标记 1：满
                        }
                        //sb.Append(carFlag.Trim());
                        sb.Append("|");
                        // sb.Append(carDirection.Trim());
                        sb.Append(txtDirection.SelectedValue.ToString().Trim());   //车头朝向
                        sb.Append("|");
                        //sb.Append("90");
                        sb.Append(cmbWordMode.SelectedValue.ToString().Trim());   //装料模式
                        sb.Append("|");
                        sb.Append("0"); //扫描行车
                        sb.Append("|");
                        string carTypeValue = cmbCarType.SelectedValue.ToString().Trim();
                        sb.Append(carTypeValue); //车辆类型
                        sb.Append("|");
                        sb.Append("0");   //停车位类型
                        sb.Append("|");
                        sb.Append(cbChoiceData);   //计划号

                        tagDP.SetData("EV_PARKING_CARARRIVE", sb.ToString());
                        DialogResult dr = MessageBox.Show("车到位成功!", "提示", MessageBoxButtons.OK);
                        ParkClassLibrary.HMILogger.WriteLog("车到位", "车到位：" + sb.ToString(), ParkClassLibrary.LogLevel.Info, this.Text);
                        if (dr == DialogResult.OK)
                        {
                            this.Close();
                            return;
                        }
                        else
                        {
                            this.Close();
                        }
                    }
                    else
                        MessageBox.Show("请填写完整");
                }
                else
                    MessageBox.Show("不存在该出库类型！");

            }
        }
    }
}
