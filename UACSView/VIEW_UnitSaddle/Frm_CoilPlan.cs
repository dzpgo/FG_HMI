using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UACSDAL;
using Baosight.iSuperframe.Forms;
using System.Runtime.InteropServices;

namespace UACSView.VIEW_UnitSaddle
{
    /// <summary>
    /// 吊运指令管理
    /// 查询已经分配了的吊运指令
    /// </summary>
    public partial class Frm_CoilPlan : FormBase
    {
        private Dictionary<string, int> dicCheck = new Dictionary<string, int>();
        DataTable dt = new DataTable();
        bool hasSetColumn = false;
        string UnitNo;
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

        public Frm_CoilPlan()
        {
            InitializeComponent();
        }
        #region 方法
        /// <summary>
        /// 查询数据
        /// </summary>
        /// 
        private void BinddataGridView1Data()
        {
            if(comboBox1.Text == "")
            {
                MessageBox.Show("请选择上料鞍座！");
                return; 
            }
            string PackFlag = cbbPackFlag.Text.ToString().Trim();
            string CoilNo = txtCoilNo.Text.ToString().Trim();
            string SaddleNo = txtSaddleNo.Text.ToString().Trim();
            string UnitExit = cbbUnitExit.Text.ToString().Trim();
            string sqlText = @"SELECT 0 AS CHECK_COLUMN,A.NEXT_UNIT_NO,A.COIL_NO,B.STOCK_NO,A.WEIGHT,A.OUTDIA,A.WIDTH,A.INDIA,A.FORBIDEN_FLAG,A.DUMMY_COIL_FLAG,A.PACK_CODE,CASE
                                        WHEN A.PACK_FLAG = 1 THEN '已包装'
                                        ELSE '未包装'
                                    END as  PACK_FLAG,B.BAY_NO";
            sqlText += " FROM UACS_YARDMAP_COIL A";
            sqlText += " LEFT JOIN UACS_YARDMAP_STOCK_DEFINE B ON A.COIL_NO = B.MAT_NO ";
            //sqlText += " WHERE B.MAT_NO != 'NULL' AND (B.STOCK_NO LIKE '%Z23%' OR B.STOCK_NO LIKE '%Z06%')";
            if (comboBox1.Text.Contains("连退包装线") || comboBox1.Text.Contains("D174包装线") || comboBox1.Text.Contains("连退8号鞍座"))
            {
                sqlText += " WHERE B.MAT_NO != 'NULL' AND B.BAY_NO ='Z23'";
                if (UnitExit != "")
                {
                    sqlText += " AND B.STOCK_NO LIKE '%" + UnitExit + "WC%'";
                }
            }
            if (comboBox1.Text.Contains("镀锌包装线") || comboBox1.Text.Contains("2#镀锌5号鞍座"))
            {
                sqlText += " WHERE B.MAT_NO != 'NULL' AND B.BAY_NO ='Z21'";
                if (UnitExit != "")
                {
                    sqlText += " AND B.STOCK_NO LIKE '%" + UnitExit + "WC%'";
                }
            }
            if (comboBox1.Text.Contains("中间跨"))
            {
                sqlText += " WHERE B.MAT_NO != 'NULL' AND B.BAY_NO ='Z22'";
                if (UnitExit != "")
                {                   
                    sqlText += " AND B.STOCK_NO LIKE '%" + UnitExit + "WC%'";
                }
            }

            if (PackFlag != "")
            {
                if (PackFlag == "已包装")
                {
                    PackFlag = "1";
                }
                else if (PackFlag == "未包装")
                {
                    PackFlag = "0";
                }
                sqlText += " AND A.PACK_FLAG = '" + PackFlag + "'";
            }

            if (CoilNo != "")
            {
                sqlText += " AND A.COIL_NO LIKE '%" + CoilNo + "%'";
            }

            if (SaddleNo != "")
            {
                string strStockNO = txtSaddleNo.Text; 
                if(txtSaddleNo.Text.Contains("-"))
                {                   
                    int index = txtSaddleNo.Text.IndexOf("-");
                    strStockNO = String.Format("{0}{1}1", strStockNO.Substring(0, index).PadLeft(2, '0'), strStockNO.Substring(index + 1).PadLeft(2, '0'));
                    sqlText += " AND B.STOCK_NO LIKE '%" + strStockNO + "'";
                }
                else
                {
                    sqlText += " AND B.STOCK_NO LIKE '%" + strStockNO + "%'";
                }
            }

            using (IDataReader rdr = DBHelper.ExecuteReader(sqlText))
            {
                dt.Clear();
                while (rdr.Read())
                {
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < rdr.FieldCount; i++)
                    {
                        if (!hasSetColumn)
                        {
                            DataColumn dc = new DataColumn();
                            dc.ColumnName = rdr.GetName(i);
                            dt.Columns.Add(dc);
                        }
                        dr[i] = rdr[i];
                    }
                    hasSetColumn = true;
                    dt.Rows.Add(dr);
                }
            }
            this.dataGridView1.DataSource = dt;
        }

        private void BinddataGridView2Data()
        {
            if(comboBox1.Text == "")
            {
                return;
            }
            DataTable dt = new DataTable();
            try
            {
                string sqlText = @"SELECT 0 AS CHECK_COLUMN,A.PLAN_SEQ PLAN_SEQ , A.COIL_NO COIL_NO,C.STOCK_NO STOCK_NO, A.WEIGHT WEIGHT,A.WIDTH WIDTH,A.IN_DIA IN_DIA,A.OUT_DIA OUT_DIA, A.PACK_FLAG PACK_FLAG, 
                               (CASE A.STATUS
                                WHEN 100 THEN '库内' 
                                WHEN 200 THEN '起吊' 
                                WHEN 300 THEN '机组鞍座'
                                WHEN 400 THEN '上开卷机'
                                WHEN 500 THEN '开卷完成' 
                                WHEN 410 THEN '上车辆'
                                WHEN 510 THEN '装车出库' 
                                else '其他' 
                                end ) STATUS   
                                ,A.SLEEVE_WIDTH ,
                                (CASE A.COIL_OPEN_DIRECTION 
                                WHEN 0 THEN '上卷取' 
                                WHEN 1 THEN '下卷取' 
                                else '其他' 
                                end ) COIL_OPEN_DIRECTION,
                                A.TIME_LAST_CHANGE TIME_LAST_CHANGE
                                FROM UACS_LINE_L2PLAN A 
                                LEFT JOIN UACS_YARDMAP_COIL B ON B.COIL_NO = A.COIL_NO 
                                LEFT JOIN UACS_YARDMAP_STOCK_DEFINE C ON C.MAT_NO = A.COIL_NO ";
                //sqlText += "WHERE A.UNIT_NO = 'PA-1' order by A.PLAN_SEQ";
                if (comboBox1.Text.Contains("连退包装线"))
                {
                    sqlText += " WHERE A.UNIT_NO = 'PA-1' order by A.PLAN_SEQ";
                }
                if(comboBox1.Text.Contains("D174包装线"))
                {
                    sqlText += " WHERE A.UNIT_NO = 'PA-6' order by A.PLAN_SEQ";
                }
                if (comboBox1.Text.Contains("镀锌包装线") || comboBox1.Text.Contains("2#镀锌5号鞍座"))
                {
                    sqlText += " WHERE A.UNIT_NO = 'PA-2' order by A.PLAN_SEQ";
                }
                if (comboBox1.Text.Contains("中间跨连退出口鞍座"))
                {
                    sqlText += " WHERE A.UNIT_NO = 'D112_Z22' order by A.PLAN_SEQ";
                }
                if (comboBox1.Text.Contains("中间跨镀锌出口鞍座"))
                {
                    sqlText += " WHERE A.UNIT_NO = 'D208_Z22' order by A.PLAN_SEQ";
                }
                if (comboBox1.Text.Contains("连退8号鞍座"))
                {
                    sqlText += " WHERE A.UNIT_NO = 'D112_Z23' order by A.PLAN_SEQ";
                }
                
                dt.Clear();
                dt = new DataTable();

                dt.Columns.Add("CHECK_COLUMN", typeof(Int32));

                dt.Columns.Add("PLAN_SEQ", typeof(String));
                dt.Columns.Add("COIL_NO", typeof(String));
                dt.Columns.Add("WEIGHT", typeof(String));
                dt.Columns.Add("WIDTH", typeof(String));
                dt.Columns.Add("IN_DIA", typeof(String));
                dt.Columns.Add("OUT_DIA", typeof(String));

                //dt.Columns.Add("MANU_VALUE", typeof(String));

                dt.Columns.Add("PACK_FLAG", typeof(String));
                dt.Columns.Add("STATUS", typeof(String));
                dt.Columns.Add("SLEEVE_WIDTH", typeof(String));
                dt.Columns.Add("COIL_OPEN_DIRECTION", typeof(String));
                dt.Columns.Add("TIME_LAST_CHANGE", typeof(String));
                dt.Columns.Add("STOCK_NO", typeof(String));
                //dt.Columns.Add("X_CENTER", typeof(String));

                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sqlText))
                {
                    while (rdr.Read())
                    {
                        DataRow dr = dt.NewRow();
                        if (rdr["CHECK_COLUMN"] != DBNull.Value)
                            dr["CHECK_COLUMN"] = rdr["CHECK_COLUMN"].ToString();
                        else
                            dr["CHECK_COLUMN"] = "";

                        if (rdr["PLAN_SEQ"] != DBNull.Value)
                            dr["PLAN_SEQ"] = rdr["PLAN_SEQ"].ToString();
                        else
                            dr["PLAN_SEQ"] = "";

                        if (rdr["COIL_NO"] != DBNull.Value)
                            dr["COIL_NO"] = rdr["COIL_NO"].ToString();
                        else
                            dr["COIL_NO"] = "";

                        if (rdr["STOCK_NO"] != DBNull.Value)
                            dr["STOCK_NO"] = rdr["STOCK_NO"].ToString();
                        else
                            dr["STOCK_NO"] = "";

                        if (rdr["WEIGHT"] != DBNull.Value)
                            dr["WEIGHT"] = rdr["WEIGHT"].ToString();
                        else
                            dr["WEIGHT"] = "";

                        if (rdr["WIDTH"] != DBNull.Value)
                            dr["WIDTH"] = rdr["WIDTH"].ToString();
                        else
                            dr["WIDTH"] = "";

                        if (rdr["IN_DIA"] != DBNull.Value)
                            dr["IN_DIA"] = rdr["IN_DIA"].ToString();
                        else
                            dr["IN_DIA"] = "";

                        if (rdr["OUT_DIA"] != DBNull.Value)
                            dr["OUT_DIA"] = rdr["OUT_DIA"].ToString();
                        else
                            dr["OUT_DIA"] = "";

                        //if (rdr["MANU_VALUE"] != DBNull.Value)
                        //    dr["MANU_VALUE"] = rdr["MANU_VALUE"].ToString();
                        //else
                        //    dr["MANU_VALUE"] = "";

                        if (rdr["PACK_FLAG"] != DBNull.Value)
                            dr["PACK_FLAG"] = rdr["PACK_FLAG"].ToString();
                        else
                            dr["PACK_FLAG"] = "";

                        if (rdr["STATUS"] != DBNull.Value)
                            dr["STATUS"] = rdr["STATUS"].ToString();
                        else
                            dr["STATUS"] = "";

                        if (rdr["SLEEVE_WIDTH"] != DBNull.Value)
                            dr["SLEEVE_WIDTH"] = rdr["SLEEVE_WIDTH"].ToString();
                        else
                            dr["SLEEVE_WIDTH"] = "";

                        if (rdr["COIL_OPEN_DIRECTION"] != DBNull.Value)
                            dr["COIL_OPEN_DIRECTION"] = rdr["COIL_OPEN_DIRECTION"].ToString();
                        else
                            dr["COIL_OPEN_DIRECTION"] = "";

                        if (rdr["TIME_LAST_CHANGE"] != DBNull.Value)
                            dr["TIME_LAST_CHANGE"] = rdr["TIME_LAST_CHANGE"].ToString();
                        else
                            dr["TIME_LAST_CHANGE"] = "";
              

                        //if (rdr["COIL_NO"] != DBNull.Value)
                        //{
                        //    dr["STOCK_NO"] = getStockByCoilNo(rdr["COIL_NO"].ToString());
                        //    dr["X_CENTER"] = getXCenterByCoilNo(rdr["COIL_NO"].ToString());
                        //}
                        //else
                        //{
                        //    dr["STOCK_NO"] = "";
                        //    dr["X_CENTER"] = "";
                        //}
                        dt.Rows.Add(dr);
                        
                    }
                }
                dataGridView2.CurrentCell = null;
            }
            catch(Exception er)
            {

            }

            //finally
            //{
            //    if (hasSetColumn == false)
            //    {
            //        dt.Columns.Add("STOCK_NO", typeof(String));
            //        dt.Columns.Add("SADDLE_L2NAME", typeof(String));
            //        dt.Columns.Add("COIL_NO", typeof(String));
            //        dt.Columns.Add("WEIGHT", typeof(String));
            //        dt.Columns.Add("WIDTH", typeof(String));
            //        dt.Columns.Add("INDIA", typeof(String));
            //        dt.Columns.Add("OUTDIA", typeof(String));
            //        dt.Columns.Add("COIL_OPEN_DIRECTION", typeof(String));
            //        dt.Columns.Add("COIL_STATUS", typeof(String));
            //    }
            //}
            this.dataGridView2.DataSource = dt;
        }


        //保存勾选的值，防止刷新重置
        private void dgvCheck(DataGridView dgv)
        {
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                foreach (string key in dicCheck.Keys)
                {
                    if (dgv.Rows[i].Cells["COIL_NO2"].Value.ToString() == key)
                    {
                        dgv.Rows[i].Cells["CHECK_COLUMN2"].Value = dicCheck[key];
                    }
                }
            }
        }

        //删除计划顺序后重新排序
        private void RefleshPlanOrder()
        {
            if (dataGridView2.Rows.Count == 0)
            {
                //string sqlSelhalfnextcoil = string.Format(@"UPDATE UACS_LINE_NEXTCOIL SET NEXT_COILNO = '' WHERE UNIT_NO = 'PA-1'");
                //DBHelper.ExecuteNonQuery(sqlSelhalfnextcoil);
                return;
            }
            else
            {
                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {

                    string coil_NO = dataGridView2.Rows[i].Cells["COIL_NO2"].Value.ToString();
                    int plan_SEQ = i;
                    string sql = string.Format("UPDATE UACS_LINE_L2PLAN SET PLAN_SEQ = {1} WHERE COIL_NO = '{0}' AND UNIT_NO = '{2}'", coil_NO, plan_SEQ, UnitNo);
                    DBHelper.ExecuteNonQuery(sql);

                    //if (i == 0)
                    //{
                    //    string sqlSelhalfnextcoil = string.Format(@"UPDATE UACS_LINE_NEXTCOIL SET NEXT_COILNO = '{0}' WHERE UNIT_NO = 'PA-1'", coil_NO);
                    //    DBHelper.ExecuteNonQuery(sqlSelhalfnextcoil);
                    //}
                }
            }
        }

        #endregion

        #region 事件
        /// <summary>
        /// 页面加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CraneOrderHisyManage_Load(object sender, EventArgs e)
        {
            BinddataGridView2Data();
        }


        /// <summary>
        /// 增加按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click_1(object sender, EventArgs e)
        {
            //string UnitNo = "PA-1";
            string status = "100";
            string NextCoil;
            long count = 0;
            string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            try
            {
                foreach (DataGridViewRow r in dataGridView1.Rows)
                {
                    if (r.Cells["CHECK_COLUMN"].Value != null && Convert.ToInt32(r.Cells["CHECK_COLUMN"].Value) == 1)
                    {
                        NextCoil = r.Cells["COIL_NO"].Value.ToString();
                        string sqlSelhalfplan = string.Format(@"SELECT MAX(PLAN_SEQ) AS NUM FROM UACS_LINE_L2PLAN WHERE UNIT_NO = '{0}'", UnitNo);
                        using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sqlSelhalfplan))
                        {
                            rdr.Read();
                            //if (String.IsNullOrEmpty(rdr["NUM"].ToString()))
                            //{
                            //    count = 0;
                            //    string sqlSelhalfnextcoil = string.Format(@"UPDATE UACS_LINE_NEXTCOIL SET NEXT_COILNO = '{0}' WHERE UNIT_NO = '{1}'", NextCoil, UnitNo);
                            //    DBHelper.ExecuteNonQuery(sqlSelhalfnextcoil);
                            //}
                            //else
                            if (!String.IsNullOrEmpty(rdr["NUM"].ToString()))
                            {
                                count = Convert.ToInt64(rdr["NUM"])+1;
                            }
                        }
                        string sqlSelhalfcoil = string.Format(@"SELECT * FROM UACS_LINE_L2PLAN WHERE UNIT_NO = '{0}' AND COIL_NO= '{1}'", UnitNo, NextCoil);
                        using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sqlSelhalfcoil))
                        {
                            if (!rdr.Read())
                            {
                                string sqlInshalfinsert = string.Format(@"INSERT INTO UACS_LINE_L2PLAN (UNIT_NO,PLAN_SEQ,COIL_NO,TIME_LAST_CHANGE,STATUS) VALUES ('{0}','{1}','{2}','{3}','{4}')", UnitNo, count, NextCoil, time, status);
                                DBHelper.ExecuteNonQuery(sqlInshalfinsert);
                            }
                            else
                            {
                                MessageBox.Show("您所勾选的钢卷，计划顺序中已存在，请重新勾选钢卷添加!");
                                foreach (DataGridViewRow r2 in dataGridView1.Rows)
                                {
                                    r2.Cells["CHECK_COLUMN"].Value = 0;
                                }
                                return;
                            }

                        }
                    }
                }
                foreach (DataGridViewRow r in dataGridView1.Rows)
                {
                    r.Cells["CHECK_COLUMN"].Value = 0;
                }
            }
            catch(Exception er)
            {
                MessageBox.Show(er.ToString());
            }
            BinddataGridView2Data();
            dgvCheck(dataGridView2);
            RefleshPlanOrder();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要删除勾选的计划？", "删除提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                try
                {
                    List<string> dt = new List<string>();
                    foreach (DataGridViewRow r in dataGridView2.Rows)
                    {
                        if (r.Cells["CHECK_COLUMN2"].Value != null && (Int32)r.Cells["CHECK_COLUMN2"].Value == 1)
                        {
                            string coilNO = r.Cells["COIL_NO2"].Value.ToString();
                            string sql = string.Format("DELETE FROM UACS_LINE_L2PLAN WHERE COIL_NO = '{0}'", coilNO);
                            dt.Add(sql);
                        }
                    }
                    foreach (string sql in dt)
                    {
                        DBHelper.ExecuteNonQuery(sql);
                    }
                    MessageBox.Show(string.Format("成功删除{0}条记录！", dt.Count));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            dicCheck.Clear();
            BinddataGridView2Data();
            dgvCheck(dataGridView2);
            RefleshPlanOrder();
        }

        private void btnCoilSelect_Click(object sender, EventArgs e)
        {
            BinddataGridView1Data();
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            BinddataGridView2Data();
            dgvCheck(dataGridView2);
            RefleshPlanOrder(); 
        }
        #endregion

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                //if (i != e.RowIndex && dataGridView2.CurrentCell.ColumnIndex == 0)
                //{
                //    DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)dataGridView2.Rows[i].Cells[0];
                //    cell.Value = false;
                //}
                string coil_NO = dataGridView2.Rows[i].Cells["COIL_NO2"].Value.ToString();
                bool hasChecked = (bool)dataGridView2.Rows[i].Cells["CHECK_COLUMN2"].EditedFormattedValue;
                if (hasChecked)
                {
                    dicCheck[coil_NO] = 1;
                }
                else
                {
                    dicCheck[coil_NO] = 0;
                }
                dataGridView2.Rows[i].Cells["CHECK_COLUMN2"].Value = dicCheck[coil_NO];
            }
        }

        private void checBox_All_Details_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                dataGridView2.Rows[i].Cells["CHECK_COLUMN2"].Value = checBox_All_Details.Checked;
                string coil_NO = dataGridView2.Rows[i].Cells["COIL_NO2"].Value.ToString();
                bool hasChecked = (bool)dataGridView2.Rows[i].Cells["CHECK_COLUMN2"].EditedFormattedValue;
                if (hasChecked)
                {
                    dicCheck[coil_NO] = 1;
                }
                else
                {
                    dicCheck[coil_NO] = 0;

                }
                dataGridView2.Rows[i].Cells["CHECK_COLUMN2"].Value = dicCheck[coil_NO];
            }          
        }

        private void btn_MoveUp_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要上移勾选的计划？", "上移提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                try
                {
                    int count = 0;
                    for (int i = 0; i < dataGridView2.Rows.Count; i++)
                    {
                        if (dataGridView2.Rows[i].Cells["CHECK_COLUMN2"].Value != null && (Int32)dataGridView2.Rows[i].Cells["CHECK_COLUMN2"].Value == 1)
                        {
                            count++;
                        }
                    }
                    if(count < 1 || count > 1)
                    {
                        MessageBox.Show("计划上移，请单独勾选一项");
                        return;
                    }

                    for (int i = 0; i < dataGridView2.Rows.Count; i++)
                    {
                        if (dataGridView2.Rows[i].Cells["CHECK_COLUMN2"].Value != null && (Int32)dataGridView2.Rows[i].Cells["CHECK_COLUMN2"].Value == 1)
                        {
                            if(i > 0 )
                            {
                                string coilNO_DOWN = dataGridView2.Rows[i - 1].Cells["COIL_NO2"].Value.ToString();
                                string coilNO_UP = dataGridView2.Rows[i].Cells["COIL_NO2"].Value.ToString();
                                int PlanSEQ_UP = i - 1;
                                int PlanSEQ_DOWN = i;

                                string sql_UP = string.Format("UPDATE UACS_LINE_L2PLAN SET PLAN_SEQ = {0} WHERE COIL_NO = '{1}'", PlanSEQ_UP, coilNO_UP);
                                DBHelper.ExecuteNonQuery(sql_UP);

                                string sql_DOWN = string.Format("UPDATE UACS_LINE_L2PLAN SET PLAN_SEQ = {0} WHERE COIL_NO = '{1}'", PlanSEQ_DOWN, coilNO_DOWN);
                                DBHelper.ExecuteNonQuery(sql_DOWN);

                                BinddataGridView2Data();
                            }
                        }
                    }
                    MessageBox.Show("上移成功！");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            dicCheck.Clear();
            BinddataGridView2Data();
            dgvCheck(dataGridView2);
            RefleshPlanOrder();
        }

        private void btn_MoveDown_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要下移勾选的计划？", "下移提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                try
                {
                    int count = 0;
                    for (int i = 0; i < dataGridView2.Rows.Count; i++)
                    {
                        if (dataGridView2.Rows[i].Cells["CHECK_COLUMN2"].Value != null && (Int32)dataGridView2.Rows[i].Cells["CHECK_COLUMN2"].Value == 1)
                        {
                            count++;
                        }
                    }
                    if (count < 1 || count > 1)
                    {
                        MessageBox.Show("计划下移，请单独勾选一项");
                        return;
                    }

                    for (int i = 0; i < dataGridView2.Rows.Count; i++)
                    {
                        if (dataGridView2.Rows[i].Cells["CHECK_COLUMN2"].Value != null && (Int32)dataGridView2.Rows[i].Cells["CHECK_COLUMN2"].Value == 1)
                        {
                            if (i < dataGridView2.Rows.Count-1)
                            {
                                string coilNO_DOWN = dataGridView2.Rows[i].Cells["COIL_NO2"].Value.ToString();
                                string coilNO_UP = dataGridView2.Rows[i + 1].Cells["COIL_NO2"].Value.ToString();
                                int PlanSEQ_UP = i;
                                int PlanSEQ_DOWN = i + 1;

                                string sql_DOWN = string.Format("UPDATE UACS_LINE_L2PLAN SET PLAN_SEQ = {0} WHERE COIL_NO = '{1}'", PlanSEQ_DOWN, coilNO_DOWN);
                                DBHelper.ExecuteNonQuery(sql_DOWN);

                                string sql_UP = string.Format("UPDATE UACS_LINE_L2PLAN SET PLAN_SEQ = {0} WHERE COIL_NO = '{1}'", PlanSEQ_UP, coilNO_UP);
                                DBHelper.ExecuteNonQuery(sql_UP);

                                BinddataGridView2Data();
                            }
                        }
                    }
                    MessageBox.Show("下移成功！");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            dicCheck.Clear();
            BinddataGridView2Data();
            dgvCheck(dataGridView2);
            RefleshPlanOrder();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Hide_cbbUnitExit();
            if (comboBox1.Text.Contains("连退包装线"))
            {
                UnitNo = "PA-1";
                BindAndShow_cbbUnitExit();
            }
            if (comboBox1.Text.Contains("D174包装线"))
            {
                UnitNo ="PA-6";
            }
            if (comboBox1.Text.Contains("镀锌包装线"))
            {
                UnitNo = "PA-2";
            }
            if (comboBox1.Text.Contains("2#镀锌5号鞍座"))
            {
                UnitNo = "PA-2";
                BindAndShow_cbbUnitExit();
            }
            if (comboBox1.Text.Contains("中间跨连退出口鞍座"))
            {
                UnitNo = "D112_Z22";
                BindAndShow_cbbUnitExit();
            }
            if (comboBox1.Text.Contains("中间跨镀锌出口鞍座"))
            {
                UnitNo = "D208_Z22";
                BindAndShow_cbbUnitExit();
            }
            if (comboBox1.Text.Contains("连退8号鞍座"))
            {
                UnitNo = "D112_Z23";              
            }

            //dataGridView1.Rows.Clear();
            BinddataGridView1Data();
            BinddataGridView2Data();
        }
        private void BindAndShow_cbbUnitExit()
        {
            if (comboBox1.Text.Contains("中间跨连退出口鞍座") || comboBox1.Text.Contains("中间跨镀锌出口鞍座"))
            {
                cbbUnitExit.Items.Clear();
                cbbUnitExit.Items.Add("D171");
                cbbUnitExit.Items.Add("D172");
                cbbUnitExit.Items.Add("D112");
                cbbUnitExit.Items.Add("D208");
            }
            else if (comboBox1.Text.Contains("2#镀锌5号鞍座"))
            {
                cbbUnitExit.Items.Clear();
                cbbUnitExit.Items.Add("D173");
            }
            else if (comboBox1.Text.Contains("连退包装线"))
            {
                cbbUnitExit.Items.Clear();
                cbbUnitExit.Items.Add("D174");
            }
            label10.Visible = true;
            cbbUnitExit.Visible = true;
        }

        private void Hide_cbbUnitExit()
        {
            label10.Visible = false;
            cbbUnitExit.Text = "";
            //cbbUnitExit.Items.Clear();
            cbbUnitExit.Visible = false;
        }

        private void cbbUnitExit_SelectedIndexChanged(object sender, EventArgs e)
        {
            BinddataGridView1Data();
        }
    }
}
