using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UACSDAL;
using Baosight.iSuperframe.TagService;
using Baosight.iSuperframe.TagService.Interface;
using Baosight.iSuperframe.Forms;
using System.Runtime.InteropServices;

namespace UACSView.CLTSHMI
{
    /// <summary>
    /// 吊运指令管理
    /// 查询已经分配了的吊运指令
    /// </summary>
    public partial class FrmCLTS_CAR_TO_YARD : FormBase
    {
        private Dictionary<string, int> dicCheck = new Dictionary<string, int>();
        DataTable dt = new DataTable();
        DataTable dt2 = new DataTable();
        bool hasSetColumn = false;
        bool hasSetColumn2 = false;
        

        private bool isCarArrive = false;
        private string parkingNo = "";
        private string carNo = "";
        private string bayNo = "Z01";
        

        #region -------------------数据库连接-------------------
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

        #region -------------------TAG配置------------------------------
        Baosight.iSuperframe.TagService.DataCollection<object> TagValues = new DataCollection<object>();
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

        private void TrigTag(string tagName, string tagValue)
        {
            try
            {
                if (tagValue.Length != 0 && tagName.Length != 0)
                {
                    tagDP.SetData(tagName, tagValue);
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        #endregion

        public FrmCLTS_CAR_TO_YARD()
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
            if(isCarArrive == false)
            {
                MessageBox.Show("该停车位未做车到位，请先做车到位！");
                return; 
            }
            string stowageID = "";
            string strStowage = @"SELECT STOWAGE_ID FROM UACS_TRUCK_STOWAGE ";
            strStowage += " WHERE FRAME_NO = '" + txtCarNo.Text.ToString().Trim() + "' ORDER BY REC_TIME DESC FETCH FIRST 1 ROWS ONLY";
            using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(strStowage))
            {
                while (rdr.Read())
                {
                    stowageID = rdr["STOWAGE_ID"].ToString().Trim();
                }
            }
            if (stowageID != "")
            {
                string strSq = @"SELECT 0 AS CHECK_COLUMN,A.STOWAGE_ID STOWAGE_ID,A.MAT_NO COIL_NO, B.WEIGHT WEIGHT, B.WIDTH WIDTH, B.INDIA INDIA, B.OUTDIA OUTDIA 
                                    FROM UACS_TRUCK_STOWAGE_DETAIL A
                                    LEFT JOIN UACS_YARDMAP_COIL B ON B.COIL_NO = A.MAT_NO ";
                strSq += " WHERE A.STOWAGE_ID  = '" + stowageID + "'";
                dt.Clear();
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(strSq))
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
        }

        private void BinddataGridView2Data(DataGridView dgv)
        {
            //DataTable dt = new DataTable();
            try
            {
                //string sqlText = @"SELECT 0 AS CHECK_COLUMN,SEQ_NO,MAT_NO COIL_NO,PARKING_NO,CAR_NO, 
                //                 (CASE STATUS WHEN '1' THEN '指令已经生成' ELSE '初始状态' END) STATUS
                //                FROM CLTS_CAR_TO_YARD ";
                //sqlText += "WHERE PARKING_NO = '"+ parkingNo + "' AND CAR_NO = '"+ carNo + "' ORDER BY SEQ_NO";
                //dt2.Clear();
                //using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sqlText))
                //{
                //    dt2.Clear();
                //    while (rdr.Read())
                //    {
                //        DataRow dr = dt2.NewRow();
                //        for (int i = 0; i < rdr.FieldCount; i++)
                //        {
                //            if (!hasSetColumn2)
                //            {
                //                DataColumn dc = new DataColumn();
                //                dc.ColumnName = rdr.GetName(i);
                //                dt2.Columns.Add(dc);
                //            }
                //            dr[i] = rdr[i];
                //        }
                //        hasSetColumn2 = true;
                //        dt2.Rows.Add(dr);
                //    }
                //}
                //dgv.DataSource = dt2;
            }
            catch(Exception er)
            {
                MessageBox.Show(er.ToString());
            }
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
                return;
            }
            else
            {
                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {

                    string coil_NO = dataGridView2.Rows[i].Cells["COIL_NO2"].Value.ToString();
                    int plan_SEQ = i;
                    string sql = string.Format("UPDATE CLTS_CAR_TO_YARD SET SEQ_NO = {1} WHERE MAT_NO = '{0}'", coil_NO, plan_SEQ);
                    DBHelper.ExecuteNonQuery(sql);
                }
            }
        }

        //绑定跨号
        private void Bind_cbbParkingNo(ComboBox comBox)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TypeValue");
            dt.Columns.Add("TypeName");
            //cmbArea.Items.Clear();
            try
            {
                string sqlText = @"SELECT DISTINCT ID as TypeValue,NAME as TypeName FROM UACS_YARDMAP_PARKINGSITE WHERE YARD_NO = '"+ bayNo + "'";
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

            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0},{1}", ex.StackTrace.ToString(), ex.Message.ToString()));
            }
        }

        //车到位
        private void carArrive()
        {
            parkingNo = cbbParkingNo.Text.Trim();
            carNo = txtCarNo.Text.Trim();
            isCarArrive = true;
            cbbParkingNo.Enabled = false;
            txtCarNo.Enabled = false;
            
        }

        //车离开
        private void carLeave()
        {
            //TrigTag("EV_NEW_PARKING_CARLEAVE", cbbParkingNo.Text.Trim());
            isCarArrive = false;           
            cbbParkingNo.Enabled = true;
            txtCarNo.Enabled = true;
            dt.Clear();
            this.dataGridView1.DataSource = dt;
            //dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
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
            TagValues.Clear();
            TagValues.Add("MatInfQuery", null);
            TagValues.Add("EV_NEW_PARKING_CARLEAVE", null);
            TagDP.Attach(TagValues);                      
            Bind_cbbParkingNo(cbbParkingNo);
        }


        /// <summary>
        /// 增加按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click_1(object sender, EventArgs e)
        {
            string NextCoil;
            //long count = 0;
            string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            if (isCarArrive == false)
            {
                MessageBox.Show("该停车位未做车到位，请先做车到位！");
                return;
            }

            try
            {
                foreach (DataGridViewRow r in dataGridView1.Rows)
                {
                    if (r.Cells["CHECK_COLUMN"].Value != null && Convert.ToInt32(r.Cells["CHECK_COLUMN"].Value) == 1)
                    {
                        NextCoil = r.Cells["COIL_NO"].Value.ToString();
                        //string sqlSelhalfplan = string.Format(@"SELECT MAX(SEQ_NO) AS NUM FROM CLTS_CAR_TO_YARD WHERE PARKING_NO= '{0}' AND CAR_NO= '{1}'", parkingNo, carNo);
                        //using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sqlSelhalfplan))
                        //{
                        //    rdr.Read();
                        //    if (!String.IsNullOrEmpty(rdr["NUM"].ToString()))
                        //    {
                        //        count = Convert.ToInt64(rdr["NUM"]) + 1;
                        //    }
                        //}
                        //string sqlSelhalfcoil = string.Format(@"SELECT * FROM CLTS_CAR_TO_YARD WHERE MAT_NO = '{0}' AND PARKING_NO= '{1}' AND CAR_NO= '{2}'", NextCoil, parkingNo,carNo);
                        //using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sqlSelhalfcoil))
                        //{
                        //    if (!rdr.Read())
                        //    {
                        //        string sqlInshalfinsert = string.Format(@"INSERT INTO CLTS_CAR_TO_YARD (SEQ_NO,MAT_NO,PARKING_NO,DEST_YARD,CAR_NO,STATUS) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}')", count, NextCoil, parkingNo, bayNo, carNo, "0");
                        //        DBHelper.ExecuteNonQuery(sqlInshalfinsert);
                        //    }
                        //    else
                        //    {
                        //        MessageBox.Show("您所勾选的钢卷，计划中已存在，请重新勾选钢卷添加!");
                        //        foreach (DataGridViewRow r2 in dataGridView1.Rows)
                        //        {
                        //            r2.Cells["CHECK_COLUMN"].Value = 0;
                        //        }
                        //        return;
                        //    }
                        //}

                        foreach(DataGridViewRow dgvR in dataGridView2.Rows)
                        {
                            //判断材料号是否相同
                            if(dgvR.Cells["COIL_NO2"].Value.ToString() != "")
                            {
                                if(dgvR.Cells["COIL_NO2"].Value.ToString() == NextCoil)
                                {
                                    MessageBox.Show(string.Format("该材料:{0}已经选择，请重新选择材料号！", NextCoil));
                                    return;
                                }
                            }
                        }
                        dataGridView2.Rows.Add("0", (dataGridView2.Rows.Count + 1).ToString(), NextCoil, parkingNo, carNo);

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
            //BinddataGridView2Data(dataGridView2);
            //dgvCheck(dataGridView2);
            //RefleshPlanOrder();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (isCarArrive == false)
            {
                MessageBox.Show("该停车位未做车到位，请先做车到位！");
                return;
            }

            if (MessageBox.Show("确定要删除勾选的计划？", "删除提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                try
                {
                    //List<string> dt = new List<string>();
                    foreach (DataGridViewRow r in dataGridView2.Rows)
                    {
                        if (r.Cells["CHECK_COLUMN2"].Value != null && Convert.ToInt32(r.Cells["CHECK_COLUMN2"].Value) == 1)
                        {
                            //string coilNO = r.Cells["COIL_NO2"].Value.ToString();
                            //string sql = string.Format("DELETE FROM CLTS_CAR_TO_YARD WHERE MAT_NO = '{0}' AND PARKING_NO= '{1}' AND CAR_NO= '{2}'", coilNO, parkingNo, carNo);
                            //dt.Add(sql);

                            dataGridView2.Rows.RemoveAt(r.Index);                           
                        }
                    }

                    //删除数据后重新排序
                    foreach (DataGridViewRow dgvR in dataGridView2.Rows)
                    {
                        //判断材料号是否相同
                        if (dgvR.Cells["COIL_NO2"].ToString() != "")
                        {
                            dgvR.Cells["SEQ_NO2"].Value = (dgvR.Index + 1).ToString();
                        }
                    }
                    //foreach (string sql in dt)
                    //{
                    //    DBHelper.ExecuteNonQuery(sql);
                    //}
                    //MessageBox.Show(string.Format("成功删除{0}条记录！", dt.Count));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            //dicCheck.Clear();
            //BinddataGridView2Data(dataGridView2);
            //dgvCheck(dataGridView2);
            //RefleshPlanOrder();
        }

        private void btnCoilSelect_Click(object sender, EventArgs e)
        {
            BinddataGridView1Data();
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            //BinddataGridView2Data(dataGridView2);
            //dgvCheck(dataGridView2);
            //RefleshPlanOrder(); 
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

                                BinddataGridView2Data(dataGridView2);
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
            BinddataGridView2Data(dataGridView2);
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

                                BinddataGridView2Data(dataGridView2);
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
            BinddataGridView2Data(dataGridView2);
            dgvCheck(dataGridView2);
            RefleshPlanOrder();
        }

        private void btnCarArrive_Click(object sender, EventArgs e)
        {           
            carArrive();
        }

        private void btnCarLeave_Click(object sender, EventArgs e)
        {           
            carLeave();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (isCarArrive == false)
            {
                MessageBox.Show("该停车位未做车到位，请先做车到位！");
                return;
            }

            try
            {
                List<string> dt1 = new List<string>();
                foreach (DataGridViewRow r in dataGridView2.Rows)
                {
                    string seqNo = r.Cells["SEQ_NO2"].Value.ToString();
                    string coilNo = r.Cells["COIL_NO2"].Value.ToString().Trim();
                    string sqlOrderCoil = string.Format(@"SELECT * FROM CLTS_ORDER WHERE MAT_NO = '{0}'", coilNo);
                    string sqlPlanCoil = string.Format(@"SELECT * FROM CLTS_CAR_TO_YARD WHERE MAT_NO = '{0}'", coilNo);
                    using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sqlOrderCoil))
                    {
                        if(rdr.Read())
                        {
                            MessageBox.Show("任务指令中已存在该钢卷:" + coilNo + "指令，该钢卷无法重复生成入库计划，请重新选卷提交!");
                            return;
                        }
                    }
                    using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sqlPlanCoil))
                    {
                        while(rdr.Read())
                        {
                            if (rdr["STATUS"] != DBNull.Value)
                            {
                                string status = rdr["STATUS"].ToString().Trim();
                                if(status == "0")
                                {
                                    MessageBox.Show("入库计划中已存在该钢卷:" + coilNo + "计划，该钢卷无法重复生成入库计划，请重新选卷提交!");
                                    return;
                                }
                            }
                        }
                    }
                    string sqlInshalfinsert = string.Format(@"INSERT INTO CLTS_CAR_TO_YARD (SEQ_NO,MAT_NO,PARKING_NO,DEST_YARD,CAR_NO,STATUS) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}')", seqNo, coilNo, parkingNo, bayNo, carNo, "0");
                    dt1.Add(sqlInshalfinsert);
                    TrigTag("MatInfQuery", coilNo);
                }
                foreach (string sql in dt1)
                {
                    DBHelper.ExecuteNonQuery(sql);
                }
                MessageBox.Show(string.Format("成功生成{0}条行车任务指令！", dt1.Count));
                carLeave();               
            }
            catch (Exception er)
            {
                MessageBox.Show(er.ToString());
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //for (int i = 0; i < dataGridView1.Rows.Count; i++)
            //{
            //    //if (i != e.RowIndex && dataGridView2.CurrentCell.ColumnIndex == 0)
            //    //{
            //    //    DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)dataGridView2.Rows[i].Cells[0];
            //    //    cell.Value = false;
            //    //}
            //    string coil_NO = dataGridView1.Rows[i].Cells["COIL_NO"].Value.ToString();
            //    bool hasChecked = (bool)dataGridView1.Rows[i].Cells["CHECK_COLUMN"].EditedFormattedValue;
            //    if (hasChecked)
            //    {
            //        dicCheck[coil_NO] = 1;
            //    }
            //    else
            //    {
            //        dicCheck[coil_NO] = 0;
            //    }
            //    dataGridView1.Rows[i].Cells["CHECK_COLUMN"].Value = dicCheck[coil_NO];
            //}
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].Cells["CHECK_COLUMN"].Value = checkBox1.Checked;
                string coil_NO = dataGridView1.Rows[i].Cells["COIL_NO"].Value.ToString();
                bool hasChecked = (bool)dataGridView1.Rows[i].Cells["CHECK_COLUMN"].EditedFormattedValue;
                if (hasChecked)
                {
                    dicCheck[coil_NO] = 1;
                }
                else
                {
                    dicCheck[coil_NO] = 0;

                }
                dataGridView1.Rows[i].Cells["CHECK_COLUMN"].Value = dicCheck[coil_NO];
            }          
        }

        
    }
}
