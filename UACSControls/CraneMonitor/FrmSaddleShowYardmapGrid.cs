using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using UACSDAL;
using ParkClassLibrary;
using System.Collections.Generic;

namespace UACSView.View_CraneMonitor
{
    /// <summary>
    /// 修改料格物料布局
    /// </summary>
    public partial class FrmSaddleShowYardmapGrid : Form
    {
        #region 全局变量
        private string areaNo;
        /// <summary>
        /// 所在区域号
        /// </summary>
        public string AreaNo { get => areaNo; set => areaNo = value; }
        /// <summary>
        /// 防止弹出信息关闭画面
        /// </summary>
        private bool isPopupMessage = false;
        private DataTable dtSource = new DataTable();
        public DataTable Initial_dgv = new DataTable();
        private ComboBox comboBox1 = new ComboBox();
        private ComboBox comboBox2 = new ComboBox();
        private Dictionary<string, string> MatCodeList = new Dictionary<string, string>();
        #endregion
        #region 初始化
        public FrmSaddleShowYardmapGrid()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 初始化加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmSaddleShowYardmapGrid_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(AreaNo))
            {
                LoadDataRefresh();
                GetDataInitial();
            }
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            this.Deactivate += new EventHandler(frmClose);
        }
        /// <summary>
        /// 刷新
        /// </summary>
        private void LoadDataRefresh()
        {
            //源数据
            GetYardmapGrid();
            Initial_dgv.Clear();
            if (dataGridView1.DataSource != null && dataGridView1.Rows.Count > 0)
            {
                Initial_dgv = GetDataGridViewToTable(dataGridView1);
            }
        }
        #endregion

        #region 数据

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void GetYardmapGrid()
        {
            if (dtSource != null && dtSource.Rows.Count > 0)
            {
                dtSource.Clear();
            }
            string sqlText = @"SELECT A.GRID_NO,A.GRID_NAME,B.MAT_CNAME,A.MAT_CODE,A.AREA_NO,CASE A.GRID_STATUS WHEN 0 THEN '未启用' WHEN 1 THEN '已启用' ELSE '' END AS GRID_STATUS FROM UACS_YARDMAP_GRID_DEFINE A LEFT JOIN UACS_L3_MAT_INFO B ON A.MAT_CODE = B.MAT_CODE WHERE AREA_NO = '" + AreaNo + "' ORDER BY GRID_NO ASC ";
            using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sqlText))
            {
                dtSource.Load(rdr);
            }
            dataGridView1.DataSource = dtSource;
        }

        private void GetDataInitial()
        {
            try
            {
                MatCodeList.Clear();
                string sqlText = @"SELECT MAT_CODE, MAT_CNAME  FROM UACS_L3_MAT_INFO";
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sqlText))
                {
                    while (rdr.Read())
                    {
                        comboBox1.Items.Add(rdr["MAT_CNAME"].ToString());
                        MatCodeList.Add(rdr["MAT_CNAME"].ToString(), rdr["MAT_CODE"].ToString());
                    }
                }
                comboBox1.Visible = false;
                dataGridView1.Controls.Add(comboBox1);
                this.comboBox1.SelectedValueChanged += new System.EventHandler(this.comboBox1_SelectedValueChanged);

                comboBox2.Visible = false;
                comboBox2.Items.Add("未启用");
                comboBox2.Items.Add("已启用");
                dataGridView1.Controls.Add(comboBox2);
                this.comboBox2.SelectedValueChanged += new System.EventHandler(this.comboBox2_SelectedValueChanged);
            }
            catch (Exception)
            {
            }

        }
        /// <summary>
        /// 下拉框值发生更改时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell != null)
                dataGridView1.CurrentCell.Value = comboBox1.SelectedItem.ToString();
        }
        /// <summary>
        /// 下拉框值发生更改时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell != null)
                dataGridView1.CurrentCell.Value = comboBox2.SelectedItem.ToString();
        }
        /// <summary>
        /// dataGridView增加下拉框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.dataGridView1.CurrentCell.ColumnIndex == 2)
                {
                    Rectangle rect = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false);
                    string sexValue = dataGridView1.CurrentCell.Value.ToString();
                    comboBox1.Left = rect.Left;
                    comboBox1.Top = rect.Top;
                    comboBox1.Width = rect.Width;
                    comboBox1.Height = rect.Height;
                    comboBox1.SelectedItem = dataGridView1.CurrentCell.Value;
                    comboBox1.Font = new Font("微软雅黑", 10F);
                    comboBox1.Visible = true;
                }
                else
                    comboBox1.Visible = false;
                if (this.dataGridView1.CurrentCell.ColumnIndex == 5)
                {
                    Rectangle rect = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false);
                    string sexValue = dataGridView1.CurrentCell.Value.ToString();
                    comboBox2.Left = rect.Left;
                    comboBox2.Top = rect.Top;
                    comboBox2.Width = rect.Width;
                    comboBox2.Height = rect.Height;
                    comboBox2.SelectedItem = dataGridView1.CurrentCell.Value;
                    comboBox2.Font = new Font("微软雅黑", 10F);
                    comboBox2.Visible = true;
                }
                else
                    comboBox2.Visible = false;
            }
            catch
            {
            }
        }

        /// <summary>
        /// 更改物料
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //得到选中行的索引
            int intRow = dataGridView1.SelectedCells[0].RowIndex;
            //得到列的索引
            int intColumn = dataGridView1.SelectedCells[0].ColumnIndex;
            var sss = dataGridView1.Rows[intRow].Cells["MAT_CNAME"].Value.ToString();
            //dataGridView1.Rows[intRow].Cells["MAT_CNAME"].Value = 1;
            var dddd = dataGridView1.Rows[intRow].Cells["MAT_CODE"].Value.ToString();
            //得到选中行某列的值
            string str = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            var data1 = dataGridView1.Rows[intRow].Cells[intColumn].Value.ToString();
            var data2 = Initial_dgv.Rows[intRow].ItemArray[intColumn];
            if (data2.Equals(data1))
            {
                dataGridView1.Rows[intRow].Cells[intColumn].Style.BackColor = System.Drawing.SystemColors.Window;
            }
            else
            {
                dataGridView1.Rows[intRow].Cells[intColumn].Style.BackColor = Color.LightGreen;
            }
            if (MatCodeList.ContainsKey(data1))
            {
                dataGridView1.Rows[intRow].Cells["MAT_CODE"].Value = MatCodeList[data1];
                var data3 = dataGridView1.Rows[intRow].Cells["MAT_CODE"].Value.ToString();
                var data4 = Initial_dgv.Rows[intRow].ItemArray[intColumn + 1];
                if (data4.Equals(data3))
                {
                    dataGridView1.Rows[intRow].Cells["MAT_CODE"].Style.BackColor = System.Drawing.SystemColors.Window;
                }
                else
                {
                    dataGridView1.Rows[intRow].Cells["MAT_CODE"].Style.BackColor = Color.LightGreen;
                }
            }
            //GetMatInfo(cmb_GridNo.SelectedValue.ToString());
        }
        /// <summary>
        /// 更改物料
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //得到选中行的索引
            int intRow = dataGridView1.SelectedCells[0].RowIndex;
            //得到列的索引
            int intColumn = dataGridView1.SelectedCells[0].ColumnIndex;
            //var sss = dgvStowageMessage.Rows[intRow].Cells["MAT_CNAME"].Value.ToString();
            //dgvStowageMessage.Rows[intRow].Cells["MAT_CNAME"].Value = 1;
            //var dddd = dgvStowageMessage.Rows[intRow].Cells["MAT_CODE"].Value.ToString();
            //得到选中行某列的值
            string str = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            var data1 = dataGridView1.Rows[intRow].Cells[intColumn].Value.ToString();
            var data2 = Initial_dgv.Rows[intRow].ItemArray[intColumn];
            if (data2.Equals(data1))
            {
                dataGridView1.Rows[intRow].Cells[intColumn].Style.BackColor = System.Drawing.SystemColors.Window;
            }
            else
            {
                dataGridView1.Rows[intRow].Cells[intColumn].Style.BackColor = Color.LightGreen;
            }
            if (MatCodeList.ContainsKey("外购中混废"))
            {
                Console.WriteLine("Key:{0},Value:{1}", "1", MatCodeList["外购中混废"]);
            }
            //GetMatInfo(cmb_GridNo.SelectedValue.ToString());
        }
        /// <summary>
        /// DataGridView 转 DataTable
        /// </summary>
        /// <param name="dgv">DataGridView</param>
        /// <returns></returns>
        public DataTable GetDataGridViewToTable(DataGridView dgv)
        {
            DataTable dt = new DataTable();

            // 列强制转换
            for (int count = 0; count < dgv.Columns.Count; count++)
            {
                DataColumn dc = new DataColumn(dgv.Columns[count].Name.ToString());
                dt.Columns.Add(dc);
            }

            // 循环行
            for (int count = 0; count < dgv.Rows.Count; count++)
            {
                DataRow dr = dt.NewRow();
                for (int countsub = 0; countsub < dgv.Columns.Count; countsub++)
                {
                    dr[countsub] = Convert.ToString(dgv.Rows[count].Cells[countsub].Value);
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        #endregion
        #region 按钮点击
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            isPopupMessage = true;
            if (dataGridView1.DataSource != null && dtSource != null && dataGridView1.Rows.Count > 0 && dtSource.Rows.Count > 0)
            {
                try
                {
                    //确认提示
                    MessageBoxButtons btn = MessageBoxButtons.OKCancel;
                    DialogResult drmsg = MessageBox.Show("确认是否修改数据？", "提示", btn, MessageBoxIcon.Asterisk);
                    if (drmsg == DialogResult.OK)
                    {
                        var isUpdate = false;
                        foreach (DataGridViewRow dgr in dataGridView1.Rows)
                        {
                            foreach (DataRow Initdgr in Initial_dgv.Rows)
                            {
                                if (!string.IsNullOrEmpty(dgr.Cells["GRID_NO"].Value.ToString()) && !string.IsNullOrEmpty(Initdgr["GRID_NO"].ToString()) && dgr.Cells["GRID_NO"].Value.ToString().Equals(Initdgr["GRID_NO"].ToString()))
                                {
                                    if (!dgr.Cells["MAT_CODE"].Value.ToString().Equals(Initdgr["MAT_CODE"].ToString()))
                                    {
                                        isUpdate = true;
                                        break;
                                    }
                                    if (!dgr.Cells["GRID_STATUS"].Value.ToString().Equals(Initdgr["GRID_STATUS"].ToString()))
                                    {
                                        isUpdate = true;
                                        break;
                                    }
                                }
                            }
                            if (isUpdate)
                            {
                                string ExeSql = @" UPDATE UACS_YARDMAP_GRID_DEFINE SET ";
                                ExeSql += "MAT_CODE = '" + dgr.Cells["MAT_CODE"].Value.ToString().Trim() + "' ";
                                ExeSql += ",GRID_STATUS = '" + (dgr.Cells["GRID_STATUS"].Value.ToString().Trim().Equals("未启用") ? 0 : 1) + "' ";
                                ExeSql += " WHERE 1=1 ";
                                ExeSql += " AND AREA_NO = '" + AreaNo + "'";
                                ExeSql += " AND GRID_NO = '" + dgr.Cells["GRID_NO"].Value.ToString().Trim() + "' ";
                                DB2Connect.DBHelper.ExecuteNonQuery(ExeSql);
                                HMILogger.WriteLog("修改料格物料布局", "料格号：" + dgr.Cells["GRID_NO"].Value.ToString().Trim() + ", 料格名：" + dgr.Cells["GRID_NAME"].Value.ToString().Trim() + ", 物料名：" + dgr.Cells["MAT_CNAME"].Value.ToString().Trim() + ", 物料代码：" + dgr.Cells["MAT_CODE"].Value.ToString().Trim() + ", 料格状态：" + dgr.Cells["GRID_STATUS"].Value.ToString().Trim(), LogLevel.Info, this.Text);
                                isUpdate = false;
                            }
                        }
                        LoadDataRefresh(); //刷新页面
                    }
                }
                catch (Exception ex)
                {
                    MessageBoxButtons btn = MessageBoxButtons.OK;
                    DialogResult drmsg = MessageBox.Show("保存失败，无数据更改！", "提示", btn, MessageBoxIcon.Warning);
                }
            }
            isPopupMessage = false;
        }

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            isPopupMessage = true;
            LoadDataRefresh();
            isPopupMessage = false;
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="gridNo">料格号</param>
        /// <param name="matCode">物料代码</param>
        /// <param name="gridStatus">料格状态</param>
        private void UpdateStatus(string gridNo, string matCode, string gridStatus)
        {
            try
            {
                string ExeSql = @"UPDATE UACS_YARDMAP_GRID_DEFINE SET MAT_CODE = '" + matCode + "',GRID_STATUS = '" + gridStatus + "' WHERE AREA_NO = '" + AreaNo + "' AND GRID_NO = '" + gridNo + "'";
                DB2Connect.DBHelper.ExecuteNonQuery(ExeSql);
                HMILogger.WriteLog("料格", gridNo + "料格更新，物料代码：" + matCode + "，料格状态：" + gridStatus, LogLevel.Info, this.Text);
            }
            catch (Exception)
            {
            }
        }
        /// <summary>
        /// 取消 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmClose(object sender, EventArgs e)
        {
            try
            {
                if (!isPopupMessage)
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
            }
        }

        #endregion

    }
}
