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
using UACSPopupForm;


namespace UACSView
{
    public partial class FrmYardToYardStraegy : FormBase
    {

        //private YardToYardRequest yardToYard = new YardToYardRequest();
        private static FrmHotCoilYard2Yard frmHotCoilYard2Yard;
        public FrmYardToYardStraegy()
        {
            InitializeComponent();
            this.Load += FrmYardToYardStraegy_Load;
        }

        #region TAG配置
        private Baosight.iSuperframe.TagService.Controls.TagDataProvider tagDataProvider = new Baosight.iSuperframe.TagService.Controls.TagDataProvider();
        Baosight.iSuperframe.TagService.DataCollection<object> inDatas = new Baosight.iSuperframe.TagService.DataCollection<object>();

        private Baosight.iSuperframe.Authorization.Interface.IAuthorization auth = null;

        private string[] arrTagAdress;
        private void readTags()
        {
            try
            {
                inDatas.Clear();
                tagDataProvider.GetData(arrTagAdress, out inDatas);
            }
            catch (Exception ex)
            {
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

        void FrmYardToYardStraegy_Load(object sender, EventArgs e)
        {
            tagDataProvider.ServiceName = "iplature";
            Bind_cbbBayNo(cbbBayNo);
            Bind_cbbCraneNo(cbbCraneNo);
            GetStatus();
        }

        private void cbbBayNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Bind_cbbCraneNo(cbbCraneNo);
        }

        private void cbbCraneNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //FrmYordToYordConfPassword password = new FrmYordToYordConfPassword();
            //DialogResult ret = password.ShowDialog();
            //if (ret == DialogResult.OK)
            //{
                GetCoilStrategy(cbbCraneNo.Text.Trim(), dgvCoilStrategy);
                GetSaddleStrategy(cbbCraneNo.Text.Trim(), dgvSaddleStrategy);
                GetStatus();
            //}

        }

        //根据跨号绑定行车号
        private void Bind_cbbCraneNo(ComboBox comBox)
        {
            string bayNo = cbbBayNo.SelectedValue.ToString().Trim();
            DataTable dt = new DataTable();
            dt.Columns.Add("TypeValue");
            dt.Columns.Add("TypeName");
            try
            {
                string strSql = @"SELECT ID as TypeValue,NAME as TypeName FROM UACS_YARDMAP_CRANE WHERE BAY_NO = '" + bayNo + "'ORDER BY ID ASC ";
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(strSql))
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
            }
        }

        //绑定跨号
        private void Bind_cbbBayNo(ComboBox comBox)
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
                comBox.SelectedItem = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0},{1}", ex.StackTrace.ToString(), ex.Message.ToString()));
            }
        }


        private void dgvCoilStrategy_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int CoilId;
            int SaddleId;
            if (e.RowIndex >= 0)
            {
                if (dgvCoilStrategy.Rows[e.RowIndex].Cells["CoilId"].Value != DBNull.Value)
                {
                    CoilId = Convert.ToInt32(dgvCoilStrategy.Rows[e.RowIndex].Cells["CoilId"].Value.ToString());

                    for (int i = 0; i < dgvSaddleStrategy.RowCount; i++)
                    {
                        dgvSaddleStrategy.Rows[i].Selected = false;
                        SaddleId = Convert.ToInt32(dgvSaddleStrategy.Rows[i].Cells["SaddleId"].Value.ToString());
                        if (CoilId == SaddleId)
                        {
                            dgvSaddleStrategy.FirstDisplayedScrollingRowIndex = i;
                            dgvSaddleStrategy.Rows[i].Selected = true;
                            dgvSaddleStrategy.CurrentCell = dgvSaddleStrategy.Rows[i].Cells["SaddleId"];
                        }
                    }
                }
            }
        }

        private void dgvSaddleStrategy_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int CoilId;
            int SaddleId;
            if (e.RowIndex >= 0)
            {
                if (dgvSaddleStrategy.Rows[e.RowIndex].Cells["SaddleId"].Value != DBNull.Value)
                {
                    SaddleId = Convert.ToInt32(dgvSaddleStrategy.Rows[e.RowIndex].Cells["SaddleId"].Value.ToString());

                    for (int i = 0; i < dgvCoilStrategy.RowCount; i++)
                    {
                        dgvCoilStrategy.Rows[i].Selected = false;
                        CoilId = Convert.ToInt32(dgvCoilStrategy.Rows[i].Cells["CoilId"].Value.ToString());
                        if (SaddleId == CoilId)
                        {
                            dgvCoilStrategy.FirstDisplayedScrollingRowIndex = i;
                            dgvCoilStrategy.Rows[i].Selected = true;
                            dgvCoilStrategy.CurrentCell = dgvCoilStrategy.Rows[i].Cells["CoilId"];
                        }
                    }
                }
            }
        }


        private void CoilReadOnlyByTrue()
        {
            txtCoilFlagByPlan.ReadOnly = true;
            txtCoilMinNullSaddle.ReadOnly = true;
            txtCoilXDir.ReadOnly = true;
            txtCoilXmax.ReadOnly = true;
            txtCoilXmin.ReadOnly = true;
            txtCoilYmax.ReadOnly = true;
            txtCoilYmin.ReadOnly = true;
        }

        private void CoilReadOnlyByFalse()
        {
            txtCoilFlagByPlan.ReadOnly = false;
            txtCoilMinNullSaddle.ReadOnly = false;
            txtCoilXDir.ReadOnly = false;
            txtCoilXmax.ReadOnly = false;
            txtCoilXmin.ReadOnly = false;
            txtCoilYmax.ReadOnly = false;
            txtCoilYmin.ReadOnly = false;
        }

        private void SaddleReadOnlyByTrue()
        {
            txtSaddleFlagByPlan.ReadOnly = true;
            txtSaddleMinNullSaddle.ReadOnly = true;
            txtSaddleXDir.ReadOnly = true;
            txtSaddleXmax.ReadOnly = true;
            txtSaddleXmin.ReadOnly = true;
            txtSaddleYmax.ReadOnly = true;
            txtSaddleYmin.ReadOnly = true;
        }

        private void SaddleReadOnlyByFalse()
        {
            txtSaddleFlagByPlan.ReadOnly = false;
            txtSaddleMinNullSaddle.ReadOnly = false;
            txtSaddleXDir.ReadOnly = false;
            txtSaddleXmax.ReadOnly = false;
            txtSaddleXmin.ReadOnly = false;
            txtSaddleYmax.ReadOnly = false;
            txtSaddleYmin.ReadOnly = false;
        }


        private void ClearCoilText()
        {
            txtCoilId.Text = "";
            txtCoilXmin.Text = "";
            txtCoilXmax.Text = "";
            txtCoilYmin.Text = "";
            txtCoilYmax.Text = "";
            txtCoilXDir.Text = "";
            txtCoilMinNullSaddle.Text = "";
        }

        private void ClearSaddleText()
        {
            txtSaddleId.Text = "";
            txtSaddleXmin.Text = "";
            txtSaddleXmax.Text = "";
            txtSaddleYmin.Text = "";
            txtSaddleYmax.Text = "";
            txtSaddleXDir.Text = "";
            txtSaddleMinNullSaddle.Text = "";
        }

        private void ShiftCoilByText()
        {
            int index = dgvCoilStrategy.CurrentRow.Index;
            txtCoilId.Text = dgvCoilStrategy.Rows[index].Cells["CoilId"].Value.ToString();
            txtCoilXmin.Text = dgvCoilStrategy.Rows[index].Cells["coilXmin"].Value.ToString();
            txtCoilXmax.Text = dgvCoilStrategy.Rows[index].Cells["coilXmax"].Value.ToString();
            txtCoilYmin.Text = dgvCoilStrategy.Rows[index].Cells["coilYmin"].Value.ToString();
            txtCoilYmax.Text = dgvCoilStrategy.Rows[index].Cells["coilYmax"].Value.ToString();
            txtCoilXDir.Text = dgvCoilStrategy.Rows[index].Cells["coilXDir"].Value.ToString();
            txtCoilMinNullSaddle.Text = dgvCoilStrategy.Rows[index].Cells["coilMinEmptySaddle"].Value.ToString();
            txtCoilFlagByPlan.Text = dgvCoilStrategy.Rows[index].Cells["coilFlagPlan"].Value.ToString();
        }

        private void ShiftSaddleByText()
        {
            int index = dgvSaddleStrategy.CurrentRow.Index;
            txtSaddleId.Text = dgvSaddleStrategy.Rows[index].Cells["SaddleId"].Value.ToString();
            txtSaddleXmin.Text = dgvSaddleStrategy.Rows[index].Cells["saddleXmin"].Value.ToString();
            txtSaddleXmax.Text = dgvSaddleStrategy.Rows[index].Cells["saddleXmax"].Value.ToString();
            txtSaddleYmin.Text = dgvSaddleStrategy.Rows[index].Cells["saddleYmin"].Value.ToString();
            txtSaddleYmax.Text = dgvSaddleStrategy.Rows[index].Cells["saddleYmax"].Value.ToString();
            txtSaddleXDir.Text = dgvSaddleStrategy.Rows[index].Cells["saddleXDir"].Value.ToString();
            txtSaddleMinNullSaddle.Text = dgvSaddleStrategy.Rows[index].Cells["saddleMinEmptySaddle"].Value.ToString();
        }

        private void btnCoilConf_Click(object sender, EventArgs e)
        {
            //FrmYordToYordConfPassword password = new FrmYordToYordConfPassword();
            //DialogResult ret = password.ShowDialog();
            //if (ret == DialogResult.OK)
            //{
                CoilReadOnlyByFalse();
                ShiftCoilByText();
            //}

        }

        private void btnSaddleConf_Click(object sender, EventArgs e)
        {
            //FrmYordToYordConfPassword password = new FrmYordToYordConfPassword();
            //DialogResult ret = password.ShowDialog();
            //if (ret == DialogResult.OK)
            //{
                SaddleReadOnlyByFalse();
                ShiftSaddleByText();
            //}

        }

        private void btnUpCoil_Click(object sender, EventArgs e)
        {

            if (txtCoilId.Text.Trim() == "")
            {
                MessageBox.Show("请先点击--更改起吊配置按钮");
                return;
            }


            string XMin = txtCoilXmin.Text.Trim();
            string XMax = txtCoilXmax.Text.Trim();
            string YMin = txtCoilYmin.Text.Trim();
            string YMax = txtCoilYmax.Text.Trim();
            string XDir = txtCoilXDir.Text.Trim();
            int coilId = Convert.ToInt32(txtCoilId.Text);
            string MinEmptySaddle = txtCoilMinNullSaddle.Text.Trim();
            
            if (XMin != "" && XMax != "" && YMin != "" && YMax != "" && XDir != "" && MinEmptySaddle != "")
            {
                bool flag = UpCoilStrategy(Convert.ToInt32(XMin), Convert.ToInt32(XMax), Convert.ToInt32(YMin), Convert.ToInt32(YMax), XDir, Convert.ToInt32(MinEmptySaddle), coilId);
                if (flag)
                    MessageBox.Show(coilId + "起吊配置保存成功");
                else
                {
                    MessageBox.Show(coilId + "起吊配置保存失败");
                    return;
                }

            }
            else
            {
                MessageBox.Show("起吊配置输入项不完整！！！");
                return;
            }
            ClearCoilText();
            CoilReadOnlyByTrue();
            GetCoilStrategy(cbbCraneNo.Text.Trim(), dgvCoilStrategy);
        }

        private void btnUpSaddle_Click(object sender, EventArgs e)
        {
            if (txtSaddleId.Text.Trim() == "")
            {
                MessageBox.Show("请先点击--更改落卷配置按钮");
                return;
            }

            string XMin = txtSaddleXmin.Text.Trim();
            string XMax = txtSaddleXmax.Text.Trim();
            string YMin = txtSaddleYmin.Text.Trim();
            string YMax = txtSaddleYmax.Text.Trim();
            string XDir = txtSaddleXDir.Text.Trim();
            string MinEmptySaddle = txtSaddleMinNullSaddle.Text.Trim();
            int saddleId = Convert.ToInt32(txtSaddleId.Text);
            if (XMin != "" && XMax != "" && YMin != "" && YMax != "" && XDir != "" && MinEmptySaddle != "")
            {
                bool flag = UpSaddleStrategy(Convert.ToInt32(XMin), Convert.ToInt32(XMax), Convert.ToInt32(YMin), Convert.ToInt32(YMax), XDir, Convert.ToInt32(MinEmptySaddle), saddleId);
                if (flag)
                    MessageBox.Show(saddleId + "落卷配置保存成功");
                else
                {
                    MessageBox.Show(saddleId + "落卷配置保存失败");
                    return;
                }
            }
            else
            {
                MessageBox.Show("落卷配置输入项不完整！！！");
                return;
            }
            ClearSaddleText();
            SaddleReadOnlyByTrue();
            GetSaddleStrategy(cbbCraneNo.Text.Trim(), dgvSaddleStrategy);
        }

        public static void GetCoilStrategy(string _CraneNo, DataGridView _dgv)
        {
            // 标记
            bool hasSetColumn = false;
            DataTable dt = new DataTable();
            try
            {
                string sql = @"SELECT a.DESC,b.COIL_STRATEGY_ID ID,b.SEQ,a.X_MIN,a.X_MAX,a.Y_MIN,a.Y_MAX,a.X_DIR,a.LIST_UNIT_NO,a.MIN_EMPTY_SADDLES,a.FLAG_FIND_BY_PLAN FROM  
                          YARD_TO_YARD_FIND_COIL_STRATEGY a left join YARD_TO_YARD_CRANE_STRAEGY b on  a.ID = b.COIL_STRATEGY_ID  WHERE a.ID in (";
                sql += " SELECT COIL_STRATEGY_ID FROM YARD_TO_YARD_CRANE_STRAEGY WHERE CRANE_NO  = '" + _CraneNo + "')";
                sql += " ORDER BY b.SEQ";
                dt.Clear();
                dt = new DataTable();

                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                {
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
            }
            catch (Exception)
            {

                throw;
            }
            if (hasSetColumn == false)
            {
                dt.Columns.Add("DESC", typeof(String));
                dt.Columns.Add("ID", typeof(String));
                dt.Columns.Add("SEQ", typeof(String));
                dt.Columns.Add("X_MIN", typeof(String));
                dt.Columns.Add("X_MAX", typeof(String));
                dt.Columns.Add("Y_MIN", typeof(String));
                dt.Columns.Add("Y_MAX", typeof(String));
                dt.Columns.Add("X_DIR", typeof(String));
                dt.Columns.Add("LIST_UNIT_NO", typeof(String));
                dt.Columns.Add("MIN_EMPTY_SADDLES", typeof(String));
                dt.Columns.Add("FLAG_FIND_BY_PLAN", typeof(String));
            }

            _dgv.DataSource = dt;
        }

        public static void GetSaddleStrategy(string _CraneNo, DataGridView _dgv)
        {
            bool hasSetColumn = false;
            DataTable dt = new DataTable();
            try
            {
                string sql = @"SELECT a.DESC,b.SADDLE_STRATEGY_ID ID,b.SEQ,a.X_MIN,a.X_MAX,a.Y_MIN,a.Y_MAX,a.X_DIR,a.MIN_EMPTY_SADDLES,
                                CASE
                                  WHEN b.FLAG_ENABLED = 0 THEN '倒垛方案取消'
                                  WHEN b.FLAG_ENABLED = 1 THEN '倒垛方案执行'
                              END as FLAG_ENABLED FROM  
                              YARD_TO_YARD_FIND_SADDLE_STRATEGY a left join YARD_TO_YARD_CRANE_STRAEGY b on  a.ID = b.SADDLE_STRATEGY_ID  WHERE a.ID in (";
                sql += " SELECT SADDLE_STRATEGY_ID FROM YARD_TO_YARD_CRANE_STRAEGY WHERE CRANE_NO  = '" + _CraneNo + "')";
                sql += " ORDER BY b.SEQ";
                dt.Clear();
                dt = new DataTable();

                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                {
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
            }
            catch (Exception)
            {

                throw;
            }
            if (hasSetColumn == false)
            {
                dt.Columns.Add("DESC", typeof(String));
                dt.Columns.Add("ID", typeof(String));
                dt.Columns.Add("SEQ", typeof(String));
                dt.Columns.Add("X_MIN", typeof(String));
                dt.Columns.Add("X_MAX", typeof(String));
                dt.Columns.Add("Y_MIN", typeof(String));
                dt.Columns.Add("Y_MAX", typeof(String));
                dt.Columns.Add("X_DIR", typeof(String));
                dt.Columns.Add("MIN_EMPTY_SADDLES", typeof(String));
                dt.Columns.Add("FLAG_ENABLED", typeof(String));
            }

            _dgv.DataSource = dt;
        }

        public static bool UpCoilStrategy(int _xMin, int _xMax, int _yMin, int _yMax, string _xDir, int _minEmptySaddle, int _id)
        {
            try
            {
                string sql = @"update YARD_TO_YARD_FIND_COIL_STRATEGY set ";
                sql += " X_MIN =  " + _xMin + ",";
                sql += " X_MAX =  " + _xMax + ",";
                sql += " Y_MIN =  " + _yMin + ",";
                sql += " Y_MAX =  " + _yMax + ",";
                sql += " X_DIR =  '" + _xDir + "',";
                sql += " MIN_EMPTY_SADDLES = " + _minEmptySaddle + " ";
                sql += " where ID = " + _id + " ";
                DB2Connect.DBHelper.ExecuteNonQuery(sql);
            }
            catch (Exception er)
            {
                return false;
            }
            return true;
        }

        public static bool UpSaddleStrategy(int _xMin, int _xMax, int _yMin, int _yMax, string _xDir, int _minEmptySaddle, int _id)
        {
            try
            {
                string sql = @"update YARD_TO_YARD_FIND_SADDLE_STRATEGY set ";
                sql += " X_MIN =  " + _xMin + ",";
                sql += " X_MAX =  " + _xMax + ",";
                sql += " Y_MIN =  " + _yMin + ",";
                sql += " Y_MAX =  " + _yMax + ",";
                sql += " X_DIR =  '" + _xDir + "',";
                sql += " MIN_EMPTY_SADDLES = " + _minEmptySaddle + " ";
                sql += " where ID = " + _id + " ";
                DB2Connect.DBHelper.ExecuteNonQuery(sql);
            }
            catch (Exception er)
            {
                return false;
            }
            return true;
        }

        public bool UpCraneFlagEnabledByStop(int _id, string _craneNo)
        {
            try
            {
                string sql = @"update YARD_TO_YARD_CRANE_STRAEGY set FLAG_ENABLED = 0 where CRANE_NO = '" + _craneNo + "' and ID = " + _id + " ";
                DB2Connect.DBHelper.ExecuteNonQuery(sql);
            }
            catch (Exception er)
            {
                return false;
            }
            return true;
        }

        public bool UpCraneFlagEnabledByOpen(int _id, string _craneNo)
        {
            try
            {
                string sql = @"update YARD_TO_YARD_CRANE_STRAEGY set FLAG_ENABLED = 1 where CRANE_NO = '" + _craneNo + "' and ID = " + _id + " ";
                DB2Connect.DBHelper.ExecuteNonQuery(sql);
            }
            catch (Exception er)
            {
                return false;
            }
            return true;
        }

        private void StopScheme_Click(object sender, EventArgs e)
        {
            if (cbbCraneNo.Text.Trim() != "")
            {
                int index = dgvSaddleStrategy.CurrentRow.Index;
                string id = dgvSaddleStrategy.Rows[index].Cells["SaddleId"].Value.ToString();
                bool flag = UpCraneFlagEnabledByStop(Convert.ToInt32(id), cbbCraneNo.Text.Trim());
                if (flag)
                    MessageBox.Show(id + "配载方案停止成功");
                else
                    MessageBox.Show(id + "配载方案停止失败");

                GetSaddleStrategy(cbbCraneNo.Text.Trim(), dgvSaddleStrategy);
            }

        }

        private void OpenScheme_Click(object sender, EventArgs e)
        {
            if (cbbCraneNo.Text.Trim() != "")
            {
                int index = dgvSaddleStrategy.CurrentRow.Index;
                string id = dgvSaddleStrategy.Rows[index].Cells["SaddleId"].Value.ToString();
                bool flag = UpCraneFlagEnabledByOpen(Convert.ToInt32(id), cbbCraneNo.Text.Trim());
                if (flag)
                    MessageBox.Show(id + "配载方案打开成功");
                else
                    MessageBox.Show(id + "配载方案打开失败");

                GetSaddleStrategy(cbbCraneNo.Text.Trim(), dgvSaddleStrategy);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("X从小到大为-ASC" + "\n" + "X从大到小为-DES");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("X从小到大为-ASC" + "\n" + "X从大到小为-DES");
        }

        private void cbbCraneNo_TextChanged(object sender, EventArgs e)
        {
            GetCoilStrategy(cbbCraneNo.Text.Trim(), dgvCoilStrategy);
            GetSaddleStrategy(cbbCraneNo.Text.Trim(), dgvSaddleStrategy);
        }

        private void txtCoilXmin_TextChanged(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            GetStatus();
            GetCoilStrategy(cbbCraneNo.Text.Trim(), dgvCoilStrategy);
            GetSaddleStrategy(cbbCraneNo.Text.Trim(), dgvSaddleStrategy);
        }

        private void GetStatus()
        {
            try
            {

                //List<string> lstAdress = new List<string>();
                //lstAdress.Clear();
                //lstAdress.Add("Z01_CoolingTime");
                //arrTagAdress = lstAdress.ToArray<string>();
                //readTags();
                //if (get_value("Z01_CoolingTime") != null)
                //{
                //    txtCurrentValue.Text = get_value("Z01_CoolingTime").ToString();
                //}
                txtCurrentValue.Text = "";
                int yard2yardID = 0;
                if (cbbCraneNo.Text.Trim() != "" && cbbCraneNo.Text.Trim() == "1_2")
                {
                    yard2yardID = 99003;
                }
                else if (cbbCraneNo.Text.Trim() != "" && cbbCraneNo.Text.Trim() == "1_3")
                {
                    yard2yardID = 13103;
                }

                string sql = @"SELECT LIST_UNIT_NO FROM YARD_TO_YARD_FIND_COIL_STRATEGY ";
                sql += " WHERE ID  = '" + yard2yardID + "'";
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        if (rdr["LIST_UNIT_NO"].ToString().Trim() != "")
                        {
                            txtCurrentValue.Text = rdr["LIST_UNIT_NO"].ToString().Trim();
                        }
                        else
                        {
                            txtCurrentValue.Text = "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0},{1}", ex.StackTrace.ToString(), ex.Message.ToString()));
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            try
            {
                //List<string> lstAdress = new List<string>();
                //lstAdress.Clear();
                //lstAdress.Add("Z01_CoolingTime");
                //arrTagAdress = lstAdress.ToArray<string>();
                //readTags();
                //if (txtModifyValue.Text != "")
                //{
                //    string time = txtModifyValue.Text;
                //    if (MessageBox.Show("确定修改降温时间吗？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
                //    {
                //        tagDataProvider.SetData("Z01_CoolingTime", time);
                //        GetStatus();
                //        txtModifyValue.Text = "";
                //        MessageBox.Show("修改成功！", "提示");
                //    }
                //}

                int change_yard2yardID = 0;
                if (cbbCraneNo.Text.Trim() != "" && cbbCraneNo.Text.Trim() == "1_2")
                {
                    change_yard2yardID = 99003;
                }
                else if (cbbCraneNo.Text.Trim() != "" && cbbCraneNo.Text.Trim() == "1_3")
                {
                    change_yard2yardID = 13103;
                }
                else if (cbbCraneNo.Text.Trim() != "" && cbbCraneNo.Text.Trim() == "1_1")
                {
                    MessageBox.Show("1_1行车无L3备料计划，无法修改！", "提示");
                    return;
                }
                else
                {
                    MessageBox.Show("没选中行车号，无法修改！", "提示");
                    return;
                }
                if(txtModifyValue.Text.Trim() == "")
                {
                    MessageBox.Show("请输入计划号的修改参数！", "提示");
                    return;
                }
                string sql = @"update YARD_TO_YARD_FIND_COIL_STRATEGY set LIST_UNIT_NO = '" + txtModifyValue.Text.Trim() + "' where ID = '" + change_yard2yardID + "'";
                DB2Connect.DBHelper.ExecuteNonQuery(sql);
                MessageBox.Show("修改成功！", "提示");
                GetStatus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0},{1}", ex.StackTrace.ToString(), ex.Message.ToString()));
            }
        }

        private void btn_HotCoilYard2Yard_Click(object sender, EventArgs e)
        {
            if (frmHotCoilYard2Yard == null || frmHotCoilYard2Yard.IsDisposed)
            {
                frmHotCoilYard2Yard = new FrmHotCoilYard2Yard();
                frmHotCoilYard2Yard.CraneNO = cbbCraneNo.Text.Trim();
                frmHotCoilYard2Yard.Show();
            }
            else
            {
                frmHotCoilYard2Yard.WindowState = FormWindowState.Normal;
                frmHotCoilYard2Yard.Activate();
            }
        }
    }
}
