using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Baosight.iSuperframe.Forms;
using UACSDAL;
using ParkClassLibrary;

namespace UACSView
{
    /// <summary>
    /// 行车指令管理
    /// </summary>
    public partial class CraneOrderHisyManage : FormBase
    {
        DataTable dt = new DataTable();
        DataTable dt_oper = new DataTable();
        DataTable dt_ack = new DataTable();
        bool hasSetColumn = false;
        bool hasSetColumn_oper = false;
        bool hasSetColumn_ack = false;
        private static Baosight.iSuperframe.Common.IDBHelper DBHelper = null;
        Dictionary<string, string> dicCmdStatus = new Dictionary<string, string>();
        Dictionary<string, string> dicFlagDispat = new Dictionary<string, string>();
        Dictionary<string, string> dicDelFlag = new Dictionary<string, string>();
        Dictionary<string, string> dicOrderType = new Dictionary<string, string>();
        string[] dgvColumnsName = { "PLAN_NO", "ORDER_NO", "ORDER_GROUP_NO", "EXE_SEQ", "ORDER_TYPE", "BOF_NO", "BAY_NO", "CRANE_NO", "CAR_NO", "CAR_TYPE", "PLAN_SRC", "ORDER_PRIORITY", "CMD_SEQ", "CMD_STATUS", "MAT_CODE", "MAT_CNAME", "FROM_STOCK_NO", "TO_STOCK_NO", "REQ_WEIGHT", "ACT_WEIGHT", "START_TIME", "UPD_TIME", "REC_TIME" };
        string[] dgvHeaderText = { "计划号", "指令号", "指令组号", "指令顺序", "指令类型", "炉号", "跨别", "行车", "料槽号", "料槽类型", "计划来源", "优先级", "吊运次数", "吊运状态", "物料代码", "物料名称", "取料位置", "落料位", "要求重量", "实绩重量", "开始时间", "更新时间", "创建时间" };
        public CraneOrderHisyManage()
        {
            InitializeComponent();
            DBHelper = Baosight.iSuperframe.Common.DataBase.DBFactory.GetHelper("ZJDB0");//平台连接数据库的Text
        }

        #region 事件
        /// <summary>
        /// 页面加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CraneOrderHisyManage_Load(object sender, EventArgs e)
        {
            try
            {
                //设置背景色
                //this.panel1.BackColor = Color.LightSteelBlue; //UACSDAL.ColorSln.FormBgColor;
                //this.panel2.BackColor = Color.LightSteelBlue;  //UACSDAL.ColorSln.FormBgColor;
                //绑定下拉框
                BindCombox();
                //开始日期
                this.dtp_StartTime.Value = DateTime.Now;

                ManagerHelper.DataGridViewInit(dataGridView1);
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                CreatDgvHeader(dataGridView1, dgvColumnsName, dgvHeaderText);
                //GetOrderData();
                //行车指令
                //getCraneOrderData2(true);
                //dataGridView1.DataSource = dt;
                //当前行车指令              
                //dataGridView2.DataSource = getCraneOrderData3(true);

                this.ucPageDemo.CurrentPage = 1;
                this.ucPageDemo.PageSize = Convert.ToInt32(this.ucPageDemo.CboPageSize.Text);
                this.ucPageDemo.TotalPages = 1;
                this.ucPageDemo.ClickPageButtonEvent += ucPageDemo_ClickPageButtonEvent;
                this.ucPageDemo.ChangedPageSizeEvent += ucPageDemo_ChangedPageSizeEvent;
                this.ucPageDemo.JumpPageEvent += ucPageDemo_JumpPageEvent;
                InitDataGridViewCtrl();
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
                //dataGridView1.DataSource = dt;

                ////当前选中页
                //int index = this.tabControl1.SelectedIndex;
                //if (index == 0)
                //{
                //    this.tabControl1.SelectedTab = this.tabPage1;
                //}
                //else if (index == 1)
                //{
                //    //this.tabControl1.SelectedTab = this.tabPage2;
                //}
                //else
                //{
                //    this.tabControl1.SelectedTab = this.tabPage1;
                //}
                ////行车指令
                //getCraneOrderData2(false);
                //dataGridView1.DataSource = dt;
                //当前行车指令              
                //dataGridView2.DataSource = getCraneOrderData3(false);
                //GetOrderData();
                InitDataGridViewCtrl();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

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

        /// <summary>
        /// 吊运历史双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (this.dataGridView1.Rows.Count <= 0)
                {
                    return;
                }
                //this.tabControl1.SelectedTab = this.tabPage2;
                string orderNo = this.dataGridView1.CurrentRow.Cells["ORDER_NO"].Value.ToString();
                string orderGroupNo = this.dataGridView1.CurrentRow.Cells["ORDER_GROUP_NO"].Value.ToString();
                string sqlText = @"SELECT UNIQUE_ID,ORDER_NO as ORDER_NO2,ORDER_GROUP_NO as ORDER_GROUP_NO2,CRANE_NO as CRANE_NO2,MAT_NO as MAT_NO2, STOCK_NO, X, Y,CMD_STATUS as CMD_STATUS2, DEL_FLAG, OPER_USERNAME, REC_TIME, OPER_EQUIPIP,ORDER_TYPE as ORDER_TYPE2, CRANE_SEQ, HG_NO,SEND_FLAG FROM UACS_CRANE_ORDER_OPER A ";
                sqlText += "WHERE A.ORDER_NO = '{0}' and A.ORDER_GROUP_NO = '{1}'";
                sqlText = string.Format(sqlText, orderNo, orderGroupNo);
                dt_oper = new DataTable();
                hasSetColumn_oper = false;
                using (IDataReader rdr = DBHelper.ExecuteReader(sqlText))
                {
                    while (rdr.Read())
                    {
                        DataRow dr = dt_oper.NewRow();
                        for (int i = 0; i < rdr.FieldCount; i++)
                        {
                            if (!hasSetColumn_oper)
                            {
                                DataColumn dc = new DataColumn();
                                dc.ColumnName = rdr.GetName(i);
                                dt_oper.Columns.Add(dc);
                            }
                            dr[i] = rdr[i];
                        }
                        hasSetColumn_oper = true;
                        dt_oper.Rows.Add(dr);
                    }
                }
                foreach (DataRow dr in dt_oper.Rows)
                {
                    string cmdStatusValue = dr["CMD_STATUS2"].ToString();
                    if (dicCmdStatus.ContainsKey(cmdStatusValue))
                    {
                        dr["CMD_STATUS2"] = dicCmdStatus[cmdStatusValue];
                    }
                    string flagDispatValue = dr["FLAG_DISPAT"].ToString();
                    if (dicFlagDispat.ContainsKey(flagDispatValue))
                    {
                        dr["FLAG_DISPAT"] = dicFlagDispat[flagDispatValue];
                    }
                    string orderTypeValue = dr["ORDER_TYPE2"].ToString();
                    if (dicOrderType.ContainsKey(orderTypeValue))
                    {
                        dr["ORDER_TYPE2"] = dicOrderType[orderTypeValue];
                    }
                    string delFlagValue = dr["DEL_FLAG"].ToString();
                    if (dicDelFlag.ContainsKey(delFlagValue))
                    {
                        dr["DEL_FLAG"] = dicDelFlag[delFlagValue];
                    }
                }

                //dataGridView2.DataSource = dt_oper;
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        /// <summary>
        /// 吊运实绩切换行事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                //if (this.dataGridView2.Rows.Count <= 0)
                //{
                //    return;
                //}
                //string craneSeq = this.dataGridView2.CurrentRow.Cells["CRANE_SEQ"].Value.ToString();
                //string hgNo = this.dataGridView2.CurrentRow.Cells["HG_NO"].Value.ToString();
                //string cmdStatus = this.dataGridView2.CurrentRow.Cells["CMD_STATUS2"].Value.ToString();
                //string matNo = this.dataGridView2.CurrentRow.Cells["MAT_NO2"].Value.ToString();
                //string sqlText = @"SELECT ACK_FLAG,MESSAGE,REC_TIME FROM UACS_PLAN_CRANPLAN_OPERACK ";
                //sqlText += "WHERE CRANE_SEQ = " + craneSeq + " and HG_NO = " + hgNo + " and CMD_STATUS = '" + cmdStatus + "' and MAT_NO = '" + matNo + "'";
                //dt_ack.Clear();
                //using (IDataReader rdr = DBHelper.ExecuteReader(sqlText))
                //{
                //    while (rdr.Read())
                //    {
                //        DataRow dr = dt_ack.NewRow();
                //        for (int i = 0; i < rdr.FieldCount; i++)
                //        {
                //            if (!hasSetColumn_ack)
                //            {
                //                DataColumn dc = new DataColumn();
                //                dc.ColumnName = rdr.GetName(i);
                //                dt_ack.Columns.Add(dc);
                //            }
                //            dr[i] = rdr[i];
                //        }
                //        hasSetColumn_ack = true;
                //        dt_ack.Rows.Add(dr);
                //    }
                //}

                //dataGridView3.DataSource = dt_ack;
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        #region 处理下拉框处理数据（忽略代码）
        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        #endregion
        #endregion

        #region 方法
        /// <summary>
        /// 绑定下拉框数据
        /// </summary>
        private void BindCombox()
        {
            CraneOrderImpl craneOrderImpl = new CraneOrderImpl();
            //绑定行车号
            //DataTable dtCRANE_DEFINE = craneOrderImpl.GetCRANE_DEFINE(true);
            //bindCombox(this.cbb_CRANE_NO, dtCRANE_DEFINE, true);
            BindCmbCRANE_DEFINE();
            //绑定料槽号
            BindCmbCar();
            //料槽车号
            //DataTable dtORDER_TYPE = craneOrderImpl.GetORDER_TYPE(true);
            //bindCombox(this.cbb_CarNo, dtORDER_TYPE, true);


        }

        /// <summary>
        /// 获取配载信息
        /// </summary>
        /// <param name="theStowageId">配载号</param>
        /// <param name="carNo">车辆号</param>
        /// <param name="packingNo">停车位号</param>
        /// <param name="dgv"></param>
        /// <returns></returns>
        public void GetOrderData(int currentPage)
        {
            DataTable dtNull = new DataTable();
            try
            {
                string craneNo = this.cbb_CRANE_NO.SelectedValue.ToString();
                string carNo = this.cbb_CarNo.SelectedValue.ToString();
                string planNo = this.txt_PLAN_NO.Text.Trim();
                string recTime1 = this.dtp_StartTime.Value.ToString("yyyyMMdd000000");
                string recTime2 = this.dtp_EndTime.Value.ToString("yyyyMMdd235959");
                //对应指令车辆配载明细
                string sqlText_ORDER = @"SELECT A.ORDER_NO,A.ORDER_GROUP_NO,A.EXE_SEQ,A.BAY_NO, CASE 
                                            WHEN A.CRANE_NO = 1 THEN '1号行车' 
                                            WHEN A.CRANE_NO = 2 THEN '2号行车' 
                                            WHEN A.CRANE_NO = 3 THEN '3号行车' 
                                            WHEN A.CRANE_NO = 4 THEN '4号行车' 
                                            END AS CRANE_NO
                                            ,A.CAR_NO,
                                            CASE 
                                            WHEN A.CAR_TYPE = 1 THEN '社会车' 
                                            WHEN A.CAR_TYPE = 2 THEN '装料车' 
                                            ELSE '其他车辆'
                                            END AS CAR_TYPE
                                            ,A.ORDER_TYPE,A.PLAN_SRC,A.ORDER_PRIORITY,CMD_SEQ,
                                            CASE 
                                            WHEN A.CMD_STATUS = 0 THEN '初始化' 
                                            WHEN A.CMD_STATUS = 1 THEN '获取指令' 
                                            WHEN A.CMD_STATUS = 2 THEN '激光扫描' 
                                            WHEN A.CMD_STATUS = 3 THEN '到取料点上方' 
                                            WHEN A.CMD_STATUS = 4 THEN '空载下降到位' 
                                            WHEN A.CMD_STATUS = 5 THEN '有载荷量' 
                                            WHEN A.CMD_STATUS = 6 THEN '重载上升到位' 
                                            WHEN A.CMD_STATUS = 7 THEN '到放料点上方' 
                                            WHEN A.CMD_STATUS = 8 THEN '重载下降到位' 
                                            WHEN A.CMD_STATUS = 9 THEN '无载荷量' 
                                            WHEN A.CMD_STATUS = 10 THEN '空载上升到位' 
                                            ELSE '其他' 
                                            END AS CMD_STATUS
                                            ,C.BOF_NO,A.PLAN_NO,A.MAT_CODE,B.MAT_CNAME ,A.FROM_STOCK_NO,A.TO_STOCK_NO,A.REQ_WEIGHT,A.ACT_WEIGHT,A.START_TIME,A.UPD_TIME,A.REC_TIME ";
                sqlText_ORDER += " FROM UACS_ORDER_QUEUE AS A ";
                sqlText_ORDER += " LEFT JOIN UACS_L3_MAT_INFO AS B ON A.MAT_CODE = B.MAT_CODE ";
                sqlText_ORDER += " LEFT JOIN UACS_L3_MAT_OUT_INFO AS C ON C.PLAN_NO = A.PLAN_NO ";
                sqlText_ORDER += "WHERE 1=1 ";
                sqlText_ORDER += "AND A.REC_TIME > '{0}' AND A.REC_TIME < '{1}' ";
                sqlText_ORDER = string.Format(sqlText_ORDER, recTime1, recTime2);
                if (!string.IsNullOrEmpty(craneNo) && craneNo != "全部")
                {
                    sqlText_ORDER = string.Format("{0} AND A.CRANE_NO = '{1}' ", sqlText_ORDER, craneNo);
                }
                if (!string.IsNullOrEmpty(carNo) && carNo != "全部")
                {
                    sqlText_ORDER = string.Format("{0} AND A.CAR_NO = '{1}' ", sqlText_ORDER, carNo);
                }
                if (!string.IsNullOrEmpty(planNo))
                {
                    sqlText_ORDER = string.Format("{0} AND A.PLAN_NO LIKE '%{1}%'", sqlText_ORDER, planNo);
                }
                sqlText_ORDER += " ORDER BY A.ORDER_NO DESC,A.REC_TIME DESC ";
                DataTable dt = new DataTable();
                using (IDataReader odrIn = DB2Connect.DBHelper.ExecuteReader(sqlText_ORDER))
                {
                    dt.Load(odrIn);
                    dataGridView1.DataSource = dt;
                    odrIn.Close();
                }
            }
            catch (Exception ex)
            { }
        }

        /// <summary>
        /// 获取行车号
        /// </summary>
        /// <returns></returns>
        public void BindCmbCRANE_DEFINE()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TypeValue");
            dt.Columns.Add("TypeName");
            DataRow dr = dt.NewRow();
            dr = dt.NewRow();
            dr["TypeValue"] = "全部";
            dr["TypeName"] = "全部";
            dt.Rows.Add(dr);
            //准备数据
            string sqlText = @"SELECT CRANE_NO AS TypeValue,CRANE_NO AS TypeName, BAY_NO, CLAMP_TYPE, SEQ_NO, WORK_POS_X_START, WORK_POS_X_END FROM UACSAPP.UACS_CRANE_DEFINE ";
            using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sqlText))
            {
                while (rdr.Read())
                {
                    dr = dt.NewRow();
                    dr["TypeValue"] = rdr["TypeValue"];
                    dr["TypeName"] = rdr["TypeName"] + "号行车";
                    dt.Rows.Add(dr);
                }
            }
            //绑定数据
            cbb_CRANE_NO.ValueMember = "TypeValue";
            cbb_CRANE_NO.DisplayMember = "TypeName";
            cbb_CRANE_NO.DataSource = dt;
            //根据text值选中项
            this.cbb_CRANE_NO.SelectedIndex = 0;
        }

        /// <summary>
        /// 绑定槽号
        /// </summary>
        /// <param name="ParkNO">停车位号</param>
        private void BindCmbCar()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("ID");
                dt.Columns.Add("NAME");
                DataRow dr = dt.NewRow();
                dr = dt.NewRow();
                dr["ID"] = "全部";
                dr["NAME"] = "全部";
                dt.Rows.Add(dr);
                //查车辆号
                string sqlText = @"SELECT FRAME_TYPE_NO FROM UACS_TRUCK_FRAME_DEFINE ";
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sqlText))
                {
                    while (rdr.Read())
                    {
                        dr = dt.NewRow();
                        dr["ID"] = rdr["FRAME_TYPE_NO"].ToString();
                        dr["NAME"] = rdr["FRAME_TYPE_NO"].ToString();
                        dt.Rows.Add(dr);
                    }
                }
                //绑定数据
                cbb_CarNo.ValueMember = "ID";
                cbb_CarNo.DisplayMember = "NAME";
                cbb_CarNo.DataSource = dt;
                //根据text值选中项
                this.cbb_CarNo.SelectedIndex = 0;
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 绑定下拉框
        /// </summary>
        /// <param name="control">下拉框控件</param>
        /// <param name="dt">数据源</param>
        /// <param name="showLastIndex">是否显示最后一条（通常用于查询条件中全部）</param>
        private void bindCombox(ComboBox control, DataTable dt, bool showLastIndex)
        {
            control.DataSource = dt;
            control.DisplayMember = "TypeName";
            control.ValueMember = "TypeValue";
            if (showLastIndex)
            {
                control.SelectedIndex = dt.Rows.Count - 1;
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
                    column.Width = 125;
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
        #endregion




        #region 分页

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
                this.ucPageDemo.PageInfo.Text = string.Format("第{0}/{1}页", "1", "1");
                this.ucPageDemo.TotalRows.Text = @"0";
                this.ucPageDemo.CurrentPage = 1;
                this.ucPageDemo.TotalPages = 1;
            }
            else
            {
                totalRows = Convert.ToInt32(dtResult.Rows[0]["TotalRows"].ToString());
                totalPages = totalRows % this.ucPageDemo.PageSize == 0 ? totalRows / this.ucPageDemo.PageSize : (totalRows / this.ucPageDemo.PageSize) + 1;
                this.ucPageDemo.PageInfo.Text = string.Format("第{0}/{1}页", currentPage, totalPages);
                this.ucPageDemo.TotalRows.Text = totalRows.ToString();
                this.ucPageDemo.CurrentPage = currentPage;
                this.ucPageDemo.TotalPages = totalPages;
            }
            return dtResult;
        }

        /// <summary>
        /// 页数跳转
        /// </summary>
        /// <param name="jumpPage">跳转页</param>
        void ucPageDemo_JumpPageEvent(int jumpPage)
        {
            if (jumpPage <= this.ucPageDemo.TotalPages)
            {
                if (jumpPage > 0)
                {
                    this.ucPageDemo.JumpPageCtrl.Text = string.Empty;
                    this.ucPageDemo.JumpPageCtrl.Text = jumpPage.ToString();
                    this.ShowDatas(jumpPage);
                }
                else
                {
                    jumpPage = 1;
                    this.ucPageDemo.JumpPageCtrl.Text = string.Empty;
                    this.ucPageDemo.JumpPageCtrl.Text = jumpPage.ToString();
                    this.ShowDatas(jumpPage);
                }
            }
            else
            {
                this.ucPageDemo.JumpPageCtrl.Text = string.Empty;
                MessageBox.Show(@"超出当前最大页数", @"提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// 改变每页展示数据长度
        /// </summary>
        void ucPageDemo_ChangedPageSizeEvent()
        {
            this.ShowDatas(1);
        }
        /// <summary>
        /// 页数改变按钮(最前页,最后页,上一页,下一页)
        /// </summary>
        /// <param name="current"></param>
        void ucPageDemo_ClickPageButtonEvent(int current)
        {
            if (totalPages != 0 && current > totalPages)
            {
                current = 1;
            }
            this.ShowDatas(current);
        } 
        #endregion

        /// <summary>
        /// 初始化DataGridView控件
        /// </summary>
        private void InitDataGridViewCtrl()
        {
            this.ShowDatas(1);
        }
        /// <summary>
        /// 数据展示
        /// </summary>
        /// <param name="currentPage">当前页</param>
        private void ShowDatas(int currentPage)
        {

            DataTable dtNull = new DataTable();
            try
            {
                string craneNo = this.cbb_CRANE_NO.SelectedValue.ToString();
                string carNo = this.cbb_CarNo.SelectedValue.ToString();
                string planNo = this.txt_PLAN_NO.Text.Trim();
                string recTime1 = this.dtp_StartTime.Value.ToString("yyyyMMdd000000");
                string recTime2 = this.dtp_EndTime.Value.ToString("yyyyMMdd235959");
                //对应指令车辆配载明细
                string sqlText_ORDER = "SELECT * FROM (SELECT COUNT(1) OVER () AS TotalRows,ROW_NUMBER() OVER () AS ROWNUM,tab.* FROM ( ";
                sqlText_ORDER += @"SELECT A.ORDER_NO,A.ORDER_GROUP_NO,A.EXE_SEQ,A.BAY_NO, CASE 
                                            WHEN A.CRANE_NO = 1 THEN '1号行车' 
                                            WHEN A.CRANE_NO = 2 THEN '2号行车' 
                                            WHEN A.CRANE_NO = 3 THEN '3号行车' 
                                            WHEN A.CRANE_NO = 4 THEN '4号行车' 
                                            END AS CRANE_NO
                                            ,A.CAR_NO,
                                            CASE 
                                            WHEN A.CAR_TYPE = 1 THEN '社会车' 
                                            WHEN A.CAR_TYPE = 2 THEN '装料车' 
                                            ELSE '其他车辆'
                                            END AS CAR_TYPE
                                            ,A.ORDER_TYPE,A.PLAN_SRC,A.ORDER_PRIORITY,CMD_SEQ,
                                            CASE 
                                            WHEN A.CMD_STATUS = 0 THEN '初始化' 
                                            WHEN A.CMD_STATUS = 1 THEN '获取指令' 
                                            WHEN A.CMD_STATUS = 2 THEN '激光扫描' 
                                            WHEN A.CMD_STATUS = 3 THEN '到取料点上方' 
                                            WHEN A.CMD_STATUS = 4 THEN '空载下降到位' 
                                            WHEN A.CMD_STATUS = 5 THEN '有载荷量' 
                                            WHEN A.CMD_STATUS = 6 THEN '重载上升到位' 
                                            WHEN A.CMD_STATUS = 7 THEN '到放料点上方' 
                                            WHEN A.CMD_STATUS = 8 THEN '重载下降到位' 
                                            WHEN A.CMD_STATUS = 9 THEN '无载荷量' 
                                            WHEN A.CMD_STATUS = 10 THEN '空载上升到位' 
                                            ELSE '其他' 
                                            END AS CMD_STATUS
                                            ,C.BOF_NO,A.PLAN_NO,A.MAT_CODE,B.MAT_CNAME ,A.FROM_STOCK_NO,A.TO_STOCK_NO,A.REQ_WEIGHT,A.ACT_WEIGHT,A.START_TIME,A.UPD_TIME,A.REC_TIME ";
                sqlText_ORDER += " FROM UACS_ORDER_QUEUE AS A ";
                sqlText_ORDER += " LEFT JOIN UACS_L3_MAT_INFO AS B ON A.MAT_CODE = B.MAT_CODE ";
                sqlText_ORDER += " LEFT JOIN UACS_L3_MAT_OUT_INFO AS C ON C.PLAN_NO = A.PLAN_NO ";
                sqlText_ORDER += "WHERE 1=1 ";
                sqlText_ORDER += "AND A.REC_TIME > '{0}' AND A.REC_TIME < '{1}' ";
                sqlText_ORDER = string.Format(sqlText_ORDER, recTime1, recTime2);



                if (!string.IsNullOrEmpty(craneNo) && craneNo != "全部")
                {
                    sqlText_ORDER = string.Format("{0} AND A.CRANE_NO = '{1}' ", sqlText_ORDER, craneNo);
                }
                if (!string.IsNullOrEmpty(carNo) && carNo != "全部")
                {
                    sqlText_ORDER = string.Format("{0} AND A.CAR_NO = '{1}' ", sqlText_ORDER, carNo);
                }
                if (!string.IsNullOrEmpty(planNo))
                {
                    sqlText_ORDER = string.Format("{0} AND A.PLAN_NO LIKE '%{1}%'", sqlText_ORDER, planNo);
                }
                sqlText_ORDER += " ORDER BY A.ORDER_NO DESC,A.REC_TIME DESC ";
                sqlText_ORDER += " ) tab ) WHERE ROWNUM BETWEEN ((" + currentPage + " - 1) * " + this.ucPageDemo.PageSize + ") + 1 AND " + currentPage + " *  " + this.ucPageDemo.PageSize;
                DataTable dtResult = new DataTable();
                using (IDataReader odrIn = DB2Connect.DBHelper.ExecuteReader(sqlText_ORDER))
                {
                    dtResult.Load(odrIn);
                }
                this.dataGridView1.DataSource = GetDataPage(dtResult, currentPage);
            }
            catch (Exception ex)
            { }
        }

        #region 日期查询        
        /// <summary>
        /// 当天
        /// </summary>
        private void GetToDayTime()
        {
            this.dtp_StartTime.Value = DateTime.Now;
            this.dtp_EndTime.Value = DateTime.Now;
            //查询
            InitDataGridViewCtrl();
        }
        /// <summary>
        /// 月度
        /// </summary>
        private void GetMonthlyTime()
        {
            var day1 = DateTime.Now.ToString("yyyy-MM-01");
            //控件设置值
            this.dtp_StartTime.Value = Convert.ToDateTime(day1);
            //查询
            InitDataGridViewCtrl();
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
            this.dtp_StartTime.Value = Convert.ToDateTime(day1);
            //查询
            InitDataGridViewCtrl();
        }
        /// <summary>
        /// 年度
        /// </summary>
        private void GetAnnualTime()
        {
            var day1 = DateTime.Now.ToString("yyyy-01-01");
            //控件设置值
            this.dtp_StartTime.Value = Convert.ToDateTime(day1);
            //查询
            InitDataGridViewCtrl();
        }
        #endregion


    }
}
