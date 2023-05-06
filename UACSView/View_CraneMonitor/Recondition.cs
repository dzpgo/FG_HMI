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

namespace UACSView.View_CraneMonitor
{
    /// <summary>
    /// 检修
    /// </summary>
    public partial class Recondition : Form
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
            ReconditionCraneX();
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
            if (bayNO == "A")
            {
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
            isPopupMessage = true;
            if (cmCraneNO.SelectedValue.ToString().Trim().Equals("0"))
            {
                MessageBox.Show("请选择行车!");
                return;
            }
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
                    HMILogger.WriteLog("检修", cmCraneNO.Text + "确认检修，X坐标：" + txt_Act_X.Text, LogLevel.Info, this.Text);
                    //MessageBox.Show("检修状态中！");
                    this.Close();
                }
                else
                {
                    return;
                }
                
            }
            else
            {
                MessageBox.Show("请填写完整！");
            }
            isPopupMessage = false;
        }
        /// <summary>
        /// 检修完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFinish_Click(object sender, EventArgs e)
        {
            isPopupMessage = true;
            if (cmCraneNO.Text != "")
            {
                if (cmCraneNO.SelectedValue.ToString().Trim().Equals("0"))
                {
                    MessageBox.Show("请选择行车!");
                    return;
                }
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
                    this.Close();
                }
                else
                {
                    return;
                }
                HMILogger.WriteLog("检修", cmCraneNO.Text + "检修完成，X坐标：" + txt_Act_X.Text, LogLevel.Info, this.Text);
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
        /// 值发生改变时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmCraneNO_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReconditionCraneX();
        }
        /// <summary>
        /// 默认行车当前位置
        /// </summary>
        private void ReconditionCraneX()
        {
            isPopupMessage = true;
            var craneNo = cmCraneNO.SelectedValue.ToString().Trim();
            if (craneNo.Equals("1"))
            {
                if (!string.IsNullOrEmpty(CraneA1_X))
                {
                    //string bbb = CraneA1_X.Replace(",", "");
                    txt_Act_X.Text = CraneA1_X.Replace(",", "");
                }
                else
                {
                    txt_Act_X.Text = "";
                }
            }
            else if (craneNo.Equals("2"))
            {
                if (!string.IsNullOrEmpty(CraneA2_X))
                {
                    txt_Act_X.Text = CraneA2_X.Replace(",", "");
                }
                else
                {
                    txt_Act_X.Text = "";
                }
            }
            else if (craneNo.Equals("3"))
            {
                if (!string.IsNullOrEmpty(CraneA3_X))
                {
                    txt_Act_X.Text = CraneA3_X.Replace(",", "");
                }
                else
                {
                    txt_Act_X.Text = "";
                }
            }
            else if (craneNo.Equals("4"))
            {
                if (!string.IsNullOrEmpty(CraneA4_X))
                {
                    txt_Act_X.Text = CraneA4_X.Replace(",", "");
                }
                else
                {
                    txt_Act_X.Text = "";
                }
            }
            isPopupMessage = false;
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
