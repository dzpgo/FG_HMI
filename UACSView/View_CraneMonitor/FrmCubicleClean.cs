using Baosight.iSuperframe.Common;
using ParkClassLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UACSDAL;

namespace UACSView.View_CraneMonitor
{
    /// <summary>
    /// 工位清扫
    /// </summary>
    public partial class FrmCubicleClean : Form
    {
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

        #region 变量
        public Z01_library_Monitor ALM;
        //防止弹出信息关闭画面
        bool isPopupMessage = false;
        #endregion

        #region 初始化
        public FrmCubicleClean()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 添加一个构造函数
        /// </summary>
        /// <param name="form"></param>
        public FrmCubicleClean(Z01_library_Monitor form) : this()
        {
            ALM = form;
        }
        /// <summary>
        /// 初始化加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmCubicleClean_Load(object sender, EventArgs e)
        {
            BindCraneNO();
            BindCubicle();
            BindMatCode();
            this.Deactivate += new EventHandler(frmClose);
        } 
        #endregion

        #region 绑定下拉框
        /// <summary>
        /// 绑定行车
        /// </summary>
        /// <param name="cmbBox"></param>
        private void BindCraneNO()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TypeValue");
            dt.Columns.Add("TypeName");
            DataRow dr;
            dr = dt.NewRow();
            dr["TypeValue"] = "0";
            dr["TypeName"] = "请选择行车";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["TypeValue"] = "1";
            dr["TypeName"] = "1#行车";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["TypeValue"] = "2";
            dr["TypeName"] = "2#行车";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["TypeValue"] = "3";
            dr["TypeName"] = "3#行车";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["TypeValue"] = "4";
            dr["TypeName"] = "4#行车";
            dt.Rows.Add(dr);
            this.cmb_CraneNO.DataSource = dt;
            this.cmb_CraneNO.DisplayMember = "TypeName";
            this.cmb_CraneNO.ValueMember = "TypeValue";
            this.cmb_CraneNO.SelectedIndex = 0;
        }
        /// <summary>
        /// 绑定工位
        /// </summary>
        private void BindCubicle()
        {
            var carneNo = cmb_CraneNO.SelectedValue.ToString().Trim();
            DataTable dt = new DataTable();
            dt.Columns.Add("TypeValue");
            dt.Columns.Add("TypeName");
            DataRow dr;
            dr = dt.NewRow();
            dr["TypeValue"] = "0";
            dr["TypeName"] = "请选择工位";
            dt.Rows.Add(dr);
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
            this.cmb_Cubicle.DataSource = dt;
            this.cmb_Cubicle.DisplayMember = "TypeName";
            this.cmb_Cubicle.ValueMember = "TypeValue";
            var index = 0;
            if (!string.IsNullOrEmpty(carneNo))
            {
                if (carneNo.Equals("1")) { index = 1; }
                else if (carneNo.Equals("2")) { index = 2; }
                else if (carneNo.Equals("3")) { index = 3; }
                else if (carneNo.Equals("4")) { index = 4; }
                else { index = 0; }
            }
            this.cmb_Cubicle.SelectedIndex = index;
        }
        /// <summary>
        /// 绑定物料
        /// </summary>
        /// <param name="cmbBox"></param>
        private void BindMatCode()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("TypeValue");
                dt.Columns.Add("TypeName");
                DataRow dr = dt.NewRow();
                //dr = dt.NewRow();
                //dr["TypeValue"] = "0";
                //dr["TypeName"] = "请选择物料";
                //dt.Rows.Add(dr);
                //string sqlText = @"SELECT MAT_CODE, BASE_RESOURCE, MAT_CNAME, MAT_TYPE, MAT_MODE, MAT_PRIORITY FROM UACSAPP.UACS_L3_MAT_INFO ORDER BY MAT_PRIORITY ";
                //using (IDataReader rdr = DBHelper.ExecuteReader(sqlText))
                //{
                //    while (rdr.Read())
                //    {
                //        dr = dt.NewRow();
                //        dr["TypeValue"] = rdr["MAT_CODE"].ToString();
                //        dr["TypeName"] = rdr["MAT_CNAME"].ToString();
                //        dt.Rows.Add(dr);
                //    }
                //}

                dr["TypeValue"] = "LCNZH";
                dr["TypeName"] = "厂内中混废";
                dt.Rows.Add(dr);

                dr = dt.NewRow();
                dr["TypeValue"] = "LWGZH";
                dr["TypeName"] = "外购中混废";
                dt.Rows.Add(dr);

                dr = dt.NewRow();
                dr["TypeValue"] = "LWGPY";
                dr["TypeName"] = "破碎料压块";
                dt.Rows.Add(dr);
                this.cmb_MatCode.ValueMember = "TypeValue";
                this.cmb_MatCode.DisplayMember = "TypeName";
                this.cmb_MatCode.DataSource = dt;
                //根据text值选中项
                this.cmb_MatCode.SelectedIndex = 0;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// 行车值发生改变时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cm_CraneNO_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCubicle();
        }

        #endregion

        #region 数据处理

        /// <summary>
        /// 确认清扫
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_Confirm_Click(object sender, EventArgs e)
        {
            isPopupMessage = true;
            if (string.IsNullOrEmpty(cmb_CraneNO.Text.Trim()) || cmb_CraneNO.SelectedValue.ToString().Trim().Equals("0"))
            {
                MessageBox.Show("请选择行车!");
                return;
            }
            if (string.IsNullOrEmpty(cmb_Cubicle.Text.Trim()) || cmb_Cubicle.SelectedValue.ToString().Trim().Equals("0"))
            {
                MessageBox.Show("请选择清扫工位!");
                return;
            }
            if (string.IsNullOrEmpty(cmb_MatCode.Text.Trim()) || cmb_MatCode.SelectedValue.ToString().Trim().Equals("0"))
            {
                MessageBox.Show("请选择物料!");
                return;
            }
            if (string.IsNullOrEmpty(txt_Height.Text.Trim()))
            {
                MessageBox.Show("重量信息不正确!");
                return;
            }
            DialogResult dr = MessageBox.Show("请确认料槽车已经离开工位,是否开始清扫？", "操作提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
            if (dr == DialogResult.OK)
            {
                string sql = @"UPDATE UACS_CRANE_MANU_ORDER_CLEAN SET MAT_NO = '" + cmb_MatCode.SelectedValue.ToString().Trim() + "', FROM_STOCK = '" + cmb_Cubicle.SelectedValue.ToString().Trim() + "', TO_STOCK = '" + cmb_Cubicle.SelectedValue.ToString().Trim() + "',PLAN_UP_Z = '"+ txt_Height.Text.Trim()+"', STATUS = 'INIT' ";
                sql += " WHERE BAY_NO = 'A' AND CRANE_NO = '" + cmb_CraneNO.SelectedValue.ToString().Trim() + "' ";
                DBHelper.ExecuteNonQuery(sql);
                //清扫按钮闪烁
                //ALM.UpdaeCubicleClean(cmb_CraneNO.SelectedValue.ToString().Trim());
                HMILogger.WriteLog("清扫", cmb_CraneNO.Text + "清扫，工位：" + cmb_Cubicle.Text, LogLevel.Info, this.Text);
                UpdateStatus(cmb_CraneNO.SelectedValue.ToString().Trim(), "41");
                MessageBox.Show("已选择清扫，请切换自动模式！");
                this.Close();
            }
            else
            {
                return;
            }
            isPopupMessage = false;
        }
        /// <summary>
        /// 清扫完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFinish_Click(object sender, EventArgs e)
        {
            isPopupMessage = true;
            if (string.IsNullOrEmpty(cmb_CraneNO.Text.Trim()) || cmb_CraneNO.SelectedValue.ToString().Trim().Equals("0"))
            {
                MessageBox.Show("请选择行车!");
                return;
            }
            DialogResult dr = MessageBox.Show("是否清扫完成？", "操作提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
            if (dr == DialogResult.OK)
            {
                string sql = @"UPDATE UACS_CRANE_MANU_ORDER_CLEAN SET MAT_NO = null, FROM_STOCK = null, TO_STOCK = null, STATUS = 'EMPTY' ";
                sql += " WHERE BAY_NO = 'A' AND CRANE_NO = '" + cmb_CraneNO.SelectedValue.ToString().Trim() + "' ";
                DBHelper.ExecuteNonQuery(sql);
                //取消清扫按钮闪烁
                //ALM.CancelCubicleClean(cmb_CraneNO.SelectedValue.ToString().Trim());
                HMILogger.WriteLog("清扫完成", cmb_CraneNO.Text + "清扫完成", LogLevel.Info, this.Text);
                UpdateStatus(cmb_CraneNO.SelectedValue.ToString().Trim(), "99");
                MessageBox.Show("清扫完成，请清空行车指令！");
                this.Close();
            }
            isPopupMessage = false;
        }
        /// <summary>
        /// 更新行车检修状态
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

        #endregion

        #region 关闭窗体
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

        private void cmb_MatCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            var matCode = cmb_MatCode.SelectedValue.ToString().Trim();
            if (matCode.Equals("LCNZH"))
            {
                txt_Height.Text = "400";
            } 
            else if (matCode.Equals("LWGZH")) 
            {
                txt_Height.Text = "800";
            }
            else if (matCode.Equals("LWGPY"))
            {
                txt_Height.Text = "1000";
            }
            else
            {
                txt_Height.Text = "400";
            }
        }

        private void txt_Height_KeyPress(object sender, KeyPressEventArgs e)
        {
            //如果输入的不是退格和数字，则屏蔽输入
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9')))
            {
                e.Handled = true;
            }
        }
    }
}
