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
    /// 吊运指令管理
    /// 查询已经分配了的吊运指令
    /// 料格信息管理
    /// </summary>
    public partial class MaterialCellManage : FormBase
    {
        DataTable dt = new DataTable();
        DataTable dt_oper = new DataTable();
        DataTable dt_ack = new DataTable();
        CraneOrderImpl craneOrderImpl = new CraneOrderImpl();
        DataTable dtAREA_DEFINE1 = new DataTable();
        DataTable dtAREA_DEFINE2 = new DataTable();
        DataTable dtGRID_DEFINE1 = new DataTable();
        DataTable dtGRID_DEFINE2 = new DataTable();
        bool hasSetColumn = false;
        bool hasSetColumn_oper = false;
        bool hasSetColumn_ack = false;
        private static Baosight.iSuperframe.Common.IDBHelper DBHelper = null;
        Dictionary<string, string> dicCmdStatus = new Dictionary<string, string>();
        Dictionary<string, string> dicFlagDispat = new Dictionary<string, string>();
        Dictionary<string, string> dicDelFlag = new Dictionary<string, string>();
        Dictionary<string, string> dicOrderType = new Dictionary<string, string>();
        public MaterialCellManage()
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
                //this.panel1.BackColor = UACSDAL.ColorSln.FormBgColor;
                //this.panel2.BackColor = UACSDAL.ColorSln.FormBgColor;
                //绑定下拉框
                BindCombox();
                //
                //this.dateTimePicker1_recTime.Value = DateTime.Now.AddDays(-1);

                //加载初始化数据
                GetYARDMAP_GRID_DEFINE();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        /// <summary>
        /// 修改按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                if(cbb_GridNo2.Text.Trim() != "")
                {
                    if (txt_gridStatus.Text != "" && txt_matCode.Text != "" && txt_matWeight.Text != "")
                    {
                        string sql = "UPDATE UACS_YARDMAP_GRID_DEFINE SET GRID_STATUS = '" + txt_gridStatus.Text + "',MAT_CODE = '" + txt_matCode.Text + "',MAT_WGT = '" + txt_matWeight.Text + "' WHERE GRID_NO = '" + cbb_GridNo2.SelectedValue.ToString().Trim() + "'";
                        DB2Connect.DBHelper.ExecuteNonQuery(sql);
                        MessageBox.Show("修改成功");
                        ParkClassLibrary.HMILogger.WriteLog(btnOk.Text, "修改", ParkClassLibrary.LogLevel.Info, this.Text);
                    }
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
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

        private void dataGridView2_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void dataGridView3_DataError(object sender, DataGridViewDataErrorEventArgs e)
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

            //绑定跨号
            DataTable dtBAY_DEFINE = craneOrderImpl.GetBayNo(true);
            bindCombox(this.cbb_BayNo1, dtBAY_DEFINE, true);
            bindCombox(this.cbb_BayNo2, dtBAY_DEFINE, true);
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        private void getCraneOrderData()
        {
            //string matNo = this.txt_MAT_CODE.Text.Trim();
            //string bayNo = this.cbb_AREA_NO.SelectedValue.ToString();
            //string cmdStatus = this.cbb_ORDER_TYPE.SelectedValue.ToString();
            //string recTime1 = this.dateTimePicker1_recTime.Value.ToString("yyyyMMdd000000");
            //string recTime2 = this.dateTimePicker2_recTime.Value.ToString("yyyyMMdd235959");
            //string sqlText = @"SELECT BAY_NO,MAT_NO,ORDER_NO,ORDER_GROUP_NO,ORDER_TYPE,ORDER_PRIORITY,FROM_STOCK_NO, TO_STOCK_NO, CMD_STATUS, FLAG_DISPAT, FLAG_ENABLE, CRANE_NO, REC_TIME, UPD_TIME FROM UACS_CRANE_ORDER ";
            //sqlText += "WHERE MAT_NO LIKE '%{0}%' and REC_TIME > '{1}' and REC_TIME < '{2}' ";
            //sqlText = string.Format(sqlText, matNo, recTime1, recTime2);
            //if (bayNo != "全部")
            //{
            //    sqlText = string.Format("{0} and BAY_NO = '{1}'", sqlText, bayNo);
            //}
            //if (cmdStatus != "全部")
            //{
            //    sqlText = string.Format("{0} and CMD_STATUS = '{1}'", sqlText, cmdStatus);
            //}
            //dt = new DataTable();
            //hasSetColumn = false;
            //using (IDataReader rdr = DBHelper.ExecuteReader(sqlText))
            //{
            //    while (rdr.Read())
            //    {
            //        DataRow dr = dt.NewRow();
            //        for (int i = 0; i < rdr.FieldCount; i++)
            //        {
            //            if (!hasSetColumn)
            //            {
            //                DataColumn dc = new DataColumn();
            //                dc.ColumnName = rdr.GetName(i);
            //                dt.Columns.Add(dc);
            //            }
            //            dr[i] = rdr[i];
            //        }
            //        hasSetColumn = true;
            //        dt.Rows.Add(dr);
            //    }
            //}
            //foreach (DataRow dr in dt.Rows)
            //{
            //    string cmdStatusValue = dr["CMD_STATUS"].ToString();
            //    if (dicCmdStatus.ContainsKey(cmdStatusValue))
            //    {
            //        dr["CMD_STATUS"] = dicCmdStatus[cmdStatusValue];
            //    }
            //    string flagDispatValue = dr["FLAG_DISPAT"].ToString();
            //    if (dicFlagDispat.ContainsKey(flagDispatValue))
            //    {
            //        dr["FLAG_DISPAT"] = dicFlagDispat[flagDispatValue];
            //    }
            //    string orderTypeValue = dr["ORDER_TYPE"].ToString();
            //    if (dicOrderType.ContainsKey(orderTypeValue))
            //    {
            //        dr["ORDER_TYPE"] = dicOrderType[orderTypeValue];
            //    }
            //}
        }

        /// <summary>
        /// 查询指令数据
        /// </summary>
        private void getCraneOrderData2()
        {
            //string matNo = this.txt_MAT_CODE.Text.Trim();
            //string bayNo = this.cbb_AREA_NO.SelectedValue.ToString();
            //string cmdStatus = this.cbb_ORDER_TYPE.SelectedValue.ToString();
            //string recTime1 = this.dateTimePicker1_recTime.Value.ToString("yyyyMMdd000000");
            //string recTime2 = this.dateTimePicker2_recTime.Value.ToString("yyyyMMdd235959");
            //string sqlText = @"SELECT BAY_NO,MAT_NO,ORDER_NO,ORDER_GROUP_NO,ORDER_TYPE,ORDER_PRIORITY,FROM_STOCK_NO, TO_STOCK_NO, CMD_STATUS, FLAG_DISPAT, FLAG_ENABLE, CRANE_NO, REC_TIME, UPD_TIME FROM UACS_CRANE_ORDER ";
            //sqlText += "WHERE MAT_NO LIKE '%{0}%' and REC_TIME > '{1}' and REC_TIME < '{2}' ";
            //sqlText = string.Format(sqlText, matNo, recTime1, recTime2);
            //if (bayNo != "全部")
            //{
            //    sqlText = string.Format("{0} and BAY_NO = '{1}'", sqlText, bayNo);
            //}
            //if (cmdStatus != "全部")
            //{
            //    sqlText = string.Format("{0} and CMD_STATUS = '{1}'", sqlText, cmdStatus);
            //}
            //dt = new DataTable();
            //hasSetColumn = false;
            //using (IDataReader rdr = DBHelper.ExecuteReader(sqlText))
            //{
            //    while (rdr.Read())
            //    {
            //        DataRow dr = dt.NewRow();
            //        for (int i = 0; i < rdr.FieldCount; i++)
            //        {
            //            if (!hasSetColumn)
            //            {
            //                DataColumn dc = new DataColumn();
            //                dc.ColumnName = rdr.GetName(i);
            //                dt.Columns.Add(dc);
            //            }
            //            dr[i] = rdr[i];
            //        }
            //        hasSetColumn = true;
            //        dt.Rows.Add(dr);
            //    }
            //}
            //foreach (DataRow dr in dt.Rows)
            //{
            //    string cmdStatusValue = dr["CMD_STATUS"].ToString();
            //    if (dicCmdStatus.ContainsKey(cmdStatusValue))
            //    {
            //        dr["CMD_STATUS"] = dicCmdStatus[cmdStatusValue];
            //    }
            //    string flagDispatValue = dr["FLAG_DISPAT"].ToString();
            //    if (dicFlagDispat.ContainsKey(flagDispatValue))
            //    {
            //        dr["FLAG_DISPAT"] = dicFlagDispat[flagDispatValue];
            //    }
            //    string orderTypeValue = dr["ORDER_TYPE"].ToString();
            //    if (dicOrderType.ContainsKey(orderTypeValue))
            //    {
            //        dr["ORDER_TYPE"] = dicOrderType[orderTypeValue];
            //    }
            //}
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

        private void btnSelect1_Click(object sender, EventArgs e)
        {
            try
            {
                if(cbb_GridNo1.Text.Trim() != "")
                {
                    string sql = "SELECT ROW_NUMBER() OVER() as ROW_INDEX,A.GRID_NO,A.GRID_STATUS,A.MAT_CODE,C.MAT_CNAME,A.MAT_WGT FROM UACS_YARDMAP_GRID_DEFINE A LEFT JOIN UACS_L3_MAT_INFO C ON C.MAT_CODE = A.MAT_CODE WHERE A.GRID_NO = '" + cbb_GridNo1.SelectedValue.ToString().Trim() + "'";
                    dt.Clear();
                    dt = new DataTable();

                    using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                    {
                        dt.Load(rdr);
                    }
                    dataGridView2.DataSource = dt;
                }

            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }         
        }
        /// <summary>
        /// 初始化加载
        /// </summary>
        private void GetYARDMAP_GRID_DEFINE()
        {
            if (cbb_GridNo1.Text.Trim() != "")
            {
                string sql = "SELECT ROW_NUMBER() OVER() as ROW_INDEX,A.GRID_NO,A.GRID_STATUS,A.MAT_CODE,C.MAT_CNAME,A.MAT_WGT FROM UACS_YARDMAP_GRID_DEFINE A LEFT JOIN UACS_L3_MAT_INFO C ON C.MAT_CODE = A.MAT_CODE WHERE A.GRID_NO = '" + cbb_GridNo1.SelectedValue.ToString().Trim() + "'";
                dt.Clear();
                dt = new DataTable();

                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                {
                    dt.Load(rdr);
                }
                dataGridView2.DataSource = dt;
            }
        }

        private void btnSelect2_Click(object sender, EventArgs e)
        {
            try
            {
                if(txt_matCode1.Text.Trim()!="")
                {
                    string sql = "SELECT ROW_NUMBER() OVER() as ROW_INDEX,A.GRID_NO,A.MAT_CODE,C.MAT_CNAME,A.MAT_WGT FROM UACS_YARDMAP_GRID_DEFINE A LEFT JOIN UACS_L3_MAT_INFO C ON C.MAT_CODE = A.MAT_CODE WHERE A.MAT_CODE = '" + txt_matCode1.Text.ToString().Trim() + "'";
                    dt.Clear();
                    dt = new DataTable();

                    using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                    {
                        dt.Load(rdr);
                    }
                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void cbb_BayNo1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtAREA_DEFINE1.Clear();
            dtAREA_DEFINE1 = craneOrderImpl.GetAREA_DEFINE(false);
            bindCombox(this.cbb_AreaNo1, dtAREA_DEFINE1, true);
        }

        private void cbb_AreaNo1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtGRID_DEFINE1.Clear();
            dtGRID_DEFINE1 = craneOrderImpl.GetGRID_DEFINE(cbb_AreaNo1.SelectedValue.ToString());
            bindCombox(this.cbb_GridNo1, dtGRID_DEFINE1, true);
        }

        private void cbb_BayNo2_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtAREA_DEFINE2.Clear();
            dtAREA_DEFINE2 = craneOrderImpl.GetAREA_DEFINE(false);
            bindCombox(this.cbb_AreaNo2, dtAREA_DEFINE2, true);
        }

        private void cbb_AreaNo2_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtGRID_DEFINE2.Clear();
            dtGRID_DEFINE2 = craneOrderImpl.GetGRID_DEFINE(cbb_AreaNo2.SelectedValue.ToString());
            bindCombox(this.cbb_GridNo2, dtGRID_DEFINE2, true);
        }
    }
}
