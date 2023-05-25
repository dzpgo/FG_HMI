using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using UACSDAL;
using UACSControls;
using ParkClassLibrary;

namespace UACSView.View_CraneMonitor
{
    /// <summary>
    /// 装冷却剂
    /// </summary>
    public partial class FrmSaddleShowYardmapGrid : Form
    {
        #region 全局变量
        public FrmSaddleShow frmSS;
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
        #endregion
        #region 初始化
        public FrmSaddleShowYardmapGrid()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 添加一个构造函数
        /// </summary>
        /// <param name="form"></param>
        public FrmSaddleShowYardmapGrid(FrmSaddleShow form) : this()
        {
            frmSS = form;
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
            }
            this.Deactivate += new EventHandler(frmClose);
        }
        /// <summary>
        /// 刷新
        /// </summary>
        private void LoadDataRefresh()
        {
            //源数据
            GetYardmapGrid();
            GetYardmapGridLoad();
        }
        #endregion
        #region 绑定下拉框

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void GetYardmapGrid()
        {
            string sqlText = @"SELECT GRID_NO,GRID_NAME,GRID_DIV,MAT_CODE,COMP_CODE,MAT_WGT,MAT_WGTTOTAL,AREA_NO,GRID_STATUS FROM UACS_YARDMAP_GRID_DEFINE WHERE AREA_NO = '" + AreaNo + "' ORDER BY GRID_NO ASC ";
            using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sqlText))
            {
                dtSource.Load(rdr);
            }
        }
        private void GetYardmapGridLoad()
        {
            if (dtSource != null && dtSource.Rows.Count > 0)
            {
                foreach (DataRow dr in dtSource.Rows)
                {
                    var gridNo = dr["GRID_NO"].ToString();
                    var gridName = dr["GRID_NAME"].ToString();
                    var gridDiv = dr["GRID_DIV"].ToString();
                    var MatCode = dr["MAT_CODE"].ToString();
                    var areano = dr["AREA_NO"].ToString();
                    var gridStatus = dr["GRID_STATUS"].ToString();
                    if (!string.IsNullOrEmpty(gridNo))
                    {
                        var grids = gridNo.Split('-');
                        if (grids[1].Equals("0"))
                        {
                            txt_GridNo_0.Text = gridNo;
                            txt_GridName_0.Text = gridName;
                            txt_MatCode_0.Text = MatCode;
                            BindMatCodeName(cmb_MatCodeName_0, MatCode);
                            BindStatus(cmb_Status_0, gridStatus);
                        }
                        if (grids[1].Equals("1"))
                        {
                            txt_GridNo_1.Text = gridNo;
                            txt_GridName_1.Text = gridName;
                            txt_MatCode_1.Text = MatCode;
                            BindMatCodeName(cmb_MatCodeName_1, MatCode);
                            BindStatus(cmb_Status_1, gridStatus);
                        }
                        if (grids[1].Equals("2"))
                        {
                            txt_GridNo_2.Text = gridNo;
                            txt_GridName_2.Text = gridName;
                            txt_MatCode_2.Text = MatCode;
                            BindMatCodeName(cmb_MatCodeName_2, MatCode);
                            BindStatus(cmb_Status_2, gridStatus);
                        }
                        if (grids[1].Equals("3"))
                        {
                            txt_GridNo_3.Text = gridNo;
                            txt_GridName_3.Text = gridName;
                            txt_MatCode_3.Text = MatCode;
                            BindMatCodeName(cmb_MatCodeName_3, MatCode);
                            BindStatus(cmb_Status_3, gridStatus);
                        }
                        if (grids[1].Equals("4"))
                        {
                            txt_GridNo_4.Text = gridNo;
                            txt_GridName_4.Text = gridName;
                            txt_MatCode_4.Text = MatCode;
                            BindMatCodeName(cmb_MatCodeName_4, MatCode);
                            BindStatus(cmb_Status_4, gridStatus);
                        }
                        if (grids[1].Equals("5"))
                        {
                            txt_GridNo_5.Text = gridNo;
                            txt_GridName_5.Text = gridName;
                            txt_MatCode_5.Text = MatCode;
                            BindMatCodeName(cmb_MatCodeName_5, MatCode);
                            BindStatus(cmb_Status_5, gridStatus);
                        }
                        if (grids[1].Equals("6"))
                        {
                            txt_GridNo_6.Text = gridNo;
                            txt_GridName_6.Text = gridName;
                            txt_MatCode_6.Text = MatCode;
                            BindMatCodeName(cmb_MatCodeName_6, MatCode);
                            BindStatus(cmb_Status_6, gridStatus);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 绑定行车
        /// </summary>
        /// <param name="cmbBox"></param>
        private void BindStatus(ComboBox cmbBox, string status)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TypeValue");
            dt.Columns.Add("TypeName");
            DataRow dr;
            dr = dt.NewRow();
            dr["TypeValue"] = "0";
            dr["TypeName"] = "未启用";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["TypeValue"] = "1";
            dr["TypeName"] = "已启用";
            dt.Rows.Add(dr);
            //var index = Convert.ToInt32(CraneNo) -1;
            cmbBox.DataSource = dt;
            cmbBox.DisplayMember = "TypeName";
            cmbBox.ValueMember = "TypeValue";
            cmbBox.SelectedIndex = status.Equals("0") ? 0 : 1;
            if (status.Equals("1"))
            {
                cmbBox.BackColor = Color.LightGreen;
            }

        }
        /// <summary>
        /// 绑定放料位置
        /// </summary>
        /// <param name="cmbBox"></param>
        private void BindMatCodeName(ComboBox cmbBox, string code)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TypeValue");
            dt.Columns.Add("TypeName");
            DataRow dr;
            var index = 0;
            string sqlText = @"SELECT MAT_CODE, MAT_CNAME  FROM UACS_L3_MAT_INFO";
            using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sqlText))
            {
                var coun = 0;
                while (rdr.Read())
                {
                    dr = dt.NewRow();
                    dr["TypeValue"] = rdr["MAT_CODE"].ToString();
                    dr["TypeName"] = rdr["MAT_CNAME"].ToString();
                    dt.Rows.Add(dr);
                    coun++;
                    if (rdr["MAT_CODE"].ToString().Equals(code))
                    {
                        index = coun - 1;
                    }
                }
            }
            cmbBox.DataSource = dt;
            cmbBox.DisplayMember = "TypeName";
            cmbBox.ValueMember = "TypeValue";
            cmbBox.SelectedIndex = index;
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
            try
            {
                var data = GetdataTable();
                foreach (DataRow dr in data.Rows)
                {
                    UpdateStatus(dr["GRID_NO"].ToString(), dr["MAT_CODE"].ToString(), dr["GRID_STATUS"].ToString());
                }
            }
            catch (Exception)
            {
            }
            //frmSS.RefreshData();
            this.Close();
            isPopupMessage = false;
        }
        /// <summary>
        /// 获取修改的数据
        /// </summary>
        /// <returns></returns>
        private DataTable GetdataTable()
        {
            DataTable dt = dtSource.Clone();
            foreach (DataRow dr in dtSource.Rows)
            {
                if (dr["GRID_NO"].ToString().Equals(txt_GridNo_0.Text.Trim()))
                {
                    DataRow drs;
                    var isFalse = false;
                    var matCode = string.Empty;
                    var gridStatus = string.Empty;
                    if (!dr["MAT_CODE"].ToString().Equals(txt_MatCode_0.Text.Trim()))
                    {
                        matCode = txt_MatCode_0.Text.Trim();
                        isFalse = true;
                    }
                    if (!dr["GRID_STATUS"].ToString().Equals(cmb_Status_0.SelectedValue.ToString().Trim()))
                    {
                        gridStatus = cmb_Status_0.SelectedValue.ToString();
                        isFalse = true;
                    }
                    if (isFalse)
                    {
                        drs = dt.NewRow();
                        drs["GRID_NO"] = dr["GRID_NO"];
                        drs["GRID_NAME"] = dr["GRID_NAME"];
                        drs["GRID_DIV"] = dr["GRID_DIV"];
                        drs["COMP_CODE"] = dr["COMP_CODE"];
                        drs["MAT_WGT"] = dr["MAT_WGT"];
                        drs["MAT_WGTTOTAL"] = dr["MAT_WGTTOTAL"];
                        drs["AREA_NO"] = dr["AREA_NO"];
                        drs["MAT_CODE"] = string.IsNullOrEmpty(matCode) ? dr["MAT_CODE"] : matCode;
                        drs["GRID_STATUS"] = string.IsNullOrEmpty(gridStatus) ? dr["GRID_STATUS"] : gridStatus;
                        dt.Rows.Add(drs);
                    }
                }
                if (dr["GRID_NO"].ToString().Equals(txt_GridNo_1.Text.Trim()))
                {
                    DataRow drs;
                    var isFalse = false;
                    var matCode = "";
                    var gridStatus = "";
                    if (!dr["MAT_CODE"].ToString().Equals(txt_MatCode_1.Text.Trim()))
                    {
                        matCode = txt_MatCode_1.Text.Trim();
                        isFalse = true;
                    }
                    if (!dr["GRID_STATUS"].ToString().Equals(cmb_Status_1.SelectedValue.ToString().Trim()))
                    {
                        gridStatus = cmb_Status_1.SelectedValue.ToString();
                        isFalse = true;
                    }
                    if (isFalse)
                    {
                        drs = dt.NewRow();
                        drs["GRID_NO"] = dr["GRID_NO"];
                        drs["GRID_NAME"] = dr["GRID_NAME"];
                        drs["GRID_DIV"] = dr["GRID_DIV"];
                        drs["COMP_CODE"] = dr["COMP_CODE"];
                        drs["MAT_WGT"] = dr["MAT_WGT"];
                        drs["MAT_WGTTOTAL"] = dr["MAT_WGTTOTAL"];
                        drs["AREA_NO"] = dr["AREA_NO"];
                        drs["MAT_CODE"] = string.IsNullOrEmpty(matCode) ? dr["MAT_CODE"] : matCode;
                        drs["GRID_STATUS"] = string.IsNullOrEmpty(gridStatus) ? dr["GRID_STATUS"] : gridStatus;
                        dt.Rows.Add(drs);
                    }
                }
                if (dr["GRID_NO"].ToString().Equals(txt_GridNo_2.Text.Trim()))
                {
                    DataRow drs;
                    var isFalse = false;
                    var matCode = "";
                    var gridStatus = "";
                    if (!dr["MAT_CODE"].ToString().Equals(txt_MatCode_2.Text.Trim()))
                    {
                        matCode = txt_MatCode_2.Text.Trim();
                        isFalse = true;
                    }
                    if (!dr["GRID_STATUS"].ToString().Equals(cmb_Status_2.SelectedValue.ToString().Trim()))
                    {
                        gridStatus = cmb_Status_2.SelectedValue.ToString();
                        isFalse = true;
                    }
                    if (isFalse)
                    {
                        drs = dt.NewRow();
                        drs["GRID_NO"] = dr["GRID_NO"];
                        drs["GRID_NAME"] = dr["GRID_NAME"];
                        drs["GRID_DIV"] = dr["GRID_DIV"];
                        drs["COMP_CODE"] = dr["COMP_CODE"];
                        drs["MAT_WGT"] = dr["MAT_WGT"];
                        drs["MAT_WGTTOTAL"] = dr["MAT_WGTTOTAL"];
                        drs["AREA_NO"] = dr["AREA_NO"];
                        drs["MAT_CODE"] = string.IsNullOrEmpty(matCode) ? dr["MAT_CODE"] : matCode;
                        drs["GRID_STATUS"] = string.IsNullOrEmpty(gridStatus) ? dr["GRID_STATUS"] : gridStatus;
                        dt.Rows.Add(drs);
                    }
                }
                if (dr["GRID_NO"].ToString().Equals(txt_GridNo_3.Text.Trim()))
                {
                    DataRow drs;
                    var isFalse = false;
                    var matCode = "";
                    var gridStatus = "";
                    if (!dr["MAT_CODE"].ToString().Equals(txt_MatCode_3.Text.Trim()))
                    {
                        matCode = txt_MatCode_3.Text.Trim();
                        isFalse = true;
                    }
                    if (!dr["GRID_STATUS"].ToString().Equals(cmb_Status_3.SelectedValue.ToString().Trim()))
                    {
                        gridStatus = cmb_Status_3.SelectedValue.ToString();
                        isFalse = true;
                    }
                    if (isFalse)
                    {
                        drs = dt.NewRow();
                        drs["GRID_NO"] = dr["GRID_NO"];
                        drs["GRID_NAME"] = dr["GRID_NAME"];
                        drs["GRID_DIV"] = dr["GRID_DIV"];
                        drs["COMP_CODE"] = dr["COMP_CODE"];
                        drs["MAT_WGT"] = dr["MAT_WGT"];
                        drs["MAT_WGTTOTAL"] = dr["MAT_WGTTOTAL"];
                        drs["AREA_NO"] = dr["AREA_NO"];
                        drs["MAT_CODE"] = string.IsNullOrEmpty(matCode) ? dr["MAT_CODE"] : matCode;
                        drs["GRID_STATUS"] = string.IsNullOrEmpty(gridStatus) ? dr["GRID_STATUS"] : gridStatus;
                        dt.Rows.Add(drs);
                    }
                }
                if (dr["GRID_NO"].ToString().Equals(txt_GridNo_4.Text.Trim()))
                {
                    DataRow drs;
                    var isFalse = false;
                    var matCode = "";
                    var gridStatus = "";
                    if (!dr["MAT_CODE"].ToString().Equals(txt_MatCode_4.Text.Trim()))
                    {
                        matCode = txt_MatCode_4.Text.Trim();
                        isFalse = true;
                    }
                    if (!dr["GRID_STATUS"].ToString().Equals(cmb_Status_4.SelectedValue.ToString().Trim()))
                    {
                        gridStatus = cmb_Status_4.SelectedValue.ToString();
                        isFalse = true;
                    }
                    if (isFalse)
                    {
                        drs = dt.NewRow();
                        drs["GRID_NO"] = dr["GRID_NO"];
                        drs["GRID_NAME"] = dr["GRID_NAME"];
                        drs["GRID_DIV"] = dr["GRID_DIV"];
                        drs["COMP_CODE"] = dr["COMP_CODE"];
                        drs["MAT_WGT"] = dr["MAT_WGT"];
                        drs["MAT_WGTTOTAL"] = dr["MAT_WGTTOTAL"];
                        drs["AREA_NO"] = dr["AREA_NO"];
                        drs["MAT_CODE"] = string.IsNullOrEmpty(matCode) ? dr["MAT_CODE"] : matCode;
                        drs["GRID_STATUS"] = string.IsNullOrEmpty(gridStatus) ? dr["GRID_STATUS"] : gridStatus;
                        dt.Rows.Add(drs);
                    }
                }
                if (dr["GRID_NO"].ToString().Equals(txt_GridNo_5.Text.Trim()))
                {
                    DataRow drs;
                    var isFalse = false;
                    var matCode = "";
                    var gridStatus = "";
                    if (!dr["MAT_CODE"].ToString().Equals(txt_MatCode_5.Text.Trim()))
                    {
                        matCode = txt_MatCode_5.Text.Trim();
                        isFalse = true;
                    }
                    if (!dr["GRID_STATUS"].ToString().Equals(cmb_Status_5.SelectedValue.ToString().Trim()))
                    {
                        gridStatus = cmb_Status_5.SelectedValue.ToString();
                        isFalse = true;
                    }
                    if (isFalse)
                    {
                        drs = dt.NewRow();
                        drs["GRID_NO"] = dr["GRID_NO"];
                        drs["GRID_NAME"] = dr["GRID_NAME"];
                        drs["GRID_DIV"] = dr["GRID_DIV"];
                        drs["COMP_CODE"] = dr["COMP_CODE"];
                        drs["MAT_WGT"] = dr["MAT_WGT"];
                        drs["MAT_WGTTOTAL"] = dr["MAT_WGTTOTAL"];
                        drs["AREA_NO"] = dr["AREA_NO"];
                        drs["MAT_CODE"] = string.IsNullOrEmpty(matCode) ? dr["MAT_CODE"] : matCode;
                        drs["GRID_STATUS"] = string.IsNullOrEmpty(gridStatus) ? dr["GRID_STATUS"] : gridStatus;
                        dt.Rows.Add(drs);
                    }
                }
                if (dr["GRID_NO"].ToString().Equals(txt_GridNo_6.Text.Trim()))
                {
                    DataRow drs;
                    var isFalse = false;
                    var matCode = "";
                    var gridStatus = "";
                    if (!dr["MAT_CODE"].ToString().Equals(txt_MatCode_6.Text.Trim()))
                    {
                        matCode = txt_MatCode_6.Text.Trim();
                        isFalse = true;
                    }
                    if (!dr["GRID_STATUS"].ToString().Equals(cmb_Status_6.SelectedValue.ToString().Trim()))
                    {
                        gridStatus = cmb_Status_6.SelectedValue.ToString();
                        isFalse = true;
                    }
                    if (isFalse)
                    {
                        drs = dt.NewRow();
                        drs["GRID_NO"] = dr["GRID_NO"];
                        drs["GRID_NAME"] = dr["GRID_NAME"];
                        drs["GRID_DIV"] = dr["GRID_DIV"];
                        drs["COMP_CODE"] = dr["COMP_CODE"];
                        drs["MAT_WGT"] = dr["MAT_WGT"];
                        drs["MAT_WGTTOTAL"] = dr["MAT_WGTTOTAL"];
                        drs["AREA_NO"] = dr["AREA_NO"];
                        drs["MAT_CODE"] = string.IsNullOrEmpty(matCode) ? dr["MAT_CODE"] : matCode;
                        drs["GRID_STATUS"] = string.IsNullOrEmpty(gridStatus) ? dr["GRID_STATUS"] : gridStatus;
                        dt.Rows.Add(drs);
                    }
                }
            }
            return dt;
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
        private void UpdateStatus(string gridNo, string matCode,string gridStatus)
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
        #region 值更改触发
        private void cmb_MatCodeName_0_SelectedIndexChanged(object sender, EventArgs e)
        {
            //GetMatInfo(cmb_MatCodeName_0.SelectedValue.ToString());
            txt_MatCode_0.Text = cmb_MatCodeName_0.SelectedValue.ToString();
            //cmb_MatCodeName_0.BackColor = Color.LightYellow;
            //txt_MatCode_0.BackColor = Color.LightYellow;
        }

        private void cmb_MatCodeName_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_MatCode_1.Text = cmb_MatCodeName_1.SelectedValue.ToString();
            //cmb_MatCodeName_1.BackColor = Color.LightYellow;
            //txt_MatCode_1.BackColor = Color.LightYellow;
        }

        private void cmb_MatCodeName_2_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_MatCode_2.Text = cmb_MatCodeName_2.SelectedValue.ToString();
            //cmb_MatCodeName_2.BackColor = Color.LightYellow;
            //txt_MatCode_2.BackColor = Color.LightYellow;
        }

        private void cmb_MatCodeName_3_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_MatCode_3.Text = cmb_MatCodeName_3.SelectedValue.ToString();
            //cmb_MatCodeName_3.BackColor = Color.LightYellow;
            //txt_MatCode_3.BackColor = Color.LightYellow;
        }

        private void cmb_MatCodeName_4_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_MatCode_4.Text = cmb_MatCodeName_4.SelectedValue.ToString();
            //cmb_MatCodeName_4.BackColor = Color.LightYellow;
            //txt_MatCode_4.BackColor = Color.LightYellow;
        }

        private void cmb_MatCodeName_5_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_MatCode_5.Text = cmb_MatCodeName_5.SelectedValue.ToString();
            //cmb_MatCodeName_5.BackColor = Color.LightYellow;
            //txt_MatCode_5.BackColor = Color.LightYellow;
        }

        private void cmb_MatCodeName_6_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_MatCode_6.Text = cmb_MatCodeName_6.SelectedValue.ToString();
            //cmb_MatCodeName_6.BackColor = Color.LightYellow;
            //txt_MatCode_6.BackColor = Color.LightYellow;
        }

        private void cmb_Status_0_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cmb_Status_0.BackColor = Color.LightYellow;
        }

        private void cmb_Status_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cmb_Status_1.BackColor = Color.LightYellow;
        }

        private void cmb_Status_2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cmb_Status_2.BackColor = Color.LightYellow;
        }

        private void cmb_Status_3_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cmb_Status_3.BackColor = Color.LightYellow;
        }

        private void cmb_Status_4_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cmb_Status_4.BackColor = Color.LightYellow;
        }

        private void cmb_Status_5_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cmb_Status_5.BackColor = Color.LightYellow;
        }

        private void cmb_Status_6_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cmb_Status_6.BackColor = Color.LightYellow;
        }
        #endregion

        
    }
}
