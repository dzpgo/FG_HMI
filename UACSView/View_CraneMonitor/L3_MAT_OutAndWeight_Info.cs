using System;
using System.Data;
using UACSDAL;
using System.Windows.Forms;
using Baosight.iSuperframe.Forms;

namespace UACSView
{
    /// <summary>
    /// L3配送料计划
    /// </summary>
    public partial class L3_MAT_OutAndWeight_Info : FormBase
    {
        private static Baosight.iSuperframe.Common.IDBHelper DBHelper = null;
        bool hasSetColumn = false;
        public L3_MAT_OutAndWeight_Info()
        {
            InitializeComponent();
            DBHelper = Baosight.iSuperframe.Common.DataBase.DBFactory.GetHelper("ZJDB0");//平台连接数据库的Text
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void L3_MAT_OutAndWeight_Info_Load(object sender, EventArgs e)
        {
            this.ucPage1.CurrentPage = 1;
            this.ucPage1.PageSize = Convert.ToInt32(this.ucPage1.CboPageSize.Text);
            this.ucPage1.TotalPages = 1;
            this.ucPage1.ClickPageButtonEvent += ucPage1_ClickPageButtonEvent;
            this.ucPage1.ChangedPageSizeEvent += ucPage1_ChangedPageSizeEvent;
            this.ucPage1.JumpPageEvent += ucPage1_JumpPageEvent;

            this.ucPage2.CurrentPage = 1;
            this.ucPage2.PageSize = Convert.ToInt32(this.ucPage2.CboPageSize.Text);
            this.ucPage2.TotalPages = 1;
            this.ucPage2.ClickPageButtonEvent += ucPage2_ClickPageButtonEvent;
            this.ucPage2.ChangedPageSizeEvent += ucPage2_ChangedPageSizeEvent;
            this.ucPage2.JumpPageEvent += ucPage2_JumpPageEvent;
            //L3送料计划
            getL3MatWeightInfo(1);
            //L3配料计划
            getL3MatOutInfo(1);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            //L3送料计划
            getL3MatWeightInfo(1);
            //L3配料计划
            getL3MatOutInfo(1);
        }

        #region L3送料计划
        /// <summary>
        /// L3送料计划查询数据
        /// </summary>
        /// <param name="currentPage">当前页面</param>
        private void getL3MatWeightInfo(int currentPage)
        {
            string work_seqNo = this.textWORK_SEQ_NO.Text.Trim();  //计划号
            string recTime1 = this.dateTimePicker1_recTime.Value.ToString("yyyyMMdd000000");  //开始时间
            string recTime2 = this.dateTimePicker2_recTime.Value.ToString("yyyyMMdd235959");  //结束时间
            string sqlText = @"SELECT * FROM (SELECT COUNT(1) OVER () AS TotalRows,ROW_NUMBER() OVER () AS ROWNUM,tab.* FROM ( ";
            sqlText += @"SELECT A.WORK_SEQ_NO,A.TRUCK_NO AS TRUCK_NO2,A.MAT_PROD_CODE AS MAT_PROD_CODE2, C.MAT_CNAME AS MAT_CNAME2,A.MAT_WT AS MAT_WT2,A.REC_TIME AS REC_TIME2,A.UPD_TIME AS UPD_TIME2 FROM UACSAPP.UACS_L3_MAT_WEIGHT_INFO A ";
            sqlText += "LEFT JOIN UACS_L3_MAT_INFO C ON C.MAT_CODE = A.MAT_PROD_CODE ";
            sqlText += "WHERE A.REC_TIME > '{0}' and A.REC_TIME < '{1}' ";
            sqlText = string.Format(sqlText, recTime1, recTime2);
            if (!string.IsNullOrEmpty(work_seqNo))
            {
                sqlText = string.Format("{0} and A.WORK_SEQ_NO LIKE '%{1}%' ", sqlText, work_seqNo);
            }
            //按 NO>>记录时间>更新时间 降序
            sqlText += " ORDER BY A.WORK_SEQ_NO DESC,A.REC_TIME DESC,A.UPD_TIME DESC ";
            sqlText += " ) tab ) WHERE ROWNUM BETWEEN ((" + currentPage + " - 1) * " + this.ucPage1.PageSize + ") + 1 AND " + currentPage + " *  " + this.ucPage1.PageSize;
            DataTable dataTable = new DataTable();
            hasSetColumn = false;
            using (IDataReader rdr = DBHelper.ExecuteReader(sqlText))
            {
                while (rdr.Read())
                {
                    DataRow dr = dataTable.NewRow();
                    for (int i = 0; i < rdr.FieldCount; i++)
                    {
                        if (!hasSetColumn)
                        {
                            DataColumn dc = new DataColumn();
                            dc.ColumnName = rdr.GetName(i);
                            dataTable.Columns.Add(dc);
                        }
                        dr[i] = rdr[i];
                    }
                    hasSetColumn = true;
                    dataTable.Rows.Add(dr);
                }
            }
            dataGridView1.DataSource = GetDataPage(dataTable, currentPage);
        }
        #endregion

        #region L3配料计划
        /// <summary>
        /// L3配料计划查询数据
        /// </summary>
        /// <param name="currentPage">当前页面</param>
        private void getL3MatOutInfo(int currentPage)
        {
            DataTable dtResult = InitDataTable(dataGridView2);
            string planNo = this.textPLAN_NO.Text.Trim();  //计划号
            string recTime1 = this.dateTimePicker1_recTime.Value.ToString("yyyyMMdd000000");  //开始时间
            string recTime2 = this.dateTimePicker2_recTime.Value.ToString("yyyyMMdd235959");  //结束时间
            string sqlText = @"SELECT * FROM (SELECT COUNT(1) OVER () AS TotalRows2,ROW_NUMBER() OVER () AS ROWNUM2,tab.* FROM ( ";
            sqlText += @"SELECT WORK_SEQ_NO, OPER_FLAG, PLAN_NO, BOF_NO, CAR_NO, MAT_CODE_1, '' AS MAT_CNAME_1, WEIGHT_1, MAT_CODE_2,  '' AS MAT_CNAME_2, WEIGHT_2, MAT_CODE_3, '' AS MAT_CNAME_3, WEIGHT_3, MAT_CODE_4,  '' AS MAT_CNAME_4, WEIGHT_4, MAT_CODE_5,  '' AS MAT_CNAME_5, WEIGHT_5, MAT_CODE_6,  '' AS MAT_CNAME_6, WEIGHT_6, MAT_CODE_7,  '' AS MAT_CNAME_7, WEIGHT_7, MAT_CODE_8,  '' AS MAT_CNAME_8, WEIGHT_8, MAT_CODE_9,  '' AS MAT_CNAME_9, WEIGHT_9, MAT_CODE_10,  '' AS MAT_CNAME_10, WEIGHT_10, PLAN_STATUS, REC_TIME, UPD_TIME, CYCLE_COUNT, MAT_NET_WT, WT_TIME FROM UACSAPP.UACS_L3_MAT_OUT_INFO ";
            sqlText += "WHERE REC_TIME > '{0}' and REC_TIME < '{1}' ";
            sqlText = string.Format(sqlText, recTime1, recTime2);

            if (!string.IsNullOrEmpty(planNo))
            {
                sqlText = string.Format("{0} and PLAN_NO LIKE '%{1}%' ", sqlText, planNo);
            }
            //按 NO>流水号>记录时间>更新时间 降序
            sqlText += " ORDER BY WORK_SEQ_NO DESC,PLAN_NO DESC,REC_TIME DESC,UPD_TIME DESC ";
            sqlText += " ) tab ) WHERE ROWNUM BETWEEN ((" + currentPage + " - 1) * " + this.ucPage2.PageSize + ") + 1 AND " + currentPage + " *  " + this.ucPage2.PageSize;
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
                    dtResult.Rows.Add(dataRow["PLAN_NO"].ToString(), dataRow["ROWNUM2"].ToString(), dataRow["TotalRows2"].ToString(), dataRow["CAR_NO"].ToString(), dataRow["MAT_CODE_1"].ToString(), MAT_CNAME, dataRow["WEIGHT_1"].ToString(), dataRow["REC_TIME"].ToString());
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
                    dtResult.Rows.Add(dataRow["PLAN_NO"].ToString(), dataRow["ROWNUM2"].ToString(), dataRow["TotalRows2"].ToString(), dataRow["CAR_NO"].ToString(), dataRow["MAT_CODE_2"].ToString(), MAT_CNAME, dataRow["WEIGHT_2"].ToString(), dataRow["REC_TIME"].ToString());
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
                    dtResult.Rows.Add(dataRow["PLAN_NO"].ToString(), dataRow["ROWNUM2"].ToString(), dataRow["TotalRows2"].ToString(), dataRow["CAR_NO"].ToString(), dataRow["MAT_CODE_3"].ToString(), MAT_CNAME, dataRow["WEIGHT_3"].ToString(), dataRow["REC_TIME"].ToString());
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
                    dtResult.Rows.Add(dataRow["PLAN_NO"].ToString(), dataRow["ROWNUM2"].ToString(), dataRow["TotalRows2"].ToString(), dataRow["CAR_NO"].ToString(), dataRow["MAT_CODE_4"].ToString(), MAT_CNAME, dataRow["WEIGHT_4"].ToString(), dataRow["REC_TIME"].ToString());
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
                    dtResult.Rows.Add(dataRow["PLAN_NO"].ToString(), dataRow["ROWNUM2"].ToString(), dataRow["TotalRows2"].ToString(), dataRow["CAR_NO"].ToString(), dataRow["MAT_CODE_5"].ToString(), MAT_CNAME, dataRow["WEIGHT_5"].ToString(), dataRow["REC_TIME"].ToString());
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
                    dtResult.Rows.Add(dataRow["PLAN_NO"].ToString(), dataRow["ROWNUM2"].ToString(), dataRow["TotalRows2"].ToString(), dataRow["CAR_NO"].ToString(), dataRow["MAT_CODE_6"].ToString(), MAT_CNAME, dataRow["WEIGHT_6"].ToString(), dataRow["REC_TIME"].ToString());
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
                    dtResult.Rows.Add(dataRow["PLAN_NO"].ToString(), dataRow["ROWNUM2"].ToString(), dataRow["TotalRows2"].ToString(), dataRow["CAR_NO"].ToString(), dataRow["MAT_CODE_7"].ToString(), MAT_CNAME, dataRow["WEIGHT_7"].ToString(), dataRow["REC_TIME"].ToString());
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
                    dtResult.Rows.Add(dataRow["PLAN_NO"].ToString(), dataRow["ROWNUM2"].ToString(), dataRow["TotalRows2"].ToString(), dataRow["CAR_NO"].ToString(), dataRow["MAT_CODE_8"].ToString(), MAT_CNAME, dataRow["WEIGHT_8"].ToString(), dataRow["REC_TIME"].ToString());
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
                    dtResult.Rows.Add(dataRow["PLAN_NO"].ToString(), dataRow["ROWNUM2"].ToString(), dataRow["TotalRows2"].ToString(), dataRow["CAR_NO"].ToString(), dataRow["MAT_CODE_9"].ToString(), MAT_CNAME, dataRow["WEIGHT_9"].ToString(), dataRow["REC_TIME"].ToString());
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
                    dtResult.Rows.Add(dataRow["PLAN_NO"].ToString(), dataRow["ROWNUM2"].ToString(), dataRow["TotalRows2"].ToString(), dataRow["CAR_NO"].ToString(), dataRow["MAT_CODE_10"].ToString(), MAT_CNAME, dataRow["WEIGHT_10"].ToString(), dataRow["REC_TIME"].ToString());
                }
            }

            dataGridView2.DataSource = GetDataPage2(dtResult, currentPage);
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

        #region 分页 - 送料计划

        int totalPages = 0;

        /// <summary>
        /// 数据分页
        /// </summary>
        /// <param name="dtResult"></param>
        /// <returns></returns>
        private DataTable GetDataPage(DataTable dtResult, int currentPage)
        {
            totalPages = 0;
            int totalRows = 0;
            if (null == dtResult || dtResult.Rows.Count == 0)
            {
                this.ucPage1.PageInfo.Text = string.Format("第{0}/{1}页", "1", "1");
                this.ucPage1.TotalRows.Text = @"0";
                this.ucPage1.CurrentPage = 1;
                this.ucPage1.TotalPages = 1;
            }
            else
            {
                totalRows = Convert.ToInt32(dtResult.Rows[0]["TotalRows"].ToString());
                totalPages = totalRows % this.ucPage1.PageSize == 0 ? totalRows / this.ucPage1.PageSize : (totalRows / this.ucPage1.PageSize) + 1;
                this.ucPage1.PageInfo.Text = string.Format("第{0}/{1}页", currentPage, totalPages);
                this.ucPage1.TotalRows.Text = totalRows.ToString();
                this.ucPage1.CurrentPage = currentPage;
                this.ucPage1.TotalPages = totalPages;
            }
            return dtResult;
        }

        /// <summary>
        /// 页数跳转
        /// </summary>
        /// <param name="jumpPage">跳转页</param>
        void ucPage1_JumpPageEvent(int jumpPage)
        {
            if (jumpPage <= this.ucPage1.TotalPages)
            {
                if (jumpPage > 0)
                {
                    this.ucPage1.JumpPageCtrl.Text = string.Empty;
                    this.ucPage1.JumpPageCtrl.Text = jumpPage.ToString();
                    //L3送料计划
                    this.getL3MatWeightInfo(jumpPage);
                }
                else
                {
                    jumpPage = 1;
                    this.ucPage1.JumpPageCtrl.Text = string.Empty;
                    this.ucPage1.JumpPageCtrl.Text = jumpPage.ToString();
                    //L3送料计划
                    this.getL3MatWeightInfo(jumpPage);
                }
            }
            else
            {
                this.ucPage1.JumpPageCtrl.Text = string.Empty;
                MessageBox.Show(@"超出当前最大页数", @"提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// 改变每页展示数据长度
        /// </summary>
        void ucPage1_ChangedPageSizeEvent()
        {
            //L3送料计划
            this.getL3MatWeightInfo(1);
        }
        /// <summary>
        /// 页数改变按钮(最前页,最后页,上一页,下一页)
        /// </summary>
        /// <param name="current"></param>
        void ucPage1_ClickPageButtonEvent(int current)
        {
            if (totalPages != 0 && current > totalPages)
            {
                current = 1;
            }
            //L3送料计划
            this.getL3MatWeightInfo(current);
        }
        #endregion

        #region 分页 - 配料计划

        int totalPages2 = 0;

        /// <summary>
        /// 数据分页
        /// </summary>
        /// <param name="dtResult"></param>
        /// <returns></returns>
        private DataTable GetDataPage2(DataTable dtResult, int currentPage)
        {
            totalPages2 = 0;
            int totalRows = 0;
            if (null == dtResult || dtResult.Rows.Count == 0)
            {
                this.ucPage2.PageInfo.Text = string.Format("第{0}/{1}页", "1", "1");
                this.ucPage2.TotalRows.Text = @"0";
                this.ucPage2.CurrentPage = 1;
                this.ucPage2.TotalPages = 1;
            }
            else
            {
                totalRows = Convert.ToInt32(dtResult.Rows[dtResult.Rows.Count - 1]["TotalRows2"].ToString());
                totalPages2 = totalRows % this.ucPage2.PageSize == 0 ? totalRows / this.ucPage2.PageSize : (totalRows / this.ucPage2.PageSize) + 1;
                this.ucPage2.PageInfo.Text = string.Format("第{0}/{1}页", currentPage, totalPages2);
                this.ucPage2.TotalRows.Text = totalRows.ToString();
                this.ucPage2.CurrentPage = currentPage;
                this.ucPage2.TotalPages = totalPages2;
            }
            return dtResult;
        }

        /// <summary>
        /// 页数跳转
        /// </summary>
        /// <param name="jumpPage">跳转页</param>
        void ucPage2_JumpPageEvent(int jumpPage)
        {
            if (jumpPage <= this.ucPage2.TotalPages)
            {
                if (jumpPage > 0)
                {
                    this.ucPage2.JumpPageCtrl.Text = string.Empty;
                    this.ucPage2.JumpPageCtrl.Text = jumpPage.ToString();
                    //L3配料计划
                    this.getL3MatOutInfo(jumpPage);
                }
                else
                {
                    jumpPage = 1;
                    this.ucPage2.JumpPageCtrl.Text = string.Empty;
                    this.ucPage2.JumpPageCtrl.Text = jumpPage.ToString();
                    //L3配料计划
                    this.getL3MatOutInfo(jumpPage);
                }
            }
            else
            {
                this.ucPage2.JumpPageCtrl.Text = string.Empty;
                MessageBox.Show(@"超出当前最大页数", @"提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// 改变每页展示数据长度
        /// </summary>
        void ucPage2_ChangedPageSizeEvent()
        {
            //L3配料计划
            this.getL3MatOutInfo(1);
        }
        /// <summary>
        /// 页数改变按钮(最前页,最后页,上一页,下一页)
        /// </summary>
        /// <param name="current"></param>
        void ucPage2_ClickPageButtonEvent(int current)
        {
            if (totalPages2 != 0 && current > totalPages2)
            {
                current = 1;
            }
            //L3配料计划
            this.getL3MatOutInfo(current);
        }
        #endregion

        #region 日期查询        
        /// <summary>
        /// 当天
        /// </summary>
        private void GetToDayTime()
        {
            this.dateTimePicker1_recTime.Value = DateTime.Now;
            this.dateTimePicker2_recTime.Value = DateTime.Now;
            //查询
            //L3送料计划
            getL3MatWeightInfo(1);
            //L3配料计划
            getL3MatOutInfo(1);
        }
        /// <summary>
        /// 月度
        /// </summary>
        private void GetMonthlyTime()
        {
            var day1 = DateTime.Now.ToString("yyyy-MM-01");
            //控件设置值
            this.dateTimePicker1_recTime.Value = Convert.ToDateTime(day1);
            //查询
            //L3送料计划
            getL3MatWeightInfo(1);
            //L3配料计划
            getL3MatOutInfo(1);
        }
        /// <summary>
        /// 当前季度
        /// </summary>
        private void GetQuarterlyTime()
        {
            //季度第一天
            var day1 = DateTime.Now.AddMonths(0 - (DateTime.Now.Month - 1) % 3).ToString("yyyy-MM-01");
            //季度最后一天
            var lastday = DateTime.Parse(DateTime.Now.AddMonths(3 - (DateTime.Now.Month - 1) % 3).ToString("yyyy-MM-01")).AddDays(-1).ToShortDateString();
            //控件设置值
            this.dateTimePicker1_recTime.Value = Convert.ToDateTime(day1);
            //查询
            //L3送料计划
            getL3MatWeightInfo(1);
            //L3配料计划
            getL3MatOutInfo(1);
        }
        /// <summary>
        /// 年度
        /// </summary>
        private void GetAnnualTime()
        {
            var day1 = DateTime.Now.ToString("yyyy-01-01");
            //控件设置值
            this.dateTimePicker1_recTime.Value = Convert.ToDateTime(day1);
            //查询
            //L3送料计划
            getL3MatWeightInfo(1);
            //L3配料计划
            getL3MatOutInfo(1);
        }
        #endregion

        private void bt_TodayTime_Click(object sender, EventArgs e)
        {
            GetToDayTime();
        }

        private void bt_MonthlyTime_Click(object sender, EventArgs e)
        {
            GetMonthlyTime();
        }

        private void bt_QuarterlyTime_Click(object sender, EventArgs e)
        {
            GetQuarterlyTime();
        }

        private void bt_AnnualTime_Click(object sender, EventArgs e)
        {
            GetAnnualTime();
        }
    }
}
