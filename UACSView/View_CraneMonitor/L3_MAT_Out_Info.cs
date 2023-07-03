using System;
using System.Data;
using System.Windows.Forms;
using Baosight.iSuperframe.Forms;
using UACSDAL;

namespace UACSView
{
    /// <summary>
    /// L3配料计划
    /// </summary>
    public partial class L3_MAT_Out_Info : FormBase
    {
        DataTable dt = new DataTable();
        bool hasSetColumn = false;
        private static Baosight.iSuperframe.Common.IDBHelper DBHelper = null;
        public L3_MAT_Out_Info()
        {
            InitializeComponent();
            DBHelper = Baosight.iSuperframe.Common.DataBase.DBFactory.GetHelper("ZJDB0");//平台连接数据库的Text
        }

        #region 事件
        /// <summary>
        /// 数据加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void L3_MAT_Out_Info_Load(object sender, EventArgs e)
        {
            try
            {
                //设置背景色
                //this.panel1.BackColor = UACSDAL.ColorSln.FormBgColor;
                //this.panel2.BackColor = UACSDAL.ColorSln.FormBgColor;
                //绑定下拉框
                //BindCombox();
                //
                this.dateTimePicker1_recTime.Value = DateTime.Now.AddDays(-1);

                dataGridView1.DataSource = getCraneOrderData(true);
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        /// <summary>
        /// 查询按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                //this.tabControl1.SelectedTab = this.tabPage1;
                //getCraneOrderData();
                dataGridView1.DataSource = getCraneOrderData(false);
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

       
        #endregion

        #region 方法
        /// <summary>
        /// 绑定下拉框数据
        /// </summary>
        private void BindCombox()
        {
            //CraneOrderImpl craneOrderImpl = new CraneOrderImpl();
            ////绑定跨号
            //DataTable dtBayNo2 = craneOrderImpl.GetBayNo(true);
            //bindCombox(this.comboBox_bayNo, dtBayNo2, true);
            ////绑定吊运状态
            //dicCmdStatus = craneOrderImpl.GetCodeValueDicByCodeId("CMD_STATUS", false);
            //DataTable dtCmdStatus2 = craneOrderImpl.GetCodeValueByCodeId("CMD_STATUS", true);
            //bindCombox(this.comboBox_cmdStatus, dtCmdStatus2, true);
            ////绑定分配标记
            //dicFlagDispat = craneOrderImpl.GetCodeValueDicByCodeId("FLAG_DISPAT", false);
            ////绑定执行类型
            //dicDelFlag = craneOrderImpl.GetCodeValueDicByCodeId("DEL_FLAG", false);
            ////绑定指令类型
            //dicOrderType = craneOrderImpl.GetCodeValueDicByCodeId("ORDER_TYPE", false);
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="isLoad">是否是初始化 true=是初始化，false=不是初始化</param>
        private DataTable getCraneOrderData(bool isLoad)
        {
            DataTable dtResult = InitDataTable(dataGridView1);
            string planNo = this.textPLAN_NO.Text.Trim();  //计划号
            string recTime1 = this.dateTimePicker1_recTime.Value.ToString("yyyyMMdd000000");  //开始时间
            string recTime2 = this.dateTimePicker2_recTime.Value.ToString("yyyyMMdd235959");  //结束时间
            string sqlText = @"SELECT WORK_SEQ_NO, OPER_FLAG, PLAN_NO, BOF_NO, CAR_NO, MAT_CODE_1, '' AS MAT_CNAME_1, WEIGHT_1, MAT_CODE_2,  '' AS MAT_CNAME_2, WEIGHT_2, MAT_CODE_3, '' AS MAT_CNAME_3, WEIGHT_3, MAT_CODE_4,  '' AS MAT_CNAME_4, WEIGHT_4, MAT_CODE_5,  '' AS MAT_CNAME_5, WEIGHT_5, MAT_CODE_6,  '' AS MAT_CNAME_6, WEIGHT_6, MAT_CODE_7,  '' AS MAT_CNAME_7, WEIGHT_7, MAT_CODE_8,  '' AS MAT_CNAME_8, WEIGHT_8, MAT_CODE_9,  '' AS MAT_CNAME_9, WEIGHT_9, MAT_CODE_10,  '' AS MAT_CNAME_10, WEIGHT_10, PLAN_STATUS, REC_TIME, UPD_TIME, CYCLE_COUNT, MAT_NET_WT, WT_TIME FROM UACSAPP.UACS_L3_MAT_OUT_INFO ";
            //sqlText += "LEFT JOIN UACS_L3_MAT_INFO C ON C.MAT_CODE = A.MAT_CODE ";
            sqlText += "WHERE REC_TIME > '{0}' and REC_TIME < '{1}' ";
            sqlText = string.Format(sqlText, recTime1, recTime2);           

            if (!isLoad)
            {
                if (!string.IsNullOrEmpty(planNo))
                {
                    sqlText = string.Format("{0} and PLAN_NO LIKE '%{1}%' ", sqlText, planNo);
                }
                //按 NO>流水号>记录时间>更新时间 降序
                sqlText += " ORDER BY WORK_SEQ_NO DESC,PLAN_NO DESC,REC_TIME DESC,UPD_TIME DESC ";
            }
            else
            {
                //初次加载时默认查询倒序30条数据（仅初始化时用）
                sqlText = @"SELECT WORK_SEQ_NO, OPER_FLAG, PLAN_NO, BOF_NO, CAR_NO, MAT_CODE_1, '' AS MAT_CNAME_1, WEIGHT_1, MAT_CODE_2,  '' AS MAT_CNAME_2, WEIGHT_2, MAT_CODE_3, '' AS MAT_CNAME_3, WEIGHT_3, MAT_CODE_4,  '' AS MAT_CNAME_4, WEIGHT_4, MAT_CODE_5,  '' AS MAT_CNAME_5, WEIGHT_5, MAT_CODE_6,  '' AS MAT_CNAME_6, WEIGHT_6, MAT_CODE_7,  '' AS MAT_CNAME_7, WEIGHT_7, MAT_CODE_8,  '' AS MAT_CNAME_8, WEIGHT_8, MAT_CODE_9,  '' AS MAT_CNAME_9, WEIGHT_9, MAT_CODE_10,  '' AS MAT_CNAME_10, WEIGHT_10, PLAN_STATUS, REC_TIME, UPD_TIME, CYCLE_COUNT, MAT_NET_WT, WT_TIME
                            FROM (
                            SELECT ROW_NUMBER() OVER(ORDER BY WORK_SEQ_NO DESC,OPER_FLAG DESC,REC_TIME DESC,UPD_TIME DESC) AS ROWNUM,
                            WORK_SEQ_NO, OPER_FLAG, PLAN_NO, BOF_NO, CAR_NO, MAT_CODE_1, '' AS MAT_CNAME_1, WEIGHT_1, MAT_CODE_2,  '' AS MAT_CNAME_2, WEIGHT_2, MAT_CODE_3, '' AS MAT_CNAME_3, WEIGHT_3, MAT_CODE_4,  '' AS MAT_CNAME_4, WEIGHT_4, MAT_CODE_5,  '' AS MAT_CNAME_5, WEIGHT_5, MAT_CODE_6,  '' AS MAT_CNAME_6, WEIGHT_6, MAT_CODE_7,  '' AS MAT_CNAME_7, WEIGHT_7, MAT_CODE_8,  '' AS MAT_CNAME_8, WEIGHT_8, MAT_CODE_9,  '' AS MAT_CNAME_9, WEIGHT_9, MAT_CODE_10,  '' AS MAT_CNAME_10, WEIGHT_10, PLAN_STATUS, REC_TIME, UPD_TIME, CYCLE_COUNT, MAT_NET_WT, WT_TIME FROM UACSAPP.UACS_L3_MAT_OUT_INFO 
                            ) a 
                            WHERE ROWNUM > 0 and ROWNUM <=30";
            }



            DataTable tbL3_MAT_OUT_INFO = new DataTable();
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
                    //tbL3_MAT_OUT_INFO.Load(rdr);
                }
            }
            DataTable dtMAT_INFO = GetMAT_INFO();

            foreach (DataRow dataRow in tbL3_MAT_OUT_INFO.Rows)
            {

                if (!string.IsNullOrEmpty(dataRow["MAT_CODE_1"].ToString()))
                {
                    var MAT_CNAME = dataRow["MAT_CNAME_1"].ToString();
                    foreach (DataRow matRow in dtMAT_INFO.Rows)
                    {
                        if (!string.IsNullOrEmpty(dataRow["MAT_CODE_1"].ToString()) && dataRow["MAT_CODE_1"].ToString().Equals(matRow["MAT_CODE"].ToString()))
                        {
                            MAT_CNAME = matRow["MAT_CNAME"].ToString();
                        }
                    }
                    dtResult.Rows.Add(dataRow["PLAN_NO"].ToString(), dataRow["CAR_NO"].ToString(), dataRow["MAT_CODE_1"].ToString(), MAT_CNAME, dataRow["WEIGHT_1"].ToString(), dataRow["REC_TIME"].ToString());
                }
                if (!string.IsNullOrEmpty(dataRow["MAT_CODE_2"].ToString()))
                {
                    var MAT_CNAME = dataRow["MAT_CNAME_2"].ToString();
                    foreach (DataRow matRow in dtMAT_INFO.Rows)
                    {
                        if (!string.IsNullOrEmpty(dataRow["MAT_CODE_2"].ToString()) && dataRow["MAT_CODE_2"].ToString().Equals(matRow["MAT_CODE"].ToString()))
                        {
                            MAT_CNAME = matRow["MAT_CNAME"].ToString();
                        }
                    }
                    dtResult.Rows.Add(dataRow["PLAN_NO"].ToString(), dataRow["CAR_NO"].ToString(), dataRow["MAT_CODE_2"].ToString(), MAT_CNAME, dataRow["WEIGHT_2"].ToString(), dataRow["REC_TIME"].ToString());
                }
                if (!string.IsNullOrEmpty(dataRow["MAT_CODE_3"].ToString()))
                {
                    var MAT_CNAME = dataRow["MAT_CNAME_3"].ToString();
                    foreach (DataRow matRow in dtMAT_INFO.Rows)
                    {
                        if (!string.IsNullOrEmpty(dataRow["MAT_CODE_3"].ToString()) && dataRow["MAT_CODE_3"].ToString().Equals(matRow["MAT_CODE"].ToString()))
                        {
                            MAT_CNAME = matRow["MAT_CNAME"].ToString();
                        }
                    }
                    dtResult.Rows.Add(dataRow["PLAN_NO"].ToString(), dataRow["CAR_NO"].ToString(), dataRow["MAT_CODE_3"].ToString(), MAT_CNAME, dataRow["WEIGHT_3"].ToString(), dataRow["REC_TIME"].ToString());
                }
                if (!string.IsNullOrEmpty(dataRow["MAT_CODE_4"].ToString()))
                {
                    var MAT_CNAME = dataRow["MAT_CNAME_4"].ToString();
                    foreach (DataRow matRow in dtMAT_INFO.Rows)
                    {
                        if (!string.IsNullOrEmpty(dataRow["MAT_CODE_4"].ToString()) && dataRow["MAT_CODE_4"].ToString().Equals(matRow["MAT_CODE"].ToString()))
                        {
                            MAT_CNAME = matRow["MAT_CNAME"].ToString();
                        }
                    }
                    dtResult.Rows.Add(dataRow["PLAN_NO"].ToString(), dataRow["CAR_NO"].ToString(), dataRow["MAT_CODE_4"].ToString(), MAT_CNAME, dataRow["WEIGHT_4"].ToString(), dataRow["REC_TIME"].ToString());
                }
                if (!string.IsNullOrEmpty(dataRow["MAT_CODE_5"].ToString()))
                {
                    var MAT_CNAME = dataRow["MAT_CNAME_5"].ToString();
                    foreach (DataRow matRow in dtMAT_INFO.Rows)
                    {
                        if (!string.IsNullOrEmpty(dataRow["MAT_CODE_5"].ToString()) && dataRow["MAT_CODE_5"].ToString().Equals(matRow["MAT_CODE"].ToString()))
                        {
                            MAT_CNAME = matRow["MAT_CNAME"].ToString();
                        }
                    }
                    dtResult.Rows.Add(dataRow["PLAN_NO"].ToString(), dataRow["CAR_NO"].ToString(), dataRow["MAT_CODE_5"].ToString(), MAT_CNAME, dataRow["WEIGHT_5"].ToString(), dataRow["REC_TIME"].ToString());
                }
                if (!string.IsNullOrEmpty(dataRow["MAT_CODE_6"].ToString()))
                {
                    var MAT_CNAME = dataRow["MAT_CNAME_6"].ToString();
                    foreach (DataRow matRow in dtMAT_INFO.Rows)
                    {
                        if (!string.IsNullOrEmpty(dataRow["MAT_CODE_6"].ToString()) && dataRow["MAT_CODE_6"].ToString().Equals(matRow["MAT_CODE"].ToString()))
                        {
                            MAT_CNAME = matRow["MAT_CNAME"].ToString();
                        }
                    }
                    dtResult.Rows.Add(dataRow["PLAN_NO"].ToString(), dataRow["CAR_NO"].ToString(), dataRow["MAT_CODE_6"].ToString(), MAT_CNAME, dataRow["WEIGHT_6"].ToString(), dataRow["REC_TIME"].ToString());
                }
                if (!string.IsNullOrEmpty(dataRow["MAT_CODE_7"].ToString()))
                {
                    var MAT_CNAME = dataRow["MAT_CNAME_7"].ToString();
                    foreach (DataRow matRow in dtMAT_INFO.Rows)
                    {
                        if (!string.IsNullOrEmpty(dataRow["MAT_CODE_7"].ToString()) && dataRow["MAT_CODE_7"].ToString().Equals(matRow["MAT_CODE"].ToString()))
                        {
                            MAT_CNAME = matRow["MAT_CNAME"].ToString();
                        }
                    }
                    dtResult.Rows.Add(dataRow["PLAN_NO"].ToString(), dataRow["CAR_NO"].ToString(), dataRow["MAT_CODE_7"].ToString(), MAT_CNAME, dataRow["WEIGHT_7"].ToString(), dataRow["REC_TIME"].ToString());
                }
                if (!string.IsNullOrEmpty(dataRow["MAT_CODE_8"].ToString()))
                {
                    var MAT_CNAME = dataRow["MAT_CNAME_8"].ToString();
                    foreach (DataRow matRow in dtMAT_INFO.Rows)
                    {
                        if (!string.IsNullOrEmpty(dataRow["MAT_CODE_8"].ToString()) && dataRow["MAT_CODE_8"].ToString().Equals(matRow["MAT_CODE"].ToString()))
                        {
                            MAT_CNAME = matRow["MAT_CNAME"].ToString();
                        }
                    }
                    dtResult.Rows.Add(dataRow["PLAN_NO"].ToString(), dataRow["CAR_NO"].ToString(), dataRow["MAT_CODE_8"].ToString(), MAT_CNAME, dataRow["WEIGHT_8"].ToString(), dataRow["REC_TIME"].ToString());
                }
                if (!string.IsNullOrEmpty(dataRow["MAT_CODE_9"].ToString()))
                {
                    var MAT_CNAME = dataRow["MAT_CNAME_9"].ToString();
                    foreach (DataRow matRow in dtMAT_INFO.Rows)
                    {
                        if (!string.IsNullOrEmpty(dataRow["MAT_CODE_9"].ToString()) && dataRow["MAT_CODE_9"].ToString().Equals(matRow["MAT_CODE"].ToString()))
                        {
                            MAT_CNAME = matRow["MAT_CNAME"].ToString();
                        }
                    }
                    dtResult.Rows.Add(dataRow["PLAN_NO"].ToString(), dataRow["CAR_NO"].ToString(), dataRow["MAT_CODE_9"].ToString(), MAT_CNAME, dataRow["WEIGHT_9"].ToString(), dataRow["REC_TIME"].ToString());
                }
                if (!string.IsNullOrEmpty(dataRow["MAT_CODE_10"].ToString()))
                {
                    var MAT_CNAME = dataRow["MAT_CNAME_10"].ToString();
                    foreach (DataRow matRow in dtMAT_INFO.Rows)
                    {
                        if (!string.IsNullOrEmpty(dataRow["MAT_CODE_10"].ToString()) && dataRow["MAT_CODE_10"].ToString().Equals(matRow["MAT_CODE"].ToString()))
                        {
                            MAT_CNAME = matRow["MAT_CNAME"].ToString();
                        }
                    }
                    dtResult.Rows.Add(dataRow["PLAN_NO"].ToString(), dataRow["CAR_NO"].ToString(), dataRow["MAT_CODE_10"].ToString(), MAT_CNAME, dataRow["WEIGHT_10"].ToString(), dataRow["REC_TIME"].ToString());
                }
            }

            return dtResult;
        }

        /// <summary>
        /// 获取废钢物料名
        /// </summary>
        /// <param name="showAll"></param>
        /// <returns></returns>
        public DataTable GetMAT_INFO()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("MAT_CODE");
            dt.Columns.Add("MAT_CNAME");
            //准备数据
            string sqlText = @"SELECT MAT_CODE,MAT_CNAME FROM UACSAPP.UACS_L3_MAT_INFO ";
            using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sqlText))
            {
                while (rdr.Read())
                {
                    DataRow dr = dt.NewRow();
                    dr["MAT_CODE"] = rdr["MAT_CODE"];
                    dr["MAT_CNAME"] = rdr["MAT_CNAME"];
                    dt.Rows.Add(dr);
                }
            }
            
            return dt;
        }

        /// <summary>
        /// 绑定下拉框
        /// </summary>
        /// <param name="control">下拉框控件</param>
        /// <param name="dt">数据源</param>
        /// <param name="showLastIndex">是否显示最后一条（通常用于查询条件中全部）</param>
        private void bindCombox(ComboBox control,DataTable dt, bool showLastIndex)
        {
            control.DataSource = dt;
            control.DisplayMember = "TypeName";
            control.ValueMember = "TypeValue";
            if (showLastIndex)
            {
                control.SelectedIndex = dt.Rows.Count - 1;
            }
        }
        #endregion

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

    }
}
