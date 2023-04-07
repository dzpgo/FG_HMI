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

namespace UACSView.View_CraneMonitor
{
    /// <summary>
    /// 检修
    /// </summary>
    public partial class Recondition : Form
    {
        Baosight.iSuperframe.TagService.DataCollection<object> TagValues = new DataCollection<object>();
        private Baosight.iSuperframe.TagService.Controls.TagDataProvider tagDP = new Baosight.iSuperframe.TagService.Controls.TagDataProvider();
        public Z01_library_Monitor ALM;
        public Recondition()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 添加一个构造函数
        /// </summary>
        /// <param name="form"></param>
        public Recondition(Z01_library_Monitor form) : this()
        {
            ALM = form;
        }
        private string craneNo;
        private string bayNO;
        private string recondition = "";
        /// <summary>
        /// 行车号
        /// </summary>
        public string CraneNo { get => craneNo; set => craneNo = value; }
        /// <summary>
        /// 跨别
        /// </summary>
        public string BayNO { get => bayNO; set => bayNO = value; }
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
            if (bayNO == "A")
            {
                dr = dt.NewRow();
                dr["TypeValue"] = "1";
                dr["TypeName"] = "1#车";
                dt.Rows.Add(dr);

                dr = dt.NewRow();
                dr["TypeValue"] = "2";
                dr["TypeName"] = "2#车";
                dt.Rows.Add(dr);

                dr = dt.NewRow();
                dr["TypeValue"] = "3";
                dr["TypeName"] = "3#车";
                dt.Rows.Add(dr);
                dr = dt.NewRow();
                dr["TypeValue"] = "4";
                dr["TypeName"] = "4#车";
                dt.Rows.Add(dr);
            }
            //var index = Convert.ToInt32(CraneNo) -1;
            cmbBox.DataSource = dt;
            cmbBox.DisplayMember = "TypeName";
            cmbBox.ValueMember = "TypeValue";
            cmbBox.SelectedIndex = 0;

        }
        /// <summary>
        /// 确定检修
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmCraneNO.Text.Trim()) && !string.IsNullOrEmpty(txt_Act_X.Text.Trim())&& Convert.ToInt32(txt_Act_X.Text) > 0 && Convert.ToInt32(txt_Act_X.Text) < 478000)
            {
                recondition = cmCraneNO.SelectedValue.ToString().Trim() + "_CraneStop";
                DialogResult dr = MessageBox.Show("是否开始检修？", "操作提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                if (dr == DialogResult.OK)
                {
                    tagDP.SetData(recondition, txt_Act_X.Text);

                    #region 检修时更改行车背景颜色
                    if (bayNO.Equals("A"))
                    {
                        ALM.UpdataCrane(cmCraneNO.SelectedValue.ToString().Trim());
                    }
                    //else if (bayNO.Equals("C"))
                    //{
                    //    ALM.UpdataCrane(cmCraneNO.SelectedValue.ToString().Trim());
                    //}
                    #endregion
                    UpdateStatus(cmCraneNO.SelectedValue.ToString().Trim(), "51");
                }
                else
                {
                    return;
                }
                HMILogger.WriteLog("检修", cmCraneNO.Text + "确认检修，X坐标："+ txt_Act_X.Text, LogLevel.Info, this.Text);
            }
            else
            {
                MessageBox.Show("请填写完整！");
            }
        }
        /// <summary>
        /// 检修完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFinish_Click(object sender, EventArgs e)
        {
            if (cmCraneNO.Text != "")
            {
                recondition = cmCraneNO.SelectedValue.ToString().Trim() + "_CraneStop";
                DialogResult dr = MessageBox.Show("确认检修完成？", "操作提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                if (dr == DialogResult.OK)
                {
                    tagDP.SetData(recondition, "0");

                    #region 检修完成时更改行车背景颜色
                    if (bayNO.Equals("A"))
                    {
                        ALM.OutCrane(cmCraneNO.SelectedValue.ToString().Trim());
                    }
                    //else if (bayNO.Equals("C"))
                    //{
                    //    ALM.OutCrane(cmCraneNO.SelectedValue.ToString().Trim());
                    //}
                    #endregion
                    UpdateStatus(cmCraneNO.SelectedValue.ToString().Trim(), "99");
                }
                else
                {
                    return;
                }
                HMILogger.WriteLog("检修", cmCraneNO.Text + "检修完成，X坐标：" + txt_Act_X.Text, LogLevel.Info, this.Text);
            }
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

        /// <summary>
        /// 取消 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
