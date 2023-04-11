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
using Baosight.iSuperframe.TagService;
using Baosight.iSuperframe.TagService.Controls;
using System.Reflection;

namespace UACSPopupForm.CraneMonitor
{
    /// <summary>
    /// 矫正高度
    /// </summary>
    public partial class FrmCorrectionHeight : Form
    {
        Baosight.iSuperframe.TagService.DataCollection<object> TagValues = new DataCollection<object>();
        private Baosight.iSuperframe.TagService.Controls.TagDataProvider tagDP = new Baosight.iSuperframe.TagService.Controls.TagDataProvider();
        /// <summary>
        /// 行车号
        /// </summary>
        public string CraneNo { get; set; }
        /// <summary>
        /// Tag
        /// </summary>
        private string tag_CraneMode = "";
        /// <summary>
        /// 防止弹出信息关闭画面
        /// </summary>
        private bool isPopupMessage = false;
        public FrmCorrectionHeight()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 初始加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmCorrectionHeight_Load(object sender, EventArgs e)
        {
            tagDP.ServiceName = "iplature";
            tagDP.AutoRegist = true;
            TagValues.Clear();
            TagValues.Add(tag_CraneMode, null);
            tagDP.Attach(TagValues);
            BindCraneNO(cmCraneNO);
            this.Deactivate += new EventHandler(frmClose);
        }

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
            cmbBox.DataSource = dt;
            cmbBox.DisplayMember = "TypeName";
            cmbBox.ValueMember = "TypeValue";
            var index = 0;
            if (!string.IsNullOrEmpty(CraneNo))
            {
                if (CraneNo.Equals("1")) { index = 1; }
                else if (CraneNo.Equals("2")) { index = 2; }
                else if (CraneNo.Equals("3")) { index = 3; }
                else if (CraneNo.Equals("4")) { index = 4; }
            }
            cmbBox.SelectedIndex = index;
        }
        /// <summary>
        /// 确认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_Submit_Click(object sender, EventArgs e)
        {
            isPopupMessage = true;
            if (string.IsNullOrEmpty(CraneNo))
            {
                MessageBox.Show("行车错误！");
                return;
            }
            if (cmCraneNO.SelectedValue.ToString().Trim().Equals("0"))
            {
                MessageBox.Show("请选择行车!");
                return;
            }
            MessageBoxButtons btn = MessageBoxButtons.OKCancel;
            DialogResult drmsg = MessageBox.Show("确认是否矫正高度？", "提示", btn, MessageBoxIcon.Asterisk);
            if (drmsg == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(txt_Password.Text.Trim()))
                {

                    if (txt_Password.Text.Trim().Equals("123"))
                    {
                        var tagValue = CraneNo + ",20";
                        tag_CraneMode = CraneNo + "_DownLoadShortCommand";
                        tagDP.SetData(tag_CraneMode, tagValue);                        
                        ParkClassLibrary.HMILogger.WriteLog("矫正高度", CraneNo + "行车矫正高度为：" + tagValue, ParkClassLibrary.LogLevel.Info, "主监控");
                        MessageBox.Show("矫正高度成功！");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("密码错误！");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("密码不能为空！");
                    return;
                }
            }
            isPopupMessage = false;
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
    }
}
