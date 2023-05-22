using Baosight.iSuperframe.TagService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ParkClassLibrary;
using UACSDAL;
using UACSView.CLTSHMI;
using UACSParking;

namespace UACSView.View_CraneMonitor
{
    /// <summary>
    /// 装冷却剂
    /// </summary>
    public partial class Coolant : Form
    {
        #region 全局变量
        Baosight.iSuperframe.TagService.DataCollection<object> TagValues = new DataCollection<object>();
        private Baosight.iSuperframe.TagService.Controls.TagDataProvider tagDP = new Baosight.iSuperframe.TagService.Controls.TagDataProvider();
        public Z01_library_Monitor ALM;
        private string craneNo;
        private string bayNO;
        private string recondition = "";
        /// <summary>
        /// 1号行车X坐标
        /// </summary>
        public string CraneA1_X { get; set; }
        /// <summary>
        /// 2号行车X坐标
        /// </summary>
        public string CraneA2_X { get; set; }
        /// <summary>
        /// 3号行车X坐标
        /// </summary>
        public string CraneA3_X { get; set; }
        /// <summary>
        /// 4号行车X坐标
        /// </summary>
        public string CraneA4_X { get; set; }
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

        public Coolant()
        {
            InitializeComponent();            
        }
        /// <summary>
        /// 添加一个构造函数
        /// </summary>
        /// <param name="form"></param>
        public Coolant(Z01_library_Monitor form) : this()
        {
            ALM = form;
        }

        #region 初始化
        /// <summary>
        /// 初始化加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Recondition_Load(object sender, EventArgs e)
        {
            tagDP.ServiceName = "iplature";
            tagDP.AutoRegist = true;
            TagValues.Clear();
            TagValues.Add(recondition, null);
            tagDP.Attach(TagValues);
            BindCraneNO(cmCraneNO);
            BindFromStock(cmb_FromStock);
            //ReconditionCraneX();
            this.Deactivate += new EventHandler(frmClose);
        } 
        #endregion

        #region 绑定下拉框
        /// <summary>
        /// 绑定行车
        /// </summary>
        /// <param name="cmbBox"></param>
        private void BindCraneNO(ComboBox cmbBox)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TypeValue");
            dt.Columns.Add("TypeName");
            DataRow dr;
            if (bayNO == "A")
            {
                //dr = dt.NewRow();
                //dr["TypeValue"] = "0";
                //dr["TypeName"] = "请选择行车";
                //dt.Rows.Add(dr);

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
            }
            //var index = Convert.ToInt32(CraneNo) -1;
            cmbBox.DataSource = dt;
            cmbBox.DisplayMember = "TypeName";
            cmbBox.ValueMember = "TypeValue";
            cmbBox.SelectedIndex = 0;
        }
        /// <summary>
        /// 绑定行车
        /// </summary>
        /// <param name="cmbBox"></param>
        private void BindFromStock(ComboBox cmbBox)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TypeValue");
            dt.Columns.Add("TypeName");
            DataRow dr;
            if (bayNO == "A")
            {
                dr = dt.NewRow();
                dr["TypeValue"] = "01-3";
                dr["TypeName"] = "01-3";
                dt.Rows.Add(dr);
                dr = dt.NewRow();
                dr["TypeValue"] = "01-4";
                dr["TypeName"] = "01-4";
                dt.Rows.Add(dr);
                dr = dt.NewRow();
                dr["TypeValue"] = "01-5";
                dr["TypeName"] = "01-5";
                dt.Rows.Add(dr);
            }
            //var index = Convert.ToInt32(CraneNo) -1;
            cmbBox.DataSource = dt;
            cmbBox.DisplayMember = "TypeName";
            cmbBox.ValueMember = "TypeValue";
            cmbBox.SelectedIndex = 0;
        }
        #endregion

        #region 按钮点击
        /// <summary>
        /// 开始装冷却剂
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            isPopupMessage = true;
            if (string.IsNullOrEmpty(cmCraneNO.Text.Trim()))
            {
                MessageBox.Show("请选择行车!");
                return;
            }
            if (string.IsNullOrEmpty(txt_MatWeight.Text.Trim()) || Convert.ToInt32(txt_MatWeight.Text.Trim()) <= 0)
            {
                MessageBox.Show("请输入正确重量!");
                return;
            }
            DialogResult dr = MessageBox.Show("是否开始装冷却剂？", "操作提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
            if (dr == DialogResult.OK)
            {
                UpdateStatusInit(cmCraneNO.SelectedValue.ToString().Trim(), txt_MatWeight.Text.ToString().Trim(), cmb_FromStock.SelectedValue.ToString().Trim(), "INIT");
                HMILogger.WriteLog("装冷却剂", cmCraneNO.Text + "开始装冷却剂：" + txt_MatWeight.Text, LogLevel.Info, this.Text);
                UpdateStatus(cmCraneNO.SelectedValue.ToString().Trim(), "42");
                this.Close();
            }
            else
            {
                return;
            }
            isPopupMessage = false;
        }
        /// <summary>
        /// 结束装冷却剂
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFinish_Click(object sender, EventArgs e)
        {
            isPopupMessage = true;
            if (string.IsNullOrEmpty(cmCraneNO.Text.Trim()))
            {
                MessageBox.Show("请选择行车!");
                return;
            }
            DialogResult dr = MessageBox.Show("是否结束装冷却剂？", "操作提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
            if (dr == DialogResult.OK)
            {
                UpdateStatusEmpty(cmCraneNO.SelectedValue.ToString().Trim());
                HMILogger.WriteLog("装冷却剂", cmCraneNO.Text + "结束装冷却剂：" + txt_MatWeight.Text, LogLevel.Info, this.Text);
                UpdateStatus(cmCraneNO.SelectedValue.ToString().Trim(), "99");
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
        /// 更新装冷却剂
        /// </summary>
        /// <param name="craneNo">行车号</param>
        /// <param name="MatWeight">重量</param>
        /// <param name="FromStock">料堆位置</param>
        /// <param name="Status">状态 空：EMPTY 初始化：INIT</param>
        private void UpdateStatusInit(string craneNo, string MatWeight,string FromStock,string Status)
        {
            try
            {
                string ExeSql = @"UPDATE UACS_CRANE_MANU_ORDER_LBC19 SET MAT_WEIGHT = '" + MatWeight + "' ,FROM_STOCK = '" + FromStock + "' ,STATUS = '" + Status + "' WHERE CRANE_NO = '" + craneNo + "'";
                DB2Connect.DBHelper.ExecuteNonQuery(ExeSql);
            }
            catch (Exception)
            {
            }
        }
        /// <summary>
        /// 结束装冷却剂
        /// </summary>
        /// <param name="craneNo">行车号</param>
        private void UpdateStatusEmpty(string craneNo)
        {
            try
            {
                string ExeSql = @"UPDATE UACS_CRANE_MANU_ORDER_LBC19 SET MAT_WEIGHT = null, FROM_STOCK = '01-0', STATUS = 'EMPTY' ";
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

        private void txt_MatWeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            //如果输入的不是退格和数字，则屏蔽输入
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9')))
            {
                e.Handled = true;
            }
        }
    }
}
