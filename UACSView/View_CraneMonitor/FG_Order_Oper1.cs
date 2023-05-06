using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UACS;
using Baosight.iSuperframe.Forms;
using UACSDAL;
using System.Windows.Forms.VisualStyles;

namespace UACSView.View_CraneMonitor
{
    /// <summary>
    /// 行车作业实绩管理
    /// </summary>
    public partial class FG_Order_Oper1 : FormBase
    {
        DataTable dtBayNo = new DataTable();
        bool hasSetColumn = false;
        private static Baosight.iSuperframe.Common.IDBHelper DBHelper = null;
        public FG_Order_Oper1()
        {
            InitializeComponent();
            DBHelper = Baosight.iSuperframe.Common.DataBase.DBFactory.GetHelper("ZJDB0");//平台连接数据库的Text
        }

        #region 事件

        private void FG_Order_Oper1_Load(object sender, EventArgs e)
        {
            try
            {
                //设置背景色
                //this.panel1.BackColor = UACSDAL.ColorSln.FormBgColor;
                //this.panel2.BackColor = UACSDAL.ColorSln.FormBgColor;
                //绑定下拉框
                BindCombox();
                //
                this.dateTimePicker1_recTime.Value = DateTime.Now.AddDays(-1);

                //加载初始化数据
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
            //绑定跨号
            DataTable dtCRANE_DEFINE = GetBAY_NO(true);
            bindCombox(this.cbb_BAY_NO, dtCRANE_DEFINE, true);
            //绑定行车模式
            DataTable dtCRANE_MODE = GetCRANE_MODE(true);
            bindCombox(this.cbb_CRANE_MODE, dtCRANE_MODE, true);
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="isLoad">是否是初始化 true=是初始化，false=不是初始化</param>
        private DataTable getCraneOrderData(bool isLoad)
        {
            string bayNo = this.cbb_BAY_NO.SelectedValue.ToString();  //跨号
            string craneMode = this.cbb_CRANE_MODE.SelectedValue.ToString();  //行车模式
            string work_seqNo = this.textWORK_SEQ_NO.Text.Trim();        //计划号
            string recTime1 = this.dateTimePicker1_recTime.Value.ToString("yyyyMMdd000000");  //开始时间
            string recTime2 = this.dateTimePicker2_recTime.Value.ToString("yyyyMMdd235959");  //结束时间
            string sqlText = @"SELECT A.OPER_ID, A.ORDER_NO, A.ORDER_TYPE, A.BAY_NO, A.MAT_CODE, C.MAT_CNAME, A.MAT_TYPE, A.MAT_REQ_WGT, A.MAT_CUR_WGT, A.HAS_COIL_WGT, A.FROM_STOCK_NO, A.TO_STOCK_NO, A.STOCK_NO, A.CMD_STATUS, A.CMD_SEQ, A.PLAN_X, A.PLAN_Y, A.ACT_X, A.ACT_Y, A.CRANE_MODE, A.REC_TIME, A.CRANE_NO FROM UACSAPP.UACS_ORDER_OPER A ";
            sqlText += "LEFT JOIN UACS_L3_MAT_INFO C ON C.MAT_CODE = A.MAT_CODE ";
            sqlText += "WHERE A.REC_TIME > '{0}' and A.REC_TIME < '{1}' ";
            sqlText = string.Format(sqlText, recTime1, recTime2);
            //if (!isLoad)
            //{
                if (!string.IsNullOrEmpty(work_seqNo))
                {
                    sqlText = string.Format("{0} and A.MAT_CODE LIKE '%{1}%' ", sqlText, work_seqNo);
                }
                if (bayNo != "全部")
                {
                    sqlText = string.Format("{0} and A.BAY_NO = '{1}' ", sqlText, bayNo);
                }
                if (craneMode != "全部")
                {
                    sqlText = string.Format("{0} and A.CRANE_MODE = '{1}' ", sqlText, craneMode);
                }
                //按 NO>流水号>记录时间>更新时间 降序
                sqlText += " ORDER BY A.OPER_ID DESC,A.ORDER_NO DESC,A.REC_TIME DESC ";
            //}
            //else
            //{
            //    //初次加载时默认查询倒序30条数据（仅初始化时用）
            //    sqlText = @"SELECT OPER_ID,ORDER_NO,ORDER_TYPE,BAY_NO,MAT_CODE,MAT_CNAME,MAT_TYPE,MAT_REQ_WGT,MAT_CUR_WGT,HAS_COIL_WGT,FROM_STOCK_NO,TO_STOCK_NO,STOCK_NO,CMD_STATUS,CMD_SEQ,PLAN_X,PLAN_Y,ACT_X,ACT_Y,CRANE_MODE,REC_TIME 
            //                FROM (
            //                SELECT ROW_NUMBER() OVER(ORDER BY A.OPER_ID DESC,A.ORDER_NO DESC,A.REC_TIME DESC) AS ROWNUM,
            //                A.OPER_ID, A.ORDER_NO, A.ORDER_TYPE, A.BAY_NO, A.MAT_CODE, C.MAT_CNAME, A.MAT_TYPE, A.MAT_REQ_WGT, A.MAT_CUR_WGT, A.HAS_COIL_WGT, A.FROM_STOCK_NO, A.TO_STOCK_NO, A.STOCK_NO, A.CMD_STATUS, A.CMD_SEQ, A.PLAN_X, A.PLAN_Y, A.ACT_X, A.ACT_Y, A.CRANE_MODE, A.REC_TIME FROM UACSAPP.UACS_ORDER_OPER A 
            //                LEFT JOIN UACS_L3_MAT_INFO C ON C.MAT_CODE = A.MAT_CODE 
            //                ) a 
            //                WHERE ROWNUM > 0 and ROWNUM <=30";
            //}

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

                    #region 转换别称
                    //跨号转跨名
                    foreach (DataRow item in dtBayNo.Rows)
                    {
                        if (item["TypeValue"] != System.DBNull.Value)
                        {
                            var TypeValue = item["TypeValue"].ToString();
                            var Bay_NO = dr["BAY_NO"].ToString();
                            if (Bay_NO.Equals(TypeValue))
                            {
                                dr["BAY_NO"] = item["TypeName"].ToString();
                            }
                        }
                    }

                    //废钢类型 1-轻废 2-中废 3-重废
                    if (dr["MAT_TYPE"] != System.DBNull.Value)
                    {
                        if (dr["MAT_TYPE"].Equals("1"))
                        {
                            dr["MAT_TYPE"] = "轻废";
                        }
                        else if (dr["MAT_TYPE"].Equals("2"))
                        {
                            dr["MAT_TYPE"] = "中废";
                        }
                        else if (dr["MAT_TYPE"].Equals("3"))
                        {
                            dr["MAT_TYPE"] = "重废";
                        }
                    }

                    //指令状态（S-激光扫描 1-获取指令 2-到取料点上方 3-空载下降到位 4-有载荷量 5-重载上升到位 6-到放料点上方 7-重载下降到位 8-无载荷量 9-空载上升到位）
                    if (dr["CMD_STATUS"] != System.DBNull.Value)
                    {
                        if (dr["CMD_STATUS"].Equals("S"))
                        {
                            dr["CMD_STATUS"] = "激光扫描";
                        }
                        else if (dr["CMD_STATUS"].Equals("1"))
                        {
                            dr["CMD_STATUS"] = "获取指令";
                        }
                        else if (dr["CMD_STATUS"].Equals("2"))
                        {
                            dr["CMD_STATUS"] = "到取料点上方";
                        }
                        else if (dr["CMD_STATUS"].Equals("3"))
                        {
                            dr["CMD_STATUS"] = "空载下降到位";
                        }
                        else if (dr["CMD_STATUS"].Equals("4"))
                        {
                            dr["CMD_STATUS"] = "有载荷量";
                        }
                        else if (dr["CMD_STATUS"].Equals("5"))
                        {
                            dr["CMD_STATUS"] = "重载上升到位";
                        }
                        else if (dr["CMD_STATUS"].Equals("6"))
                        {
                            dr["CMD_STATUS"] = "到放料点上方";
                        }
                        else if (dr["CMD_STATUS"].Equals("7"))
                        {
                            dr["CMD_STATUS"] = "重载下降到位";
                        }
                        else if (dr["CMD_STATUS"].Equals("8"))
                        {
                            dr["CMD_STATUS"] = "无载荷量";
                        }
                        else if (dr["CMD_STATUS"].Equals("9"))
                        {
                            dr["CMD_STATUS"] = "空载上升到位";
                        }
                    }

                    //行车模式转中文展示，1-远程操控 2-人工 4-自动 5-等待
                    if (dr["CRANE_MODE"] != System.DBNull.Value)
                    {
                        if (dr["CRANE_MODE"].Equals("1"))
                        {
                            dr["CRANE_MODE"] = "远程操控";
                        }
                        else if (dr["CRANE_MODE"].Equals("2"))
                        {
                            dr["CRANE_MODE"] = "人工";
                        }
                        else if (dr["CRANE_MODE"].Equals("4"))
                        {
                            dr["CRANE_MODE"] = "自动";
                        }
                        else if (dr["CRANE_MODE"].Equals("5"))
                        {
                            dr["CRANE_MODE"] = "等待";
                        }
                    }
                    #endregion

                    hasSetColumn = true;
                    dataTable.Rows.Add(dr);
                }
            }
            return dataTable;
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
        #endregion


        #region 下拉框数据源
        /// <summary>
        /// 获取跨号
        /// </summary>
        /// <param name="showAll"></param>
        /// <returns></returns>
        public DataTable GetBAY_NO(bool showAll)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TypeValue");
            dt.Columns.Add("TypeName");
            //准备数据
            string sqlText = @"SELECT BAY_NO AS TypeValue, BAY_NAME AS TypeName FROM UACSAPP.UACS_YARDMAP_BAY_DEFINE ";
            using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sqlText))
            {
                while (rdr.Read())
                {
                    DataRow dr = dt.NewRow();
                    dr["TypeValue"] = rdr["TypeValue"];
                    dr["TypeName"] = rdr["TypeName"];
                    dt.Rows.Add(dr);
                }
            }
            if (showAll)
            {
                dt.Rows.Add("全部", "全部");
            }
            //拷贝整个表
            dtBayNo = dt.Copy();
            return dt;
        }

        /// <summary>
        /// 获取行车模式 1-远程操控 2-人工 4-自动 5-等待
        /// </summary>
        /// <param name="showAll"></param>
        /// <returns></returns>
        public DataTable GetCRANE_MODE(bool showAll)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TypeValue");
            dt.Columns.Add("TypeName");
            dt.Rows.Add("1", "远程操控");
            dt.Rows.Add("2", "人工");
            dt.Rows.Add("4", "自动");
            dt.Rows.Add("5", "等待");
            if (showAll)
            {
                dt.Rows.Add("全部", "全部");
            }
            return dt;
        }
        #endregion

        /// <summary>
        /// 当前季度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            //季度第一天
            var day1 = DateTime.Now.AddMonths(0 - (DateTime.Now.Month - 1) % 3).ToString("yyyy-MM-01");
            //季度最后一天
            var lastday = DateTime.Parse(DateTime.Now.AddMonths(3 - (DateTime.Now.Month - 1) % 3).ToString("yyyy-MM-01")).AddDays(-1).ToShortDateString();
            //控件设置值
            this.dateTimePicker1_recTime.Value = Convert.ToDateTime(day1);
            //查询
            DataTable dataTable = getCraneOrderData(false);
            if (dataTable.Rows.Count > 0)
            {
                dataGridView1.DataSource = dataTable;
            }
            else
            {
                while (dataGridView1.Rows.Count != 0)
                {
                    dataGridView1.Rows.RemoveAt(0);
                }
            }
        }

        /// <summary>
        /// 月度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            var day1 = DateTime.Now.ToString("yyyy-MM-01");
            //控件设置值
            this.dateTimePicker1_recTime.Value = Convert.ToDateTime(day1);
            //查询
            DataTable dataTable = getCraneOrderData(false);
            if (dataTable.Rows.Count > 0)
            {
                dataGridView1.DataSource = dataTable;
            }
            else
            {
                while (dataGridView1.Rows.Count != 0)
                {
                    dataGridView1.Rows.RemoveAt(0);
                }
            }
            
        }

        /// <summary>
        /// 年度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            var day1 = DateTime.Now.ToString("yyyy-01-01");
            //控件设置值
            this.dateTimePicker1_recTime.Value = Convert.ToDateTime(day1);
            //查询
            DataTable dataTable = getCraneOrderData(false);
            if (dataTable.Rows.Count > 0)
            {
                dataGridView1.DataSource = dataTable;
            }
            else
            {
                while (dataGridView1.Rows.Count != 0)
                {
                    dataGridView1.Rows.RemoveAt(0);
                }
            }
        }
    }
}
