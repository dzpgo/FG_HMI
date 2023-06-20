using Baosight.iSuperframe.Common;
using ParkClassLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using UACSDAL;

namespace UACSPopupForm
{
    /// <summary>
    /// 吸料补料
    /// </summary>
    public partial class SuctionAndReplenishment : Form
    {
        #region 全局变量
        private Baosight.iSuperframe.TagService.Controls.TagDataProvider tagDP = new Baosight.iSuperframe.TagService.Controls.TagDataProvider();
        private string craneNo;
        private string bayNO;
        /// <summary>
        /// 行车号
        /// </summary>
        public string CraneNo { get => craneNo; set => craneNo = value; }
        /// <summary>
        /// 跨别
        /// </summary>
        public string BayNO { get => bayNO; set => bayNO = value; }
        /// <summary>
        /// 防止弹出信息关闭画面
        /// </summary>
        private bool isPopupMessage = false;
        #endregion

        public SuctionAndReplenishment()
        {
            InitializeComponent();            
        }

        #region 初始化
        /// <summary>
        /// 初始化加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SuctionAndReplenishment_Load(object sender, EventArgs e)
        {
            this.label1.Text = CraneNo + "# 吸料补料";
            //this.Rdb_Replenishment.Checked = false;
            //this.Rdb_Suction.Checked = true;
            BindFromStock(cmb_FromStock);
            BindToStock(cmb_ToStock);
            BindMatCode(cbb_MatCode);
            //ReconditionCraneX();
            this.Deactivate += new EventHandler(frmClose);
        } 
        #endregion

        #region 绑定下拉框
        /// <summary>
        /// 取料位置
        /// </summary>
        /// <param name="cmbBox"></param>
        private void BindFromStock(ComboBox cmbBox)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TypeValue");
            dt.Columns.Add("TypeName");
            DataRow dr;
            dr = dt.NewRow();
            dr["TypeValue"] = "A1";
            dr["TypeName"] = "A1";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["TypeValue"] = "A2";
            dr["TypeName"] = "A2";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["TypeValue"] = "A3";
            dr["TypeName"] = "A3";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["TypeValue"] = "A4";
            dr["TypeName"] = "A4";
            dt.Rows.Add(dr);
            cmbBox.DataSource = dt;
            cmbBox.DisplayMember = "TypeName";
            cmbBox.ValueMember = "TypeValue";
            cmbBox.SelectedIndex = craneNo.Equals("1") ? 0 : craneNo.Equals("2") ? 1 : craneNo.Equals("3") ? 2 : craneNo.Equals("4") ? 3 : 0;
        }
        /// <summary>
        /// 放料位置
        /// </summary>
        /// <param name="cmbBox"></param>
        private void BindToStock(ComboBox cmbBox)
        {
            try
            {
                Dictionary<string, string> myDictionary = new Dictionary<string, string>();
                DataTable dt = new DataTable();
                dt.Columns.Add("ID");
                dt.Columns.Add("NAME");
                DataRow dr = dt.NewRow();
                dr = dt.NewRow();
                dr["ID"] = "无";
                dr["NAME"] = "无";
                dt.Rows.Add(dr);
                //查区域号 
                string sqlText = @"SELECT AREA_NO,AREA_NAME FROM UACS_YARDMAP_AREA_DEFINE WHERE AREA_TYPE = '0' AND YARD_NO = 'A' ";
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sqlText))
                {
                    while (rdr.Read())
                    {
                        myDictionary.Add(rdr["AREA_NO"].ToString(), rdr["AREA_NO"].ToString().Remove(0, 1).PadLeft(2, '0'));
                    }
                }
                var dicSort = from objDic in myDictionary orderby objDic.Value ascending select objDic;  //排序
                foreach (KeyValuePair<string, string> keyvalue in dicSort)
                {
                    dr = dt.NewRow();
                    dr["ID"] = keyvalue.Key;
                    dr["NAME"] = keyvalue.Value.ToString();
                    dt.Rows.Add(dr);
                }
                //绑定数据
                cmbBox.ValueMember = "ID";
                cmbBox.DisplayMember = "NAME";
                cmbBox.DataSource = dt;
                //根据text值选中项
                cmbBox.SelectedIndex = 0;
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 放料位置
        /// </summary>
        /// <param name="cmbBox"></param>
        private void BindMatCode(ComboBox cmbBox)
        {
            try
            {
                Dictionary<string, string> myDictionary = new Dictionary<string, string>();
                DataTable dt = new DataTable();
                dt.Columns.Add("ID");
                dt.Columns.Add("NAME");
                DataRow dr = dt.NewRow();
                dr = dt.NewRow();
                dr["ID"] = "无";
                dr["NAME"] = "无";
                dt.Rows.Add(dr);
                //查区域号 
                string sqlText = @"SELECT MAT_CODE,MAT_CNAME FROM UACS_L3_MAT_INFO ORDER BY MAT_CODE DESC ";
                using (IDataReader rdr = DB2Connect.DBHelper.ExecuteReader(sqlText))
                {
                    while (rdr.Read())
                    {
                        //myDictionary.Add(rdr["MAT_CODE"].ToString(), rdr["MAT_CNAME"].ToString());
                        dr = dt.NewRow();
                        dr["ID"] = rdr["MAT_CODE"].ToString();
                        dr["NAME"] = rdr["MAT_CNAME"].ToString();
                        dt.Rows.Add(dr);
                    }
                }
                //var dicSort = from objDic in myDictionary orderby objDic.Value ascending select objDic;  //排序
                //foreach (KeyValuePair<string, string> keyvalue in dicSort)
                //{
                //    dr = dt.NewRow();
                //    dr["ID"] = keyvalue.Key;
                //    dr["NAME"] = keyvalue.Value.ToString();
                //    dt.Rows.Add(dr);
                //}
                //绑定数据
                cmbBox.ValueMember = "ID";
                cmbBox.DisplayMember = "NAME";
                cmbBox.DataSource = dt;
                //根据text值选中项
                cmbBox.SelectedIndex = 1;
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        #endregion

        #region 按钮点击
        /// <summary>
        /// 开始吸料补料
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            isPopupMessage = true;
            if (string.IsNullOrEmpty(cmb_FromStock.SelectedValue.ToString().Trim()))
            {
                MessageBox.Show("请选择工位!");
                return;
            }
            if (string.IsNullOrEmpty(cmb_ToStock.SelectedValue.ToString().Trim()))
            {
                MessageBox.Show("请选择跨位!");
                return;
            }
            if (string.IsNullOrEmpty(txt_MatWeight.Text.Trim()) || Convert.ToInt32(txt_MatWeight.Text.Trim()) <= 0)
            {
                MessageBox.Show("请输入正确重量!");
                return;
            }
            DialogResult dr = MessageBox.Show("是否开始吸料补料？", "操作提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
            if (dr == DialogResult.OK)
            {
                var fromStock = string.Empty; 
                var toStock = string.Empty;
                if (Rdb_Suction.Checked)
                {
                    fromStock = cmb_FromStock.Text.Trim();
                    toStock = cmb_ToStock.Text.Trim() + "-0";
                }
                else if (Rdb_Replenishment.Checked)
                {
                    fromStock = cmb_ToStock.Text.Trim() + "-0";
                    toStock = cmb_FromStock.Text.Trim();
                }                
                UpdateStatusInit(craneNo, fromStock, toStock, txt_MatWeight.Text.ToString().Trim(), "INIT", cbb_MatCode.SelectedValue.ToString().Trim());//INIT
                ParkClassLibrary.HMILogger.WriteLog("吸料补料", craneNo + "开始吸料补料：" + txt_MatWeight.Text, LogLevel.Info, this.Text);
                UpdateStatus(craneNo, "43");
                this.Close();
            }
            else
            {
                return;
            }
            isPopupMessage = false;
        }
        /// <summary>
        /// 结束吸料补料
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFinish_Click(object sender, EventArgs e)
        {
            isPopupMessage = true;
            DialogResult dr = MessageBox.Show("是否结束吸料补料？", "操作提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
            if (dr == DialogResult.OK)
            {
                UpdateStatusEmpty(craneNo);
                ParkClassLibrary.HMILogger.WriteLog("吸料补料", craneNo + "结束吸料补料：" + txt_MatWeight.Text, LogLevel.Info, this.Text);
                UpdateStatus(craneNo, "99");
                this.Close();
            }
            else
            {
                return;
            }
            isPopupMessage = false;
        }
        /// <summary>
        /// 更新行车状态
        /// </summary>
        /// <param name="craneNo">行车</param>
        /// <param name="oederType">状态</param>
        private void UpdateStatus(string craneNo, string oederType)
        {
            try
            {
                string ExeSql = @" UPDATE UACS_CRANE_ORDER_CURRENT SET ORDER_TYPE = '" + oederType + "' WHERE CRANE_NO = '" + craneNo + "' ";
                DB2Connect.DBHelper.ExecuteNonQuery(ExeSql);
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
        #endregion

        /// <summary>
        /// 更新吸料补料
        /// </summary>
        /// <param name="craneNo">行车号</param>
        /// <param name="FromStock">工位</param>
        /// <param name="ToStock">跨位</param>
        /// <param name="MatWeight">重量</param>
        /// <param name="Status">状态 空：EMPTY 初始化：INIT</param>
        private void UpdateStatusInit(string craneNo,string FromStock, string ToStock, string MatWeight, string Status, string MatCode)
        {
            try
            {
                string ExeSql = @"UPDATE UACS_CRANE_MANU_ORDER_INOUT SET MAT_WEIGHT = '" + MatWeight + "' ,MAT_NO = '"+ MatCode + "'  ,FROM_STOCK = '" + FromStock + "',TO_STOCK = '" + ToStock + "' ,STATUS = '" + Status + "' WHERE CRANE_NO = '" + craneNo + "'";
                DB2Connect.DBHelper.ExecuteNonQuery(ExeSql);
            }
            catch (Exception)
            {
            }
        }
        /// <summary>
        /// 结束吸料补料
        /// </summary>
        /// <param name="craneNo">行车号</param>
        private void UpdateStatusEmpty(string craneNo)
        {
            try
            {
                string ExeSql = @"UPDATE UACS_CRANE_MANU_ORDER_INOUT SET MAT_WEIGHT = null, MAT_NO = null, FROM_STOCK = null, TO_STOCK = null, STATUS = 'EMPTY' ";
                ExeSql += " WHERE BAY_NO = 'A' AND CRANE_NO = '" + craneNo + "'";
                DB2Connect.DBHelper.ExecuteNonQuery(ExeSql);
            }
            catch (Exception)
            {
            }
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
        /// <summary>
        /// 限制只能输入数字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_MatWeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            //如果输入的不是退格和数字，则屏蔽输入
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9')))
            {
                e.Handled = true;
            }
        }
        /// <summary>
        /// 补料
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Rdb_Replenishment_CheckedChanged(object sender, EventArgs e)
        {
            isPopupMessage = true;
            if (Rdb_Replenishment.Checked)
            {
                //cmb_FromStock = null; 
                //cmb_ToStock = null;
                //BindFromStock(cmb_FromStock);
                //BindToStock(cmb_ToStock);
            }
            isPopupMessage = false;
        }
        /// <summary>
        /// 吸料
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Rdb_Suction_CheckedChanged(object sender, EventArgs e)
        {
            isPopupMessage = true;
            if (Rdb_Suction.Checked)
            {
                //cmb_FromStock = null;
                //cmb_ToStock = null;
                //BindFromStock(cmb_ToStock);
                //BindToStock(cmb_FromStock);
            }
            isPopupMessage = false;
        }
    }
}
