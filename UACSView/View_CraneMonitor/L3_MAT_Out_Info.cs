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
                this.panel1.BackColor = UACSDAL.ColorSln.FormBgColor;
                this.panel2.BackColor = UACSDAL.ColorSln.FormBgColor;
                //绑定下拉框
                //BindCombox();
                //
                this.dateTimePicker1_recTime.Value = DateTime.Now.AddDays(-1);
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
                dataGridView1.DataSource = getCraneOrderData();
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
        private DataTable getCraneOrderData()
        {
            DataTable dtResult = InitDataTable(dataGridView1);
            string planNo = this.textPLAN_NO.Text.Trim();  //计划号
            string recTime1 = this.dateTimePicker1_recTime.Value.ToString("yyyyMMdd000000");  //开始时间
            string recTime2 = this.dateTimePicker2_recTime.Value.ToString("yyyyMMdd235959");  //结束时间
            string sqlText = @"SELECT WORK_SEQ_NO, OPER_FLAG, PLAN_NO, BOF_NO, CAR_NO, MAT_CODE_1, WEIGHT_1, MAT_CODE_2, WEIGHT_2, MAT_CODE_3, WEIGHT_3, MAT_CODE_4, WEIGHT_4, MAT_CODE_5, WEIGHT_5, MAT_CODE_6, WEIGHT_6, MAT_CODE_7, WEIGHT_7, MAT_CODE_8, WEIGHT_8, MAT_CODE_9, WEIGHT_9, MAT_CODE_10, WEIGHT_10, PLAN_STATUS, REC_TIME, UPD_TIME, CYCLE_COUNT, MAT_NET_WT, WT_TIME FROM UACSAPP.UACS_L3_MAT_OUT_INFO ";
            sqlText += "WHERE REC_TIME > '{0}' and REC_TIME < '{1}' ";
            sqlText = string.Format(sqlText, recTime1, recTime2);
            if (!string.IsNullOrEmpty(planNo))
            {
                sqlText = string.Format("{0} and PLAN_NO LIKE '%{1}%' ", sqlText, planNo);
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

            foreach (DataRow dataRow in tbL3_MAT_OUT_INFO.Rows)
            {

                if (!string.IsNullOrEmpty(dataRow["MAT_CODE_1"].ToString()))
                {
                    dtResult.Rows.Add(dataRow["PLAN_NO"].ToString(), dataRow["CAR_NO"].ToString(), dataRow["MAT_CODE_1"].ToString(), dataRow["WEIGHT_1"].ToString(), dataRow["REC_TIME"].ToString());
                }
                if (!string.IsNullOrEmpty(dataRow["MAT_CODE_2"].ToString()))
                {
                    dtResult.Rows.Add(dataRow["PLAN_NO"].ToString(), dataRow["CAR_NO"].ToString(), dataRow["MAT_CODE_2"].ToString(), dataRow["WEIGHT_2"].ToString(), dataRow["REC_TIME"].ToString());
                }
                if (!string.IsNullOrEmpty(dataRow["MAT_CODE_3"].ToString()))
                {
                    dtResult.Rows.Add(dataRow["PLAN_NO"].ToString(), dataRow["CAR_NO"].ToString(), dataRow["MAT_CODE_3"].ToString(), dataRow["WEIGHT_3"].ToString(), dataRow["REC_TIME"].ToString());
                }
                if (!string.IsNullOrEmpty(dataRow["MAT_CODE_4"].ToString()))
                {
                    dtResult.Rows.Add(dataRow["PLAN_NO"].ToString(), dataRow["CAR_NO"].ToString(), dataRow["MAT_CODE_4"].ToString(), dataRow["WEIGHT_4"].ToString(), dataRow["REC_TIME"].ToString());
                }
                if (!string.IsNullOrEmpty(dataRow["MAT_CODE_5"].ToString()))
                {

                    dtResult.Rows.Add(dataRow["PLAN_NO"].ToString(), dataRow["CAR_NO"].ToString(), dataRow["MAT_CODE_5"].ToString(), dataRow["WEIGHT_5"].ToString(), dataRow["REC_TIME"].ToString());
                }
                if (!string.IsNullOrEmpty(dataRow["MAT_CODE_6"].ToString()))
                {

                    dtResult.Rows.Add(dataRow["PLAN_NO"].ToString(), dataRow["CAR_NO"].ToString(), dataRow["MAT_CODE_6"].ToString(), dataRow["WEIGHT_6"].ToString(), dataRow["REC_TIME"].ToString());
                }
                if (!string.IsNullOrEmpty(dataRow["MAT_CODE_7"].ToString()))
                {

                    dtResult.Rows.Add(dataRow["PLAN_NO"].ToString(), dataRow["CAR_NO"].ToString(), dataRow["MAT_CODE_7"].ToString(), dataRow["WEIGHT_7"].ToString(), dataRow["REC_TIME"].ToString());
                }
                if (!string.IsNullOrEmpty(dataRow["MAT_CODE_8"].ToString()))
                {

                    dtResult.Rows.Add(dataRow["PLAN_NO"].ToString(), dataRow["CAR_NO"].ToString(), dataRow["MAT_CODE_8"].ToString(), dataRow["WEIGHT_8"].ToString(), dataRow["REC_TIME"].ToString());
                }
                if (!string.IsNullOrEmpty(dataRow["MAT_CODE_9"].ToString()))
                {

                    dtResult.Rows.Add(dataRow["PLAN_NO"].ToString(), dataRow["CAR_NO"].ToString(), dataRow["MAT_CODE_9"].ToString(), dataRow["WEIGHT_9"].ToString(), dataRow["REC_TIME"].ToString());
                }
                if (!string.IsNullOrEmpty(dataRow["MAT_CODE_10"].ToString()))
                {

                    dtResult.Rows.Add(dataRow["PLAN_NO"].ToString(), dataRow["CAR_NO"].ToString(), dataRow["MAT_CODE_10"].ToString(), dataRow["WEIGHT_10"].ToString(), dataRow["REC_TIME"].ToString());
                }
            }

            return dtResult;
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
